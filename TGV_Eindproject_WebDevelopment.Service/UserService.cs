﻿using System;
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

        public void Create(Users entity)
        {
            userDAO.Create(entity);
        }
    }
}
