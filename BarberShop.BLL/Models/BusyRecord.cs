using System;

namespace BarberShop.BLL.Models
{
    public class BusyRecord
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
