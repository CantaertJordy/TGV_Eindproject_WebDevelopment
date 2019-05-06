using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class LineDAO
    {

        #region DatabaseContext

        private readonly TGVsContext _dbContext;

        #endregion

        #region Constructors

        public LineDAO()
        {
            _dbContext = new TGVsContext();
        }

        #endregion

        #region Select-Methods

        public IEnumerable<Lines> All()
        {
            return _dbContext.Lines.Include(line => line.DepartureNavigation).Include(line => line.DestinationNavigation).ToList();
        }

        public Lines LineWithDepartureAndDestination(int departureId, int destinationId)
        {
            return _dbContext.Lines.Where(line => line.Departure == departureId && line.Destination == destinationId).FirstOrDefault();
        }

        public IEnumerable<Lines> LinesWithDeparture(int departureId)
        {
            return _dbContext.Lines.Where(line => line.Departure == departureId).ToList();
        }

        public IEnumerable<Lines> LinesWithDestination(int destinationId)
        {
            return _dbContext.Lines.Where(line => line.Destination == destinationId).ToList();
        }

        #endregion

    }
}
