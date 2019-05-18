using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class Tickets
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Tgvid { get; set; }
        public string Name { get; set; }
        [Display(Name="Date of departure")]
        public DateTime DateOfDeparture { get; set; }
        [Display(Name ="Type")]
        public byte IsBusiness { get; set; }
        [Display(Name = "Seat number")]
        public string SeatNumber { get; set; }
        [Display(Name = "Is cancelled")]
        public byte IsCancelled { get; set; }
        public double Price { get; set; }

        public Tgvs Tgv { get; set; }
        public Users User { get; set; }
    }
}
