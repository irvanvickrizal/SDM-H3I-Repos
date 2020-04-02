<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BrowserInfo.aspx.vb" Inherits="BrowserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Your browser is:
            <asp:Literal ID="ltlBrowserName" runat="server" />
            <p>
                <b><u>Here is your browser's information:</u></b><br />
                <asp:Literal runat="server" ID="ltlAllData" />
        </div>
    </form>
</body>
</html>
