<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsageSummary.aspx.vb" Inherits="UsageSummary" %>

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
                    <td align="left" class="lblTitle" style="width: 53%">
                        Start Date:
                        <input id="txtStart" runat="Server" class="textFieldStyle" readonly="readonly" type="text" />
                        <asp:ImageButton ID="btnStart" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" />
                        End Date:
                        <input id="txtEnd" runat="Server" class="textFieldStyle" readonly="readonly" type="text" />&nbsp;
                        <asp:ImageButton ID="btnEnd" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" />
                        <input type="button" id="btnGenerate" runat="server" class="buttonStyle" value="Generate" />
                    </td>
                    <td align="right">
                        <input id="btnExport" runat="server" type="button" value="Export to Excel" class="buttonStyle"
                            style="width: 86pt" />
                        <input type="button" id="btnClose" runat="server" class="buttonStyle" value="Close"
                            onclick="window.close();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
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
                                <asp:BoundField DataField="name" HeaderText="User Name" />
                                <asp:BoundField DataField="type" HeaderText="Company" />
                                <asp:BoundField DataField="roledesc" HeaderText="Role" />
                                <asp:BoundField DataField="tskname" HeaderText="Task" />
                                <asp:BoundField DataField="docname" HeaderText="Document" />
                                <asp:BoundField DataField="pono" HeaderText="PO No." />
                                <asp:BoundField DataField="scope" HeaderText="Scope" />
                                <asp:BoundField DataField="site_no" HeaderText="Site ID" />
                                <asp:BoundField DataField="site_name" HeaderText="Site Name" />
                                <asp:BoundField DataField="eventstarttime" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date Approval" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="display: none">
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
                                <asp:BoundField DataField="name" HeaderText="User Name" />
                                <asp:BoundField DataField="type" HeaderText="Company" />
                                <asp:BoundField DataField="roledesc" HeaderText="Role" />
                                <asp:BoundField DataField="tskname" HeaderText="Task" />
                                <asp:BoundField DataField="docname" HeaderText="Document" />
                                <asp:BoundField DataField="pono" HeaderText="PO No." />
                                <asp:BoundField DataField="scope" HeaderText="Scope" />
                                <asp:BoundField DataField="site_no" HeaderText="Site ID" />
                                <asp:BoundField DataField="site_name" HeaderText="Site Name" />
                                <asp:BoundField DataField="eventstarttime" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date Approval" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
