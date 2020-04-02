<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Customer-Digital-signature.aspx.vb" Inherits="Digital_signature_cus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function WindowsClose() 
{ 
    
         window.opener.location.href = '../dashboard/DashboardpopupBaut.aspx?time='+(new Date()).getTime();
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();

 
} 
function getQueryVariable(variable)
{
var query = window.location.search.substring(1);
var vars = query.split("&");
for (var i=0;i<vars.length;i++)  {
var pair = vars[i].split("=");
if (pair[0] == variable)
{
return pair[1];
}
}
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center"><iframe runat="server" id="PDFViwer" width="95%" height="500px" scrolling="auto"></iframe>
                </td>
            </tr>
            <tr>
                <td align="center" class="hgap">
                    <asp:Label ID="lblLinks" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="hgap">
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                        <tr>
                            <td >
                                User Name</td>
                            <td >
                                <asp:TextBox ID="txtUserName" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator> <asp:LinkButton ID="lnkrequest" runat="server">Request Password</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >
                                Password</td>
                            <td >
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td >
                            </td>
                            <td >
                                <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="150px" /> <input id="BtnClose" type="submit" value="Close &  updated" onclick="WindowsClose();"  />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSign" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
