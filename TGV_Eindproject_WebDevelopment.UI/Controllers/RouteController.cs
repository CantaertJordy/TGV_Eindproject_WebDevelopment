using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using TGV_Eindproject_WebDevelopment.UI.ViewModels;

namespace TGV_Eindproject_WebDevelopment.UI.Controllers
{
    public class RouteController : Controller
    {
        private readonly TGVService tgvService;
        private readonly RailwayStationService railwayStationService;
        private readonly LineService lineService;

        public RouteController()
        {
            tgvService = new TGVService();
            railwayStationService = new RailwayStationService();
            lineService = new LineService();
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

            IList<IList<Tgvs>> calculatedRoutes = tgvService.GetJourneys((int)departureId, (int)destinationId, (DateTime)dateOfDeparture);
            IList<IList<RouteVM>> routes = new List<IList<RouteVM>>();

            foreach (IList<Tgvs> calculatedRoute in calculatedRoutes)
            {
                IList<RouteVM> route = new List<RouteVM>();

                foreach(Tgvs tgv in calculatedRoute)
                {
                    RouteVM r = new RouteVM()
                    {
                        StartStation = tgv.LineNavigation.DepartureNavigation.City,
                        EndStation = tgv.LineNavigation.DestinationNavigation.City,
                        Tgv = tgv
                    };

                    route.Add(r);
                }

                routes.Add(route);
            }

            return PartialView("_JourneyResultPartial", routes);
        }
    }
}