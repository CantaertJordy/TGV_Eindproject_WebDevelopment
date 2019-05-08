using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class HotelReservationService
    {
        private HotelReservationDAO hotelReservationDAO;

        public HotelReservationService()
        {
            hotelReservationDAO = new HotelReservationDAO();
        }

        public IEnumerable<HotelReservations> All()
        {
            return hotelReservationDAO.All();
        }

        public HotelReservations Get(int id)
        {
            return hotelReservationDAO.Get(id);
        }

        public IEnumerable<HotelReservations> GetFromUser(int userId)
        {
            return hotelReservationDAO.GetFromUser(userId);
        }
    }
}
