<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="Senior_Project.Question" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question</title>

    <link href="~/Question.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="title">
                <h1>Mo's Library</h1>
            </div>

            <div class="form">
                <label for="lblQuestion">Question:</label>
                <asp:Label ID="lblQuestion" runat="server"/><br/>
                <label for="txtAnswer">Answer:</label>
                <asp:TextBox ID="txtAnswer" runat="server"/><br/>
            </div>

            <div class="button">
                <asp:Button ID="btnVerifyAnswer" runat="server" Text="Verify Answer" OnClick="btnVerifyAnswer_Click"/>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Black" Visible="true"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
