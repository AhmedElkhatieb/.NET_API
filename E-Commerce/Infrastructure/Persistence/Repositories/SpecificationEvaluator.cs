using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class SpecificationEvaluator
    {
        public static IQueryable<T> BuildQuery<T>(IQueryable<T> inputQuery, Specifications<T> specifications) where T : class
        {
            // Step 01
            var query = inputQuery;
            // Step 02 
            if (specifications.Criteria is not null)
            {
                query = query.Where(specifications.Criteria);
            }
            // Step 03 For Loop (Aggregate Expressions)
            //foreach(var item in specifications.IncludeExpression)
            //{
            //    query = query.Include(item);
            //}
            //// or use linq
            query = specifications.IncludeExpression.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            else if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            return query;
        }
    }
}
