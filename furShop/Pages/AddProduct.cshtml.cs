using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace furShop.Pages;

public class AddProduct : PageModel
{
    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _environment;

    public AddProduct(ApplicationContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }


    public IFormFile ImageFile { get; set; }

    public string Message { get; set; }

    public IActionResult OnPost(string name, string description, int price)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }

            Furniture newFurniture = new Furniture
            {
                Name = name,
                Description = description,
                Price = price,
                ImagePath = fileName // Сохраняем имя файла, а не base64 строку
            };

            _context.Furnitures.Add(newFurniture);
            _context.SaveChanges();

            Message = "Товар добавлен";
        }
        else
        {
            Message = "Пожалуйста, добавьте фото";
        }

        return Page();
    }
    public void OnGet()
    {
        Message = "";
    }
}