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

        [Authorize]
        public PartialViewResult LoadCategory()
        {
            Guid UserID = new Guid(User.Identity.Name);

            List<Category> listCategory = db.Categories.Where(x=>x.UserID==UserID).ToList();

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

            return PartialView("_CategoryContainer" , newListCategory);
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            var colours = db.Colours.ToList();
            ViewBag.ColourID = new SelectList(colours, "ColourID", "ColourName");

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
