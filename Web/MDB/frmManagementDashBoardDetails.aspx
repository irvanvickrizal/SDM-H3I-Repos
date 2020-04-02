<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmManagementDashBoardDetails.aspx.vb"
    Inherits="DashBoard_frmManagementDashBoardDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Management Dashboard | PO Site List</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="1" cellpadding="1">
            <tr>
                <td class="lblTitle" style="width: 15%">
                    Purchase Order No
                </td>
                <td style="width: 1%">
                    :
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
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="WPID" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="POType" HeaderText="Po Type" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="CustomerPORecordDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Contract Date" />
                            <asp:BoundField DataField="CommonDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="On Air Date" />
                            <asp:BoundField DataField="BastDate" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="BAST Date" />
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
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="WPID" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="POType" HeaderText="Po Type" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="CustomerPORecordDate" HeaderText="Contract Date" />
                            <asp:BoundField DataField="CommonDate" HeaderText="On Air Date" />
                            <asp:BoundField DataField="BastDate" HeaderText="BAST Date" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
