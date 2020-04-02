<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserPOList.aspx.vb" Inherits="frmUserPOList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="pageTitle">
                <td colspan="3" runat="server" id="rowTitle">
                    Purchase Order List</td>
            </tr>
            <tr>
                <td class="lblTitle" style="width: 20%">
                    Search</td>
                <td style="width: 1%">
                    :</td>
                <td>
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="SiteNo">SiteId</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
            </tr>
            <tr>
                <td class="lblTitle" style="height: 21px">
                    Select PONo</td>
                <td style="height: 21px">
                    :</td>
                <td style="height: 21px">
                    <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr style="height: 5px">
            </tr>
            <tr>
                <td colspan="3" style="width: 100%">
                    <br />
                    <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="true">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total " ItemStyle-HorizontalAlign="right" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <%# container.DataItemIndex + 1 %>
                                    .
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SiteId">
                                <ItemTemplate>
                                    <a href="frmPODetails.aspx?id='<%# Eval("SiteNo") %>'&sno=<%# Eval("po_id") %>&TT=P&C=0">
                                        <%# Eval("SiteNo") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pono" HeaderText="PO No." />
                            <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" />
                            <asp:BoundField DataField="siteno" HeaderText="Site No." />
                            <asp:BoundField DataField="sitenamepo" HeaderText="Site Name" />
                            <asp:BoundField DataField="scope" HeaderText="Scope" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
