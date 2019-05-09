using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class HotelDAO
    {
        private readonly TGVsContext _dbContext;

        public HotelDAO()
        {
            _dbContext = new TGVsContext();
        }

        public IEnumerable<Hotels> All()
        {
            return _dbContext.Hotels.ToList();
        }

        public Hotels Get(int id)
        {
            return _dbContext.Hotels.Where(hotel => hotel.Id == id).FirstOrDefault();
        }

        public IEnumerable<Hotels> Get(string city)
        {
            return _dbContext.Hotels.Where(hotel => hotel.City == city).ToList();
        }
    }
}
