using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Store.Controllers;

public class ShopController : Controller
{
    private readonly ShopDbContext _context;

    public ShopController(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        var categories = await _context.Categories.ToListAsync();

        var sortedCategories = categories
            .OrderBy(c => c.Name == "Uncategorized")
            .ThenBy(c => c.Name)
            .ToList();

        ViewBag.Categories = sortedCategories;
        ViewBag.CurrentCategoryId = categoryId;

        var articlesQuery = _context.Articles
            .Include(a => a.Category)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            articlesQuery = articlesQuery.Where(a => a.Category.Id == categoryId.Value);
        }

        var articles = await articlesQuery.ToListAsync();
        return View(articles);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = await _context.Articles
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id.Value);

        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }
}
