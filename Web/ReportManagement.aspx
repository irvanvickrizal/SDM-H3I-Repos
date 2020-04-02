<%@ Page Language="VB"  Theme="ThemeBlue" AutoEventWireup="false" CodeFile="ReportManagement.aspx.vb" Inherits="ReportManagement" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="pageTitle" style="width: 225px; height: 21px; font-weight: bold;">
                    Management Report</td>
                <td class="pageTitleSub" style="width: 286px; height: 21px">
                  <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" style="width: 137px; height: 64px" />
                </td>
            </tr>
            <tr>
                <td style="width: 225px" class="pageTitleSub">
                    Summary</td>
                <td style="width: 286px" class="pageTitleSub">
                    Acceptance Progress Status - Summary</td>
            </tr>
            <tr>
                <td style="width: 225px; vertical-align: top; text-align: left;">
                    <asp:GridView ID="grdsummary" runat="server" AutoGenerateColumns="False" Width="354px">
                        <Columns>
                            <asp:BoundField DataField="status" HeaderText="Description" />
                            <asp:BoundField DataField="total" HeaderText="No of Sites" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td style="width: 286px; vertical-align: top; text-align: left;">
                    <asp:GridView ID="grdaccprogress" runat="server" AutoGenerateColumns="False" Width="237px">
                        <Columns>
                            <asp:BoundField DataField="status" HeaderText="Description" />
                            <asp:BoundField DataField="total" HeaderText="No of Sites" />
                        </Columns>
                        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 225px" class="pageTitleSub">
                   Acceptance Progress Status - Regional</td>
                <td style="width: 286px; vertical-align: top; text-align: left;" class="pageTitleSub">
                     Acceptance Status - Regional</td>
            </tr>
            <tr>
                <td style="width: 225px; vertical-align: top; text-align: left;">
                   <asp:GridView ID="grdregaccprostatus" runat="server" Width="274px">
                    </asp:GridView>  
                </td>
                <td style="width: 286px">
                   
                    <asp:GridView ID="grdregaccstatus" runat="server" AutoGenerateColumns="False" Width="241px">
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="delay" />
                            <asp:BoundField DataField="No Of Sites" HeaderText="No Of Sites" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 225px; vertical-align: top; text-align: left;" class="pageTitleSub">
                    Acceptance Status - Final</td>
                <td style="width: 286px">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left;" colspan="2">
                    <asp:GridView ID="grdaccstatusfinal" runat="server" AutoGenerateColumns="False" Width="627px">
                     <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="delay" />
                            <asp:BoundField DataField="No Of Sites" HeaderText="No Of Sites" />
                            <asp:BoundField DataField="USD" HeaderText="Value in USD" />
                            <asp:BoundField DataField="IDR" HeaderText="Value in IDR" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            
        </table>
    
    </div>
    </form>
</body>
</html>
