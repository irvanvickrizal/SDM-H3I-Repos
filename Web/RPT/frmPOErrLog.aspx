<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOErrLog.aspx.vb" Inherits="RPT_frmPOErrLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User List</title>
    <base target="_self" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%" id="divWidth">
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" id="rowadd">
                        PO Error Log List</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdPOErrLogList" runat="server" AllowPaging="True" AllowSorting="True"
                            Width="100%" AutoGenerateColumns="False" PageSize="5" EmptyDataText="No Records Found">
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
                                <asp:BoundField DataField="UPLFILENAME" HeaderText="File Name" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="TRCOUNT" HeaderText="Trannsaction Count" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="MISIDCOUNT" HeaderText="WPId Missed" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="PUR1800" HeaderText="Error Pur1800" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="PUR900" HeaderText="Error Pur900" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="DUALQTY" HeaderText="Dual Band Qty" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="CONFIGERR" HeaderText="Config Err" HeaderStyle-HorizontalAlign="center" />
                                <asp:BoundField DataField="MISSITE" HeaderText="MISSITE" HeaderStyle-HorizontalAlign="center" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
