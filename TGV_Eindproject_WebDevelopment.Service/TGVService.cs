using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class TGVService
    {

        #region Repositories

        private readonly TGVDAO tgvDAO;
        private readonly TicketDAO ticketDAO;

        #endregion

        #region Services

        private readonly LineService lineService;
        private readonly HolidayPeriodService holidayPeriodService;

        #endregion

        #region Constructors

        public TGVService()
        {
            tgvDAO = new TGVDAO();
            lineService = new LineService();
            ticketDAO = new TicketDAO();
            holidayPeriodService = new HolidayPeriodService();
        }

        #endregion

        #region Get-Methods

        public Tgvs Get(int id)
        {
            return tgvDAO.Get(id);
        }

        public IList<IList<Tgvs>> GetJourneys(int departureId, int destinationId, DateTime dateOfDeparture)
        {
            Lines firstLine = lineService.GetRoute(departureId, destinationId).First();

            IEnumerable<Tgvs> tgvs = GetWithLine(firstLine.Id);
            IList<IList<Tgvs>> journeys = new List<IList<Tgvs>>();

            foreach (Tgvs tgv in tgvs)
            {
                IList<Tgvs> route = GetJourney(departureId, destinationId, dateOfDeparture);

                journeys.Add(route);

                dateOfDeparture = dateOfDeparture.Date.Add(route[0].TimeOfDeparture).AddSeconds(1);
            }

            return journeys;
        }

        public IList<Tgvs> GetJourney(int departureId, int destinationId, DateTime dateOfDeparture)
        {
            IList<Lines> route = lineService.GetRoute(departureId, destinationId);

            IList<Tgvs> journey = new List<Tgvs>();

            TimeSpan fullDay = new TimeSpan(24, 0, 0);

            foreach (Lines l in route)
            {
                Tgvs tgv = GetJourney(l, dateOfDeparture);
                tgv.LineNavigation = l;
                journey.Add(tgv);

                dateOfDeparture = dateOfDeparture.Date.Add(tgv.TimeOfDeparture.Add(tgv.LineNavigation.Duration));
            }

            return journey;
        }

        public Tgvs GetJourney(Lines line, DateTime dateOfDeparture)
        {
            IEnumerable<Tgvs> tgvs = GetWithLine(line.Id);
            Tgvs bestTgv = BestTgv(tgvs, dateOfDeparture.TimeOfDay);

            if (bestTgv == null)
                return EarliestTgv(tgvs);

            IEnumerable<HolidayPeriods> holidayPeriods = holidayPeriodService.All();

            foreach (HolidayPeriods h in holidayPeriods)
            {
                if (h.StartDate.CompareTo(dateOfDeparture) <= 0 && h.EndDate.CompareTo(dateOfDeparture) >= 0)
                {
                    bestTgv.AvailableBusinessSeats = (int) (bestTgv.AvailableBusinessSeats * (1 + h.ExtraSeatsPercent));
                    bestTgv.AvailableEconomicSeats = (int)(bestTgv.AvailableEconomicSeats * (1 + h.ExtraSeatsPercent));
                    bestTgv.BasePriceBusiness *= 1 + h.ExtraPricePercent;
                    bestTgv.BasePriceEconomic *= 1 + h.ExtraPricePercent;
                }
            }

            IEnumerable<Tickets> tickets = ticketDAO.AllForTGV(bestTgv.Id, dateOfDeparture);

            foreach (Tickets t in tickets)
            {
                if (t.IsCancelled == 0)
                {
                    if (t.IsBusiness == 0)
                        bestTgv.AvailableBusinessSeats--;
                    else
                        bestTgv.AvailableEconomicSeats--;
                }
            }

            return bestTgv;
        }

        public IEnumerable<Tgvs> GetWithLine(int lineId)
        {
            return tgvDAO.GetWithLine(lineId);
        }

        public int GetAmountOfSeats(int id, byte IsBusiness)
        {
            if (IsBusiness == 0)
                return Get(id).AvailableEconomicSeats;
            return Get(id).AvailableBusinessSeats;
        }

        #endregion

        #region Helper-Methods

        private Tgvs EarliestTgv(IEnumerable<Tgvs> tgvs)
        {
            TimeSpan earliestTime = new TimeSpan(24, 0, 0);
            Tgvs earliestTgv = null;

            foreach (Tgvs t in tgvs)
            {
                if (earliestTime.CompareTo(t.TimeOfDeparture) > 0)
                {
                    earliestTgv = t;
                    earliestTime = t.TimeOfDeparture;
                }
            }

            return earliestTgv;
        }

        private Tgvs BestTgv(IEnumerable<Tgvs> tgvs, TimeSpan time)
        {
            Tgvs bestTgv = null;

            foreach (Tgvs tgv in tgvs)
            {
                if ((bestTgv == null && tgv.TimeOfDeparture.CompareTo(time) >= 0) || (bestTgv != null && tgv.TimeOfDeparture.CompareTo(bestTgv.TimeOfDeparture) < 0))
                    bestTgv = tgv;
            }

            return bestTgv;
        }

        #endregion

    }
}
