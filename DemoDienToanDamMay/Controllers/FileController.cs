using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDienToanDamMay.Controllers
{
    public class FileController : Controller
    {
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
        private ActionResult Download(string file,string fileName)
        {

            if (!System.IO.File.Exists(file))
            {
                return HttpNotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = fileName,
            };
            return response;
        }
        public ActionResult DownloadKey()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return Download(Session["pathKey"].ToString(),"file.key");
        }
        public ActionResult DownloadEnc()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return Download(Session["pathEnc"].ToString(), "file.enc");
        }
        public ActionResult DownloadImg()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return Download(Session["pathImg"].ToString(),"file.img");
        }

    }
}