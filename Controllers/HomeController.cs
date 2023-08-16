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
            var viewModels = articles.Select(a => new CardViewModel
            {
                ImageUrl = a.CoverPhoto,
                Title = a.Title,
                Text = a.Body,
                Links = a.Tags.Split(',').ToList()
            }).ToList();

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