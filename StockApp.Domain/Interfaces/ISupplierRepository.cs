using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetById(int? id);
        Task<Supplier> Create(Supplier supplier);
        Task<Supplier> Update(Supplier supplier);
        Task<Supplier> Remove(Supplier supplier);
        Task<IEnumerable<Supplier>> SearchAsync(string name, string contactEmail, string phoneNumber);
    }
}
