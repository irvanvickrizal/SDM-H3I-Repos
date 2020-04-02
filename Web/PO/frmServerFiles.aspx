<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmServerFiles.aspx.vb"
    Inherits="PO_frmServerFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Uploaded File</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Search Work Package ID:
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Width="166px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle"></asp:Button><br />
            <asp:ListBox ID="lstFiles" runat="server" Font-Names="Verdana" Height="550px" Width="580px">
            </asp:ListBox><br />
            <asp:Button ID="btnOK" runat="server" CssClass="buttonStyle" Text="Select" />
        </div>
    </form>
</body>
</html>
