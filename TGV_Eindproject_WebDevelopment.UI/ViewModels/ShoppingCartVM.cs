using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    public class ShoppingCartVM
    {
        public Users User;
        public IList<string> Names;
        public IList<Tgvs> Route;
        public DateTime DateOfDeparture;
    }
}
