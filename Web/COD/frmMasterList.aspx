<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMasterList.aspx.vb" Inherits="COD_frmMasterList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scope Detail</title>
    <style type="text/css">
        .ltrLabel {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }

        .lblField {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }

        .HeaderGrid {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
        }

        .oddGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .HeaderPanel {
            padding: 8px;
            width: 99%;
            background-color: #124191;
            border-style: solid;
            border-color: gray;
            border-width: 1px;
            font-family: Verdana;
            font-size: 14px;
            margin-top: -10px;
            font-weight: bolder;
            color: #ffffff;
            margin-left: -10px;
        }
    </style>

    <script type="text/javascript">
        function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete ' + desc + ' ?');
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
        <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px; border-color: Gray;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">Master List Detail</span>
            </div>
            <table>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrScopeName" runat="server" Text="SN"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSN" runat="server" CssClass="lblField" Width="250px"
                            ValidationGroup="subcon"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrDescription" runat="server" Text="Doc Name"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtDocName" runat="server" CssClass="lblField" Height="40px"
                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="Literal2" runat="server" Text="Serial Order"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtSerialOrder" runat="server" CssClass="lblField" Height="40px"
                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="Literal1" runat="server" Text="Parent Doc"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlParentName" runat="server" CssClass="lblField">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="text-align: right; margin-top: 5px;">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="scope" />
            </div>
        </div>
        <div class="HeaderPanel">
            Master List
        </div>
        <div>
            <div>
                <span class="ltrLabel">Group by</span>:&nbsp;<asp:DropDownList ID="DdlScopes" runat="server" CssClass="lblField" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div style="margin-top: 5px;">
                    <ContentTemplate>
                        <asp:GridView ID="GvScopeGrouping" runat="server" AutoGenerateColumns="false" DataKeyNames="DScope_Id"
                            AllowPaging="true" PageSize="8" Width="450px" OnRowCancelingEdit="gvscopegrouping_RowCancelingEdit"
                            EmptyDataText="No Data Found" CellSpacing="2" CellPadding="2" OnRowDeleting="gvscopegrouping_RowDeleting"
                            OnRowEditing="gvscopegrouping_RowEditing" HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="gvscopegrouping_RowUpdating">
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
                                <asp:TemplateField ShowHeader="true" HeaderText="SN">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtGvDScopeName" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                            Width="20px" Text='<%#Eval("SN") %>'> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGvScopeName" runat="server" ControlToValidate="TxtGvDScopeName"
                                            Text="*" ValidationGroup="gvscope"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblScopeNameDetail" runat="server" Text='<%#Eval("SN") %>'
                                            CssClass="lblField" Width="20px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="true" HeaderText="Doc Name">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtGvDScopeDesc" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                            TextMode="MultiLine" Height="30px" Text='<%#Eval("DocName") %>' Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblScopeDesc" runat="server" Text='<%#Eval("DocName") %>'
                                            CssClass="lblField" Width="350px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvScopeGrouping" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
