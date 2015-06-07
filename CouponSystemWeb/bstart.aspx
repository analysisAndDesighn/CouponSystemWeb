<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bstart.aspx.cs" Inherits="CouponSystemWeb.bstart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Owner</title>
    <script type="text/javascript" src="js/jquery-2.1.3.js"></script>
    <%--    <script type="text/javascript" src="js/cstartScripts.js"></script>--%>
    <script>
        function ConfirmRemove(buisName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_value";
            if (confirm("Are you sure you want to delete the Business: " + buisName + "?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ConfirmRemoveCatalog(catName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_catalog_value";
            if (confirm("Are you sure you want to delete the Coupon: " + catName + "?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ConfirmRemoveSocial(catName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_social_value";
            if (confirm("Are you sure you want to delete the Coupon: " + catName + "?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <link href="css/MenuStyles.css" rel="stylesheet" type="text/css" />
    <link href="css/TableStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }

        .auto-style2 {
            height: 31px;
        }
    </style>
</head>
<body>
    <form id="buisform" runat="server" class="form-style-5">
        <div id='cssmenu'>
            <ul>
                <li class='active' runat="server" id="myBusinessSection"><a runat="server" onserverclick="myBusiness_onClick"><span>My Business</span></a></li>
                <li id="addBusinessSection" runat="server"><a runat="server" onserverclick="addBusiness_onClick"><span>Add Business</span></a></li>
                <li id="MyCouponsSection" runat="server"><a runat="server" onserverclick="myCoupons_onClick"><span>My Coupons</span></a></li>
                <li id="ActivateCouponsSection" runat="server"><a runat="server" onserverclick="activateCoupons_onClick"><span>Activate Coupon</span></a></li>
                <li id="profileSection" runat="server"><a runat="server" onserverclick="businessProfile_onClick"><span>Profile</span></a></li>
                <li>
                    <asp:HyperLink ID="logoutLink" NavigateUrl="login.aspx" Text="Logout" runat="server" />
                </li>
                <%-- <li id="socialSection" runat="server"><a href='#'><span>My Profile</span></a></li>--%>
            </ul>
        </div>
        <%--       My Buisness Section--%>
        <div id="myBusinessDiv" class="datagrid" runat="server" visible="false">
            <asp:Table ID="myBusinessTable" runat="server" />
        </div>

        <%--      Add Buisness Section--%>

        <div id="addBusinessDiv" class="datagrid" runat="server" visible="false">
            <table>
                <tr>
                    <td align="center" colspan="2">Add New Business</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="busiNameLabel" runat="server" AssociatedControlID="BusinessName">Business Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="BusinessName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="adressLabel" runat="server" AssociatedControlID="Adress">Adress:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Adress" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="cityLabel" runat="server" AssociatedControlID="City">City:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="City" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="DescriptionLabel" runat="server" AssociatedControlID="Description">Description:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Description" runat="server"></asp:TextBox>

                    </td>
                </tr>

                <tr id="Tr1" runat="server">
                    <td align="right" class="auto-style2">
                        <asp:Label ID="Label1" runat="server">Choose Business Category:</asp:Label>
                    </td>
                    <td class="auto-style2">
                        <select id="CategorySelect" runat="server">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="addBusinessBtn" runat="server" Text="Add Business" OnClick="addBusinesToSystem_onCLick" />
                    </td>
                </tr>
            </table>


        </div>


        <%--UpdateBusinessSection--%>

        <div id="updateBusinessDiv" class="datagrid" runat="server" visible="false">
            <table>
                <tr>
                    <td align="center" colspan="2">Update Business</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="busislbl" runat="server" AssociatedControlID="BusiName">Business Name:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="BusiName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="adresslbl" runat="server" AssociatedControlID="Adress1">Adress:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Adress1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="citylbl" runat="server" AssociatedControlID="City1">City:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="City1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Descriptionlbl" runat="server" AssociatedControlID="Description1">Description:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Description1" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td align="right" class="auto-style2">
                        <asp:Label ID="lbl1" runat="server">Choose Business Category:</asp:Label>
                    </td>
                    <td class="auto-style2">
                        <select id="CategoryUpdate" runat="server">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <tr>
                            <td align="right">
                                <asp:Button ID="updateBusinessBtn" runat="server" Text="Update Business" OnClick="updateBusinesToSystem_onCLick" />
                            </td>
                        </tr>
                    </td>
                </tr>
            </table>


        </div>

        <%--My Coupons Section--%>


        <div id="MyCouponsDiv" class="datagrid" runat="server" visible="false">
            <p>
                <center>
                <asp:Button ID="catalogViewBtn" runat="server" Text="Watch Catalog Coupons" OnClick="myCatalogCoupons_onClick" />
                <asp:Button ID="socialViewBtn" runat="server" Text="Watch Social Coupons" OnClick="mySocialCoupons_onClick" />
            </center>
            </p>
            <%--catalog coupon div--%>
            <div id="catalogCouponsDiv" runat="server" visible="false">
                <br />
                <p>
                    <center>
                    <asp:DropDownList ID="selectBusiness" runat="server" AutoPostBack="True" 
                         OnSelectedIndexChanged="showSelected_Click">

                    </asp:DropDownList>
                    
                    <asp:Button ID="addCouponButton" runat="server" Text="Add New Catalog Coupon" OnClick="addCouponButton_Click" />
                    </center>
                </p>
                <asp:HiddenField runat="server" ID="numberOfControls" Value="0" />
                <p>
                    <asp:Table ID="CatalogCouponTable" runat="server" />
                </p>
            </div>

            <%--social coupon div--%>
            <div id="socialCouponDiv" runat="server" visible="false">
                <p>
                    <center>
                    <asp:Button ID="addSocialButton" runat="server" Text="Add New Social Coupon" OnClick="addSocialCouponButton_Click" />
                    </center>
                </p>
                <p>
                    <asp:Table ID="socialCouponTable" runat="server" />
                </p>
            </div>

        </div>

        <%--My  Social Coupons Section--%>


        <%--Activate Coupon Screen--%>

        <div id="ActivateCouponsDiv" class="datagrid" runat="server" visible="false">

            <table>
                <tr>
                    <td align="center" colspan="2">Insert Serial Key To Activate Coupon</td>
                </tr>
                <tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Serial" runat="server" AssociatedControlID="serialBox">Serial Key:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="serialBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <td align="left">
                        <asp:Button ID="activateBtn" runat="server" Text="Activate" OnClick="activate_onClick" />
                    </td>
                </tr>
            </table>
        </div>

        <%---------------Update Profile Screen ----------------%>


        <div id="profileDiv" class="datagrid" runat="server" visible="false">
            <table>
                <tr>
                    <td align="center" colspan="2">Update Profile</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="UserName" runat="server"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ConfirmPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email">E-mail is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelName" runat="server" AssociatedControlID="FullName">Full Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="FullName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="FullNameValidator" runat="server" ControlToValidate="FullName">Name is required.</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Phone" runat="server" AssociatedControlID="TextBoxPhone">Phone:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPhone" ErrorMessage="Phone is required." ToolTip="Phone is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="color: Red;">
                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="businessUpdate_onClick" />
                    </td>
                </tr>
            </table>

        </div>






    </form>
</body>
</html>
