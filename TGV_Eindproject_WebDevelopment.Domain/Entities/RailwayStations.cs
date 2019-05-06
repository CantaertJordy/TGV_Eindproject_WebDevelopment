using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class RailwayStations
    {
        public RailwayStations()
        {
            LinesDepartureNavigation = new HashSet<Lines>();
            LinesDestinationNavigation = new HashSet<Lines>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public ICollection<Lines> LinesDepartureNavigation { get; set; }
        public ICollection<Lines> LinesDestinationNavigation { get; set; }
    }
}
