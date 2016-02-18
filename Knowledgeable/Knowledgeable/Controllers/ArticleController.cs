using Knowledgeable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knowledgeable.Controllers
{
    public class ArticleController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        // GET: Article
        public ActionResult Index()
        {
            Guid UserID = new Guid(User.Identity.Name);
            List<Article> listArticle = db.Articles.Where(x => x.UserID == UserID).ToList();

            List<ArticleModel> newListArticle = new List<ArticleModel>();
            foreach (var item in listArticle)
            {
                Category category = db.Categories.Find(item.CategoryID);
                newListArticle.Add(new ArticleModel
                {
                    ArticleID = item.ArticleID,
                    UserID = item.UserID,
                    CategoryID = item.CategoryID,
                    Title = item.Title,
                    DatePosted = item.DatePosted,
                    DateModified = item.DateModified,
                    Article1 = item.Article1
                });
            }

            


            return View(newListArticle);
        }

        public ActionResult AddArticle()
        {
            var Categories = db.Categories.ToList();
            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddArticle(ArticleModel Article)
        {
            Guid UserID = new Guid(User.Identity.Name);
            Article newArticle = new Article();
            newArticle.ArticleID = Guid.NewGuid();
            newArticle.UserID = UserID;
            newArticle.CategoryID = Article.CategoryID;
            newArticle.Title = Article.Title;
            newArticle.DatePosted = Article.DatePosted;
            newArticle.DateModified = Article.DateModified;
            newArticle.Article1 = Article.Article1;
            db.Articles.Add(newArticle);
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