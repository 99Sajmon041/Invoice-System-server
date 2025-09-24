using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Základní generický repozitář poskytující CRUD operace nad entitami.
    /// </summary>
    /// <typeparam name="T">Typ entity.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Databázový kontext aplikace.
        /// </summary>
        protected readonly AppDbContext context;

        /// <summary>
        /// Sada entit pro typ <typeparamref name="T"/>.
        /// </summary>
        protected readonly DbSet<T> dbSet;

        /// <summary>
        /// Inicializuje novou instanci <see cref="BaseRepository{T}"/> s daným kontextem.
        /// </summary>
        /// <param name="context">Databázový kontext.</param>
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// <summary>
        /// Načte všechny entity bez sledování změn.
        /// </summary>
        /// <returns>Kolekce všech entit typu <typeparamref name="T"/>.</returns>
        public IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Vyhledá entitu podle identifikátoru.
        /// </summary>
        /// <param name="id">Identifikátor entity.</param>
        /// <returns>Nalezená entita nebo <c>null</c>, pokud neexistuje.</returns>
        public T? FindById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Přidá novou entitu do kontextu.
        /// </summary>
        /// <param name="entity">Přidávaná entita.</param>
        /// <returns>Přidaná entita.</returns>
        public T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        /// <summary>
        /// Aktualizuje existující entitu v kontextu.
        /// </summary>
        /// <param name="entity">Aktualizovaná entita.</param>
        /// <returns>Aktualizovaná entita.</returns>
        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }

        /// <summary>
        /// Odstraní entitu z kontextu.
        /// </summary>
        /// <param name="entity">Entita určená k odstranění.</param>
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Uloží provedené změny do databáze.
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Ověří existenci entity s daným identifikátorem.
        /// </summary>
        /// <param name="id">Identifikátor entity.</param>
        /// <returns><c>true</c>, pokud entita existuje; jinak <c>false</c>.</returns>
        public bool ExistsWithId(int id)
        {
            return dbSet.Find(id) is not null;
        }
    }
}
