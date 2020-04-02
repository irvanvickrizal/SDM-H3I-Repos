<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODScopeNew.aspx.vb" Inherits="COD_frmCODScopeNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scope Master</title>
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
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Master Scope List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-9">
                                <asp:UpdatePanel ID="UpGvScope" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GvMasterScope" runat="server" AutoGenerateColumns="false" DataKeyNames="GScope_Id" AllowPaging="true" PageSize="10"
                                            OnRowCancelingEdit="gvmscope_RowCancelingEdit" EmptyDataText="No Data Found" OnRowDeleting="gvmscope_RowDeleting" 
                                            OnRowEditing="gvmscope_RowEditing" OnRowUpdating="gvmscope_RowUpdating" CssClass="table table-bordered table-condensed">
                                            <RowStyle CssClass="oddGrid" />
                                            <HeaderStyle CssClass="HeaderGrid" />
                                            <AlternatingRowStyle CssClass="evenGrid" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
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
                                                        <asp:TextBox ID="TxtGvScopeName" runat="server" cssClass="form-control" ValidationGroup="gvscope"
                                                             Text='<%#Eval("Scope_Name") %>'> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGvScopeName" runat="server" ControlToValidate="TxtGvScopeName"
                                                            Text="*" ValidationGroup="gvscope"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblScopeName" runat="server" Text='<%#Eval("Scope_Name") %>' CssClass="lblField"
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TxtGvScopeDesc" runat="server" ValidationGroup="gvscope"
                                                            TextMode="MultiLine" CssClass="form-control" Text='<%#Eval("Description") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGvScopeDescName" runat="server" ControlToValidate="TxtGvScopeName"
                                                            Text="*" ValidationGroup="gvsubcons"></asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblScopeDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblField"
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GvMasterScope" />
                                    </Triggers>
                                </asp:UpdatePanel>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="row">
            <div class="col-sm-4"><span style="display:none"></span></div>
	        <div class="col-sm-4">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Add New Master Scope</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-1"><span style="display:none"></span></div>
					        <div class="col-md-9">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-4 control-label">Scope Name</label>
								        <div class="col-sm-7">
                                            <asp:TextBox ID="TxtScopeName" runat="server" CssClass="form-control" ValidationGroup="subcon" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-4 control-label">Description</label>
								        <div class="col-sm-7">
                                            <asp:TextBox ID="TxtScopeDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-4"></div>
								        <div class="col-sm-7">
                                            <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-block btn-primary" Text="Add" ValidationGroup="scope" />
								        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
			        <asp:RequiredFieldValidator ID="rfvScopeName" runat="server" ControlToValidate="TxtScopeName"
                        ErrorMessage="Please fill Scope name Field" ValidationGroup="scope"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="vsScope" runat="server" DisplayMode="List" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="scope" />
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpGvScope" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GvMasterScope" runat="server" AutoGenerateColumns="false" DataKeyNames="GScope_Id"
                        Width="450px" OnRowCancelingEdit="gvmscope_RowCancelingEdit" EmptyDataText="No Data Found"
                        CellSpacing="2" CellPadding="2" OnRowDeleting="gvmscope_RowDeleting" OnRowEditing="gvmscope_RowEditing"
                        HeaderStyle-CssClass="HeaderGrid" OnRowUpdating="gvmscope_RowUpdating">
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
                                    <asp:TextBox ID="TxtGvScopeName" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                        Width="250px" Text='<%#Eval("Scope_Name") %>'> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGvScopeName" runat="server" ControlToValidate="TxtGvScopeName"
                                        Text="*" ValidationGroup="gvscope"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblScopeName" runat="server" Text='<%#Eval("Scope_Name") %>' CssClass="lblField"
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtGvScopeDesc" runat="server" CssClass="lblField" ValidationGroup="gvscope"
                                        TextMode="MultiLine" Height="30px" Text='<%#Eval("Description") %>' Width="350px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvGvScopeDescName" runat="server" ControlToValidate="TxtGvScopeName"
                                        Text="*" ValidationGroup="gvsubcons"></asp:RequiredFieldValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblScopeDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblField"
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvMasterScope" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="width: 350px; margin-top: 10px; border-style: solid; border-width: 1px;
            border-color: Gray;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">New Master Scope</span>
            </div>
            <table>
                <tr>
                    <td class="ltrLabel">
                        <asp:Literal ID="LtrScopeName" runat="server" Text="Scope Name"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtScopeName" runat="server" CssClass="lblField" Width="250px"
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
            </table>
            <div style="text-align: right; margin-top: 5px;">
                <asp:RequiredFieldValidator ID="rfvScopeName" runat="server" ControlToValidate="TxtScopeName"
                    ErrorMessage="Please fill Scope name Field" ValidationGroup="scope"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="vsScope" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="scope" />
                <asp:Button ID="BtnAdd" runat="server" Text="Add" ValidationGroup="scope" />
            </div>
        </div>
    </form>--%>
</body>
</html>
