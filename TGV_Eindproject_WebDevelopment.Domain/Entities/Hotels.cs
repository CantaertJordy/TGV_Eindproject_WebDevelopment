using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class Hotels
    {
        public Hotels()
        {
            HotelReservations = new HashSet<HotelReservations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public double Prijs { get; set; }

        public ICollection<HotelReservations> HotelReservations { get; set; }
    }
}
