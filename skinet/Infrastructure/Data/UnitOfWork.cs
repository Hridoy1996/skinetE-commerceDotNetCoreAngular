using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        private Hashtable _repositories;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }
        /*
        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }
        */
      

        public void Dispose()
        {
            _context.Dispose();
        }
      
        public Task<int> Complete()
        {
            return _context.SaveChanges();
        }


        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }
            _repositories["Product"] =  "Infrastructure.Data.GenericRepository<Core.Entities.Product>" ;
            return (IGenericRepository<T>)_repositories[type];
        }


    }

}

