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
        public Barber Barber { get; set; }
        [Required]
        public DateTime RecordTime { get; set; }
    }
}
