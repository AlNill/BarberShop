﻿using System;
using System.Collections.Generic;

namespace BarberShop.DAL.Common
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find();
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}