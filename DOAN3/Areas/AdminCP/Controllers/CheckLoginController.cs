using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOAN3.Areas.AdminCP.Controllers
{
    public class CheckLoginController : Controller
    {
        // GET: AdminCP/CheckLogin
        public CheckLoginController()
        {
            if (System.Web.HttpContext.Current.Session["UserAdmin"] == "")
            {
                System.Web.HttpContext.Current.Response.Redirect("~/AdminCP/LoginCP/login");
            }

        }
    }
}