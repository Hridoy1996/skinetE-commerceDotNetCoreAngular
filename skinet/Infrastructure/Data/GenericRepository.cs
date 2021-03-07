using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private IMongoCollection<Product> Product;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IMongoClient client)
        {
            _client = client;
            database = client.GetDatabase("skinet_db");
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));

        }

        [AttributeUsage(AttributeTargets.Class, Inherited = false)]
        public class BsonCollectionAttribute : Attribute
        {
            public string CollectionName { get; }

            public BsonCollectionAttribute(string collectionName)
            {
                CollectionName = collectionName;
            }
        }
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
        /
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            _collection = database.GetCollection<T>(GetCollectionName(typeof(TDocument)));

            var x =  repository.GetCollection<T>($"{T}");
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
