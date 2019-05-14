using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;

namespace TGV_Eindproject_WebDevelopment.Repository
{
    public class UserDAO
    {
        #region DatabaseContext

        private readonly TGVsContext _dbContext;

        #endregion

        #region Constructors

        public UserDAO()
        {
            _dbContext = new TGVsContext();
        }

        #endregion

        #region Select-Methods

        public IEnumerable<Users> All()
        {
            return _dbContext.Users.ToList();
        }

        public Users Get(int id)
        {
            return _dbContext.Users.Where(user => user.Id == id).FirstOrDefault();
        }

        #endregion

        #region Update-Methods



        #endregion

        #region Create-Methods
        public void Create(Users entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        #endregion
    }
}
