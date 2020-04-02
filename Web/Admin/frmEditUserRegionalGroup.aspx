<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEditUserRegionalGroup.aspx.vb"
    Inherits="Admin_frmEditUserRegionalGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Regional User Group</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GvRegions" runat="server" AutoGenerateColumns="false" Width="255px"
            ShowHeader="false" GridLines="None">
            <AlternatingRowStyle CssClass="evenGrid" />
            <RowStyle CssClass="oddGrid" />
            <HeaderStyle CssClass="HeaderGrid" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <input id="ChkReview" runat="server" type="checkbox" />
                        <asp:Label ID="LblRgnId" runat="server" Text='<%#Eval("RgnId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RgnName" ReadOnly="true" HeaderText="Region" />
            </Columns>
        </asp:GridView>
    </div>
    <div style="margin-top: 10px;">
        <asp:Button ID="BtnSave" runat="server" Text="Save" />
    </div>
    </form>
</body>
</html>
