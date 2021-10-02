using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDienToanDamMay.Controllers
{
    public class FileController : Controller
    {
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
        // GET: File
        public ActionResult Index(string err = null)
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            ViewBag.ERR = err;
            return View();
        }
        public ActionResult Dowload()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }
        private ActionResult Download(string file)
        {
            var x = file.Split('\\');
            if (!System.IO.File.Exists(file))
            {
                return HttpNotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = x[x.Count() - 1],
            };
            UpdateHisoty($"Download {x[x.Count() - 1]}");
            return response;
        }
        public ActionResult DownloadKey()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return Download(Session["pathKey"].ToString());
        }
        public ActionResult DownloadEnc()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return Download(Session["pathEnc"].ToString());
        }
        public ActionResult DownloadImg()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            var x = Session["pathImg"].ToString();
            Session["pathImg"] = null;
            return Download(x);
        }

    }
}