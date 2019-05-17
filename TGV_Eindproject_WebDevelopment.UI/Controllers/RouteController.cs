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
        private readonly UserService userService;

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
                        Tgv = tgv
                    };

                    lastArrival = temp.Add(tgv.LineNavigation.Duration);

                    r.TimeOfArrival = lastArrival;

                    route.Add(r);
                }

                routes.Add(route);
            }

            return PartialView("_JourneyResultPartial", routes);
        }

        [Authorize]
        public IActionResult BuyTicket(int departureId, int destinationId, string dateOfDeparture)
        {
            DateTime date = Convert.ToDateTime(dateOfDeparture);

            OrderDetailVM orderDetails = new OrderDetailVM()
            {
                Route = tgvService.GetJourney(departureId, destinationId, date),
                DateOfDeparture = date,
            };

            foreach (Tgvs tgv in orderDetails.Route)
            {
                tgv.LineNavigation = null;
                tgv.Tickets = null;
            }

            //HttpContext.Session.SetObject("OrderDetails", orderDetails);

            if (userService.Get(User.FindFirst(ClaimTypes.NameIdentifier).Value) != null)
                return RedirectToAction("OrderDetails", orderDetails);
            else
                return RedirectToAction("SetCredentials", "Account");
        }

        public IActionResult OrderDetails(OrderDetailVM orderDetails)
        {
            //OrderDetailVM orderDetails = HttpContext.Session.GetObject<OrderDetailVM>("OrderDetails");

            return View(orderDetails);
        }
    }
}