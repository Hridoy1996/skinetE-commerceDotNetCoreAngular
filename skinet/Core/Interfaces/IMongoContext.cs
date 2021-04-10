using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}