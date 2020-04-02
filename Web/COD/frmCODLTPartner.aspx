<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODLTPartner.aspx.vb"
    Inherits="COD_frmCODLTPartner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Master LeadTime Partner</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="background-color: #c3c3c3; padding: 8px;">
            <span class="lblFieldHeader">LT Partner Base on Scope Configuration</span>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdatePanel ID="UpGvScope" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvScopeLTPartner" runat="server" AutoGenerateColumns="false" DataKeyNames="LT_ID"
                        Width="500px" OnRowCancelingEdit="GvScopeLTPartner_RowCancelingEdit" EmptyDataText="No Record Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="GvScopeLTPartner_RowDeleting"
                        OnRowEditing="GvScopeLTPartner_RowEditing" HeaderStyle-CssClass="HeaderGrid"
                        OnRowUpdating="GvScopeLTPartner_RowUpdating">
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
                            <asp:TemplateField ShowHeader="true" HeaderText="Lead Time" ItemStyle-Width="60px"
                                ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtLeadTimeEdit" runat="server" CssClass="ltrLabel" Text='<%#Eval("LT_Value") %>'
                                        Width="30px"></asp:TextBox>
                                    <asp:Label ID="LblDScopeID" runat="server" Text='<%#Eval("Dscope_Id") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblLeadTime" runat="server" Text='<%#Eval("LT_Value") %>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Type of Work" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DdlActivitiesEdit" runat="server" CssClass="ltrLabel"></asp:DropDownList>
                                    <asp:Label ID="LblActivityIdEdit" runat="server" Text='<%#Eval("Activity_Id") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblActivity" runat="server" Text='<%#Eval("Activity_Name") %>' CssClass="ltrLabel"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top: 5px;">
                        <asp:Label ID="LblErrMessageGv" runat="server" CssClass="lblField"></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvScopeLTPartner" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <hr />
        </div>
        <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px; border-color: Gray;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">Add / Update LT Partner</span>
            </div>
            <table>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LblActiviy" runat="server" Text="Scope Of Work"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlActivity" runat="server" CssClass="ltrLabel" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
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
                        <asp:Literal ID="LblLTValue" runat="server" Text="LT Value"></asp:Literal>
                    </td>
                    <td>
                        <input id="TxtLTValue" type="text" runat="Server" class="ltrLabel" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                            style="width: 40px;" /><span class="ltrLabel">Day(s)</span>
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
