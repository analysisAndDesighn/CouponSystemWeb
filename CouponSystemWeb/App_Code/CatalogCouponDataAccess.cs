using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class CatalogCouponDataAccess
    {
        public CS_DBEntities3 db { get; private set; }

        public CatalogCouponDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public CatalogCouponDataAccess(CS_DBEntities3 context)
        {
            db = context;
        }


        public void addCatalogCoupon(CatalogCoupon cp)
        {
            db.CatalogCoupons.Add(cp);
            db.SaveChanges();
        }


        public bool existsCatalogCoupon(CatalogCoupon cp)
        {
            if (cp != null)
            {
                CatalogCoupon cTemp = db.CatalogCoupons.Find(cp.catalogID);
                return (cTemp != null);
            }
            return false;
            
        }

        public CatalogCoupon findCatalogCoupon(CatalogCoupon cp)
        {
            CatalogCoupon cTmp = db.CatalogCoupons.Find(cp.catalogID);
            return cTmp;
        }

        public CatalogCoupon findCatalogCoupon(int id)
        {
            CatalogCoupon cTmp = db.CatalogCoupons.Find(id);
            return cTmp;
        }

        public void removeCatalogCoupon(int CatId)
        {
            CatalogCoupon cp = db.CatalogCoupons.Find(CatId);
            if (cp.OrderedCoupons != null)
                db.OrderedCoupons.RemoveRange(cp.OrderedCoupons.ToList());
            if (cp.CatalogCoupons_SocialNetworkCoupon != null)
                db.CatalogCoupons_SocialNetworkCoupon.Remove(cp.CatalogCoupons_SocialNetworkCoupon);
            cp.CatalogCoupons_SocialNetworkCoupon = null;
            db.CatalogCoupons.Remove(cp);
            db.SaveChanges();
        }

        public void updateDescription(CatalogCoupon cp, string name)
        {
            db.CatalogCoupons.Find(cp.catalogID).description = name;
            db.SaveChanges();
        }


        public void updatePriceAfterDiscount(CatalogCoupon cp, double newValue)
        {
            db.CatalogCoupons.Find(cp.catalogID).priceAfterDiscount = newValue;
            db.SaveChanges();
        }

        public void updatAverageRank(CatalogCoupon cp)
        {
            double avg = getAverage(cp);
            if (avg > 0)
            {
                //db.CatalogCoupons.Find(cp.catalogID).averageRank = avg;
                cp.averageRank = avg;
                db.SaveChanges();
            }

            
        }


        private double getAverage(CatalogCoupon cp)
        {
            ICollection<OrderedCoupon> orderd = cp.OrderedCoupons;
            if (orderd == null || orderd.Count == 0)
            {
                return 0;
            }
            else
            {
                double size = 0;
                double sum = 0;
                foreach (OrderedCoupon item in orderd)
                {
                    if (item.rank != null)
                    {
                        size++;
                        sum += (int)item.rank;
                    }
                }
                double avg = sum / size;
                return avg;
            }
        }

        /// <summary>
        /// gets all catalog coupons that is not expired and approved
        /// </summary>
        /// <returns></returns>
        public List<CatalogCoupon> getAllCoupons()
        {
            List<CatalogCoupon> tmp = db.CatalogCoupons.ToList();
            List<CatalogCoupon> result = db.CatalogCoupons.ToList();
            foreach (var item in tmp)
            {
                if (item.deadLineForUse < DateTime.Today || item.Status == "Waiting For approval")
                {
                    result.Remove(item);
                }
            }
            return result;
        }

        public void updateCatalogCoupon(CatalogCoupon toUpdate, double originalPrice, double afterDiscountPrice, string couponName, string description, string date)
        {
            toUpdate.originalPrice = originalPrice;
            toUpdate.priceAfterDiscount = afterDiscountPrice;
            toUpdate.CouponName = couponName;
            toUpdate.description = description;
            toUpdate.deadLineForUse = Convert.ToDateTime(date);
            toUpdate.Status = "Waiting For approval";
            db.SaveChanges();
        }

        public void approveCatalogCoupon(int catId)
        {
            CatalogCoupon res = db.CatalogCoupons.Find(catId);
            res.Status = "Approved";
            db.SaveChanges();
        }

    }
}
