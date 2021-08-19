﻿using System;
using System.Collections.Generic;
using System.Linq;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.DAL.EF.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly ApplicationContext _context;

        public ReviewRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            // Use the Eager loading
            return _context.Reviews.Include(r => r.Barber).ToList();
        }

        public Review Get(int id)
        {
            return _context.Reviews.Find(id);
        }

        public Review IsExists(Func<Review, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Review item)
        {
            _context.Reviews.Add(item);
            _context.SaveChanges();
        }

        public void Update(Review item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
                _context.Reviews.Remove(review);
        }
    }
}
