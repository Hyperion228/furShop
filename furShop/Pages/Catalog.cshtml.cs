using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace furShop.Pages;
public class Catalog : PageModel
{
    public static string Message { get; private set; } = "";
    ApplicationContext context;
    public static List<Furniture> Furnitures { get; private set; } = new();
    public List<Cart> Carts { get; private set; } = new();

    public Catalog(ApplicationContext db)
    {
        context = db;
    }

    public void OnPost(int itemId, string itemName, int itemPrice)
    {
        Furnitures = context.Furnitures.ToList(); // Получаем список всех товаров из БД
        var furn = context.Furnitures.FirstOrDefault(u => u.Id == itemId);
        if (furn != null)
        {
            var cart = new Cart
            {
                Title = furn.Name,
                Price = furn.Price
            };
            // Если пользователь найден, отправить сообщение с его именем
            Message = "Товар добавлен в корзину";
            context.Carts.Add(cart);
            context.SaveChanges();
            //return Page();
        }
        else
        {
            // Если пользователь не найден, отправить сообщение об ошибке
            Message = "Товар не добавлен в корзину";
            //return Page();
        }
    }
    public void OnGet()
    {
        Message = "";
        Furnitures = context.Furnitures.ToList(); // Получаем список всех товаров из БД
    }
}