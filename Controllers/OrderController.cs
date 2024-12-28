using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _50DersteMvcProjeKampi.Models.Entity;
namespace _50DersteMvcProjeKampi.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        MVCProjectDbEntities db = new MVCProjectDbEntities();

        #region Siparişleri Indexe Yazdır
        public ActionResult Index()
        {
            ViewBag.Orders = db.tblOrders.ToList();
            ViewBag.Products = db.tblProducts.ToList();

            #region Ürünleri Getir
            ViewBag.getProduct = db.tblProducts.Select(p => new SelectListItem
            {
                Text = p.ProductName,
                Value = p.ProductId.ToString(),

            }).ToList();
            #endregion

            #region Müşterileri Getir
            ViewBag.customers = db.tblCustomers.Select(c => new SelectListItem
            {
                Text = c.CustomerName,
                Value = c.CustomerId.ToString(),
            }).ToList();
            #endregion

            //ViewBag.orderProduct = getProduct;
            //ViewBag.customers = customers;


            var values = db.tblOrders.ToList();
            return View(values);

        }
        #endregion

        #region Satış Yap
        [HttpPost]
        public ActionResult NewOrder(tblOrders order)
        {
            var product = db.tblProducts.FirstOrDefault(products => products.ProductId == order.ProductId);
            var customer = db.tblCustomers.FirstOrDefault(customers => customers.CustomerId == order.CustomerId);
            order.CustomerId = customer.CustomerId;
            order.ProductId = product.ProductId;
            //order.OrderPrice = product.ProductPrice;
            //order.OrderStocks = order.OrderStocks;
            if ((order.OrderStocks > product.ProductStocks)  &&  product.ProductStocks<=0)
            {
                TempData["ErrorMessage"] = "Maalesef Sipariş Oluşturulamadı - Depoda Bu Kadar Ürün Yok";
                return RedirectToAction("Index");
            }
            else
            {
                product.ProductStocks -= order.OrderStocks;
            }
            db.tblOrders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult NewOrder()
        {
            return View();
        }
        #endregion

        #region Satışları Sil

        public ActionResult DeleteOrder(int id)
        {
            var deleteOrder = db.tblOrders.Find(id);
            db.tblOrders.Remove(deleteOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

    }
}