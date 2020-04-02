<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTextFile.aspx.vb" Inherits="frmTextFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="100%">
     <tr><td class="pageTitle">Generate Text File</td></tr>
     <tr><td><asp:Button ID="btnExport" runat="server" Text="Generate Text file" CssClass="buttonStyle" width="150px" /></td></tr>
     <tr id="link" runat="Server"><td><asp:LinkButton ID="lnkDownload" runat="Server" Text="Click here"></asp:LinkButton>
         to download the file</td></tr>
     </table> 
        
        </div>
    </form>
</body>
</html>
