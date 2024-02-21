namespace ProductCatalogService.Core.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int CategoryId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? PhotoLink { get; set; }
        public DateTime? ReleaseDate { get; set; }

    }
}
