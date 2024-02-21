using Microsoft.EntityFrameworkCore;
using ProductCatalogService.Core.Business.Interfaces;
using ProductCatalogService.Core.Domain.Entities;
using ProductCatalogService.Infrastructure.Data;

namespace ProductCatalogService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ApplicationDbContext _StoreContext;

        public ProductRepository(ApplicationDbContext storeContext)
        {
            _StoreContext = storeContext;
        }
        public async Task<IReadOnlyList<Product>> GetListOfproducts()
        {
            return _StoreContext.Products
                .ToList();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var obj = await _StoreContext.Products
                 .FirstOrDefaultAsync(x => x.Id == Id);
            return obj;
        }
    }
}
