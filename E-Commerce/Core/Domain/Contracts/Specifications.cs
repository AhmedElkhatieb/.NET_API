﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
        protected Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpression { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy {  get; private set; }
        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Skip {  get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> expression)
            => IncludeExpression.Add(expression);
        protected void SetOrderBy(Expression<Func<T, object>> expression)
           => OrderBy = expression;
        protected void SetOrderByDesc(Expression<Func<T, object>> expression)
           => OrderByDesc = expression;

        protected void ApplyPagination (int pageIndex, int pageSize)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}
