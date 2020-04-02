<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAgendaAllScopes.aspx.vb" Inherits="DashBoard_frmagendaallscopes" %>

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
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                    Agenda - All Scopes</td>
            </tr>            
 
        <tr><td class="bodyDataMsgEvenRows" >CME</td></tr>
        <tr>
        <td id="tdAgendacme" runat="server" valign="top" style="width: 100%; height: 18px; font-weight: normal;
         font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left;">
        </td>
        </tr>
        <tr><td class="bodyDataMsgEvenRows" > SIS</td></tr>
        <tr>
        <td id="tdAgendasis" runat="server" valign="top" style="width: 100%; height: 18px; font-weight: normal;
         font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left;">
        </td>
        </tr>
        <tr><td class="bodyDataMsgEvenRows" >SITAC</td></tr>
        <tr>
        <td id="tdAgendasitac" runat="server" valign="top" style="width: 100%; height: 18px; font-weight: normal;
         font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left;">
        </td>
        </tr>
        </table>
     
    </div>
    <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
    </form>
</body>
</html>
