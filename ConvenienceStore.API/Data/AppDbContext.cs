using ConvenienceStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvenienceStore.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
