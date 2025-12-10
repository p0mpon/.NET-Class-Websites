using Store.Models;

namespace Store.Data;

public interface IArticlesContext
{
    void AddArticle(Article article);
    Article? GetArticle(int id);
    IEnumerable<Article> GetAllArticles();
    Article? UpdateArticle(Article article);
    bool DeleteArticle(int id);
}