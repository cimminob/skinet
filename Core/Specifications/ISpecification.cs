using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{

    /* Specification Pattern resolves the problems of a generic repository
    having non specific methods that accept an Expression as a parameter.
    An Expression is basically pointless because its so non specific. Looking
    at the code its hard to understand the purpose. 

    Specification Pattern describes a query in an object and returns an
    IQueryable<T>. A specification can have a meaninfulname such as
    ProductsWithTypesAndBrandsSpecification
    */
    public interface ISpecification<T>
    {

        /* System.LINQ.Expression Provides the base class from which the classes 
        that represent expression tree nodes are derived.  

        Func<T,TResult> is a delegate that points to a method that accepts one 
        or more arguments and returns a value
        */

        //represents an expression to test
        Expression<Func<T, bool>> Criteria { get; }

        //a list of Includes statements that represent queries
        List<Expression<Func<T, object>>> Includes { get; }

        //ex: order by name
        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get;  }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}