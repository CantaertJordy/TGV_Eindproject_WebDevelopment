using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class HolidayPeriodDAO
    {
        private readonly TGVsContext _dbContext;

        public HolidayPeriodDAO()
        {
            _dbContext = new TGVsContext();
        }

        public IEnumerable<HolidayPeriods> All()
        {
            return _dbContext.HolidayPeriods.ToList();
        }

        public HolidayPeriods Get(int id)
        {
            return _dbContext.HolidayPeriods.Where(holidayPeriod => holidayPeriod.Id == id).FirstOrDefault();
        }

    }
}
