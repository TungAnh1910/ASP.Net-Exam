using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExTest01.Models;

namespace ExTest01.Controllers
{
    public class LoginController : Controller
    {
        private Model1 db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenTK,string Pass)
        {
            var user = db.Users.Where(u => u.TenTK == TenTK && u.Pass == Pass).FirstOrDefault();
            if (user == null)
            {
                ViewBag.errorMsg = "Sai ten dang nhap hoac mat khau";
                return View("Login");
            }
            else
            {
                Session["TenTK"] = TenTK;
                return RedirectToAction("Index", "Management");
            }
        }
        public ActionResult Logout()
        {
            Session["TenTK"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}