using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
  [BsonIgnoreExtraElements]
  public class Product
  {
    public string Id {get; set;}
    public string Name {get; set;}
  }
}