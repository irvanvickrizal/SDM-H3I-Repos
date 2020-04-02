<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeProfile.aspx.vb"
    Inherits="USR_frmChangeProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Profile</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

</head>

<script language="javascript" type="text/javascript">
        function checkIsEmpty(){
            var msg = "";
            if (IsEmptyCheck(document.getElementById("txtName").value) == false) {
                msg = msg + "Name should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtEpm").value) == false) {
                msg = msg + "EpmId should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtEmail").value) == false) {
                msg = msg + "Email should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtEmail").value) == true) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var address = document.getElementById("txtEmail").value;
                if (reg.test(address) == false) {
                    msg = msg + "Invalid Email Address\n";
                }
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }
</script>

<body>
    <form id="form1" runat="server">
        <div style="width: 100%" id="divWidth">
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd">
                        User Details
                    </td>
                </tr>
                <tr id="lblUsertype" runat="Server">
                    <td class="lblTitle" style="width: 20%">
                        User Type
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="lblUser" runat="Server" colspan="2" class="lblText">
                    </td>
                </tr>
                <tr id="trUserTypeName" runat="Server">
                    <td class="lblTitle" style="width: 20%">
                        User Type Name
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="lblUserTypeDesc" runat="Server" class="lblText" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Name <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2">
                        <input id="txtName" runat="server" class="textFieldStyle" maxlength="30" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        EPM Id <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2">
                        <input id="txtEpm" runat="server" class="textFieldStyle" maxlength="50" type="text" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Login ID
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="lblLogin" runat="Server" colspan="2" class="lblText">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Role
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="lblRole" runat="Server" colspan="2" class="lblText">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        E-mail <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2">
                        <input id="txtEmail" runat="server" class="textFieldStyle" maxlength="50" type="text"
                            size="50" onkeypress="javascript:return allowKeyAcceptsData('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@_');" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Phone Number
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2">
                        <input id="txtPhnumber" runat="server" class="textFieldStyle" type="text" maxlength="20"
                            onkeypress="javascript:return allowKeyAcceptsData('0123456789-');" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Signature Title
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2">
                        <input id="txtSignTitle" runat="server" class="textFieldStyle" maxlength="50" type="text"
                            size="50" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td colspan="2">
                        <br />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="buttonStyle" Text="Update" /><asp:Button
                            ID="btnCancel" runat="server" CssClass="buttonStyle" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
                if (screen.width == 1440) {
                    document.getElementById("divWidth").style.width = screen.width - (232 + 416);
                }
                else 
                    if (screen.width == 1024) {
                        document.getElementById("divWidth").style.width = screen.width - (257 + 64);
                    }
                    else {
                        document.getElementById("divWidth").style.width = screen.width - 230;
                    }
        </script>

    </form>
</body>
</html>
