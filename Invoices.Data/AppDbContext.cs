using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(p => p.Country).HasConversion<string>();
                builder.HasIndex(p => p.IdentificationNumber);
                builder.HasIndex(p => p.Hidden);
            });

            modelBuilder.Entity<Invoice>(builder =>
            {
                builder.HasIndex(i => i.InvoiceNumber);

                builder.HasOne(i => i.Buyer)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(i => i.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                builder.HasOne(i => i.Seller)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(i => i.SellerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });
        }
    }
}