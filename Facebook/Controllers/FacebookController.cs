using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;

namespace Facebook.Controllers
{
    public class FacebookController : Controller
    {
        private FacebookDatabaseEntities db = new FacebookDatabaseEntities();

        public ActionResult Login()
        {
            Session["user"] = null;
            Session["id"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind (Include ="Email,Password")] User user)
        {
            var data = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password).ToList().FirstOrDefault();
            if(data != null)
            {
                Session["user"] = data.Fname;
                Session["id"] = data.Id;
                return RedirectToAction("Home","User",new {id = data.Id });
            }
            else
            {
                ViewBag.error = "Invalid user Try Again";
                return View(user);
            }
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Login");
        }

    }
}