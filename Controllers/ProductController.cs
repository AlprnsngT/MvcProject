using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _50DersteMvcProjeKampi.Models.Entity;
namespace _50DersteMvcProjeKampi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MVCProjectDbEntities db = new MVCProjectDbEntities();
        #region ProductList
        public ActionResult Index()
        {
            var values = db.tblProducts.ToList();
            return View(values);
        }
        #endregion

        #region Get Post Notu
        /*
         * Doğrudan ekleme yapmak siteye doğrudan kategori eklemesi gerçekleştiriyor fakat boş veriler de eklenebiliyor. 
         * Bunun olmaması için [HttpGet] ve [HttpPost] işlemlerini tanımlanması gerekiyor.
         * 
        public ActionResult NewProduct(tblProducts product)
        {
            db.tblProducts.Add(product);
            db.SaveChanges();
            return View();
        }
        */
        #endregion

        #region AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            //ürün ekle sayfasında kategorilerin gelmesi gerek dropdowndan seçebilmek için
            List<SelectListItem> items = (from i in db.tblCategories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryId.ToString(),
                                             }).ToList();
            #region Id ile category Çekme
            /*
             * bir liste oluşturduk ve listeden elemanlar çekeceğiz
             * kategoriler tablosu üzerinde lnq sorgusu atıyoruz
             * kategorileri bir listeye dönüştürdük ve yeni listedem item seçiyoruz
             * itemin texti kategori adı
             * valuesi ise IDsi ama stringe dönüştür çünkü id short geliyo
             * daha sonra bu dönen itemi listeye dönüştür.
             * 
             */
            #endregion
            //dönüştürdüğün listeyi başka yere taşıyabilmek için
            ViewBag.CategoryList = items;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(tblProducts product)
        {
            var addProduct = db.tblCategories.Where(item=>item.CategoryId==product.tblCategories.CategoryId).FirstOrDefault();
            product.tblCategories = addProduct;
            db.tblProducts.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteProduct
        public ActionResult DeleteProduct(int id)
        {
            var deleteProduct = db.tblProducts.Find(id);
            if(db.tblOrders.Any(p=>p.ProductId == id))
            {
                TempData["ErrorMessage"] = $"{deleteProduct.ProductName}  ürünün siparişi verilmiş, ürün silinemez";
                return RedirectToAction("Index");
            }
            db.tblProducts.Remove(deleteProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Ürün GÜncelleme Sayfası Bilgilerini Getir
        public ActionResult GetProduct(int id)
        {
            var getProduct = db.tblProducts.Find(id);
            //ürün güncelleme sayfasında kategoriler dropdown ile gözükmesini sağlayalım
            List<SelectListItem> items = (from i in db.tblCategories.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.CategoryName,
                                              Value = i.CategoryId.ToString(),
                                          }).ToList();
            ViewBag.degerler = items;
            return View("GetProduct", getProduct);
        }
        #endregion

        #region UpdateProduct
        public ActionResult UpdateProduct(tblProducts products)
        {
            var updateProduct = db.tblProducts.Find(products.ProductId);
            updateProduct.ProductPrice = products.ProductPrice;
            updateProduct.ProductBrand = products.ProductBrand;
            updateProduct.ProductStocks = products.ProductStocks;
            updateProduct.ProductName = products.ProductName;

            // ürünün kategorisini güncelleme
            var category = db.tblCategories.Where(categories => categories.CategoryId == products.tblCategories.CategoryId).FirstOrDefault();
            updateProduct.CategoryId = category.CategoryId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}