<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEStorageProgressSummary.aspx.vb" Inherits="RPT_frmEStorageProgressSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Storage Progress Summary</title>
    <script type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
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

    <script type="text/javascript" src="../fusioncharts/fusioncharts.js"></script>
    <script type="text/javascript" src="../fusioncharts/jquery.min.js"></script>
    <style type="text/css">
        #PleaseWait {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 50px;
            width: 50px;
            background-image: url(../images/loading.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }

        #blur {
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/updatepanelhook.fusioncharts.js" />
            </Scripts>
        </asp:ScriptManager>
        <span class="header h3">E-Storage Progress Report</span>
        <div style="height: 2px; background-color: gray; width: 99%; margin-top: 5px; margin-bottom: 10px;"></div>
        <div style="margin-top: 10px;">
            <asp:UpdateProgress ID="upgViewPrgoress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="up1">
                <ProgressTemplate>
                    <div id="blur">
                        <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                            <img src="../images/loading.gif" style="vertical-align: middle" alt="Processing" height="180" width="180" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                    <div class="col-lg-12">
                        <div style="height:10px; padding-top:10px;"></div>
                    </div>
                    <div class="col-lg-12">
                        <div class="col-lg-8">
                            <asp:Literal ID="ltrGraphProgress" runat="server"></asp:Literal>
                        </div>
                        <div class="col-lg-4">
                            <div class="panel panel-default">
                                <div class="panel-heading">Filtering Report</div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label for="Region">Filtered by Region</label>
                                            <asp:DropDownList ID="DdlRegions" runat="server" CssClass="list-group-item" AutoPostBack="true" Font-Size="12px"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="CPONO">Filtered by Cust.PONO</label>
                                            <asp:DropDownList ID="DdlPOList" runat="server" CssClass="list-group-item" AutoPostBack="true" Font-Size="12px"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                         <%--  <div style="height: 1px; width: 50%; background-color: gray; margin-top: 5px; margin-bottom: 5px;"></div>
                 <div style="margin-top: 5px;">
                       <asp:GridView ID="GvReport" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" color="" Width="100%" Style="font-family: Verdana;">
                           <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" Height="20px" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="4pt" Height="2px"
                             HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="CPO No" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPoNo" runat="server" Text='<%# Eval("hotasperpo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Total Sites" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("TotalSites") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Completed" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("dochasdone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In-Progress" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("docinprogress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending" HeaderStyle-ForeColor="black" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("notstartedyet") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
            </div>
        </div>--%>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="col-lg-3">
                            <div class="panel-body">
                                <div class="small-box bg-blue">
                                    <div class="inner">
                                        <h3>
                                            <asp:Label ID="lblTotalSites" runat="server" Text="0"></asp:Label>
                                            <sup style="font-size: 20px">Total Sites</sup></h3>
                                    </div>                                    
                                    <asp:LinkButton ID="lbtDownloadTotalSites" runat="server" CssClass="small-box-footer">Download List<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>                                
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="panel-body">
                                <div class="small-box bg-aqua">
                                    <div class="inner">
                                        <h3>
                                            <asp:Label ID="lblCompleted" runat="server" Text="58"></asp:Label>
                                            <sup style="font-size: 20px">Completed</sup></h3>
                                    </div>
                                    <asp:LinkButton ID="lbtDownloadCompleted" runat="server" CssClass="small-box-footer">Download List<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>                                
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="panel-body">
                                <div class="small-box bg-green">
                                    <div class="inner">
                                        <h3>
                                            <asp:Label ID="lblInProgress" runat="server" Text="58"></asp:Label>
                                            <sup style="font-size: 20px">In-Progress</sup></h3>
                                    </div>
                                    <asp:LinkButton ID="lbtDownloadInProgress" runat="server" CssClass="small-box-footer">Download List<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>                                
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="panel-body">
                                <div class="small-box bg-yellow">
                                    <div class="inner">
                                        <h3>
                                            <asp:Label ID="lblPending" runat="server" Text="58"></asp:Label>
                                            <sup style="font-size: 20px">Pending</sup></h3>
                                    </div>
                                    <asp:LinkButton ID="lbtDownloadPending" runat="server" CssClass="small-box-footer">Download List<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>                                
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DdlPOList" />
                    <asp:AsyncPostBackTrigger ControlID="DdlRegions" />
                    <asp:PostBackTrigger ControlID="lbtDownloadTotalSites" />
                    <asp:PostBackTrigger ControlID="lbtDownloadCompleted" />
                    <asp:PostBackTrigger ControlID="lbtDownloadInProgress" />
                    <asp:PostBackTrigger ControlID="lbtDownloadPending" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
