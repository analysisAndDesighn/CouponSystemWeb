using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public class UserDataAccesss
    {
        private CS_DBEntities3 db;

        public UserDataAccesss()
        {
            db = DataAccess.getDataAccess();
        }

        public UserDataAccesss(CS_DBEntities3 cS_DBEntities3)
        {
            db = cS_DBEntities3;
        }

        public User getUser(string userName)
        {
            User user = db.Users.Find(userName);
            return user;
        }

    }
}