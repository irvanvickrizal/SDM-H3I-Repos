<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmForgotPwd.aspx.vb" Inherits="frmForgotPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>   
    <script language="javascript" type="text/javascript">
    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtUserID").value) == false)
        {
            msg = msg + "User Id should not be Empty\n";
        }
        if (msg != "")
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
    <div style="width:100%">
        
     <table id="tblPWD" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">        
        <tr>  
               <td align="left" colspan="2" valign="top">
                    <asp:Image ID="Image2" runat="server" ImageAlign="Left" ImageUrl="~/Images/bar1440.png"/></td>
             </tr>
         </table>
            <table id="fr" runat="server"  border="0" cellpadding="1" cellspacing="1" width="100%"> 
            <tr class="pageTitle">
                <td align="left" colspan="2">Forgot Password</td>
            </tr>
            <tr>
                <td class="lblTitle">
                    User ID<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td>
                    <input id="txtUserID" runat="Server" type="text" class="textFieldStyle" style="width: 179px"/>&nbsp;<asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="goButtonStyle"/>&nbsp;
                    <asp:Button ID="btnHome" runat="server" Text="Back" CssClass="buttonStyle"/></td>
            </tr> 
            </table>
            <table id="tblSecurity" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td class="lblTitle">Security Question</td>
                <td>
                    <asp:DropDownList ID="ddlQA" runat="server" CssClass="selectFieldStyle">
                <asp:ListItem Value="0" Text="Favourite Pet Name"></asp:ListItem>
                <asp:ListItem Value="1" Text="Mother's Maiden Name"></asp:ListItem>
                <asp:ListItem Value="2" Text="Childhood Scholl"></asp:ListItem>
                <asp:ListItem Value="3" Text="Favourite Visiting Place"></asp:ListItem>
                </asp:DropDownList></td>
            </tr>
       <tr>
           <td class="lblTitle">
               Security Answer</td>
           <td>
           <input id="txtAnswer" runat="Server" type="text"  class="textFieldStyle" style="width: 304px" />
           </td>           
       </tr>
                       <tr>
                           <td class="lblTitle">
                           </td>
                           <td><br />
                               <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonStyle"/>&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle"/></td>
                       </tr>
        </table> 
    </div>
        <asp:HiddenField ID="hdnanswer" runat="server" />
        <asp:HiddenField ID="hdnmail" runat="server" />
        <asp:HiddenField ID="hdnname" runat="server" />
        <asp:HiddenField ID="hdnusrid" runat="server" />
    </form>
</body>
</html>
