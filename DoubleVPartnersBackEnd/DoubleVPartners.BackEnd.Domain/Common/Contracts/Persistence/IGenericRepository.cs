using DoubleVPartners.BackEnd.Domain.Common.Dtos;
using System.Linq.Expressions;

namespace DoubleVPartners.BackEnd.Domain.Common.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string includes = "", int? pageSize = null);
        Task<T?> GetById(string key);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string includes = "");
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate, string includes = "", PaginationFilter? pagination = null, OrderByClausure<T>? orderBy = null);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task<double> Sum(Expression<Func<T, double>> summaryPredicate, Expression<Func<T, bool>>? filterPredicate = null);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        void Add(T entity);
        void Delete(T entity);
    }
}
