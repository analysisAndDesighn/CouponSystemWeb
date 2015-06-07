using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class Users_CustomerDataAccess
    {
         private CS_DBEntities3 db;

        public Users_CustomerDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public Users_CustomerDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addUsers_Customer(Users_Customer user)
        {
            db.Users_Customer.Add(user);
            db.SaveChanges();
        }

        public bool existsUsers_Customer(Users_Customer user)
        {
            Users_Customer b = db.Users_Customer.Find(user.userName);
            return (b != null);
        }

        public Users_Customer findUsers_Customer(Users_Customer user)
        {
            Users_Customer b = db.Users_Customer.Find(user.userName);
            return b;
        }

        public Users_Customer findByName(string user)
        {
            Users_Customer b = db.Users_Customer.Find(user);
            return b;
        }

        public void removeUsers_Customer(Users_Customer user)
        {
            db.Users_Customer.Remove(user);
            db.SaveChanges();
        }


        public void updateLocation(Users_Customer user, Location loc)
        {
            db.Users_Customer.Find(user.userName).Location = loc;
            db.SaveChanges();
        }

        private string createSerial()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";
            Random random = new Random(DateTime.Now.Millisecond);
            string result = new string(
                Enumerable.Repeat(chars, 20)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public void addCoupon(Users_Customer user, CatalogCoupon c)
        {
            OrderedCoupon orded = new OrderedCoupon();
            orded.CatalogCoupon = c;
            orded.dateOfPurchase = DateTime.Today;
            orded.isUsed = false;
            orded.Users_Customer = user;
            orded.serialKey = createSerial();
            db.OrderedCoupons.Add(orded);
            db.SaveChanges();
        }

        public void removeCategory(Users_Customer user, Category c)
        {
            db.Users_Customer.Find(user.userName).Categories.Remove(c);
            db.SaveChanges();
        }


        //****************assignment 2***************//

        public bool existsUsers_CustomerByUsername(string userName, string pass)
        {
            Users_Customer b = db.Users_Customer.Find(userName);
            if (b == null)
            {
                return false;
            }
            return (pass.Equals(b.User.password));
        }

        public void updateCustomer(string userName,string password, string email, string fullName, string phone, string gender, short age, List<string>stringCategories)
        {
            Users_Customer b = db.Users_Customer.Find(userName);
            b.Categories.Clear();
            
            b.User.password = password;
            b.User.email = email;
            b.User.fullName = fullName;
            b.User.phone = phone;
            b.gender = gender;
            b.age = age;
            ICollection<Category> newCat = new List<Category>();
            foreach(string cat in stringCategories)
            {
                newCat.Add(db.Categories.Find(cat));
            }
            b.Categories = newCat;
            db.SaveChanges();
        }
    }
}
