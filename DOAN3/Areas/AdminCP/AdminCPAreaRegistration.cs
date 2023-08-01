using System.Web.Mvc;

namespace DOAN3.Areas.AdminCP
{
    public class AdminCPAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminCP";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminLogin",
                "AdminCP/LoginCP/login",
                new {Controller = "LoginCP" ,action = "login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminCP_default",
                "AdminCP/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}