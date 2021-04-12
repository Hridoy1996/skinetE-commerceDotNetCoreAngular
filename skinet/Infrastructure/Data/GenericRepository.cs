using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
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

        public async Task<List<T>> GetAllAsync()
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

       

        public async Task<IReadOnlyList<T>> ListDescAsync(Expression<Func<T, object>> fieldName)
        {
            var filter = Builders<T>.Filter.Empty;
            var sort = Builders<T>.Sort.Descending(fieldName);
            return await _collection.Find(filter).Sort(sort).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAscAsync(Expression<Func<T, object>> fieldName, Expression<Func<T, bool>> criteria , int? skip , int? limit)
        {
            var filter = Builders<T>.Filter.Empty;
 
            var sort = Builders<T>.Sort.Ascending(fieldName);

            return await _collection.Find(criteria).Sort(sort).Skip(skip).Limit(limit).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            var filter = Builders<T>.Filter.Empty;
            return await _collection.Find(criteria).ToListAsync();
        }
        public async Task AddAsync(T entity)
        {

            entity.Id = Guid.NewGuid().ToString();
            await  _collection.InsertOneAsync(entity);
            
        }
    }
}
