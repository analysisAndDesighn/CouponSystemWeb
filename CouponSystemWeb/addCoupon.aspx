<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addCoupon.aspx.cs" Inherits="CouponSystemWeb.addCoupon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add\Update Coupon</title>
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <form id="addCouponform" runat="server" class="form-style-5">
    <div>
    <table>
            <tr>
                <td>
                    <label>Name:</label>
                </td>
                <td>
                    <input id="nameInput" type="text" runat="server" placeholder="enter Name" required="required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Description:</label>
                </td>
                <td>
                    <input id="descriptionInput" type="text" runat="server" placeholder="enter Description"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>DeadLine for use:</label>
                </td>
                <td>
                    <input id="DeadLineInput" type="date" min="" runat="server" required="required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Original Price:</label>
                </td>
                <td>
                    <input id="OriginalPriceInput" type="number" runat="server" min="0"  placeholder="enter Original Price" required="required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label>Price After Discount:</label>
                </td>
                <td>
                    <input id="discountInput" type="number" runat="server" min="0" placeholder="enter Discount" required="required"/>
                </td>
            </tr>
        <tr id="buisnessRow" runat="server" visible="false">
                <td>
                    <label>Buisness:</label>
                </td>
                <td>
                     <select id="buisSelect" runat="server">

                      </select>
                </td>
            </tr>
            <tr id="webSiteUpdate" runat="server" visible="false">
                <td>
                    <label>WebSitet:</label>
                </td>
                <td>
                    <input id="webSiteInput" type="url" runat="server" placeholder="enter website"/>
                </td>
            </tr>
    </table>
        <asp:Button ID="eventButton" runat="server" Text="Add Coupone" OnClick="button_Click" />
    </div>
    </form>
</body>
</html>
