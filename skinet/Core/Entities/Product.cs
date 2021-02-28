using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
  [BsonIgnoreExtraElements]
  public class Product
  {
    public string Id {get; set;}
    public string Name {get; set;}
  }
}