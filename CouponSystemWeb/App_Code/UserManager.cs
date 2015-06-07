using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public class UserManager
    {
        Users_BuisnessOwnerDataAccess buisData;
        Users_CustomerDataAccess custonerData;
        Users_SystemAdministratorDataAccess adminData;
        UserDataAccesss userData;
        CategoryDataAccess categoryData;


        public UserManager()
        {
            buisData = new Users_BuisnessOwnerDataAccess();
            custonerData = new Users_CustomerDataAccess();
            adminData = new Users_SystemAdministratorDataAccess();
            categoryData = new CategoryDataAccess();
            userData = new UserDataAccesss();
        }

        public UserManager(CS_DBEntities3 cS_DBEntities3)
        {
            buisData = new Users_BuisnessOwnerDataAccess(cS_DBEntities3);
            custonerData = new Users_CustomerDataAccess(cS_DBEntities3);
            adminData = new Users_SystemAdministratorDataAccess(cS_DBEntities3);
            categoryData = new CategoryDataAccess(cS_DBEntities3);
            userData = new UserDataAccesss(cS_DBEntities3);   
        }

        public UserType findUser(string userName, string password)
        {
            if (buisData.existsUsers_BuisnessOwnerByUsername(userName, password))
            {
                return UserType.BuisnessOwner;
            }
            else if (custonerData.existsUsers_CustomerByUsername(userName, password))
            {
                return UserType.Customer;
            }
            else if (adminData.existsUsers_adminByUsername(userName, password))
            {
                return UserType.Admin;
            }
            return UserType.NONE;
            
        }

        public User findUser(string userName)
        {
            return userData.getUser(userName);
        }

        public Users_Customer findCustomerByName(string userName)
        {
            return custonerData.findByName(userName);

        }

        public bool createBuisnessOwner(string userName, string pass, string email, string name, string phone)
        {
            if (checkIfUSerExist(userName))
                return false;
            Users_BuisnessOwner toAdd = new Users_BuisnessOwner();
            User tmpUser = new User();
            tmpUser.userName = userName;
            tmpUser.password = pass;
            tmpUser.email = email;
            tmpUser.fullName = name;
            tmpUser.phone = phone;
            toAdd.User = tmpUser;
            try
            {
                buisData.addUsers_BuisnessOwner(toAdd);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool createCustomer(string userName, string pass, string email, string name, string phone, string gender, short age, List<string> categories)
        {
            if (checkIfUSerExist(userName))
                return false;
            Users_Customer toAdd = new Users_Customer();
            User tmpUser = new User();
            tmpUser.userName = userName;
            tmpUser.password = pass;
            tmpUser.email = email;
            tmpUser.fullName = name;
            tmpUser.phone = phone;
            toAdd.User = tmpUser;
            toAdd.Location = getLocation();
            if (age!=0)
                toAdd.age = age;
            toAdd.gender = gender;
            List<Category> catList = getCategories(categories);
            toAdd.Categories = catList;
            try
            {
                custonerData.addUsers_Customer(toAdd);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private List<Category> getCategories(List<string> list)
        {
            List<Category> result = new List<Category>();
            foreach (var item in list)
            {
                result.Add(categoryData.FindCategory(item));
            }
            return result;
        } 

        public Location getLocation()
        {
            Location result = new Location();
            Random r = new Random();
            result.longitude = r.NextDouble() * 100;
            result.latitude = r.NextDouble() * 100;
            return result;
        }

        public List<string> getAllCategories()
        {
            List<string> categories = categoryData.getAllCategories();
            return categories;
        }
        public List<string> getCustomerCategories(Users_Customer customer)
        {
            List<string> categories = categoryData.getCustomerCategory(customer);
            return categories;
        }

        public void addCoupon(Users_Customer loggedUser,CatalogCoupon coupon)
        {
            custonerData.addCoupon(loggedUser, coupon);
        }

        public Users_BuisnessOwner findBuisnessOwnerByName(string userName)
        {
            return buisData.findByName(userName);
        }

        public Users_SystemAdministrator findAdminByName(string userName)
        {
            return adminData.findByName(userName);
        }
        public Users_SystemAdministrator getSystemAdminstrator()
        {
            return adminData.getSystemAdministrator();
        }

        /// <summary>
        /// gets all the businesses that belings to the owner with
        /// the user Name "userName"
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<string> getBusinessForOwner(string userName)
        {
            Users_BuisnessOwner buisUser = buisData.findByName(userName);
            ICollection<Buisness> list = buisUser.Buisnesses;
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                result.Add(item.buisName);
            }
            return result;
        }

        public List<string> getApprovedBusinessForOwner(string userName)
        {
            Users_BuisnessOwner buisUser = buisData.findByName(userName);
            ICollection<Buisness> list = buisUser.Buisnesses;
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                if (item.Status == "Approved")
                    result.Add(item.buisName);
            }
            return result;
        }



        public void updateCustomer(string userName,string password, string email, string fullName, string phone, string gender, short age, List<string>stringCategories)
        {
            custonerData.updateCustomer(userName, password, email, fullName, phone, gender, age, stringCategories);
        }

        public void updateBusinessOwner(string userName, string password, string email, string fullName, string phone)
        {
            buisData.updateBusinessOwner(userName, password, email, fullName, phone);
        }

        public List<CatalogCoupon> getAllAwaitingCoupons()
        {
            return adminData.getAllAwaitingCoupons();
        }

        public List<Buisness> getAllAwaitingBusiness()
        {
            return adminData.getAllAwaitingBusiness();
        }


        public bool addAdmin(string adminName, string password, string email, string fullName, string phone)
        {
            if (checkIfUSerExist(adminName))
                return false;
            User tmpUser = new User();
            Users_SystemAdministrator toAdd = new Users_SystemAdministrator();
            tmpUser.userName = adminName;
            tmpUser.password = password;
            tmpUser.email = email;
            tmpUser.fullName = fullName;
            tmpUser.phone = phone;
            toAdd.User = tmpUser;
            try
            {
                adminData.addUsers_SystemAdministrator(toAdd);
            }
            catch
            {
                return false;
            }
            return true;
            
        }

        public bool checkIfUSerExist(string userName)
        {
            return (userData.getUser(userName) != null);
        }
    }
}