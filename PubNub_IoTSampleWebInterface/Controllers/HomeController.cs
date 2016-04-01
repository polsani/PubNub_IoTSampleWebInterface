using System.Web.Mvc;
using PubNub_IoTSampleWebInterface.Models;
using PubNub_IoTSampleWebInterface.Services;

namespace PubNub_IoTSampleWebInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new MessageModel());
        }

        [HttpPost]
        public ActionResult Index(string message)
        {
            var messageService = new MessageService();
            messageService.SendMessage(message);
            return View(new MessageModel());
        }
    }
}