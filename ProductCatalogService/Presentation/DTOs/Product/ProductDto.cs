namespace ProductCatalogService.Presentation.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;
        
        public string Title { get; set; } = string.Empty;
        //public int Qty { get; set; }
        
        public decimal Price { get; set; }
    }
}
