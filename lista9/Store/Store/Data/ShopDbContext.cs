using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data;

public class ShopDbContext : DbContext {
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
}