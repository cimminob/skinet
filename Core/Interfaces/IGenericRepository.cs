using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    /*
        IGenericRepository represents a repository of any object that is or 
        derives from BaseEntity. 
    */
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        //System.Collections.IReadOnlyList Represents a read-only collection of elements 
        //that can be accessed by index
        Task<IReadOnlyList<T>> ListAllAsync();

        //Get entity with specification
        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        //list of entities with specification
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        //counts the number of items
        Task<int> CountAsync(ISpecification<T> spec);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}