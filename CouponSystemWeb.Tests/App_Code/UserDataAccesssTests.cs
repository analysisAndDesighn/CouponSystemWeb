﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouponSystemWeb.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CouponSystemWeb.App_Code.Tests
{
    [TestClass()]
    public class UserDataAccesssTests
    {
        CatalogCoupon cat1;
        OrderedCoupon order1;
        OrderedCoupon order2;
        Location l1;
        Category c1;
        UserDataAccesss udt;
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
            udt = new UserDataAccesss(context);
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
            CatalogCouponDataAccess cdt = new CatalogCouponDataAccess(context);
            cdt.addCatalogCoupon(cat1);
        }

        [TestMethod()]
        public void getUserTest()
        {            
            Assert.IsTrue(udt.getUser(user1.userName).userName==user1.userName);
        }
    }
}
