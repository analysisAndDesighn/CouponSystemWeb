<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cstart.aspx.cs" Inherits="CouponSystemWeb.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer page</title>
    <script type="text/javascript" src="js/jquery-2.1.3.js"></script>
    <script type="text/javascript" src="js/cstartScripts.js"></script>
    <script>
        function Confirm(couponName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to buy the coupone: " + couponName + "?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function ConfirmRank(inputId) {
            var rank_input = document.getElementById(inputId);
            if (rank_input.value === "") {
                alert('Must enter a rank');
            }
            else if (rank_input.value < 0 || rank_input.value > 5) {
                alert('Rank must be between 1 and 5');
            }
            else {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirmRank_value";
                if (confirm("Are you sure you want to submit the rank " + rank_input.value + "?")) {
                    confirm_value.value = "Yes";
                    var rank = document.createElement("INPUT");
                    rank.type = "hidden";
                    rank.name = "rank_value";
                    rank.value = rank_input.value;
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
                document.forms[0].appendChild(rank);
            }

        }

        function ConfirmRemove(couponName) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_remove_value";
            if (confirm("Are you sure you want to delete the coupone: " + couponName + "?")) {
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
</head>
<body>
    <div id='cssmenu'>
        <ul>

            <li class='active' runat="server" id="searchSection"><a runat="server" onserverclick="searchDiv_onClick"><span>Search Coupons</span></a></li>
            <li id="purchasedSection" runat="server"><a runat="server" onserverclick="purchased_onClick"><span>Purchased Coupons</span></a></li>
            <li id="usedSection" runat="server"><a runat="server" onserverclick="used_onClick"><span>Used Coupons</span></a></li>
            <li id="socialSection" runat="server"><a runat="server" onserverclick="socialSection_onClick"><span>Social Coupons</span></a></li>
            <li id="profileSection" runat="server"><a runat="server" onserverclick="profile_onClick"><span>Profile</span></a></li>
            <li>
                <asp:HyperLink ID="logoutLink" NavigateUrl="login.aspx" Text="Logout" runat="server" />
            </li>
        </ul>

    </div>
    <form id="customerForm" runat="server" class="form-style-5">
        <div id="searchDiv" runat="server">
            <p>
                <input id="BuisnessCbx" type="checkbox" runat="server" title="Search by Buisness" /><label>Search by Buisness</label>
            </p>
            <div id="buisNameDiv" style="display: none;" runat="server">

                <label>Buisness Name:</label>
                <input id="BuisName" runat="server" type="text" />
            </div>
            <p>
                <input id="CategoryCbx" type="checkbox" runat="server" />
                <label>Search by Category</label>
            </p>
            <div id="CategoryOptions" style="display: none;">
                <asp:CheckBoxList ID="categoriesLst" runat="server">
                </asp:CheckBoxList>
            </div>
            <p>
                <input id="LocationCbx" type="checkbox" runat="server" />
                <label>Search by Location</label>
            </p>

            <div id="locationOptions" style="display: none;">
                <p>
                    <input id="city" type="radio" name="location" runat="server" />
                    <label>Search by city</label>
                    <input id="gps" type="radio" name="location" runat="server" />
                    <label>Search by GPS</label>
                </p>
            </div>


            <div id="CitySelection" style="display: none;">
                <p>
                    <select id="SelectCity" runat="server">
                        <option>Tel Aviv</option>
                        <option>Beer-Sheva</option>
                        <option>Ashkelon</option>
                    </select>
                </p>
            </div>
            <p>
                <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="searchBtn_Click" />
            </p>
            <br />
            <div class="datagrid" id="searchTableDive" runat="server" visible="false">
                <asp:Table ID="searchCouponTbl" runat="server" />
            </div>
        </div>


        <%---------------Purchased Coupon Screen ----------------%>
        <div id="purchasedDiv" class="datagrid" runat="server" visible="false">
            <asp:Table ID="purchasedTable" runat="server" />
        </div>

        <%---------------used Coupon Screen ----------------%>

        <div id="usedCouponDiv" class="datagrid" runat="server" visible="false">
            <asp:Table ID="usedCouponsTable" runat="server" />
        </div>

        <%---------------Social Coupon Screen ----------------%>

        <div id="socialCouponDiv" class="datagrid" runat="server" visible="false">
            <p>
                <asp:Button ID="addCouponButton" runat="server" Text="Add New Social Coupon" OnClick="addCouponButton_Click" />
            </p>
            <asp:Table ID="socialCouponTable" runat="server" />
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
                    </td>
                </tr>
                <tr id="age" runat="server">
                    <td align="right">
                        <asp:Label ID="ageLabel1" runat="server">age:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ageTextBox" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="ageValidator" ControlToValidate="ageTextBox" runat="server" MinimumValue="12" MaximumValue="70" Type="Integer" EnableClientScript="false" Text="The value must be from 1 to 10!"></asp:RangeValidator>

                    </td>
                </tr>
                <tr id="gender" runat="server">
                    <td align="right">
                        <asp:Label ID="genderLabel" runat="server">Select Gender:</asp:Label>
                    </td>
                    <td>
                        <input id="femaleRadio" type="radio" name="gender" runat="server" /><label>Female </label>
                        <input id="maleRadio" type="radio" name="gender" runat="server" /><label>Male</label>

                    </td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td align="right">
                        <asp:Label ID="Label1" runat="server">Choose youre interests:</asp:Label>
                    </td>
                    <td>
                        <asp:CheckBoxList ID="catList" runat="server">
                        </asp:CheckBoxList>
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
                        <asp:Button ID="updateBtn" runat="server" Text="Update" OnClick="update_onClick" />
                    </td>
                </tr>
            </table>

        </div>

    </form>

</body>
</html>
