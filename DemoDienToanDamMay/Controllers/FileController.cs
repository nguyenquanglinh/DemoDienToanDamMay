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
        public ActionResult Index(string err=null)
        {
            if (Session["code"] == null)
                return RedirectToAction("Login","Home");
            ViewBag.ERR = err;
            return View();
        }
        public ActionResult Dowload()
        {
            if (Session["code"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }
    }
}