using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService userService;

        public AccountController()
        {
            userService = new UserService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetCredentials()
        {
            return View();
        }

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

            return View(); ////////////////////////////////////////
        }
    }
}