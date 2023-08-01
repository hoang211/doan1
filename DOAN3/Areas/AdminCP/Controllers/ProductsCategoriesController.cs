using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Areas.AdminCP.Controllers
{
    public class ProductsCategoriesController : Controller
    {
        private DOAN3Entities1 db = new DOAN3Entities1();

        // GET: AdminCP/ProductsCategories
        public ActionResult Index()
        {
            return View(db.ProductsCategory.ToList());
        }

        public JsonResult GetAllData()
        {
            var ds = db.ProductsCategory.Select(x => new { x.CategoryId, x.CategoryName }).ToList();
            return Json(ds,JsonRequestBehavior.AllowGet);
        }
        // GET: AdminCP/ProductsCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCategory productsCategory = db.ProductsCategory.Find(id);
            if (productsCategory == null)
            {
                return HttpNotFound();
            }
            return View(productsCategory);
        }

        // GET: AdminCP/ProductsCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminCP/ProductsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,Description")] ProductsCategory productsCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductsCategory.Add(productsCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productsCategory);
        }

        // GET: AdminCP/ProductsCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCategory productsCategory = db.ProductsCategory.Find(id);
            if (productsCategory == null)
            {
                return HttpNotFound();
            }
            return View(productsCategory);
        }

        // POST: AdminCP/ProductsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,Description")] ProductsCategory productsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productsCategory);
        }

        // GET: AdminCP/ProductsCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsCategory productsCategory = db.ProductsCategory.Find(id);
            if (productsCategory == null)
            {
                return HttpNotFound();
            }
            return View(productsCategory);
        }

        // POST: AdminCP/ProductsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsCategory productsCategory = db.ProductsCategory.Find(id);
            db.ProductsCategory.Remove(productsCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
