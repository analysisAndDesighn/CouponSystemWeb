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
    public class CategoryDataAccessTests
    {
        Category cat1;
        Category cat2;
        Category cat3;
        CategoryDataAccess cat_da;

        [TestInitialize]
        public void TestInitCategory()
        {
            //making sure the table is empty
            TestUtils.clearAllTable();

            cat1 = new Category();
            cat2 = new Category();
            cat3 = new Category();
            cat1.catName = "food";
            cat2.catName = "sport";
            cat3.catName = "Entertaiment";
            cat_da = new CategoryDataAccess(new CS_DBEntities3());
        }


        [TestMethod]
        public void addCategory()
        {
            cat_da.addCategory(cat1);
            Assert.IsTrue(cat_da.FindCategory(cat1.catName)!= null);
            TestUtils.clearAllTable();

        }
        
        [TestMethod()]
        public void addCategoryTest()
        {
            cat_da.addCategory(cat1.catName);
            Assert.IsTrue(cat_da.FindCategory(cat1.catName) != null);
            TestUtils.clearAllTable();
        }

        [TestMethod()]
        public void getAllCategoriesTest()
        {
            cat_da.addCategory(cat1.catName);
            Assert.IsTrue(cat_da.FindCategory(cat1.catName) != null);
            cat_da.addCategory(cat2.catName);
            Assert.IsTrue(cat_da.FindCategory(cat2.catName) != null);
            cat_da.addCategory(cat3.catName);
            Assert.IsTrue(cat_da.FindCategory(cat3.catName) != null);
            List<string> list = cat_da.getAllCategories();
            Assert.IsTrue(list.Count == 3);
            Assert.IsTrue(list.Contains(cat1.catName));
            Assert.IsTrue(list.Contains(cat2.catName));
            Assert.IsTrue(list.Contains(cat3.catName));
            TestUtils.clearAllTable();

        }

        [TestMethod()]
        public void getCustomerCategoryTest()
        {
            User user = new User();
            user.userName = "userC";
            user.fullName = "name user";
            Users_Customer customer = new Users_Customer();
            customer.User = user;
            customer.Categories.Add(cat1);
            customer.Categories.Add(cat2);
            Location l = new Location();
            l.latitude = 36.9;
            l.longitude = 40.8;
            customer.Location = l;
            Users_CustomerDataAccess user_da = new Users_CustomerDataAccess(new CS_DBEntities3());
            user_da.addUsers_Customer(customer);
            Assert.IsTrue(user_da.existsUsers_Customer(customer));
            cat_da = new CategoryDataAccess(new CS_DBEntities3());
            List<string> list = cat_da.getCustomerCategory(customer);
            Assert.IsTrue(list.Count == 2);
            Assert.IsTrue(list.Contains(cat1.catName));
            Assert.IsTrue(list.Contains(cat2.catName));
            TestUtils.clearAllTable();
        }
    }
}
