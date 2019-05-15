using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class TGVDAO
    {

        #region DatabaseContext

        private readonly TGVsContext _dbContext;

        #endregion

        #region Constructors

        public TGVDAO()
        {
            _dbContext = new TGVsContext();
        }

        #endregion

        #region Select-Statements

        public IEnumerable<Tgvs> All()
        {
            return _dbContext.Tgvs.Include(tgv => tgv.LineNavigation).ToList();
        }

        public IEnumerable<Tgvs> GetWithLine(int lineId)
        {
            return _dbContext.Tgvs.Where(tgv => tgv.Line == lineId).Include(tgv => tgv.LineNavigation).ToList();
        }

        public Tgvs Get(int id)
        {
            return _dbContext.Tgvs.Where(tgv => tgv.Id == id).Include(tgv => tgv.LineNavigation).First();
        }

        #endregion

    }
}
