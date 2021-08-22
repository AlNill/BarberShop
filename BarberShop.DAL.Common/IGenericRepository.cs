﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BarberShop.DAL.Common
{
    public interface IGenericRepository<TEntity> where TEntity: class 
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}