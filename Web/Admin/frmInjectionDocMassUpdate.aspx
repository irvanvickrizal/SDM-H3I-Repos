<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInjectionDocMassUpdate.aspx.vb"
    Inherits="Admin_frmInjectionDocMassUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Injection Mass Update</title>
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
        .btnstyle
        {
            font-family: Verdana;
            font-size: 11px;
            border-style: solid;
            border-color: Black;
            border-width: 1px;
            background-color: Gray;
            padding: 3px;
            color: White;
        }
        .ltrLabelWarning
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
        }
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
        .HeaderPanel2
        {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            margin-top: -5px;
            margin-bottom: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 8px;
            border-style: solid;
            border-color: Gray;
            border-width: 0px;
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
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }
    </style>

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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
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
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Injection Doc Mass Update</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3">DOC ID List</label>
							        </div>
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <table class="table table-bordered table-condensed">
                                                <tr>
                                                    <th style="background-color: Gray;">Doc Name</th>
                                                    <th style="background-color: Gray;">Doc ID</th>
                                                </tr>
                                                <tr>
                                                    <td>ATP Online</td>
                                                    <td>2001</td>
                                                </tr>
                                                <tr>
                                                    <td>QC Online</td>
                                                    <td>2025</td>
                                                </tr>
                                                <tr>
                                                    <td>CR Online</td>
                                                    <td>2024</td>
                                                </tr>
                                                <tr>
                                                    <td>CO Online</td>
                                                    <td>2023</td>
                                                </tr>
                                            </table>
								        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <a href="Example_InjectonMassUpdate.xls" class="ltrLabel" style="color: Blue;">Download Template Example</a>
								        </div>
                                    </div>
                                    <div class="form-group">
								        <label class="col-sm-4">Injection Doc Upload</label>
                                    </div>
                                    <div>
								        <div class="col-sm-8">
                                            <asp:FileUpload ID="POUpload" runat="server" Width="518px" />
								        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-block btn-primary" Text="Upload" />
								        </div>
                                    </div>
						        </div>
					        </div>
                            <div class="col-md-6">
                                <div class="pull-right">
                                    <asp:Button runat="server" ID="btnBack" Text="Back to Injection Document" CssClass="btn btn-block btn-warning" />
                                </div>
                            </div>
				        </div>
			        </div>
			        <div class="box-footer">
                        <asp:Label ID="LblWarningMessage" runat="server" CssClass="ltrLabelWarning" />
			        </div>
		        </div>
	        </div>
        </div>

        
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Error Transaction List</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
                                        <div style="margin-top: 10px; min-height: 25px; max-height: 300px; overflow-y: scroll;">
                                            <asp:GridView ID="GvInjectionDocExist" runat="server" AutoGenerateColumns="false"
                                                EmptyDataText="No record found" CssClass="table table-bordered table-condensed">
                                                <RowStyle CssClass="oddGrid" />
                                                <AlternatingRowStyle CssClass="evenGrid" />
                                                <HeaderStyle CssClass="HeaderGrid" />
                                                <EmptyDataRowStyle CssClass="emptyrow" />
                                                <PagerSettings Position="TopAndBottom" />
                                                <PagerStyle CssClass="customPagination" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="docname" HeaderText="Document" />
                                                    <asp:BoundField DataField="PackageId" HeaderText="Package id" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
					                </div>
				                </div>
			                </div>
		                </div>
	                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        <span>Injection Doc Mass Update</span>
    </div>
    <table width="100%">
        <tr class="ltrLabel">
            <td colspan="2">
                <span class="lblField">DOC ID List :</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="1" width="30%">
                    <tr class="lblField">
                        <th style="background-color: Gray;">
                            Doc Name
                        </th>
                        <th style="background-color: Gray;">
                            Doc ID
                        </th>
                    </tr>
                    <tr class="ltrLabel">
                        <td>
                            ATP Online
                        </td>
                        <td>
                            2001
                        </td>
                    </tr>
                    <tr class="ltrLabel">
                        <td>
                            QC Online
                        </td>
                        <td>
                            2025
                        </td>
                    </tr>
                    <tr class="ltrLabel">
                        <td>
                            CR Online
                        </td>
                        <td>
                            2024
                        </td>
                    </tr>
                    <tr class="ltrLabel">
                        <td>
                            CO Online
                        </td>
                        <td>
                            2023
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 5px;">
                    <a href="Example_InjectonMassUpdate.xls" class="ltrLabel" style="color: Blue;">Download
                        Template Example</a>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr class="lblField">
            <td colspan="2">
                Injection Docs Upload
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:FileUpload ID="POUpload" runat="server" Width="518px" />&nbsp;
                <asp:Button ID="btnUpload" runat="server" CssClass="btnstyle" Text="Upload" Width="71px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="LblWarningMessage" runat="server" CssClass="ltrLabelWarning"></asp:Label>
            </td>
        </tr>
    </table>
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
                padding-bottom: 10px; min-height: 300px; margin-top: 10px;">
                <div class="HeaderPanel2">
                    <span>Error Transaction List</span>
                </div>
                <div style="margin-top: 5px;">
                    <asp:GridView ID="GvInjectionDocExist" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No record found" PageSize="20" AllowPaging="true" Width="100%">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <HeaderStyle CssClass="HeaderGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="docname" HeaderText="Document" />
                            <asp:BoundField DataField="PackageId" HeaderText="Package id" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="Status" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>--%>
</body>
</html>
