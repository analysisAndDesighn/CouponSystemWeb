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
    public class BusinessDataAccessTest
    {
        
        Buisness b1;
        Buisness b2;
        Users_BuisnessOwner ub;
        User user1;
        Category c1;
        BuisnessDataAccess buisness_da;
        Location l;
        CS_DBEntities3 context;


        [TestInitialize]
        public void init()
        {
            TestUtils.clearAllTable();
            user1 = new User();
            c1 = new Category();
            b1 = new Buisness();
            b2 = new Buisness();
            l = new Location();
            l.latitude = 34.8999;
            l.longitude = 45.3666;
            
            b1.BuisDescription = "Sushi bar";
            b1.buisAddress = "Aharom Meskin 13";
            b1.buisCity = "Beer-Sheva";
            b1.buisName = "JAPANIKA";
            user1.userName = "userNamebusiness";
            c1.catName = "food";
            ub = new Users_BuisnessOwner();
            ub.User = user1;
            
            
            
            b1.Users_BuisnessOwner = ub;
            b1.Category = c1;
            b1.Location = l;
            b1.Status = TestUtils.WAIT_STATUS;
           

            Location l2 = new Location();
            l2.latitude = 44.8999;
            l2.longitude = 35.3666;
            b2.Users_BuisnessOwner = ub;
            b2.Category = c1;
            b2.Location = l2;
            b2.BuisDescription = "Sushi bar";
            b2.buisAddress = "rotshild 13";
            b2.buisCity = "Tel Aviv";
            b2.buisName = "JAPANIKA TA";
            b2.Status = TestUtils.WAIT_STATUS;
            context = new CS_DBEntities3();
            buisness_da = new BuisnessDataAccess(context);

        }

        [TestMethod()]
        public void addBuisnessTest()
        {
            Users_BuisnessOwnerDataAccess userda = new Users_BuisnessOwnerDataAccess(context);
            userda.addUsers_BuisnessOwner(ub);
            CategoryDataAccess catdt = new CategoryDataAccess(context);
            catdt.addCategory(c1);
            string userName = ub.userName;
            string busiName = "Arabika";
            string descriptuin = "Restorant";
            string address = "Alenby 8";
            string city = "Beer Sheva";
            Assert.IsFalse(buisness_da.existsBuisness(busiName));
            Assert.IsTrue(buisness_da.addBuisness(userName, busiName, address, city, descriptuin, c1.catName, l));
            Assert.IsTrue(buisness_da.existsBuisness(busiName));
            Assert.IsFalse(buisness_da.addBuisness(userName, busiName, address, city, descriptuin, c1.catName, l));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void removeBuisnessByNameTest()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            buisness_da.removeBuisnessByName(b1.buisName);
            Assert.IsFalse(buisness_da.existsBuisness(b1));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void approveBusinessCouponTest()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            buisness_da.approveBusinessCoupon(b1.buisName);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(TestUtils.APPROVED_STATUS, afterUpdate.Status);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void updateBusinessTest()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            string address = "Alenby 8";
            buisness_da.updateBusiness(b1.buisName, address, b1.buisCity, b1.BuisDescription, c1.catName);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(address, afterUpdate.buisAddress);
            TestUtils.clearAllTable();
        }


        [TestMethod]
        public void removeBuisness()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            buisness_da.removeBuisness(b1);
            Assert.IsFalse(buisness_da.existsBuisness(b1));
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateBuisnessAddress()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            string newAddress = "Ehad Haam 12";
            buisness_da.updateAddress(b1, newAddress);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(newAddress, afterUpdate.buisAddress);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllApprovedBuisnessesTest()
        {
            b1.Status = TestUtils.APPROVED_STATUS;
            b2.Status = TestUtils.WAIT_STATUS;
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            buisness_da.addBuisness(b2);
            Assert.IsTrue(buisness_da.existsBuisness(b2));

            List<string> result = buisness_da.getAllApprovedBuisnesses();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].Equals(b1.buisName));
            buisness_da.approveBusinessCoupon(b2.buisName);
            result = buisness_da.getAllApprovedBuisnesses();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(b1.buisName));
            Assert.IsTrue(result.Contains(b2.buisName));
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllApprovedTest()
        {
            b1.Status = TestUtils.APPROVED_STATUS;
            b2.Status = TestUtils.WAIT_STATUS;
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            buisness_da.addBuisness(b2);
            Assert.IsTrue(buisness_da.existsBuisness(b2));

            List<Buisness> result = buisness_da.getAllApproved();
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].buisName.Equals(b1.buisName));
            buisness_da.approveBusinessCoupon(b2.buisName);
            result = buisness_da.getAllApproved();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(b1));
            Assert.IsTrue(result.Contains(b2));
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateBuisnessCity()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            string newCity = "Jerusalem";
            buisness_da.updateCity(b1, newCity);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(newCity, afterUpdate.buisCity);
            TestUtils.clearAllTable();
        }

        public void updateBuisnessDescription()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            string newDescription = "Shusi and gril";
            buisness_da.updateDescription(b1, newDescription);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(newDescription, afterUpdate.BuisDescription);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateBuisnessCategory()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            Category newCategory = new Category();
            newCategory.catName = "sport";
            buisness_da.updateCategory(b1, newCategory);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(newCategory.catName, afterUpdate.Category.catName);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateBuisnessLocation()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            Location newLoc = new Location();
            newLoc.latitude = 34.5;
            newLoc.longitude = 31.8;
            buisness_da.updateLocation(b1, newLoc);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(newLoc.longitude, afterUpdate.Location.longitude);
            Assert.AreEqual(newLoc.latitude, afterUpdate.Location.latitude);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void updateBuisnessOwner()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            User u2 = new User();
            Users_BuisnessOwner owner = new Users_BuisnessOwner();
            u2.userName = "Moshe";
            owner.User = u2;
            buisness_da.updateOwner(b1, owner);
            Buisness afterUpdate = buisness_da.findBuisness(b1);
            Assert.AreEqual(owner.userName, afterUpdate.Users_BuisnessOwner.userName);
            TestUtils.clearAllTable();
        }

        [TestMethod]
        public void addBuisness()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            TestUtils.clearAllTable();
        }
    }
}
