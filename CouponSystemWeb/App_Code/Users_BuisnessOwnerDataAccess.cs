using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class Users_BuisnessOwnerDataAccess
    {
        private CS_DBEntities3 db;

        public Users_BuisnessOwnerDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public Users_BuisnessOwnerDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addUsers_BuisnessOwner(Users_BuisnessOwner b)
        {
            db.Users_BuisnessOwner.Add(b);
            db.SaveChanges();
        }

        public bool existsUsers_BuisnessOwner(Users_BuisnessOwner buis)
        {
            Users_BuisnessOwner b = db.Users_BuisnessOwner.Find(buis.userName);
            return (b != null);
        }

        public void removeUsers_BuisnessOwner(string userName)
        {
            Users_BuisnessOwner b = findByName(userName);
            if (b != null)
            {
                db.Users_BuisnessOwner.Remove(b);
                db.SaveChanges();
            }
            
        }

        public Users_BuisnessOwner findByName(string user)
        {
            Users_BuisnessOwner b = db.Users_BuisnessOwner.Find(user);
            return b;
        }

        public bool existsUsers_BuisnessOwnerByUsername(string userName, string pass)
        { 
            Users_BuisnessOwner b = db.Users_BuisnessOwner.Find(userName);
            if (b == null)
            {
                return false;
            }
            return (pass.Equals(b.User.password));
        }

        public void updateBusinessOwner(string userName, string password, string email, string fullName, string phone)
        {
            Users_BuisnessOwner b = db.Users_BuisnessOwner.Find(userName);
            b.User.password = password;
            b.User.email = email;
            b.User.fullName = fullName;
            b.User.phone = phone;
            db.SaveChanges();
        }

    }
}
