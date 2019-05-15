using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class RailwayStationDAO
    {

        #region DatabaseContext

        private readonly TGVsContext _dbContext;

        #endregion

        #region Constructors

        public RailwayStationDAO()
        {
            _dbContext = new TGVsContext();
        }

        #endregion

        #region Select-Methods

        public IEnumerable<RailwayStations> All()
        {
            return _dbContext.RailwayStations.ToList();
        }

        public RailwayStations Get(int id)
        {
            return _dbContext.RailwayStations.Where(station => station.Id == id).First();
        }

        public RailwayStations Get(string city)
        {
            return _dbContext.RailwayStations.Where(station => station.City.ToLower().Equals(city.ToLower())).First();
        }

        #endregion

    }
}
