using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class BuisnessDataAccess
    {
        CS_DBEntities3 db;

        public BuisnessDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public BuisnessDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addBuisness(Buisness b)
        {
            db.Buisnesses.Add(b);
            db.SaveChanges();
        }

        public bool addBuisness(string userName, string busiName, string address, string city, string description,string category,Location location)
        {
            if(existsBuisness(busiName))
            {
                return false;
            }
            Users_BuisnessOwner owner = db.Users_BuisnessOwner.Find(userName);
            Category cat = db.Categories.Find(category);
            Buisness b = new Buisness();
            b.buisName = busiName;
            b.buisAddress = address;
            b.buisCity = city;
            b.BuisDescription = description;
            b.Users_BuisnessOwner = owner;
            b.Category = cat;
            b.Location = location;
            b.Status = "Waiting For approval";
            db.Buisnesses.Add(b);
            try
            {

                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return false;
            }

            //catch
            //{
            //    return false;
            //}
            return true;
        }
        public bool existsBuisness(Buisness buis)
        {
            Buisness b = db.Buisnesses.Find(buis.buisName);
            return (b != null);
        }
        public bool existsBuisness(string buis)
        {
            Buisness b = db.Buisnesses.Find(buis);
            return (b != null);
        }

        public Buisness findBuisness(Buisness buis)
        {
            Buisness b = db.Buisnesses.Find(buis.buisName);
            return b;
        }

        public Buisness findBuisness(string buisName)
        {
            Buisness b = db.Buisnesses.Find(buisName);
            return b;
        }

        public void removeBuisness(Buisness b)
        {
            db.Buisnesses.Remove(b);
            db.SaveChanges();
        }

        public void removeBuisnessByName(string name)
        {
            Buisness b = db.Buisnesses.Find(name);
            db.Buisnesses.Remove(b);
            db.SaveChanges();
        }

        public void updateAddress(Buisness b, string address)
        {
            db.Buisnesses.Find(b.buisName).buisAddress = address;
            db.SaveChanges();
        }

        public void updateCity(Buisness b, string city)
        {
            db.Buisnesses.Find(b.buisName).buisCity = city;
            db.SaveChanges();
        }

        public void updateDescription(Buisness b, string description)
        {
            db.Buisnesses.Find(b.buisName).BuisDescription = description;
            db.SaveChanges();
        }

        public void updateOwner(Buisness b, Users_BuisnessOwner user)
        {
            db.Buisnesses.Find(b.buisName).Users_BuisnessOwner = user;
            db.SaveChanges();
        }

        public void updateLocation(Buisness b, Location loc)
        {
            db.Buisnesses.Find(b.buisName).Location = loc;
            db.SaveChanges();
        }

        public void updateCategory(Buisness b, Category c)
        {
            db.Buisnesses.Find(b.buisName).Category = c;
            db.SaveChanges();
        }

        public List<string> getAllApprovedBuisnesses()
        {
            List<Buisness> list = db.Buisnesses.ToList();
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                if(item.Status=="Approved")
                 result.Add(item.buisName);
            }
            return result;
        }

        public bool updateBusiness(string busiName, string address, string city, string description, string category)
        {
            try
            {
                Buisness b = findBuisness(busiName);
                Category cat = db.Categories.Find(category);
                if (!b.buisAddress.Equals(address) || !b.buisCity.Equals(city) ||
                    !b.BuisDescription.Equals(description) || !b.Category.catName.Equals(cat.catName))
                {
                    b.Status = "Waiting For approval";
                }

                b.buisAddress = address;
                b.buisCity = city;
                b.BuisDescription = description;

                b.Category = cat;

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void approveBusinessCoupon(string buisName)
        {
            Buisness res = findBuisness(buisName);
            res.Status = "Approved";
            db.SaveChanges();
        }

        public List<Buisness> getAllApproved()
        {
            List<Buisness> list = db.Buisnesses.ToList();
            List<Buisness> result = new List<Buisness>();
            foreach (var item in list)
            {
                if (item.Status == "Approved")
                    result.Add(item);
            }
            return result;
        }
    }
}
