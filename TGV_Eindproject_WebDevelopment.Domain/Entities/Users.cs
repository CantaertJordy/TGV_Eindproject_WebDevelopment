using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class Users
    {
        public Users()
        {
            HotelReservations = new HashSet<HotelReservations>();
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string NetUserId { get; set; }

        public ICollection<HotelReservations> HotelReservations { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
