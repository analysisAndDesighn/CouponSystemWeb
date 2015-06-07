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
    public class CouponManagerTests
    {
        CatalogCoupon cat1;
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Category c1;
        Users_SystemAdministrator uAdmin;
        Users_BuisnessOwner ub1;
        Buisness b1;
        User user1;
        User user2;
        User user3;
        CouponManager cManager;

        [TestInitialize]
        public void init()
        {
            TestUtils.clearAllTable();
            uAdmin = new Users_SystemAdministrator();
            user2 = new User();
            user2.userName = "admin";
            user2.password = "admin";
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
            user1.password = "123";
            c1.catName = "food";
            ub1.User = user1;
            b1.Users_BuisnessOwner = ub1;
            b1.Category = c1;
            b1.Location = l1;
            b1.Status = TestUtils.APPROVED_STATUS;
            BuisnessDataAccess bdt = new BuisnessDataAccess(new CS_DBEntities3());
            Users_SystemAdministratorDataAccess udt = new Users_SystemAdministratorDataAccess(new CS_DBEntities3());
            udt.addUsers_SystemAdministrator(uAdmin);
            bdt.addBuisness(b1);
            cat1.Category = c1;
            cat1.Location = l1;
            cat1.Buisness = b1;
            cat1.Status = TestUtils.APPROVED_STATUS;
            cat1.deadLineForUse = DateTime.Today;

            cManager = new CouponManager(new CS_DBEntities3());
        }


        [TestMethod()]
        public void createCatalogCouponTest()
        {
            double originalPrice = 100;
            double newPrice = 50.5;
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons.ToList().Count, 0);
            Assert.IsTrue(cManager.createCatalogCoupon(originalPrice, newPrice, b1.buisName, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString()));
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons.ToList().Count, 1);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void createSocialCouponTest()
        {
            double originalPrice = 100;
            double newPrice = 50.5;
            string webSit = "www.pwpwp.com";
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons_SocialNetworkCoupon
                .ToList().Count, 0);
            Assert.IsTrue(cManager.createSocialCoupon(originalPrice, newPrice, b1.buisName, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString(), webSit, ub1.userName));
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons_SocialNetworkCoupon
               .ToList().Count, 1);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateSocialCouponTest()
        {
            double originalPrice = 100;
            double newPrice = 50.5;
            string webSit = "www.pwpwp.com";
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons_SocialNetworkCoupon
                .ToList().Count, 0);
            Assert.IsTrue(cManager.createSocialCoupon(originalPrice, newPrice, b1.buisName, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString(), webSit, ub1.userName));
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons_SocialNetworkCoupon
               .ToList().Count, 1);
            originalPrice = 150;
            int catId = DataAccess.getDataAccess().CatalogCoupons_SocialNetworkCoupon.First().CatalogID;
            Assert.IsTrue(cManager.updateSocialCoupon(originalPrice, newPrice, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString(), webSit, catId));
            Assert.AreEqual(originalPrice, DataAccess.getDataAccess().
                CatalogCoupons_SocialNetworkCoupon.First().CatalogCoupon.originalPrice);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateCatalogCouponTest()
        {
            double originalPrice = 100;
            double newPrice = 50.5;
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons.ToList().Count, 0);
            Assert.IsTrue(cManager.createCatalogCoupon(originalPrice, newPrice, b1.buisName, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString()));
            Assert.AreEqual(DataAccess.getDataAccess().CatalogCoupons.ToList().Count, 1);
            originalPrice = 200;
            int catId =  DataAccess.getDataAccess().CatalogCoupons.First().catalogID;
            Assert.IsTrue(cManager.updateCatalogCoupon(originalPrice, newPrice, cat1.CouponName,
                cat1.description, DateTime.Today.AddDays(7).ToString(), catId));
            TestUtils.clearAllTable();
        }
    }
}
