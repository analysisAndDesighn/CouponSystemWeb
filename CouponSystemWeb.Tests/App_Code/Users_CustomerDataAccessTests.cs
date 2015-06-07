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
    public class Users_CustomerDataAccessTests
    {
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Users_CustomerDataAccess dt;
        Users_SystemAdministrator uAdmin;
        Buisness b1;
        User user1;
        Category c1;
        Users_Customer userC;
        CatalogCoupon cat1;

        [TestInitialize]
        public void init()
        {
            TestUtils.clearAllTable();
            uAdmin = new Users_SystemAdministrator();
            User user2 = new User();
            user2.userName = "admin";
            user2.password = "admin";
            uAdmin.User = user2;
            cat1 = new CatalogCoupon();
            cat1.Users_SystemAdministrator = uAdmin;
            CS_DBEntities3 context = new CS_DBEntities3();
            dt = new Users_CustomerDataAccess(context);
            cat1.CouponName = "free desert";
            user1 = new User();
            c1 = new Category();
            b1 = new Buisness();
            l1 = new Location();
            l1.latitude = 34.8999;
            l1.longitude = 45.3666;
            Users_BuisnessOwner ub1 = new Users_BuisnessOwner();
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
            user.password = "pass";
            userC = new Users_Customer();
            userC.User = user;
            userC.Location = l1;
            userC.Categories.Add(c1);

            //order1 = new OrderedCoupon();
            //order1.CatalogCoupon = cat1;
            //order1.dateOfPurchase = DateTime.Now;
            //order1.dateOfUse = DateTime.Now;
            //order1.isUsed = true;
            //order1.rank = 3;
            //order1.serialKey = "hjcfekbnr37vv8263gb3hyg723";
            //order1.Users_Customer = userC;
        }


        [TestMethod()]
        public void addUsers_CustomerTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void findByNameTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            Assert.IsTrue(dt.findByName(userC.userName).userName.Equals(userC.userName));
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void removeUsers_CustomerTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            dt.removeUsers_Customer(userC);
            Assert.IsFalse(dt.existsUsers_Customer(userC));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateCustomerTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            string phone = "05641564165";
            string email = "userc@email.com";
            string fullName = "customer user";
            short age = 56;
            List<string> categories = new List<string>();
            categories.Add(c1.catName);
            dt.updateCustomer(userC.userName, userC.User.password,
                email, fullName, phone, "M", age, categories);
            Users_Customer after = dt.findUsers_Customer(userC);
            Assert.AreEqual(after.age, age);
            Assert.AreEqual(after.gender, "M");
            Assert.AreEqual(after.User.fullName, fullName);
            Assert.AreEqual(after.User.phone, phone);
            Assert.AreEqual(after.User.email, email);
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void existsUsers_CustomerByUsernameTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            Assert.IsTrue(dt.existsUsers_CustomerByUsername(userC.userName, userC.User.password));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void removeCategoryTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            Assert.IsTrue(userC.Categories.Contains(c1));
            dt.removeCategory(userC, c1);
            Assert.IsFalse(userC.Categories.Contains(c1));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void addCouponTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            Assert.IsTrue(userC.OrderedCoupons.Count == 0);
            dt.addCoupon(userC, cat1);
            Assert.IsTrue(userC.OrderedCoupons.Count == 1);
            Assert.AreEqual(userC.OrderedCoupons.First().CatalogCoupon.catalogID, cat1.catalogID);
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void updateLocationTest()
        {
            dt.addUsers_Customer(userC);
            Assert.IsTrue(dt.existsUsers_Customer(userC));
            Location newLoc = new Location();
            newLoc.latitude = 34.5;
            newLoc.longitude = 31.8;
            dt.updateLocation(userC, newLoc);
            Users_Customer afterUpdate = dt.findUsers_Customer(userC);
            Assert.AreEqual(newLoc.longitude, afterUpdate.Location.longitude);
            Assert.AreEqual(newLoc.latitude, afterUpdate.Location.latitude);
            TestUtils.clearAllTable();
        }

    }
}
