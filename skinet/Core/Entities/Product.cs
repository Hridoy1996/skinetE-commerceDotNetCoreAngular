using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
  [BsonIgnoreExtraElements]
  public class Product: BaseEntity
  {
        public string Name {get; set;}
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public string ProductBrandId { get; set; }

    }
}