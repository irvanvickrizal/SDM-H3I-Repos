<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWriterList.aspx.vb" Inherits="CR_frmWriterList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <base target="_self" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
        <tr class="pageTitle"><td >Writers List</td></tr>
        <tr><td class="hgap"></td></tr>
       
         <tr><td >
            <asp:GridView ID="grdWriter" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" Width="100%">
                <HeaderStyle CssClass="GridHeader" />
                <RowStyle CssClass="GridOddRows" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <PagerSettings Position="TopAndBottom" />
                <PagerStyle CssClass="PagerTitle" />
                <columns>
                    <asp:TemplateField HeaderText=" Total ">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="2%" />
                    </asp:TemplateField>
                         <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmWriterList.aspx?id={0}" HeaderText="Name" SortExpression="NAME">
                        <ItemStyle Width="25%" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID,Name" DataNavigateUrlFormatString="frmWriterList.aspx?id={0}&amp;SS={1}&amp;SelMode=True" HeaderText="Name" SortExpression="NAME">
                        <ItemStyle Width="25%" />
                    </asp:HyperLinkField>                    
                    <asp:BoundField DataField="EPM_Id" HeaderText="EPM Id" SortExpression="EPM_Id" />
                    <asp:BoundField DataField="Email" HeaderText="Mail Address" SortExpression="Email" />                    
                </columns>
            </asp:GridView>             
         </td></tr>
         
         <tr>
         <td align="right" >
            <input type="button" runat="server" id="btnClose" class="buttonStyle" value="Close" />
            <input type="hidden" runat="server" id="hdnsort" />
         </td>
         </tr>
                         
    </table>
    
    </div>
    </form>
</body>
</html>
