using Microsoft.EntityFrameworkCore;
using projetPanier.Models;

namespace projetPanier.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produit> Products { get; set; }
    }
}
