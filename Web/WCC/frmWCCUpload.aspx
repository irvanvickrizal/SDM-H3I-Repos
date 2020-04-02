<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCCUpload.aspx.vb" Inherits="frmWCCUpload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>WCC Upload</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <br />
      <table width="100%">
            <tr class="pageTitle">
                <td colspan="2">
                    WCC Upload</td>
            </tr>
          <tr valign="top">
              <td colspan="2">
                  <asp:FileUpload ID="POUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnview"
                      runat="server" CssClass="buttonStyle" Text="Upload" Width="71px" /></td>
          </tr>
            <tr valign="top">
                <td colspan="2">
                    </td>                
            </tr>
            <tr><td colspan="2" id="EPMcount" runat="server" style="width:100%"></td></tr>
        </table>
    </div>                
    </form>
</body>
</html>
