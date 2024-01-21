using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineJudge.Models;
using OnlineJudge.Services;
using System.Diagnostics;

namespace OnlineJudge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CodeRunner code = new CodeRunner();
            code.BuildAndRun();
            ViewBag.Vredict = code.GetError();
            TestRunner test = new TestRunner("SAfd", "sdf", "SDf");
            test.MakeDir();
            test.MakeFiles();
            test.WriteData();
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
