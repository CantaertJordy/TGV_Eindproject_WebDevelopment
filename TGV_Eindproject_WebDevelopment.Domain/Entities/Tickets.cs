using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class Tickets
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Tgvid { get; set; }
        public string Name { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public byte IsBusiness { get; set; }
        public string SeatNumber { get; set; }
        public byte IsCancelled { get; set; }

        public Tgvs Tgv { get; set; }
        public Users User { get; set; }
    }
}
