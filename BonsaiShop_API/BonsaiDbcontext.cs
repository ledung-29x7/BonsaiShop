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


    }
}
