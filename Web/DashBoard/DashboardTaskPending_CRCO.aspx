<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashboardTaskPending_CRCO.aspx.vb"
    Inherits="DashBoard_DashboardTaskPending_CRCO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CO Task Pending</title>
    <style type="text/css">
        .labelText
        {
            font-family:verdana;
            font-size:9pt;
        }
        #leftPane
        {
            width:60%;
            Float:left;
            Border-style:none;
        }
        #rightPane
        {
            width:30%;
            Float:Right;
            Border-style:none;
        }
        
    </style>

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
                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">CO Document
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
                                <asp:HyperLink ID="HpSIS2GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSIS2GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC2GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSITAC2GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME2GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblCME2GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:LinkButton ID="LbtTI2GCRLink" runat="server" CssClass="labelText"></asp:LinkButton>
                                <asp:Label ID="LblTI2GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSIS3GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSIS3GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpSITAC3GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblSITAC3GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpCME3GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblCME3GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <asp:HyperLink ID="HpTI3GCRLink" runat="server" CssClass="labelText"></asp:HyperLink>
                                <asp:Label ID="LblTI3GCRLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div style="margin-top: 10px;">
                    <table>
                        <tr>
                            <td>
                                <img src="../images/tick.jpg" alt="tick" width="20" />
                            </td>
                            <td>
                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">Staff CO
                                    Document [Waiting Review / Approval] </span>
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
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=sis2g"
                                    id="aSIS2GLinkCount" runat="server">
                                    <asp:Literal ID="LtrSIS2GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblSIS2GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=sitac2g"
                                    id="aSITAC2GLinkCount" runat="server">
                                    <asp:Literal ID="LtrSITAC2GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblSITAC2GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=cme2g"
                                    id="aCME2GLinkCount" runat="server">
                                    <asp:Literal ID="LtrCME2GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblCME2GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=ti2g"
                                    id="aTI2GLinkCount" runat="server">
                                    <asp:Literal ID="LtrTI2GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblTI2GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=sis3g"
                                    id="aSIS3GLinkCount" runat="server">
                                    <asp:Literal ID="LtrSIS3GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblSIS3GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=sitac3g"
                                    id="aSITAC3GLinkCount" runat="server">
                                    <asp:Literal ID="LtrSITAC3GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblSITAC3GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=cme3g"
                                    id="aCME3GLinkCount" runat="server">
                                    <asp:Literal ID="LtrCME3GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblCME3GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText" Visible="true"></asp:Label>
                            </td>
                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                <a class="popup" title="CO under Task Pending Under Your Staff" href="CRCODashboard_TselUnderSignature.aspx?scope=ti3g"
                                    id="aTI3GLinkCount" runat="server">
                                    <asp:Literal ID="LtrTI3GStaffCount" runat="server"></asp:Literal></a>
                                <asp:Label ID="lblTI3GStaffLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="rightPane">
                <div style="margin-top: 10px; padding: 5px; background-color: Gray;">
                    <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder; color: White">
                        eBAST Mutual Link</span>
                </div>
                <div style="margin-top: 10px;">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
