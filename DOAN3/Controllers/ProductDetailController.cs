using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Controllers
{



    public class ProductDetailController : Controller
    {
        DOAN3Entities1 db = new DOAN3Entities1();
        // GET: ProductDetail
        public ActionResult Index(int id)
        {
            var ds = db.Products.FirstOrDefault(x => x.ProductId == id);
            return View(ds);
        }
        public ActionResult ViewProductDetail()
        {
            var ds = db.Products.ToList();
            return View(ds);
        }
    }
}