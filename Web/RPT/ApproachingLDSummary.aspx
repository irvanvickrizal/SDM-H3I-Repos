<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApproachingLDSummary.aspx.vb"
    Inherits="ApproachingLDSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td class="lblTitle" style="width: 15%">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td id="lblPOno" runat="server" class="lblText" style="width: 50%">
                    </td>
                    <td align="right">
                        <input id="btnExport" runat="server" type="button" value="Export to Excel" class="buttonStyle"
                            style="width: 86pt" />
                    </td>
                    <td align="right">
                        <input type="button" id="btnClose" runat="server" class="buttonStyle" value="Close"
                            onclick="window.close();" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 15%">
                        Region
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="Td3" runat="server" class="lblText" style="width: 50%">
                        <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="false" EmptyDataText="No Criteria Met">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="region" HeaderText="Region">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="custpono" HeaderText="PO No.">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="remaining2" HeaderText="Site LD's Approaching">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ontime2" HeaderText="Site LD's None">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latenotdone2" HeaderText="Site LD's Future">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latedone2" HeaderText="Site LD's Actual">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="display: none">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="region" HeaderText="Region">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="custpono" HeaderText="PO No.">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="remaining2" HeaderText="Site LD's Approaching">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ontime2" HeaderText="Site LD's None">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latenotdone2" HeaderText="Site LD's Future">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latedone2" HeaderText="Site LD's Actual">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        Summary:
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="grdDB2" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="false" EmptyDataText="No Criteria Met">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="region" HeaderText="Region">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="remaining2" HeaderText="Site LD's Approaching">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ontime2" HeaderText="Site LD's None">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latenotdone2" HeaderText="Site LD's Future">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="latedone2" HeaderText="Site LD's Actual">
                                    <ItemStyle HorizontalAlign="center" Wrap="true" Width="10%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
