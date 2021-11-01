using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;
using Microsoft.AspNetCore.Http;

namespace BarberShop.BLL.Services
{
    public class BarberService : IBarberService
    {
        private readonly IGenericRepository<Barber> _repository;

        public BarberService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.BarberRepository();
        }

        public async Task<Barber> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<IEnumerable<Barber>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(Barber barber)
        {
            await _repository.CreateAsync(barber);
        }

        public async Task UpdateAsync(Barber barber)
        {
            await _repository.UpdateAsync(barber);
        }

        public async Task DeleteAsync(int id)
        {
            if (await _repository.ExistsAsync(id))
            {
                var barber = await _repository.GetAsync(id);
                var imagePath = Directory.GetCurrentDirectory() + "/wwwroot/" + barber.ImagePath;
                if(File.Exists(imagePath))
                    File.Delete(imagePath);
                await _repository.DeleteAsync(id);
            }
        }

        public async Task SaveAvatarAsync(Barber barber, IFormFile image)
        {
            var avatarName = barber.Surname + image.FileName;
            var filePath = Directory.GetCurrentDirectory() + "/wwwroot/images/" + avatarName;
            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);
            barber.ImagePath = $"/images/{avatarName}";
        }
    }
}