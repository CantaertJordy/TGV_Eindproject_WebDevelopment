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
            Content = new List<CartItemVM>();
        }

        public IList<CartItemVM> Content { get; set; }
    }

    public class CartItemVM
    {
        public CartItemVM()
        {
            Route = new List<RouteVM>();
        }

        public IList<RouteVM> Route { get; set; }
    }
}
