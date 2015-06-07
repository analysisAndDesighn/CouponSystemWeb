using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouponSystemWeb.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CouponSystemWeb.App_Code.Tests
{
    [TestClass()]
    public class SocialNetworkCouponDataAccessTests
    {

        SocialNetworkCouponDataAccess soCup_da;
        CatalogCoupon cat1;
        Location l1;
        Category c1;
        Users_SystemAdministrator uAdmin;
        Users_BuisnessOwner ub1;
        Buisness b1;
        User user1;
        User user2;
        User user3;
        CatalogCoupons_SocialNetworkCoupon socCoup;

        [TestInitialize]
        public void TestInitSocialCoupon()
        {
            //making sure the table is empty
            TestUtils.clearAllTable();
            uAdmin = new Users_SystemAdministrator();
            user2 = new User();
            user2.userName = "admin";
            uAdmin.User = user2;
            cat1 = new CatalogCoupon();
            cat1.Users_SystemAdministrator = uAdmin;
            cat1.CouponName = "free desert";
            user1 = new User();
            c1 = new Category();
            b1 = new Buisness();
            l1 = new Location();
            l1.latitude = 34.8999;
            l1.longitude = 45.3666;
            ub1 = new Users_BuisnessOwner();
            b1.BuisDescription = "Sushi bar";
            b1.buisAddress = "Aharom Meskin 13";
            b1.buisCity = "Beer-Sheva";
            b1.buisName = "JAPANIKA";
            user1.userName = "buserName";
            c1.catName = "food";
            ub1.User = user1;
            b1.Users_BuisnessOwner = ub1;
            b1.Category = c1;
            b1.Location = l1;
            b1.Status = TestUtils.APPROVED_STATUS;
            cat1.Category = c1;
            cat1.Location = l1;
            cat1.Buisness = b1;
            cat1.Status = TestUtils.APPROVED_STATUS;
            cat1.deadLineForUse = DateTime.Today;
            cat1.originalPrice = 30;
            cat1.priceAfterDiscount = 10;

            user3 = new User();
            user3.userName = "customer";
            socCoup = new CatalogCoupons_SocialNetworkCoupon();
            socCoup.CatalogCoupon = cat1;
            socCoup.User = user3;
            socCoup.origionWebsite = "www.rrere.com";
            
            soCup_da = new SocialNetworkCouponDataAccess(new CS_DBEntities3());

        }

        [TestMethod]
        public void addSocialNetwork()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void removeSocialNetwork()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            soCup_da.removeSocialNetworkCoupon(socCoup.CatalogID);
            Assert.IsFalse(soCup_da.existsSocialNetworkCoupon(socCoup));
            CatalogCouponDataAccess ca_da = new CatalogCouponDataAccess(new CS_DBEntities3());
            Assert.IsFalse(ca_da.existsCatalogCoupon(socCoup.CatalogCoupon));
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateOriginWebsite()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            string newWeb = "www,test.co.il";
            soCup_da.updateWebSite(socCoup, newWeb);
            Assert.AreEqual(socCoup.origionWebsite, newWeb);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateCouponName()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            string newName = "AGADIR";
            soCup_da.updateCouponName(socCoup, newName);
            Assert.AreEqual(socCoup.CatalogCoupon.CouponName, newName);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updatePriceAfterDiscount()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            double newPrice = 8.9;
            soCup_da.updatPriceAfterDiscount(socCoup, newPrice);
            Assert.AreEqual(socCoup.CatalogCoupon.priceAfterDiscount, newPrice);
            TestUtils.clearAllTable();
        }

        
        [TestMethod()]
        public void updateSocialNetworkCouponTest()
        {
            soCup_da.addSocialNetworkCoupon(socCoup);
            Assert.IsTrue(soCup_da.existsSocialNetworkCoupon(socCoup));
            string newName = "1+2 on sushi";
            string website = "www.jupanika.com";
            soCup_da.updateSocialNetworkCoupon(socCoup, (double)socCoup.CatalogCoupon.originalPrice,
                (double)socCoup.CatalogCoupon.priceAfterDiscount, newName,
                socCoup.CatalogCoupon.description, 
                socCoup.CatalogCoupon.deadLineForUse.ToString(), website);
            CatalogCoupons_SocialNetworkCoupon after = 
                soCup_da.findCatalogCoupons_SocialNetworkCoupon(socCoup.CatalogID);
            Assert.AreEqual(after.CatalogCoupon.CouponName, newName);
            Assert.AreEqual(after.origionWebsite, website);
            TestUtils.clearAllTable();
        }
    }
}
