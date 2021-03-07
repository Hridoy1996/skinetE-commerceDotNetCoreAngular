using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    [BsonIgnoreExtraElements]

    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }

    }
}