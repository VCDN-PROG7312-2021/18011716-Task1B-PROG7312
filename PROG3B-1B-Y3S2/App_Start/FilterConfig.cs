using System.Web;
using System.Web.Mvc;

namespace PROG3B_1B_Y3S2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
