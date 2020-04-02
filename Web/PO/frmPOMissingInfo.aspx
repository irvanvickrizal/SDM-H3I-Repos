<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOMissingInfo.aspx.vb" Inherits="PO_frmPOMissingInfo" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    
    <form id="form1" runat="server">
    <table cellpadding="1" cellspacing="1" width="100%">
        <tr id="trPono" runat="Server"><td class="lblTitle" style="width:10%">Select PO No</td>
            <td style="width:1%">:</td>
            <td align="Left"><asp:DropDownList ID="ddlPO" runat="Server" CssClass="selectFieldStyle" AutoPostBack="true" ></asp:DropDownList></td></tr>
        <tr>
            <td align="right" colspan="3"><input id="btnExport" runat="server"  type="button" value="Export to Excel"  class="buttonStyle" style="width: 86pt" visible="False" />
            </td>
        </tr>
        <tr><td colspan="3">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="true" PageSize="15">
    <PagerSettings Position="TopAndBottom" />
    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total " ItemStyle-Width="1%" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                       <%#Container.DataItemIndex + 1%>.&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PONo" HeaderText="PONo" />
                <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" />
                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                <asp:BoundField DataField="SiteName" HeaderText="SiteName" />
                <asp:BoundField DataField="Band_Type" HeaderText="Band Type" />
                <asp:BoundField DataField="Band" HeaderText="Band" /> 
                <asp:BoundField DataField="Config" HeaderText="Config" /> 
                <asp:TemplateField>
                                  <ItemTemplate>
                        <table>
                            <tr><td>Original :</td></tr>
                            <tr><td>Modified :</td></tr>
                        </table>
                  
                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Purchase 900</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label1" runat="server"><%#Container.DataItem("Purchase900")%></asp:Label></td></tr>
                            <tr><td> <asp:Label ID="Label4" runat="server"><%#Container.DataItem("CPurchase900")%></asp:Label></td></tr>                            
                        </table>
                  
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Purchase 1800</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label5" runat="server"><%#Container.DataItem("Purchase1800")%></asp:Label></td></tr>                            
                            <tr><td> <asp:Label ID="Label6" runat="server"><%#Container.DataItem("CPurchase1800")%></asp:Label></td></tr>
                        </table>
                  
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Qty</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label5" runat="server"><%#Container.DataItem("Qty")%></asp:Label></td></tr>                            
                            <tr><td> <asp:Label ID="Label6" runat="server"><%#Container.DataItem("CQty")%></asp:Label></td></tr>
                        </table>
 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="Qty" />
            </Columns>
        </asp:GridView></td>
        </tr>
        <tr><td style="display:none">
   <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
    <PagerSettings Position="TopAndBottom" />
    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total " ItemStyle-Width="1%" ItemStyle-HorizontalAlign="right">
                    <ItemTemplate>
                       <%#Container.DataItemIndex + 1%>.&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PONo" HeaderText="PONo" />
                <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" />
                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                <asp:BoundField DataField="SiteName" HeaderText="SiteName" />
                <asp:BoundField DataField="Band_Type" HeaderText="Band Type" />
                <asp:BoundField DataField="Band" HeaderText="Band" /> 
                <asp:BoundField DataField="Config" HeaderText="Config" /> 
                <asp:TemplateField>
                                  <ItemTemplate>
                        <table>
                            <tr><td>Original :</td></tr>
                            <tr><td>Modified :</td></tr>
                        </table>
                  
                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Purchase 900</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label1" runat="server"><%#Container.DataItem("Purchase900")%></asp:Label></td></tr>
                            <tr><td> <asp:Label ID="Label4" runat="server"><%#Container.DataItem("CPurchase900")%></asp:Label></td></tr>                            
                        </table>
                  
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Purchase 1800</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label5" runat="server"><%#Container.DataItem("Purchase1800")%></asp:Label></td></tr>                            
                            <tr><td> <asp:Label ID="Label6" runat="server"><%#Container.DataItem("CPurchase1800")%></asp:Label></td></tr>
                        </table>
                  
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>     <table cellspacing="1" cellpadding="1">
                            <tr><td>Qty</td></tr>                            
                            <tr></tr>  
                        </table></HeaderTemplate>
                    <ItemTemplate>
                        <table>
                            <tr><td><asp:Label ID="Label5" runat="server"><%#Container.DataItem("Qty")%></asp:Label></td></tr>                            
                            <tr><td> <asp:Label ID="Label6" runat="server"><%#Container.DataItem("CQty")%></asp:Label></td></tr>
                        </table>
 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="Qty" />
            </Columns>
        </asp:GridView>
         </td>
        </tr>
        
    </table>
    <br />
        <input id="Button1" type="button" class="buttonStyle" runat="server" value="Close" onclick="window.close();" />
    </form>
</body>
</html>
