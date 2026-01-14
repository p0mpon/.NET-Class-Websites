using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Data;

namespace Store.Controllers
{
    public class ArticlesController : Controller
    {
        private const long MaxFileSize = 2 * 1024 * 1024;
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const string PlaceholderImage = "default.png";

        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ArticlesController(ShopDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Articles
        public async Task<IActionResult> List()
        {
            var shopDbContext = _context.Articles.Include(a => a.Category);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article, int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
    
            if (category != null)
            {
                article.Category = category;
                ModelState.Remove("Category");
            }

            ValidateUpload(article.FormFile);

            if (ModelState.IsValid)
            {
                article.ImagePath = await SaveUploadedFileAsync(article.FormFile);
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
    
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", categoryId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", article.Category.Id);
            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article, int categoryId)
        {
            if (id != article.Id) return NotFound();

            var articleToUpdate = await _context.Articles.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
            if (articleToUpdate == null)
            {
                return NotFound();
            }

            var selectedCategory = await _context.Categories.FindAsync(categoryId);
            if (selectedCategory != null)
            {
                articleToUpdate.Category = selectedCategory;
                ModelState.Remove("Category");
            } else {
                ModelState.AddModelError("CategoryId", "Please select a valid category.");
            }

            ValidateUpload(article.FormFile);

            if (ModelState.IsValid)
            {
                try
                {
                    articleToUpdate.Name = article.Name;
                    articleToUpdate.Price = article.Price;
                    articleToUpdate.ExpirationDate = article.ExpirationDate;

                    if (article.FormFile != null && article.FormFile.Length > 0)
                    {
                        var savedFileName = await SaveUploadedFileAsync(article.FormFile);
                        DeleteFileIfExists(articleToUpdate.ImagePath);
                        articleToUpdate.ImagePath = savedFileName;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(List));
            }

            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", categoryId);
            return View(articleToUpdate);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                DeleteFileIfExists(article.ImagePath);
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }

        private async Task<string?> SaveUploadedFileAsync(IFormFile? file)
        {
            if (file is null || file.Length == 0)
            {
                return null;
            }

            var uploadFolder = Path.Combine(_environment.WebRootPath, "upload");
            Directory.CreateDirectory(uploadFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }

        private void DeleteFileIfExists(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || fileName == PlaceholderImage)
            {
                return;
            }

            var uploadFolder = Path.Combine(_environment.WebRootPath, "upload");
            var filePath = Path.Combine(uploadFolder, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private void ValidateUpload(IFormFile? file)
        {
            if (file is null || file.Length == 0)
            {
                return;
            }

            if (file.Length > MaxFileSize)
            {
                ModelState.AddModelError(nameof(Article.FormFile), "Plik jest zbyt duży. Maksymalnie 2 MB.");
            }

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !AllowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(Article.FormFile), "Dozwolone są tylko pliki .jpg, .jpeg, .png, .gif, .webp.");
            }
        }
    }
}
