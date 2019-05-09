using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class HotelReservationDAO
    {
        private readonly TGVsContext _dbContext;

        public HotelReservationDAO()
        {
            _dbContext = new TGVsContext();
        }

        public IEnumerable<HotelReservations> All()
        {
            return _dbContext.HotelReservations.ToList();
        }

        public HotelReservations Get(int id)
        {
            return _dbContext.HotelReservations.Where(hotelReservation => hotelReservation.Id == id).FirstOrDefault();
        }

        public IEnumerable<HotelReservations> GetFromUser(int userId)
        {
            return _dbContext.HotelReservations.Where(hotelReservation => hotelReservation.UserId == userId).ToList();
        }
    } 
}
