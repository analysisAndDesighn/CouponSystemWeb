using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public class BusinessManager
    {
        BuisnessDataAccess BuisData;

        private const string WAIT_STATUS = "Waiting For approval";
        private const string APPROVED_STATUS = "Approved";


        public BusinessManager()
        {
            BuisData = new BuisnessDataAccess();
        }

        public BusinessManager(CS_DBEntities3 cS_DBEntities3)
        {
            BuisData = new BuisnessDataAccess(cS_DBEntities3);
        }

        

        public void removeBuisnessByName(string buisName)
        {
            CouponManager couponManager = new CouponManager();
            List<CatalogCoupon> allCoupons = BuisData.findBuisness(buisName).CatalogCoupons.ToList();
            for (int i = 0; i < allCoupons.Count;i++ )
            {
               couponManager.removeCatalogCoupon(allCoupons.ElementAt(i).catalogID);
            }
            BuisData.findBuisness(buisName).CatalogCoupons.ToList().Clear();
                BuisData.removeBuisnessByName(buisName);
        }



        public Buisness findBuisnessByName(string buisName)
        {
            return BuisData.findBuisness(buisName);
        }


       

        public bool addBuisness(string userName, string busiName, string address, string city, string description, string category)
        {
            UserManager uManager = new UserManager();
            Location tmpLocation = uManager.getLocation();
            return BuisData.addBuisness(userName, busiName, address, city, description, category, tmpLocation);

        }

       

        public void approveBusinessCoupon(string buisName)
        {
            BuisData.approveBusinessCoupon(buisName);
        }


        public bool updateBusiness(string busiName, string address, string city, string description, string category)
        {
            return BuisData.updateBusiness(busiName, address, city, description, category);
        }

        

        public List<string> getApprovedBusiness()
        {
            return BuisData.getAllApprovedBuisnesses();
        }

        internal List<Buisness> getAllApprovedBusiness()
        {
            return BuisData.getAllApproved(); ;
        }
    }
}