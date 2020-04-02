<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEPMView.aspx.vb" Inherits="PO_frmEPMView" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EPM View</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="updPanel" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
                <ContentTemplate>
                    <table width="100%">
                        <tr class="pageTitle">
                            <td runat="server" id="rowTitle" colspan="4">
                                EPM List</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                Select PONo :
                                <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                                </asp:DropDownList>&nbsp;&nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="buttonStyle"
                                    Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="True" AllowPaging="True"
                                    Width="100%">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <%--<Columns>
                                        <asp:TemplateField HeaderText="Total Sites">
                                            <ItemTemplate>
                                                <%# container.DataItemIndex + 1 %>.
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="POType" HeaderText="PO Type" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="POName" HeaderText="PO Name" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="125px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PoNo" HeaderText="Customer PO" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkPkgId" HeaderText="WPID" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SiteNo" HeaderText="Site ID" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FldType" HeaderText="Reason"
                                            HtmlEncode="False" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="100px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Scope" HeaderText="Scope" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>    
                                        <asp:BoundField DataField="Band_Type" HeaderText="New Band Type" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Band" HeaderText="Band" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="75px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExistConfig" HeaderText="Existing Configuration" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Config" HeaderText="New Configuration" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Purchase900" HeaderText="900 Purchased" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Purchase1800" HeaderText="1800 Purchased" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BSSHW" HeaderText="Hardware (OUTDOOR/INDOOR)" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BSSCode" HeaderText="ID CODE" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="QTY" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AntennaName" HeaderText="Planned Antenna" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="75px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AntennaQty" HeaderText="Antenna Qty" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FeederLen" HeaderText="Feeder Length" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FeederType" HeaderText="Feeder Type" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FeederQty" HeaderText="Feeder Qty" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Value1" HeaderText="Value in USD" DataFormatString="{0:0.00}"
                                            HtmlEncode="False" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Value2" HeaderText="Value in IDR" DataFormatString="{0:0.00}"
                                            HtmlEncode="False" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Value2IDR" HeaderText="Value 2in IDR" DataFormatString="{0:0.00}"
                                            HtmlEncode="False" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>    
                                        <asp:BoundField DataField="MS9075_EquipArrivalPlanned" HeaderText="MS9075 Equip Arrival Planned" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>  
                                        <asp:BoundField DataField="MS9350_OnAirPlanned" HeaderText="MS9350 On Air Planned" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>  
                                        <asp:BoundField DataField="MS9550_BAUTPlanned" HeaderText="MS9550 BAUT Planned" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>  
                                        <asp:BoundField DataField="MS9750_BASTPlanned" HeaderText="MS9750 BAST Planned" >
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField> 
                                    </Columns>--%>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="grdExport" runat="server" AutoGenerateColumns="True" Width="100%"
                                    Visible="false">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                                        HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <%--<Columns>
                                        <asp:TemplateField HeaderText="Total Sites" ItemStyle-HorizontalAlign="right" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <%# container.DataItemIndex + 1 %>.</ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="POType" HeaderText="PO Type" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="POName" HeaderText="PO Name" ItemStyle-Width="125px" ItemStyle-HorizontalAlign="Left"/>
                                        <asp:BoundField DataField="PoNo" HeaderText="Customer PO" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="WorkPkgId" HeaderText="WPID" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left"/>
                                        <asp:BoundField DataField="SiteNo" HeaderText="Site ID" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="FldType" HeaderText="Reason" ItemStyle-Width="100px" ItemStyle-Wrap="true"
                                            HtmlEncode="false" />
                                        <asp:BoundField DataField="Scope" HeaderText="Scope" ItemStyle-Width="50px" />    
                                        <asp:BoundField DataField="Band_Type" HeaderText="New Band Type" ItemStyle-Width="50px" />
                                        <asp:BoundField DataField="Band" HeaderText="Band" ItemStyle-Width="75px" />
                                        <asp:BoundField DataField="ExistConfig" HeaderText="Existing Configuration" ItemStyle-Width="75px"
                                            ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField DataField="Config" HeaderText="New Configuration" ItemStyle-Width="75px"
                                            ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField DataField="Purchase900" HeaderText="900 Purchased" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="75px" />
                                        <asp:BoundField DataField="Purchase1800" HeaderText="1800 Purchased" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="75px" />
                                        <asp:BoundField DataField="BSSHW" HeaderText="Hardware (OUTDOOR/INDOOR)" ItemStyle-Width="100px" />
                                        <asp:BoundField DataField="BSSCode" HeaderText="ID CODE" ItemStyle-Width="50px" />
                                        <asp:BoundField DataField="Qty" HeaderText="QTY" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="AntennaName" HeaderText="Planned Antenna" ItemStyle-Width="75px" />
                                        <asp:BoundField DataField="AntennaQty" HeaderText="Antenna Qty" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="FeederLen" HeaderText="Feeder Length" ItemStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField DataField="FeederType" HeaderText="Feeder Type" ItemStyle-Width="60px"
                                            ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField DataField="FeederQty" HeaderText="Feeder Qty" ItemStyle-HorizontalAlign="right"
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="Value1" HeaderText="Value In USD" DataFormatString="{0:0.00}"
                                            HtmlEncode="false" ItemStyle-HorizontalAlign="right" ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="Value2" HeaderText="Value In IDR" DataFormatString="{0:0.00}"
                                            HtmlEncode="false" ItemStyle-HorizontalAlign="right" ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="Value2IDR" HeaderText="Value 2in IDR" DataFormatString="{0:0.00}"
                                            HtmlEncode="false" ItemStyle-HorizontalAlign="right" ItemStyle-Width="60px" />    
                                        <asp:BoundField DataField="MS9075_EquipArrivalPlanned" HeaderText="MS9075 Equip Arrival Planned" ItemStyle-Width="50px" />  
                                        <asp:BoundField DataField="MS9350_OnAirPlanned" HeaderText="MS9350 On Air Planned" ItemStyle-Width="50px" />  
                                        <asp:BoundField DataField="MS9550_BAUTPlanned" HeaderText="MS9550 BAUT Planned" ItemStyle-Width="50px" />  
                                        <asp:BoundField DataField="MS9750_BASTPlanned" HeaderText="MS9750 BAST Planned" ItemStyle-Width="50px" />      
                                    </Columns>--%>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
