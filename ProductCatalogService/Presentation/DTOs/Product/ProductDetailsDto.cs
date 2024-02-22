namespace ProductCatalogService.Presentation.DTOs.Product
{
    public class ProductDetailsDto
    {
        public int CategoryId { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string PhotoLink { get; set; } = string.Empty;
        
        public DateTime? ReleaseDate { get; set; } = DateTime.Today;
        
        public decimal? Price { get; set; }

        public int Id { get; set; }
    }
}
