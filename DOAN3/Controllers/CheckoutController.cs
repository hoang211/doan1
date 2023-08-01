using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Controllers
{
    public class CheckoutController : Controller
    {
        DOAN3Entities1 db = new DOAN3Entities1();
        // GET: Checkout
        public ActionResult Index()
        {
            if (Session["CustomerId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                List<OrderDetail> dstt = (List<OrderDetail>)Session["cart"];
                int id = int.Parse(Session["CustomerId"].ToString());
                Users us = db.Users.Find(id);
                ViewBag.customer = us;
                return View(dstt);
            }
        }
        public ActionResult DatMua(FormCollection frm)
        {
            string mes = "";
            List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
            int id = int.Parse(Session["CustomerId"].ToString());
            Users us = db.Users.Find(id);
            //Lưu thông tin hóa đơn
            Orders od = new Orders();
            od.UserId = id;
            
            od.Status = false;
            //od.Description = frm["Description"];
            od.DateCreated = DateTime.Today;
            od.Address = frm["Address"];
            od.TotalPrice = Convert.ToDecimal(summoney(ds));
            if (saveOrder(od) == 1)
            {
                foreach (OrderDetail item in ds)
                {
                    //var prod = db.Products.Find(item.ProductId);
                    //prod.Quantily -= item.Quantily;
                    OrderDetail odt = new OrderDetail();
                    //Products pr = new Products();
                    //pr.Images = prod.Images;
                    //pr.MoreImages = prod.MoreImages;
                    //pr.Price = prod.Price;
                    //pr.ProductName = prod.ProductName;
                    //pr.Size = prod.Size;
                    //pr.Description = prod.Description;
                    //pr.Content = prod.Content;
                    //pr.CategoryId = prod.CategoryId;
                    //pr.ProductId = item.Products.ProductId;
                    //pr.Quantily = prod.Quantily;

                    odt.OrderId = od.OrderId;
                    odt.ProductId = item.ProductId;
                    odt.Price = item.Products.Price;
                    odt.Quantily = item.Quantily;
                    odt.Acount = item.Acount;
                    //savePro(pr);
                    saveOrderDetail(odt);
                }
                mes = "Thanh toán thành công";
            }
            TempData["err"] = mes;
            Session["cart"] = "";
            return RedirectToAction("Index","MyAccount");
        }
        public double summoney(List<OrderDetail> ds)
        {
            double sum = 0;
            foreach (OrderDetail item in ds)
            {
                sum += Convert.ToDouble(item.Acount);
            }
            return sum;
        }
        public void savePro(Products pr)
        {
            db.Products.Add(pr);
            db.SaveChanges();
        }
        public int saveOrder(Orders od)
        {
            db.Orders.Add(od);
            return db.SaveChanges();
        }
        public int saveOrderDetail(OrderDetail od)
        {
            db.OrderDetail.Add(od);
            return db.SaveChanges();
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
        public ActionResult thanhcong()
        {
            //List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
            //ds.Clear();
            return View();
        }
        public JsonResult GetDataOrder()
        {

            return Json(db.Orders.Select(x => new { x.DateCreated, x.OrderDetail.Count, x.Users.FullName, x.TotalPrice, x.OrderId, x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChiTietDonHangView()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ChiTietDonHang(int id)
        {
            var x = (from sp in db.OrderDetail where sp.OrderId == id select new { sp.ProductId, sp.Products.Images, sp.Products.Price, sp.Products.ProductName, sp.Quantily, sp.OrderId, sp.Orders.DateCreated, sp.Orders.Status, sp.Orders.TotalPrice, sp.Acount });
            return Json(x, JsonRequestBehavior.AllowGet);
        }
        public JsonResult dathanhtoan()
        {
            return Json(db.Orders.Where(s => s.Status == true).Select(x => new { x.DateCreated, x.Users.FullName, x.TotalPrice,  x.OrderId,  x.Status, x.UserId }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string Delete(Orders ck)
        {
            if (ck != null)
            {
                var ds = (from sp in db.Orders where sp.OrderId == ck.OrderId select sp).FirstOrDefault();
                if (ds != null)
                {
                    db.Orders.Remove(ds);
                    db.SaveChanges();
                    return "Hủy hóa đơn thành công";
                }
                else
                {
                    return "Hủy không thành công";
                }
            }
            else
            {
                return "Hủy không thành công";
            }

        }
    }
}