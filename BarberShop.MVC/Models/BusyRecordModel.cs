using System;
using BarberShop.BLL.Models;

namespace BarberShop.MVC.Models
{
    public class BusyRecordModel
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
