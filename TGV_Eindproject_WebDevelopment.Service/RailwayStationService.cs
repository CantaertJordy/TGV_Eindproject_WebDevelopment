using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class RailwayStationService
    {

        #region Repositories

        private readonly RailwayStationDAO railwayStationDAO;

        #endregion

        #region Constructors

        public RailwayStationService()
        {
            railwayStationDAO = new RailwayStationDAO();
        }

        #endregion

        #region Get-Methods

        public IEnumerable<RailwayStations> All()
        {
            return railwayStationDAO.All();
        }

        public RailwayStations Get(int id)
        {
            return railwayStationDAO.Get(id);
        }

        #endregion

    }
}
