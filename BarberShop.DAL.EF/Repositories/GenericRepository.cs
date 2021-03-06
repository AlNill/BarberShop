using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BarberShop.DAL.Common.Repositories;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: class
    {
        private readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(int id) => await DbSet.FindAsync(id) != null;

        public async Task<int> GetCountAsync() => await DbSet.CountAsync();

        public IEnumerable<TEntity> GetRange(int skipPos=0, int count=10) => 
            DbSet.AsNoTracking().Skip(skipPos).Take(count);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

        public async Task<TEntity> GetAsync(int id) => await DbSet.FindAsync(id);

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate) => 
            DbSet.AsNoTracking().AsEnumerable().Where(predicate).ToList();

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task CreateAsync(TEntity item)
        {
            await DbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public void Create(TEntity item)
        {
            DbSet.Add(item);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await DbSet.FindAsync(id);
            DbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
