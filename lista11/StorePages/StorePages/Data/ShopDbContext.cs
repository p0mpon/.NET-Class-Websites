using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StorePages.Models;

namespace StorePages.Data;

public class ShopDbContext : IdentityDbContext {
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
}