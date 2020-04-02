<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewOnlineDocument.aspx.vb" Inherits="DashBoard_ViewOnlineDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center"><iframe runat="server" id="PDFViwer" width="100%" height="665px" scrolling="auto"></iframe>
                </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
