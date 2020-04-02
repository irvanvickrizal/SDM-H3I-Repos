<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardNPO_Subcon.aspx.vb" Inherits="DashBoard_frmDashboardNPO_Subcon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Dashboard Agenda NPO</title>
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

    <script type="text/javascript" src="../../fusioncharts/fusioncharts.js"></script>
    <script type="text/javascript" src="../../fusioncharts/fusioncharts.maps.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('a.popup').live('click', function (e) {

                var page = $(this).attr("href")
                var titlePanel = $(this).attr("title")
                var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" scrolling="AUTO" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 450,
                    width: 800,
                    title: "Report Panel",
                    buttons: {
                        "Close": function () { $dialog.dialog('close'); }
                    },
                    close: function (event, ui) {

                        //__doPostBack('<%= btnRefresh.ClientID %>', '');
                    }
                });
                $dialog.dialog('open');
                e.preventDefault();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnRefresh" runat="server" Visible="false" />
        <div class="col-md-12">
            <div class="col-md-8">
                <div class="box box-default">
                    <div class="box-header with-border">
                        <h3 class="box-title">Your Agenda</h3>
                        <div class="col-md-12" style="height: 20px;"></div>
                        <div class="col-md-4">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSSVL0NUCount" runat="server" Text="0"></asp:Label>
                                        <sup style="font-size: 20px">Document(s)</sup></h3>
                                    <p>SSV L0 Report Need Upload</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-document-text"></i>
                                </div>
                                <asp:LinkButton ID="lbtViewDetailSSVRC" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblKPIL0RCCount" runat="server" Text="0"></asp:Label>
                                        <sup style="font-size: 20px">Document(s)</sup></h3>
                                    <p>KPI L0 Ready Creation</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-document"></i>
                                </div>
                                <asp:LinkButton ID="lbtViewDetailKPIL0RC" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                        <%--Added by Fauzan 30 Nov 2018. SSV L2--%>
                        <div class="col-md-4">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblKPIL2RCCount" runat="server" Text="0"></asp:Label>
                                        <sup style="font-size: 20px">Document(s)</sup></h3>
                                    <p>KPI L2 Ready Creation</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-document"></i>
                                </div>
                                <asp:LinkButton ID="lbtViewDetailKPIL2RC" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="small-box bg-primary">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblSSVL2NUCount" runat="server" Text="0"></asp:Label>
                                        <sup style="font-size: 20px">Document(s)</sup></h3>
                                    <p>SSV L2 Report Need Upload</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-document-text"></i>
                                </div>
                                <asp:LinkButton ID="lbtViewDetailSSVL2" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <h3>
                                        <asp:Label ID="lblRejectedDocCount" runat="server" Text="0"></asp:Label>
                                        <sup style="font-size: 20px">Document(s)</sup></h3>
                                    <p>Rejected Document</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bookmark"></i>
                                </div>
                                <asp:LinkButton ID="lbtViewRejectedDoc" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="h4">Site Document Progress</div>
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:DropDownList ID="DdlPONO" runat="server" CssClass="list-group-item" AutoPostBack="true" Font-Size="12px"></asp:DropDownList>
                        </div>
                    </div>
                    <div style="margin-top: 5px;" class="col-lg-6">
                        <asp:Literal ID="ltrDocProgress" runat="server"></asp:Literal>
                    </div>
                </div>

            </div>
            <div class="col-md-4">
                <div class="box box-default">
                    <div class="box-header with-border">
                        <h3 class="box-title">Site Document Done</h3>

                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>
                        <div class="box-body">
                            <div class="info-box bg-green">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>

                                <div class="info-box-content">
                                    <span class="info-box-text">KPI L0 Done</span>
                                    <span class="info-box-number"></span>
                                    <div class="progress">
                                        <asp:Literal ID="ltrKPIL0DonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">
                                        <span style="font-size: 16pt; font-weight: bolder;">
                                            <asp:Literal ID="ltrKPIL0DoneCount" runat="server"></asp:Literal></span> Document
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                            <div class="info-box bg-blue">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text">Document Approval Status</span>
                                    <span class="info-box-number"></span>


                                    <div class="progress">
                                        <asp:Literal ID="ltrDASPerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description"><span style="font-size: 16pt; font-weight: bolder;">
                                        <asp:Literal ID="ltrDASCount" runat="server"></asp:Literal></span> Document
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
