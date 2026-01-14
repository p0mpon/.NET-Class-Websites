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
    public class DeleteModel : PageModel
    {
        private readonly StorePages.Data.ShopDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DeleteModel(StorePages.Data.ShopDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (article is not null)
            {
                Article = article;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImagePath))
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "upload", article.ImagePath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                Article = article;
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
