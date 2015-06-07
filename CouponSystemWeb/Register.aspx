<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CouponSystemWeb.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <script type="text/javascript" src="js/jquery-2.1.3.js"></script>
    <script type="text/javascript" src="js/regScripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td align="center" colspan="2">Sign Up for Your New Account</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName">User Name is required.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password">Password is required.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword">Confirm Password is required.</asp:RequiredFieldValidator>
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
                <td align="right">
                    <asp:Label ID="userType" runat="server">Select Type:</asp:Label>
                </td>
                <td>
                    <input id="BuisOwnerRadio" type="radio" name="type" runat="server" required="required" /><label>Buisness owner </label>
                    <input id="customerRadio" type="radio" name="type" runat="server" required="required"/><label>Customer</label>
                </td>
            </tr>
            <tr id="age" style="display: none;" runat="server">
                <td align="right">
                    <asp:Label ID="ageLabel1" runat="server">age:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ageTextBox" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="ageValidator" ControlToValidate="ageTextBox" runat="server" MinimumValue="12" MaximumValue="70" Type="Integer" EnableClientScript="false" Text="The value must be from 1 to 10!"></asp:RangeValidator>

                </td>
            </tr>
            <tr id="gender" style="display: none;" runat="server">
                <td align="right">
                    <asp:Label ID="genderLabel" runat="server">Select Gender:</asp:Label>
                </td>
                <td>
                    <input id="femaleRadio" type="radio" name="gender" runat="server"  /><label>Female </label>
                    <input id="maleRadio" type="radio" name="gender" runat="server"  /><label>Male</label>

                </td>
            </tr>
                <tr id="CategoryOptions" style="display:none;" runat="server">
                <td align="right">
                    <asp:Label ID="Label1" runat="server">Choose youre interests:</asp:Label>
                </td>
                <td><asp:checkboxlist id="categoriesLst"  runat="server">
                  </asp:checkboxlist>
                  <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one category."
                  ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
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
        </table>

        <asp:Button ID="registerBtn" runat="server" Text="Register!" OnClick="registerBtn_Click" />
        <pre></pre>
        <button onclick="location.href = 'Login.aspx';" id="mainBtn" class="float-left submit-button"  >Back to main page</button>
    </form>
</body>
</html>
