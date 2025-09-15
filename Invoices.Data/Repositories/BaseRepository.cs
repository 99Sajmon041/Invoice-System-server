using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Obecné (generické) úložiště pro práci s entitami v databázi
    /// Poskytuje základní CRUD operace pro libovolný typ T
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T? FindById(int id)
        {
            // Find využívá EF cache – rychlé, ale nelze použít s dalšími podmínkami nebo Include()
            return dbSet.Find(id);
        }

        public T Add(T entity)
        {
            // Entita se přidá do DbContextu, ale změny je třeba uložit SaveChanges()
            dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public bool ExistsWithId(int id)
        {
            return dbSet.Find(id) is not null;
        }
    }
}