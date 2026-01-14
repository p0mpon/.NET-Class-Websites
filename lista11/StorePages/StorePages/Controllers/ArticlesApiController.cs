using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;
using Microsoft.AspNetCore.Authorization;

namespace StorePages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ArticlesApiController(ShopDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();
            return article;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id) return BadRequest();
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}