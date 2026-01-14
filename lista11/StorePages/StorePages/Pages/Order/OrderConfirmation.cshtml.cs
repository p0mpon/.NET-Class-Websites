using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StorePages.Pages.Order
{
    public class OrderConfirmationModel : PageModel
    {
        public string? CustomerName { get; set; }
        public string? PaymentMethod { get; set; }

        public void OnGet()
        {
            CustomerName = TempData["CustomerName"] as string;
            PaymentMethod = TempData["PaymentMethod"] as string;
        }
    }
}