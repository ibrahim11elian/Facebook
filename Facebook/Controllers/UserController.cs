using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;

namespace Facebook.Controllers
{
    public class UserController : Controller
    {
        private FacebookDatabaseEntities db = new FacebookDatabaseEntities();

        // GET: User
        public ActionResult Home(int id)
        {
            //if (Session["user"] != null){

            //var users = db.Users.Include(u => u.CommentLike).Include(u => u.PostLike);
            var user = db.Users.Where(use => use.Id == id).FirstOrDefault();
            ViewBag.user = user;
            List<Post> posts = db.Posts.Where(post => post.userID == user.Id).ToList();
                return View(posts);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Facebook");
            //}
            
        }

        public ActionResult Index()
        {
            return View(db.Users);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                string path = "";
                if(imgFile.FileName.Length > 0)
                {
                    path = "~/Images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }

                user.Photo = path;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }


        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ConfirmEmail,ConfirmPassword,Password,Photo")] User user)
        {
            var before = db.Users.AsNoTracking().Where(id => id.Id == user.Id).ToList().FirstOrDefault();
            user.Password = before.Password;
            user.ConfirmEmail = before.Email;
            user.ConfirmPassword = before.Password;
            user.Photo = before.Photo;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
