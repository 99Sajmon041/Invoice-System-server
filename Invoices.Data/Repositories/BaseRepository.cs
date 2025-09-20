using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
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
            return dbSet.AsNoTracking().ToList();
        }

        public T? FindById(int id)
        {
            return dbSet.Find(id);
        }

        public T Add(T entity)
        {
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