<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProgramLicense.aspx.vb"
    Inherits="frmProgramLicense" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="3">
                    <b>eBAST Uploader Installation File<br />
                    </b>
                </td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <br />
                    1. Please download and install the Easy PDF Installer <a href="http://telkomsel.www.telkomsel.nsnebast.com/download/easypdfsdk60.zip">
                        here</a><br />
                    <br />
                    2. Please download and configure the Easy PDF License
                    <asp:Button ID="btnLicense" runat="server" CssClass="buttonStyle" Text="Here" /><br />
                    <br />
                    3. Please download and install the eBAST Uploader <a href="http://telkomsel.www.telkomsel.nsnebast.com/download/E-BastUpload-Latest.zip">
                        here</a><br />
                </td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <strong>
                        <br />
                        Extra Component<br />
                    </strong>
                </td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <br />
                    1. Please download and install .Net Framework 2.0 <a href="http://telkomsel.www.telkomsel.nsnebast.com/download/dotnetfx20.zip">
                        here</a><br />
                </td>
            </tr>
            <tr>
                <td style="height: 17px">
                    <br />
                </td>
            </tr>
            <tr>
                <td id="tdLicense1" runat="server" style="height: 17px" visible="false">
                    Please copy the Easy PDF license no below to Word Doc for backup.
                </td>
            </tr>
            <tr>
                <td id="tdLicense2" runat="server" style="height: 17px" visible="false">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
