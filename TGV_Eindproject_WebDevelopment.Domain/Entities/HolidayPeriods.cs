using System;
using System.Collections.Generic;

namespace TGV_Eindproject_WebDevelopment.Domain.Entities
{
    public partial class HolidayPeriods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ExtraSeatsPercent { get; set; }
        public double ExtraPricePercent { get; set; }
    }
}
