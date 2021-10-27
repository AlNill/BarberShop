using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BarberShop.DAL.Common.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        public IEnumerable<TEntity> GetRange(int skipPos = 0, int count = 10);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity> Get(int id);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task Create(TEntity item);
        Task Update(TEntity item);
        Task Delete(int id);
        public Task<int> GetCount();
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
