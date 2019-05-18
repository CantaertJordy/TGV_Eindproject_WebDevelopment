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
        public IActionResult Index(ShoppingCartVM shoppingCartVM)
        {
            HttpContext.Session.SetObject("ShoppingCart", shoppingCartVM);

            return RedirectToAction("ToUsers");
        }

        [Authorize]
        public IActionResult ToUsers()
        {
            ShoppingCartVM shoppingCartVM = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

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
            int total = 0;

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
                    total++;
                }
            }

            HttpContext.Session.SetObject("ShoppingCart", null);

            return RedirectToAction("PlaceOrder", new { @amount = total });
        }

        [Authorize]
        public async Task<ActionResult> PlaceOrder(int amount) 
        {
            IList<Tickets> tickets = ticketService.All().Reverse().Take(amount).Reverse().ToList();

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
                            "<div class='col-md-4'>" +
                                "<h5>" + t.Name + "</h5>" +
                            "</div>" +
                        "</div>" +
                        "<div class='row'>" +
                            "<div class='col-md-4'>" +
                                "<p>Time of departure: " + t.DateOfDeparture.Add(t.Tgv.TimeOfDeparture) + "<br/>" +
                                "Time of arrival: " + t.DateOfDeparture.Add(t.Tgv.TimeOfDeparture).Add(t.Tgv.LineNavigation.Duration) + "</p>" +
                            "</div>" +
                        "</div>" +
                        
                        "<div class='row'>" +
                            "<div class='col-md-4'>" +
                                "<p>" + t.Tgv.LineNavigation.DepartureNavigation.City + " &rarr; " + t.Tgv.LineNavigation.DestinationNavigation.City + "</p>" +
                            "</div>" +
                        "</div>" +
                        
                        "<div class='row'>" +
                            "<div class='col-md-4'>" +
                                "<p>Type: " + type + "<br/>" +
                                "Seat: " + t.SeatNumber + "<br/>" +
                                "Price: " + t.Price + "</p>" +
                            "</div>" +
                        "</div>";
                }

                EmailSender mail = new EmailSender();
                await mail.SendEmailAsync(
                    User.Identity.Name,
                    "Order placed on " + DateTime.Now.ToString("dd/MM/yyyy"), 
                    body);

                return View(tickets);
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("ErrorWhileSendingMail");
            }
        }

        [Route("/CustomErrorPages/ErrorWhileSendingMail")]
        public IActionResult ErrorWhileSengingMail()
        {
            return View();
        }
    }
}