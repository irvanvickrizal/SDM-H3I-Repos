<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocTimestampApproval.aspx.vb"
    Inherits="Admin_frmDocTimestampApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Timestamp Document</title>
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
        #HeaderPanel
        {
            width: 99%;
            background-repeat: repeat-x;
            background-color: #c5c3c3;
            font-family: verdana;
            font-weight: bolder;
            font-size: 10pt;
            color: white;
            padding-top: 5px;
            padding-bottom: 5px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            text-shadow: 0px 1px 1px #000;
        }
        .gridHeader_2
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 1px;
            border-color: gray;
        }
        .gridOdd
        {
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
            border-style: solid;
            border-width: 1px;
            border-color: gray;
        }
        .gridEven
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            padding: 5px;
             border-style: solid;
            border-width: 1px;
            border-color: gray;
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
                    progress.style.top = document.documentElement.clientHeight / 3 - progress.style.height.replace('px', '') / 2 + 'px';
                    progress.style.left = document.body.offsetWidth / 2 - progress.style.width.replace('px', '') / 2 + 'px';
                }
            }
        )
        function errorDelete() {
            alert('Failed while deleting process!');
            return false;
        }
        function errorInsert() {
            alert('Failed while inserting process!');
            return false;
        }
        function successInsert() {
            alert('Insert Successfully!');
            return true;
        }
        function successDelete() {
            alert('Delete Successfully!');
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Timestamp Approval Document</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
                                    <div class="col-md-3"><span style="display:none;height:10px"></span></div>
					                <div class="col-md-6">
                                        <asp:GridView ID="GvTimestampDocument" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="3" CssClass="table table-bordered table-condensed">
                                            <HeaderStyle CssClass="gridHeader_2" />
                                            <RowStyle CssClass="gridOdd" />
                                            <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:BoundField DataField="docname" HeaderText="Document Name" />
                                                <asp:BoundField DataField="LMDT" HeaderText="Last modified date" HtmlEncode="false"
                                                    DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                                <asp:BoundField DataField="ModifiedUser" HeaderText="Last Modified User" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/Cancel.jpg"
                                                            Width="20px" Height="20px" CommandArgument='<%#Eval("TimestampDocID") %>' CommandName="deletewf"
                                                            OnClientClick="return confirm('Are you sure you want to delete this document');" />
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
                    <div class="col-sm-4"><span style="display:none;height:10px"></span></div>
	                <div class="col-sm-3">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Timestamp Document</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
                                    <div class="col-md-1"><span style="display:none;height:10px"></span></div>
					                <div class="col-md-8">
						                <asp:GridView ID="GvTimestamp" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                            AllowPaging="true" PageSize="3" CssClass="table table-bordered table-condensed">
                                            <HeaderStyle CssClass="gridHeader_2" />
                                            <RowStyle CssClass="gridOdd" />
                                            <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:BoundField DataField="TDocName" HeaderText="Timestamp Document Name" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/images/gridview/AddNewItem.jpg"
                                                            Width="20px" Height="20px" CommandArgument='<%#Eval("Docid") %>' CommandName="addwf"
                                                            OnClientClick="return confirm('Are you sure you want to add this document');" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div id="HeaderPanel">
       Timestamp Approval Document
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
            <div style="margin-top: 10px;">
                <asp:GridView ID="GvTimestampDocument" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" Width="99%">
                    <HeaderStyle CssClass="gridHeader_2" />
                    <RowStyle CssClass="gridOdd" />
                    <AlternatingRowStyle CssClass="gridEven" />
                    <Columns>
                        
                        <asp:BoundField DataField="docname" HeaderText="Document Name" />
                        <asp:BoundField DataField="LMDT" HeaderText="Last modified date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                        <asp:BoundField DataField="ModifiedUser" HeaderText="Last Modified User" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/Cancel.jpg"
                                    Width="20px" Height="20px" CommandArgument='<%#Eval("TimestampDocID") %>' CommandName="deletewf"
                                    OnClientClick="return confirm('Are you sure you want to delete this document');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="margin-top: 10px; width: 99%;">
                <hr />
                <asp:GridView ID="GvTimestamp" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                    AllowPaging="true" PageSize="10">
                    <HeaderStyle CssClass="gridHeader_2" />
                    <RowStyle CssClass="gridOdd" />
                    <AlternatingRowStyle CssClass="gridEven" />
                    <Columns>
                        <asp:BoundField DataField="TDocName" HeaderText="Timestamp Document" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/images/gridview/AddNewItem.jpg"
                                    Width="20px" Height="20px" CommandArgument='<%#Eval("Docid") %>' CommandName="addwf"
                                    OnClientClick="return confirm('Are you sure you want to add this document');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>--%>
</body>
</html>
