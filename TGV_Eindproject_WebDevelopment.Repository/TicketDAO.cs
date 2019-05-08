using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class TicketDAO
    {

        #region DatabaseContext

        private readonly TGVsContext _dbContext;

        #endregion

        #region Contstructors

        public TicketDAO()
        {
            _dbContext = new TGVsContext();
        }

        #endregion

        #region Select-Methods

        private IEnumerable<Tickets> All()
        {
            return _dbContext.Tickets.ToList();
        }

        private IEnumerable<Tickets> AllFromUser(int userId)
        {
            return _dbContext.Tickets.Where(t => t.UserId == userId).ToList();
        }

        private Tickets Get(int id)
        {
            return _dbContext.Tickets.Where(t => t.Id == id).First();
        }

        private IEnumerable<Tickets> AllForTGV(int tgvId, DateTime dateOfDeparture)
        {
            return _dbContext.Tickets.Where(t => t.Tgvid == tgvId && t.DateOfDeparture.Date.Equals(dateOfDeparture.Date)).ToList();
        }

        #endregion

        #region Update-Methods

        public void Update(Tickets entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        #endregion

        #region Create-Methods

        public void Create(Tickets entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        #endregion

    }
}
