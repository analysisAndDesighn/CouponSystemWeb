using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CouponSystemWeb.App_Code;

namespace CouponSystemWeb
{
    public partial class Register : System.Web.UI.Page
    {
        SystemController system;
        protected void Page_Load(object sender, EventArgs e)
        {
            system = new SystemController();
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
            if ((UserName.Text != "") && (Password.Text != "") && (ConfirmPassword.Text!=""))
            {
                if (BuisOwnerRadio.Checked)
                {
                    result = system.createBuisnessOwner(UserName.Text, Password.Text, Email.Text, FullName.Text, TextBoxPhone.Text);
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
                    if (ageTextBox.Text != "")
                        age = short.Parse(ageTextBox.Text);
                    string gender = "F";
                    if (maleRadio.Checked)
                        gender = "M";
                    result = system.createCustomer(UserName.Text, Password.Text, Email.Text, FullName.Text, TextBoxPhone.Text, gender, age, catList);
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
        
    }
}