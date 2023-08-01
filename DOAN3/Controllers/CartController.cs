using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Controllers
{
    public class CartController : Controller
    {
        DOAN3Entities1 db = new DOAN3Entities1();
        // GET: Cart
        public ActionResult Index(int id)
        {
            var ds = db.Products.FirstOrDefault(x => x.ProductId == id);
            return View(ds);
        }
        public ActionResult Viewcartnull()
        {
            return View();
        }

        public ActionResult AddCart(int id)
        {
            var sp = db.Products.FirstOrDefault(s => s.ProductId == id);

            if (!Session["cart"].Equals(""))
            {

                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                var kt = ds.FirstOrDefault(s => s.ProductId == id);
                if (kt == null)
                {
                    OrderDetail dt = new OrderDetail() { ProductId = id, Quantily = 1, Acount = 1, Products = sp };
                    dt.Acount = (dt.Quantily) * (dt.Products.Price);
                    ds.Add(dt);
                    Session["cart"] = ds;
                }
                else
                {
                    kt.Quantily += 1;
                }
                Session["cart"] = ds;
            }
            else
            {

                List<OrderDetail> ds = new List<OrderDetail>();
                OrderDetail dt = new OrderDetail() { ProductId = id, Quantily = 1, Acount = 1, Products = sp };
                dt.Acount = (dt.Quantily) * (dt.Products.Price);
                ds.Add(dt);
                Session["cart"] = ds;
            }
            return Redirect("~/Home/Index");


        }
        public ActionResult ViewCart()
        {
            List<OrderDetail> dsdh=new List<OrderDetail>();
            try
            {
                dsdh = (List<OrderDetail>)Session["cart"];
            }
            catch
            {
                dsdh = new List<OrderDetail>();
            }
            
            return View(dsdh);


        }

        public ActionResult cartdelete(int productid)
        {
            if (!Session["cart"].Equals(""))
            {
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                int vt = 0;
                foreach (var item in ds)
                {
                    if (item.ProductId == productid)
                    {
                        ds.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                Session["cart"] = ds;
            }
            return RedirectToAction("ViewCart", "Cart");
        }
        
        public ActionResult cartupdate(FormCollection x)
        {


            if (!string.IsNullOrEmpty(x["CapNhat"]))
            {
                var listsl = x["quantily"]; //mảng chứa lần lượt các giá trị số lượng của giỏ hàng
                var listarr = listsl.Split(',');
                List<OrderDetail> ds = (List<OrderDetail>)Session["cart"];
                int vt = 0;
                foreach (OrderDetail item in ds)
                {
                    ds[vt].Quantily = int.Parse(listarr[vt]);
                    ds[vt].Acount = ds[vt].Products.Price * ds[vt].Quantily;
                    vt++;
                }
                Session["cart"] = ds;

            }
            return RedirectToAction("ViewCart", "Cart");
        }
    }
}