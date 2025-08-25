using DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence;
using DoubleVPartners.BackEnd.Domain.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Expressions;

namespace DoubleVPartners.BackEnd.Infrastructure.Common.Persistence
{
    public class GenericRepository<T>(DbContext context) : IGenericRepository<T>
    where T : class
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAll(string includes = "", int? pageSize = null)
        {
            IQueryable<T>? query = _dbSet.AsNoTracking();

            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            if (!string.IsNullOrEmpty(includes))
            {
                string[]? entities = includes.Split(';');
                foreach (string? entity in entities)
                {
                    query = query.Include(entity);
                }
            }

            return (await query.AsNoTracking().ToListAsync()).ToImmutableList();
        }

        public async Task<T?> GetById(string key)
        {
            return await _dbSet.FindAsync(new object[]
            {
            key,
            });
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string includes = "")
        {
            var query = _dbSet.AsQueryable<T>();

            if (!string.IsNullOrEmpty(includes))
            {
                string[]? entities = includes.Split(';');
                foreach (string? entity in entities)
                {
                    query = query.Include(entity);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<IEnumerable<T>> Where(
            Expression<Func<T, bool>> predicate,
            string includes = "",
            PaginationFilter? pagination = null,
            OrderByClausure<T>? orderBy = null)
        {
            IQueryable<T>? query = _dbSet.Where(predicate);

            if (orderBy is { Predicate: not null })
            {
                if (orderBy.Order == OrderByDirection.Asc)
                {
                    query = query.OrderBy(orderBy.Predicate);
                }
                else
                {
                    query = query.OrderByDescending(orderBy.Predicate);
                }
            }

            if (pagination is { Limit: > 0, Offset: > 0 })
            {
                var previousOffset = pagination.Offset - 1;
                query = query
                    .Skip(previousOffset * pagination.Limit)
                    .Take(pagination.Limit);
            }

            if (string.IsNullOrEmpty(includes))
            {
                return query
                    .IgnoreAutoIncludes()
                    .AsNoTracking()
                    .ToImmutableList();
            }

            string[]? entities = includes.Split(';');
            foreach (string? entity in entities)
            {
                query = query.Include(entity);
            }

            return (await query.AsNoTracking().ToListAsync()).ToImmutableList();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<decimal> Sum(Expression<Func<T, decimal>> summaryPredicate, Expression<Func<T, bool>>? filterPredicate = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (filterPredicate is not null)
            {
                query = query.Where(filterPredicate).AsNoTracking();
            }

            return await query.SumAsync(summaryPredicate);
        }
    }
}
