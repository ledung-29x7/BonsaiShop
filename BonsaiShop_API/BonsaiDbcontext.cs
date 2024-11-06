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

        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<GardenImage> GardenImages { get; set; }
    }
}
