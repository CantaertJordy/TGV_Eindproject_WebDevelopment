using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class LineService
    {

        #region Repositories

        private readonly LineDAO lineDAO;

        #endregion

        #region Services

        private readonly RailwayStationService railwayStationService;

        #endregion

        #region Constructors

        public LineService()
        {
            lineDAO = new LineDAO();

            railwayStationService = new RailwayStationService();
        }

        #endregion

        #region Get-Methods

        public IList<Lines> GetRoute(int departureId, int destinationId)
        {
            if (departureId == destinationId)
            {
                throw new ArgumentException("The departure and destination cannot be the same.");
            }

            int brussels = 1;
            int london = 2;
            int paris = 3;
            int amsterdam = 4;
            int berlin = 5;
            int rome = 6;
            int moscow = 7;

            IList<Lines> route = new List<Lines>();

            if (departureId == london && destinationId != brussels)
            {
                route.Add(lineDAO.Get(departureId, brussels));
                departureId = brussels;
            }
            else if (departureId == moscow && destinationId != berlin)
            {
                route.Add(lineDAO.Get(departureId, berlin));
                departureId = brussels;
            }

            if (destinationId == moscow && departureId != berlin)
            {
                route.Add(lineDAO.Get(departureId, berlin));
                route.Add(lineDAO.Get(berlin, destinationId));
            }
            else if ((departureId == amsterdam && destinationId == rome) || (departureId == amsterdam && destinationId == paris)
                || (departureId == rome && destinationId == amsterdam) || (departureId == paris && destinationId == amsterdam)
                || (destinationId == london && departureId != brussels))
            {
                route.Add(lineDAO.Get(departureId, brussels));
                route.Add(lineDAO.Get(brussels, destinationId));
            }
            else
            {
                route.Add(lineDAO.Get(departureId, destinationId));
            }

            return route;
        }

        public IEnumerable<Lines> GetLinesWithDeparture(int departureId)
        {
            return lineDAO.GetWithDeparture(departureId);
        }

        public IEnumerable<Lines> GetLinesWithDestination(int destinationId)
        {
            return lineDAO.GetWithDestination(destinationId);
        }

        #endregion

    }
}
