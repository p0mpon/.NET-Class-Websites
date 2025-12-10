using Store.Models;

namespace Store.Data;

public class DictionaryArticlesContext : IArticlesContext
{
    private readonly Dictionary<int, Article> _articles = new Dictionary<int, Article>();
    private int _nextId = 1;

    public void AddArticle(Article article)
    {
        article.Id = _nextId++;
        _articles.Add(article.Id, article);
    }

    public Article? GetArticle(int id)
    {
        return _articles.GetValueOrDefault(id);
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _articles.Values;
    }

    public Article? UpdateArticle(Article article)
    {
        if (_articles.ContainsKey(article.Id))
        {
            _articles[article.Id] = article;
            return _articles[article.Id];
        }
        
        return null;
    }

    public bool DeleteArticle(int id)
    {
        return _articles.Remove(id);
    }
}