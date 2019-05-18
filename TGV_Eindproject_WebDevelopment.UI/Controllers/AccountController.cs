using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using Microsoft.AspNetCore.Identity;
using TGV_Eindproject_WebDevelopment.UI.Services;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService userService;
        private readonly TicketService ticketService;

        public AccountController()
        {
            userService = new UserService();
            ticketService = new TicketService();
        }

        [Authorize]
        public IActionResult SetCredentials()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetCredentials([Bind("Name, FirstName, BirthDate, Country, City, Address")]Users entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.NetUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    userService.Create(entity);
                    //return RedirectToAction("");      ///////////////////////////////////////////////
                }
            }
            catch (DataException e)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Something went wrong, please try again.");
            }

            return RedirectToAction("PlaceOrder", "SchoppingCart");
        }

        [Authorize]
        public IActionResult History()
        {
            String netUserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Users u = userService.Get(netUserID);

            IEnumerable<Tickets> history = ticketService.AllFromUser(u.Id);

            foreach(Tickets t in history)
            {
                t.DateOfDeparture = t.DateOfDeparture.Add(t.Tgv.TimeOfDeparture);
            }

            return View(history);
        }

        [Authorize]
        public IActionResult CancelTicket(int id)
        {
            return View(ticketService.Get(id));
        }

        [Authorize]
        public async Task<ActionResult> CancelTicketConfirm(int id)
        {
            try
            {
                ticketService.Cancel(id);

                Tickets t = ticketService.Get(id);

                String netUserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Users u = userService.Get(netUserID);


                var body = "<h3>Hi " + u.FirstName + ", this email in a confirmation of the fact that you canceled a ticket on " + DateTime.Now.ToString("dd/MM/yyyy") + "</h3>";
                body += "<h4>You can find a summary of this ticket below</h4>";

                string type;
                if (t.IsBusiness == 1)
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
                            "<h5>Ticket for " + t.Name + "</h5>" +
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

                EmailSender mail = new EmailSender();
                await mail.SendEmailAsync(
                    User.Identity.Name,
                    "Ticket canceled on " + DateTime.Now.ToString("dd/MM/yyyy"),
                    body);

                return RedirectToAction("History");

            }
            catch(Exception e)
            {
                return RedirectToAction("ErrorWhileSendingMail");
            }
            

        }

        public IActionResult ErrorWhileSengingMail()
        {
            return View();
        }
    }

}