using System.Web;
using System.Web.Mvc;

namespace WebRole1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());//originally here
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute());//authorize everything
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
