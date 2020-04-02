<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SLADetails.aspx.vb" Inherits="DashBoard_SLADetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SLA Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="1" cellpadding="1">
            <tr>
                <td class="lblTitle" style="width: 15%">
                
                </td>
                <td style="width: 1%">
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
                        AllowPaging="true">
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
                            <asp:BoundField DataField="custPoNo" HeaderText="PoNo" />
                             <asp:BoundField DataField="Siteid" HeaderText="Site No" />
                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="WPID" />    
                            <asp:BoundField DataField="FldType" HeaderText="Scope" />            
                            <asp:BoundField DataField="PlannedDate" HeaderText="Planned Date" />
                            <asp:BoundField DataField="ActualDate" HeaderText="Actual  Date" />                          
                            <asp:BoundField DataField="CurrentDays" HeaderText="Current days" />
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
                              <asp:BoundField DataField="custPoNo" HeaderText="PoNo" />
                             <asp:BoundField DataField="Siteid" HeaderText="Site No" />
                            <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="WPID" />
                            <asp:BoundField DataField="FldType" HeaderText="Scope" />  
                            <asp:BoundField DataField="PlannedDate" HeaderText="Planned Date" />
                            <asp:BoundField DataField="ActualDate" HeaderText="Actual  Date" />                          
                            <asp:BoundField DataField="CurrentDays" HeaderText="Current days" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
