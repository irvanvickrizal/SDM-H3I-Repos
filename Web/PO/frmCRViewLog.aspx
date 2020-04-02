<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCRViewLog.aspx.vb" Inherits="PO_frmCRViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <title>CR View Log</title>
    <style type="text/css">
        .GridHeaderStyle
        {
            padding:5px;
            color:#ffffff;
            background-color:Orange;
            font-family:verdana;
            font-size:12px;
            font-weight:bolder;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="Td1">
                        CR View Log
                    </td>
                </tr>
                <tr class="lblText">
                    <td style="width: 20%">
                        CR No</td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2" id="tdcrno" runat="server">
                    </td>
                </tr>
                <tr class="lblText">
                    <td style="width: 20%">
                        Po No</td>
                    <td style="width: 1%">
                        :
                    </td>
                    <td colspan="2" id="tdpono" runat="server">
                    </td>
                </tr>
                <tr class="lblText">
                    <td>
                        PO Name</td>
                    <td>
                        :</td>
                    <td colspan="2" id="tdponame" runat="server">
                    </td>
                </tr>
                <tr class="lblText">
                    <td>
                        Site No</td>
                    <td>
                        :</td>
                    <td colspan="2" id="tdsiteno" runat="server">
                    </td>
                </tr>
                <tr class="lblText">
                    <td>
                        Site Name</td>
                    <td>
                        :</td>
                    <td colspan="2" id="tdsitename" runat="server">
                    </td>
                </tr>
                <tr class="lblText">
                    <td>
                        Scope</td>
                    <td>
                        :</td>
                    <td runat="server" id="tdscope" colspan="2">
                    </td>
                </tr>
                <tr class="lblText">
                    <td>
                        Work Package ID</td>
                    <td>
                        :</td>
                    <td colspan="2" id="tdwpid" runat="server">
                    </td>
                </tr>
                <tr class="lblText" valign="top">
                    <td>
                        View Log</td>
                    <td>
                        :</td>
                    <td id="Td2" colspan="2" runat="server" align="left">
                    </td>
                </tr>
                <tr align="left">
                    <td colspan="4" align="left">
                        <div style="overflow: auto; height: 250px">
                            <asp:MultiView ID="MvCorePanel" runat="server">
                                <asp:View ID="VwGeneralLog" runat="server">
                                    <asp:GridView ID="gvViewLog" runat="server" BackColor="White" CssClass="GridOddRows"
                                        BorderWidth="1px" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false"
                                        EmptyDataText="No Records Found">
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total " ItemStyle-BorderWidth="1">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="AuditInfo"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Role" DataField="SignTitle"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStartTime"
                                                DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEndTime"
                                                DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Categories" DataField="Categories"
                                                ItemStyle-BorderWidth="1" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="VwSummaryLog" runat="server">
                                    <asp:GridView ID="GvSummaryViewLog" runat="server" BackColor="White" CssClass="GridOddRows"
                                        BorderWidth="1px" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="false"
                                        EmptyDataText="No Records Found">
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="GridHeader" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total " ItemStyle-BorderWidth="1">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="CRNo" DataField="CRNo"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="AuditInfo"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Role" DataField="SignTitle"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStartTime"
                                                DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEndTime"
                                                DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks"
                                                ItemStyle-BorderWidth="1" />
                                            <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Categories" DataField="Categories"
                                                ItemStyle-BorderWidth="1" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <input type="button" runat="server" id="btnClose" class="buttonStyle" value="Close"
                            onclick="javascript:window.close()" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
