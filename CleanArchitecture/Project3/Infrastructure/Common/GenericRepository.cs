using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Abstraction;

namespace Infrastructure.Common
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        protected AppDbContext _context;
        internal DbSet<T> _dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public virtual async Task<T?> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }
        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
             _dbSet.Update(entity);
            return true;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        //public virtual async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        //{
        //    return await _dbSet.Skip((page - 1) * pageSize)
        //                     .Take(pageSize)
        //                     .ToListAsync();
        //}

        //public Task<IEnumerable<T>> GetByPage(int page, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
