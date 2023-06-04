using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Base;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Persistence.Contexts
{
    public class EcommerceAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public EcommerceAPIDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                   .HasKey(b => b.Id);

            builder.Entity<Order>()
                   .HasIndex(o => o.OrderCode)
                   .IsUnique();

            builder.Entity<Basket>()
                   .HasOne(b => b.Order)
                   .WithOne(o => o.Basket)
                   .HasForeignKey<Order>(b => b.Id);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker - is the property that enables the capture of the changes made on the Entities or the newly added data. It allows us to capture and obtain the data tracked in update operations.

            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
