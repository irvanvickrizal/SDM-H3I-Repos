<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOUploadNew.aspx.vb" Inherits="frmPOUploadNew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PO Upload</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp;</div>                
      <table width="100%">
            <tr class="pageTitle">
                <td colspan="2">
                    EPM Upload</td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                    <asp:FileUpload ID="EPMUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnEview" runat="server" CssClass="buttonStyle" Text="Upload" Width="70px"/></td>                
            </tr>
            <tr><td colspan="2" id="EPMcount" runat="server" style="width:100%"></td></tr>
        </table>
    </form>
</body>
</html>
