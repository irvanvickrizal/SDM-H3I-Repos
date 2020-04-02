<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAUser.aspx.vb" Inherits="CR_frmAUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Additional Users</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
     <script language="javascript" type="text/javascript">     
        function checkIsEmpty()
        {
         
            var msg="";           
            if (IsEmptyCheck(document.getElementById("txtAUsers").value) == true)
            {
                if (IsEmptyCheck(document.getElementById("txtAEmail").value) == false)
                {
                    msg = msg + "Additional Users Email should not be Empty\n"
                }
                else
                {  
                    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                    var address = document.getElementById('txtAEmail').value;
                    if(reg.test(address) == false) 
                    {
                        msg = msg + "Invalid Email Address\n";
                    }                          
                }
            }
            else
            {
                msg = msg + "Additional Users Name should not be Empty\n"
            }
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return accessConfirm();
            }          
        }
      </script>      
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="1" cellspacing="1" width="50%">
        <tr class="pageTitle"><td colspan="3">
            Additional Users</td></tr>
        <tr>
            <td class="lblTitle"> Users<font style="color:Red; font-size:16px"><sup> </sup></font></td>
            <td style="width: 1%">:</td>
            <td><input type="text" id="txtAUsers" runat="server" class="textFieldStyle" size="50"/></td>            
        </tr>
        <tr>
            <td class="lblTitle"> Email<font style="color:Red; font-size:16px"><sup> </sup></font></td>
            <td style="width: 1%">:</td>
            <td><input type="text" id="txtAEmail" runat="server" class="textFieldStyle" size="50"/>
             </td>            
       </tr>
       <tr><td colspan="2"></td><td><br /><asp:Button runat="server" ID="btnSave" Text="Save" CssClass="buttonStyle" />&nbsp;
       <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="buttonStyle" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
