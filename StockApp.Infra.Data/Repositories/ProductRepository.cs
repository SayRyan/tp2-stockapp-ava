using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StockApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
        {
            _productContext = context;
            _logger = logger;
        }

        public async Task<Product> Create(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(p => p.Id == id, CancellationToken.None);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogInformation("Teste de log");
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> SearchAsync(string name, decimal? minPrice, decimal? maxPrice)
        {
            var query = _productContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }
    }
}
