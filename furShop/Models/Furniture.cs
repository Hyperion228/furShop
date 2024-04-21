namespace furShop.Models;

public class Furniture
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string ImagePath { get; set; } // Путь к изображению товара
}
