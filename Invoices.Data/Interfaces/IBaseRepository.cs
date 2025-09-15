namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Generické rozhraní pro repozitář entit.
    /// Poskytuje základní CRUD operace a společné metody pro práci s databází.
    /// </summary>
    /// <typeparam name="T">Typ entity, se kterou repozitář pracuje.</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Vrátí všechny entity daného typu.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Vyhledá entitu podle jejího ID.
        /// </summary>
        /// <param name="id">ID entity.</param>
        /// <returns>Entita nebo <c>null</c>, pokud nebyla nalezena.</returns>
        T? FindById(int id);

        /// <summary>
        /// Přidá novou entitu do databáze (je nutné zavolat <see cref="SaveChanges"/> pro uložení).
        /// </summary>
        /// <param name="entity">Nová entita.</param>
        /// <returns>Vrací přidanou entitu.</returns>
        T Add(T entity);

        /// <summary>
        /// Aktualizuje existující entitu v databázi.
        /// </summary>
        /// <param name="entity">Entita s novými hodnotami.</param>
        /// <returns>Aktualizovaná entita.</returns>
        T Update(T entity);

        /// <summary>
        /// Smaže entitu z databáze.
        /// </summary>
        /// <param name="entity">Entita určená ke smazání.</param>
        void Delete(T entity);

        /// <summary>
        /// Uloží všechny změny provedené v DbContextu do databáze.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Ověří, zda v databázi existuje entita s daným ID.
        /// </summary>
        /// <param name="id">ID entity.</param>
        /// <returns><c>true</c>, pokud existuje; jinak <c>false</c>.</returns>
        bool ExistsWithId(int id);
    }
}
