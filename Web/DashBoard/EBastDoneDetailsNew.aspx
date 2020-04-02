<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EBastDoneDetailsNew.aspx.vb" Inherits="DashBoard_frmEbastDoneDetailsNew" %>

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
                <td align="right">
                    <input id="btnExport" runat="server" type="button" value="Export to Excel" class="buttonStyle"
                        style="width: 86pt" />
                    <asp:Button ID="btnfullinfo" class="buttonStyle" runat="server" Text="FullInfo" Width="120px" Visible="False" />
                </td>
                <td align="right">
                    <input type="button" id="btnClose" runat="server" class="buttonStyle" value="Close"
                        onclick="window.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="5" Class="pageTitle">BAST Status
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
                            <asp:BoundField DataField="RGNname" HeaderText="Region" />
                            <asp:BoundField DataField="Site_no" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                            <asp:BoundField DataField="Tselprojectid" HeaderText="TSEL ID" />
                            <asp:BoundField DataField="SCOPE" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="Date_time" HeaderText="BAST Date(Telkomsel)" />
                            <asp:BoundField DataField="name" HeaderText="Telkomsel User" />
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
                           <asp:BoundField DataField="RGNname" HeaderText="Region" />
                            <asp:BoundField DataField="Site_no" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                            <asp:BoundField DataField="Tselprojectid" HeaderText="TSEL ID" />
                            <asp:BoundField DataField="SCOPE" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="Date_time" HeaderText="BAST Date(Telkomsel)" />
                            <asp:BoundField DataField="name" HeaderText="Telkomsel User" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
