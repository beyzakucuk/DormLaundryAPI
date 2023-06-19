using System.Linq.Expressions;

namespace Contracts
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> FindAllAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
