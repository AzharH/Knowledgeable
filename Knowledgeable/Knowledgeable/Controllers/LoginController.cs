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

namespace Knowledgeable.Controllers
{
    public class LoginController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        // GET: Login
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegModel register)
        {
            if (ModelState.IsValid)
            {

                User user = new Knowledgeable.User();
                user = db.Users.Where(x => x.Email == register.Email).FirstOrDefault();
                if(user == null)
                {
                    user = new User();
                    user.UserID = Guid.NewGuid();
                    user.Email = register.Email;



                    user.Name = register.Name;
                    user.Surname = register.Surname;

                    string salt = BCrypt.Net.BCrypt.GenerateSalt(4);
                    string hashed1 = BCrypt.Net.BCrypt.HashPassword(register.Password, salt);
                    string hashed2 = BCrypt.Net.BCrypt.HashPassword(register.Password, hashed1);


                    user.Salt = salt;
                    user.Password = hashed2;
                    user.Active = false;

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("ConfirmEmail");



                }
                else
                {
                    ViewBag.Error = "Email already exists.";
                    return View();

                }

            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(x => x.Email == login.Email).FirstOrDefault();

                if (user != null)
                {

                    string hashed1 = BCrypt.Net.BCrypt.HashPassword(login.Password, user.Salt);
                    string hashed2 = BCrypt.Net.BCrypt.HashPassword(login.Password, hashed1);

                    if (hashed2 == user.Password)
                    {

                        return RedirectToAction("");

                    }
                }
            }
            login.Password = "";//redirect to main page
            return View(login);
        }



        // GET: Login/Details/5
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

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Email,Password,Name,Surname")] User user)
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

        // GET: Login/Edit/5
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
            return View(user);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Login/Delete/5
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

        // POST: Login/Delete/5
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
