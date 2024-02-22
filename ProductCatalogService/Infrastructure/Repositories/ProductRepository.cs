using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Core.Business.Interfaces;
using ProductCatalogService.Core.Domain.Entities;
using ProductCatalogService.Infrastructure.Data;

namespace ProductCatalogService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task<Product?> GetAsync(int? productId)
        {
            if (productId == null)
            {
                return null;
            }
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await this._context.AddAsync(product);
            await this._context.SaveChangesAsync();

            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await GetAsync(productId);

            if (product is null)
            {
                throw new Exception($"ProductID {productId} is not found.");
            }
            this._context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
