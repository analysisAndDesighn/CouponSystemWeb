using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class Users_SystemAdministratorDataAccess
    {
       private CS_DBEntities3 db;

        public Users_SystemAdministratorDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public Users_SystemAdministratorDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addUsers_SystemAdministrator(Users_SystemAdministrator user)
        {
            db.Users_SystemAdministrator.Add(user);
            db.SaveChanges();
        }


        public bool existsUsers_SystemAdministrator(Users_SystemAdministrator user)
        {
            Users_SystemAdministrator b = db.Users_SystemAdministrator.Find(user.userName);
            return (b != null);
        }


        public Users_SystemAdministrator findByName(string userName)
        {
            Users_SystemAdministrator b = db.Users_SystemAdministrator.Find(userName);
            return b;
        }

        public void updatePassword(Users_SystemAdministrator user, string password)
        {
            db.Users_SystemAdministrator.Find(user.userName).User.password = password;
            db.SaveChanges();
        }

        public void updateName(Users_SystemAdministrator user, string name)
        {
            db.Users_SystemAdministrator.Find(user.userName).User.fullName = name;
            db.SaveChanges();
        }

        public void updateEmail(Users_SystemAdministrator user, string email)
        {
            db.Users_SystemAdministrator.Find(user.userName).User.email = email;
            db.SaveChanges();
        }

        public void updatePhone(Users_SystemAdministrator user, string phone)
        {
            db.Users_SystemAdministrator.Find(user.userName).User.phone = phone;
            db.SaveChanges();
        }

        public Users_SystemAdministrator getSystemAdministrator()
        {
            return db.Users_SystemAdministrator.First();
        }
        //****************assignment 2***************//


        public List<CatalogCoupon> getAllAwaitingCoupons()
        {
            List<CatalogCoupon> tmp = db.CatalogCoupons.ToList();
            List<CatalogCoupon> result = new List<CatalogCoupon>();
            foreach(CatalogCoupon cat in tmp)
            {
                if (cat.Status!="Approved")
                {
                    result.Add(cat);
                }
            }
            return result;
        }

        public List<Buisness> getAllAwaitingBusiness()
        {
            List<Buisness> tmp = db.Buisnesses.ToList();
            List<Buisness> result = new List<Buisness>();
            foreach (Buisness cat in tmp)
            {
                if (cat.Status != "Approved")
                {
                    result.Add(cat);
                }
            }
            return result;
        }



        public bool existsUsers_adminByUsername(string userName, string password)
        {
            Users_SystemAdministrator b = db.Users_SystemAdministrator.Find(userName);
            if (b == null)
            {
                return false;
            }
            return (password.Equals(b.User.password));

        }
    }
}
