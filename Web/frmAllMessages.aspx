<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAllMessages.aspx.vb" Inherits="frmAllMessages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" Width="332px">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle CssClass="GridEvenRows" />
            <ItemStyle CssClass="GridOddRows"/>
            <HeaderStyle CssClass="GridHeader"/>
            <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                    <table width=100%>
                    <tr style="font-weight:bold " align="left"><td><%#container.DataItem("Title") %></td></tr>
                      <tr><td align=left ><%#Container.DataItem("Message")%></td></tr>
                      <tr style="font-weight:bold "  align="right"><td> Posted By :<%#Container.DataItem("PostedBy")%></td></tr>                                     
                    
                    </table>
                    </ItemTemplate>
                    </asp:DataList>
    
        
        
        
        
        
        
        
        </div>
    </form>
</body>
</html>
