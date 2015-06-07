<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="astart.aspx.cs" Inherits="CouponSystemWeb.astart" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Owner</title>
    <script type="text/javascript" src="js/jquery-2.1.3.js"></script>
    <script>
        function ConfirmApprove(catName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Approve coupon: " + catName + " ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function ConfirmApproveBusiness(busiName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Approve Business: " + busiName + " ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }


        function ConfirmRemoveCatalog(catName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_value";
            if (confirm("Are you sure you want to delete the Coupon: " + catName + "?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function ConfirmRemoveBusiness(BusiName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_value";
            if (confirm("Are you sure you want to delete the Business: " + BusiName + "?")) {
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
                <li class='active' runat="server" id="approveCoupons"><a runat="server" onserverclick="approveCoupons_onClick"><span>Approve Coupons</span></a></li>
                <li id="approveBusiness" runat="server"><a runat="server" onserverclick="approveBysiness_onClick"><span>Approve Business</span></a></li>
                <li id="addAdmin" runat="server"><a runat="server" onserverclick="addAdmin_onClick"><span>Add System Adminstrator</span></a></li>
                <li id="RemoveBuisness" runat="server"><a runat="server" onserverclick="allBuisnessDiv_Click"><span>Remove Business</span></a></li>
                <li id="addCategoryLi" runat="server"><a runat="server" onserverclick="adCategoryDiv_Click"><span>Add Category</span></a></li>
                <li>
                    <asp:HyperLink ID="logoutLink" NavigateUrl="login.aspx" Text="Logout" runat="server" />
                </li>
            </ul>
        </div>


        <%--   Approve Coupons Section   --%>


        <div id="approveCouponsDiv" class="datagrid" runat="server" visible="false">
            <center>
            <asp:Button runat="server" ID="AddSocialBtn" OnClick="AddSocialBtn_Click"
                Text="Add Social Coupon" />
                </center>
            <br />
            <asp:Table ID="approveCouponsTable" runat="server" />
        </div>

        <%--   Approve Business Section  --%>


        <div id="approveBusinessDiv" class="datagrid" runat="server" visible="false">
            <asp:Table ID="aapproveBusiness" runat="server" />
        </div>


        <%--    Add Admin Section  --%>


        <div id="addAdminDiv" class="datagrid" runat="server" visible="false">
            <table>
                <tr>
                    <td align="center" colspan="2">Add System Adminstrator</td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="userLabel" runat="server" AssociatedControlID="userNameAdmin">User Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="userNameAdmin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="passlbl" runat="server" AssociatedControlID="PasswordAdmin">Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="PasswordAdmin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="emailLbl" runat="server" AssociatedControlID="Email">Email:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="fullLbl" runat="server" AssociatedControlID="FullName">Full Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="FullName" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="phoneLbl" runat="server" AssociatedControlID="Phone">Phone:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Phone" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="addAdminBtn" runat="server" Text="Add Admin" OnClick="addAdminBtn_onCLick" />
                    </td>
                </tr>
            </table>
        </div>


        <%--    Remove Business Section  --%>
        <div id="allBusinessDiv" class="datagrid" runat="server" visible="false">
            <asp:Table ID="AllbusinessTable" runat="server" />
        </div>

        <%--    Add Category Section  --%>
        <div id="addCategoryDiv" class="datagrid" runat="server" visible="false">
            <center>
            <asp:TextBox ID="newCategoryTxt" runat="server" placeHolder="enter new category"></asp:TextBox><br />
            <asp:Button runat="server" OnClick="addCategory_ServerClick" Text="Add Category" />
            </center>
        </div>

    </form>
</body>
</html>
