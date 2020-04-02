<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApproachingLD.aspx.vb" Inherits="ApproachingLD" %>

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
                    <td class="lblTitle" style="width: 15%">
                        PO No.
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="Td4" runat="server" class="lblText" style="width: 50%">
                        <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 15%">
                        Type
                    </td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td id="Td1" runat="server" class="lblText" style="width: 50%">
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" CssClass="selectFieldStyle">
                            <asp:ListItem Text="Site LD's Approaching" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Site LD's None" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Site LD's Future" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Site LD's Actual" Value="4"></asp:ListItem>
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
                                <asp:BoundField DataField="custpono" HeaderText="PO No." />
                                <asp:BoundField DataField="siteid" HeaderText="Site No." />
                                <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                                <asp:BoundField DataField="region" HeaderText="Region" />
                                <asp:BoundField DataField="cplanbast" DataFormatString="{0:dd-M-yyyy}" HeaderText="BAST Contract" />
                                <asp:BoundField DataField="actual" DataFormatString="{0:dd-MM-yyyy}" HeaderText="BAST Actual" />
                                <asp:BoundField DataField="total" HeaderText="Total" />
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
                                <asp:BoundField DataField="custpono" HeaderText="PO No." />
                                <asp:BoundField DataField="siteid" HeaderText="Site No." />
                                <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                                <asp:BoundField DataField="region" HeaderText="Region" />
                                <asp:BoundField DataField="cplanbast" DataFormatString="{0:dd-MM-yyyy}" HeaderText="BAST Contract" />
                                <asp:BoundField DataField="actual" DataFormatString="{0:dd-MM-yyyy}" HeaderText="BAST Actual" />
                                <asp:BoundField DataField="total" HeaderText="Total" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
