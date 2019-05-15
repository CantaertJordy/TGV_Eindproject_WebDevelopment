using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    public class RouteVM
    {
        public string StartStation { get; set; }
        public string EndStation { get; set; }
        public DateTime TimeOfDeparture { get; set; }
        public DateTime TimeOfArrival { get; set; }
        public Tgvs Tgv { get; set; }
    }
}
