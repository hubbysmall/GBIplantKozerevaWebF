<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBIcomp.aspx.cs" Inherits="GBIplantWeb.GBIcomp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="GBIcomp name: "></asp:Label>
            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
            <asp:Button ID="save" runat="server" OnClick="save_Click" Text="save" />
            <asp:Button ID="cancel" runat="server" Text="cancel" OnClick="cancel_Click" />
        </div>
    </form>
</body>
</html>
