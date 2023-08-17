using Microsoft.AspNetCore.Mvc;
using MVCHomework6.Models;
using System.Diagnostics;
using MVCHomework6.Data;
using MVCHomework6.Data.Database;
using MVCHomework6.Service;

namespace MVCHomework6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogDbContext _context;
        private readonly ArticleService _articleService;


        public HomeController(ILogger<HomeController> logger, BlogDbContext context, ArticleService articleService)
        {
            _logger = logger;
            _context = context;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index(int? p, string query)
        {
            //建議改成擴充方法
            var pageNumber = p.HasValue ? p.Value < 1 ? 1 : p.Value : 1;

            var pageSize = 6; //建議抽成設定檔
            ViewData.Model = await _articleService.SearchArticles(query, pageNumber, pageSize);
            ViewBag.Query = query;
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