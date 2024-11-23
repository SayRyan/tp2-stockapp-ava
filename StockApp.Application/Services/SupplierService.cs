using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private ISupplierRepository _supplierRepository;
        private IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> Add(SupplierDTO supplierDto)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.Create(supplierEntity);
            return supplierEntity;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            var suppliersEntity = await _supplierRepository.GetSuppliers();
            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliersEntity);
        }

        public async Task<SupplierDTO> GetSupplierById(int? id)
        {
            var supplierEntity = await _supplierRepository.GetById(id);
            return _mapper.Map<SupplierDTO>(supplierEntity);
        }

        public async Task Remove(int? id)
        {
            var supplierEntity = _supplierRepository.GetById(id).Result;
            await _supplierRepository.Remove(supplierEntity);
        }

        public async Task Update(SupplierDTO supplierDto)
        {
            var supplierEntity = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.Update(supplierEntity);
        }

        public async Task<IEnumerable<SupplierDTO>> SearchSuppliersAsync(string name, string contactEmail, string phoneNumber)
        {
            var suppliers = await _supplierRepository.SearchAsync(name, contactEmail, phoneNumber);

            return suppliers.Select(supplier => new SupplierDTO
            {
                Name = supplier.Name,
                ContactEmail = supplier.ContactEmail,
                PhoneNumber = supplier.PhoneNumber
            });
        }
    }
}
