using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly StorePages.Data.ShopDbContext _context;

        public IndexModel(StorePages.Data.ShopDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Category).ToListAsync();
        }
    }
}
