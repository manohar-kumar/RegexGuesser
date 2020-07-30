using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegexService.Models;

namespace RegexService.Controllers
{
    public class HomeController : Controller
    {
        [Route("RegexFirst")]
        public string RegexFirst()
        {
            return "MyFirstAPI";
        }

        public bool ValidateMyRegex(string regexString)
        {
            return false;
        }

        public string QueueForAGame(string name)
        {
            return MatchMaker.GetMeAGame(name).ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GamePlay()
        {
            ViewData["Message"] = "Instructions";

            return View();
        }

        public IActionResult GameScreen()
        {
            ViewData["Message"] = "Your Game Screen.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Manohar Kumar";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
