<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CodDocProgress.aspx.vb" Inherits="DashBoard_CodDocProgress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Progress</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>
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
    .header-center{
        text-align:center;
    }
</style>
</head>
    <script language="javascript" type="text/javascript">
    function showModal() {
          $('#modalHeader').text("Document Progress Details");
          $('#modalDocDetails').modal('show');
      }
  </script>
<body>
    <form id="form1" runat="server" >
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Document Progress</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            PO Number
                                        </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlPO" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged" />
                                        </div>
                                    </div>
						        </div>
                                <div class="col-md-3">
                                    <div class="form-inline">
                                        <div class="col-xs-8">
                                            <asp:Button runat="server" ID="btnDownload" Text="Download Progress" CssClass="btn btn-block btn-success" OnClick="btnDownload_Click" />
                                        </div>
                                    </div>
                                </div>
					        </div>
                            <h3></h3>
                            <div class="col-xs-8">
                                <asp:GridView ID="grdDocuments" runat="server" AllowPaging="False" EmptyDataText="Empty" AutoGenerateColumns="False" CssClass="table table-condensed table-condensed" OnRowDataBound="grdDocuments_RowDataBound">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Right" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                    <Columns>
                                    <asp:BoundField DataField="DOCName" HeaderText="Document Name" HeaderStyle-CssClass="header-center"/>
                                    <asp:TemplateField HeaderText="Approved Document" SortExpression="docApproved" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Approved_Document" runat="server" 
                                                Text='<%# Eval("docApproved") %>'
                                                CommandName="ApprovedCount"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:Label runat="server" ID="lblApprovedDoc" />
                                            <asp:HiddenField ID="hiddenId" runat="server" 
                                                Value='<%#Eval("DOC_ID")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document On Progress" SortExpression="onProgress" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="OnProgress_Document" runat="server" 
                                                Text='<%# Eval("onProgress") %>'
                                                CommandName="OnProgressCount"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:Label runat="server" ID="lblOnProgressDoc" />
                                            <asp:HiddenField ID="hiddenId1" runat="server" 
                                                Value='<%#Eval("DOC_ID")%>'/>
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

        <%--Modal--%>
        <div class="modal fade" id="modalDocDetails" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label id="modalHeader"></label>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div style="margin-top: 10px; min-height: 200px; max-height: 350px; overflow-y: scroll;">
                                <asp:GridView ID="grdDocDetail" runat="server" AllowPaging="false" PageSize="3" EmptyDataText="Empty" 
                                    AutoGenerateColumns="False" CssClass="table table-condensed table-condensed">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:BoundField DataField="DOCName" HeaderText="Document Name" />
                                        <asp:BoundField DataField="WorkPkgId" HeaderText="Workpackage ID" />
                                        <asp:BoundField DataField="Site_No" HeaderText="Site No" />
                                        <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                        <asp:BoundField DataField="RGNName" HeaderText="Region" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
