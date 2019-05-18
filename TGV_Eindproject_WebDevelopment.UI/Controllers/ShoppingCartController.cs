using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using TGV_Eindproject_WebDevelopment.UI.Extensions;
using TGV_Eindproject_WebDevelopment.UI.Services;
using TGV_Eindproject_WebDevelopment.UI.ViewModels;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private UserService userService;
        private TicketService ticketService;

        public ShoppingCartController()
        {
            userService = new UserService();
            ticketService = new TicketService();
        }

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

            return RedirectToAction("Users", new { @amount = shoppingCartVM.Content[0].Amount });
        }

        [Authorize]
        public IActionResult Users(int amount)
        {
            IList<string> userNames = new List<string>();
            Users user = userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            userNames.Add(user.Name + " " + user.FirstName);

            for (int i = 1; i < amount; i++)
                userNames.Add("");

            return View("Users", userNames);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Confirm(IList<string> users)
        {
            foreach (string s in users)
            {
                if (s == null)
                    return RedirectToAction("Users", new { @amount = users.Count });
            }

            ShoppingCartVM shoppingCart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            Users user = userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            foreach (RouteVM route in shoppingCart.Content)
            {
                foreach (string u in users)
                {
                    Tickets ticket = new Tickets()
                    {
                        UserId = user.Id,
                        Tgvid = route.TgvId,
                        Name = u,
                        DateOfDeparture = route.TimeOfDeparture,
                        IsBusiness = route.Business ? (byte)1 : (byte)0,
                        Price = route.Business ? route.PriceBusiness : route.PriceEconomic
                    };

                    ticketService.Create(ticket);
                }
            }

            IList<Tickets> tickets = new List<Tickets>(ticketService.AllFromUser(user.Id));

            for (int i = 0; i < tickets.Count - shoppingCart.Content[0].Amount; i++)
                tickets.RemoveAt(i);

            return RedirectToAction("PlaceOrder", new { @tickets = tickets });
        }

        [Authorize]
        public async Task<ActionResult> PlaceOrder(IList<Tickets> tickets) 
        {
            try
            {
                var body = "<h3>Thank you for your order placed on " + DateTime.Now.ToString("dd/MM/yyyy") + "</h3>";
                body += "<h4>You can find a summary of your order below</h4>";

                foreach(Tickets t in tickets)
                {
                    string type;
                    if(t.IsBusiness == 1)
                    {
                        type = "Business seat";
                    }
                    else
                    {
                        type = "Economic seat";
                    }

                    body += "<hr/>";
                    body += 
                        "<div class='row'>" +
                            "<div class='col-md-4>" +
                                "<p>Time of departure: " + t.DateOfDeparture + "<br/>" +
                                "Time of arrival: " + t.DateOfDeparture + t.Tgv.LineNavigation.Duration + "</p>" +
                            "</div>" +
                        "</div>" +
                        
                        "<div class='row'>" +
                            "<div class='col-md-4'>" +
                                "<p>" + t.Tgv.LineNavigation.Departure + " &rarr; " + t.Tgv.LineNavigation.Destination + "</p>" +
                            "</div>" +
                        "</div>" +
                        
                        "<div class='row'>" +
                            "<div class='col-md-4'>" +
                                "<p>Type:" + type + "<br/>" +
                                "Price: " + t.Price + "</p>" +
                            "</div>" +
                        "</div>";
                }

                EmailSender mail = new EmailSender();
                await mail.SendEmailAsync(
                    User.Identity.Name,
                    "Order" + DateTime.Now.ToString("dd/MM/yyyy"), 
                    body);

                return View(tickets);
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorWhileSendingMail");
            }


            return View();  ///////////
        }

        [Route("/CustomErrorPages/ErrorWhileSendingMail")]
        public IActionResult ErrorWhileSengingMail()
        {
            return View();
        }
    }
}