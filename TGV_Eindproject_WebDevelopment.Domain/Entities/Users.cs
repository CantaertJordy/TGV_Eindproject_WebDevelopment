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
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your firstname")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Please enter your birthdate")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter your country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }
        public string NetUserId { get; set; }

        public ICollection<HotelReservations> HotelReservations { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
