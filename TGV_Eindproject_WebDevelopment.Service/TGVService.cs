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

        #endregion

        #region Services

        private readonly LineService lineService;

        #endregion

        #region Constructors

        public TGVService()
        {
            tgvDAO = new TGVDAO();
            lineService = new LineService();
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
                IList<Tgvs> route = GetJourney(departureId, destinationId, dateOfDeparture.TimeOfDay);

                journeys.Add(route);
            }

            return journeys;
        }

        public IList<Tgvs> GetJourney(int departureId, int destinationId, TimeSpan timeOfDeparture)
        {
            IList<Lines> route = lineService.GetRoute(departureId, destinationId);

            IList<Tgvs> journey = new List<Tgvs>();

            TimeSpan fullDay = new TimeSpan(24, 0, 0);

            foreach (Lines l in route)
            {
                Tgvs tgv = GetJourney(l, timeOfDeparture);
                journey.Add(tgv);

                timeOfDeparture.Add(tgv.TimeOfDeparture);
                if (timeOfDeparture.CompareTo(fullDay) >= 0)
                    timeOfDeparture.Subtract(fullDay);
            }

            return journey;
        }

        public Tgvs GetJourney(Lines line, TimeSpan timeOfDeparture)
        {
            IEnumerable<Tgvs> tgvs = GetWithLine(line.Id);
            Tgvs bestTgv = BestTgv(tgvs, timeOfDeparture);

            if (bestTgv == null)
                return EarliestTgv(tgvs);
            else
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
                    earliestTgv = t;
            }

            return earliestTgv;
        }

        private Tgvs BestTgv(IEnumerable<Tgvs> tgvs, TimeSpan time)
        {
            Tgvs bestTgv = null;

            foreach (Tgvs tgv in tgvs)
            {
                if ((bestTgv == null && tgv.TimeOfDeparture.CompareTo(time) >= 0) || (bestTgv != null && tgv.TimeOfDeparture.CompareTo(bestTgv.TimeOfDeparture) > 0))
                    bestTgv = tgv;
            }

            return bestTgv;
        }

        #endregion

    }
}
