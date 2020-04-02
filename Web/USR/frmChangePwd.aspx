<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangePwd.aspx.vb" Inherits="USR_frmChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>   
<script language="javascript" type="text/javascript">
    function checkIsEmpty()
    {
        var msg='';
        if (IsEmptyCheck(document.getElementById('txtPwd').value) == false)
        {
            msg = msg + "Current Password should not be Empty\n"
        }
        if (IsEmptyCheck(document.getElementById("txtNpwd").value) == false)
        {
            msg = msg + "New Password should not be Empty\n"
        }
        if (IsEmptyCheck(document.getElementById("txtRPwd").value) == false)
        {
            msg = msg + "Retype-Password should not be Empty\n"
        }
        if (IsEmptyCheck(document.getElementById("txtAns").value) == false)
        {
            msg = msg + "Answer should not be Empty\n"
        }
        if (document.getElementById("txtNpwd").value !='' && document.getElementById("txtRpwd").value != '')
        {
            if (document.getElementById("txtNpwd").value != document.getElementById("txtRpwd").value)    
            {
                msg = msg + "Re-Password mismatched\n"
            }
        }
        if (msg != '')
        {
            alert("Mandatory field information : \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }
    }
</script>        
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="1" cellspacing="1">
        <tr class="pageTitle"><td colspan="3">Change Password</td></tr>
        <tr><td class="lblTitle">Current Password <font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td style="width:1%">:</td><td><input id="txtPwd" type="password" runat="server" class="textFieldStyle" maxlength="20" /></td></tr>
        <tr><td class="lblTitle">New - Password <font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td><td><input id="txtNpwd" type="password" runat="server" class="textFieldStyle" maxlength="20" /></td></tr>
        <tr><td class="lblTitle">Retype - Password <font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td><td><input id="txtRPwd" type="password" runat="server" class="textFieldStyle" maxlength="20" /></td></tr>      
        <tr><td class="lblTitle">Security Question <font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td>
            <td><asp:DropDownList ID="ddlQA" runat="server" CssClass="selectFieldStyle">
                <asp:ListItem Value="0" Text="Favourite Pet Name"></asp:ListItem>
                <asp:ListItem Value="1" Text="Mother's Maiden Name"></asp:ListItem>
                <asp:ListItem Value="2" Text="Childhood Scholl"></asp:ListItem>
                <asp:ListItem Value="3" Text="Favourite Visiting Place"></asp:ListItem>
                </asp:DropDownList></td></tr>      
        <tr><td class="lblTitle">Answer <font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td><td><input id="txtAns" type="text" runat="server" class="textFieldStyle" maxlength="50" style="width: 312px" /></td></tr>      
        <tr><td colspan="2"></td><td><br /><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />&nbsp;<input type="button" runat="server" id="btnCancel" value="Cancel" class="buttonStyle" visible="false" /></td></tr>    
    </table>
   
    </form>
</body>
</html>
