using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        ApplicationDbContext _supplierContext;
        public SupplierRepository(ApplicationDbContext context)
        {
            _supplierContext = context;
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            _supplierContext.Add(supplier);
            await _supplierContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> GetById(int? id)
        {
            var supplier = await _supplierContext.Suppliers.FindAsync(id);
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierContext.Suppliers.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            _supplierContext.Update(supplier);
            await _supplierContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> Remove(Supplier supplier)
        {
            _supplierContext.Remove(supplier);
            await _supplierContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> SearchAsync(string name, string contactEmail, string phoneNumber)
        {
            var query = _supplierContext.Suppliers.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }

            return await query.ToListAsync();
        }
    }
}
