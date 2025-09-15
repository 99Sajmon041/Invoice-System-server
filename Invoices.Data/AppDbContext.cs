using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    /// <summary>
    /// Hlavní třída pro práci s databází pomocí Entity Framework Core
    /// Obsahuje definice tabulek a konfigurace entit
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Konstruktor, který přijímá možnosti konfigurace databáze
        /// Tuto třídu zaregistrujeme v Program.cs pro Dependency Injection
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Tabulka osob (pojištěnců).
        /// DbSet představuje kolekci entit v databázi
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Místo, kde se definují specifické konfigurace pro jednotlivé entity
        /// Volá se automaticky při vytváření modelu
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurace entity Person:
            modelBuilder.Entity<Person>(builder =>
            {
                // Enum Country se ukládá do databáze jako řetězec místo číselné hodnoty
                builder.Property(p => p.Country)
                       .HasConversion<string>();

                builder.HasIndex(p => p.IdentificationNumber);

                // Přidáme index na sloupec Hidden – zrychlí filtrování skrytých osob
                builder.HasIndex(p => p.Hidden);
            });
        }
    }
}
