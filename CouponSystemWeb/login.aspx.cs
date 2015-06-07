using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CouponSystemWeb.App_Code;

namespace CouponSystemWeb
{
    public partial class login : System.Web.UI.Page
    {
        SystemController system;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Users_SystemAdministratorDataAccess dt = new Users_SystemAdministratorDataAccess();
            //User a = new User();
            //a.userName = "admin";
            //a.password = "1";
            //Users_SystemAdministrator b = new Users_SystemAdministrator();
            //b.User = a;
            //dt.addUsers_SystemAdministrator(b);
            //string check = dt.getSystemAdministrator().userName;
            system = new SystemController();
            //selectType.Items.Add(new ListItem("Customer"));
           // selectType.Items.Add(new ListItem("Business Owner"));
            List<string> categories = system.getAllCategories();
            if (!IsPostBack)
            {
                foreach (var item in categories)
                {
                    categoriesLst.Items.Add(new ListItem(item));
                }
                
            }

        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            bool result = false;
            if ((UserNamesignup.Value != "") && (passwordsignup.Value != "") && (ConfirmPassword.Value!= ""))
            {
                if (selectType.Items[selectType.SelectedIndex].Text.Equals("Business Owner"))
                {
                    result = system.createBuisnessOwner(UserNamesignup.Value, passwordsignup.Value,
                       emailsignup.Value, fullNamesignup.Value, phonsignup.Value);
                }
                else
                {
                    List<string> catList = new List<string>();
                    foreach (ListItem item in categoriesLst.Items)
                    {
                        if (item.Selected == true)
                        {
                            catList.Add(item.Text);
                        }
                    }
                    short age = 0;
                    if (ageInput.Value != "")
                        age = short.Parse(ageInput.Value);
                    string gender = "F";
                    if (selectGender.Items[selectGender.SelectedIndex].Text.Equals("Male"))
                        gender = "M";
                    result = system.createCustomer(UserNamesignup.Value, passwordsignup.Value,
                       emailsignup.Value, fullNamesignup.Value, phonsignup.Value, gender, age, catList);
                }
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                    "alert('User was successfully added'); window.location='" +
                    Request.ApplicationPath + "login.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('UserName is already used')", true);
                }
            }
        }



        //protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        //{
        //    string userName = Login1.UserName;
        //    string pass = Login1.Password;
        //    SystemController system = new SystemController();
        //    UserType type = system.findUser(userName, pass);
        //    switch (type)
        //    {
        //        case(UserType.Admin):
        //            Response.Redirect("astart.aspx?userName=" + userName);
        //            break;
        //        case(UserType.BuisnessOwner):
        //            Response.Redirect("bstart.aspx?userName=" + userName);
        //            break;
        //        case(UserType.Customer):
        //            Response.Redirect("cstart.aspx?userName=" + userName);
        //            break;
        //        default:
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User was not found!')", true);
        //            break;
        //    }
        //}

        protected void LoginButton_ServerClick(object sender, EventArgs e)
        {
            string userName = username.Value;
            string pass = password.Value;
            SystemController system = new SystemController();
            UserType type = system.findUser(userName, pass);
            switch (type)
            {
                case (UserType.Admin):
                    Response.Redirect("astart.aspx?userName=" + userName);
                    break;
                case (UserType.BuisnessOwner):
                    Response.Redirect("bstart.aspx?userName=" + userName);
                    break;
                case (UserType.Customer):
                    Response.Redirect("cstart.aspx?userName=" + userName);
                    break;
                default:
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User was not found!')", true);
                    break;
            }
        }
    }
}