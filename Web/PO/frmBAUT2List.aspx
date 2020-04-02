<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBAUT2List.aspx.vb" Inherits="frmBAUT2List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAUT2 List</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                        BAUT2 List</td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="grdBAST2List" runat="server" AutoGenerateColumns="False" Width="100%">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                    HorizontalAlign="Right" VerticalAlign="Middle" />
                <Columns>
                    <asp:BoundField DataField="sw_id" HeaderText="SWID" />
                    <asp:BoundField DataField="siteno" HeaderText="Site ID" />
                    <asp:BoundField DataField="site_name" HeaderText="Site Name" />
                    <asp:BoundField DataField="pono" HeaderText="PO No." />
                    <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                    <asp:HyperLinkField HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
