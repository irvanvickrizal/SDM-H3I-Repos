<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOUpload101130.aspx.vb" Inherits="frmPOUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PO Upload</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table width="100%">
                <tr class="pageTitle">
                    <td colspan="2">
                        Po Upload</td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="POUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnview"
                            runat="server" CssClass="buttonStyle" Text="Upload" Width="71px" /></td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td runat="server" id="PrCount">
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=1','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A1" class="ASmall">Missing Workpackage Id</a></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=2','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A2" class="ASmall">Configuration Error (Band1800
                        - Purchased Shows in 900)</a></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=3','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A3" class="ASmall">Configuration Error (Band900
                        - Purchased Shows in 1800)</a></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=4','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A4" class="ASmall">Dual Band - Qty MisMatch</a></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=5','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A5" class="ASmall">Configuration Error (config
                        shows 333+444, But Quantity is not matching with Config Total)</a></td>
            </tr>
            <tr>
                <td runat="server" id="DupSites">
                </td>
            </tr>
            <tr>
                <td runat="server" id="errrow">
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table width="100%">
            <tr class="pageTitle">
                <td colspan="2">
                    EPM Upload</td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                    <asp:FileUpload ID="EPMUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnEview"
                        runat="server" CssClass="buttonStyle" Text="Upload" Width="70px" />
                    <asp:Button ID="Button1" runat="server" CssClass="buttonStyle" Text="Backup" Width="70px" />
                    <asp:Button ID="Button2" runat="server" CssClass="buttonStyle" Text="Restore" Width="70px" /></td>
            </tr>
            <tr>
                <td colspan="2" id="EPMcount" runat="server" style="width: 100%">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <b>FTP Uploaded File Name:</b></td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <asp:TextBox ID="fileUpload1" Enabled="true" runat="server" EnableTheming="True"
                        Width="456px" />
                    <asp:Button ID="btnupload1" runat="server" Text="Process" CssClass="buttonStyle" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
