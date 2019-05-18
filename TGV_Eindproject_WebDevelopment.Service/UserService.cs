using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{

    

    public class UserService
    {
        private UserDAO userDAO;
        
        public UserService()
        {
            userDAO = new UserDAO();
        }

        public IEnumerable<Users> All()
        {
            return userDAO.All();
        }

        public Users Get(int id)
        {
            return userDAO.Get(id);
        }

        public Users Get(String netUserId)
        {
            return userDAO.Get(netUserId);
        }

        public void Create(Users entity)
        {
            if (All().Count() > 0)
                entity.Id = All().Last().Id + 1;
            else
                entity.Id = 1;
            userDAO.Create(entity);
        }
    }
}
