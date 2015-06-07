using CouponSystemWeb.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace CouponSystemWeb
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        Users_Customer loggedUser;
        private bool coupBuisName = false;
        private bool coupCategory = false;
        private bool coupLocation = false;
        private List<string> allCategories = new List<string>();
        SystemController system;
        private const int radius = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            system = new SystemController();
            string userName = Request.QueryString["userName"];
            loggedUser = system.findCustomerByName(userName);
            if (!IsPostBack)
            {
                List<string> categories = system.getAllCategories();
                foreach (var item in categories)
                {
                    categoriesLst.Items.Add(new ListItem(item));
                }
                foreach (var item in categories)
                {
                    catList.Items.Add(new ListItem(item));
                }
            }

            if (IsPostBack)
            {
                if (searchDiv.Visible && validateInputs())
                {
                    createSearchTable(SearchForCoupon());
                    searchTableDive.Visible = true;
                }
                else if (usedCouponDiv.Visible)
                {
                    List<OrderedCoupon> usedlist = new List<OrderedCoupon>();
                    foreach (OrderedCoupon item in loggedUser.OrderedCoupons)
                    {
                        if (item.isUsed == true)
                        {
                            usedlist.Add(item);
                        }
                    }
                    createUsedTable(usedlist);
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                system = new SystemController();
                string loggeduserName = Request.QueryString["userName"];
                loggedUser = system.findCustomerByName(loggeduserName);
                createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);
            }
        }



        protected void searchBtn_Click(object sender, EventArgs e)
        {
            if (!validateInputs())
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please insert values for search')", true);
            }
            else
            {
                searchTableDive.Visible = true;
                createSearchTable(SearchForCoupon());
            }

        }

        private bool validateInputs()
        {
            if (BuisnessCbx.Checked == true && BuisName.Value != "")
            {
                coupBuisName = true;
            }
            if (LocationCbx.Checked == true && (gps.Checked == true || city.Checked == true))
            {
                coupLocation = true;
            }
            if (CategoryCbx.Checked == true)
            {

                foreach (ListItem item in categoriesLst.Items)
                {
                    if (item.Selected == true)
                    {
                        coupCategory = true;
                        allCategories.Add(item.Text);
                    }
                }
            }
            return (coupBuisName || coupLocation || coupCategory);
        }


        public List<CatalogCoupon> SearchForCoupon()
        {

            List<CatalogCoupon> allCoupon = system.getAllCatalogCoupons();
            List<CatalogCoupon> tmpCoupons = system.getAllCatalogCoupons();
            double longitude = loggedUser.Location.longitude;
            double lat = loggedUser.Location.longitude;
            foreach (CatalogCoupon item in tmpCoupons)
            {
                if (coupBuisName == true)
                {
                    if (item.Buisness.buisName != BuisName.Value)
                        allCoupon.Remove(item);
                }
                if (coupCategory == true)
                {
                    if (!allCategories.Contains(item.Category.catName))
                    {
                        allCoupon.Remove(item);
                    }
                }
                if (coupLocation == true)
                {
                    if (city.Checked && 
                        item.Buisness.buisCity != SelectCity.Items[SelectCity.SelectedIndex].Text)
                    {
                        allCoupon.Remove(item);
                    }
                    if (gps.Checked && !(longitude < item.Location.longitude + radius
                        && longitude > item.Location.longitude - radius) && 
                        !(lat < item.Location.latitude + radius
                        && lat > item.Location.latitude - radius))
                    {
                        allCoupon.Remove(item);
                    }
                }
            }

            return allCoupon;
        }

        public void populate(List<string> titels)
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
            titels.Add("Purchase");
        }

        public void populateOrder(List<string> titels)
        {
            titels.Add("Name");
            titels.Add("Buisness Name");
            titels.Add("Discount Price");
            titels.Add("DeadLine");
            titels.Add("Average Rank");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("City");
            titels.Add("Date Of Purchase");
            titels.Add("Serial Key");
            titels.Add("Used");
        }

        public void populateUsed(List<string> titels)
        {
            titels.Add("Name");
            titels.Add("Buisness Name");
            titels.Add("Discount Price");
            titels.Add("DeadLine");
            titels.Add("Average Rank");
            titels.Add("Description");
            titels.Add("Category");
            titels.Add("City");
            titels.Add("Date Of Purchase");
            titels.Add("Date Of Use");
            titels.Add("Rank");
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


        public void createSearchTable(List<CatalogCoupon> couponeList)
        {
            if (couponeList != null)
            {
                searchCouponTbl.Controls.Clear();
                List<string> titels = new List<string>();
                populate(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                searchCouponTbl.Controls.Add(hederRow);

                int i = 1;
                foreach (CatalogCoupon item in couponeList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = {item.CouponName, item.Buisness.buisName, item.originalPrice, 
                                    item.priceAfterDiscount, item.deadLineForUse, item.averageRank, 
                                    item.description, item.Category.catName, item.Buisness.buisCity};

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
                    btn.Text = "Purchase";
                    btn.Click += new EventHandler(this.purchaseCoupon);
                    btn.Attributes.Add("onclick", "Confirm('" + item.CouponName + "');");
                    //btn.Attributes.Add("onServerclick", "Confirm('" + item.CouponName + "');");
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);
                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    searchCouponTbl.Controls.Add(rowNew);
                }
            }

        }

        public void createPurchesudCouponsTable(ICollection<OrderedCoupon> couponeList)
        {
            if (couponeList != null)
            {
                purchasedTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateOrder(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                purchasedTable.Controls.Add(hederRow);

                int i = 1;
                foreach (OrderedCoupon item in couponeList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = { item.CatalogCoupon.CouponName, 
                                            item.CatalogCoupon.Buisness.buisName,
                                           item.CatalogCoupon.priceAfterDiscount,
                                           item.CatalogCoupon.deadLineForUse,
                                           item.CatalogCoupon.averageRank,
                                           item.CatalogCoupon.description,
                                           item.CatalogCoupon.Category.catName,
                                           item.CatalogCoupon.Buisness.buisCity,
                                           item.dateOfPurchase,
                                           item.serialKey};

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
                    TableCell cell = new TableCell();
                    Label lbl = new Label();
                    if (item.isUsed == true)
                    {
                        lbl.Text = "Yes";
                    }
                    else
                    {
                        lbl.Text = "No";
                    }
                    cell.Controls.Add(lbl);
                    rowNew.Controls.Add(cell);
                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    purchasedTable.Controls.Add(rowNew);
                }
            }

        }

        public void createUsedTable(List<OrderedCoupon> couponeList)
        {
            if (couponeList != null)
            {
                usedCouponsTable.Controls.Clear();
                List<string> titels = new List<string>();
                populateUsed(titels);
                TableHeaderRow hederRow = new TableHeaderRow();
                foreach (var title in titels)
                {
                    TableHeaderCell headerCell = new TableHeaderCell();
                    headerCell.Text = title;
                    hederRow.Controls.Add(headerCell);
                }
                usedCouponsTable.Controls.Add(hederRow);

                int i = 1;
                foreach (OrderedCoupon item in couponeList)
                {
                    TableRow rowNew = new TableRow();
                    Object[] information = { item.CatalogCoupon.CouponName, 
                                            item.CatalogCoupon.Buisness.buisName,
                                           item.CatalogCoupon.priceAfterDiscount,
                                           item.CatalogCoupon.deadLineForUse,
                                           item.CatalogCoupon.averageRank,
                                           item.CatalogCoupon.description,
                                           item.CatalogCoupon.Category.catName,
                                           item.CatalogCoupon.Buisness.buisCity,
                                           item.dateOfPurchase, item.dateOfUse};

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
                    if (item.rank == null)
                    {
                        TableCell cellBtn = new TableCell();
                        HtmlInputText rankInput = new HtmlInputText();
                        rankInput.ID = "rank_" + item.serialKey;
                        rankInput.Attributes.Add("placeholder", "enter rank ");
                        rankInput.Attributes.Add("pattern", "[1-5]");
                        rankInput.Attributes.Add("title", "The rank should be between 1 and 5");
                        cellBtn.Controls.Add(rankInput);
                        Button btn = new Button();
                        btn.ID = "" + item.serialKey;
                        btn.Text = "Save Rank";
                        btn.Click += new EventHandler(addRank);
                        string serialKey = item.serialKey;
                        btn.Attributes.Add("onclick",
                            "ConfirmRank('rank_" + serialKey + "');");
                        cellBtn.Controls.Add(btn);
                        rowNew.Controls.Add(cellBtn);
                    }
                    else
                    {
                        TableCell cellNew = new TableCell();
                        Label lblNew = new Label();
                        lblNew.Text = item.rank.ToString();
                        cellNew.Controls.Add(lblNew);
                        rowNew.Controls.Add(cellNew);
                    }

                    if (i % 2 == 0)
                    {
                        rowNew.Attributes["Class"] = "alt";
                    }
                    i++;
                    usedCouponsTable.Controls.Add(rowNew);
                }
            }

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
                    btn.ID = "" + item.CatalogCoupon.catalogID;
                    btn.Text = "Update";
                    btn.Click += new EventHandler(updateSocialCoupon);
                    cellBtn.Controls.Add(btn);
                    rowNew.Controls.Add(cellBtn);

                    TableCell cellRemoveBtn = new TableCell();
                    Button btnRemove = new Button();
                    btnRemove.ID = "remove_" + item.CatalogCoupon.catalogID;
                    btnRemove.Text = "Remove";
                    btnRemove.Click += new EventHandler(removeSocialCoupon);
                    btnRemove.Attributes.Add("onclick",
                        "ConfirmRemove('" + item.CatalogCoupon.CouponName + "');");
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

        protected void updateSocialCoupon(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
         "&couponType=social&userType=customer&action=update&couponId=" + btn.ID);
        }

        protected void removeSocialCoupon(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_remove_value"];
            if (confirmValue == "Yes")
            {
                Button btn = (Button)sender;
                string id = btn.ID.Replace("remove_", "");
                int catalogId = int.Parse(id);
                system.removeSocialNetworkCoupon(catalogId);
                createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);
            }
        }
        protected void addRank(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirmRank_value"];
            if (confirmValue == "Yes")
            {
                Button btn = (Button)sender;
                string serialKey = btn.ID;
                OrderedCoupon oc = system.findOrderedCoupon(serialKey);
                string rank = Request.Form["rank_value"];
                short rankValue = short.Parse(rank);
                system.updateOrderedCouponRank(oc, rankValue);
                Control cell = btn.Parent;
                cell.Controls.Clear();
                Label lbl = new Label();
                lbl.Text = rank;
                cell.Controls.Add(lbl);
            }
        }

        protected void purchased_onClick(object sender, EventArgs e)
        {
            changeDiv(purchasedDiv, purchasedSection);
            createPurchesudCouponsTable(loggedUser.OrderedCoupons);

        }

        protected void searchDiv_onClick(object sender, EventArgs e)
        {
            BuisnessCbx.Checked = false;
            CategoryCbx.Checked = false;
            LocationCbx.Checked = false;
            changeDiv(searchDiv, searchSection);

        }
        protected void profile_onClick(object sender, EventArgs e)
        {

            changeDiv(profileDiv, profileSection);
            showOldData();

        }

        protected void socialSection_onClick(object sender, EventArgs e)
        {
            changeDiv(socialCouponDiv, socialSection);
            createSocialCouponTable(loggedUser.User.CatalogCoupons_SocialNetworkCoupon);

        }

        protected void used_onClick(object sender, EventArgs e)
        {
            changeDiv(usedCouponDiv, usedSection);
            List<OrderedCoupon> usedlist = new List<OrderedCoupon>();
            foreach (OrderedCoupon item in loggedUser.OrderedCoupons)
            {
                if (item.isUsed == true)
                {
                    usedlist.Add(item);
                }
            }
            createUsedTable(usedlist);

        }

        protected void purchaseCoupon(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                Button btn = (Button)sender;
                int catalogId = int.Parse(btn.ID);
                CatalogCoupon coupon = system.findCatalogCoupon(catalogId);
                system.addCoupon(loggedUser, coupon);
            }
        }

        private void changeDiv(HtmlGenericControl div, HtmlGenericControl menuItem)
        {
            purchasedDiv.Visible = false;
            searchDiv.Visible = false;
            usedCouponDiv.Visible = false;
            socialCouponDiv.Visible = false;
            profileDiv.Visible = false;
            div.Visible = true;

            purchasedSection.Attributes["Class"] = "";
            searchSection.Attributes["Class"] = "";
            usedSection.Attributes["Class"] = "";
            socialSection.Attributes["Class"] = "";
            profileSection.Attributes["Class"] = "";
            menuItem.Attributes["Class"] = "active";

        }

        private void showOldData()
        {
            List<string> categories = system.getAllCategories();
            List<string> userCategory = system.getCustomerCategories(loggedUser);
            UserName.Text = loggedUser.userName;
            foreach (ListItem item in catList.Items)
            {
                if (userCategory.Contains(item.Text))
                {
                    item.Selected = true;
                }
            }
            Password.Text = loggedUser.User.password;
            ConfirmPassword.Text = loggedUser.User.password;
            Email.Text = loggedUser.User.email;
            FullName.Text = loggedUser.User.fullName;
            TextBoxPhone.Text = loggedUser.User.phone;
            if (loggedUser.gender == "M")
            {
                maleRadio.Checked = true;
            }
            else
            {
                femaleRadio.Checked = true;
            }
            ageTextBox.Text = loggedUser.age.ToString();
        }

        public void update_onClick(object sender, EventArgs e)
        {
            if ((Password.Text != "") && (ConfirmPassword.Text != ""))
            {
                List<string> selectedCategory = new List<string>();
                foreach (ListItem item in catList.Items)
                {
                    if (item.Selected == true)
                    {
                        selectedCategory.Add(item.Text);
                    }
                }
                if (selectedCategory.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please choose at least on category')", true);
                }
                else
                {
                    string tmpGender = "F";
                    if (maleRadio.Checked)
                    {
                        tmpGender = "M";
                    }
                    system.updateCustomer(loggedUser.userName, Password.Text, Email.Text, FullName.Text, Phone.Text, tmpGender, short.Parse(ageTextBox.Text), selectedCategory);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Details updated successfully')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Password and it confirmation is required')", true);
            }
        }

        protected void addCouponButton_Click(object sender, EventArgs e)
        {
            if (system.getApprovedBusiness().Count > 0)
            {
                Response.Redirect("addCoupon.aspx?userName=" + loggedUser.userName +
                    "&couponType=social&userType=customer");
            }
            else
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('At least one business approved by adminstrator is required')", true);
        }

    }
}