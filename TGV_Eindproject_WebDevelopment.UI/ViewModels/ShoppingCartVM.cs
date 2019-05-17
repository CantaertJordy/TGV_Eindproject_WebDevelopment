using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGV_Eindproject_WebDevelopment.UI.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCartVM()
        {
            Content = new List<RouteVM>();
        }

        public IList<RouteVM> Content { get; set; }
        public string UserName { get; set; }
    }
}
