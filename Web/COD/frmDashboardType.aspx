<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardType.aspx.vb"
    Inherits="COD_frmDashboardType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <title>Dashboard Configuration</title>
    <style type="text/css">
    .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:11px;
            font-weight:bold;
            margin-bottom:10px;
            padding:3px;
        }
     .HeaderGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        font-weight: bold;
        color: White;
        background-color: #ffc90E;
        border-color:white;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color: White;
    }
    .evenGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color:#cfcfcf;
    }
        .HeaderPanel
        {
           width:99%;
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
        .lblDdlText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
        }
        .evengrid2
        {
            background-color: #cfcfcf;
        }
     
        
    </style>

    <script type="text/javascript">
        function UpdateConfig() {
            alert('Data Update Successfully!');
            return true;
        }
        function DeleteConfig() {
            alert('Configuration Delete Successfully!');
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
	        <div class="col-md-6">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">dashboard type</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <asp:GridView ID="GvDashboardType" runat="server" AutoGenerateColumns="false" CellPadding="3" AllowPaging="true" AllowSorting="true"
                                CellSpacing="2" EmptyDataText="No Record Data Found" CssClass="table table-bordered table-condensed">
                                <AlternatingRowStyle CssClass="evenGrid2" />
                                <EmptyDataRowStyle CssClass="lblSubHeader" ForeColor="red" />
                                <PagerSettings Position="Bottom" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Font-Size="8pt" HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" HeaderStyle-Width="30px"
                                        HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:Label ID="LblDashboardID" runat="server" Text='<%#Eval("Dashboard_Id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RoleDesc" HeaderText="Role" ItemStyle-Font-Size="10pt"
                                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" ItemStyle-Width="200px" />
                                    <asp:TemplateField HeaderText="Dashboard Type" ItemStyle-Font-Size="8pt" HeaderStyle-CssClass="HeaderGrid"
                                        HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="LblMasterDashboardId" runat="server" Text='<%#Eval("MDashboard_Id") %>'
                                                Visible="false"></asp:Label>
                                            <asp:DropDownList ID="DdlSaveDashboardType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Width="30px"
                                        HeaderStyle-Height="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%#Eval("Dashboard_Id") %>'
                                                CommandName="deletedashboard" OnClientClick="return confirm('Are you sure you want to delete this configuration ?')"
                                                ImageUrl="~/images/action_delete.gif" ToolTip="Delete this configuration" />
                                            <asp:ImageButton ID="ImgSave" runat="server" CommandArgument='<%#Eval("Dashboard_Id") %>'
                                                CommandName="savedashboard" OnClientClick="return confirm('Are you sure you want to Save Change the configuration ?')"
                                                ImageUrl="~/images/save-icon.jpg" ToolTip="Save Change" Height="16" Width="16" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
		        </div>
	        </div>
            <div class="col-md-6">
		        <div class="box box-success">
			        <div class="box-header with-border">
				        <h3 class="box-title">panel config</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <%--<div class="col-md-1">
                                <span style="display:block; width:10px;"></span>
                            </div>--%>
					        <div class="">
						        <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">User Type</label>
                                        <div class="col-sm-7">
                                            <asp:dropdownlist id="ddlusertype" runat="server" cssclass="form-control" validationgroup="addconfig" autopostback="true"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Role</label>
                                        <div class="col-sm-7">
                                            <asp:dropdownlist id="ddlrole" runat="server" cssclass="form-control" validationgroup="addconfig"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Dashboard type</label>
                                        <div class="col-sm-7">
                                            <asp:dropdownlist id="ddldashboardtype" runat="server" cssclass="form-control" validationgroup="addconfig"/>
                                        </div>
                                    </div>
						        </div>
					        </div>
				        </div>
			        </div>
			        <div class="box-footer">
                        <div class="col-md-3">
                            <span style="display:block; width:10px;"></span>
                        </div>
                        <div class="col-sm-6">
                            <asp:button id="btnaddrole" cssclass="btn btn-block btn-primary" runat="server" text="add configuration" validationgroup="addconfig" />
                        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <asp:comparevalidator id="comparevalidator1" runat="server" controltovalidate="ddlusertype"
            display="none" valuetocompare="0" operator="greaterthan" setfocusonerror="true"
            validationgroup="addconfig" errormessage="please select usertype!"></asp:comparevalidator>
        <asp:comparevalidator id="cvrole" runat="server" controltovalidate="ddlrole" display="none"
            valuetocompare="0" operator="greaterthan" setfocusonerror="true" validationgroup="addconfig"
            errormessage="please select role!"></asp:comparevalidator>
        <asp:comparevalidator id="cvdashboardtype" runat="server" controltovalidate="ddldashboardtype"
            display="none" valuetocompare="0" operator="greaterthan" setfocusonerror="true"
            validationgroup="addconfig" errormessage="please select dashboard type!"></asp:comparevalidator>
        <asp:validationsummary id="vsconfiguration" runat="server" validationgroup="addconfig"
            displaymode="list" showmessagebox="true" showsummary="false" />
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div style="width: 100%">
            <asp:GridView ID="GvDashboardType" runat="server" AutoGenerateColumns="false" CellPadding="3"
                CellSpacing="2" EmptyDataText="No Record Data Found">
                <AlternatingRowStyle CssClass="evenGrid2" />
                <EmptyDataRowStyle CssClass="lblSubHeader" ForeColor="red" />
                <Columns>
                    <asp:TemplateField ItemStyle-Font-Size="8pt" HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px"
                        HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            <asp:Label ID="LblDashboardID" runat="server" Text='<%#Eval("Dashboard_Id") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RoleDesc" HeaderText="Role" ItemStyle-Font-Size="10pt"
                        HeaderStyle-CssClass="HeaderGrid" HeaderStyle-Height="25px" ItemStyle-Width="200px" />
                    <asp:TemplateField HeaderText="Dashboard Type" ItemStyle-Font-Size="8pt" HeaderStyle-CssClass="HeaderGrid"
                        HeaderStyle-Height="25px" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                        <ItemTemplate>
                            <asp:Label ID="LblMasterDashboardId" runat="server" Text='<%#Eval("MDashboard_Id") %>'
                                Visible="false"></asp:Label>
                            <asp:DropDownList ID="DdlSaveDashboardType" runat="server" CssClass="lblDdlText">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Font-Size="7.5pt" HeaderStyle-CssClass="HeaderGrid"
                        HeaderStyle-Height="25px">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%#Eval("Dashboard_Id") %>'
                                CommandName="deletedashboard" OnClientClick="return confirm('Are you sure you want to delete this configuration ?')"
                                ImageUrl="~/images/action_delete.gif" ToolTip="Delete this configuration" />
                            <asp:ImageButton ID="ImgSave" runat="server" CommandArgument='<%#Eval("Dashboard_Id") %>'
                                CommandName="savedashboard" OnClientClick="return confirm('Are you sure you want to Save Change the configuration ?')"
                                ImageUrl="~/images/save-icon.jpg" ToolTip="Save Change" Height="16" Width="16" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="margin-top: 20px; border-color: #cfcfcf; border-width: 1px; border-style: solid;
            width: 35%;">
            <div class="HeaderReport">
                Panel Config
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="User Type" CssClass="lblDdlText"></asp:Label>
                        </td>
                        <td class="lblText">
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlUserType" runat="server" CssClass="lblDdlText" ValidationGroup="addconfig" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblText" runat="server" Text="Role" CssClass="lblDdlText"></asp:Label>
                        </td>
                        <td class="lblText">
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlRole" runat="server" CssClass="lblDdlText" ValidationGroup="addconfig">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="DashboardType" CssClass="lblDdlText"></asp:Label>
                        </td>
                        <td class="lblText">
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="DDlDashboardType" runat="server" CssClass="lblDdlText" ValidationGroup="addconfig">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; text-align: right;">
                <asp:Button ID="BtnAddRole" runat="server" Text="Add Configuration" ValidationGroup="addconfig" />
            </div>
            <div>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DdlUserType"
                    Display="None" ValueToCompare="0" Operator="GreaterThan" SetFocusOnError="true"
                    ValidationGroup="addconfig" ErrorMessage="Please select UserType!"></asp:CompareValidator>
                <asp:CompareValidator ID="CvRole" runat="server" ControlToValidate="DdlRole" Display="None"
                    ValueToCompare="0" Operator="GreaterThan" SetFocusOnError="true" ValidationGroup="addconfig"
                    ErrorMessage="Please select Role!"></asp:CompareValidator>
                <asp:CompareValidator ID="CvDashboardType" runat="server" ControlToValidate="DDlDashboardType"
                    Display="None" ValueToCompare="0" Operator="GreaterThan" SetFocusOnError="true"
                    ValidationGroup="addconfig" ErrorMessage="Please select Dashboard Type!"></asp:CompareValidator>
                <asp:ValidationSummary ID="VsConfiguration" runat="server" ValidationGroup="addconfig"
                    DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
            </div>
        </div>
    </form>--%>
</body>
</html>
