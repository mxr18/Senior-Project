<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Security.aspx.cs" Inherits="Senior_Project.Security" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Security</title>

    <link href="~/Security.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="title">
                    <h1>Mo's Library</h1>
                </div>

                <div class="form">
                    <label for="txtEmail">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server"/><br/>
                </div>

                <div class="button">
                    <asp:Button ID="btnVerify" runat="server" Text="Verify Identity" OnClick="btnVerify_Click" CssClass="verify-button"/>
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Black" Visible="true"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
