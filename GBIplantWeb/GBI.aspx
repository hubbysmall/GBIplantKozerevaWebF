<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBI.aspx.cs" Inherits="GBIplantWeb.GBI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div>
            <asp:Label ID="labelName" runat="server" Width="120px">GBI</asp:Label>
            <asp:TextBox ID="textBoxName" runat="server" Width="160px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Width="120px">Price</asp:Label>
            <asp:TextBox ID="textBoxPrice" runat="server" Width="54px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="add" runat="server" OnClick="add_Click" Text="add" />
            <asp:Button ID="update" runat="server" OnClick="update_Click" Text="update" />
            <asp:Button ID="delete" runat="server" OnClick="delete_Click" Text="delete" />
            <asp:Button ID="refresh" runat="server" OnClick="refresh_Click" Text="refresh" />
            <asp:GridView ID="dataGridView" runat="server">
                <Columns>
                    <asp:CommandField SelectText="&gt;&gt;" ShowSelectButton="true" />
                </Columns>
                <SelectedRowStyle BackColor="#CCCCCC" />
            </asp:GridView>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="GBIpieceofArtId" HeaderText="GBIpieceofArtId" SortExpression="GBIpieceofArtId" />
                    <asp:BoundField DataField="GBIingridientId" HeaderText="GBIingridientId" SortExpression="GBIingridientId" />
                    <asp:BoundField DataField="GBIingridientName" HeaderText="GBIingridientName" SortExpression="GBIingridientName" />
                    <asp:BoundField DataField="Count" HeaderText="Count" SortExpression="Count" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListOfComps" TypeName="GBIplantService.realizationOfInterfaces.GBIpieceOfArtServiceList">
                <SelectParameters>
                    <asp:SessionParameter Name="id" SessionField="id" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <asp:Button ID="save" runat="server" OnClick="ButtonSave_Click" Text="save" />
            <asp:Button ID="cancel" runat="server" OnClick="ButtonCancel_Click" Text="cancel" />
        </div>
    </form>
</body>
</html>
