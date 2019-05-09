using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class HotelsService
    {
        private HotelDAO hotelDAO;
        
        public HotelsService()
        {
            hotelDAO = new HotelDAO();
        }

        public IEnumerable<Hotels> All()
        {
            return hotelDAO.All();
        }

        public Hotels Get(int id)
        {
            return hotelDAO.Get(id);
        }

        public IEnumerable<Hotels> Get(string city)
        {
            return hotelDAO.Get(city);
        }
    }
}
