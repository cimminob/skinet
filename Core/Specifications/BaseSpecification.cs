using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{


    public class BaseSpecification<T> : ISpecification<T>
    {
        /* System.LINQ.Expression Provides the base class from which the classes 
        that represent expression tree nodes are derived.  

        Func<T,TResult> is a delegate that points to a method that accepts one 
        or more arguments and returns a value
         */
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }
        public BaseSpecification()
        {

        }

        //the criteria 
        public Expression<Func<T, bool>> Criteria { get; }

        //create a list of queries
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        //add another query to the list of queries
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}