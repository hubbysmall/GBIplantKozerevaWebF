<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GBIplantWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body id="Executors">
    <form id="form1" runat="server">
        <asp:Button ID="Halls" runat="server" OnClick="Halls_Click" Text="Halls" />
        <asp:Button ID="Executors" runat="server" OnClick="Executors_Click" Text="Executors" />
        <asp:Button ID="GBIcomponents" runat="server" Text="GBI components" OnClick="GBIcomponents_Click" />
        <asp:Button ID="GBIs" runat="server" Text="GBIs" OnClick="GBIs_Click" />
        <asp:Button ID="Buyers" runat="server" Text="Buyers" OnClick="Buyers_Click" />
        <asp:Button ID="RefillHalls" runat="server" OnClick="RefillHalls_Click" Text="Refill halls" />
        <div>
        </div>
        <div>
            <asp:Button ID="ButtonCreateOrder" runat="server" OnClick="ButtonCreateOrder_Click" Text="Create GBI Order" />
            <asp:Button ID="ButtonTakeInPocess" runat="server" OnClick="ButtonTakeInPocess_Click" Text="Take In Pocess" />
            <asp:Button ID="ButtonOrderReady" runat="server" OnClick="ButtonOrderReady_Click" Text="Order Ready" />
            <asp:Button ID="ButtonOrderPaid" runat="server" OnClick="ButtonOrderPaid_Click" Text="Order Paid" />
            <asp:Button ID="ButtonRefresh" runat="server" OnClick="ButtonRefresh_Click" Text="Refresh" />
            <div>
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="BuyerId" HeaderText="BuyerId" SortExpression="BuyerId" />
                <asp:BoundField DataField="BuyerFIO" HeaderText="BuyerFIO" SortExpression="BuyerFIO" />
                <asp:BoundField DataField="GBIpieceOfArtId" HeaderText="GBIpieceOfArtId" SortExpression="GBIpieceOfArtId" />
                <asp:BoundField DataField="GBIpieceOfArtName" HeaderText="GBIpieceOfArtName" SortExpression="GBIpieceOfArtName" />
                <asp:BoundField DataField="ExecutorId" HeaderText="ExecutorId" SortExpression="ExecutorId" />
                <asp:BoundField DataField="ExecutorName" HeaderText="ExecutorName" SortExpression="ExecutorName" />
                <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                <asp:BoundField DataField="Sum" HeaderText="Sum" SortExpression="Sum" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="DateCreate" HeaderText="DateCreate" SortExpression="DateCreate" />
                <asp:BoundField DataField="DateExecute" HeaderText="DateExecute" SortExpression="DateExecute" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList" TypeName="GBIplantService.realizationDB.MainServiceDB"></asp:ObjectDataSource>
    </form>
</body>
</html>
