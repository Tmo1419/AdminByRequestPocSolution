﻿namespace ProductCatalogService.Presentation.DTOs.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
    }
}
