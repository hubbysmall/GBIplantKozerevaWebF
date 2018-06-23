<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateZakaz.aspx.cs" Inherits="GBIplantWeb.CreateZakaz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Buyer"></asp:Label>
            <asp:DropDownList ID="DropDownBuyers" runat="server">
            </asp:DropDownList>
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="GBI"></asp:Label>
            <asp:DropDownList ID="DropDownGBIs" runat="server" OnSelectedIndexChanged="DropDownGBIs_SelectedIndexChanged">
            </asp:DropDownList>
        </p>
        <asp:Label ID="Label3" runat="server" Text="Quantity   "></asp:Label>
        <asp:TextBox ID="TextBoxQuantity" runat="server"></asp:TextBox>
        <p>
            &nbsp;</p>
        <p>
            <asp:Label ID="Label4" runat="server" Text="Total   "></asp:Label>
            <asp:TextBox ID="TextBoxTotal" runat="server" OnTextChanged="TextBoxTotal_TextChanged"></asp:TextBox>
            <asp:Button ID="ButtonTotal" runat="server" OnClick="ButtonTotal_Click" Text="Calculate" />
        </p>
        <p>
            <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" />
            <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" />
        </p>
    </form>
</body>
</html>
