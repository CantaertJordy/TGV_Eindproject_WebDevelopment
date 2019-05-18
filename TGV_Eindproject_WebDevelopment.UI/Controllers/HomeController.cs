using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using TGV_Eindproject_WebDevelopment.UI.Models;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;
        private HotelsService hotelsService;

        public HomeController()
        {
            userService = new UserService();
            hotelsService = new HotelsService();
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Welcome";

            DateTime time = DateTime.Now;
            string welcome = "Welcome";
            String name = "";

            if (time.Hour >= 5 && time.Hour <= 12)
            {
                welcome = "Good morning";
            }
            else if (time.Hour >= 13 && time.Hour <= 17)
            {
                welcome = "Good afternoon";
            }
            else if (time.Hour >= 18 && time.Hour <= 20)
            {
                welcome = "Good evening";
            }
            else if (time.Hour >= 21 && time.Hour <= 24)
            {
                welcome = "Good night";
            }

            name = getName();
            if(name == null)
            {
                ViewData["Message"] = welcome + "!";
            }
            else
            {
                ViewData["Message"] = welcome + " " + name + "!";
            }

            return View();
        }


        public string getName()
        {
            try
            {
                String netUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (netUserId == null)
                {
                    return null;
                }


                Users u = userService.Get(netUserId);
                if (u == null)
                    return null;
                else
                    return u.FirstName;

            }
            catch (Exception ex)
            {
                return null;
            }         
        }

        

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Hotels(string destination)
        {
            return View(hotelsService.Get(destination));
        }
    }
}
