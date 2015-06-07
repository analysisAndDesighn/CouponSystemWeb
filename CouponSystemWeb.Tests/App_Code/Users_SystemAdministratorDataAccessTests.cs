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
    public class Users_SystemAdministratorDataAccessTests
    {
    

        CatalogCoupon cat1;
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Category c1;
        Users_SystemAdministratorDataAccess dt;
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
            uAdmin.CatalogCoupons.Add(cat1);
            //cat1.Users_SystemAdministrator = uAdmin;
            CS_DBEntities3 context = new CS_DBEntities3();
            dt = new Users_SystemAdministratorDataAccess(context);
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
        }

        [TestMethod()]
        public void addUsers_SystemAdministratorTest()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void existsUsers_adminByUsernameTest()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            Assert.IsTrue(dt.existsUsers_adminByUsername(uAdmin.userName, uAdmin.User.password));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllAwaitingBusinessTest()
        {
            b1.Status = TestUtils.WAIT_STATUS;
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            Assert.IsTrue(dt.getAllAwaitingBusiness().Count == 1);
            Assert.IsTrue(dt.getAllAwaitingBusiness().First().buisName.Equals(b1.buisName));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllAwaitingCouponsTest()
        {
            cat1.Status = TestUtils.WAIT_STATUS;
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            Assert.IsTrue(dt.getAllAwaitingCoupons().Count == 1);
            Assert.IsTrue(dt.getAllAwaitingCoupons().First().catalogID.Equals(cat1.catalogID));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void findByNameTest()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            Assert.AreEqual(dt.findByName(uAdmin.userName), uAdmin);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getSystemAdministratorTest()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            Assert.AreEqual(dt.getSystemAdministrator(), uAdmin);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateSystemAdministratorName()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            String nwName = "Dor";
            dt.updateName(uAdmin, nwName);
            Users_SystemAdministrator afterUpdate = dt.findByName(uAdmin.userName);
            Assert.AreEqual(nwName, uAdmin.User.fullName);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateSystemAdministratorPassword()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            String nwPassword = "321";
            dt.updatePassword(uAdmin, nwPassword);
            Users_SystemAdministrator afterUpdate = dt.findByName(uAdmin.userName);
            Assert.AreEqual(nwPassword, uAdmin.User.password);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateSystemAdministratorEmail()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            String nwEmail = "Email@Email.com";
            dt.updateEmail(uAdmin, nwEmail);
            Users_SystemAdministrator afterUpdate = dt.findByName(uAdmin.userName);
            Assert.AreEqual(nwEmail, uAdmin.User.email);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateSystemAdministratorPhone()
        {
            dt.addUsers_SystemAdministrator(uAdmin);
            Assert.IsTrue(dt.existsUsers_SystemAdministrator(uAdmin));
            String nwPhone = "0009987766";
            dt.updatePhone(uAdmin, nwPhone);
            Users_SystemAdministrator afterUpdate = dt.findByName(uAdmin.userName);
            Assert.AreEqual(nwPhone, uAdmin.User.phone);
            TestUtils.clearAllTable();
        }

    }
}
