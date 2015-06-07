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
    public class OrderedCouponDataAccessTests
    {

        OrderedCouponDataAccess or_da;
        CatalogCoupon cat1;
        OrderedCoupon ordCoup;
        Location l1;
        Category c1;
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


            User user = new User();
            user.userName = "customer";
            Users_Customer userC = new Users_Customer();
            userC.User = user;
            userC.Location = l1;
            userC.Categories.Add(c1);

            ordCoup = new OrderedCoupon();
            ordCoup.CatalogCoupon = cat1;
            ordCoup.dateOfPurchase = DateTime.Now;
            ordCoup.dateOfUse = DateTime.Now;
            ordCoup.isUsed = true;
            ordCoup.serialKey = "hjcfekbnr37vv8263gb3hyg723";
            ordCoup.Users_Customer = userC;

            or_da = new OrderedCouponDataAccess(new CS_DBEntities3());

        }

        [TestMethod]
        public void addOrderedCoupon()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void removeOrderedCoupon()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            or_da.removeOrderedCoupons(ordCoup);
            Assert.IsFalse(or_da.existsOrderedCoupons(ordCoup));
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateOrderedCouponRank()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            short newValue = 5;
            or_da.updateOrderedCouponRank(ordCoup, newValue);
            Assert.AreEqual(ordCoup.rank, newValue);
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void updateOrderedCouponsDeadLineForUse()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            DateTime newValue = new DateTime(2016, 1, 1);
            or_da.updateOrderedCouponsDeadLineForUse(ordCoup, newValue);
            Assert.AreEqual(ordCoup.CatalogCoupon.deadLineForUse, newValue);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updatOrderedCouponsPriceAfterDiscount()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            int newValue = 5;
            or_da.updatOrderedCouponsPriceAfterDiscount(ordCoup, newValue);
            Assert.AreEqual(ordCoup.CatalogCoupon.priceAfterDiscount, newValue);
            TestUtils.clearAllTable();

        }


        [TestMethod]
        public void updateOrderedisUsed()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            bool newValue = true;
            or_da.updateOrderedisUsed(ordCoup, newValue);
            Assert.AreEqual(ordCoup.isUsed, newValue);
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void updateOrderedCouponsDateOfPurchase()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            DateTime newValue = new DateTime(2014, 4, 1);
            or_da.updateOrderedCouponsDateOfPurchase(ordCoup, newValue);
            Assert.AreEqual(ordCoup.dateOfPurchase, newValue);
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void updateOrderedCouponsDateOfUses()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            DateTime newValue = new DateTime(2016, 4, 9);
            or_da.updateOrderedCouponsDateOfUse(ordCoup, newValue);
            Assert.AreEqual(ordCoup.dateOfUse, newValue);
            TestUtils.clearAllTable();
        }


        [TestMethod()]
        public void updateOrderedCouponRankTest()
        {
            or_da.addOrderedCoupons(ordCoup);
            Assert.IsTrue(or_da.existsOrderedCoupons(ordCoup));
            Assert.IsTrue(ordCoup.rank == null);
            short rank = 4;
            or_da.updateOrderedCouponRank(ordCoup, rank);
            OrderedCoupon after = or_da.findOrderedCoupon(ordCoup.serialKey);
            Assert.AreEqual(after.rank, rank);
            Assert.AreEqual(after.CatalogCoupon.averageRank, (double)rank);
            TestUtils.clearAllTable();
        }
    }
}
