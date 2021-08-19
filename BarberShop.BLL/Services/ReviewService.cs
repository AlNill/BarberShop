﻿using System.Collections.Generic;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;

namespace BarberShop.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repository;

        public ReviewService(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public Review GetById(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
