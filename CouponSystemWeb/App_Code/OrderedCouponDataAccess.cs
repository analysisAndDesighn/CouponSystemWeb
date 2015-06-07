using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class OrderedCouponDataAccess
    {
        private CS_DBEntities3 db;

        public OrderedCouponDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public OrderedCouponDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addOrderedCoupons(OrderedCoupon ordered)
        {

            db.OrderedCoupons.Add(ordered);
            db.SaveChanges();
        }

        public bool existsOrderedCoupons(OrderedCoupon ordered)
        {
            OrderedCoupon b = db.OrderedCoupons.Find(ordered.serialKey);
            return (b != null);
        }

        public OrderedCoupon findOrderedCoupon(string serialKey)
        {
            OrderedCoupon b = db.OrderedCoupons.Find(serialKey);
            return b;
        }

        public void removeOrderedCoupons(OrderedCoupon ordered)
        {
            db.OrderedCoupons.Remove(ordered);
            db.SaveChanges();
        }

        public void updateOrderedCouponRank(OrderedCoupon ordered, short newRank)
        {
            OrderedCoupon oc = db.OrderedCoupons.Find(ordered.serialKey);
            oc.rank = newRank;
            db.SaveChanges();
            CatalogCouponDataAccess dt = new CatalogCouponDataAccess(this.db);
            dt.updatAverageRank(oc.CatalogCoupon);
        }


        public void updateOrderedCouponsDeadLineForUse(OrderedCoupon ordered, DateTime newTime)
        {
            db.OrderedCoupons.Find(ordered.serialKey).CatalogCoupon.deadLineForUse = newTime;
            db.SaveChanges();
        }

        public void updatOrderedCouponsPriceAfterDiscount(OrderedCoupon ordered, double newPrice)
        {
            db.OrderedCoupons.Find(ordered.serialKey).CatalogCoupon.priceAfterDiscount = newPrice;
            db.SaveChanges();
        }

        public void updateOrderedisUsed(OrderedCoupon ordered, bool isUsesd)
        {
            db.OrderedCoupons.Find(ordered.serialKey).isUsed = isUsesd;
            db.SaveChanges();
        }

        public void updateOrderedCouponsDateOfPurchase(OrderedCoupon ordered, DateTime newTime)
        {
            db.OrderedCoupons.Find(ordered.serialKey).dateOfPurchase = newTime;
            db.SaveChanges();
        }

        public void updateOrderedCouponsDateOfUse(OrderedCoupon ordered, DateTime newTime)
        {
            db.OrderedCoupons.Find(ordered.serialKey).dateOfUse = newTime;
            db.SaveChanges();
        }


        public void updateOrderedCouponToUse(OrderedCoupon toUpdate)
        {
            toUpdate.isUsed = true;
            toUpdate.dateOfUse = DateTime.Now;
            db.SaveChanges();
        }
    }
}
