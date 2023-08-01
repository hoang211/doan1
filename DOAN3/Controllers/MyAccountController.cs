using DOAN3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN3.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: MyAccount
        DOAN3Entities1 db = new DOAN3Entities1();
        public ActionResult Index()
        {
            var sp = db.Orders.ToList();
            return View(sp);
        }
        public ActionResult viewchitiet()
        {
            
            return View();
        }
        [HttpGet]
        public JsonResult chitiet(int id)
        {
            var x = (from sp in db.OrderDetail where sp.OrderId == id select new { sp.ProductId, sp.Products.Images, sp.Products.Price, sp.Products.ProductName, sp.Quantily, sp.OrderId, sp.Orders.DateCreated, sp.Orders.Status, sp.Orders.TotalPrice, sp.Acount });
            return Json(x, JsonRequestBehavior.AllowGet);
        }
    }
}