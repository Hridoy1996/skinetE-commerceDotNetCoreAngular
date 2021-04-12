﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using MongoDB.Bson;
namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity 
    {
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> criteria);

        Task<IReadOnlyList<T>> ListAscAsync(Expression<Func<T, object>> filterExpression, Expression<Func<T, bool>> criteria , int? pageIndex , int? pageSize   );
        Task<IReadOnlyList<T>> ListDescAsync(Expression<Func<T, object>> filterExpression);

        Task AddAsync(T entity);
        // bool UpdateWithFilterAsync(FilterDefination T)
    }
}
