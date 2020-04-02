<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODBAUTSIT.aspx.vb" Inherits="COD_frmCODBAUTSIT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAUT SIT</title>
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

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10pt;
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
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="background-color: #c3c3c3; padding: 8px;">
            <span class="lblFieldHeader">Milestone Achievement Base on Scope Configuration</span>
        </div>
        <div style="margin-top:10px;">
            <asp:UpdatePanel ID="UpGvScope" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvScopeBAUTSIT" runat="server" AutoGenerateColumns="false" DataKeyNames="MSIT_Id"
                        Width="450px" OnRowCancelingEdit="GvScopeBAUTSIT_RowCancelingEdit" EmptyDataText="No Record Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="GvScopeBAUTSIT_RowDeleting" OnRowEditing="GvScopeBAUTSIT_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="GvScopeBAUTSIT_RowUpdating">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <EmptyDataRowStyle CssClass="evenGrid" />
                        <Columns>
                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                        ToolTip="Update" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to Update?');" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                        ToolTip="Edit" Height="16px" Width="16px" />
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                        ToolTip="Delete" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to delete?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DScope_Name" HeaderText="Scope Name" ReadOnly="true" />
                            <asp:TemplateField ShowHeader="true" HeaderText="BAUT Approved" ItemStyle-Width="30px"
                                ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DdlApprovedDocEdit" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="Not Required" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="BAUT Approved" Value="1031"></asp:ListItem>
                                        <asp:ListItem Text="QC Approved" Value="2025"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DdlApprovedDoc" runat="server" CssClass="ltrLabel">
                                        <asp:ListItem Text="Not Required" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="BAUT Approved" Value="1031"></asp:ListItem>
                                        <asp:ListItem Text="QC Approved" Value="2025"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top: 5px;">
                        <asp:Label ID="LblErrMessageGv" runat="server" CssClass="lblField"></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvScopeBAUTSIT" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <hr />
        </div>
        <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px; border-color: Gray;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">Detail Scope Grouping</span>
            </div>
            <table>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="MasterScope" runat="server" Text="Master Scope"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMasterScope" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrDetailScope" runat="server" Text="Detail Scope"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlDScope" runat="server" CssClass="ltrLabel">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LblApprovedDocName" runat="server" Text="Document Dependency"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlDocs" runat="server" CssClass="ltrLabel">
                            <asp:ListItem Text="Not Required" Value="0"></asp:ListItem>
                            <asp:ListItem Value="1031" Text="BAUT Approved"></asp:ListItem>
                            <asp:ListItem Value="2025" Text="QC Approved"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="margin-top: 5px;">
                <asp:Label ID="LblErrMessage" runat="server" CssClass="lblField"></asp:Label>
            </div>
            <div style="text-align: right; margin-top: 5px;">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="scope" />
            </div>
        </div>
    </form>
</body>
</html>
