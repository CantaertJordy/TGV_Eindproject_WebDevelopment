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

            RailwayStations london = railwayStationService.Get("Londen");
            RailwayStations amsterdam = railwayStationService.Get("Amsterdam");
            RailwayStations rome = railwayStationService.Get("Rome");
            RailwayStations paris = railwayStationService.Get("Parijs");
            RailwayStations moscow = railwayStationService.Get("Moskou");
            RailwayStations brussels = railwayStationService.Get("Brussel");
            RailwayStations berlin = railwayStationService.Get("Berlijn");

            IList<Lines> route = new List<Lines>();

            if (departureId == london.Id && destinationId != brussels.Id)
            {
                route.Add(lineDAO.Get(departureId, brussels.Id));
                departureId = brussels.Id;
            }
            else if (departureId == moscow.Id && destinationId != berlin.Id)
            {
                route.Add(lineDAO.Get(departureId, berlin.Id));
                departureId = brussels.Id;
            }

            if (destinationId == moscow.Id)
            {
                route.Add(lineDAO.Get(departureId, berlin.Id));
                route.Add(lineDAO.Get(berlin.Id, destinationId));
            }
            else if ((departureId == amsterdam.Id && destinationId == rome.Id) || (departureId == amsterdam.Id && destinationId == paris.Id)
                || (departureId == rome.Id && destinationId == amsterdam.Id) || (departureId == paris.Id && destinationId == amsterdam.Id)
                || destinationId == london.Id)
            {
                route.Add(lineDAO.Get(departureId, brussels.Id));
                route.Add(lineDAO.Get(brussels.Id, destinationId));
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
