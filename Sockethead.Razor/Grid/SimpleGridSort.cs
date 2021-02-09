﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sockethead.Razor.Grid
{
    public enum SortOrder
    { 
        Ascending,
        Descending,
    }

    public class SimpleGridSort<T> where T : class
    {
        public Expression<Func<T, string>> Expression { get; set; }

        public bool IsSortable => Expression != null;

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public SortOrder SortOrderFlipped => Flip(SortOrder);

        public static SortOrder Flip(SortOrder sortOrder)
            => sortOrder switch
            {
                SortOrder.Ascending => SortOrder.Descending,
                SortOrder.Descending => SortOrder.Ascending,
                _ => throw new ArgumentException($"Unexpected SortOrder {sortOrder}"),
            };

        public SimpleGridSort<T> Flip()
        {
            SortOrder = SortOrderFlipped;
            return this;
        }

        public IQueryable<T> ApplyTo(IQueryable<T> source, bool isThenBy)
        {
            if (Expression == null)
                return source;

            return isThenBy && source is IOrderedQueryable<T> orderedSource
                ? SortOrder switch
                {
                    SortOrder.Ascending => orderedSource.ThenBy(Expression),
                    SortOrder.Descending => orderedSource.ThenByDescending(Expression),
                    _ => throw new ArgumentException($"Unexpected SortOrder {SortOrder}"),
                }
                :
                SortOrder switch
                {
                    SortOrder.Ascending => source.OrderBy(Expression),
                    SortOrder.Descending => source.OrderByDescending(Expression),
                    _ => throw new ArgumentException($"Unexpected SortOrder {SortOrder}"),
                };
        }
    }
}
