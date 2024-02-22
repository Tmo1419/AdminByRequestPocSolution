using ProductCatalogService.Core.Domain.Entities;

namespace ProductCatalogService.Core.Business.Interfaces
{
    public interface IProductRepository
    {
        //Task<Product> GetProductByIdAsync(int Id);
        //Task<IReadOnlyList<Product>> GetListOfproducts();

        Task<Product> GetAsync(int? categoryId);

        Task<List<Product>> GetAllAsync();

        Task<Product> CreateAsync(Product product);

        Task DeleteAsync(int productId);

        Task UpdateAsync(Product product);
    }
}
