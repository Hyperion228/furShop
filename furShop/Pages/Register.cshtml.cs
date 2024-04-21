using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace furShop.Pages;

[IgnoreAntiforgeryToken]
public class Register : PageModel
{
    public static string Message { get; private set; } = "";
    ApplicationContext context;
    [BindProperty]
    public User Person { get; set; } = new();
    
    public Register(ApplicationContext db)
    {
        context = db;
    }
    public async Task<IActionResult> OnPostAsync(string login, string password)
    {
        // Поиск пользователя по логину и паролю
        var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

        if (user != null)
        {
            // Если пользователь найден, отправить сообщение с его именем
            Message = $"Такой пользователь уже существует!";
            return Page();
        }
        else
        {
            // Если пользователь не найден, отправить сообщение об ошибке
            Message = "Пользователь заргистрирован";
            context.Users.Add(Person);
            await context.SaveChangesAsync();
            return Page ();
        }
        
    }
    public void OnGet()
    {
        Message = "Зарегистрируйте нового пользователя";
    }
}