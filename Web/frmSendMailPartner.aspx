<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSendMailPartner.aspx.vb" Inherits="frmSendMailPartner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="BtnSendMail" runat="server" Text="Send Partner Invitation" />
        <asp:Label ID="LblSendCursor" runat="server">-</asp:Label><asp:Label ID="LblTotalSend" runat="server"></asp:Label>
        <asp:Label ID="LblSuccess" runat="server" Text="All of Partner already Email notification for eBAST account"></asp:Label>
    </div>
    </form>
</body>
</html>
