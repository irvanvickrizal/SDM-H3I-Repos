<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DashBoardSubConSubAdmin.ascx.vb" Inherits="Include_DashBoardSubConSubAdmin" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr style="border-right: 0px; border-top: 0px; background-image: url(Images/newpixal.jpg);
        border-left: 0px; border-bottom: 0px; background-repeat: repeat-x; height: 33px">
        <td valign="top">
            <img alt="" src="Images/ov.jpg" />
        </td>
    </tr>
    <tr><td class="hgap"></td></tr>
    <tr>
        <td>
            <asp:GridView ID="grdDashBoard" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="SEC_Id" EmptyDataText="No Records Found"
                PageSize="5" Width="100%">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText="PO #">
                        <ItemTemplate>
                            <a href="#" onclick="DashBoard('<%# DataBinder.Eval(Container.DataItem,"RegionName") %>')">
                                <%# DataBinder.Eval(Container.DataItem,"RegionName") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Site">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalSite" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalSite") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Section">
                        <ItemTemplate>
                            <asp:Label ID="lblSection" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Sec_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblUploaded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocCount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NSN Approved">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblNSNApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NsnApprove") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Approved">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustomerApprove") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Complete") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remaining">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemainingApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remaining") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle"/>
                        <ItemTemplate>
                            <asp:Label ID="lblPrentageTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalPerc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
