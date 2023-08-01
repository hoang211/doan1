using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Areas.AdminCP.Controllers
{
    public class ProductsController : CheckLoginController
    {
        private DOAN3Entities1 db = new DOAN3Entities1();

        // GET: AdminCP/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductsCategory);
            return View(products.ToList());
        }
        public ActionResult ViewData()
        {           
            return View();
        }
        public ActionResult GetAllData()
        {
            return Json(db.Products.Select(x=>new { x.ProductId,x.ProductName,x.ProductsCategory.CategoryId,x.ProductsCategory.CategoryName,x.Quantily,x.Price,x.Images,x.Description}).ToList(),JsonRequestBehavior.AllowGet);
        }

        // GET: AdminCP/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: AdminCP/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ProductsCategory, "CategoryId", "CategoryName");
            return View();
        }

        // POST: AdminCP/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Quantily,Price,Description")] Products products, HttpPostedFileBase file2)
        {
            string _FileName = "";
            string _path = "";
            try
            {
                if (file2.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file2.FileName);
                    _path = Path.Combine(Server.MapPath("~/FileUpload/files/"), _FileName);
                    file2.SaveAs(_path);
                }

            }
            catch
            {

            }


            if (ModelState.IsValid)
            {
                products.Images =  _FileName;
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.ProductsCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Quantily,Price,Description")] Products products)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(products);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CategoryId = new SelectList(db.ProductsCategory, "CategoryId", "CategoryName", products.CategoryId);
        //    return View(products);

        //}

        // GET: AdminCP/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.ProductsCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // POST: AdminCP/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Quantily,Price,Description")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.ProductsCategory, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: AdminCP/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: AdminCP/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Angular
        public ActionResult InputData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputData([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Quantily,Price,Description")] Products products)
        {
            products.MoreImages = "";

            if (ModelState.IsValid)
            {
                try
                {
                    db.Products.Add(products);
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetailDataView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetailData(int id)
        {
            var x = (from sp in db.Products where sp.ProductId == id select new { sp.ProductId, sp.ProductName, sp.Quantily, sp.Price, sp.Images, sp.MoreImages, sp.Description, sp.ProductsCategory.CategoryName, sp.ProductsCategory.CategoryId }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DeleteProduct(Products product)
        {
            if (product != null)
            {
                var ds = (from sp in db.Products where sp.ProductId == product.ProductId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Products.Remove(ds);
                    db.SaveChanges();
                    return "Xóa sản phẩm thành công";
                }
                else
                {
                    return "Xóa sản phẩm không thành công";
                }
            }
            else
            {
                return "Xóa sản phẩm không thành công";
            }

        }
        public ActionResult EditData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "ProductId,ProductName,CategoryId,Images,MoreImages,Quantily,Price,Description")] Products products)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Products where ds.ProductId == products.ProductId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.ProductName = products.ProductName;
                    sp.Description = products.Description;
                    sp.Quantily = products.Quantily;
                    sp.MoreImages = products.MoreImages;
                    
                    sp.Price = products.Price;
                    sp.Images = products.Images;
                    
                    sp.CategoryId = products.CategoryId;
                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { msg = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { msg = false }, JsonRequestBehavior.AllowGet);
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
