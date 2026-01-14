using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Articles
{
    public class EditModel : PageModel
    {
        private readonly StorePages.Data.ShopDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public EditModel(StorePages.Data.ShopDbContext context, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
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

            var article =  await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            Article = article;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Reload the article to get current values
            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == Article.Id);
            if (article == null)
            {
                return NotFound();
            }

            article.Name = Article.Name;
            article.Price = Article.Price;
            article.ExpirationDate = Article.ExpirationDate;
            article.CategoryId = Article.CategoryId;

            if (Article.FormFile != null)
            {
                if (!string.IsNullOrWhiteSpace(article.ImagePath) && article.ImagePath != "images/default.png")
                {
                    string oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, "upload", article.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + Article.FormFile.FileName;
                string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "upload", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await Article.FormFile.CopyToAsync(stream);
                }
                article.ImagePath = fileName;
            }

            _context.Update(article);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
