using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Specifications;

namespace Infrastructure.Data
{
    public class SpecificationEvalutor<T> where T: BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, 
            ISpecification<T> spec)
        {
            var query = inputQuery;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);

            }
            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);

            }
            if(spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);

            }


            return query;
        }
    }
}
