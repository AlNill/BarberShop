using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberShop.BLL.Interfaces;
using BarberShop.DAL.Common;
using BarberShop.DAL.Common.Models;
using BarberShop.DAL.Common.Repositories;

namespace BarberShop.BLL.Services
{
    public class BusyRecordService: IBusyRecordService
    {
        private readonly IGenericRepository<BusyRecord> _recordRepository;
        private readonly IGenericRepository<Offer> _offerRepository;
        public BusyRecordService(IUnitOfWork unitOfWork)
        {
            _recordRepository = unitOfWork.BusyRecordRepository();
            _offerRepository = unitOfWork.OfferRepository();
        }

        public async Task<BusyRecord> GetAsync(int id)
        {
            return await _recordRepository.GetAsync(id);
        }

        public async Task<IEnumerable<BusyRecord>> GetAllAsync()
        {
            return await _recordRepository.GetAllAsync();
        }

        public async Task CreateAsync(int barberId, int offerId, DateTime date)
        {
            if (await IsExists(barberId, offerId, date) != null)
                throw new ArgumentException("Sorry! This date is booked.");

            await _recordRepository.CreateAsync(
                new BusyRecord()
                {
                    BarberId = barberId,
                    RecordTime = date,
                    ServiceId = offerId
                }
            );
        }

        public async Task UpdateAsync(BusyRecord record)
        {
            await _recordRepository.UpdateAsync(record);
        }

        private async Task<BusyRecord> IsExists(int barberId, int offerId, DateTime date)
        {
            //TODO: change Result to Task
            var offer = await _offerRepository.GetAsync(offerId);
            var startDate = date.AddMinutes(-offer.Duration);
            var endDate = date.AddMinutes(offer.Duration);
            return _recordRepository.Get(b => b.BarberId == barberId && 
                                        (b.RecordTime > startDate && b.RecordTime <= endDate)
                                        ).FirstOrDefault();
        }
    }
}
