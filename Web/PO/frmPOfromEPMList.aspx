<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOfromEPMList.aspx.vb" Inherits="PO_frmPOfromEPMList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>POlist from EPM</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="pageTitle">
                <td colspan="3" runat="server" id="rowTitle">Purchase Order List from EPM</td>
            </tr>
            <tr style="height:5px"></tr>      
            <tr>
                <td colspan="3" style="width:100%"><br />
                    <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total ">
                    <ItemTemplate><%# container.DataItemIndex + 1 %>.
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                </asp:TemplateField>
                <asp:BoundField DataField="pono" HeaderText="PO No" />
               <asp:HyperLinkField DataNavigateUrlFields="SiteNo,PO_Id" DataNavigateUrlFormatString="frmPODetails.aspx?id={0}&amp;Sno={1}&amp;TT=P&amp;from=epm" HeaderText="SiteId" DataTextField="SiteNo" />               
                <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" />
                <asp:BoundField DataField="WorkPackageName" HeaderText="WorkPackageName" />
           </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
