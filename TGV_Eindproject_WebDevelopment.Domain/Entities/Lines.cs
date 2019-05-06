using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class Lines
    {
        public Lines()
        {
            Tgvs = new HashSet<Tgvs>();
        }

        public int Id { get; set; }
        public int Departure { get; set; }
        public int Destination { get; set; }

        public RailwayStations DepartureNavigation { get; set; }
        public RailwayStations DestinationNavigation { get; set; }
        public ICollection<Tgvs> Tgvs { get; set; }
    }
}
