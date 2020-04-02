<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteAllTaskPending.aspx.vb"
    Inherits="DashBoard_frmSiteAllTaskPending" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Dashboard</title>
    <style type="text/css">
        .labelText {
            font-family: verdana;
            font-size: 9pt;
        }

        #leftPane {
            width: 60%;
            float: left;
            border-style: none;
        }

        #rightPane {
            width: 30%;
            float: Right;
            border-style: none;
        }
    </style>

    <script type="text/javascript">
        function popSitesDetails(id) {
            if (id == 1) {
                window.open('EBastDoneDetails.aspx?id=0', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            else
                if (id == 8) {
                    window.open('dashboardpopupbaut.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
                else {
                    window.open('EBastDoneDetails.aspx?id=' + id, 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
        }
        function PopupRejectedDoc() {
            window.open('PendingUploadDocument.aspx?id=2', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }
    </script>

    <script type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>

    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>

    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />

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
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="box box-default">
                    <div class="box-header with-border">
                        <h3 class="box-title">Your Agenda</h3>
                        <div class="col-md-12" style="height: 20px;"></div>
                        <div class="col-md-4">
                            <div class="info-box">
                                <span class="info-box-icon bg-red"><i class="icon ion-document-text"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-number">
                                        <asp:LinkButton ID="lbtTaskPendingCount" runat="server" Text="Your Task Pending" CssClass="text-info" data-toggle="modal" data-target="#myModal"></asp:LinkButton>
                                    </span>
                                    <span class="info-box-text">
                                        <asp:LinkButton ID="LbtTI2GLink" runat="server" CssClass="labelText" Font-Size="30px"></asp:LinkButton>
                                        <asp:Label ID="LblTI2GLinkDisabled" runat="server" Text="0" CssClass="labelText" Font-Size="30px"></asp:Label>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>

                        </div>
                        <div class="col-md-4">
                            <div class="info-box">
                                <span class="info-box-icon bg-green"><i class="icon ion-document"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-number">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Signed in 30 Days" Enabled="false" CssClass="text-info" data-toggle="modal" data-target="#myModal"></asp:LinkButton>
                                    </span>
                                    <span class="info-box-text">
                                        <asp:Label ID="LblTI2GAPPLink" runat="server" Text="0" CssClass="labelText" Font-Size="30px"></asp:Label>
                                        <a class="popup" title="Doc Signed 30 days" href="../HCPT_Dashboard/frmDoc30days.aspx"
                                            runat="server" id="LbtTI2GAppLink">
                                            <asp:Label ID="LblLinkAPP2G" runat="server" CssClass="labelText" Font-Size="30px"></asp:Label>
                                        </a>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>                            
                        </div>
						 <div class="col-md-4">  <%--Modified by Fauzan, 13 Nov 2018. Hide Rejected Document Panel (Assignment 2)--%>
							<div class="info-box">
                                <span class="info-box-icon bg-yellow"><i class="icon ion-document"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-number">
                                        <asp:LinkButton ID="lbtRejectedDocTitle" runat="server" Text="Rejected Document" Enabled="false" CssClass="text-info" data-toggle="modal" data-target="#myModal"></asp:LinkButton>
                                    </span>
                                    <span class="info-box-text">
                                        <asp:Label ID="lblRejectedDocCountDisabled" runat="server" Text="0"></asp:Label>
                                        <a class="popup" title="Rejected Document" href="frmRejectedDocNPOHCPT.aspx"
                                            runat="server" id="LbtRejectedDoc">
                                            <asp:Label ID="lblRejectedDocCount" runat="server" CssClass="labelText" Font-Size="30px"></asp:Label>
                                        </a>
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>    
                            <div class="small-box bg-yellow" style="display:none;">
                                <div class="inner">
                                    <h3>
                                        
                                       
                                    <p>Rejected</p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bookmark"></i>
                                </div>
                                 <asp:LinkButton ID="lbtViewRejectedDoc" runat="server" CssClass="small-box-footer">View Detail<i class="glyphicon glyphicon-circle-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
				
                <div class="h4">Site Document Progress</div>
				<div class="col-md-8">
					<div class="form-inline">
						<div class="form-group">
							<asp:DropDownList ID="DdlPONO" runat="server" CssClass="list-group-item" AutoPostBack="true" Font-Size="12px"></asp:DropDownList>
						</div>                   
					</div>
					<div style="margin-top: 5px;" class="col-lg-6">
						<asp:Literal ID="ltrDocProgress" runat="server"></asp:Literal>
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
                                    <span class="info-box-text">FAC</span>
                                    <span class="info-box-number">
                                        <asp:Literal ID="ltrFACDoneCount" runat="server"></asp:Literal></span>

                                    <div class="progress">
                                        <asp:Literal ID="ltrFACDonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">0% Increase in 30 Days
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                            <div class="info-box bg-green">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>

                                <div class="info-box-content">
                                    <span class="info-box-text">PAC</span>
                                    <span class="info-box-number">
                                        <asp:Literal ID="ltrPACDoneCount" runat="server"></asp:Literal></span>

                                    <div class="progress">
                                        <asp:Literal ID="ltrPACDonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">0% Increase in 30 Days
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                            <div class="info-box bg-green">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>
                                <div class="info-box-content">
                                    <span class="info-box-text">CAC</span>
                                    <span class="info-box-number">
                                        <asp:Literal ID="ltrCACDoneCount" runat="server"></asp:Literal></span>

                                    <div class="progress">
                                        <asp:Literal ID="ltrCACDonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">0% Increase in 30 Days
                                    </span>
                                </div>
                            </div>
                            <div class="info-box bg-blue" style="display:none;">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>

                                <div class="info-box-content">
                                    <span class="info-box-text">MSFI</span>
                                    <span class="info-box-number">
                                        <asp:Literal ID="ltrMSFICount" runat="server"></asp:Literal></span>

                                    <div class="progress">
                                        <asp:Literal ID="ltrMSFIDonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">0% Increase in 30 Days
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
							<div class="info-box bg-blue">
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
                            <div class="info-box bg-yellow" style="display:none;">
                                <span class="info-box-icon"><i class="ion ion-ios-gear-outline"></i></span>

                                <div class="info-box-content">
                                    <span class="info-box-text">ATP Document</span>
                                    <span class="info-box-number">
                                        <asp:Literal ID="ltrATPDoneCount" runat="server"></asp:Literal></span>

                                    <div class="progress">
                                        <asp:Literal ID="ltrATPDonePerc" runat="server"></asp:Literal>
                                    </div>
                                    <span class="progress-description">0% Increase in 30 Days
                                    </span>
                                </div>
                                <!-- /.info-box-content -->
                            </div>
                           
                        </div>
                    </div>

                </div>
            </div>
            </div>
            
        </div>
        <div id="MainPanel" style="display: none;">
            <div id="leftPane">
                <div>
                    <table>
                        <tr>
                            <td>
                                <img src="../images/tick.jpg" alt="tick" width="20" />
                            </td>
                            <td>
                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">TASK PENDING
                                    [Waiting Your Review / Approval] </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px; margin-left: 25px;">
                    <table border="1" cellpadding="2" cellspacing="2">
                        <tr>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS2GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSIS2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC2GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSITAC2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME2GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblCME2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;"></td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS3GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSIS3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC3GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSITAC3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME3GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblCME3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpTI3GLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblTI3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 10px;">
                    <table>
                        <tr>
                            <td>
                                <img src="../images/tick.jpg" alt="tick" width="20" />
                            </td>
                            <td>
                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">DOCUMENT
                                    SIGNED LAST 30 DAYS </span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px; margin-left: 25px;">
                    <table border="1" cellpadding="2" cellspacing="2">
                        <tr>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS2GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSIS2GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC2GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSITAC2GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME2GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblCME2GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;"></td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS3GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSIS3GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC3GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSITAC3GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME3GAPPLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblCME3GAPPLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpTI3GAPPLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblTI3GAPPLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 10px;">
                    <table>
                        <tr>
                            <td>
                                <img src="../images/tick.jpg" alt="tick" width="20" />
                            </td>
                            <td>
                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">RFT READY
                                    CREATION</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px; margin-left: 25px;">
                    <table border="1" cellpadding="2" cellspacing="2">
                        <tr>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                            </th>
                            <th style="background-color: #ff7f27;">
                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS2GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSIS2GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC2GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSITAC2GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME2GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblCME2GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:Label ID="LblTI2GRFTLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                <a class="popup" title="Doc Signed 30 days" href="../HCPT_Dashboard/frmRFTReadyCreation_viewonly.aspx"
                                    runat="server" id="LbtTI2GRFTLink">
                                    <asp:Label ID="LblLinkRFT2G" runat="server" CssClass="labelText"></asp:Label></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS3GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSIS3GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC3GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblSITAC3GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME3GRFTLink" runat="server" CssClass="labelText" Visible="false"></asp:HyperLink>
                                <asp:Label ID="LblCME3GRFTLink" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpTI3GRFTLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblTI3GRFTLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="rightPane">
                <div style="margin-top: 10px; padding: 5px; background-color: Gray;">
                    <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder; color: White">Document Report</span>
                </div>
                <div style="margin-top: 10px;">
                    <table cellpadding="2px" width="100%">
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" href="../RPT/frmEBAUTDone.aspx?docid=1031" title="RFT DONE"><span
                                    style="font-family: Verdana; font-size: 8pt;">RFT Done Report</span></a>
                                <asp:Button ID="btnRefresh" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="KPI Report" href="../RPT/frmEBAUTDone.aspx?docid=2025"><span
                                    style="font-family: Verdana; font-size: 8pt;">KPI Done Report</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="KPI Report" href="../RPT/frmKPIL0Rpt.aspx?docid=2145"><span
                                    style="font-family: Verdana; font-size: 8pt;">KPI Done Report</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="ATP DONE" href="../RPT/frmEBAUTDone.aspx?docid=2001"><span
                                    style="font-family: Verdana; font-size: 8pt;">ATP Done Report</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a href="#" onclick="PopupRejectedDoc()"><span style="font-family: Verdana; font-size: 8pt;">Rejected Documents(Last 30 days)</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class='popup' href='../frmUserActivityLog.aspx' title='User Activity Log'><span style="font-family: Verdana; font-size: 8pt;">User Activity
                                    Log</span></a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
