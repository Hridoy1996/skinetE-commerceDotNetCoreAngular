using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket cusomerBasket)
        {
            var created = await _database.StringSetAsync(cusomerBasket.Id,
                JsonSerializer.Serialize(cusomerBasket), TimeSpan.FromDays(10));
            
            if (!created) return null;
     
            return await GetBasketAsync(cusomerBasket.Id);
            
        }
    }
}
