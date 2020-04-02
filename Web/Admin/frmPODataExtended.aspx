<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPODataExtended.aspx.vb"
    Inherits="Admin_frmPODataExtended" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage PO Date</title>
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
        .HeaderPanel
        {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top: -5px;
            margin-bottom: 10px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-left: 8px;
            border-style: solid;
            border-color: Gray;
            border-width: 2px;
            color: White;
        }
        .HeaderGrid
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
            height: 25px;
        }
        .oddGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .emptyrow
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Green;
            color: White;
            cursor: pointer;
            padding: 2px;
            vertical-align: middle;
            text-align: center;
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            /*width: 100%;*/
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            /*height: 100%;*/
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>
    
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../Scripts/SDMController.js"></script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )
         function invalidExportToExcel() {
            alert('Data is empty!');
            return false;
        }

         function adjustSize() {
             adjustBlurScreen();
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
    <form id="formid" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="col-xs-12">
            <asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>
                    <div class="row">
	                    <div class="box box-info">
		                    <div class="box-header with-border">
                                <h3 class="box-title">PO Date Management</h3>
		                    </div>
		                    <div class="box-body">
			                    <div class="table table-responsive">
                                    <div class="col-md-4">
                                        <form class="form-inline">
                                            <div class="form-group">
                                                <asp:TextBox ID="TxtPONOSearch" runat="server" CssClass="form-control" placeholder="PO Number"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary" Width="100px" />
                                            </div>
                                        </form>
                                    </div>
                                    <div class="col-md-4">
                                        <span style="display:block; width:50px;"></span>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <asp:Button ID="LbtExportToExcel" runat="server" Text="Null PODate Export to Excel" CssClass="btn btn-block btn-primary"></asp:Button>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="LbtMassUpdated" runat="server" Text="PO Date Mass Update" CssClass="btn btn-block btn-primary"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <span style="display:block; height:10px;"></span>
                                    </div>
                                    <asp:GridView ID="GvPOData" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                                        DataKeyNames="PO_ID" PageSize="10" AllowPaging="true" Width="99%" OnRowCancelingEdit="GvPOData_RowCancelingEdit"
                                        OnRowEditing="GvPOData_RowEditing" OnRowUpdating="GvPOData_RowUpdating" CssClass="table table-bordered table-condensed">
                                        <RowStyle CssClass="oddGrid" />
                                        <AlternatingRowStyle CssClass="evenGrid" />
                                        <HeaderStyle CssClass="HeaderGrid" />
                                        <EmptyDataRowStyle CssClass="emptyrow" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                        ToolTip="Update" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to update with this date?')" />
                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit-icon.png"
                                                        ToolTip="Edit" Height="16px" Width="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PONO" HeaderText="PONO" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="PO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPODate" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="LblPoNo" runat="server" Text='<%#Eval("PONO") %>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="TxtPODate" runat="server" CssClass="ltrLabel"></asp:TextBox>
                                                    <asp:ImageButton ID="ImgPODate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                        Width="18px" />
                                                    <cc1:CalendarExtender ID="cePODate" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="ImgPODate"
                                                        TargetControlID="TxtPODate">
                                                    </cc1:CalendarExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="CDB" HeaderText="Create User" ReadOnly="true" />
                                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="LMBY" HeaderText="Modified User" ReadOnly="true" />
                                        </Columns>
                                    </asp:GridView>
			                    </div>
                                <div id="gridviewNullPODate" style="display: none;">
                                    <asp:GridView ID="GvNullPODate" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PONO" HeaderText="PONO" />
                                            <asp:BoundField DataField="PODate" HeaderText="PODate" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
		                    </div>
		                    <div class="box-footer">
                                <asp:Label ID="LblGvWarningMessage" runat="server" Font-Name="Verdana" Font-Size="11px"
                                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
		                    </div>
	                    </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="LbtExportToExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        <span>PO Date Management</span>
    </div>
    <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="border-bottom-style: solid; border-bottom-color: Gray; border-bottom-width: 2px;
                padding-bottom: 10px; min-height: 300px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <span class="ltrLabel">PONO</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPONOSearch" runat="server" CssClass="ltrLabel"></asp:TextBox>
                                <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btnstyle" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px;">
                    <asp:GridView ID="GvPOData" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                        DataKeyNames="PO_ID" PageSize="20" AllowPaging="true" Width="99%" OnRowCancelingEdit="GvPOData_RowCancelingEdit"
                        OnRowEditing="GvPOData_RowEditing" OnRowUpdating="GvPOData_RowUpdating">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <HeaderStyle CssClass="HeaderGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                        ToolTip="Update" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to update with this date?')" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit-icon.png"
                                        ToolTip="Edit" Height="16px" Width="16px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PONO" HeaderText="PONO" ReadOnly="true" />
                            <asp:TemplateField HeaderText="PO Date">
                                <ItemTemplate>
                                    <asp:Label ID="LblPODate" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="LblPoNo" runat="server" Text='<%#Eval("PONO") %>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="TxtPODate" runat="server" CssClass="ltrLabel"></asp:TextBox>
                                    <asp:ImageButton ID="ImgPODate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                        Width="18px" />
                                    <cc1:CalendarExtender ID="cePODate" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="ImgPODate"
                                        TargetControlID="TxtPODate">
                                    </cc1:CalendarExtender>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                ReadOnly="true" />
                            <asp:BoundField DataField="CDB" HeaderText="Create User" ReadOnly="true" />
                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                ReadOnly="true" />
                            <asp:BoundField DataField="LMBY" HeaderText="Modified User" ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:Label ID="LblGvWarningMessage" runat="server" Font-Name="Verdana" Font-Size="11px"
                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
                <div style="display: none;">
                    <asp:GridView ID="GvNullPODate" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="PONO" HeaderText="PONO" />
                            <asp:BoundField DataField="PODate" HeaderText="PODate" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="margin-top: 5px; text-align: right;">
                    <asp:LinkButton ID="LbtExportToExcel" runat="server" Text="Null PODate Export to Excel"
                        CssClass="ltrLabel" ForeColor="Blue" Font-Size="11px"></asp:LinkButton>
                </div>
            </div>
            <div style="margin-top: 5px;">
                <asp:LinkButton ID="LbtMassUpdated" runat="server" Text="PO Date Mass Update" CssClass="ltrLabel"
                    ForeColor="Blue" Font-Size="12px"></asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LbtExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
    </form>--%>
</body>
</html>
