using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models;

namespace Store.Controllers;

public class ArticlesController : Controller
{
    private readonly IArticlesContext _articlesContext;

    public ArticlesController(IArticlesContext articlesContext)
    {
        _articlesContext = articlesContext;
    }

    public IActionResult List()
    {
        var articles = _articlesContext.GetAllArticles();
        return View(articles);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Article article)
    {
        if (ModelState.IsValid)
        {
            _articlesContext.AddArticle(article);
            return RedirectToAction(nameof(List));
        }
        return View(article);
    }
    
    public IActionResult Details(int id)
    {
        var article = _articlesContext.GetArticle(id); 

        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }
    
    public IActionResult Edit(int id)
    {
        var article = _articlesContext.GetArticle(id);

        if (article == null)
            return NotFound();
        
        return View(article);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Article article)
    {
        if (id != article.Id)
            return BadRequest();

        if (ModelState.IsValid)
        {
            var updatedArticle = _articlesContext.UpdateArticle(article);
            
            if (updatedArticle == null)
            {
                return NotFound(); 
            }
            
            return RedirectToAction(nameof(List));
        }

        return View(article);
    }
    
    public IActionResult Delete(int id)
    {
        var article = _articlesContext.GetArticle(id);

        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        bool wasDeleted = _articlesContext.DeleteArticle(id); 

        if (!wasDeleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(List));
    }
}