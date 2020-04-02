<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardGeneralReport.aspx.vb"
    Inherits="frmDashboardGeneralReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard General Report</title>

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
        .labelText
        {
            font-family:verdana;
            font-size:9.5pt;
        }
        .labelTextLink
        {
            font-family:verdana;
            font-size:7.5pt;
            font-weight:bolder;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnRefresh" runat="server" Visible="false" />
        <div style="width: 100%;">
            <div style="float: left; width: 60%;">
                <div style="padding: 5px; background-color: Gray;">
                    <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;
                        color: White">Task Agenda User</span>
                </div>
                <div style="margin-top: 10px;">
                    <table cellpadding="2px" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="2px">
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAST Tsel Undersignature</span>
                                            <asp:LinkButton ID="LbtEbastTselUnderSignature" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAST NSN Undersignature</span>
                                            <asp:LinkButton ID="LbtEbastNSNUnderSignature" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAST Ready Creation</span>
                                            <asp:LinkButton ID="LbtEBASTReadyCreation" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table cellpadding="2px">
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAUT Tsel Undersignature</span>
                                            <asp:LinkButton ID="LbtBautTselUnderSignature" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAUT NSN Undersignature</span>
                                            <asp:LinkButton ID="LbtBautNSNUnderSignature" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <span class="labelText">BAUT Ready Creation</span>
                                            <asp:LinkButton ID="LbtBautReadyCreation" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span class="labelText">Rejected Documents</span>
                                            <asp:LinkButton ID="LbtRejectedDocuments" runat="server" Text="[More Details]" CssClass="labelTextLink"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="float: right; width: 30%">
                <div style="padding: 5px; background-color: Gray;">
                    <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;
                        color: White">eBAST Mutual Link</span>
                </div>
                <div style="margin-top: 10px;">
                    <table cellpadding="2px">
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" href="../RPT/frmEBASTDone.aspx" title="BAST DONE"><span style="font-family: Verdana;
                                    font-size: 11pt;">BAST Done Report</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" href="../RPT/frmEBAUTDone.aspx" title="BAUT DONE"><span style="font-family: Verdana;
                                    font-size: 11pt;">BAUT Done Report</span></a>
                                <asp:Button ID="Button1" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="ATP DONE" href="../Rpt/frmQCReport.aspx"><span style="font-family: Verdana;
                                    font-size: 11pt;">QC Done Report</span></a>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                            </td>
                            <td>
                                <a class="popup" title="ATP DONE" href="../Rpt/frmATPReport.aspx"><span style="font-family: Verdana;
                                    font-size: 11pt;">ATP Done Report</span></a>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
