
using BonsaiShop_API.Areas.Admin.Models;
using BonsaiShop_API.Areas.Auther.Model;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Garden> Gardens { get; set; }
        public DbSet<GardenImages> GardenImages { get; set; }
        public DbSet<PlantImages> PlantImages { get; set; }
        public DbSet<Plant> Plants { get; set; }

    }
}
