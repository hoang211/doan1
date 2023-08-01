using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOAN3.Models;


namespace DOAN3.Areas.AdminCP.Controllers
{
    public class AdminController : CheckLoginController
    {
        // GET: AdminCP/Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
    }
}