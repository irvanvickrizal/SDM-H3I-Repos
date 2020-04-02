<%@ Control Language="VB"   AutoEventWireup="false" CodeFile="LDsummary.ascx.vb" Inherits="LDsummary" %>
&nbsp;<asp:Label ID="lblregion" runat="server" Width="230px"></asp:Label>
<asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False"
    EmptyDataText="No Data" Width="58%">
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
               <asp:BoundField DataField="custpono" HeaderText="PO No.">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="total" HeaderText="Total">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="remaining2" HeaderText="Site LD's Approaching">
            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="ontime2" HeaderText="Site LD's None">
            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="latenotdone2" HeaderText="Site LD's Future">
            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
        </asp:BoundField>
        <asp:BoundField DataField="latedone2" HeaderText="Site LD's Actual">
            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="True" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
