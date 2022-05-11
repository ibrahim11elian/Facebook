using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Facebook.Models;
using System.IO;

namespace Facebook.Controllers
{
    public class ProfileController : Controller
    {
        private FacebookDatabaseEntities db = new FacebookDatabaseEntities();

        // GET: Profile
        public ActionResult Index(int id)
        {
            if (Session["user"] != null)
            {
                var posts = db.Posts.Where(post => post.userID == id).OrderByDescending(x => x.date).ToList();
                return View(posts);
            }
            else
            {
                return RedirectToAction("Login", "Facebook");
            }

            
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            if (Session["user"] != null)
            {
                //ViewBag.userID = new SelectList(db.Users, "Id", "Fname");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Facebook");
            }

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "content,userID,privacy")] Post post, PostPhoto photo, HttpPostedFileBase imgFile)
        {

            if (ModelState.IsValid)
            {
                string path = "";
                if (imgFile!= null && imgFile.FileName.Length > 0)
                {
                    path = "~/Images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                }else if(post.content == null)
                {
                    ModelState.AddModelError("content","There is no Photo or Text Content!!");
                    return View(post);
                }


                photo.pphoto = path;
                photo.postID = post.Id;
                db.PostPhotos.Add(photo);


                post.userID =int.Parse(Session["id"].ToString());
                post.date = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = post.userID });
            }

            ViewBag.userID = new SelectList(db.Users, "Id", "Fname", post.userID);
            return View(post);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Post post = db.Posts.Find(id);
                if (post == null)
                {
                    return HttpNotFound();
                }

                //ViewBag.userID = new SelectList(db.Users, "Id", "Fname", post.userID);
                return View(post);
            }
            else
            {
                return RedirectToAction("Login", "Facebook");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,content,date,userID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Users, "Id", "Fname", post.userID);
            return View(post);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
