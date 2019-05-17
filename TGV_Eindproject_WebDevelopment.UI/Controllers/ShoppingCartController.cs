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
        [HttpGet]
        public IActionResult Index()
        {
            ShoppingCartVM shoppingCart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {
            HttpContext.Session.SetObject("ShoppingCart", shoppingCartVM);

            if (userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value) == null)
                return RedirectToAction("SetCredentials", "Account");

            return RedirectToAction("Users", shoppingCartVM.Content[0].Amount);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Users(int amount)
        {
            string[] userNames = new string[amount];

            Users user = userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userNames[0] = user.Name + user.FirstName;

            return View(userNames);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Confirm(string[] users)
        {
            throw new NotImplementedException();
        }
    }
}