<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOView.aspx.vb" Inherits="PO_frmPOView"
    EnableEventValidation="False" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PO View</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />

    <script language="javascript" type="text/javascript">
     function Validate()
     {
     var ddlPO = document.getElementById('<%=ddlPO.ClientID%>');     
     
      if(ddlPO.selectedIndex ==0)
      {
        alert("Select the PO Number");
        return false;
      }  
        return true;
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="updPanel" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
                <ContentTemplate>
                    <table width="100%">
                        <tr class="pageTitle">
                            <td runat="server" id="rowTitle" colspan="3">
                                Purchase Order List</td>
                        </tr>
                        <tr>
                            <td class="lblTitle" style="width: 20%">
                                Select PONo</td>
                            <td width="1%">
                                :</td>
                            <td style="width: 1077px">
                                <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                                </asp:DropDownList>&nbsp;&nbsp;
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="buttonStyle"
                                    Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    Width="100%" AllowSorting="True">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Total Sites">
                                            <ItemTemplate>
                                                <%# container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="10px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PoNo" HeaderText="Customer PO">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkPkgId" HeaderText="WPID">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SiteNo" HeaderText="Site ID">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FldType" HeaderText="FldType(scope+Reason)" HtmlEncode="False">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="250px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sitefrom" HeaderText="Site From" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="grdExport" runat="server" AutoGenerateColumns="False" Width="100%"
                                    Visible="false" AllowSorting="True">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                                        HorizontalAlign="Right" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Total Sites">
                                            <ItemTemplate>
                                                <%# container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" Width="10px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PoNo" HeaderText="Customer PO">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkPkgId" HeaderText="WPID">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SiteNo" HeaderText="Site ID">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FldType" HeaderText="FldType(scope+Reason)" HtmlEncode="False">
                                            <HeaderStyle VerticalAlign="Middle" />
                                            <ItemStyle Width="250px" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sitefrom" HeaderText="Site From" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
