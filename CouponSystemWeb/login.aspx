<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CouponSystemWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CouponSystem Login</title>
    <link rel="shortcut icon" href="../favicon.ico" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/animate-custom.css" />
    <script type="text/javascript" src="js/jquery-2.1.3.js"></script>
    <script type="text/javascript" src="js/regScripts.js"></script>
    <script>
        $(document).ready(function () {
            $("#selectType").change(function () {
                var value = $(this).val();
                if (value == "Customer") {
                    document.getElementById("additional_info").style.display = '';
                }
                else {
                    document.getElementById("additional_info").style.display = 'none';
                }

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <section>
                <div id="container_demo">
                    <a class="hiddenanchor" id="toregister"></a>
                    <a class="hiddenanchor" id="tologin"></a>
                    <div id="wrapper">
                        <div id="login" class="animate form">
                            <h1>Log in</h1>
                            <p>
                                <label for="username" class="uname" data-icon="u">Your username </label>
                                <input id="username" runat="server" name="username" type="text" placeholder="enter username" />
                            </p>
                            <p>
                                <label for="password" class="youpasswd" data-icon="p">Your password </label>
                                <input id="password" name="password" runat="server"  type="password" placeholder="enter password" />
                            </p>
                            <p class="login button">
                                <input type="submit" id="LoginButton" runat="server" value="Log In" onserverclick="LoginButton_ServerClick" />
                            </p>
                            <p class="change_link">
                                Not a member yet ?
									<a href="#toregister" class="to_register">Join us</a>
                            </p>
                        </div>

                        <div id="register" class="animate form">
                            <h1>Sign up </h1>
                            <p>
                                <label for="fullNamesignup" class="uname" data-icon="u">Your full name</label>
                                <input id="fullNamesignup" runat="server" type="text" placeholder="enter your full name" />
                            </p>
                            <p>
                                <label for="usernamesignup" class="uname" data-icon="u">Your username</label>
                                <input id="UserNamesignup" runat="server" name="usernamesignup"  type="text" placeholder="enter a username" />
                            </p>
                            <p>
                                <label for="emailsignup" class="youmail" data-icon="e">Your email</label>
                                <input id="emailsignup" runat="server" name="emailsignup" type="email" placeholder="enter your email" />
                            </p>
                            <p>
                                <label for="passwordsignup" class="youpasswd" data-icon="p">Your password </label>
                                <input id="passwordsignup" runat="server" name="passwordsignup" type="password" placeholder="enter your passeord" />
                            </p>
                            <p>
                                <label for="passwordsignup_confirm" class="youpasswd" data-icon="p">Please confirm your password </label>
                                <input id="ConfirmPassword" runat="server" name="passwordsignup_confirm" type="password" placeholder="confirm your passeord" />
                            </p>
                            <p>
                                <label for="phonsignup">Your phone number</label>
                                <input id="phonsignup" name="phonsignup" runat="server"  type="Text" placeholder="enter your phone number" />
                            </p>
                            <p>
                                <label id="typeLbl" runat="server">User type:</label>
                                <select id="selectType" runat="server">
                                    <option value="BusinessOwner">Business Owner</option>
                                    <option value="Customer">Customer</option>
                                </select>
                            </p>
                            <div id="additional_info" style="display: none;" runat="server">
                                <p>
                                    <label id="ageLbl" runat="server">Your age:</label>
                                    <input id="ageInput" type="number" runat="server" placeholder="enter your age" min="12" max="70" />

                                </p>
                                <p>
                                    <label id="genderLabel" runat="server">Select Gender:</label>
                                    <select id="selectGender" runat="server">
                                        <option value="male">Male</option>
                                        <option value="female">Female</option>
                                    </select>
                                </p>
                                <p>
                                    <label id="catLbl" runat="server">Select Intrests:</label>
                                    <asp:CheckBoxList ID="categoriesLst" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one category."
                                        ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
                                </p>
                            </div>

                            <p>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="passwordsignup" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."></asp:CompareValidator>
                            </p>
                            <p class="signin button">
                                <input type="submit" value="sigh in!" runat="server" onserverclick="registerBtn_Click" />
                            </p>
                            <p class="change_link">
                                Already a member ?
									<a href="#tologin" class="to_register">Go and log in </a>
                            </p>
                        </div>

                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
