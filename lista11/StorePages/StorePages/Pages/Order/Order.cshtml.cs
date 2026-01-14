using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StorePages.Data;
using StorePages.Models;

namespace StorePages.Pages.Order
{
    [Authorize(Policy = "ConsumerOnly")]
    public class OrderModel : PageModel
    {
        private readonly ShopDbContext _context;

        public OrderModel(ShopDbContext context) => _context = context;

        public CartViewModel Cart { get; set; } = new CartViewModel();

        [BindProperty]
        public OrderInfo Input { get; set; } = new();

        public async Task OnGetAsync()
        {
            foreach (var cookie in Request.Cookies)
            {
                if (cookie.Key.StartsWith("prod"))
                {
                    if (int.TryParse(cookie.Key.Replace("prod", ""), out int id))
                    {
                        var article = await _context.Articles.FindAsync(id);
                        if (article != null)
                        {
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

        public IActionResult OnPost()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                if (cookie.StartsWith("prod"))
                {
                    Response.Cookies.Delete(cookie);
                }
            }

            TempData["CustomerName"] = Input.FullName;
            TempData["PaymentMethod"] = Input.PaymentMethod;

            return RedirectToPage("/Order/OrderConfirmation");
        }
    }

    public class OrderInfo
    {
        public string FullName { get; set; } = "";
        public string Address { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
    }
}