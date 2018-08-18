using System.Web;
using System.Web.Mvc;
using WebApp.Filters;

namespace WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilter());
            filters.Add(new MyAuthenticationFilter());
            //filters.Add(new MyAuthorizeFilter());
        }
    }
}
