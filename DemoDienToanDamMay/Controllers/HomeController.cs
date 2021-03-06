using DemoDienToanDamMay.Enity;
using DemoDienToanDamMay.MoreOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDienToanDamMay.Controllers
{
    public class HomeController : Controller
    {
        static Model1 db = new Model1();
        private void UpdateHisoty(string history)
        {
            var x = (List<string>)Session["history"];
            if (x == null)
                Session["history"] = new List<string>() { history };
            else
            {
                x.Add(history);
                Session["history"] = x;
            }
        }
        public ActionResult RemoveFolder()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login");
            if (Session["folderName"] != null)
            {
                var f = db.FolderByUsers.ToList().Where(i => i.FolderName == Session["folderName"].ToString()).FirstOrDefault();
                var x = db.FileImgs.ToList().Where(i => i.FolderName == f.FolderName).ToList();
                foreach (var item in x)
                {
                    db.FileImgs.Remove(item);
                }
                db.FolderByUsers.Remove(f);
                Session["folderName"] = null;
                db.SaveChanges();
            }
            return Redirect("Index");
        }
        public ActionResult Index(string err = null)
        {
            if (Session["code"] == null)
                return RedirectToAction("Login");
            ViewBag.ERR = err;
            ViewBag.Folders = db.FolderByUsers.ToList().Where(i => i.Email.Contains(Session["email"].ToString())).ToList();
            if (Session["folderName"] != null)
                ViewBag.Files = db.FileImgs.ToList().Where(i => i.FolderName.Contains(Session["folderName"].ToString()));

            return View();
        }

        [HttpPost]
        public ActionResult CreateFolder(string question, string answer, string folderName)
        {
            if (FileFolder.CreateFolder(Session["email"].ToString(), folderName))
            {
                db.FolderByUsers.Add(new FolderByUser()
                {
                    FolderName = folderName,
                    Question = question,
                    Answer = answer,
                    Email = Session["email"].ToString()

                });
                db.SaveChanges();
                UpdateHisoty($"CreateFolder {folderName}");
            }
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Unlock(string question, string answer, string folderName)
        {
            if (db.FolderByUsers.ToList().Where(i => i.Email.Contains(Session["email"].ToString()) && i.Question.Contains(question) && i.Answer.Contains(answer) && i.FolderName.Contains(folderName)).FirstOrDefault() != null)
            {
                Session["folderName"] = folderName;
                UpdateHisoty($"Unlock {folderName} success");
                return Redirect($"index?err=Unlock folder {folderName}");
            }
            UpdateHisoty($"Unlock {folderName} false");
            return Redirect("Index?err=answer incorrect");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult UploadFile()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login");
            else
            {
                if (Session["folderName"] != null)
                    return View();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Dowload(HttpPostedFileBase fileKey, HttpPostedFileBase fileEncryption, string code)
        {
            try
            {
                var c = db.LoginEmails.ToList().Where(i => i.Email.Contains(Session["email"].ToString())).FirstOrDefault().Code;
                if (c == code)
                {
                    string key = FileFolder.ReadFile(fileKey);
                    string encrypt = FileFolder.ReadFile(fileEncryption);
                    string decrypt = ED.DecryptString(encrypt, key);
                    var path = db.FileImgs.ToList().Where(i => i.KeyImg.Contains(key) && i.ValueImg.Contains(decrypt)).FirstOrDefault();
                    if (path != null)
                    {
                        Session["pathImg"] = path.ValueImg;
                        return RedirectToAction("Dowload", "File");
                    }
                }
                else
                {
                    SendMail.Send(Session["email"].ToString(), c, false);
                    return Redirect("Index?Err=Wrong activation code.We have sent a new code to your email");
                }

            }
            catch
            {
            }
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult UploadFile(IEnumerable<HttpPostedFileBase> file1)
        {
            try
            {
                foreach (var item in file1)
                {
                    var path = FileFolder.GetPathImg(Session["email"].ToString(), Session["folderName"].ToString(), item.FileName);
                    var filePathImg = $"{path}\\{item.FileName}";
                    var filePathKey = path + ".key";
                    var filePathEnc = path + ".enc";
                    var key = ED.GeneraKey(Session["email"].ToString() + Session["folderName"].ToString());
                    var entry = ED.EncryptString(filePathImg, key);

                    FileFolder.WirteFile(filePathKey, key);
                    FileFolder.WirteFile(filePathEnc, entry);
                    UpdateHisoty($"UploadFile {item.FileName} success");
                    if (db.FileImgs.ToList().Where(i => i.FileName.Contains(item.FileName) && i.FolderName.Contains(Session["folderName"].ToString())).FirstOrDefault() == null)
                    {
                        item.SaveAs(filePathImg);
                        db.FileImgs.Add(new FileImg()
                        {
                            KeyImg = key,
                            ValueImg = filePathImg,
                            FileName = item.FileName,
                            FolderName = Session["folderName"].ToString()
                        });
                    }
                    Session["pathKey"] = filePathKey;
                    Session["pathEnc"] = filePathEnc;
                }
                db.SaveChanges();
                return RedirectToAction("Dowload", "File");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "File");
            }
        }

        public ActionResult Login(string err = null)
        {
            ViewBag.Title = "Login";
            ViewBag.ERR = err;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.LoginEmails.Where(i => i.Email.Contains(email) && i.PassWords.Contains(password)).FirstOrDefault();
            if (user != null)
            {
                Session["code"] = user.Code;
                Session["email"] = email;
                UpdateHisoty($"Login by {Session["email"].ToString()}");
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("Login?err=Login information is incorrect");
            }
        }

        public ActionResult Logup(string err = null)
        {
            ViewBag.Title = "Register";
            ViewBag.ERR = err;
            return View();
        }

        [HttpPost]
        public ActionResult Logup(string email, string password)
        {
            if (db.LoginEmails.Where(i => i.Email == email).FirstOrDefault() != null)
            {
                return Redirect("Logup?err=Account already exists");
            }
            else
            {
                var code = ED.GeneraCode();
                if (SendMail.Send(email, code))
                {
                    db.LoginEmails.Add(new LoginEmail()
                    {
                        Email = email,
                        PassWords = password,
                        Code = code
                    });
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                return Redirect("Logup?err=Unable to verify account");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}