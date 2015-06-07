using CouponSystemWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CouponSystemWeb
{
    public partial class bstart : System.Web.UI.Page
    {
        Users_BuisnessOwner loggedUser;
        SystemController system;

        protected void Page_Load(object sender, EventArgs e)
        {
            system = new SystemController();
            string userName = Request.QueryString["userName"];
            loggedUser = system.findBuisnessOwnerByName(userName);
            List<string> allCategory=system.getAllCategories();
            if (!IsPostBack)
            {
                changeDiv(myBusinessDiv, myBusinessSection);
                createBusinessTable(loggedUser.Buisnesses);
                foreach (string cat in allCategory)
                {
                    CategorySelect.Items.Add(cat);
                }

                updateBusinessDropDownList(userName);

            }
            if (IsPostBack)
            {
                system = new SystemController();
                string loggeduserName = Request.QueryString["userName"];
                if (MyCouponsDiv.Visible)
                {
                    if (catalogCouponsDiv.Visible)
                    {
                        createCatalogouponsTable(system.getAllCouponsBusinessOwner(loggedUser.userName));
                    }
                    else
                    {
                        createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);
                    }
                }
                if (myBusinessDiv.Visible)
                {
                    createBusinessTable(system.findBuisnessOwnerByName(loggeduserName).Buisnesses);
                }

            }

        }

        public void updateBusinessDropDownList(string loggeduserName)
        {
            selectBusiness.Items.Clear();
            List<string> list = system.getBusinessForOwner(loggeduserName);
            selectBusiness.Items.Add(new ListItem("All"));
            foreach (string item in list)
            {
                selectBusiness.Items.Add(new ListItem(item));
            }
        }

        protected void myBusiness_onClick(object sender, EventArgs e)
        {
            changeDiv(myBusinessDiv, myBusinessSection);
            createBusinessTable(loggedUser.Buisnesses);

        }

        protected void activateCoupons_onClick(object sender, EventArgs e)
        {
            serialBox.Text = "";
            changeDiv(ActivateCouponsDiv, ActivateCouponsSection);
        }
        public void createBusinessTable(ICollection<Buisness> buisnessList)
        {
            if (buisnessList != null)
            {
                myBusinessTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateBusiness(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                myBusinessTable.Controls.Add(hederRow);

                int i = 1;
                foreach (Buisness item in buisnessList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = { item.buisName,
                                            item.buisAddress,
                                           item.buisCity,
                                           item.BuisDescription,
                                           item.Category.catName,
                                           item.Status};

                    foreach (var info in information)
                    {
                        TableCell cellNew = new TableCell();
                        Label lblNew = new Label();
                        if (info != null)
                        {
                            lblNew.Text = info.ToString();
                        }
                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);

                    }

                    TableCell cellBtn = new TableCell();
                    Button btn = new Button();
                    btn.ID = "update_"+ item.buisName;
                    btn.Text = "Update";
                    btn.Click += new EventHandler(this.updateCurrentBusiness);
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);


                    cellBtn = new TableCell();
                    btn = new Button();
                    btn.ID = item.buisName;
                    btn.Text = "Remove";
                    btn.Click += new EventHandler(this.removeCurrentBusiness);
                    btn.Attributes.Add("onclick",
                        "ConfirmRemove('" + item.buisName + "');");
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);

                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    myBusinessTable.Controls.Add(rowNew);
                }
            }

        }

        private void removeCurrentBusiness(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_remove_value"];
            if (confirmValue == "Yes")
            {
                Button btn = (Button)sender;
                string buisName = btn.ID;
                system.removeBuisnessByName(buisName);
            }
            createBusinessTable(system.findBuisnessOwnerByName(loggedUser.userName).Buisnesses);
            updateBusinessDropDownList(loggedUser.userName);
        }

        private void updateCurrentBusiness(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Buisness toUpdate = system.findBuisnessByName(btn.ID.Replace("update_", ""));
            changeDiv(updateBusinessDiv, myBusinessSection);
            BusiName.Text = toUpdate.buisName;
            Adress1.Text = toUpdate.buisAddress;
            City1.Text = toUpdate.buisCity;
            Description1.Text = toUpdate.BuisDescription;
            int count = CategoryUpdate.Items.Count;
            for (int i = 0; i < count; i++)
            {
                CategoryUpdate.Items.RemoveAt(0);
            }
            List<string> allCategory = system.getAllCategories();
            foreach (string cat in allCategory)
            {
                CategoryUpdate.Items.Add(cat);
            }
            CategoryUpdate.Value = toUpdate.Category.catName;
        }


        public void populateBusiness(List<string> titels)
        {
            titels.Add("Buisness Name");
            titels.Add("Adress");
            titels.Add("City");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("Status");
            titels.Add("Update");
            titels.Add("Remove");
        }

        public void populateSocialCoupons(List<string> titels)
        {
            titels.Add("Name");
            titels.Add("Buisness Name");
            titels.Add("Original Price");
            titels.Add("Discount Price");
            titels.Add("DeadLine");
            titels.Add("Average Rank");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("City");
            titels.Add("Site");
            titels.Add("Status");
            titels.Add("Update");
            titels.Add("Remove");

        }

        public void createSocialCouponTable(ICollection<CatalogCoupons_SocialNetworkCoupon> couponeList)
        {
            if (couponeList != null)
            {
                socialCouponTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateSocialCoupons(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                socialCouponTable.Controls.Add(hederRow);

                int i = 1;
                foreach (CatalogCoupons_SocialNetworkCoupon item in couponeList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = { item.CatalogCoupon.CouponName, 
                                            item.CatalogCoupon.Buisness.buisName,
                                            item.CatalogCoupon.originalPrice,
                                           item.CatalogCoupon.priceAfterDiscount,
                                           item.CatalogCoupon.deadLineForUse,
                                           item.CatalogCoupon.averageRank,
                                           item.CatalogCoupon.description,
                                           item.CatalogCoupon.Category.catName,
                                           item.CatalogCoupon.Buisness.buisCity,
                                           item.origionWebsite,
                                           item.CatalogCoupon.Status
                                           };

                    foreach (var info in information)
                    {
                        TableCell cellNew = new TableCell();
                        Label lblNew = new Label();
                        if (info != null)
                        {
                            lblNew.Text = info.ToString();
                        }
                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);

                    }

                    TableCell cellBtn = new TableCell();
                    Button btn = new Button();
                    btn.ID = "social_" + item.CatalogCoupon.catalogID;
                    btn.Text = "Update";
                    btn.Click += btnUpdateSocial_Click;
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);

                    TableCell cellRemoveBtn = new TableCell();
                    Button btnRemove = new Button();
                    btnRemove.ID = "remove_social_" + item.CatalogCoupon.catalogID;
                    btnRemove.Text = "Remove";
                    btnRemove.Click += new EventHandler(removeSocialCoupon);
                    btnRemove.Attributes.Add("onclick",
                        "ConfirmRemoveSocial('" + item.CatalogCoupon.CouponName + "');");
                    cellRemoveBtn.Controls.Add(btnRemove);
                    rowNew.Controls.Add(cellRemoveBtn);

                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    socialCouponTable.Controls.Add(rowNew);
                }
            }

        }

        void btnUpdateSocial_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID.Replace("social_", "");
            int couponId = int.Parse(id);
            Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
         "&couponType=social&userType=businessOwner&action=update&couponId=" + couponId);
        }


        protected void removeSocialCoupon(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_remove_social_value"];
            if (confirmValue == "Yes")
            {
                Button btn = (Button)sender;
                string id = btn.ID.Replace("remove_social_", "");
                int couponId = int.Parse(id);
                system.removeSocialNetworkCoupon(couponId);
                createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);
            }
        }

        protected void myCoupons_onClick(object sender, EventArgs e)
        {
            changeDiv(MyCouponsDiv, MyCouponsSection);           
        }

        protected void myCatalogCoupons_onClick(object sender, EventArgs e)
        {
            catalogCouponsDiv.Visible = true;
            socialCouponDiv.Visible = false;
            createCatalogouponsTable(system.getAllCouponsBusinessOwner(loggedUser.userName));
        }

        protected void mySocialCoupons_onClick(object sender, EventArgs e)
        {
            catalogCouponsDiv.Visible = false;
            socialCouponDiv.Visible = true;
            createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);
        }


        protected void showSelected_Click(object sender, EventArgs e)
        {
            string selected = selectBusiness.Items[selectBusiness.SelectedIndex].Text;
            if (selected.Equals("All"))
            {
                createCatalogouponsTable(system.getAllCouponsBusinessOwner(loggedUser.userName));
            }
            else
            {
                createCatalogouponsTable(system.getAllCouponsBusiness(selected));
            }
        }

        public void populateCatalogCoupons(List<string> titels)
        {
            titels.Add("Name");
            titels.Add("Buisness Name");
            titels.Add("Original Price");
            titels.Add("Discount Price");
            titels.Add("DeadLine");
            titels.Add("Average Rank");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("City");
            titels.Add("Status");
            titels.Add("Update");
            titels.Add("Remove");
        }

        public void createCatalogouponsTable(List<CatalogCoupon> couponeList)
        {
            if (couponeList != null)
            {
                CatalogCouponTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateCatalogCoupons(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                CatalogCouponTable.Controls.Add(hederRow);

                int i = 1;
                foreach (CatalogCoupon item in couponeList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = {item.CouponName, item.Buisness.buisName, item.originalPrice, 
                                    item.priceAfterDiscount, item.deadLineForUse, item.averageRank, 
                                    item.description, item.Category.catName, item.Buisness.buisCity, item.Status};

                    foreach (var info in information)
                    {
                        TableCell cellNew = new TableCell();
                        Label lblNew = new Label();
                        if (info != null)
                        {
                            lblNew.Text = info.ToString();
                        }
                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);
                    }
                        TableCell cellBtn = new TableCell();
                        Button btn = new Button();
                        btn.ID = "" + item.catalogID;
                        btn.Text = "Update";
                        btn.Click += new EventHandler(updateCatalogCoupon);
                        cellBtn.Controls.Add(btn);
                        rowNew.Controls.Add(cellBtn);

                        TableCell cellRemoveBtn = new TableCell();
                        Button btnRemove = new Button();
                        btnRemove.ID = "remove_" + item.catalogID;
                        btnRemove.Text = "Remove";
                        btnRemove.Click += new EventHandler(removeCatalogCoupon);
                        btnRemove.Attributes.Add("onclick",
                            "ConfirmRemoveCatalog('" + item.CouponName + "');");
                        cellRemoveBtn.Controls.Add(btnRemove);
                        rowNew.Controls.Add(cellRemoveBtn);


                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    CatalogCouponTable.Controls.Add(rowNew);
                }
            }

        }

        private void removeCatalogCoupon(object sender, EventArgs e)
        {
            if (Request.Form["confirm_remove_catalog_value"] == "Yes")
            {
                Button btn = (Button)sender;
                int couponId = int.Parse(btn.ID.Replace("remove_",""));
                system.removeCatalogCoupon(couponId);
                createCatalogouponsTable(system.getAllCouponsBusinessOwner(loggedUser.userName));
            }
        }

        private void updateCatalogCoupon(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
                     "&couponType=Catalog&userType=business&action=update&couponId=" + btn.ID);
        }
        protected void addBusiness_onClick(object sender, EventArgs e)
        {
            changeDiv(addBusinessDiv, addBusinessSection);

            BusinessName.Text="";
            Adress.Text="";
            City.Text=""; 
            Description.Text="";
        }
        protected void addBusinesToSystem_onCLick(object sender, EventArgs e)
        {
            if (validateBuisInput())
            {
                bool insert=system.addBuisness(loggedUser.userName, BusinessName.Text, Adress.Text, City.Text, Description.Text, CategorySelect.Value);
                updateBusinessDropDownList(loggedUser.userName);
                if(insert)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Business added successfully and waiting for approval')", true);
                    changeDiv(myBusinessDiv, myBusinessSection);
                    createBusinessTable(loggedUser.Buisnesses);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Business Name is allready taken')", true);
                }
                updateBusinessDropDownList(loggedUser.userName);
            }
        }
        public bool validateBuisInput()
        {
            bool isOk = true;
            string errorMessage = "The Following fields required:";
            if (BusinessName.Text=="")
            {
                errorMessage += " Business Name,";
                isOk = false;
            }
            if (Adress.Text == "")
            {
                errorMessage += " Adress,";
                isOk = false;
            }
            if (City.Text == "")
            {
                errorMessage += " City,";
                isOk = false;
            }
            if (Description.Text == "")
            {
                errorMessage += " Description,";
                isOk = false;
            }
            if(!isOk)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('All Fields Required')", true);// need to check how to insert errorMesage
            }
            return isOk;
        }
        protected void businessProfile_onClick(object sender, EventArgs e)
        {

            changeDiv(profileDiv, profileSection);
            showOldData();

        }

        private void showOldData()
        {
            UserName.Text = loggedUser.userName;      
            Password.Text = loggedUser.User.password;
            ConfirmPassword.Text = loggedUser.User.password;
            Email.Text = loggedUser.User.email;
            FullName.Text = loggedUser.User.fullName;
            TextBoxPhone.Text = loggedUser.User.phone;
        }



        protected void addCouponButton_Click(object sender, EventArgs e)
        {
            List<string> Approved = system.getApprovedBusinessForOwner(loggedUser.userName);
            if(Approved.Count==0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('At least one business approved by adminstrator is required')", true);
            }
            else
            Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
                "&couponType=Catalog&userType=business");
        }

        protected void addSocialCouponButton_Click(object sender, EventArgs e)
        {
            if (system.getApprovedBusiness().Count > 0)
            {
                Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
                    "&couponType=social&userType=businessOwner");
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('At least one business approved by adminstrator is required')", true);
        }

        public void businessUpdate_onClick(object sender, EventArgs e)
        {
            if ((Password.Text != "") && (ConfirmPassword.Text != ""))
            {

                system.updateBusinessOwner(loggedUser.userName, Password.Text, Email.Text, FullName.Text, Phone.Text);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Details updated successfully')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Password and it confirmation is required')", true);
            }
        }

        protected void updateBusinesToSystem_onCLick(object sender, EventArgs e)
        {
            if (Adress1.Text == "" || City1.Text == "" || Descriptionlbl.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please fill all data')", true);
            }
            else
            {
                bool res = system.updateBusiness(BusiName.Text, Adress1.Text, City1.Text, Description1.Text, CategoryUpdate.Value);
                if(res)
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Business have been updated')", true);
                else
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('A problem occured, couldn't update business')", true);
                changeDiv(myBusinessDiv, myBusinessSection);
                createBusinessTable(loggedUser.Buisnesses);
            }
        }


        protected void activate_onClick(object sender, EventArgs e)
        {
            if(serialBox.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Insert Serial Key')", true);
            }
            else
            {
                bool check = system.checkCouponBelong(loggedUser.userName, serialBox.Text);
                if (check)
                {
                    system.updateOrderedCouponToUse(serialBox.Text);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Coupon was activated succesfully')", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Coupon was not found')", true);
            }
            
        }

        private void changeDiv(HtmlGenericControl div, HtmlGenericControl menuItem)
        {
            myBusinessDiv.Visible = false;
            addBusinessDiv.Visible = false;
            MyCouponsDiv.Visible = false;
            updateBusinessDiv.Visible = false;
            profileDiv.Visible = false;
            ActivateCouponsDiv.Visible = false;
            div.Visible = true;

            myBusinessSection.Attributes["Class"] = "";
            addBusinessSection.Attributes["Class"] = "";
            MyCouponsSection.Attributes["Class"] = "";
            ActivateCouponsSection.Attributes["Class"] = "";
            profileSection.Attributes["Class"] = "";
            menuItem.Attributes["Class"] = "active";

        }
    }

}