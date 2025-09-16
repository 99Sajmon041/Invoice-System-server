using Invoices.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Interfaces
{
    /// <summary>
    /// Specializované rozhraní pro repozitář pracující s entitou <see cref="Invoice"/>.
    /// Rozšiřuje obecné metody z <see cref="IRepository{T}"/> o specifické funkce pro faktury.
    /// </summary>
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {

    }
}
