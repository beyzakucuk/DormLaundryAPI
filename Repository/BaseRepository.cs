using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly RepositoryContext repositoryContext;

        protected BaseRepository(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public async Task<List<T>> FindAllAsync() =>
            await repositoryContext.Set<T>().AsNoTracking().ToListAsync();
        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression) =>
           await repositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
        public  IQueryable<T> Find(Expression<Func<T, bool>> expression) => repositoryContext.Set<T>().Where(expression).AsNoTracking();

        public async Task CreateAsync(T entity)
        {
            await repositoryContext.Set<T>().AddAsync(entity);
            await repositoryContext.SaveChangesAsync(); 
        }
        public async Task DeleteAsync(T entity)
        {
           repositoryContext.Set<T>().Update(entity);
           await repositoryContext.SaveChangesAsync();
        }

    }
}
