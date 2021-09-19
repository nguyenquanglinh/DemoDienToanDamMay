using DemoDienToanDamMay.Enity;
using DemoDienToanDamMay.MoreOptions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDienToanDamMay.Controllers
{
    public class HomeController : Controller
    {
        static Model1 db = new Model1();
        public ActionResult Index(string err = null)
        {
            if (Session["code"] == null)
                return RedirectToAction("Login");
            ViewBag.ERR = err;
            ViewBag.Folders = db.FolderByUsers.ToList().Where(i => i.Email.Contains(Session["email"].ToString())).ToList();
            if (Session["folderName"] != null)
                ViewBag.Files = db.FileImgs.ToList().Where(i => i.IdFolder.ToString().Contains(Session["folderName"].ToString()));
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
            }
            return Redirect("Index");
        }
        [HttpPost]
        public ActionResult Unlock(string question, string answer, string folderName)
        {
            var foder = db.FolderByUsers.ToList().Where(i => i.Email.Contains(Session["email"].ToString()) && i.Question.Contains(question) && i.Answer.Contains(answer) && i.FolderName.Contains(folderName)).FirstOrDefault();
            if (foder != null)
            {
                Session["folderName"] = foder.id;
                return Redirect($"index?err=Unlock folder {folderName}");
            }
            return Redirect("Index?err=answer incorrect");
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
        public ActionResult UploadFile(HttpPostedFileBase file1)
        {
            var x = Request.Files.Count;
            if (Session["code"] == null)
                return RedirectToAction("Login");
            return View();
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
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("Login?err=Login information is incorrect");
            }
        }
        public ActionResult Logup(string err = null)
        {
            ViewBag.Title = "Logup";
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
                if (SendMail.Send(email, db.LoginEmails.Count()))
                {
                    db.LoginEmails.Add(new LoginEmail()
                    {
                        Email = email,
                        PassWords = password,
                        Code = db.LoginEmails.Count()
                    });
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                return Redirect("Logup?err=Unable to verify account");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}