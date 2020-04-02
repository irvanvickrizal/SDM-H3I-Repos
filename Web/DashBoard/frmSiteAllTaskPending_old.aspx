<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteAllTaskPending.aspx.vb"
    Inherits="DashBoard_frmSiteAllTaskPending" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

</head>
<body>
    <form id="form1" runat="server">
        <div id="MainPanel">
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
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:LinkButton ID="LbtTI2GLink" runat="server" CssClass="labelText"></asp:LinkButton>
                                <asp:Label ID="LblTI2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
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
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:Label ID="LblTI2GAPPLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                <a class="popup" title="Doc Signed 30 days" href="../HCPT_Dashboard/frmDoc30days.aspx"
                                    runat="server" id="LbtTI2GAppLink">
                                    <asp:Label ID="LblLinkAPP2G" runat="server" CssClass="labelText"></asp:Label>
                                </a>
                            </td>
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
                <div style="margin-top: 10px;display:none;">
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
                <div style="margin-top: 5px; margin-left: 25px;display:none;">
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
                    <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder; color: White">
                        Document Report</span>
                </div>
                <div style="margin-top: 10px;">
                    <table cellpadding="2px" width="100%">
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" href="../RPT/frmEBASTDone.aspx?docid=1031" title="FAC DONE"><span
                                    style="font-family: Verdana; font-size: 8pt;">FAC Done Report</span></a>
                                <asp:Button ID="btnRefresh" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="CAC Done" href="../RPT/frmCACReport.aspx?docid=2046"><span
                                    style="font-family: Verdana; font-size: 8pt;">CAC Done Report</span></a>
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
                                <a class="popup" title="ATP DONE" href="../RPT/frmATPReport.aspx?docid=2001"><span
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
                                <a href="#" onclick="PopupRejectedDoc()"><span style="font-family: Verdana; font-size: 8pt;">
                                    Rejected Documents(Last 30 days)</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td >
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
