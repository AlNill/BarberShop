using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BarberShop.DAL.Common
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        public IEnumerable<TEntity> GetRange(int skipPos = 0, int count = 10);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        public int GetCount();
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
