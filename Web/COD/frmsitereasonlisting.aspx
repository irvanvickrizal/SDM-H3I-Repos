<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteReasonListing.aspx.vb"
    Inherits="COD_frmsitereasonlisting" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Reason Listing</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="tblReason1" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%">
                <tr>
                    <td colspan="2" class="pageTitle" style="height: 23px">
                        Site Reason Listing WCTR</td>
                    <td align="right" runat="server" id="addrow" class="pageTitleSub" style="height: 23px"
                        colspan="2">
                        List
                    </td>
                </tr>
                <tr style="height: 5">
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td align="right" class="lblTitle" valign="top" style="width: 273px">
                        Select Reason Category <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td>
                        :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlReasonCategory" runat="server" AutoPostBack="true" CssClass="selectFieldStyle"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <asp:LinkButton ID="lnkAll" runat="server" CssClass="ASmall">Display All</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 273px;">
                        Po No<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td>
                        :
                    </td>
                    <td style="height: 21px">
                        <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 273px">
                        Site<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlsite" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:DataGrid ID="grdSiteReason" runat="server" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="GridEvenRows" />
                            <AlternatingItemStyle CssClass="GridEvenRows" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Display" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelection" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"checked") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BAUT" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelection1" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"isbaut") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BAST" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelection2" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem,"isbast") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Reason ID" DataField="pk_reason" Visible="False" />
                                <asp:TemplateColumn HeaderText="Reason ID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReasonID" Text='<%# DataBinder.Eval(Container.DataItem,"sno") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Site No" DataField="siteid" />
                                <asp:BoundColumn HeaderText="Version" DataField="version" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundColumn HeaderText="PO No" DataField="pono" />
                                <asp:BoundColumn HeaderText="Reason Category" DataField="rcdesc" />
                                <asp:BoundColumn HeaderText="Reason" DataField="reason" />
                                <asp:BoundColumn HeaderText="Days" DataField="noofdays" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="display: none">
                        <asp:GridView ID="grdSiteReason1" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="True">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundField HeaderText="Site No" DataField="siteid" />
                                <asp:BoundField HeaderText="Version" DataField="version" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField HeaderText="PO No" DataField="pono" />
                                <asp:BoundField HeaderText="Reason Category" DataField="rcdesc" />
                                <asp:BoundField HeaderText="Reason" DataField="reason" />
                                <asp:BoundField HeaderText="Days" DataField="noofdays" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <input id="btnExport" runat="server" class="buttonStyle" style="width: 86pt" type="button"
                            value="Export to Excel" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="buttonStyle" />
                        <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="buttonStyle" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
