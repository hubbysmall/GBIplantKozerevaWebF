<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBIcomps.aspx.cs" Inherits="GBIplantWeb.GBIcomps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <asp:Button ID="add" runat="server" OnClick="add_Click" Text="add" />
        <asp:Button ID="delete" runat="server" Text="delete" OnClick="delete_Click" />
        <asp:Button ID="update" runat="server" Text="update" OnClick="update_Click" />
        <asp:Button ID="refresh" runat="server" Text="refresh" OnClick="refresh_Click" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="GBIingridientName" HeaderText="GBIingridientName" SortExpression="GBIingridientName" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="Home" runat="server" OnClick="Home_Click" Text="Home" />
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList" TypeName="GBIplantService.realizationOfInterfaces.GBIingridientsServiceList"></asp:ObjectDataSource>
    </form>
</body>
</html>
