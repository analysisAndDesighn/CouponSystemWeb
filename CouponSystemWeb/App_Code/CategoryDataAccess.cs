using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponSystemWeb.App_Code
{
    public class CategoryDataAccess
    {
        private CS_DBEntities3 db;

        public CategoryDataAccess()
        {
            db = DataAccess.getDataAccess();
        }

        public CategoryDataAccess(CS_DBEntities3 con)
        {
            db = con;
        }

        public bool addCategory(string c)
        {
            Category newCat = new Category();
            newCat.catName = c;
            db.Categories.Add(newCat);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void addCategory(Category c)
        {
            db.Categories.Add(c);
            db.SaveChanges();
        }

        public Category FindCategory(string c)
        {
            Category res = db.Categories.Find(c);
            return res;
        }

        /// <summary>
        /// get all categories in the system
        /// </summary>
        /// <returns></returns>
        public List<string> getAllCategories()
        {
            List<Category> catList = db.Categories.ToList();
            List<string> result = new List<string>();
            foreach (var item in catList)
            {
                result.Add(item.catName);
            }
            return result;
        }


        /// <summary>
        /// get all the categories of a spesific customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public List<string> getCustomerCategory(Users_Customer customer)
        {
            List<Category> catList = customer.Categories.ToList();
            List<string> result = new List<string>();
            foreach (var item in catList)
            {
                result.Add(item.catName);
            }
            return result;
        }

    }
}