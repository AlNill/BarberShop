using System.Collections.Generic;
using System.Linq;
using BarberShop.BLL.Models;
using BarberShop.DAL.Common;
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
            return _context.Reviews.ToList();
        }

        public Review Get(int id)
        {
            return _context.Reviews.Find(id);
        }

        public IEnumerable<Review> Find()
        {
            throw new System.NotImplementedException();
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
