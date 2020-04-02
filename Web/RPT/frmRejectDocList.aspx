<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRejectDocList.aspx.vb" Inherits="frmRejectDocList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="1" cellspacing="1" width="100%">                
        <tr class="pageTitle"><td>Rejected Document List</td></tr>

        <tr>
            <td colspan="3">
<asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" AllowSorting="False" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" /> 
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle"/>
            <Columns>
                    <asp:TemplateField HeaderText=" Total ">
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server"></asp:Label>    
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:BoundField DataField="sw_id" HeaderText="swid" /> 
                <asp:BoundField DataField="docname" HeaderText="Document" /> 
               <asp:BoundField DataField="site_no" HeaderText="SiteNo"/>
               <asp:BoundField DataField="fldtype" HeaderText="Scope"/>
               <asp:BoundField DataField="pono" HeaderText="PONo"/>
               <asp:BoundField DataField="remarks" HeaderText="Remarks" />
               <asp:BoundField DataField="eventendtime" HeaderText="Rejected Date" DataFormatString="{0:dd MMM yyyy}" />
               </Columns>
        </asp:GridView>
                    </td>
                </tr>
            </table>
       </form>
</body>
</html>
