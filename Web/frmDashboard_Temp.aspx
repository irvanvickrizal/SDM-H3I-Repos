<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboard_Temp.aspx.vb"
    Inherits="frmDashboard_Temp" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics2.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Dashboard</title>

    <script type="text/javascript" language="javascript">
        var vardes = 0;
        function popwindowDashBoard(id) {
            if (id == 1) {
                window.open('DashBoard/dashboardpopupbast.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            else
                if (id == 2) {
                    window.open('DashBoard/dashboardpopupbaut.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
                else
                    if (id == 3) {
                        window.open('DashBoard/DocumentWrokFlow.aspx', 'welcome', 'width=500,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                    }
                    else
                        if (id == 4) {//Pending upload docs
                            window.open('DashBoard/PendingUploadDocument.aspx?id=1', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                        }
                        else
                            if (id == 5) {
                                window.open('DashBoard/frmDocApproved.aspx', 'welcome', 'width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                            }
                            else
                                if (id == 6) {//sls time
                                    window.open('DashBoard_New/frmRejectedDocument_Initiator.aspx', 'welcome', 'width=500,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                }
                                else
                                    if (id == 7) {//Rejected docs
                                        window.open('DashBoard_New/frmRejectedDocument_Initiator.aspx', 'welcome', 'width=850,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                    }
                                    else
                                        if (id == 8) {//DocSign 30days
                                            window.open('HCPT_Dashboard/frmDoc30days.aspx', 'welcome', 'width=850,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                        }
                                        else
                                            if (id == 9) {//Missing EPM
                                                window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=1', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                            }
                                            else
                                                if (id == 10) {//Duplicate Sites
                                                    window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=2', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                }
                                                else
                                                    if (id == 11) {//EBAST Done
                                                        window.open('DashBoard/EBastDoneDetails.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                    }
                                                    else {
                                                        window.open('DashBoard/frmsitestatus.aspx', 'welcome', 'width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                    }
        }

        function popSitesDetails(id) {
            if (id == 1) {
                window.open('DashBoard/dashboardpopupbast.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            else
                if (id == 8) {
                    window.open('DashBoard/dashboardpopupbaut.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
                else {
                    window.open('DashBoard/EBastDoneDetails.aspx?id=' + id, 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
        }

        function popwindowMissingSites(pono) {
            window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=0&pono=' + pono, 'welcome', 'width=550,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }

        function popwindowDashBoardpending(id) {
            window.open('DashBoard/frmDocApproved.aspx?id=' + id, 'welcome', 'width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }

        function DashBoard(id) {
            window.open('DashBoard/dashboard.aspx?pono=' + id, 'welcome', 'width=750,height=325,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
        }

        function SiteDetails() {
            window.open('DashBoard/SiteDetails.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
        }

        function SiteStatus() {
            if (vardes == 0) {
                for (intcount = 0; intcount < document.getElementById("hdTotal").value; intcount++) {
                    document.getElementById("TRDesc" + intcount).style.display = "none";
                    document.getElementById("TRAsc" + intcount).style.display = "";
                }
                document.getElementById("idUpArrow").src = "Images/arrow2.png";
                vardes = 1;
            }
            else {
                for (intcount = 0; intcount < document.getElementById("hdTotal").value; intcount++) {
                    document.getElementById("TRDesc" + intcount).style.display = "";
                    document.getElementById("TRAsc" + intcount).style.display = "none";
                }
                document.getElementById("idUpArrow").src = "Images/arrow1.png";
                vardes = 0;
            }
        }

        function OverAllStatus(pono, id) {
            window.open('DashBoard/frmDashBoardDetails.aspx?P=' + pono + '&id=' + id, 'welcome', 'width=475,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }

        function popSitesSLA(id) {
            window.open('DashBoard/SLADetails.aspx?id=' + id, 'welcomeSLA', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
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
    
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left: 10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 99%; height: 450px;">
            <div class="headerpanel">
                HOME DASHBOARD
            </div>
            <div style="margin-top: 10px;">
                <asp:Button ID="btnRefresh" runat="server" Visible="false" />
                <asp:MultiView ID="MvCorePanel" runat="server">
                    <asp:View ID="VwGeneral" runat="server">
                        <table>
                            <tr>
                                <td style="vertical-align: top; width: 96px; height: 21px; text-align: left">
                                    <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" ExpandEffect="None"
                                        Height="190px" Style="vertical-align: top; text-align: left" StyleSetName=""
                                        Width="421px">
                                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                                ColorTop="0, 45, 150" />
                                        </PanelStyle>
                                        <Header Text="Agenda" TextAlignment="Left">
                                            <ExpandedAppearance>
                                                <Styles BackColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    Font-Names="Verdana" Font-Size="14px" ForeColor="White">
                                                    <Padding Bottom="8px" Left="10px" Top="8px" />
                                                    <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                                        WidthBottom="0px" />
                                                </Styles>
                                            </ExpandedAppearance>
                                            <HoverAppearance>
                                                <Styles ForeColor="Blue">
                                                </Styles>
                                            </HoverAppearance>
                                            <CollapsedAppearance>
                                                <Styles Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                                                </Styles>
                                            </CollapsedAppearance>
                                            <ExpansionIndicator Height="0px" Width="0px" />
                                        </Header>
                                        <Template>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <b>Process-TI:</b></td>
                                                </tr>
                                                <tr>
                                                    <td id="tdAgenda" runat="server" valign="top" style="width: 100%; height: 25px; font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left;"></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 100%; height: 25px; font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: right;">
                                                        [<a class='popup' href='../frmUserActivityLog.aspx' title='User Activity Log'>User Activity Log</a>]
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebPanel>
                                </td>
                                <td style="vertical-align: top; width: 96px; height: 50px; text-align: left">
                                    <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" ExpandEffect="None"
                                        Height="150px" Style="vertical-align: top; text-align: left" StyleSetName=""
                                        Width="421px">
                                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                                ColorTop="0, 45, 150" />
                                        </PanelStyle>
                                        <Header Text="Site Acceptance Status" TextAlignment="Left">
                                            <ExpandedAppearance>
                                                <Styles BackColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                    Font-Names="Verdana" Font-Size="14px" ForeColor="White">
                                                    <Padding Bottom="8px" Left="10px" Top="8px" />
                                                    <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                                        WidthBottom="0px" />
                                                </Styles>
                                            </ExpandedAppearance>
                                            <HoverAppearance>
                                                <Styles ForeColor="Blue">
                                                </Styles>
                                            </HoverAppearance>
                                            <CollapsedAppearance>
                                                <Styles Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                                                </Styles>
                                            </CollapsedAppearance>
                                            <ExpansionIndicator Height="0px" Width="0px" />
                                        </Header>
                                        <Template>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="font-family: verdana; padding: 5px; background-color: #cfcfcf; font-weight: bolder; color: Black; font-size: 10pt; width: 60%"
                                                        colspan="3">Milestone
                                                    </td>
                                                    <td style="width: 40%; background-color: #cfcfcf; font-weight: bolder; color: Black; font-family: Verdana; font-size: 10pt;"
                                                        align="center">Total</td>
                                                </tr>
                                                <tr>
                                                    <td class="dashboard" valign="top" style="width: 3%">
                                                        <asp:Image ID="Image1" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                                            Width="14px" /></td>
                                                    <td valign="top" class="dashboard" style="width: 57%">
                                                        <div runat="server" id="Tdbastdone" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" valign="top" style="width: 15%">
                                                        <div runat="server" id="TdbastdoneNO" style="font-weight: normal; font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" style="width: 25%" valign="top">
                                                        <div runat="server" id="Tdbastdonedays" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: center">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="dashboard" valign="top" style="width: 3%">
                                                        <asp:Image ID="Image3" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                                            Width="14px" /></td>
                                                    <td valign="top" class="dashboard" style="width: 57%">
                                                        <div runat="server" id="Tdready4bast" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" valign="top" style="width: 15%">
                                                        <div runat="server" id="Tdready4bastno" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" style="width: 25%" valign="top">
                                                        <div runat="server" id="Tdready4bastdays" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: center">
                                                        </div>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td class="dashboard" valign="top" style="width: 3%">
                                                        <asp:Image ID="Image5" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                                            Width="14px" /></td>
                                                    <td valign="top" class="dashboard" style="width: 57%">
                                                        <div runat="server" id="tdCACdone" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" valign="top" style="width: 15%">
                                                        <div runat="server" id="tdCACdoneno" style="font-weight: normal; font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" style="width: 25%" valign="top">
                                                        <div runat="server" id="tdcacdonedays" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: center">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="dashboard" valign="top" style="width: 3%">
                                                        <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                                            Width="14px" /></td>
                                                    <td valign="top" class="dashboard" style="width: 57%">
                                                        <div runat="server" id="tdbautdone" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" valign="top" style="width: 15%">
                                                        <div runat="server" id="tdbautdoneno" style="font-weight: normal; font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" style="width: 25%" valign="top">
                                                        <div runat="server" id="tdbautdonedays" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: center">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="dashboard" valign="top" style="width: 3%">
                                                        <asp:Image ID="Image4" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                                            Width="14px" /></td>
                                                    <td valign="top" class="dashboard" style="width: 57%">
                                                        <div runat="server" id="tdsiteapprovalstatus" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" valign="top" style="width: 15%">
                                                        <div runat="server" id="tdsiteapprovalno" style="font-weight: normal; font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left">
                                                        </div>
                                                    </td>
                                                    <td class="dashboard" style="width: 25%" valign="top">
                                                        <div runat="server" id="tdsiteapprovalstatusdays" style="font-weight: normal; font-size: 9pt; vertical-align: top; font-family: Verdana; text-align: center">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div style="text-align: right; vertical-align: bottom; margin-top: 5px; display:none;">
                                                            <asp:Button ID="BtnViewDashboard" runat="server" Text="View Detail Dashboard" ForeColor="white"
                                                                Style="border-style: solid; border-width: 1px; background-color: Gray; font-family: Verdana; font-size: 10px; padding: 3px 3px 3px 3px; height: 22px;"
                                                                OnClick="BtnViewDashboardClick" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </igmisc:WebPanel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="VwCRCOOnly" runat="server">
                        <div style="width: 100%;">
                            <div style="float: left; width: 60%">
                                <div>
                                    <table cellpadding="2px" width="100%;">
                                        <tr>
                                            <td colspan="2" style="background-color: Gray; padding: 5px;">
                                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder; color: White">Task Agenda</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder; color: Black">CR Task Agenda</span>
                                            </td>
                                            <td>
                                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder; color: Black">CO Task Agenda</span>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblSiteNeedCRNull" runat="server" Text="Site Need CR(0)" CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=nc" title="Site Need CR"
                                                                runat="server" id="SiteNeedCRLink">
                                                                <asp:Label ID="LblSiteNeedCR" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                            <asp:LinkButton ID="LbtSiteNeedCR" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCRReadyCreationNull" runat="server" Text="CR Ready Creation (0)" CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../CR/frmCRReadyCreation.aspx?typedash=nc" title="Site Need CR"
                                                                runat="server" id="A2">
                                                                <asp:Label ID="LblCRReadyCreation" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCRRejection" runat="server" Text="CR Rejection(0)" CssClass="lblTaskPending"></asp:Label>
                                                            <asp:LinkButton ID="LbtCRRejection" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCRUnderNSNSignatureNull" runat="server" Text="CR NSN Under Signature(0)"
                                                                CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=ns" title="NSN Under Signature"
                                                                runat="server" id="CRNSNSignLink">
                                                                <asp:Label ID="LblCRUnderNSNSignature" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCRTselUnderSignatureNull" runat="server" Text="CR Tsel Under Signature(0)"
                                                                CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=ts" title="NSN Under Signature"
                                                                runat="server" id="CRTselSignLink">
                                                                <asp:Label ID="LblCRTselUnderSignature" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCRFinalNull" runat="server" Text="CR Final(0)" CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=cf" title="Document CR Final"
                                                                runat="server" id="CRFinalLink">
                                                                <asp:LinkButton ID="LbtCRDashboardAgenda" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
                                                                <asp:Label ID="LblCRFinal" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCOReadyCreationNull" runat="server" Text="CO Ready Creation(0)"
                                                                CssClass="lblTaskPending"></asp:Label>
                                                            <a href="../CO/frmCOReadyCreation.aspx" title="CO Ready Creation" runat="server"
                                                                id="COReadyCreationLink">
                                                                <asp:Label ID="LblCOReadyCreation" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCORejectionNull" runat="server" Text="CO Rejection(0)" CssClass="lblTaskPending"></asp:Label>
                                                            <asp:LinkButton ID="LbtCORejection" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCONSNUnderSignatureNull" runat="server" Text="CO NSN Under Signature(0)"
                                                                CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=cons" title="CO NSN Under Signature"
                                                                runat="server" id="CONSNSignLink">
                                                                <asp:Label ID="LblCONSNUnderSignature" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCOTselUnderSignatureNull" runat="server" Text="CO Tsel Under Signature(0)"
                                                                CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=cots" title="CO Tsel Under Signature"
                                                                runat="server" id="COTselSignLink">
                                                                <asp:Label ID="LblCOTselUnderSignature" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="LblCODoneNull" runat="server" Text="CO Done" CssClass="lblTaskPending"></asp:Label>
                                                            <a class="popup" href="../Dashboard/CRDashboardAgenda.aspx?typedash=cod" title="CO Done"
                                                                runat="server" id="A1">
                                                                <asp:Label ID="LblCODone" runat="server" CssClass="lblTaskPending"></asp:Label>
                                                            </a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div style="float: right; width: 30%">
                                <div style="padding: 5px; background-color: Gray;">
                                    <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder; color: White">eBAST Mutual Link</span>
                                </div>
                                <div style="margin-top: 10px;">
                                    <table cellpadding="2px">
                                        <tr valign="top">
                                            <td>
                                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                            </td>
                                            <td>
                                                <a class="popup" href="../RPT/frmEBASTDone.aspx" title="BAST DONE"><span style="font-family: Verdana; font-size: 11pt;">BAST Done Report</span></a>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                            </td>
                                            <td>
                                                <a class="popup" href="../RPT/frmEBAUTDone.aspx" title="BAUT DONE"><span style="font-family: Verdana; font-size: 11pt;">BAUT Done Report</span></a>
                                                <asp:Button ID="Button1" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                            </td>
                                            <td>
                                                <a class="popup" title="ATP DONE" href="../Rpt/frmQCReport.aspx"><span style="font-family: Verdana; font-size: 11pt;">QC Done Report</span></a>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                            </td>
                                            <td>
                                                <a class="popup" title="ATP DONE" href="../Rpt/frmATPReport.aspx"><span style="font-family: Verdana; font-size: 11pt;">ATP Done Report</span></a>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
                <div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
