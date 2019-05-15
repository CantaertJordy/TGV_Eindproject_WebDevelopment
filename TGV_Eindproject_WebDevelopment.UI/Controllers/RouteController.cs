using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TGV_Eindproject_WebDevelopment.Service;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class RouteController : Controller
    {
        private readonly TGVService tgvService;
        private readonly RailwayStationService railwayStationService;

        public RouteController()
        {
            tgvService = new TGVService();
            railwayStationService = new RailwayStationService();
        }

        public IActionResult Index()
        {
            ViewBag.stations = new SelectList(railwayStationService.All(), "Id", "City");
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? departureId, int? destinationId, DateTime? dateOfDeparture)
        {
            if (departureId == null || destinationId == null || dateOfDeparture == null || destinationId == departureId)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_FaultyInputPartial");
            }

            return View();
        }
    }
}