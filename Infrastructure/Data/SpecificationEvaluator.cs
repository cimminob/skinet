using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //TEntity would be a product and query is a query of product. This function returns a query that matches
        //the criteria 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria!=null)
            {
                //query is filtered based on spec.Criteria - fetch a product that matches the criteria
                query = query.Where(spec.Criteria); // for example spec.Criteria could be p=>p.ProductTypeId == id
            }

            if (spec.OrderBy!=null)
            {
                query = query.OrderBy(spec.OrderBy); 
            }

            if (spec.OrderByDescending!=null)
            {
          
                query = query.OrderByDescending(spec.OrderByDescending); 
            }

            if (spec.IsPagingEnabled){
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //combines the list of includes queries into one query
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}