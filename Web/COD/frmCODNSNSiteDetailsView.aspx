<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODNSNSiteDetailsView.aspx.vb" Inherits="COD_frmCODNSNSiteDetailsView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblCountry" runat="server" border="0" cellpadding="2" cellspacing="2" width="100%" >
        <tr>
            <td class="pageTitle" colspan="3">Site Details</td>
        </tr>        
          <tr>
            <td class="lblTitle">
                Site No</td>
            <td style="width: 1%">
                :</td>
            <td>
                <asp:Label ID="lblSiteNo" runat="server" CssClass="selectField"></asp:Label></td>
        </tr>
             
                <tr>
                    <td class="lblTitle" style="width:20%">Site Name</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width:20%">
                        Supervisor</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblSiteDesc" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width:20%">
                        Java</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width:20%">
                        Area</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width:20%">
                        Region</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width:20%">
                        Zone</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                    <td>
                        <asp:Label ID="lblCity" runat="server" CssClass="lblText"></asp:Label></td>
                </tr>
                
        <tr>
            <td>
                </td>
            <td style="width: 1%; height: 20px;">
            </td>
            <td style="height: 20px">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="lblTitle">
            </td>
            <td style="width: 1%; height: 20px;">
                &nbsp;</td>
            <td style="height: 20px">
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="buttonStyle" /></td>
        </tr>
        <tr>
            <td class="lblTitle">
            </td>
            <td style="width: 1%">
            </td>
            <td>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
