<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBI_comp.aspx.cs" Inherits="GBIplantWeb.GBI_comp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="sadName" runat="server" Width="120px">GBI component</asp:Label>
            <asp:DropDownList ID="DropDownListElement" runat="server" Height="16px" Width="200px">
            </asp:DropDownList>
            <asp:TextBox ID="selectedId" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Width="120px">Quantity</asp:Label>
            <asp:TextBox ID="TextBoxCount" runat="server" Width="194px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" />
            <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" />
        </div>
    </form>
</body>
</html>
