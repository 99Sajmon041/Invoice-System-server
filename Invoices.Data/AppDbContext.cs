using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    /// <summary>
    /// Entity Framework Core databázový kontext pro aplikaci Invoices.
    /// Obsahuje DbSety hlavních entit (Person, Invoice) a konfiguraci modelu (indexy, FK vztahy).
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Vytvoří instanci kontextu s dodanými možnostmi (např. connection string, provider).
        /// </summary>
        /// <param name="options">Možnosti konfigurace DbContextu (předávané typicky v DI).</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Kolekce osob (subjekty s IČO atd.).
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Kolekce faktur (doklady mezi prodávajícím a kupujícím).
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Konfigurace modelu. Nastavuje indexy, konverze typů a relace mezi entitami.
        /// </summary>
        /// <param name="modelBuilder">Builder pro konfiguraci EF Core modelu.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PERSON ------------------------------------------------------------
            modelBuilder.Entity<Person>(builder =>
            {
                // Uloží enum Country jako string (čitelnost/nezávislost na pořadí enumu)
                builder.Property(p => p.Country).HasConversion<string>();

                // Uživatelské indexy pro rychlé filtrování a dotazy
                builder.HasIndex(p => p.IdentificationNumber);
                builder.HasIndex(p => p.Hidden);
            });

            // INVOICE -----------------------------------------------------------
            modelBuilder.Entity<Invoice>(builder =>
            {
                // Index na číslo faktury (časté vyhledávání/validace duplicit)
                builder.HasIndex(i => i.InvoiceNumber);

                // Vztah: Invoice.BuyerId -> Person.Purchases (1:N)
                builder.HasOne(i => i.Buyer)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(i => i.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction) // žádné kaskádní mazání, držíme integritu ručně
                    .IsRequired();

                // Vztah: Invoice.SellerId -> Person.Sales (1:N)
                builder.HasOne(i => i.Seller)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(i => i.SellerId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });
        }
    }
}
