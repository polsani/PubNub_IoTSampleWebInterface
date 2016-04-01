using System.Web;
using System.Web.Mvc;

namespace PubNub_IoTSampleWebInterface
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
