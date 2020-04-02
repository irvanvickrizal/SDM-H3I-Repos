<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ArchiveSingle.aspx.vb" Inherits="ArchiveSingle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <title>Archived Documents</title>
</head>
  <script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("ddlpono"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "PO should be select\n";
        }
        else
        {
            var k = document.getElementById("ddlsiteno"); 
            var strUser1 = k.options[k.selectedIndex].value;
            if (strUser1 == 0)
            {
                msg = msg + "Site should be select\n";
            }
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
    
    <table width="100%">
    <tr>
                <td class="pageTitle" colspan="3">
                    Single Site Documents Archive</td>
            </tr>   
    <tr>
    <td class="lblTitle">PO No</td>
    <td style="width:1%">:</td>
    <td>
         <asp:DropDownList ID="ddlpono" runat="server" AutoPostBack="True" CssClass="selectFieldStyle">
        </asp:DropDownList>
    
    </td>
    </tr>
    <tr>
    <td class="lblTitle">Site No</td>
   <td style="width:1%">:</td>
    <td><asp:DropDownList ID="ddlsiteno" runat="server" AutoPostBack="True" CssClass="selectFieldStyle">
        </asp:DropDownList></td>
    </tr>
    
    <tr>
    <td class="lblTitle">Target Location</td>
    <td style="width:1%">:</td>
    <td><asp:RadioButtonList ID="rbtnlist" runat="server" RepeatDirection="Horizontal" RepeatLayout="flow" CssClass="lblText" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">Zip File</asp:ListItem>
            <asp:ListItem Value="1">FTP Upload</asp:ListItem>
        </asp:RadioButtonList></td>
   </tr>
   
   
   
   <tr id="rowUser" runat="server" visible="false">
    <td class="lblTitle"> User Id </td>
      
     <td style="width:1%">:</td>
       <td><asp:TextBox ID="txtuserid" runat="server" CssClass="textFieldStyle"></asp:TextBox> </td>
   </tr>
   
   <tr id="rowPwd" runat="server" visible="false">
    <td class="lblTitle">Pass Word</td>
     <td style="width:1%">:</td>
       <td> <asp:TextBox ID="txtpwd" runat="server" CssClass="textFieldStyle" ></asp:TextBox></td>
   </tr>
   
   <tr id="rowURL" runat="server" visible="false">
    <td class="lblTitle">FTP Url </td>
     <td style="width:1%">:</td>
       <td> <asp:TextBox ID="txtftpurl" runat="server"  CssClass="textFieldStyle"></asp:TextBox> </td>
       </tr>
      
   
    
    
    
    
    <tr>
    <td></td>
        
           <td style="width: 1%">
           </td>
        <td ><asp:Button ID="btnArchive" runat="server" Text="Archive"   CssClass="buttonStyle"/> </td>
        
    </tr>
      <tr>
      
      <td class="lblTitle"  id="msg" runat="server" align="center"></td>
    
      </tr>
    </table>
       
   
    </form>
</body>
</html>
