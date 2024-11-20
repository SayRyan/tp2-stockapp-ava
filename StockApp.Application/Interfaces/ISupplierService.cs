using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetSuppliers();
        Task<SupplierDTO> GetSupplierById(int? id);
        Task<Supplier> Add(SupplierDTO supplierDto);
        Task Update(SupplierDTO supplierDto);
        Task Remove(int? id);
    }
}
