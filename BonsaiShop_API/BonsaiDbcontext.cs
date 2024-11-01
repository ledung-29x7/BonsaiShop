using BonsaiShop_API.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace BonsaiShop_API
{
    public class BonsaiDbcontext : DbContext
    {
        public BonsaiDbcontext(DbContextOptions<BonsaiDbcontext> options):base(options) {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categories>().HasKey(id=>id.CategoryId);
            modelBuilder.Entity<Users>().HasKey(id =>id.UserId);
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public object RevenueDetails { get; internal set; }
    }
}
