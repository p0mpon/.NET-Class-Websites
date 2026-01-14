using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly ShopDbContext _context;

        public IndexModel(ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public async Task OnGetAsync()
        {
            var categoriesList = await _context.Categories.ToListAsync();
            Categories = categoriesList
                .OrderBy(c => c.Name == "Uncategorized")
                .ThenBy(c => c.Name)
                .ToList();

            IQueryable<Article> articlesQuery = _context.Articles
                .Include(a => a.Category);

            if (CategoryId.HasValue)
            {
                articlesQuery = articlesQuery.Where(a => a.Category!.Id == CategoryId.Value);
            }

            Articles = await articlesQuery.Take(9).ToListAsync();
        }

        public IActionResult OnGetAddToCart(int id)
        {
            string cookieName = "prod" + id;
            int count = 1;

            if (Request.Cookies.ContainsKey(cookieName))
            {
                int.TryParse(Request.Cookies[cookieName], out count);
                count++;
            }

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };

            Response.Cookies.Append(cookieName, count.ToString(), options);

            return RedirectToPage();
        }
    }
}