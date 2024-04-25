<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Senior_Project.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <link href="~/Login.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="title">
                <h1>Mo's Library</h1>
            </div>

            <div class="form">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"/><br/>

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/><br/>
            </div>

            <div class="button">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="login-button"/>
                <asp:Button ID="btnAdd" runat="server" Text="Sign Up" OnClick="btnAdd_Click" CssClass="add-button"/>
                <asp:Button ID="btnPassword" runat="server" Text="Forgot Password?" OnClick="btnPassword_Click" CssClass="password-button"/>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Black" Visible="true"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
