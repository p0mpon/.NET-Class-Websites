using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Cart;

public class CartModel : PageModel
{
    private readonly ShopDbContext _context;

    public CartModel(ShopDbContext context) => _context = context;

    public CartViewModel Cart { get; set; } = new CartViewModel();

    public async Task OnGetAsync()
    {
        foreach (var cookie in Request.Cookies)
        {
            if (cookie.Key.StartsWith("prod"))
            {
                if (int.TryParse(cookie.Key.Replace("prod", ""), out int id))
                {
                    var article = await _context.Articles.FindAsync(id);
                    if (article == null) {
                        Response.Cookies.Delete(cookie.Key);
                    } else {
                        Cart.Items.Add(new CartItem
                        {
                            ArticleId = id,
                            Name = article.Name,
                            Price = article.Price,
                            Quantity = int.Parse(cookie.Value)
                        });
                    }
                }
            }
        }
    }

    public IActionResult OnGetUpdateQuantity(int id, int delta)
    {
        string cookieName = "prod" + id;
        if (Request.Cookies.TryGetValue(cookieName, out string value))
        {
            int newQuantity = int.Parse(value) + delta;
            
            if (newQuantity <= 0) 
            {
                Response.Cookies.Delete(cookieName);
            }
            else 
            {
                Response.Cookies.Append(cookieName, newQuantity.ToString(), 
                    new CookieOptions { Expires = DateTime.Now.AddDays(7) });
            }
        }
        return RedirectToPage();
    }
}