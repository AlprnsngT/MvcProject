using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _50DersteMvcProjeKampi.Models.Entity;//db ekledi
using PagedList;
using PagedList.Mvc;
namespace _50DersteMvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        MVCProjectDbEntities db = new MVCProjectDbEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var values = db.tblCategories.ToList();

            //sayfa değişkeni ile başlangıç sayfamızı işaretledik
            //ikinci parametre(4) ile bir sayfada kaç category gözükeceğini işaretledik
            var values = db.tblCategories.ToList().ToPagedList(sayfa, 5);  
            return View(values);
        }
        /*
         * Doğrudan ekleme yapmak siteye doğrudan kategori eklemesi gerçekleştiriyor fakat boş veriler de eklenebiliyor. 
         * Bunun olmaması için [HttpGet] ve [HttpPost] işlemlerini tanımlanması gerekiyor.
        public ActionResult NewCategory(Categories category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return View();
        }
        */
        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCategory(tblCategories category)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCategory");
            }
            if(db.tblCategories.Any(c=>c.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError("CategoryName", "Bu Kategori Adı Zaten Mevcut");
                return View("NewCategory");
            }
            db.tblCategories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            var deleteCategory = db.tblCategories.Find(id);
            db.tblCategories.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //güncelleme için ayrı bir view kullanalım çünkü kategori adı ve id almamız gerekecek
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var updateCategory = db.tblCategories.Find(id);
            return View("UpdateCategory", updateCategory);
        }
        [HttpPost]
        public ActionResult HtmlUpdateCategory(tblCategories category)
        {
            var updateCategory = db.tblCategories.Find(category.CategoryId);
            updateCategory.CategoryName = category.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}