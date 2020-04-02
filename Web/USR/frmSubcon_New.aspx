<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubcon_New.aspx.vb" Inherits="USR_frmSubcon_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subcon Registration</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family:verdana;
            font-size:8pt;
            color:#000;
        }
        .lblField
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:#000;
        }
         .HeaderGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        font-weight: bold;
        color:white;
        background-color:Orange;
        border-color:black;
        border-style:solid;
        border-width:1px;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color: White;
        border-color:black;
        border-style:solid;
        border-width:1px;
    }
    .evenGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color:#cfcfcf;
        border-color:black;
        border-style:solid;
        border-width:1px;
    }
    </style>

    <script type="text/javascript">
        function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete '+desc+' ?' );
            if (result) {
            return true;
            }
            else {
            return false;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="width: 450px;">
            <asp:UpdatePanel ID="UpSubcon" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvSubcon" runat="server" AutoGenerateColumns="false" DataKeyNames="Subcon_Id"
                        Width="450px" OnRowCancelingEdit="gvsubcon_RowCancelingEdit" EmptyDataText="No Data Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="gvsubcon_RowDeleting" OnRowEditing="gvsubcon_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="gvsubcon_RowUpdating">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <Columns>
                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                        ToolTip="Update" Height="16px" Width="16px" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                        ToolTip="Edit" Height="16px" Width="16px" />
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                        ToolTip="Delete" Height="16px" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Subcon Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtGvSubconName" runat="server" CssClass="lblField" ValidationGroup="gvsubcons"
                                        Width="250px" Text='<%#Eval("Subcon_Name") %>'> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGvSubconName" runat="server" ControlToValidate="TxtGvSubconName"
                                        Text="*" ValidationGroup="gvsubcons"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSubconName" runat="server" Text='<%#Eval("Subcon_Name") %>' CssClass="lblField"
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtGvSubconDesc" runat="server" CssClass="lblField" ValidationGroup="gvsubcons"
                                        TextMode="MultiLine" Height="30px" Text='<%#Eval("Description") %>' Width="350px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGvSubconDesc" runat="server" ControlToValidate="TxtGvSubconDesc"
                                        Text="*" ValidationGroup="gvsubcons"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSubconDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblField"
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvSubcon" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px;
            border-color: Gray;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">New Subcon</span>
            </div>
            <table>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrSubconName" runat="server" Text="Subcon Name"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSubconName" runat="server" CssClass="lblField" Width="250px"
                            ValidationGroup="subcon"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrSubconDescription" runat="server" Text="Description"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSubconDescription" runat="server" CssClass="lblField" Height="40px"
                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="text-align: right; margin-top: 5px;">
                <asp:RequiredFieldValidator ID="rfvSubconName" runat="server" ControlToValidate="TxtSubconName"
                    ErrorMessage="Please fill Subcon name Field" ValidationGroup="subcon"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="vsSubcon" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="subcon" />
                <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="subcon" />
            </div>
        </div>
    </form>
</body>
</html>
