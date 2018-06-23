<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakeInProcess.aspx.cs" Inherits="GBIplantWeb.TakeInProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Executor   "></asp:Label>
            <asp:DropDownList ID="DropDownExecutors" runat="server">
            </asp:DropDownList>
            <div>
            </div>
        </div>
        <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" />
        <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" />
    </form>
</body>
</html>
