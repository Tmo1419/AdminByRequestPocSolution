using MongoDB.Bson;

namespace ProductCatalogService.Core.Domain.Entities
{
    public class Product2
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; } = null!;
        public List<string> Features { get; set; } = null!;

        public Product2(string title, List<string> featureIds)
        {
            Title = title;
            Features = featureIds;
        }
    }
}
