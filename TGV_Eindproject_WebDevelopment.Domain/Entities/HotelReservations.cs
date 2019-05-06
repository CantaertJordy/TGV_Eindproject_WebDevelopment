using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class HotelReservations
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }

        public Hotels Hotel { get; set; }
        public Users User { get; set; }
    }
}
