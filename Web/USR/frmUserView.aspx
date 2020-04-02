<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserView.aspx.vb" Inherits="USR_frmUserView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="3" id="rowadd">User Details</td>
            </tr>
            <tr id="lblUsertype" runat="Server">
                <td class="lblTitle">User Type</td>
                <td style="width: 4px">:</td>
                <td id="lblUser" runat="Server"><asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="selectFieldStyle"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="lblTitle">Name</td>
                <td style="width: 4px">:</td>
                <td id="lblName" runat="Server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Login ID</td>
                <td style="width: 4px">:</td>
                <td id="lblLogin" runat="Server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Role</td>
                <td style="width: 4px">:</td>
                <td id="lblRole" runat="Server"><asp:DropDownList ID="ddlRole" runat="Server" CssClass="selectFieldStyle"></asp:DropDownList></td>
            </tr>  
            <tr>
                <td class="lblTitle">E-mail</td>
                <td style="width: 4px">:</td>
                <td id="lblEmail" runat="Server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Phone Number</td>
                <td style="width: 4px">:</td>
                <td id="lblPhone" runat="Server"></td>
            </tr>
        <tr id="trstatus" runat="Server">
            <td class="lblTitle">Status</td>
            <td style="width: 4px">:</td>
            <td id="lblStatus" runat="Server"></td>
        </tr>
        <tr>
            <td class="lblTitle">Account Status</td>
            <td style="width: 4px">:</td>
            <td id="lblAccstatus" runat="Server"></td>
        </tr>
        <tr id="trArea" runat="Server">
            <td class="lblTitle">Area</td>
            <td style="width: 4px">:</td>
            <td id="lblArea" runat="Server"></td>
        </tr>
        <tr id="trRegion" runat="Server">
            <td class="lblTitle">Region</td>
            <td style="width: 4px">:</td>
            <td id="lblRegion" runat="Server"></td>
        </tr>
        <tr id="trZone" runat="Server">
            <td class="lblTitle">Zone</td>
            <td style="width: 4px">:</td>
            <td id="lblZone" runat="Server">
            </td>
        </tr>
        <tr id="trSite" runat="Server">
            <td class="lblTitle">Site</td>
            <td style="width: 4px">:</td>
            <td id="lblSite" runat="Server"></td>
        </tr>
        <tr><td></td><td style="width: 4px"></td>
                <td> <br />
                    <input type="button" id="BtnCancel" runat="server" class="buttonStyle" value="Close" onclick="window.close();"/></td>
            </tr>
              
        </table>
    </div>
    </form>
</body>
</html>
