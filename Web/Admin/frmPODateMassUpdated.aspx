<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPODateMassUpdated.aspx.vb"
    Inherits="Admin_frmPODateMassUpdated" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Date Mass Update</title>
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
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="col-xs-12">
	            <div class="box box-info">
		            <div class="box-header with-border">
                        <h3 class="box-title">PO Date Mass Update</h3>
		            </div>
		            <div class="box-body">
			            <div class="table table-responsive">
                            <div class="col-md-10">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <label>PO Upload</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:FileUpload ID="POUpload" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-inline">
                                    <div class="form-group">
                                        <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-block btn-info" Text="Upload" Width="71px" />
                                    </div>
                                </div>
                            </div>
			            </div>
		            </div>
		            <div class="box-footer">
                        <asp:Label ID="LblWarningMessage" runat="server" CssClass="ltrLabelWarning"></asp:Label>
		            </div>
	            </div>
            </div>
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
                <div class="row">    
                    <div class="col-xs-12">
	                    <div class="box box-info">
		                    <div class="box-header with-border">
                                <h3 class="box-title">Different PO Date Update</h3>
		                    </div>
		                    <div class="box-body">
                                <div class="col-md-4">
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <asp:Button ID="LbtRefresh" runat="server" Text="Refresh" CssClass="btn btn-block btn-success"></asp:Button>
                                        </div>
                                    </div>
                                </div>
			                    <div class="table table-responsive">
                                    <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                                        <asp:GridView ID="GvPOData" runat="server" AllowPaging="false" AutoGenerateColumns="false" EmptyDataText="No record found" 
                                            CssClass="table table-bordered table-condensed" AllowSorting="true">
                                            <RowStyle CssClass="oddGrid" />
                                            <AlternatingRowStyle CssClass="evenGrid" />
                                            <HeaderStyle CssClass="HeaderGrid" />
                                            <EmptyDataRowStyle CssClass="emptyrow" />
                                            <Columns>
                                                <asp:BoundField DataField="PONO" HeaderText="PONO" ReadOnly="true" />
                                                <asp:BoundField DataField="PODate" HeaderText="Existing PODate" ReadOnly="true" HtmlEncode="false"
                                                    DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField DataField="NewPODate" HeaderText="New PO Date" HtmlEncode="false"
                                                    DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                                    ReadOnly="true" />
                                                <asp:BoundField DataField="ModifiedUser" HeaderText="Create User" ReadOnly="true" />
                                                <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPoNoRaw" runat="server" Text='<%#Eval("PONO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LblPOIDRaw" runat="server" Text='<%#Eval("POIDRaw") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LblNewPODate" runat="server" Text='<%#Eval("NewPODateString") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="updatepodate" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                            CommandArgument='<%#Eval("POID") %>' ToolTip="Update" Height="16px" Width="16px"
                                                            OnClientClick="return confirm('Are you sure you want to update with this date?')" />
                                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="deletepodate" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                            CommandArgument='<%#Eval("POIDRaw") %>' ToolTip="Cancel" Height="16px" Width="16px"
                                                            OnClientClick="return confirm('Are you sure you want to cancel with this date?')" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
			                    </div>
		                    </div>
		                    <div class="box-footer">
                                <asp:Label ID="LblGvWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
		                    </div>
	                    </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server" enctype="multipart/form-data" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        <span>PO Date Mass Update</span>
    </div>
    <table width="100%">
        <tr class="ltrLabel">
            <td colspan="2">
                Po Upload
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
    <div style="margin-top: 10px;">
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
                    <div class="HeaderPanel2">
                        <span>Different PO Date Update</span>
                    </div>
                    <div style="margin-top: 5px;">
                        <asp:GridView ID="GvPOData" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                            PageSize="20" AllowPaging="true" Width="100%">
                            <RowStyle CssClass="oddGrid" />
                            <AlternatingRowStyle CssClass="evenGrid" />
                            <HeaderStyle CssClass="HeaderGrid" />
                            <EmptyDataRowStyle CssClass="emptyrow" />
                            <Columns>
                                <asp:BoundField DataField="PONO" HeaderText="PONO" ReadOnly="true" />
                                <asp:BoundField DataField="PODate" HeaderText="Existing PODate" ReadOnly="true" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="NewPODate" HeaderText="New PO Date" HtmlEncode="false"
                                    DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="CDT" HeaderText="Created Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true"
                                    ReadOnly="true" />
                                <asp:BoundField DataField="ModifiedUser" HeaderText="Create User" ReadOnly="true" />
                                <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-VerticalAlign="Top"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPoNoRaw" runat="server" Text='<%#Eval("PONO") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblPOIDRaw" runat="server" Text='<%#Eval("POIDRaw") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblNewPODate" runat="server" Text='<%#Eval("NewPODateString") %>'
                                            Visible="false"></asp:Label>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="updatepodate" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                            CommandArgument='<%#Eval("POID") %>' ToolTip="Update" Height="16px" Width="16px"
                                            OnClientClick="return confirm('Are you sure you want to update with this date?')" />
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="deletepodate" ImageUrl="~/Images/gridview/Cancel.jpg"
                                            CommandArgument='<%#Eval("POIDRaw") %>' ToolTip="Cancel" Height="16px" Width="16px"
                                            OnClientClick="return confirm('Are you sure you want to cancel with this date?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="margin-top: 5px; text-align: right; width: 100%;">
                        <asp:LinkButton ID="LbtRefresh" runat="server" Text="Refresh" ForeColor="Blue" CssClass="ltrLabel"
                            Font-Size="12px"></asp:LinkButton>
                    </div>
                    <br />
                    <asp:Label ID="LblGvWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                        ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>--%>
</body>
</html>
