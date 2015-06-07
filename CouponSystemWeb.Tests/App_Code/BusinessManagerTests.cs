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
    public class BusinessManagerTests
    {
        Buisness b1;
        Buisness b2;
        Users_BuisnessOwner ub;
        User user1;
        Category c1;
        BuisnessDataAccess buisness_da;
        BusinessManager bmanager;
        Location l;


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

            CS_DBEntities3 context = new CS_DBEntities3();
            buisness_da = new BuisnessDataAccess(context);
            bmanager = new BusinessManager(context);

        }

        [TestMethod()]
        public void removeBuisnessByNameTest()
        {
            buisness_da.addBuisness(b1);
            Assert.IsTrue(buisness_da.existsBuisness(b1));
            bmanager.removeBuisnessByName(b1.buisName);
            Assert.IsFalse(buisness_da.existsBuisness(b1));
        }
    }
}
