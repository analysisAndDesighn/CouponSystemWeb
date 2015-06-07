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
    public class Users_BuisnessOwnerDataAccessTests
    {
        CatalogCoupon cat1;
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Category c1;
        Users_BuisnessOwnerDataAccess dt;
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
            dt = new Users_BuisnessOwnerDataAccess(context);
            
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
            cat1.CouponName = "free desert";
            cat1.Status = TestUtils.APPROVED_STATUS;
            cat1.description = "free desert if you buy a meal";
            cat1.originalPrice = 30;
            cat1.priceAfterDiscount = 0;
            cat1.deadLineForUse = DateTime.Today.AddDays(10);


            User user = new User();
            user.userName = "customer";
            user.password = "pass";
            Users_Customer userC = new Users_Customer();
            userC.User = user;
            userC.Location = l1;
            userC.Categories.Add(c1);

            order1 = new OrderedCoupon();
            order1.CatalogCoupon = cat1;
            order1.dateOfPurchase = DateTime.Now;
            //order1.dateOfUse = DateTime.Now;
            //order1.isUsed = true;
            //order1.rank = 3;
            order1.serialKey = "hjcfekbnr37vv8263gb3hyg723";
            order1.Users_Customer = userC;

            order2 = new OrderedCoupon();
            order2.CatalogCoupon = cat1;
            order2.dateOfPurchase = DateTime.Now;
            order2.serialKey = "UYTfekbnr37TRE85653gb3hyg723";
            order2.Users_Customer = userC;
            //order2.dateOfUse = DateTime.Now;
            //order2.isUsed = true;
        }


        [TestMethod()]
        public void existsUsers_BuisnessOwnerByUsernameTest()
        {
            dt.addUsers_BuisnessOwner(ub1);
            Assert.IsTrue(dt.existsUsers_BuisnessOwner(ub1));
            Assert.IsTrue(dt.existsUsers_BuisnessOwnerByUsername(ub1.userName,
                ub1.User.password));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateBusinessOwnerTest()
        {
            dt.addUsers_BuisnessOwner(ub1);
            Assert.IsTrue(dt.existsUsers_BuisnessOwner(ub1));
            string email = "email@email.com";
            string phone = "0521521452";
            string fullName = "business name";
            dt.updateBusinessOwner(ub1.userName, ub1.User.password, email, fullName, phone);
            Users_BuisnessOwner after = dt.findByName(ub1.userName);
            Assert.AreEqual(email, after.User.email);
            Assert.AreEqual(phone, after.User.phone);
            Assert.AreEqual(fullName, after.User.fullName);
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void findByNameTest()
        {
            dt.addUsers_BuisnessOwner(ub1);
            Assert.IsTrue(dt.existsUsers_BuisnessOwner(ub1));
            Users_BuisnessOwner after = dt.findByName(ub1.userName);
            Assert.AreEqual(after.userName, ub1.userName);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void removeUsers_BuisnessOwnerTest()
        {
            dt.addUsers_BuisnessOwner(ub1);
            Assert.IsTrue(dt.existsUsers_BuisnessOwner(ub1));
            dt.removeUsers_BuisnessOwner(ub1.userName);
            Assert.IsFalse(dt.existsUsers_BuisnessOwner(ub1));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void addUsers_BuisnessOwnerTest()
        {
            dt.addUsers_BuisnessOwner(ub1);
            Assert.IsTrue(dt.existsUsers_BuisnessOwner(ub1));
            TestUtils.clearAllTable();
        }
    }
}
