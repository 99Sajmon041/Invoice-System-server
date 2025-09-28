namespace Invoices.Data.Entities.Enums
{
    /// <summary>
    /// Výčet oprávnění (autorizační atributy) na end-pointy
    /// </summary>
    public enum Policy
    {
        /// <summary>
        /// Právo na úpravu, vložení
        /// </summary>
        CanWrite,
        /// <summary>
        /// Právo na úpravu, vložení a mazání
        /// </summary>
        CanDelete
    }
}
