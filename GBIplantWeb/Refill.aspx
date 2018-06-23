<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Refill.aspx.cs" Inherits="GBIplantWeb.Refill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="GBI component   "></asp:Label>
            <asp:DropDownList ID="DropDownComps" runat="server" AutoPostBack="True" Width="200px">
            </asp:DropDownList>
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Hall   "></asp:Label>
            <asp:DropDownList ID="DropDownHalls" runat="server" AutoPostBack="True" Width="200px">
            </asp:DropDownList>
        </p>
        <asp:Label ID="Label3" runat="server" Text="Quantity   "></asp:Label>
        <asp:TextBox ID="TextBoxQuantity" runat="server"></asp:TextBox>
        <p>
            <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" />
            <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" />
        </p>
    </form>
</body>
</html>
