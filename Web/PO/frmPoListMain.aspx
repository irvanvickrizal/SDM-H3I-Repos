<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPoListMain.aspx.vb" Inherits="PO_frmPoListMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="pageTitle">
                <td colspan="3" runat="server" id="rowTitle">Purchase Order List</td>
            </tr>
            <%--<tr>
                <td class="lblTitle" style="width:20%">Search</td>
                <td style="width:1%">:</td>
                <td>
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="PONo">PoNo</asp:ListItem>
                                            
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
            </tr>--%>
            
            <tr>
            <td align="right" colspan="3" style="height: 26px"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
               <asp:HyperLinkField DataNavigateUrlFields="PONo" DataNavigateUrlFormatString="frmPOList.aspx?id={0}&type=P" HeaderText="PONo" DataTextField="PONo" />
                          </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
