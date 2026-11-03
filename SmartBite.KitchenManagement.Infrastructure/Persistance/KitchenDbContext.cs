using Microsoft.EntityFrameworkCore;
using SmartBite.KitchenManagement.Domain.Entities;

namespace SmartBite.KitchenManagement.Infrastructure.Persistance
{
    public class KitchenDbContext(DbContextOptions<KitchenDbContext> options) : DbContext(options)
    {
        public DbSet<Kitchens> Kitchens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Kitchens>().HasQueryFilter(k => k.IsActive);
            builder.Entity<Kitchens>().HasKey(k => k.KitchenId).HasName("PK_Kitchens_KitchenId");
        }
    }
}
