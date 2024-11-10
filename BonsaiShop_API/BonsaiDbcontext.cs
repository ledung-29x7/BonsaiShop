using BonsaiShop_API.Areas.Garden.Models;
using BonsaiShop_API.Areas.User.Models;
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
            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne()
            .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Garden> Gardens { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<GardenImages> GardenImages { get; set; }


    }
}
