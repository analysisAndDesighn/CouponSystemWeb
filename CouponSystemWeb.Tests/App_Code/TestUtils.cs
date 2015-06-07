using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code.Tests
{
    public static class TestUtils
    {

        public const string WAIT_STATUS = "Waiting For approval";
        public const string APPROVED_STATUS = "Approved";

        public static void clearAllTable()
        {
            using (var db = new CS_DBEntities3())
            {
                foreach (var cat in db.Categories)
                {
                    db.Entry(cat).Collection("Users_Customer").Load();

                    foreach (var item in db.Users_Customer)
                    {
                        cat.Users_Customer.Remove(item);
                    }
                }

                db.SaveChanges();

                var query1 = from b in db.Buisnesses
                             select b;
                foreach (var item in query1)
                {
                    db.Buisnesses.Remove(item);
                }

                var query2 = from b in db.Users
                             select b;
                foreach (var item in query2)
                {
                    db.Users.Remove(item);
                }

                var query3 = from b in db.Categories
                             select b;
                foreach (var item in query3)
                {
                    db.Categories.Remove(item);
                }

                var query4 = from b in db.Users_BuisnessOwner
                             select b;
                foreach (var item in query4)
                {
                    db.Users_BuisnessOwner.Remove(item);
                }

                var query5 = from b in db.Locations
                             select b;
                foreach (var item in query5)
                {
                    db.Locations.Remove(item);
                }

                var query6 = from b in db.CatalogCoupons
                             select b;
                foreach (var item in query6)
                {
                    db.CatalogCoupons.Remove(item);
                }

                var query7 = from b in db.CatalogCoupons_SocialNetworkCoupon
                             select b;
                foreach (var item in query7)
                {
                    db.CatalogCoupons_SocialNetworkCoupon.Remove(item);
                }

                var query8 = from b in db.OrderedCoupons
                             select b;
                foreach (var item in query8)
                {
                    db.OrderedCoupons.Remove(item);
                }

                var query9 = from b in db.Users_Customer
                             select b;
                foreach (var item in query9)
                {
                    db.Users_Customer.Remove(item);
                }

                var query10 = from b in db.Users_SystemAdministrator
                              select b;
                foreach (var item in query10)
                {
                    db.Users_SystemAdministrator.Remove(item);
                }
                db.SaveChanges();
            }

        }
    }
}
