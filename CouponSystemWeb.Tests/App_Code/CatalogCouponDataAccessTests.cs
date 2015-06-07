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
    public class CatalogCouponDataAccessTests
    {

        CatalogCoupon cat1;
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Category c1;
        CatalogCouponDataAccess ca_da;
        Users_SystemAdministrator uAdmin;
        Users_BuisnessOwner ub1;
        Buisness b1;
        User user1;
        User user2;

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
            CS_DBEntities3 context = new CS_DBEntities3();
            ca_da = new CatalogCouponDataAccess(context);
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
            cat1.Category = c1;
            cat1.Location = l1;
            cat1.Buisness = b1;
            cat1.Status = TestUtils.APPROVED_STATUS;
            cat1.deadLineForUse = DateTime.Today;


            User user = new User();
            user.userName = "customer";
            Users_Customer userC = new Users_Customer();
            userC.User = user;
            userC.Location = l1;
            userC.Categories.Add(c1);

            order1 = new OrderedCoupon();
            order1.CatalogCoupon = cat1;
            order1.dateOfPurchase = DateTime.Now;
            order1.dateOfUse = DateTime.Now;
            order1.isUsed = true;
            order1.rank = 3;
            order1.serialKey = "hjcfekbnr37vv8263gb3hyg723";
            order1.Users_Customer = userC;

            order2 = new OrderedCoupon();
            order2.CatalogCoupon = cat1;
            order2.dateOfPurchase = DateTime.Now;
            order2.serialKey = "UYTfekbnr37TRE85653gb3hyg723";
            order2.Users_Customer = userC;
            order2.dateOfUse = DateTime.Now;
            order2.isUsed = true;

        }


        [TestMethod]
        public void addCatalogCoupon()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            //TestUtils.clearAllTable();
        }


        [TestMethod]
        public void updateCatalogCouponDescription()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            string newDescription = "Shusi and gril";
            ca_da.updateDescription(cat1, newDescription);
            CatalogCoupon afterUpdate = ca_da.findCatalogCoupon(cat1);
            Assert.AreEqual(newDescription, afterUpdate.description);
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void updateCatalogCouponPriceAfterDiscount()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            double newValue = 5.54;
            ca_da.updatePriceAfterDiscount(cat1, newValue);
            CatalogCoupon afterUpdate = ca_da.findCatalogCoupon(cat1);
            Assert.AreEqual(newValue, afterUpdate.priceAfterDiscount);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updatAverageRankTest()
        {
            OrderedCouponDataAccess orderd_da = new OrderedCouponDataAccess(new CS_DBEntities3());
            orderd_da.addOrderedCoupons(order1);
            orderd_da.addOrderedCoupons(order2);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            Assert.AreEqual(cat1.averageRank, null);
            ca_da.updatAverageRank(cat1);
            Assert.IsTrue(cat1.averageRank == order1.rank);
            orderd_da.updateOrderedCouponRank(order2, 4);
            //ca_da.updatAverageRank(cat1);
            Assert.IsTrue(cat1.averageRank == (((double)order1.rank + (double)order2.rank) / 2));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void findCatalogCouponTest()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            CatalogCoupon found = ca_da.findCatalogCoupon(cat1);
            Assert.IsTrue(found.catalogID == cat1.catalogID);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void removeCatalogCouponTest()
        {
            OrderedCouponDataAccess orderd_da = new OrderedCouponDataAccess(new CS_DBEntities3());
            orderd_da.addOrderedCoupons(order1);
            orderd_da.addOrderedCoupons(order2);
            Assert.IsTrue(orderd_da.existsOrderedCoupons(order1));
            Assert.IsTrue(orderd_da.existsOrderedCoupons(order2));
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            ca_da.removeCatalogCoupon(cat1.catalogID);
            Assert.IsFalse(ca_da.existsCatalogCoupon(cat1));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllCouponsTest()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            List<CatalogCoupon> list = ca_da.getAllCoupons();
            Assert.IsTrue(list.Count == 1);
            Assert.IsTrue(list[0].catalogID == cat1.catalogID);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void approveCatalogCouponTest()
        {
            cat1.Status = TestUtils.WAIT_STATUS;
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            ca_da.approveCatalogCoupon(cat1.catalogID);
            CatalogCoupon after = ca_da.findCatalogCoupon(cat1);
            Assert.AreEqual(TestUtils.APPROVED_STATUS, after.Status);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateCatalogCouponTest()
        {
            ca_da.addCatalogCoupon(cat1);
            Assert.IsTrue(ca_da.existsCatalogCoupon(cat1));
            double newOriginPrice = 100.5;
            double newAfter = 50.5;
            ca_da.updateCatalogCoupon(cat1, newOriginPrice, newAfter, cat1.CouponName, cat1.description, cat1.deadLineForUse.ToString());
            CatalogCoupon after = ca_da.findCatalogCoupon(cat1);
            Assert.AreEqual(after.priceAfterDiscount, newAfter);
            Assert.AreEqual(after.originalPrice, newOriginPrice);
            TestUtils.clearAllTable();
        }
    }
}
