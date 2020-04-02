<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardAgendaCRCO.aspx.vb"
    Inherits="DashBoard_frmDashboardAgendaCRCO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashoard Agenda CR CO</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <div style="float: left; width: 60%">
                <div>
                    <table cellpadding="2px" width="100%;">
                        <tr>
                            <td colspan="2" style="background-color: Gray; padding: 5px;">
                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;
                                    color: White">Task Agenda</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;
                                    color: Black">CR Task Agenda</span>
                            </td>
                            <td>
                                <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;
                                    color: Black">CO Task Agenda</span>
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
                                            <asp:Label ID="LblCRRejection" runat="server" Text="CR Rejection(0)" CssClass="lblTaskPending"></asp:Label>
                                            <asp:LinkButton ID="LbtCRRejection" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblCRReadyCreationNull" runat="server" Text="CR Ready Creation(0)" CssClass="lblTaskPending"></asp:Label>
                                            <asp:LinkButton ID="LbtCRReadyCreation" runat="server" CssClass="lblTaskPending"></asp:LinkButton>
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
