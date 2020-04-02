<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardMaster.aspx.vb"
    Inherits="COD_frmDashboardMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Master Dashboard</title>
    <style type="text/css">
        .gridHeader_2
        {
	        font-family:Verdana;
	        font-size:11px;
	        background-color:#ffc727;
	        font-weight:bolder;
	        text-align:center;
	        padding:5px;
	        color:white;
	        border-style:solid;
	        border-width:2px;
	        border-color:gray;
        }
        .gridOdd
        {
            font-family:Verdana;
	        font-size:11px;
	        padding:5px;
        }
        .gridEven
        {
            font-family:Verdana;
	        font-size:11px;
	        background-color:#cfcfcf;
	        padding:5px;
        }
        .textLabel
        {
            font-family:verdana;
            font-size:11px;
            
        }
        .fieldLabel
        {
            font-family:verdana;
            font-size:11px;
            font-weight:Bolder;
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Dashboard Master</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <asp:MultiView ID="MvDashboardConfiguration" runat="server">
                                <asp:View ID="VwDashboardMaster" runat="server">
                                    <asp:GridView ID="GvMasterDashboard" runat="server" AutoGenerateColumns="false" AllowPaging="true" EmptyDataText="Data is not exist"
                                                PageSize="10" AllowSorting="True" CssClass="table table-bordered table-condensed">
                                        <PagerSettings Position="Bottom" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DashboardName" HeaderText="Dashboard Name" HeaderStyle-CssClass="gridHeader_2"
                                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                            <asp:BoundField DataField="DashboardDesc" HeaderText="Description" HeaderStyle-CssClass="gridHeader_2"
                                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                            <asp:BoundField DataField="FormName" HeaderText="URL Form" HeaderStyle-CssClass="gridHeader_2"
                                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                            <asp:BoundField DataField="LMBY" HeaderText="Last Modified by" HeaderStyle-CssClass="gridHeader_2"
                                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HeaderStyle-CssClass="gridHeader_2"
                                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                                            <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgEdit" runat="server" CommandName="EditDashboard" ImageUrl="~/images/gridview/edit.jpg" CommandArgument='<%#Eval("MDashboardId") %>'
                                                        Width="18px" Height="18px" />
                                                    <asp:ImageButton ID="ImgDelete" runat="server" CommandName="DeleteDashboard" ImageUrl="~/images/gridview/delete.jpg" CommandArgument='<%#Eval("MDashboardId") %>'
                                                        Width="18px" Height="18px" OnClientClick="return confirm('Are you sure you want to delete this Master Dashboard ?')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="VwEditDashboard" runat="server">
                                    <div>
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            <div class="col-md-3">
                                <span style="display:block; width:10px;"></span>
                            </div>
					        <div class="col-md-6">
                                <div style="background-color:aliceblue">
						            <div class="form-horizontal">
                                        <div class="box-body">
							                <div class="form-group">
                                                <label class="col-sm-2 control-label">Dashboard Name</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="TxtDashboardName" runat="server" CssClass="form-control" ValidationGroup="dashboardform"></asp:TextBox>
                                                </div>
							                </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Description</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="TxtDashboardDesc" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">URL Form</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="TxtURLForm" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Set as Default</label>
                                                <div class="col-sm-10">
                                                    <asp:CheckBox ID="ChkSetDefaultDashboard" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <div class="col-xs-4">
                                                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btn btn-block btn-primary" ValidationGroup="dashboardform" />
                                            </div>
                                            <div class="col-xs-4">
                                                <span style="display:block; width:10px;"></span>
                                            </div>
                                            <div class="col-xs-4">
                                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-danger" CausesValidation="false" />
                                            </div>
                                        </div>
						            </div>
                                </div>
					        </div>
				        </div>
			        </div>
                    <div class="box-footer">
                        <asp:RequiredFieldValidator ID="rfvDashboardName" runat="server" ControlToValidate="TxtDashboardName"
                            ValidationGroup="dashboardform" SetFocusOnError="true" ErrorMessage="Dashboard Name is Required"
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="VsDashboardMaster" runat="server" DisplayMode="BulletList"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="dashboardform" />
                    </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MvDashboardConfiguration" runat="server">
                <asp:View ID="VwDashboardMaster" runat="server">
                    <asp:GridView ID="GvMasterDashboard" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DashboardName" HeaderText="Dashboard Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="DashboardDesc" HeaderText="Description" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="FormName" HeaderText="URL Form" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="LMBY" HeaderText="Last Modified by" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEdit" runat="server" CommandName="EditDashboard" ImageUrl="~/images/gridview/edit.jpg" CommandArgument='<%#Eval("MDashboardId") %>'
                                        Width="18px" Height="18px" />
                                    <asp:ImageButton ID="ImgDelete" runat="server" CommandName="DeleteDashboard" ImageUrl="~/images/gridview/delete.jpg" CommandArgument='<%#Eval("MDashboardId") %>'
                                        Width="18px" Height="18px" OnClientClick="return confirm('Are you sure you want to delete this Master Dashboard ?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="VwEditDashboard" runat="server">
                    <div>
                        <table>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Lbldashboardname" runat="server" CssClass="fieldLabel" Text="Dashboard Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDashboardName" runat="server" CssClass="textLabel" ValidationGroup="dashboardform" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblDashboardDescription" runat="server" CssClass="fieldLabel" Text="Description"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDashboardDesc" runat="server" CssClass="textLabel" TextMode="MultiLine" Width="350px"
                                Height="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblURLForm" runat="server" CssClass="fieldLabel" Text="URL Form"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtURLForm" runat="server" CssClass="textLabel" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblSetDefaultDashboard" runat="server" CssClass="fieldLabel" Text="Set is default dashboard"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChkSetDefaultDashboard" runat="server" CssClass="textLabel" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:RequiredFieldValidator ID="rfvDashboardName" runat="server" ControlToValidate="TxtDashboardName"
                    ValidationGroup="dashboardform" SetFocusOnError="true" ErrorMessage="Dashboard Name is Required"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="VsDashboardMaster" runat="server" DisplayMode="BulletList"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="dashboardform" />
                <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="textLabel" ValidationGroup="dashboardform" />
                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="textLabel" CausesValidation="false" />
            </div>
        </div>
    </form>--%>
</body>
</html>
