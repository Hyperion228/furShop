using furShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace furShop.Pages;

public class Catalog : PageModel
{
    ApplicationContext context;

    public Catalog(ApplicationContext context)
    {
        context = context;
    }
    public async Task<IActionResult> OnPostAsync(Furniture newItem, IFormFile image)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (image != null && image.Length > 0)
        {
            var imagePath = "/images/" + Guid.NewGuid() + "_" + image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
        
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            newItem.ImagePath = imagePath;
        }

        context.Furnitures.Add(newItem);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
    public void OnGet()
    {
        
    }
}