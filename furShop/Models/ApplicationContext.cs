using Microsoft.EntityFrameworkCore;

namespace furShop.Models;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Furniture> Furnitures { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
}