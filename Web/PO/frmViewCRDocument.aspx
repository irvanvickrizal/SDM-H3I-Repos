<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewCRDocument.aspx.vb" Inherits="PO_frmViewCRDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Document</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:100%;width:100%">
        <iframe runat="server" id="docView" width="100%" height="575px" scrolling="auto">
        </iframe>
    </div>
    <div>
        <asp:button ID="btnRefresh" runat="server" text="Refresh" />
    </div>
    </form>
</body>
</html>
