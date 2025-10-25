using PROMPERU.PeruInfo.CMS.Filtros;
using System.Web;
using System.Web.Mvc;

namespace PROMPERU.PeruInfo.CMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionExpireFilterAttribute());
        }
    }
}
