using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        const int PageSize = 50;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page = 1)
        {
            var count = DataManager.Tags.Count;
            var data = DataManager.Tags.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            int maxPage = (int)Math.Ceiling((float)count / PageSize);
            ViewBag.MaxPage = maxPage;
            ViewBag.Page = page;
            ViewBag.PreviousPage = Math.Max(1, page - 1);
            ViewBag.NextPage = Math.Min(maxPage, page + 1);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
