using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    public class RouteVM
    {
        
        public int DepartureId { get; set; }
        [DisplayName("Departure")]
        public string StartStation { get; set; }
        public int DestinationId { get; set; }
        [DisplayName("Destination")]
        public string EndStation { get; set; }
        [DisplayName("Time of Departure")]
        public DateTime TimeOfDeparture { get; set; }
        [DisplayName("Time of Arrival")]
        public DateTime TimeOfArrival { get; set; }
        public int TgvId { get; set; }
        [DisplayName("Price economic")]
        public double PriceEconomic { get; set; }
        [DisplayName("Price business")]
        public double PriceBusiness { get; set; }
        [DisplayName("Seats economic")]
        public int AvailableSeatsEconomic { get; set; }
        [DisplayName("Seats business")]
        public int AvailableSeatsBusiness { get; set; }
        public int Amount { get; set; }
        public bool Business { get; set; }
    }
}
