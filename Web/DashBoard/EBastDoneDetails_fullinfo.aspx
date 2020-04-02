<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EBastDoneDetails_fullinfo.aspx.vb"
    Inherits="EBastDoneDetails_fullinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>eBAST Reporting</title>
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
                            AllowPaging="True">
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
                                <asp:BoundField DataField="Region" HeaderText="Region" />
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                                <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID" />
                                <asp:BoundField DataField="TSELPROJECTID" HeaderText="TSEL ID" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="POName" HeaderText="Po Name" />
                                <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                                <asp:BoundField DataField="ACT_9350" HeaderText="Work Completed" />
                                <asp:BoundField DataField="BAUTRefNo" HeaderText="BAUT Ref. No." />
                                <asp:BoundField DataField="BAUTNSN" HeaderText="BAUT Date(NSN)" />
                                <asp:BoundField DataField="BUnsnuser" HeaderText="BAUT User(NSN)" />
                                <asp:BoundField DataField="BAUTTELKOMSEL" HeaderText="BAUT Date(Telkomsel)" />
                                <asp:BoundField DataField="BUtelkomseluser" HeaderText="BAUT User(Telkomsel)" />
                                <asp:BoundField DataField="BASTNSN" HeaderText="BAST Date(NSN)" />
                                <asp:BoundField DataField="BSnsnuser" HeaderText="BAST User(NSN)" />
                                <asp:BoundField DataField="BASTTELKOMSEL" HeaderText="BAST Date(Telkomsel)" />
                                <asp:BoundField DataField="BStelkomseluser" HeaderText="BAST User(Telkomsel)" />
                                <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
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
                                <asp:BoundField DataField="Region" HeaderText="Region" />
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                                <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID" />
                                <asp:BoundField DataField="TSELPROJECTID" HeaderText="TSEL ID" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="POName" HeaderText="Po Name" />
                                <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                                <asp:BoundField DataField="ACT_9350" HeaderText="Work Completed" />
                                <asp:BoundField DataField="BAUTRefNo" HeaderText="BAUT Ref. No." />
                                <asp:BoundField DataField="BAUTNSN" HeaderText="BAUT Date(NSN)" />
                                <asp:BoundField DataField="BUnsnuser" HeaderText="BAUT User(NSN)" />
                                <asp:BoundField DataField="BAUTTELKOMSEL" HeaderText="BAUT Date(Telkomsel)" />
                                <asp:BoundField DataField="BUtelkomseluser" HeaderText="BAUT User(Telkomsel)" />
                                <asp:BoundField DataField="BASTNSN" HeaderText="BAST Date(NSN)" />
                                <asp:BoundField DataField="BSnsnuser" HeaderText="BAST User(NSN)" />
                                <asp:BoundField DataField="BASTTELKOMSEL" HeaderText="BAST Date(Telkomsel)" />
                                <asp:BoundField DataField="BStelkomseluser" HeaderText="BAST User(Telkomsel)" />
                                <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
