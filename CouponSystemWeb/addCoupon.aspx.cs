using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CouponSystemWeb.App_Code;

namespace CouponSystemWeb
{
    public partial class addCoupon : System.Web.UI.Page
    {
        string userName;
        SystemController system;
        string userType;
        string action;
        string couponType;
        string couponId;

        protected void Page_Load(object sender, EventArgs e)
        {
            system = new SystemController();
            userName = Request.QueryString["userName"];
            couponType = Request.QueryString["couponType"];
            userType = Request.QueryString["userType"];
            action = Request.QueryString["action"];
            couponId = Request.QueryString["couponId"];
            if (!IsPostBack)
            {
                if (couponType.Equals("social"))
                {
                    List<string> list = system.getApprovedBusiness();
                    foreach (var item in list)
                    {
                        buisSelect.Items.Add(new ListItem(item));
                    }
                    webSiteUpdate.Visible = true;
                }
                else // it's a catalog coupon
                {
                    List<string> list = system.getApprovedBusinessForOwner(userName);
                    foreach (var item in list)
                    {
                        buisSelect.Items.Add(new ListItem(item));
                    }

                }
                buisnessRow.Visible = true;
                if (action == "update")
                {
                    eventButton.Text = "Update Coupon";
                    buisnessRow.Visible = false;
                    updateBusinessCase();//check in case of customer

                }
            }


        }

        private void updateBusinessCase()
        {
            CatalogCoupon catUpdate;
            CatalogCoupons_SocialNetworkCoupon socialUpdate;
            if (couponType.Equals("Catalog"))
            {
                catUpdate = system.findCatalogCoupon(int.Parse(couponId));
                nameInput.Value = catUpdate.CouponName;
                descriptionInput.Value = catUpdate.description;
                DeadLineInput.Value = Convert.ToString(catUpdate.deadLineForUse);
                OriginalPriceInput.Value = catUpdate.originalPrice.ToString();
                discountInput.Value = catUpdate.priceAfterDiscount.ToString();
            }
            else
            {
                socialUpdate = system.findSocialCoupon(int.Parse(couponId));
                nameInput.Value = socialUpdate.CatalogCoupon.CouponName;
                descriptionInput.Value = socialUpdate.CatalogCoupon.description;
                DeadLineInput.Value = Convert.ToString(socialUpdate.CatalogCoupon.deadLineForUse);
                OriginalPriceInput.Value = socialUpdate.CatalogCoupon.originalPrice.ToString();
                discountInput.Value = socialUpdate.CatalogCoupon.priceAfterDiscount.ToString();
                webSiteInput.Value = socialUpdate.origionWebsite;
            }



        }

        public void button_Click(object sender, EventArgs e)
        {
            double originalPrice = Double.Parse(OriginalPriceInput.Value);
            double afterDiscountPrice = Double.Parse(discountInput.Value);
            if (originalPrice < afterDiscountPrice)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), 
                    "ClientScript", "alert('Price after discount must be smaller than original price')", true);
            }
            else if (Convert.ToDateTime(DeadLineInput.Value) < DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), 
                    "ClientScript", "alert('Deadline for use must be greater than now')", true);
            }
            else
            {
                if (couponType == "social")
                {
                    if (webSiteInput.Value == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please Insert Website')", true);
                    }
                    else// all input are fine
                    {
                        if (action == null) // Add Coupon state
                        {
                            bool result = system.createSocialCoupon(originalPrice, afterDiscountPrice, buisSelect.Items[buisSelect.SelectedIndex].Text,
                                nameInput.Value, descriptionInput.Value, DeadLineInput.Value, webSiteInput.Value, userName);
                            if (result)
                            {
                                if (userType.Equals("customer"))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                 "alert('Social Coupon Added succesfuly, awaiting for Admin approval');window.location ='cstart.aspx?userName=" + userName + "';", true);
                                }
                                else if (userType.Equals("businessOwner"))
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                 "alert('Social Coupon Added succesfuly, awaiting for Admin approval');window.location ='bstart.aspx?userName=" + userName + "';", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                "alert('Social Coupon Added succesfuly, awaiting for Admin approval');window.location ='astart.aspx?userName=" + userName + "';", true);
                                }

                            }
                            else //cannot add coupon
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Could not add coupon')", true);
                            }
                        }
                        else//meaning we are on update social catalog coupon Mode
                        {
                            bool result = system.updateSocialCoupon(originalPrice, afterDiscountPrice,
                                  nameInput.Value, descriptionInput.Value, DeadLineInput.Value, webSiteInput.Value, int.Parse(couponId));
                            if (result)
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                                 "alert('Social Coupon updated succesfully');window.location ='cstart.aspx?userName=" + userName + "';", true);
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Could not update coupon')", true);
                            }

                        }
                    }
                }
                else //meaning-coupon is catalog Coupon and the user is buisness owner
                {
                    if (action == null) // Add Coupon state
                    {
                        bool result = system.createCatalogCoupon(originalPrice, afterDiscountPrice, buisSelect.Items[buisSelect.SelectedIndex].Text,
                                nameInput.Value, descriptionInput.Value, DeadLineInput.Value);
                        if (result)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                         "alert('Catalog Coupon Added succesfuly, awaiting for Admin approval');window.location ='bstart.aspx?userName=" + userName + "';", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Could not add coupon')", true);
                        }
                    }
                    else //meaning- update catalog mode
                    {
                        bool result = system.updateCatalogCoupon(originalPrice, afterDiscountPrice,
                             nameInput.Value, descriptionInput.Value, DeadLineInput.Value, int.Parse(couponId));
                        if (result)
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                             "alert('Catalog Coupon updated succesfully');window.location ='bstart.aspx?userName=" + userName + "';", true);
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Could not update coupon')", true);
                        }
                    }
                }
            }
        }
    }
}