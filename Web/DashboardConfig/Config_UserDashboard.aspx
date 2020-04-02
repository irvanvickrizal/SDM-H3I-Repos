<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Config_UserDashboard.aspx.vb"
    Inherits="DashboardConfig_Config_UserDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard User Configuration</title>
    <style type="text/css">
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
        font-size: 9pt;
        background-color:#cfcfcf;
        border-color:black;
        border-style:solid;
        border-width:1px;
    }
        .HeaderPanel
        {
           width:100%;
           background-repeat: repeat-x;
           background-image: url(../Images/banner/BG_Banner.png);
           font-family:verdana;
           font-weight:bolder;
           font-size:10pt;
           color:white;
           padding-top:5px;
           padding-bottom:5px;
        }
        .lblSubHeader
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            font-weight:bolder;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:10pt;
            font-weight:bolder;
        }
         .lblBText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblCheckText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
        }
        
        .evengrid2
        {
            background-color: #cfcfcf;
        }
        .btnSave
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_0.gif);
        }
        .btnSave:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_1.gif);
        }
        .btnSave:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_2.gif);
        }
        .btnCancel
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_0.gif);
        }
        .btnCancel:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_1.gif);
        }
        .btnCancel:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_2.gif);
        }      
        .clearDiv
        {
            height:5px;
        }
    </style>

    <script type="text/javascript">
         function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete dashboard configuration for '+desc+' ?' );
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
        <div>
            <asp:UpdatePanel ID="UpDashboardConfig" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvDashboardConfig" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                        DataKeyNames="config_Id" Width="450px" OnRowCancelingEdit="gvdashboardconfig_RowCancelingEdit"
                        EmptyDataText="No Data Found" CellSpacing="2" CellPadding="2" OnRowDeleting="gvdashboardconfig_RowDeleting"
                        OnRowEditing="gvdashboardconfig_RowEditing">
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
                            <asp:TemplateField HeaderText="Role Desc">
                                <ItemTemplate>
                                    <asp:Label ID="LblRoleDesc" runat="server" Text='<%#Eval("RoleDesc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="LblRoleId" runat="server" Text='<%#Eval("role_id") %>'></asp:Label>
                                    <asp:DropDownList ID="DdlRoles" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="General TaskPending">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkGeneralTaskPendingRO" runat="server" Checked='<%#Eval("General_TaskPending") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkGeneralTaskPending" runat="server" Checked='<%#Eval("General_TaskPending") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ATP TaskPending">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkATPTaskPendingRO" runat="server" Checked='<%#Eval("ATP_TaskPending") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkATPTaskPending" runat="server" Checked='<%#Eval("ATP_TaskPending") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CR CO TaskPending">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkCRCOTaskPendingRO" runat="server" Checked='<%#Eval("CRCO_TaskPending") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkCRCOTaskPending" runat="server" Checked='<%#Eval("CRCO_TaskPending") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QC TaskPending">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkQCTaskPendingRO" runat="server" Checked='<%#Eval("QC_TaskPending") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkQCTaskPending" runat="server" Checked='<%#Eval("QC_TaskPending") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WCC TaskPending">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkWCCTaskPendingRO" runat="server" Checked='<%#Eval("WCC_TaskPending") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkWCCTaskPending" runat="server" Checked='<%#Eval("WCC_TaskPending") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CRCO Dashboard">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkCRCODashboardRO" runat="server" Checked='<%#Eval("CRCO_Dashboard") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkCRCODashboard" runat="server" Checked='<%#Eval("CRCO_Dashboard") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="General Dashboard">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkGeneralDashboardRO" runat="server" Checked='<%#Eval("General_Dashboard") %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ChkGeneralDashboard" runat="server" Checked='<%#Eval("General_Dashboard") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LMBY" HeaderText="Last Modified By" />
                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvDashboardConfig" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="LblRole" runat="server" Text="Role"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlNewRoles" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblATPTaskPending" runat="server" Text="ATP TaskPending"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkATPTP" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LlblQCTaskPending" runat="server" Text="QC TaskPending"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkQCTP" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblCRCOTaskPending" runat="server" Text="CRCO TaskPending"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkCRCOTP" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblWCCTaskPending" runat="server" Text="WCC TaskPending"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkWCCTP" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblGeneralDashboard" runat="server" Text="General Dashboard"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkGeneralDashboard" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblCRCODashboard" runat="server" Text="CRCO Dashboard"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkCRCODashboard" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblWCCDashboard" runat="server" Text="WCC Dashboard"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkWCCDashboard" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="BtnAddNew" runat="server" Text="Add New" />
            </div>
        </div>
    </form>
</body>
</html>
