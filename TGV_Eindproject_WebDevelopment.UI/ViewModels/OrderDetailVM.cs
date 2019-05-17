using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    [Serializable]
    public class OrderDetailVM
    {
        public IList<Tgvs> Route { get; set; }
        public DateTime DateOfDeparture { get; set; }
    }
}
