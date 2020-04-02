<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMessageBoardPost.aspx.vb" Inherits="frmMessageBoardPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>message Board</title>
   <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>  
</head>
<script language="javascript" type="text/javascript">
        function checkIsEmpty()    
        {
            var msg="";                       
            if (IsEmptyCheck(document.getElementById("txtTitle").value) == false)
            {
                msg = msg + "Title should not be empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtMessage").value) == false)
            {
                msg = msg + "Message should not be empty\n"
            } 
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true;
            }           
        } 

        </script> 
<body>
    <form id="form1" runat="server">  

   
                   <table id="tblSetup" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td align="left" colspan="4">Message Board Post</td>
            </tr>
                       <tr>
                           <td class="lblTitle" style="width: 169px">
                               Title</td>
                           <td>
                               <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
                       </tr>
            <tr>
                <td class="lblTitle" style="width: 169px">
                    Message</td>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" Height="175px" TextMode="MultiLine" Width="391px"></asp:TextBox></td>
            </tr>
            
                       <tr>
                           <td class="lblTitle" style="width: 169px">
                           </td>
                           <td style="height: 21px">
                               <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttonStyle" Width="75px" />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle" Width="75px" /></td>
                       </tr>   
    
     </table>
    </form>
</body>
</html>
