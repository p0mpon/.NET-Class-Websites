using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly StorePages.Data.ShopDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public CreateModel(StorePages.Data.ShopDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? FormFile { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (FormFile != null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + FormFile.FileName; 
                string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "upload", fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await FormFile.CopyToAsync(stream);
                }
                Article.ImagePath = fileName;
            }
            else
            {
                Article.ImagePath = null;
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
