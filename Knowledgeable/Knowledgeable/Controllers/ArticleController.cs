using Knowledgeable.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Knowledgeable.Controllers
{
    public class ArticleController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Article newarticle = db.Articles.Find(id);

                Guid UserID = new Guid(User.Identity.Name);
                var Categories = db.Categories.Where(x => x.UserID == UserID).ToList();
                ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name", newarticle.CategoryID);

                ArticleModel newarticleModel = new ArticleModel();
                newarticle.Title = newarticleModel.Title;
                newarticle.Article1 = newarticleModel.Article1;
                return View(newarticleModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ArticleModel Article)
        {
            Article newarticle = db.Articles.Find(Article.ArticleID);

            newarticle.CategoryID = Article.CategoryID;
            newarticle.Title = Article.Title;
            newarticle.DateModified = DateTime.Now;
            newarticle.Article1 = Article.Article1;

            db.Entry(newarticle).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Article
        [Authorize]
        public ActionResult Index(Guid? id)
        {
            Guid UserID = new Guid(User.Identity.Name);
            List<Article> listArticle = new List<Article>();
            if (id == null)
            {
                listArticle = db.Articles.Where(x => x.UserID == UserID).ToList();
            }
            else
            {
                listArticle = db.Articles.Where(x => x.UserID == UserID && x.CategoryID == id).ToList();
                ViewBag.CategoryID = id;
            }

            List<ArticleModel> newListArticle = new List<ArticleModel>();
            foreach (var item in listArticle)
            {
                Category category = db.Categories.Find(item.CategoryID);
                bool modified = false;
                if(item.DateModified != null)
                {
                    modified = true;
                }
                newListArticle.Add(new ArticleModel
                {
                    ArticleID = item.ArticleID,
                    UserID = item.UserID,
                    CategoryID = item.CategoryID,
                    category = category.Name,
                    Title = item.Title,
                    DatePosted = item.DatePosted,
                    DateModified = item.DateModified,
                    Modified = modified,
                    Article1 = item.Article1
                });
            }

            


            return View(newListArticle);
        }

        [Authorize]
        public ActionResult AddArticle(Guid? id)
        {
            Guid UserID = new Guid(User.Identity.Name);

            var Categories = db.Categories.Where(x=>x.UserID == UserID).ToList();

            if(id == null)
            {
                ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name","");
            }
            else
            {
                ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name", id);
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddArticle(ArticleModel Article)
        {
            Guid UserID = new Guid(User.Identity.Name);
            var Categories = db.Categories.Where(x => x.UserID == UserID).ToList();

            if ((Article.Title == null) || (Article.Title.Trim() == ""))
            {
                ViewBag.error = "Add a title to your article.";
                ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name", "");
                return View();
            }

            if (ModelState.IsValid)
            {
                Article newArticle = new Article();
                newArticle.ArticleID = Guid.NewGuid();
                newArticle.UserID = UserID;
                newArticle.CategoryID = Article.CategoryID;
                newArticle.Title = Article.Title;
                newArticle.Article1 = Article.Article1;
                newArticle.DatePosted = DateTime.Now;
                db.Articles.Add(newArticle);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name","");
            return View();
        }


        public ActionResult ViewArticle(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {


                Article article = db.Articles.Find(id);
                ArticleModel newArticle = new ArticleModel();
                newArticle.Article1 = article.Article1;
                newArticle.ArticleID = article.ArticleID;
                Category category = db.Categories.Find(article.CategoryID);
                newArticle.category = category.Name;
                newArticle.CategoryID = article.CategoryID;
                newArticle.DateModified = article.DateModified;
                newArticle.DatePosted = article.DatePosted;
                newArticle.Title = article.Title;
                return View(newArticle);
            }

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