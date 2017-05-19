using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PinochleDeck;

namespace SignalR_WebChat.Controllers
{
    public class HomeController : Controller
    {
        private PinochleDealer _dealer;

        public HomeController()
        {
            _dealer = new PinochleDealer();
        }
        public HomeController(PinochleDealer PinochleDealer)
        {
            _dealer = PinochleDealer ?? new PinochleDealer();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            var dealer = _dealer;

            return View(dealer);
        }

        public ActionResult Monitor()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}