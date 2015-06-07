using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public enum UserType {Admin, BuisnessOwner, Customer,NONE}; 

    public class SystemController
    {
        private BusinessManager businessManager;
        private UserManager userManagar;
        private CouponManager couponManager;

        public SystemController()
        {
            businessManager = new BusinessManager();
            userManagar = new UserManager();
            couponManager = new CouponManager();
        }

        public SystemController(CS_DBEntities3 cS_DBEntities3)
        {
            businessManager = new BusinessManager(cS_DBEntities3);
            userManagar = new UserManager(cS_DBEntities3);
            couponManager = new CouponManager(cS_DBEntities3);   
        }

        public UserType findUser(string userName, string password)
        {
            return userManagar.findUser(userName, password);
        }

        public bool createBuisnessOwner(string userName, string pass, string email, string name, string phone)
        {
            return userManagar.createBuisnessOwner(userName, pass, email, name, phone);
        }

        public bool createCustomer(string userName, string pass, string email, string name, string phone, string gender, short age, List<string> categories)
        {
            return userManagar.createCustomer(userName, pass, email, name, phone, gender, age, categories);
        }

        public List<string> getAllCategories()
        {
            return userManagar.getAllCategories();
        }

        public Users_Customer findCustomerByName(string userName)
        {
            return userManagar.findCustomerByName(userName);

        }

        public List<CatalogCoupon> getAllCatalogCoupons()
        {
            return couponManager.getAllCatalogCoupons();
        }

        public void removeSocialNetworkCoupon(int catalogId)
        {
            couponManager.removeSocialNetworkCoupon(catalogId);
        }

        public OrderedCoupon findOrderedCoupon(string serialKey)
        {
            return couponManager.findOrderedCoupon(serialKey);
        }

        public void updateOrderedCouponRank(OrderedCoupon oc, short rankValue)
        {
            couponManager.updateOrderedCouponRank(oc, rankValue);
        }

        public CatalogCoupon findCatalogCoupon(int catalogId)
        {
            return couponManager.findCatalogCoupon(catalogId);
        }

        public CatalogCoupons_SocialNetworkCoupon findSocialCoupon(int catalogId)
        {
            return couponManager.findSocialCoupon(catalogId);
        }

        public void addCoupon(Users_Customer loggedUser, CatalogCoupon coupon)
        {
            userManagar.addCoupon(loggedUser, coupon);
        }

        public Users_BuisnessOwner findBuisnessOwnerByName(string userName)
        {
            return userManagar.findBuisnessOwnerByName(userName);
        }

        public Users_SystemAdministrator findAdminByName(string userName)
        {
            return userManagar.findAdminByName(userName);
        }

        public Buisness findBuisnessByName(string buisName)
        {
            return businessManager.findBuisnessByName(buisName);
        }

        public Users_SystemAdministrator getSystemAdminstrator()
        {
            return userManagar.getSystemAdminstrator();
        }

        public List<string> getBusinessForOwner(string userName)
        {
            return userManagar.getBusinessForOwner(userName);
        }

        public List<string> getApprovedBusinessForOwner(string userName)
        {
            return userManagar.getApprovedBusinessForOwner(userName);
        }

        public bool createSocialCoupon(double originalPrice, double afterDiscountPrice,
             string buisName, string couponName, string description, string date, string webSite, string userName)
        {
            return couponManager.createSocialCoupon(originalPrice, afterDiscountPrice, buisName, couponName, description, date, webSite, userName);
        }

        public bool updateSocialCoupon(double originalPrice, double afterDiscountPrice,
               string couponName, string description, string date, string webSite, int catalogId)
        {
            return couponManager.updateSocialCoupon(originalPrice, afterDiscountPrice, couponName, description, date, webSite, catalogId);
        }

        public bool updateCatalogCoupon(double originalPrice, double afterDiscountPrice,
       string couponName, string description, string date, int catalogId)
        {
            return couponManager.updateCatalogCoupon(originalPrice, afterDiscountPrice, couponName, description, date, catalogId);
        }

        public bool createCatalogCoupon(double originalPrice, double afterDiscountPrice,
         string buisName, string couponName, string description, string date)
        {
            return couponManager.createCatalogCoupon(originalPrice, afterDiscountPrice, buisName, couponName, description, date);
        }
        public bool addBuisness(string userName, string busiName, string address, string city, string description, string category)
        {
            return businessManager.addBuisness(userName, busiName, address, city, description, category);
        }

        public List<string> getCustomerCategories(Users_Customer customer)
        {
            return userManagar.getCustomerCategories(customer);
        }

        public void updateCustomer(string userName, string password, string email, string fullName, string phone, string gender, short age, List<string> stringCategories)
        {
            userManagar.updateCustomer(userName, password, email, fullName, phone, gender, age, stringCategories);
        }

        public void updateBusinessOwner(string userName, string password, string email, string fullName, string phone)
        {
            userManagar.updateBusinessOwner(userName, password, email, fullName, phone);
        }


        public void removeBuisnessByName(string buisName)
        {
            businessManager.removeBuisnessByName(buisName);
        }

        public List<CatalogCoupon> getAllCouponsBusinessOwner(string userName)
        {
            Users_BuisnessOwner owner = userManagar.findBuisnessOwnerByName(userName);
            ICollection<Buisness> buisness = owner.Buisnesses;
            List<CatalogCoupon> result = new List<CatalogCoupon>();
            foreach (var item in buisness)
            {
                foreach (CatalogCoupon coupon in item.CatalogCoupons)
                {
                    result.Add(coupon);
                }

            }
            return result;
        }

        public List<CatalogCoupon> getAllCouponsBusiness(string buisName)
        {
            Buisness business = businessManager.findBuisnessByName(buisName);
            return business.CatalogCoupons.ToList();
        }

        public void removeCatalogCoupon(int catId)
        {
            couponManager.removeCatalogCoupon(catId);
        }

        public List<CatalogCoupon> getAllAwaitingCoupons()
        {
            return userManagar.getAllAwaitingCoupons();
        }

        public void approveCatalogCoupon(int catId)
        {
            couponManager.approveCatalogCoupon(catId);
        }

        public void approveBusinessCoupon(string buisName)
        {
            businessManager.approveBusinessCoupon(buisName);
        }

        public bool updateBusiness(string busiName, string address,string city, string description, string category)
        {
            return businessManager.updateBusiness( busiName,  address, city,  description,  category);
        }

        public List<Buisness> getAllAwaitingBusiness()
        {
            return userManagar.getAllAwaitingBusiness();
        }




        public bool checkCouponBelong(string owner, string serial)
        {
            OrderedCoupon orCoup=findOrderedCoupon(serial);
            if (orCoup == null || orCoup.isUsed==true)
                return false;
            return orCoup.CatalogCoupon.Buisness.Users_BuisnessOwner.userName.Equals(owner);
        }

        public void updateOrderedCouponToUse(string serial)
        {
            OrderedCoupon toUpdate = findOrderedCoupon(serial);
            couponManager.updateOrderedCouponToUse(toUpdate);
        }

        internal bool addAdmin(string adminName, string password, string email, string fullName, string phone)
        {
           return userManagar.addAdmin( adminName,  password,  email,  fullName,  phone);
        }

        public List<string> getApprovedBusiness()
        {
            return businessManager.getApprovedBusiness();
        }

        public List<Buisness> getAllApprovedBusiness()
        {
            return businessManager.getAllApprovedBusiness();
        }

        internal bool addCategory(string categoryName)
        {
            return couponManager.addCategory(categoryName);
        }
    }
}