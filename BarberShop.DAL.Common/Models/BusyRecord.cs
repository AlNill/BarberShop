using System;

namespace BarberShop.DAL.Common.Models
{
    public class BusyRecord
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public DateTime RecordTime { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
