<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODPackageName_Scope.aspx.vb"
    Inherits="COD_frmCODPackageName_Scope" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COD Package Name</title>
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
        .emptyRowStyle
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:maroon;
            border-style:solid;
            padding:3px;
            border-width:1px;
            border-color:gray;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:9pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:7.5pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:7.5pt;
        }
    </style>

    <script type="text/javascript">
        function FailedSaved() {
            alert('Data Failed to Save, Please Try Again!');
            return false;
        }
        function SucceedSaved() {
            alert('Data Successfully Save');
            return true;
        }

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
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Package Name List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-9">
                                <asp:GridView ID="GvPackageNames" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                    CssClass="table table-bordered table-condensed" AllowPaging="true" PageSize="10" DataKeyNames="PackageNameId">
                                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <SelectedRowStyle CssClass="GridEvenRows" />
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle CssClass="customPagination" />
                                    <Columns>
                                        <asp:TemplateField>
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
                                        <asp:TemplateField ShowHeader="true" HeaderText="Package Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPackageName" runat="server" cssClass="form-control" ValidationGroup="gvPckg"
                                                        Text='<%#Eval("PackageName") %>'> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGvPckgName" runat="server" ControlToValidate="txtPackageName"
                                                    Text="*" ValidationGroup="gvPckg"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackageName" runat="server" Text='<%#Eval("PackageName") %>' CssClass="lblField"
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Description">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPackageDesc" runat="server" ValidationGroup="gvPckg"
                                                    TextMode="MultiLine" CssClass="form-control" Text='<%#Eval("Description") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvGvPckgDesc" runat="server" ControlToValidate="txtPackageName"
                                                    Text="*" ValidationGroup="gvPckg"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hiddenId" runat="server" Value='<%#Eval("DScopeId")%>'/>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackageDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblField"
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DScopeName" HeaderText="Scope Detail" ReadOnly="true" ControlStyle-CssClass="lblField" />
                                        <asp:BoundField DataField="LMBY" HeaderText="Last Modified" ReadOnly="true" />
                                        <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ReadOnly="true" />
                                    </Columns>
                                </asp:GridView>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="row">
            <div class="col-sm-4"><span style="display:none"></span></div>
	        <div class="col-xs-4">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Add New Package Name</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-1"><span style="display:none"></span></div>
					        <div class="col-md-9">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-4 control-label">Package Name</label>
								        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtPackageName" runat="server" CssClass="form-control" ValidationGroup="CreatePackageName" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-4 control-label">Description</label>
								        <div class="col-sm-8">
                                            <asp:TextBox ID="TxtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" ValidationGroup="CreatePackageName" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-4 control-label">Scope Detail</label>
								        <div class="col-sm-8">
                                            <asp:DropDownList ID="DdlScopeDetails" runat="server" CssClass="form-control" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-4"></div>
								        <div class="col-sm-4">
                                            <asp:Button ID="BtnAdd" runat="server" Text="Create" ValidationGroup="CreatePackageName" CssClass="btn btn-block btn-primary" />
								        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="BtnClear" runat="server" Text="Clear" CausesValidation="false" CssClass="btn btn-block btn-warning" />
                                        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
                    <asp:RequiredFieldValidator ID="RfvPackageName" runat="server" ControlToValidate="TxtPackageName"
                        SetFocusOnError="true" Display="None" ErrorMessage="Please Fill Package Name"
                        ValidationGroup="CreatePackageName"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CvScopeDetails" ErrorMessage="Please Choose Scope Detail"
                        runat="server" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="DdlScopeDetails"
                        ValidationGroup="CreatePackageName" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="VsCreatePackageName" runat="server" ValidationGroup="CreatePackageName"
                        DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" />
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <asp:MultiView ID="MvCorePanel" runat="server">
            <asp:View ID="VwMainPanel" runat="server">
                <div>
                    <asp:GridView ID="GvPackageNames" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                        Width="600px">
                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                        <HeaderStyle CssClass="GridHeader" />
                        <RowStyle CssClass="GridOddRows" />
                        <SelectedRowStyle CssClass="GridEvenRows" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="editpackagename" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                        ToolTip="Edit" Height="16px" Width="16px" />
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="deletepackagename" runat="server" CommandArgument='<%#Eval("PackageNameId") %>'
                                        OnClientClick="return confirm('Are you sure you want to delete this Package Name ?')"
                                        ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PackageName" HeaderText="Package Name" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="DScopeName" HeaderText="Scope Detail" />
                            <asp:BoundField DataField="LMBY" HeaderText="Last Modified" />
                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="width: 360px; margin-top: 10px; border-style: solid; padding: 3px; border-color: #c3c3c3;">
                    <div style="background-color: #c3c3c3; padding: 3px;">
                        <span class="lblField">Add New Package Name</span>
                    </div>
                    <div style="margin-top: 2px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPackageName" runat="server" CssClass="ltrLabel" Text="Package Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPackageName" runat="server" CssClass="ltrLabel" Width="250px"
                                        ValidationGroup="CreatePackageName"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblDescription" runat="server" CssClass="ltrLabel" Text="Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtDescription" runat="server" CssClass="ltrLabel" TextMode="MultiLine"
                                        ValidationGroup="CreatePackageName" Width="250px" Height="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblScope" runat="server" CssClass="ltrLabel" Text="Scope Detail"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlScopeDetails" runat="server" CssClass="ltrLabel">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin-top: 3px; text-align: right;">
                        <asp:RequiredFieldValidator ID="RfvPackageName" runat="server" ControlToValidate="TxtPackageName"
                            SetFocusOnError="true" Display="None" ErrorMessage="Please Fill Package Name"
                            ValidationGroup="CreatePackageName"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CvScopeDetails" ErrorMessage="Please Choose Scope Detail"
                            runat="server" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="DdlScopeDetails"
                            ValidationGroup="CreatePackageName" Display="None"></asp:CompareValidator>
                        <asp:ValidationSummary ID="VsCreatePackageName" runat="server" ValidationGroup="CreatePackageName"
                            DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" />
                        <asp:Button ID="BtnAdd" runat="server" Text="Create" ValidationGroup="CreatePackageName" />
                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CausesValidation="false" />
                    </div>
                </div>
            </asp:View>
            <asp:View ID="VwSecondPanel" runat="server">
                <div style="background-color: #c3c3c3; padding: 3px;">
                    <span class="lblField">Update Package Name</span>
                </div>
                <div>
                    <asp:HiddenField ID="HdnPackageNameId" runat="server" />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblPackageNameUpdate" runat="server" CssClass="ltrLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPackageNameUpdate" runat="server" CssClass="lblField" Width="250px"
                                    ValidationGroup="UpdatePackageName"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDescriptionUpdate" runat="server" CssClass="ltrLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDescriptionUpdate" runat="server" CssClass="lblField" TextMode="MultiLine"
                                    ValidationGroup="UpdatePackageName" Width="250px" Height="40px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblIsActive" runat="server" CssClass="ltrLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkIsActive" runat="server" CssClass="ltrLabel" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" ValidationGroup="UpdatePackageName" />
                    <asp:Button ID="BtnCancelUpdate" runat="server" Text="Cancel" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="rfvPackageNameUpdate" runat="server" ControlToValidate="TxtPackageNameUpdate"
                        Display="None" ErrorMessage="Please Fill Package Name" ValidationGroup="UpdatePackageName"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="vsupdate" runat="server" ValidationGroup="UpdatePackageName"
                        DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true" />
                </div>
            </asp:View>
            <asp:View ID="VwErrorMessage" runat="server">
            </asp:View>
        </asp:MultiView>
    </form>--%>
</body>
</html>
