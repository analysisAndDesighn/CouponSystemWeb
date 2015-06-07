using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public class CouponManager
    {
        public CatalogCouponDataAccess catalogData { get; private set; }
        public SocialNetworkCouponDataAccess socialData { get; private set; }
        public OrderedCouponDataAccess orderData { get; private set; }

        private const string WAIT_STATUS = "Waiting For approval";
        private const string APPROVED_STATUS = "Approved";

        public CouponManager()
        {
            catalogData = new CatalogCouponDataAccess();
            socialData = new SocialNetworkCouponDataAccess();
            orderData = new OrderedCouponDataAccess();
        }

        public CouponManager(CS_DBEntities3 context)
        {
            catalogData = new CatalogCouponDataAccess(context);
            socialData = new SocialNetworkCouponDataAccess(context);
            orderData = new OrderedCouponDataAccess(context);
        }

        public List<CatalogCoupon> getAllCatalogCoupons()
        {
            return catalogData.getAllCoupons();
        }

        public void removeCatalogCoupon(int catalogId)
        {
            catalogData.removeCatalogCoupon(catalogId);
        }
        public OrderedCoupon findOrderedCoupon(string serialKey)
        {
            return orderData.findOrderedCoupon(serialKey);
        }

        public void removeSocialNetworkCoupon(int catalogId)
        {
            //socialData.removeSocialNetworkCoupon(catalogId);
            removeCatalogCoupon(catalogId);
        }

        public void updateOrderedCouponRank(OrderedCoupon oc, short rankValue)
        {
            orderData.updateOrderedCouponRank(oc, rankValue);
        }

        public CatalogCoupon findCatalogCoupon(int catalogId)
        {
            return catalogData.findCatalogCoupon(catalogId);
        }

        public CatalogCoupons_SocialNetworkCoupon findSocialCoupon(int catalogId)
        {
            return socialData.findCatalogCoupons_SocialNetworkCoupon(catalogId);
        }

        public bool createSocialCoupon(double originalPrice, double afterDiscountPrice,
           string buisName, string couponName, string description, string date, string webSite, string userName)
        {
            BusinessManager busiManager = new BusinessManager(catalogData.db);
            UserManager uManager = new UserManager(catalogData.db);
            CatalogCoupon toAdd = new CatalogCoupon();
            toAdd.originalPrice = originalPrice;
            toAdd.priceAfterDiscount = afterDiscountPrice;
            Buisness buis = busiManager.findBuisnessByName(buisName);
            toAdd.Category = buis.Category;
            toAdd.Buisness = buis;
            toAdd.CouponName = couponName;
            toAdd.description = description;
            toAdd.deadLineForUse = Convert.ToDateTime(date);
            toAdd.Location = buis.Location;
            toAdd.Users_SystemAdministrator = uManager.getSystemAdminstrator();
            CatalogCoupons_SocialNetworkCoupon socialAdd = new CatalogCoupons_SocialNetworkCoupon();
            socialAdd.origionWebsite = webSite;
            socialAdd.CatalogCoupon = toAdd;
            socialAdd.CatalogCoupon.Status = WAIT_STATUS;
            socialAdd.User = uManager.findUser(userName);
            try
            {
                socialData.addSocialNetworkCoupon(socialAdd);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool updateSocialCoupon(double originalPrice, double afterDiscountPrice,
             string couponName, string description, string date, string webSite, int catId)
        {
            CatalogCoupons_SocialNetworkCoupon toUpdate = socialData.findCatalogCoupons_SocialNetworkCoupon(catId);
            toUpdate.CatalogCoupon.Status = WAIT_STATUS;
            try
            {
                socialData.updateSocialNetworkCoupon(toUpdate, originalPrice, afterDiscountPrice, couponName, description, date, webSite);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool updateCatalogCoupon(double originalPrice, double afterDiscountPrice,
             string couponName, string description, string date, int catId)
        {
            CatalogCoupon toUpdate = catalogData.findCatalogCoupon(catId);
            toUpdate.Status = WAIT_STATUS;
            try
            {
                catalogData.updateCatalogCoupon(toUpdate, originalPrice, afterDiscountPrice, couponName, description, date);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public bool createCatalogCoupon(double originalPrice, double afterDiscountPrice,
          string buisName, string couponName, string description, string date)
        {
            BusinessManager busiManager = new BusinessManager(catalogData.db);
            UserManager uManager = new UserManager(catalogData.db);
            CatalogCoupon toAdd = new CatalogCoupon();
            toAdd.originalPrice = originalPrice;
            toAdd.priceAfterDiscount = afterDiscountPrice;
            Buisness buis = busiManager.findBuisnessByName(buisName);
            toAdd.Category = buis.Category;
            toAdd.Buisness = buis;
            toAdd.CouponName = couponName;
            toAdd.description = description;
            toAdd.deadLineForUse = Convert.ToDateTime(date);
            toAdd.Location = buis.Location;
            toAdd.Users_SystemAdministrator = uManager.getSystemAdminstrator();

            toAdd.Status = WAIT_STATUS;
            try
            {
                catalogData.addCatalogCoupon(toAdd);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void updateOrderedCouponToUse(OrderedCoupon toUpdate)
        {
            orderData.updateOrderedCouponToUse(toUpdate);
        }

        public void approveCatalogCoupon(int catId)
        {
            catalogData.approveCatalogCoupon(catId);
        }


        internal bool addCategory(string categoryName)
        {
            CategoryDataAccess categoryDt = new CategoryDataAccess();
            return categoryDt.addCategory(categoryName);
        }
    }
}