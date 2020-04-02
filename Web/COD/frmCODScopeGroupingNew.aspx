<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODScopeGroupingNew.aspx.vb"
    Inherits="COD_frmCODScopeGroupingNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scope Detail</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/plugins/iCheck/flat/blue.css" />
    <link rel="stylesheet" href="~/plugins/morris/morris.css" />
    <link rel="stylesheet" href="~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link href="../css/Pagination.css" rel="stylesheet" />
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }
        .HeaderGrid
        {
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
        .oddGrid
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
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
        <asp:UpdatePanel ID="UpGvScope" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title"></h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Group by</label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DdlScopes" runat="server" CssClass="form-control" AutoPostBack="true" />
								                </div>
							                </div>
						                </div>
                                        <asp:GridView ID="GvScopeGrouping" runat="server" AutoGenerateColumns="false" DataKeyNames="DScope_Id"
                                            AllowPaging="true" PageSize="8" OnRowCancelingEdit="gvscopegrouping_RowCancelingEdit" CssClass="table table-bordered table-condensed"
                                            EmptyDataText="No Data Found" CellSpacing="2" CellPadding="2" OnRowDeleting="gvscopegrouping_RowDeleting"
                                            OnRowEditing="gvscopegrouping_RowEditing" HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="gvscopegrouping_RowUpdating">
                                            <RowStyle CssClass="oddGrid" />
                                            <AlternatingRowStyle CssClass="evenGrid" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle CssClass="customPagination" />
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
                                                <asp:TemplateField ShowHeader="true" HeaderText="Scope Name">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtGvDScopeName" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                                            Width="250px" Text='<%#Eval("DScope_Name") %>'> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGvScopeName" runat="server" ControlToValidate="TxtGvDScopeName"
                                                            Text="*" ValidationGroup="gvscope"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblScopeNameDetail" runat="server" Text='<%#Eval("DScope_Name") %>'
                                                            CssClass="lblField" Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtGvDScopeDesc" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                                            TextMode="MultiLine" Height="30px" Text='<%#Eval("DScope_Description") %>' Width="350px"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblScopeDesc" runat="server" Text='<%#Eval("DScope_Description") %>'
                                                            CssClass="lblField" Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="true" HeaderText="Scope Master">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="DdlScope" runat="server" CssClass="lblField">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LblScopeId" runat="server" Text='<%#Eval("GScope_Id") %>' Visible="false"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblScopeName" runat="server" Text='<%#Eval("Scope_Name") %>' CssClass="lblField"
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
					                </div>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Detail Scope Grouping</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Detail Scope Name</label>
								                <div class="col-sm-6">
                                                    <asp:TextBox ID="TxtDetailScopeName" runat="server" CssClass="form-control" ValidationGroup="subcon" />
								                </div>
							                </div>
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Description</label>
								                <div class="col-sm-6">
                                                    <asp:TextBox ID="TxtScopeDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
								                </div>
							                </div>
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Scope</label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DdlMasterScope" runat="server" CssClass="form-control" />
								                </div>
							                </div>
							                <div class="form-group">
								                <div class="col-sm-3 "></div>
								                <div class="col-sm-6">
                                                    <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="scope" CssClass="btn btn-block btn-primary" />
								                </div>
							                </div>
						                </div>
					                </div>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvScopeGrouping" />
            </Triggers>
        </asp:UpdatePanel>
    <asp:RequiredFieldValidator ID="rfvScopeName" runat="server" ControlToValidate="TxtDetailScopeName"
        ErrorMessage="Please fill Scope name Field" ValidationGroup="scope"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="vsScope" runat="server" DisplayMode="List" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="scope" />
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div>
        <div>
            <span class="ltrLabel">Group by</span>:&nbsp;<asp:DropDownList ID="DdlScopes" runat="server"
                CssClass="lblField" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div style="margin-top: 5px;">
            <asp:UpdatePanel ID="UpGvScope" runat="server">
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
                            <asp:TemplateField ShowHeader="true" HeaderText="Scope Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtGvDScopeName" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                        Width="250px" Text='<%#Eval("DScope_Name") %>'> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGvScopeName" runat="server" ControlToValidate="TxtGvDScopeName"
                                        Text="*" ValidationGroup="gvscope"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblScopeNameDetail" runat="server" Text='<%#Eval("DScope_Name") %>'
                                        CssClass="lblField" Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtGvDScopeDesc" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                        TextMode="MultiLine" Height="30px" Text='<%#Eval("DScope_Description") %>' Width="350px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblScopeDesc" runat="server" Text='<%#Eval("DScope_Description") %>'
                                        CssClass="lblField" Width="350px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Scope Master">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DdlScope" runat="server" CssClass="lblField">
                                    </asp:DropDownList>
                                    <asp:Label ID="LblScopeId" runat="server" Text='<%#Eval("GScope_Id") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblScopeName" runat="server" Text='<%#Eval("Scope_Name") %>' CssClass="lblField"
                                        Width="350px"></asp:Label>
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
    <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px;
        border-color: Gray;">
        <div style="background-color: #c3c3c3; padding: 3px;">
            <span class="lblField">Detail Scope Grouping</span>
        </div>
        <table>
            <tr>
                <td class="ltrLabel">
                    <asp:Literal ID="LtrScopeName" runat="server" Text="Detail Scope Name"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="TxtDetailScopeName" runat="server" CssClass="lblField" Width="250px"
                        ValidationGroup="subcon"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ltrLabel">
                    <asp:Literal ID="LtrDescription" runat="server" Text="Description"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="TxtScopeDescription" runat="server" CssClass="lblField" Height="40px"
                        TextMode="MultiLine" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ltrLabel">
                    <asp:Literal ID="Literal1" runat="server" Text="Scope"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="DdlMasterScope" runat="server" CssClass="lblField">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div style="text-align: right; margin-top: 5px;">
            <asp:RequiredFieldValidator ID="rfvScopeName" runat="server" ControlToValidate="TxtDetailScopeName"
                ErrorMessage="Please fill Scope name Field" ValidationGroup="scope"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="vsScope" runat="server" DisplayMode="List" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="scope" />
            <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="scope" />
        </div>
    </div>
    </form>--%>
</body>
</html>
