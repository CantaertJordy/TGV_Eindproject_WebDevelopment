using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    public class RouteVM
    {
        public int DepartureId { get; set; }
        public string StartStation { get; set; }
        public int DestinationId { get; set; }
        public string EndStation { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public int TgvId { get; set; }
        public double PriceEconomic { get; set; }
        public double PriceBusiness { get; set; }
        public int AvailableSeatsEconomic { get; set; }
        public int AvailableSeatsBusiness { get; set; }
        public int Amount { get; set; }
        public bool Business { get; set; }
    }
}
