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
    public class UserManagerTests
    {
        CatalogCoupon cat1;
        Location l1;
        Category c1;
        Users_SystemAdministrator uAdmin;
        Users_BuisnessOwner ub1;
        Buisness b1;
        User user1;
        User user2;
        UserManager uManager;

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

            uManager = new UserManager(new CS_DBEntities3());
        }
        [TestMethod()]
        public void findUserTest()
        {
            Assert.AreEqual(uManager.findUser(ub1.userName, ub1.User.password), UserType.BuisnessOwner);
            Assert.AreEqual(uManager.findUser(uAdmin.userName, uAdmin.User.password), UserType.Admin);
        }

        [TestMethod()]
        public void createBuisnessOwnerTest()
        {
            string userName = "user_business";
            string pass = "pass";
            string phone = "0534153436";
            string email = "hj@fdcg.com";
            string fullName = "full name";
            Assert.IsTrue(uManager.createBuisnessOwner(userName, pass, email, fullName, phone));
            Assert.AreEqual(userName, uManager.findUser(userName).userName);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void createCustomerTest()
        {
            string userName = "user_business";
            string pass = "pass";
            string phone = "0534153436";
            string email = "hj@fdcg.com";
            string fullName = "full name";
            List<string> categories = new List<string>();
            categories.Add("food");
            Assert.IsTrue(uManager.createCustomer(userName, pass, email, fullName, phone,
                "F", 32, categories));
            Assert.AreEqual(userName, uManager.findUser(userName).userName);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getBusinessForOwnerTest()
        {
            List<string> list = uManager.getBusinessForOwner(ub1.userName);
            Assert.IsTrue(list.Count == 1);
            Assert.AreEqual(list[0], b1.buisName);
        }

        [TestMethod()]
        public void getApprovedBusinessForOwnerTest()
        {
            List<string> list = uManager.getApprovedBusinessForOwner(ub1.userName);
            Assert.IsTrue(list.Count == 1);
            Assert.AreEqual(list[0], b1.buisName);
        }

        [TestMethod()]
        public void addAdminTest()
        {
            string userName = "userAdmin";
            string pass = "pass";
            string phone = "0534153436";
            string email = "hj@fdcg.com";
            string fullName = "full name";
            Assert.IsTrue(uManager.addAdmin(userName, pass, email, fullName, phone));
            Assert.AreEqual(userName, uManager.findUser(userName).userName);
            TestUtils.clearAllTable();
        }
    }
}
