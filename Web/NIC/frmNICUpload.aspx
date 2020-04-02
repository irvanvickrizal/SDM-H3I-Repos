<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNICUpload.aspx.vb" Inherits="MSD_frmNICUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr class="pageTitle">
                    <td colspan="2" style="width: 1159px">
                        NIC Upload</td>
                </tr>
                <tr valign="top">
                    <td colspan="2" style="width: 1159px">
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="2" style="width: 1159px">
                        <asp:Label ID="lblMSG" runat="server" ForeColor="Blue" Text="Label" Visible="False"></asp:Label></td>
                </tr>
                <tr valign="top">
                    <td colspan="2" style="width: 1159px">
                    </td>
                </tr>
                <tr>
                    <td id="EPMcount" runat="server" colspan="2" style="width: 1159px">
                    </td>
                </tr>
                <tr>
                    <td id="Td1" runat="server" colspan="2" style="width: 1159px">
                        <asp:FileUpload ID="EPMUpload" runat="server" Width="518px" />
                        <asp:Button ID="btnSaveData" runat="server" CssClass="buttonStyle" Text="Upload"
                            Width="70px" /></td>
                </tr>
                <tr>
                    <td id="Td2" runat="server" colspan="2" style="width: 1159px">
                        <input id="hdnSLNO" runat="server" type="hidden" />
                        <input id="hdnFieldName" runat="server" type="hidden" /></td>
                </tr>
                <tr>
                    <td id="Td3" runat="server" colspan="2" style="width: 1159px">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
