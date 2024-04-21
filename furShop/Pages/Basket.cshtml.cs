using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace furShop.Pages;

public class Basket : PageModel
{
    ApplicationContext context;
    public static string Message { get; private set; } = "";

    public static List<Cart> Carts { get; set; } = new();
    
    public Basket(ApplicationContext db)
    {
        context = db;
    }

    public IActionResult OnPost(string itemName, int itemPrice, int itemId)
    {
        Carts = context.Carts.ToList(); // Пересчитываем корзину, если она уже существует
        var cart = context.Carts.FirstOrDefault(c => c.Id == itemId);
        if (cart != null)
        {
            context.Carts.Remove(cart);
            context.SaveChanges();
            Message = "Товар удалён из корзины";
            return Page();
        }
        else
        {
            return Page();
        }
        return Page();
    }
    public void OnGet()
    {
        Message = "";
        Carts = context.Carts.ToList();
    }
}