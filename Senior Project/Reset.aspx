<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="Senior_Project.Reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset</title>

    <link href="~/Reset.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="title">
                    <h1>Mo's Library</h1>
                </div>
     
                <div class="form">
                    <label for="txtNewPassword">New Password:</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"/><br/>
                    <label for="txtConfirmPassword">Confirm Password:</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"/><br/>
                </div>

                <div class="button">
                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click"/>
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
