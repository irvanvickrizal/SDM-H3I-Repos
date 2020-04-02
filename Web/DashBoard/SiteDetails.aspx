<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SiteDetails.aspx.vb" Inherits="SiteDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ready For Bast</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <SCRIPT LANGUAGE="JavaScript">
function WindowsClose() 
{ 
    window.opener.location.href = '../frmdashboard.aspx';
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();
 
} 
</SCRIPT> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">     
            <tr>
                <td colspan="2" style="background-image: url(../Images/barbg.jpg) ;background-repeat: repeat-x; width:180px">
                    &nbsp;</td>
            </tr>       
            <tr>
                <td class="hgap"></td>
            </tr>
            <tr>
                <td runat="server">
                    <asp:DataList ID="DlBast" runat="server" BackColor="White" BorderColor="#E7E7FF"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" RepeatColumns="2">
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Width="100%" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "site_no") %>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList></td>
            </tr>
            <tr>
                <td class="hgap"></td>
            </tr>
               <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" value="Close" onclick="WindowsClose();"  /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
