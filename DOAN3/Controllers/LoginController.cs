using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;

namespace DOAN3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DOAN3Entities1 db = new DOAN3Entities1();
        // GET: AdminCP/LoginCP
        public ActionResult Index()
        {
            ViewBag.Err = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection x)
        {

            string err = "";
            string tk = x["tk"];
            string mk = x["mk"];
            Users ck = db.Users.Where(y => (y.UserName == tk) && y.Role == true && y.Active == true).FirstOrDefault();
            if (ck == null)
            {
                err = "Tên đăng nhập không tồn tại";
            }
            else
            {
                if (ck.PassWord.Equals(mk))
                {
                    Session["CustomerId"] = ck.UserId;
                    Session["tk"] = ck.UserName;
                    Session["mk"] = ck.PassWord;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    err = "Mật khẩu không chính xác";
                }
            }
            ViewBag.Err = err;
            return View();

        }
        public ActionResult Logout()
        {
            Session["tk"] = "";
            Session["mk"] = "";

            return RedirectToAction("Index", "Login");
        }
        [HttpPost]       
        public ActionResult DangKy(FormCollection frm)
        {
            Users users = new Users();
            users.FullName = frm["fullname"];
            users.PassWord = frm["password"];
            users.Sdt = frm["sdt"];
            users.Email = frm["email"];
            users.Address = frm["address"];
            users.UserName = frm["username"];
            users.Role = false;
            users.Active = true;
            users.DateCreated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }
    }
}