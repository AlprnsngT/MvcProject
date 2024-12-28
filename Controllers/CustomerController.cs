using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _50DersteMvcProjeKampi.Models.Entity;
namespace _50DersteMvcProjeKampi.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        MVCProjectDbEntities db = new MVCProjectDbEntities();
        //// bu kısmı biraz daha güçlendirelim
        //public ActionResult Index()
        //{
        //    var values = db.tblCustomers.ToList();
        //    return View(values);
        //}
        public ActionResult Index(string customer)
        {
            var values = from d in db.tblCustomers select d ;
            if (!string.IsNullOrEmpty(customer))
            {
                values = values.Where(m => m.CustomerName.Contains(customer));
            }
            return View(values.ToList());
        }
        /*
         * Doğrudan ekleme yapmak siteye doğrudan kategori eklemesi gerçekleştiriyor fakat boş veriler de eklenebiliyor. 
         * Bunun olmaması için [HttpGet] ve [HttpPost] işlemlerini tanımlanması gerekiyor.
         * 
        public ActionResult NewCustomer(tblCustomers customer)
        {
            db.tblCustomers.Add(customer);
            db.SaveChanges();
            return View(customer);
        } 
        */
        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomer(tblCustomers customer)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            db.tblCustomers.Add(customer);
            db.SaveChanges();
            return View();
        }
        public ActionResult DeleteCustomer(int id)
        {
            var deleteCustomer = db.tblCustomers.Find(id);
            db.tblCustomers.Remove(deleteCustomer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //güncellenecek customeri ekrana getir
        public ActionResult UpdateCustomer(int id)
        {
            var updateCustomer = db.tblCustomers.Find(id);
            return View("UpdateCustomer", updateCustomer);
        }
        public ActionResult HtmlUpdateCustomer(tblCustomers customer)
        {
            var updateCustomer = db.tblCustomers.Find(customer.CustomerId);
            updateCustomer.CustomerName = customer.CustomerName;
            updateCustomer.CustomerSurname = customer.CustomerSurname;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}