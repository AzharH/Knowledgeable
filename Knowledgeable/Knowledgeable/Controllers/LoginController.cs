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

        // GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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

                    string name = register.Name;
                    string Subject = "Email Confirmation";
                    string mailContent = "<p>Thank you for your registration. Click on the link below to confirm your account.</p> <a href=\"http://localhost:23060/ConfirmEmail/" + user.UserID + "\"></a>";


                    Utility.SendMail(name, user.Email, Subject, mailContent);


                }
                else
                {
                    ViewBag.Error = "Email already exists.";
                    return View();

                }

            }
            return View();
        }

        // GET: Login

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(x => x.Email == login.Email).FirstOrDefault();
                if (user.Active)
                {
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
            }
            login.Password = "";//redirect to main page
            return View(login);
        }

        [AllowAnonymous]
        public ActionResult Reset()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Reset(ResetModel resetModel)
        {

            User user = db.Users.Where(x => x.Email == resetModel.Email).FirstOrDefault();
            if (user != null)
            {

                ResetPassword resetPassword = db.ResetPasswords.Find(user.UserID);
                if (resetPassword == null)
                {
                    resetPassword = new ResetPassword();
                    resetPassword.UserID = user.UserID;
                    resetPassword.ResetID = Guid.NewGuid();
                    db.ResetPasswords.Add(resetPassword);
                }
                else
                {
                    resetPassword.ResetID = Guid.NewGuid();
                    db.Entry(resetPassword).State = EntityState.Modified;
                }
                db.SaveChanges();

                string name = user.Name;
                string Subject = "Password reset";
                string mailContent = "<p>Your password was requested to be reset. Click on the link below to reset your password.</p> <a href=\"http://localhost:23060/resetpassword/" + resetPassword + "\"></a>";

                Utility.SendMail(name, user.Email, Subject, mailContent);
            }
            else
            {
                ViewBag.error = "Email does not exists.";
                return View();
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult EmailConfirmed(Guid UserID)
        {
            if (UserID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                User user = db.Users.Find(UserID);
                if(user != null)
                {
                    user.Active = true;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return View("");
                }
                else
                {
                    return RedirectToAction("Register");
                }
                
            }
        }


        [AllowAnonymous]
        public ActionResult ResetPassword(Guid resetPasswordID)
        {

            ResetPassword resetPassword = db.ResetPasswords.Where(x => x.ResetID == resetPasswordID).FirstOrDefault();
            if(resetPassword != null)
            {
                User user = db.Users.Find(resetPassword.UserID);
                UserRegModel userRegModel = new UserRegModel();
                userRegModel.UserID = user.UserID;
                userRegModel.Email = user.Email;
                return View(userRegModel);
            }
            else
            {
                return RedirectToAction("Register");
            }

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(UserRegModel userRegModel)
        {


            string salt = BCrypt.Net.BCrypt.GenerateSalt(4);
            string hashed1 = BCrypt.Net.BCrypt.HashPassword(userRegModel.Password, salt);
            string hashed2 = BCrypt.Net.BCrypt.HashPassword(userRegModel.Password, hashed1);

            User user = db.Users.Find(userRegModel.UserID);
            user.Password = hashed2;
            user.Salt = salt;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            ResetPassword resetPassword = db.ResetPasswords.Find(user.UserID);
            db.ResetPasswords.Remove(resetPassword);
            db.SaveChanges();

            return RedirectToAction("Login");
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
