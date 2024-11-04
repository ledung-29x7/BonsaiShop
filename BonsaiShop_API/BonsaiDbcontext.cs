using BonsaiShop_API.Areas.Garden.Models;
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
        }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }


    }
}
