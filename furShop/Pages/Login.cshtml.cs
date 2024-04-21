using System.Linq;
using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace furShop.Pages;
[IgnoreAntiforgeryToken]
public class Login : PageModel
{
    public static string Message { get; private set; } = "";
    ApplicationContext context;
    public List<User> Users { get; private set; } = new();
    public Login(ApplicationContext db)
    {
        context = db;
    }

    public IActionResult OnPost(string login, string password)
    {
        // Поиск пользователя по логину и паролю
        var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

        if (user != null)
        {
            // Если пользователь найден, отправить сообщение с его именем
            TempData["WelcomeMessage"] = $"Добро пожаловать, {user.Name}!";
            return RedirectToPage("/Index"); // Перенаправить на страницу сайта
        }
        else
        {
            // Если пользователь не найден, отправить сообщение об ошибке
            Message = "Неправильный логин или пароль.";
            return Page();
        }
    }

    public void OnGet()
    {
        Message = "Введите Логин и Пароль";
    }
}