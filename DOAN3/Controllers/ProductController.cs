using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;
using PagedList;
namespace DOAN3.Controllers
{
    public class ProductController : Controller
    {
        DOAN3Entities1 db = new DOAN3Entities1();
        // GET: Product
        public ActionResult Index(int? page)
        {
          
            if (page == null) page = 1;
            var ds = db.Products.ToList();
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(ds.ToPagedList(pageNumber, pageSize));


        }
        public ActionResult Product()
        {
          
           
            return View();
        }
        public ActionResult TimKiem(FormCollection frm)
        {
            string tk = frm["search"];
            if (tk == null)
            {
                var x = db.Products.ToList();
                return View("Product", x);
            }

            else
            {
                List<Products> ds = db.Products.Where(x => x.ProductName.Contains(tk) || x.Price.ToString().Contains(tk) /*|| x.Description.Contains(tk)*/).ToList();
                return View("Product", ds);
            }

        }
    }
}