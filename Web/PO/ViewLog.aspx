<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLog.aspx.vb" Inherits="ViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr id="title" runat="server" >
                <td class="pageTitle" colspan="7" >View Log For <asp:label runat="server" ID="lblid">EPM</asp:label> RawData</td>
            </tr>            
            <tr id="porpt" runat="server" >
                <td class="lblTitle" style="width:15%">Month<font style="color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width:1%">:</td>
                  <td style="width:20%">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="selectFieldStyle"  >
                        <asp:ListItem Value="0">January</asp:ListItem>
                        <asp:ListItem Value="1">February</asp:ListItem>
                        <asp:ListItem Value="2">March</asp:ListItem>
                        <asp:ListItem Value="3">April</asp:ListItem>
                        <asp:ListItem Value="4">May</asp:ListItem>
                        <asp:ListItem Value="5">June</asp:ListItem>
                        <asp:ListItem Value="6">July</asp:ListItem>
                        <asp:ListItem Value="7">August</asp:ListItem>
                        <asp:ListItem Value="8">September</asp:ListItem>
                        <asp:ListItem Value="9">October</asp:ListItem>
                        <asp:ListItem Value="10">Novemer</asp:ListItem>
                        <asp:ListItem Value="11">December</asp:ListItem>
                    </asp:DropDownList></td>
                    <td class="lblTitle" style="width:15%">Year<font style="color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width:1%">:</td>
              <td style="width:20%">
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="selectFieldStyle"></asp:DropDownList></td>
                    <td><asp:Button ID="btnViewLog" runat="server" Text="ViewLog" CssClass="buttonStyle" /></td>                                    
               </tr>                      
       </table>
      <table id="tbldisplay" runat="server" cellpadding="1" border="0" cellspacing="1" width="100%">
      <tr><td><asp:TextBox ID="txtViewLog" runat="Server"  CssClass  ="textFieldStyle" Height="380px" TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox></td></tr>
      </table>       
    </div>    
    </form>
</body>
</html>
