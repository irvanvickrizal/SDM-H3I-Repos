<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPendingforCostImpact.aspx.vb" Inherits="CR_frmPendingforCostImpact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="3" width="100%">
            <tr class="pageTitle">
                <td colspan="2">Pending for Cost Impact</td>
            </tr>
            <tr>
            <td colspan="2">
            </td>
            </tr>
            <tr>
            <td colspan="2">
            </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView  ID="GrdPo" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        BorderColor="Black" EmptyDataText="No Records Found" PageSize="5" Width="100%">
                        <PagerSettings Position="TopAndBottom" />
                        <RowStyle CssClass="GridOddRows" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="1%" />
                            </asp:TemplateField>                       
                            <asp:HyperLinkField DataTextField="SiteNo"  HeaderText="Site No" DataNavigateUrlFields="SiteNo" DataNavigateUrlFormatString="frmCostImpact.aspx?id={0}">                                    
                            </asp:HyperLinkField> 
                            <asp:BoundField DataField="PoNo" HeaderText="PO No" SortExpression="PONo"  />
                            <asp:BoundField DataField="WorkPkgId" ReadOnly="true" HeaderText="Work Package Id" />
                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" SortExpression="SiteName" /> 
                            <asp:BoundField DataField="ReqName" HeaderText="Requester" />
                             <asp:BoundField DataField="AccName" HeaderText="Accepter" />
                             <asp:BoundField DataField="FldType" HeaderText="Type" /> 
                             <asp:BoundField DataField="Status" HeaderText="Status" />                            
                         </Columns>
                        <PagerStyle CssClass="PagerTitle " />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        
    </div>
    <br /> <br /> <br /> 
        <asp:LinkButton ID="frmlist" Visible="false" runat="server">Back To MOM List >></asp:LinkButton><br />
        <%--<a href="frmChangeRequest.aspx" id="frmlist" runat="server" visible="false">Back To MOM List >></a>--%>
    </form>
</body>
</html>
