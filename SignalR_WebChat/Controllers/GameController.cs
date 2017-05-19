using SignalR_WebChat.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalR_WebChat.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pinochle()
        {
            return View();
        }
        [HttpGet]
        public ActionResult JoinGameTable()
        {
            return View();
        }
        [HttpPost]
        public ActionResult JoinGameTable(List<AppUser> Players)
        {
            return View();
        }
    }
}