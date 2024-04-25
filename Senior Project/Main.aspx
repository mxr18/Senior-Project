<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Senior_Project.Main" Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Books</title>

    <link href="~/Main.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <nav>
            <asp:Button ID="btnLogout" CssClass="button-logout" runat="server" Text="LogOut" OnClick="btnLogout_Click"/>
        </nav>

        <div class="container">
            <div class="title">
                <h1>Mo's Library</h1>
                <asp:Label ID="lblGreeting" runat="server" Text="" ForeColor="Black" Visible="true"></asp:Label>
            </div>

            <div class="form">
                <input type="text" id="searchBox" runat="server"/>
                <asp:Button ID="btnSearch" CssClass="button-general" runat="server" Text="Search" OnClick="btnSearch_Click"/>
            </div>

            <div id="litResultsWrapper">
                <asp:Literal ID="litResults" runat="server"/>
            </div>
        </div>
    </form>
</body>
</html>
