<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteReason.aspx.vb" Inherits="COD_frmSiteReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Reason BAST</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="tblReason1" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%">
                <tr>
                    <td colspan="2" class="pageTitle">
                        Site Reason</td>
                    <td align="right" runat="server" id="addrow" class="pageTitleSub">
                        Create</td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 20%">
                        Search</td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                            <asp:ListItem Value="SiteNo">SiteId</asp:ListItem>
                            <asp:ListItem Value="Scope">Scope</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top" style="width: 200px">
                        Select Po No <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%" valign="top">
                        :</td>
                    <td>
                        <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="true" CssClass="selectFieldStyle"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <asp:LinkButton ID="lnkAll" runat="server" CssClass="ASmall">Display All</asp:LinkButton></td>
                </tr>
                <tr>
                    <td colspan="2" class="pageTitle" id="tdTitle" runat="server" style="height: 23px">
                        Site Reason
                    </td>
                    <td align="right" class="pageTitleSub" style="height: 23px">
                        List
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 196px; height: 18px" valign="top">
                    </td>
                    <td style="width: 1%; height: 18px" valign="top">
                    </td>
                    <td align="right" colspan="3" style="height: 18px">
                        &nbsp;<input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" /></td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdSiteReason" runat="server" Width="100%" AutoGenerateColumns="False"
                EmptyDataText="No Records Found" AllowPaging="True" AllowSorting="True">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total ">
                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                        <ItemTemplate>
                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="SiteNo" DataNavigateUrlFormatString="frmSiteReason.aspx?id={0}&amp;SId={1}&amp;version={2}&amp;Mode=E"
                        HeaderText="Site No" SortExpression="Site No" DataNavigateUrlFields="PONo,SiteNo,SiteVersion">
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="SiteName" DataField="Site_Name" SortExpression="Site_Name" />
                    <asp:BoundField DataField="SiteVersion" HeaderText="Version No" SortExpression="SiteVersion" />
                </Columns>
            </asp:GridView>
            <table id="tblReason" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
                visible="false">
                <tr>
                    <td class="lblTitle" colspan="3" style="height: 16px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        PO No
                    </td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <asp:Label ID="lblPONo" runat="server" Width="104px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 3px" valign="top">
                        Site ID</td>
                    <td style="width: 1%; height: 3px" valign="top">
                        :</td>
                    <td style="height: 3px">
                        <asp:Label ID="lblSiteNo" runat="server" Width="104px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 23px;" valign="top">
                        Version&nbsp;</td>
                    <td style="width: 1%; height: 23px;" valign="top">
                        :</td>
                    <td style="height: 23px">
                        <asp:Label ID="lblVersion" runat="server" Width="104px"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        Reason&nbsp; Category
                    </td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <asp:DropDownList ID="ddlReasonCategory" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        Reason WCTR&nbsp;</td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="right" class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        Remark
                    </td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <textarea id="txtRemark" runat="server" class="textFieldStyle" cols="50" rows="5"></textarea></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        Additional&nbsp; Remarks
                    </td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <textarea id="txtAddRemark" runat="server" class="textFieldStyle" cols="50" rows="5"></textarea></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 198px; height: 16px" valign="top">
                        No.&nbsp; Of&nbsp; Days</td>
                    <td style="width: 1%; height: 16px" valign="top">
                        :</td>
                    <td style="height: 16px">
                        <input id="txtNoofdays" runat="server" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789 ')"
                            class="textFieldStyle" type="text" style="width: 64px" /></td>
                </tr>
                <tr>
                    <td class="lblTitle" colspan="2" style="height: 15px" valign="top">
                    </td>
                    <td style="height: 15px">
                        </td>
                </tr>
                <tr>
                    <td class="lblTitle" colspan="2" style="height: 15px" valign="top">
                    </td>
                    <td style="height: 15px">
                        <input id="btnNewGroup" runat="server" class="buttonStyle" type="button" value="Save" /></td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right" colspan="3" style="height: 38px">
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="hdnDisp" value="1" />
        <input type="hidden" runat="server" id="hdnSort" value="" />
    </form>
</body>
</html>
