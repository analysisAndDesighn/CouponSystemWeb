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

    public partial class astart : System.Web.UI.Page
    {
        Users_SystemAdministrator loggedUser;
        SystemController system;
        protected void Page_Load(object sender, EventArgs e)
        {
            system = new SystemController();
            string userName = Request.QueryString["userName"];
            loggedUser = system.findAdminByName(userName);
            if(IsPostBack)
            {
                if (approveCouponsDiv.Visible)
                {
                    createCouponsTable();
                }
                if (approveBusinessDiv.Visible)
                {
                    createBusinessTable();
                }
                if (allBusinessDiv.Visible)
                {
                    createAllBusinessTable();
                }                               
            }
            else
            {
                approveCoupons_onClick(null, null);
            }

        }

        protected void approveCoupons_onClick(object sender, EventArgs e)
        {
            changeDiv(approveCouponsDiv, approveCoupons);
            createCouponsTable();

        }
        protected void addAdminBtn_onCLick(object sender, EventArgs e)
        {
            string adminName = userNameAdmin.Text;
            string password = PasswordAdmin.Text;
            string email = Email.Text;
            string fullName = FullName.Text;
            string phone = Phone.Text;
            if(adminName=="" || password=="" || email=="" || fullName=="" || phone=="")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('All fields must be filled')", true);
            }
            else
            {
                bool result = system.addAdmin(adminName, password, email, fullName, phone);
                if (result)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Admin added succesfully')", true);
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User Name is allready taken')", true);
            }

        }


        protected void approveBysiness_onClick(object sender, EventArgs e)
        {
            changeDiv(approveBusinessDiv, approveBusiness);
           // createBusinessTable();

        }

        protected void addAdmin_onClick(object sender, EventArgs e)
        {
            changeDiv(addAdminDiv, addAdmin);
            createCouponsTable();

        }

        public void createCouponsTable()
        {
            List<CatalogCoupon> waitList = system.getAllAwaitingCoupons();
            approveCouponsTable.Controls.Clear();
            List<string> titels = new List<string>();
            populateCoupons(titels);
            TableHeaderRow hederRow = new TableHeaderRow();
            foreach (var title in titels)
            {
                TableHeaderCell headerCell = new TableHeaderCell();
                headerCell.Text = title;
                hederRow.Controls.Add(headerCell);
            }
            approveCouponsTable.Controls.Add(hederRow);

            int i = 1;
            foreach (CatalogCoupon item in waitList)
            {
                TableRow rowNew = new TableRow();

                Object[] information = { item.CouponName,
                                            item.Buisness.buisName,
                                           item.originalPrice,
                                           item.priceAfterDiscount,
                                           item.deadLineForUse,
                                           item.description,
                                           item.Category.catName,
                                           item.Buisness.buisCity,
                                           item.Buisness.Users_BuisnessOwner.User.fullName};
                titels.Add("Name");
                titels.Add("Buisness Name");
                titels.Add("Original Price");
                titels.Add("Discount Price");
                titels.Add("DeadLine");
                titels.Add("Description");
                titels.Add("Category");
                titels.Add("City");
                titels.Add("Owner");
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
                btn.Text = "Approve";
                btn.Click += new EventHandler(this.ApproveForCatalog);
                btn.Attributes.Add("onclick",
                    "ConfirmApprove('" + item.CouponName + "');");
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
                approveCouponsTable.Controls.Add(rowNew);
            }
        }

        private void removeCatalogCoupon(object sender, EventArgs e)
        {
            if (Request.Form["confirm_remove_value"] == "Yes")
            {
                Button btn = (Button)sender;
                int couponId = int.Parse(btn.ID.Replace("remove_", ""));
                system.removeCatalogCoupon(couponId);
                createCouponsTable();
            }
        }

        private void ApproveForCatalog(object sender, EventArgs e)
        {
            if (Request.Form["confirm_value"] == "Yes")
            {
                Button btn = (Button)sender;
                int couponId = int.Parse(btn.ID);
                system.approveCatalogCoupon(couponId);
                createCouponsTable();
            }
        }


        public void populateCoupons(List<string> titels)
        {
            titels.Add("Name");
            titels.Add("Buisness Name");
            titels.Add("Original Price");
            titels.Add("Discount Price");
            titels.Add("DeadLine");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("City");
            titels.Add("Owner");
            titels.Add("Approve");
            titels.Add("Remove");
        }



        public void createBusinessTable()
        {
            List<Buisness> buisnessList = system.getAllAwaitingBusiness();
            if (buisnessList != null)
            {
                aapproveBusiness.Controls.Clear();
                List<string> titels = new List<string>();
                populateBusiness(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                aapproveBusiness.Controls.Add(hederRow);

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
                    btn.ID = "Approve_" + item.buisName;
                    btn.Text = "Approve";
                    btn.Click += new EventHandler(this.ApproveCurrentBusiness);
                    btn.Attributes.Add("onclick",
                            "ConfirmApproveBusiness('" + item.buisName + "');");
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);


                    cellBtn = new TableCell();
                    btn = new Button();
                    btn.ID = item.buisName;
                    btn.Text = "Remove";
                    btn.Click += new EventHandler(this.removeCurrentBusiness);
                    btn.Attributes.Add("onclick",
                        "ConfirmRemoveBusiness('" + item.buisName + "');");
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);

                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    aapproveBusiness.Controls.Add(rowNew);
                }
            }


        }

        private void ApproveCurrentBusiness(object sender, EventArgs e)
        {
            if (Request.Form["confirm_value"] == "Yes")
            {
                Button btn = (Button)sender;
                string buisName = btn.ID.Replace("Approve_","");
                system.approveBusinessCoupon(buisName);
                createBusinessTable();
            }
        }

        private void removeCurrentBusiness(object sender, EventArgs e)
        {
            if (Request.Form["confirm_remove_value"] == "Yes")
            {
                Button btn = (Button)sender;
                string buisName = btn.ID;
                system.removeBuisnessByName(buisName);
                createBusinessTable();
            }
        }


        public void populateBusiness(List<string> titels)
        {
            titels.Add("Buisness Name");
            titels.Add("Adress");
            titels.Add("City");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("Owner");
            titels.Add("Approve");
            titels.Add("Remove");
        }

        public void populateAllBusiness(List<string> titels)
        {
            titels.Add("Buisness Name");
            titels.Add("Adress");
            titels.Add("City");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("Owner");
            titels.Add("Remove");
        }



        private void changeDiv(HtmlGenericControl div, HtmlGenericControl menuItem)
        {
            approveCouponsDiv.Visible = false;
            approveBusinessDiv.Visible = false;
            addAdminDiv.Visible = false;
            allBusinessDiv.Visible = false;
            addCategoryDiv.Visible = false;
            div.Visible = true;

            approveCoupons.Attributes["Class"] = "";
            approveBusiness.Attributes["Class"] = "";
            addAdmin.Attributes["Class"] = "";
            RemoveBuisness.Attributes["Class"] = "";
            addCategoryLi.Attributes["Class"] = "";
            menuItem.Attributes["Class"] = "active";

        }

        protected void AddSocialBtn_Click(object sender, EventArgs e)
        {
            if (system.getApprovedBusiness().Count > 0)
            {
                Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
                    "&couponType=social&userType=admin");
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('At least one business approved by adminstrator is required')", true);
        }


        protected void adCategoryDiv_Click(object sender, EventArgs e)
        {
            changeDiv(addCategoryDiv, addCategoryLi);
        }

        protected void allBuisnessDiv_Click(object sender, EventArgs e)
        {
            changeDiv(allBusinessDiv, RemoveBuisness);
            createAllBusinessTable();
        }

        public void createAllBusinessTable()
        {
            List<Buisness> buisnessList = system.getAllApprovedBusiness();
            if (buisnessList != null)
            {
                AllbusinessTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateAllBusiness(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                AllbusinessTable.Controls.Add(hederRow);

                int i = 1;
                foreach (Buisness item in buisnessList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = { item.buisName,
                                            item.buisAddress,
                                           item.buisCity,
                                           item.BuisDescription,
                                           item.Category.catName,
                                           item.Users_BuisnessOwner.userName};

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
                    btn.ID = item.buisName;
                    btn.Text = "Remove";
                    btn.Click += new EventHandler(this.removeCurrentBusiness);
                    btn.Attributes.Add("onclick",
                        "ConfirmRemoveBusiness('" + item.buisName + "');");
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);

                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    AllbusinessTable.Controls.Add(rowNew);
                }
            }


        }

        protected void addCategory_ServerClick(object sender, EventArgs e)
        {
            if (newCategoryTxt.Text.Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Must Enter a Category Name')", true);
            }
            else
            {
                string categoryName = newCategoryTxt.Text;
                bool succ = system.addCategory(categoryName);
                if (succ)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('The Category was added suuccefuly')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('The Category already exists')", true);
                }
            }
        }
    }
}