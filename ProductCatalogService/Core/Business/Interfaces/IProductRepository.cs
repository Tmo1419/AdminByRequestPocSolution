using ProductCatalogService.Core.Domain.Entities;

namespace ProductCatalogService.Core.Business.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int Id);
        Task<IReadOnlyList<Product>> GetListOfproducts();
    }
}
