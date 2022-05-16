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
using Microsoft.Ajax.Utilities;

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
                    photo.pphoto = path;
                    photo.postID = post.Id;
                    db.PostPhotos.Add(photo);
                }
                else if(post.content == null)
                {
                    ModelState.AddModelError("content","There is no Photo or Text Content!!");
                    return View(post);
                }


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
        public ActionResult Edit(Post post, PostPhoto photo, HttpPostedFileBase imgFile)
        {
            var before = db.Posts.AsNoTracking().Where(id => id.Id == post.Id).ToList().FirstOrDefault();
            var photoForPost = db.PostPhotos.Where(p => p.postID == post.Id).ToList();

            string path = "";

            if (post.content == null && photoForPost == null)
            {

                ModelState.AddModelError("content", "There is no Photo or Text Content!!");
                return View(post);
            }
            else
            {
                if (imgFile != null && imgFile.FileName.Length > 0)
                {
                    path = "~/Images/" + Path.GetFileName(imgFile.FileName);
                    imgFile.SaveAs(Server.MapPath(path));
                    photo.pphoto = path;
                    photo.postID = post.Id;
                    db.PostPhotos.Add(photo);
                }
                post.date = before.date;
                post.userID = before.userID;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = post.userID});
            }
                
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
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
                return View(post);
            }
            else
            {
                return RedirectToAction("Index", new { id = Session["id"] });
            }
   
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            var deletephoto =
            from photos in db.PostPhotos
            where photos.postID == post.Id
            select photos;
            
            foreach(var photo in deletephoto)
            {
                db.PostPhotos.Remove(photo);
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["id"] });
        }

        //public ActionResult AddComment(Comment newcomment)
        //{
        //    return JsonResult{ };
        //}

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
