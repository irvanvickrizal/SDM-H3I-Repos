<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLastUserLogin.aspx.vb"
    Inherits="frmLastUserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" cellspacing="1" cellpadding="1">
                <tr>
                    <td align="right">
                        <input id="btnExport" runat="server" type="button" value="Export to Excel" class="buttonStyle"
                            style="width: 86pt" />
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
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
                                <asp:BoundField DataField="usrtype" HeaderText="User Type" />
                                <asp:BoundField DataField="sconname" HeaderText="Company name" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="email" HeaderText="eMail" />
                                <asp:BoundField DataField="lastlogin" HeaderText="Last Login (Days)" />
                                <asp:BoundField DataField="java" HeaderText="Java" />
                                <asp:BoundField DataField="area" HeaderText="Area" />
                                <asp:BoundField DataField="region" HeaderText="Region" />
                                <asp:BoundField DataField="usrrole" HeaderText="Role" />
                                <asp:BoundField DataField="neverlogin" HeaderText="Never Login" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" style="display: none">
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
                                <asp:BoundField DataField="usrtype" HeaderText="User Type" />
                                <asp:BoundField DataField="sconname" HeaderText="Company name" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="email" HeaderText="eMail" />
                                <asp:BoundField DataField="lastlogin" HeaderText="Last Login (Days)" />
                                <asp:BoundField DataField="java" HeaderText="Java" />
                                <asp:BoundField DataField="area" HeaderText="Area" />
                                <asp:BoundField DataField="region" HeaderText="Region" />
                                <asp:BoundField DataField="usrrole" HeaderText="Role" />
                                <asp:BoundField DataField="neverlogin" HeaderText="Never Login" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
