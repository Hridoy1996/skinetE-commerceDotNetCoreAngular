using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductType : BaseEntity
    {
        public  string  Name { get; set; }
    }
}