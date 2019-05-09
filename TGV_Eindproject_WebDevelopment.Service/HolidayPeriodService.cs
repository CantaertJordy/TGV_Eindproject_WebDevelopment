using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class HolidayPeriodService
    {
        private HolidayPeriodDAO holidayPeriodDAO;

        public HolidayPeriodService()
        {
            holidayPeriodDAO = new HolidayPeriodDAO();
        }

        public IEnumerable<HolidayPeriods> All()
        {
            return holidayPeriodDAO.All();
        }

        public HolidayPeriods Get(int id)
        {
            return holidayPeriodDAO.Get(id);
        }
    }
}
