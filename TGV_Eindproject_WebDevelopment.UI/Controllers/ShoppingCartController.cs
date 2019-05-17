using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using TGV_Eindproject_WebDevelopment.UI.Extensions;
using TGV_Eindproject_WebDevelopment.UI.ViewModels;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private UserService userService;

        public ShoppingCartController()
        {
            userService = new UserService();
        }

        [Authorize]
        public IActionResult Index()
        {
            ShoppingCartVM shoppingCart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            if (shoppingCart != null)
            {
                Users user = userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                shoppingCart.UserName = user.Name + user.FirstName;
            }

            return View(shoppingCart);
        }
    }
}