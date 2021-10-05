using System;
using System.ComponentModel.DataAnnotations;
using BarberShop.DAL.Common.Models;

namespace BarberShop.MVC.Models
{
    public class BusyRecordModel
    {
        public int Id { get; set; }
        [Required]
        public int BarberId { get; set; }
        public BarberModel Barber { get; set; }
        [Required]
        public DateTime RecordTime { get; set; }
        public int ServiceId { get; set; }
        public ServiceModel Service {get; set; }
    }
}
