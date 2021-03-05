using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;

namespace Core.Entities
{
  [BsonIgnoreExtraElements]
  public class Product
  {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
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