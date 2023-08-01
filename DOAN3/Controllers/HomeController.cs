using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Controllers
{
   
    public class HomeController : Controller
    {
        DOAN3Entities1 db = new DOAN3Entities1();
        public ActionResult Index()
        {
           
            var ds = db.Products.ToList();

            
            return View(ds);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetAllData()
        {
            return Json(db.Products.Select(x => new { x.ProductId, x.ProductName, x.ProductsCategory.CategoryId, x.ProductsCategory.CategoryName, x.Quantily, x.Price, x.Images, x.Description }).ToList(), JsonRequestBehavior.AllowGet);
        }




    }
}