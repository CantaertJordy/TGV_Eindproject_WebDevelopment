using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Service;
using TGV_Eindproject_WebDevelopment.UI.Extensions;
using TGV_Eindproject_WebDevelopment.UI.ViewModels;

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
                return PartialView("_FaultyInputPartial");

            IList<IList<Tgvs>> calculatedRoutes = tgvService.GetJourneys((int)departureId, (int)destinationId, (DateTime)dateOfDeparture);
            IList<IList<RouteVM>> routes = new List<IList<RouteVM>>();

            foreach (IList<Tgvs> calculatedRoute in calculatedRoutes)
            {
                IList<RouteVM> route = new List<RouteVM>();

                DateTime departureDate = (DateTime)dateOfDeparture;
                DateTime lastArrival = departureDate;
                DateTime temp;

                foreach(Tgvs tgv in calculatedRoute)
                {
                    temp = departureDate.Add(tgv.TimeOfDeparture);

                    while (temp.CompareTo(lastArrival) <= 0)
                        temp = temp.AddDays(1);

                    RouteVM r = new RouteVM()
                    {
                        StartStation = tgv.LineNavigation.DepartureNavigation.City,
                        EndStation = tgv.LineNavigation.DestinationNavigation.City,
                        TimeOfDeparture = temp,
                        AvailableSeatsBusiness = tgv.AvailableBusinessSeats,
                        AvailableSeatsEconomic = tgv.AvailableEconomicSeats,
                        PriceBusiness = tgv.BasePriceBusiness,
                        PriceEconomic = tgv.BasePriceEconomic,
                        TgvId = tgv.Id,
                        DepartureId = tgv.LineNavigation.Departure,
                        DestinationId = tgv.LineNavigation.Destination
                    };

                    lastArrival = temp.Add(tgv.LineNavigation.Duration);

                    r.TimeOfArrival = lastArrival;

                    route.Add(r);
                }

                routes.Add(route);
            }

            return PartialView("_JourneyResultPartial", routes);
        }
        
        public IActionResult BuyTicket(int departureId, int destinationId, string dateOfDeparture)
        {
            DateTime date = Convert.ToDateTime(dateOfDeparture);
            
            IList<Tgvs> journey = tgvService.GetJourney(departureId, destinationId, date);
            ShoppingCartVM shoppingCart = new ShoppingCartVM();

            foreach (Tgvs tgv in journey)
            {
                if (tgv.TimeOfDeparture.CompareTo(date.TimeOfDay) < 0)
                    date = date.AddDays(1);

                RouteVM route = new RouteVM()
                {
                    StartStation = tgv.LineNavigation.DepartureNavigation.City,
                    EndStation = tgv.LineNavigation.DestinationNavigation.City,
                    TimeOfDeparture = date.Date.Add(tgv.TimeOfDeparture),
                    TgvId = tgv.Id,
                    PriceEconomic = tgv.BasePriceEconomic,
                    PriceBusiness = tgv.BasePriceBusiness,
                    AvailableSeatsBusiness = tgv.AvailableBusinessSeats,
                    AvailableSeatsEconomic = tgv.AvailableEconomicSeats,
                    DepartureId = tgv.LineNavigation.Departure,
                    DestinationId = tgv.LineNavigation.Destination,
                    Amount = 1
                };

                tgv.Tickets = null;
                tgv.LineNavigation.Tgvs = null;
                tgv.LineNavigation.DepartureNavigation = null;
                tgv.LineNavigation.DestinationNavigation = null;

                route.TimeOfArrival = route.TimeOfDeparture.Add(tgv.LineNavigation.Duration);

                date = route.TimeOfArrival;

                shoppingCart.Content.Add(route);
            }

            HttpContext.Session.SetObject("ShoppingCart", shoppingCart);

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}