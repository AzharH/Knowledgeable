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
    public class CategoriesController : Controller
    {
        private KnowledgeableDBEntities db = new KnowledgeableDBEntities();

        // GET: Categories
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult LoadEditCategory()
        {
            Guid UserID = new Guid(User.Identity.Name);

            List<Category> listCategory = db.Categories.Where(x => x.UserID == UserID).OrderBy(x => x.Name).ToList();

            List<CategoryModel> newListCategory = new List<CategoryModel>();
            foreach (var item in listCategory)
            {
                Colour colour = db.Colours.Find(item.ColourID);
                newListCategory.Add(new CategoryModel
                {
                    CategoryID = item.CategoryID,
                    ColourID = item.ColourID,
                    Hex = colour.Hex,
                    Name = item.Name,
                    UserID = item.UserID
                });
            }

            return PartialView("_LoadEditCategory", newListCategory);
        }

        [Authorize]
        public PartialViewResult LoadCategory()
        {
            Guid UserID = new Guid(User.Identity.Name);

            List<Category> listCategory = db.Categories.Where(x=>x.UserID==UserID).OrderBy(x=>x.Name).ToList();

            List<CategoryModel> newListCategory = new List<CategoryModel>();
            foreach(var item in listCategory)
            {
                Colour colour = db.Colours.Find(item.ColourID);
                newListCategory.Add(new CategoryModel
                {
                    CategoryID = item.CategoryID,
                    ColourID = item.ColourID,
                    Hex = colour.Hex,
                    Name = item.Name,
                    UserID = item.UserID
                });
            }

            List<Share> share = db.Shares.Where(x => x.UserID == UserID).ToList();
            List<CategorySharedModel> categoryShared = new List<CategorySharedModel>();
            foreach (var item in share)
            {
                Article newArticle = db.Articles.Find(item.ArticleID);
                if (categoryShared.Where(x => x.CategoryID == newArticle.CategoryID).FirstOrDefault() == null)
                {
                    Category newCategory = db.Categories.Find(newArticle.CategoryID);
                    Colour colour = db.Colours.Find(newCategory.ColourID);
                    categoryShared.Add(new CategorySharedModel
                    {
                        CategoryID = newCategory.CategoryID,
                        ColourID = newCategory.ColourID,
                        Name = newCategory.Name,
                        Hex = colour.Hex
                    });
                }
            }

            indexCategory indexCategory = new indexCategory();
            indexCategory.Category = newListCategory;
            indexCategory.CategoryShared = categoryShared;

            return PartialView("_CategoryContainer" , indexCategory);
        }

        public JsonResult Delete(Guid id)
        {

            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            var colours = db.Colours.ToList();
            ViewBag.ColourID = new SelectList(colours, "ColourID", "ColourName");

            return View();
        }

        public ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            CategoryModel newCategory = new CategoryModel();
            newCategory.Name = category.Name;
            newCategory.CategoryID = category.CategoryID;

            var colours = db.Colours.ToList();
            ViewBag.ColourID = new SelectList(colours, "ColourID", "ColourName",category.ColourID);

            return View(newCategory);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {

                Category newCategory = db.Categories.Find(category.CategoryID);
                newCategory.ColourID = category.ColourID;
                newCategory.Name = category.Name;

                db.Entry(newCategory).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            var colours = db.Colours.ToList();
            ViewBag.ColourID = new SelectList(colours, "ColourID", "ColourName", category.ColourID);

            return View();

        }


        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCategory(CategoryModel Category)
        {
            if (ModelState.IsValid)
            {
                Guid UserID = new Guid(User.Identity.Name);
                Category newCategory = new Category();
                newCategory.CategoryID = Guid.NewGuid();
                newCategory.ColourID = Category.ColourID;
                newCategory.Name = Category.Name;
                newCategory.UserID = UserID;
                db.Categories.Add(newCategory);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            var colours = db.Colours.ToList();
            ViewBag.ColourID = new SelectList(colours, "ColourID", "ColourName");

            return View();

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
