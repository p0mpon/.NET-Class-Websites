using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly StorePages.Data.ShopDbContext _context;

        public DeleteModel(StorePages.Data.ShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category is not null)
            {
                Category = category;

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

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                Category = category;

                var uncategorizedCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == "Uncategorized");

                if (uncategorizedCategory == null)
                {
                    uncategorizedCategory = new Category { Name = "Uncategorized" };
                    _context.Categories.Add(uncategorizedCategory);
                    await _context.SaveChangesAsync();
                }

                var articlesInCategory = await _context.Articles
                    .Where(a => a.CategoryId == category.Id)
                    .ToListAsync();

                foreach (var article in articlesInCategory)
                {
                    article.CategoryId = uncategorizedCategory.Id;
                }

                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
