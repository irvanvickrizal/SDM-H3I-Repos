<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sendmail.aspx.vb" Inherits="sendmail"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MailReport</title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
           <table>
            <tr>
                <td style="text-align:center">
                    <asp:Label ID="lbl2g" runat="server" Width="100%" BackColor="Silver" Font-Bold="True" Font-Names="Verdana" ForeColor="Maroon" Font-Size="Small"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="ti2g" runat="server" CellPadding="4" ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:GridView ID="ti2gp" runat="server" CellPadding="4" ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="cme2g" runat="server" CellPadding="4" ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     <asp:GridView ID="cme2gp" runat="server" CellPadding="4" ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="sis2g" runat="server" CellPadding="4"  ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     <asp:GridView ID="sis2gp" runat="server" CellPadding="4"  ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="sitac2g" runat="server" CellPadding="4"  ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     <asp:GridView ID="sitac2gp" runat="server" CellPadding="4"  ForeColor="#333333" Width="98%" Font-Names="Verdana" Font-Size="Small" CaptionAlign="Left" >
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Label ID="lbl3g" runat="server" Width="100%" BackColor="Silver" Font-Bold="True" Font-Names="Verdana" ForeColor="Maroon" Font-Size="Small"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="ti3g" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     <asp:GridView ID="ti3gp" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="CME3G" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     <asp:GridView ID="CME3Gp" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
             <tr>
                <td style="text-align:center">
                    <asp:GridView ID="SIS3G" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                      <asp:GridView ID="SIS3Gp" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
             <tr>
                <td style="text-align:center">
                    <asp:GridView ID="SITAC3G" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                      <asp:GridView ID="SITAC3Gp" runat="server" CellPadding="4" Width="98%" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333" CaptionAlign="Left">
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#666666" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
