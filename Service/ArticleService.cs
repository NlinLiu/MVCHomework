using Microsoft.EntityFrameworkCore;
using MVCHomework6.Data.Database;
using X.PagedList;

namespace MVCHomework6.Service
{

    public class ArticleService
    {
        private readonly BlogDbContext _context;
        public ArticleService(BlogDbContext context)
        {
            _context = context;
        }

        public async ValueTask<(IPagedList<Articles>, List<TagCount>)> SearchArticles(string query, int pageNumber, int pageSize)
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

            var tags = articles
                                   .SelectMany(a => a.Tags.Split(','))
                                   .GroupBy(t => t)
                                   .Select(g => new TagCount { Tag = g.Key, Count = g.Count() })
                                   .ToList();
            var pagedArticles = await articles.ToPagedListAsync(pageNumber, pageSize);
            return (pagedArticles, tags);
        }

        public async ValueTask<IList<Articles>> LookUpAllDataAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async ValueTask<IPagedList<Articles>> LookupPagedListAsync(int pageNumber, int pageSize)
        {
            return await _context.Articles.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
