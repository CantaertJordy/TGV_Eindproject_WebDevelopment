using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    [Serializable]
    public partial class Tgvs
    {
        public Tgvs()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int Id { get; set; }
        public TimeSpan TimeOfDeparture { get; set; }
        public int AvailableBusinessSeats { get; set; }
        public int AvailableEconomicSeats { get; set; }
        public double BasePriceBusiness { get; set; }
        public double BasePriceEconomic { get; set; }
        public int Line { get; set; }

        public Lines LineNavigation { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
