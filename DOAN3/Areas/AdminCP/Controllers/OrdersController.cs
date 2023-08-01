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
    public class OrdersController : Controller
    {
        private DOAN3Entities1 db = new DOAN3Entities1();

        // GET: AdminCP/Orders
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetBilloff()
        {
            return Json(db.Orders.Where(s => s.Status == false).Select(x => new { x.DateCreated, x.Users.FullName, x.TotalPrice, x.OrderId, x.Address, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDetailData(int id)
        {
            return Json(db.Orders.Where(y => y.OrderId == id).Select(x => new { x.OrderId, x.Status, x.Users.FullName, x.Address, x.TotalPrice, x.DateCreated }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBillon()
        {
            return Json(db.Orders.Where(s => s.Status == true).Select(x => new { x.DateCreated, x.Users.FullName, x.TotalPrice, x.OrderId, x.Address, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewDetail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Status(Orders id)
        {

            var news = (from sp in db.Orders where sp.OrderId == id.OrderId select sp).FirstOrDefault();
            news.Status = true;
            var odid = db.OrderDetail.Where(x => x.OrderId == news.OrderId).ToList();
            foreach (OrderDetail item in odid)
            {
                var pro = db.Products.Find(item.ProductId);
                pro.Quantily -= Convert.ToByte(item.Quantily);
                savePro(pro);
            }
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public void savePro(Products pr)
        {
            db.Entry(pr).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpGet]
        public ActionResult ChiTietDonHang(int id)
        {
            var x = (from sp in db.OrderDetail where sp.OrderId == id select new { sp.ProductId, sp.Products.Images, sp.Products.Price, sp.Products.ProductName, sp.Quantily, sp.OrderId, sp.Orders.DateCreated, sp.Orders.Status, sp.Orders.TotalPrice, sp.Acount });
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ChiTietKhach(int id)
        {
            var x = (from sp in db.Orders
                     join kh in db.Users on sp.UserId equals kh.UserId
                     where sp.OrderId == id
                     select new { kh.UserId, kh.FullName, kh.Sdt, kh.Email, kh.Address }).ToList();
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSum(int id)
        {
            var x = (from sp in db.Orders where sp.OrderId == id select new { sp.TotalPrice }).FirstOrDefault();
            return Json(x, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string DeleteOrder(Orders ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Orders where sp.OrderId == ck.OrderId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Orders.Remove(ds);
                    db.SaveChanges();
                    return "Xóa hóa đơn thành công";
                }
                else
                {
                    return "Xóa không thành công";
                }
            }
            else
            {
                return "Xóa không thành công";
            }

        }

        // GET: AdminCP/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: AdminCP/Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: AdminCP/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,DateCreated,Address,Status,TotalPrice,UserId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", orders.UserId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", orders.UserId);
            return View(orders);
        }

        
        // GET: AdminCP/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: AdminCP/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditData()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditData([Bind(Include = "OrderId,CustomerId,DateCreated,Address,Status,TotalPrice,UserId")] Orders oder)
        {
            if (ModelState.IsValid)
            {
                var sp = (from ds in db.Orders where ds.OrderId == oder.OrderId select ds).FirstOrDefault();
                if (sp != null)
                {
                    sp.DateCreated = oder.DateCreated;
                    sp.Status = oder.Status;
                    sp.Address = oder.Address;
                   
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
