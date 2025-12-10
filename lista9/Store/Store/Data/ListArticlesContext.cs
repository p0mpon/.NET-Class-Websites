using Store.Models;

namespace Store.Data;

public class ListArticlesContext : IArticlesContext
{
    private readonly List<Article> _articles = new List<Article>();
    private int _nextId = 1;

    public void AddArticle(Article article)
    {
        article.Id = _nextId++;
        _articles.Add(article);
    }

    public Article? GetArticle(int id)
    {
        return _articles.FirstOrDefault(a => a.Id == id);
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _articles;
    }

    public Article? UpdateArticle(Article article)
    {
        int index = _articles.FindIndex(a => a.Id == article.Id);
        if (index != -1)
        {
            _articles[index] = article;
            return _articles[index];
        }

        return null;
    }

    public bool DeleteArticle(int id)
    {
        int index = _articles.FindIndex(a => a.Id == id);
        if (index != -1)
        {
            _articles.RemoveAt(index);
            return true;
        }
        
        return false;
    }
}