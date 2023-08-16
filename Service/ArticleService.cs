using MVCHomework6.Data.Database;
namespace MVCHomework6.Service
{

    public class ArticleService
    {
        private readonly BlogDbContext _context;
        public ArticleService(BlogDbContext context)
        {
            _context = context;
        }

        public List<CardViewModel> SearchArticles(string query)
        {
            List<Articles> articles = new List<Articles>();
            if (string.IsNullOrEmpty(query))
            {
                articles = _context.Articles.ToList();
            }
            else
            {
                articles = _context.Articles.Where(a => a.Title.Contains(query) || a.Body.Contains(query)).ToList();

            }
            List<CardViewModel> viewModels = new List<CardViewModel>();
            if (articles.Count() > 0)
            {

                viewModels = articles.Select(a => new CardViewModel
                {
                    ImageUrl = a.CoverPhoto,
                    Title = a.Title,
                    Text = a.Body,
                    Links = a.Tags.Split(',').ToList()
                }).ToList();
            }

            return viewModels;
        }
    }
}
