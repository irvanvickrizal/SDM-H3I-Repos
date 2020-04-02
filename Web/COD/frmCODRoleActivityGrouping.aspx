<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODRoleActivityGrouping.aspx.vb"
    Inherits="COD_frmCODRoleActivityGrouping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity Role Grouping</title>
    <style type="text/css">
        .lblText
        {
            font-family:verdana;
            font-size:11px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
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
        .btnstyle
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            background-color:#c3c3c3;
            color:#fff;
            padding:8px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
    </style>

    <script type="text/javascript">
        function ErrorSave()  
        {
            alert("Error While Saving the Data, Please Try Again!");
            return false;
        }
        function SucceedSave()  
        {
            alert("Data Successful saved");
            return true;
        }
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info collapsed-box">
			        <div class="box-header with-border">
				        <h3 class="box-title">Role Activity Group</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                            </button>
                        </div>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <asp:GridView ID="GvActiviyRolesGroup" runat="server" EmptyDataText="No Record Found"
                                AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="10" CssClass="table table-bordered table-condensed">
                                <HeaderStyle CssClass="GridHeader" />
                                <RowStyle CssClass="GridOddRows" />
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <PagerSettings Position="TopAndBottom" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                                CommandName="DeleteRole" Width="20px" Height="20px" CommandArgument='<%#Eval("RoleActivityId") %>'
                                                OnClientClick="return confirm('Are you sure you want to delete this Role ?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                    <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                    <asp:BoundField DataField="ActivityName" HeaderText="ActivityName" />
                                    <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                    <asp:BoundField DataField="ModifiedUser" HeaderText="Last Modified By" />
                                    <asp:BoundField DataField="CDT" HeaderText="Create Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                        ConvertEmptyStringToNull="true" />
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <span style="display:none;height:10px" />
            </div>
	        <div class="col-md-7">
		        <div class="box box-success">
			        <div class="box-header with-border">
				        <h3 class="box-title">Create New Role Activity</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-3">
                                <span style="display:none;height:10px" />
                            </div>
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-3 control-label">User Type</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="DdlUserType" runat="server" CssClass="form-control" AutoPostBack="true"/>
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">User Role</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="DdlRoles" runat="server" CssClass="form-control" AutoPostBack="true" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Activity Type</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="DdlActivities" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn btn-block btn-primary" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-danger" />
                                    </div>
						        </div>
					        </div>
				        </div>
			        </div>
			        <div class="box-footer">
                        <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText" ForeColor="red" Visible="false" />
			        </div>
		        </div>
	        </div>
        </div>

        <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
        <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
        <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div>
            <asp:GridView ID="GvActiviyRolesGroup" runat="server" EmptyDataText="No Record Found"
                AutoGenerateColumns="false" Width="99%">
                <HeaderStyle CssClass="GridHeader" />
                <RowStyle CssClass="GridOddRows" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                CommandName="DeleteRole" Width="20px" Height="20px" CommandArgument='<%#Eval("RoleActivityId") %>'
                                OnClientClick="return confirm('Are you sure you want to delete this Role ?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleName" HeaderText="Role" />
                    <asp:BoundField DataField="UserType" HeaderText="User Type" />
                    <asp:BoundField DataField="ActivityName" HeaderText="ActivityName" />
                    <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    <asp:BoundField DataField="ModifiedUser" HeaderText="Last Modified By" />
                    <asp:BoundField DataField="CDT" HeaderText="Create Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                        ConvertEmptyStringToNull="true" />
                </Columns>
            </asp:GridView>
        </div>
        <div style="padding:5px; border-style:solid; border-width:2px; border-color:Gray;width:30%; margin-top:10px;">
            <table> 
                <tr>
                    <td>
                        <span class="lblBoldText">User Type</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlUserType" runat="server" CssClass="lblText" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="lblBoldText">User Role</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlRoles" runat="server" CssClass="lblText" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="lblBoldText">Activity Type</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlActivities" runat="server" CssClass="lblText">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div style="width:100%;">
                <div style="margin-top: 10px; text-align:right; text-align:100%;">
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btnstyle" />
                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btnstyle" />
                </div>
                <div style="margin-top: 5px;" id="panelwarningmessage" runat="server">
                    <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText" ForeColor="red"></asp:Label>
                </div>
            </div>
        </div>
    </form>--%>
</body>
</html>
