<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sendsms.aspx.vb" Inherits="sendsms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css"  href="CSS/Styles.css" />
  <script language="javascript" type="text/javascript" >
var fso = new ActiveXObject("Scripting.FileSystemObject");
if (fso.FileExists("c:/text.txt")){
 alert("File Exist's");
} else {
 alert("File Doesn't Exist");
} 
</script>     
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        <table>
            <tr>
                <td colspan="2">
                    SMS Testing</td>
            </tr>
            <tr>
                <td style="width: 79px">
                    Mobile No</td>
                <td style="width: 180px">
                    <asp:TextBox ID="txtmobile" runat="server" Width="187px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 79px">
                    Message</td>
                <td style="vertical-align: top; width: 180px; text-align: left">
                  <asp:TextBox ID="txtmessage" runat="server" Height="68px" TextMode="Multiline"
                        Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 79px">
                </td>
                <td style="width: 180px">
                    <asp:Button ID="Button1" runat="server" Text="Send SMS" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
