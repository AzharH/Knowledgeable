using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Knowledgeable;
using Knowledgeable.Models;
using System.IO;

namespace Knowledgeable.Controllers
{
    public class ProfileController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        // GET: Profile
        public ActionResult Index(Guid? id)
        {
            User user = new User();
            if (id == null)
            {
                Guid UserID = new Guid(User.Identity.Name);

                user = db.Users.Find(UserID);
                
            }
            else
            {
                user = db.Users.Find(id);
            }
            ProfileModel profile = new ProfileModel();
            profile.UserID = user.UserID;
            profile.Surname = user.Surname;
            profile.Name = user.Name;
            profile.Email = user.Email;
            profile.ProfilePicture = user.ProfilePicture;

            List<Article> article = db.Articles.Where(x => x.UserID == profile.UserID).ToList();
            int countShare = 0;
            foreach (var item in article)
            {
                List<Share> share = db.Shares.Where(x => x.ArticleID == item.ArticleID).ToList();
                countShare = countShare + share.Count();
            }
            profile.NumArticles = article.Count();
            profile.NumShared = countShare;
            return View(profile);

        }

        // GET: Profile/Details/5
        public ActionResult Details(Guid? id)
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

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Email,Password,Salt,Name,Surname,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(Guid? id)
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

            ProfileModel profile = new ProfileModel();
            profile.Name = user.Name;
            profile.Surname = user.Surname;
            profile.UserID = user.UserID;
            profile.ProfilePicture = user.ProfilePicture;

            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                Guid UserID = new Guid(User.Identity.Name);
                if (UserID != profile.UserID)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(UserID);

                if (profile.PictureFile != null)
                {
                    string dirname = "~/Content/ProfilePicture";
 
                    var fileName = Path.GetFileName(profile.PictureFile.FileName);
                    int extPos = fileName.LastIndexOf('.');
                    string ext = fileName.Substring(extPos);
                    string newFileName = UserID.ToString() + ext;
                    var path = Path.Combine(Server.MapPath(dirname), (newFileName));
                    profile.PictureFile.SaveAs(path);
                    user.ProfilePicture = ext;
                }


                user.Name = profile.Name;
                user.Surname = profile.Surname;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
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
