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
            RailwayStations london = railwayStationService.Get("Londen");
            RailwayStations amsterdam = railwayStationService.Get("Amsterdam");
            RailwayStations rome = railwayStationService.Get("Rome");
            RailwayStations paris = railwayStationService.Get("Parijs");
            RailwayStations moscow = railwayStationService.Get("Moskou");

            IList<Lines> route = new List<Lines>();

            if (departureId == london.Id || destinationId == london.Id)
            {

            }

            return route;
        }

        public IEnumerable<Lines> GetLinesWithDeparture(int departureId)
        {
            return lineDAO.LinesWithDeparture(departureId);
        }

        public IEnumerable<Lines> GetLinesWithDestination(int destinationId)
        {
            return lineDAO.LinesWithDestination(destinationId);
        }

        #endregion

    }
}
