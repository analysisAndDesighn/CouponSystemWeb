using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class SocialNetworkCouponDataAccess
    {
        private CS_DBEntities3 db;

        public SocialNetworkCouponDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public SocialNetworkCouponDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }

        public void addSocialNetworkCoupon(CatalogCoupons_SocialNetworkCoupon newValue)
        {
            db.CatalogCoupons_SocialNetworkCoupon.Add(newValue);
            db.SaveChanges();
        }

        public bool existsSocialNetworkCoupon(CatalogCoupons_SocialNetworkCoupon newValue)
        {
            CatalogCoupons_SocialNetworkCoupon b = db.CatalogCoupons_SocialNetworkCoupon.Find(newValue.CatalogID);
            return (b != null);
        }

        public CatalogCoupons_SocialNetworkCoupon findCatalogCoupons_SocialNetworkCoupon(int catId)
        {
            CatalogCoupons_SocialNetworkCoupon b = db.CatalogCoupons.Find(catId).CatalogCoupons_SocialNetworkCoupon;
            return b;
        }


        public void removeSocialNetworkCoupon(int catalogID)
        {
            CatalogCoupons_SocialNetworkCoupon sc = db.CatalogCoupons_SocialNetworkCoupon.Find(catalogID);
            //db.CatalogCoupons.Remove(sc.CatalogCoupon);
            db.CatalogCoupons_SocialNetworkCoupon.Remove(sc);
            db.SaveChanges();
        }

        public void updateWebSite(CatalogCoupons_SocialNetworkCoupon newValue, string address)
        {
            db.CatalogCoupons_SocialNetworkCoupon.Find(newValue.CatalogID).origionWebsite = address;
            db.SaveChanges();
        }


        public void updateCouponName(CatalogCoupons_SocialNetworkCoupon newValue, string newName)
        {
            db.CatalogCoupons_SocialNetworkCoupon.Find(newValue.CatalogID).CatalogCoupon.CouponName = newName;
            db.SaveChanges();
        }

        //public void updateDateTime(CatalogCoupons_SocialNetworkCoupon newValue, DateTime newTime)
        //{
        //    db.CatalogCoupons_SocialNetworkCoupon.Find(newValue.CatalogID).CatalogCoupon.deadLineForUse = newTime;
        //    db.SaveChanges();
        //}

        public void updatPriceAfterDiscount(CatalogCoupons_SocialNetworkCoupon newValue, double newPrice)
        {
            db.CatalogCoupons_SocialNetworkCoupon.Find(newValue.CatalogID).CatalogCoupon.priceAfterDiscount = newPrice;
            db.SaveChanges();
        }

        public void updateSocialNetworkCoupon(CatalogCoupons_SocialNetworkCoupon toUpdate,double originalPrice, double afterDiscountPrice,string couponName, string description, string date, string webSite)
        {
            toUpdate.CatalogCoupon.originalPrice = originalPrice;
            toUpdate.CatalogCoupon.priceAfterDiscount = afterDiscountPrice;
            toUpdate.CatalogCoupon.CouponName = couponName;
            toUpdate.CatalogCoupon.description = description;
            toUpdate.CatalogCoupon.deadLineForUse = Convert.ToDateTime(date);
            toUpdate.origionWebsite = webSite;
            toUpdate.CatalogCoupon.Status = "Waiting For approval";
            db.SaveChanges();
        }

    }
}
