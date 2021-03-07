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
            _collection = database.GetCollection<T>($"{typeof(T).Name}");

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, id);
            var data = await _collection.FindAsync(filter);
            return data.FirstOrDefault();
           
        }
    }
}
