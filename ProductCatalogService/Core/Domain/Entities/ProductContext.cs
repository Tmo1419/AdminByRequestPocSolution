using Microsoft.EntityFrameworkCore;

namespace ProductCatalogService.Core.Domain.Entities
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
