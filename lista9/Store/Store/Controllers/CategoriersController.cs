using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;

namespace Store.Controllers;

public class CategoriesController : Controller
{
    private readonly ShopDbContext _context;

    public CategoriesController(ShopDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> List()
    {
        return View(await _context.Categories.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        return View(category);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(List));
        }
        return View(category);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories
                .Include(c => c.Articles)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category != null)
        {
            var defaultCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == "Uncategorized");

            if (defaultCategory == null)
            {
                defaultCategory = new Category { Name = "Uncategorized" };
                _context.Categories.Add(defaultCategory);
                await _context.SaveChangesAsync();
            }

            if (category.Articles != null)
            {
                foreach (var article in category.Articles)
                {
                    article.Category = defaultCategory;
                }
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(List));
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}
