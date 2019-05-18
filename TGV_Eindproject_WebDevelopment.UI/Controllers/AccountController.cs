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

            return View(history);
        }

        [Authorize]
        [HttpPost]

        public IActionResult History(int t)
        {

            return View();
        }

        [Authorize]
        [HttpPost]     
        public IActionResult CancelTicket(int Id)
        {
            Tickets ticket = ticketService.Get(Id);
            return View(ticket);
        }

    }

}