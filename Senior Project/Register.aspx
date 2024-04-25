<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Senior_Project.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>

    <link href="~/Register.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="title">
                <h1>Mo's Library</h1>
            </div>

            <div class="form">
                <label for="txtFirstName">First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server"/><br/>

                <label for="txtLastName">Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server"/><br/>

                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"/><br/>

                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"/><br/>

                <label for="ddlQuestion">Security Question:</label>
                <asp:DropDownList ID="ddlQuestion" runat="server"></asp:DropDownList>
                <asp:TextBox ID="txtAnswer" runat="server" placeholder="Answer"/><br/>
            </div>

            <div class="button">
                <asp:Button ID="btnAdd" runat="server" Text="Add Account" OnClick="btnAdd_Click" CssClass="add-button"/>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Black" Visible="true"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
