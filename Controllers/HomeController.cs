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

        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();

            // 轉換為 CardViewModel
            var viewModels = articles.Select(a => new CardViewModel
            {
                ImageUrl = a.CoverPhoto, // 如果需要，請將此部分替換為正確的圖片URL屬性
                Title = a.Title,
                Text = a.Body,
                Links = a.Tags.Split(',').ToList() // 假設標籤存儲為逗號分隔的字符串
            }).ToList();

            // 將轉換後的 List<CardViewModel> 返回給視圖
            return View(viewModels);
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

        public ActionResult Search(string query)
        {
            {
                List<CardViewModel> viewModels = _articleService.SearchArticles(query);
                ViewBag.Query = query;
                return View("Index", viewModels);
            }

        }
    }
}