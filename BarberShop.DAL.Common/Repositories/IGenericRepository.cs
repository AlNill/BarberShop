using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BarberShop.DAL.Common.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        public IEnumerable<TEntity> GetRange(int skipPos = 0, int count = 10);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetAsync(int id);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        public Task<bool> ExistsAsync(int id);
        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task CreateAsync(TEntity item);
        void Create(TEntity item);
        Task UpdateAsync(TEntity item);
        Task DeleteAsync(int id);
        public Task<int> GetCountAsync();
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
