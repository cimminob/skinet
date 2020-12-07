using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

/*
 A repository is an abstraction that provides access to DbContext(StoreContext)
 and a generic repository can be used for various types of entities without creating
various types of repositories. 

 A Generic Repository is considered a bad pattern, but it is remedied with the 
 Specification Pattern, which describes a query object as a specification.


- ISpecification<T> defines a Criteria expression and a collection of Include expressions. 
Criteria is for storing our filtering expression and Includes is for storing our expressions 
that determine what data we want to include in the query. Note that expressions are nothing 
more than filter criteria you pass into LINQ methods, e.g. Where(p => p.Description == "Test")

- BaseSpecification<T> is the concrete implementation of our interface. It defines Criteria 
and Includes as properties and an additional method for adding include expressions to the 
Includes collection.

- SpecificationEvaluator<T> and the GetQuery method is used for *applying the filtering and 
includes expressions to our query. If a criteria expression exists, it is *applied with a 'Where', 
following by any Include expressions. *Note the query is not actually executed at this point. 
To execute the query requires calling an EF core execution method such as .ToList()

- The ApplySpecification method in the GenericRepository<T> calls the GetQuery method, passing in
 the 'raw' data retrieved from the db context as an IQueryable of a generic dbset. 
 Note that _context.Set<T>() (Assume T = Product) is exactly the same as _context.Products 
 and allows the generic nature of this implementation. We also pass in the 'spec' which 
 defines the filter criteria and additional data to include.
*/

namespace Infrastructure.Data
{

    
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        //inject the StoreContext
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /*make use of specification pattern to accept a specific query
          on an entity because Include cannot be used here. 
          Ex: Cannot use Include( x => x.productId = id) because type is
          unknown
        */
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }


        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        //returns a queryable that matches the specification criteria. The queryable can 
        //then executed on a database in the above function calls.
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            //T would be a product and is converted into a Queryable
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}