namespace ProductCatalogService.Presentation.DTOs.Product
{
    public class CreateProductDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
