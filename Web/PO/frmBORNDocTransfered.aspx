<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBORNDocTransfered.aspx.vb" Inherits="PO_frmBORNDocTransfered" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Doc Review Panel</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lblBoldHeader {
            font-family: Verdana;
            font-size: 15px;
            font-weight: bolder;
        }

        .PageBreak {
            page-break-before: always;
        }

        #PleaseWait {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/animation_processing.gif);
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
            z-index: 1;
            height: 100%;
            position: fixed;
            top: 0;
            left: 0;
        }

        .MainCSS {
            margin-bottom: 0px;
            margin-left: 20px;
            margin-right: 20px;
            margin-top: 0px;
            width: 800px;
            height: 700px;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10px;
            color: #000000;
            text-align: center;
            font-weight: bold;
        }

        .lblFieldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
        }

        .lblFieldBoldText {
            font-family: verdana;
            font-size: 9px;
            color: #000000;
            text-align: left;
            font-weight: bolder;
        }
    </style>

    <script type="text/javascript">
        function gopage(id) {
            var url = '../BAUT/frmTI_WCTRBAST.aspx?id=' + id + '&from=bast';
            window.location = url;
        }

        function WindowsCloseApprover() {
            alert('Signed Sucessfully');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function DGFailedProcess(desc) {
            alert('Digital Signature Failed, Please try again!' + desc);
        }

        function WindowsCloseApproverFail() {
            alert('Sign Process Failed');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowsCloseReviewer() {
            alert('Reviewed Sucessfully.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowsCloseReviewerFail() {
            alert('Review Process Sucessfully.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function windowsCloseATPReviewerFailGenerate() {
            alert('Fail to generate ATP Approval Sheet.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function windowsCloseATPReviewer() {
            alert('Reviewed Sucessfully.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0&doctype=atp';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowRejectClose() {
            alert('Doc Rejected Sucessfully.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowRejectFailClose() {
            alert('Doc Rejected Fail.');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowRejectCloseATP() {
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0&doctype=atp';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function getQueryVariable(variable) {
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                if (pair[0] == variable) {
                    return pair[1];
                }
            }
        }

        function waitPreloadPage() { //DOM
            document.getElementById('prepage').style.display = '';
            if (document.getElementById) {
                document.getElementById('prepage').style.visibility = 'hidden';
            }
            else {
                if (document.layers) { //NS4
                    document.prepage.visibility = 'hidden';
                }
                else { //IE4
                    document.all.prepage.style.visibility = 'hidden';
                }
            }
        }
        function Showmain(type) {
            if (type == "success") {
                alert('Document Successfully Generated');
            }
            window.location = '../PO/frmSiteDocUploadTree.aspx'
        }
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
            function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>
</head>
<body class="MainCSS">

    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnDocName" runat="server" />
        <asp:HiddenField ID="hdnSiteNo" runat="server" />
        <asp:HiddenField ID="hdnScope" runat="server" />
        <asp:HiddenField ID="hdnPONO" runat="server" />
        <asp:HiddenField ID="hdnSiteid" runat="server" />
        <asp:HiddenField ID="hdnVersion" runat="server" />
        <div id="dvPrintApproval" runat="server" style="width: 100%; height: 700px;">
            <table style="width: 100%;">
                <tr>
                    <td style="height: 295px">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 97%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/nokia.png" height="36" width="104" alt="nsnlogo" />
                                </td>
                                <td colspan="4" align="center" class="lblBoldHeader" valign="top" style="width: 60%; text-align: center;">
                                    <b>Document Approval Sheet</b>
                                    <br />
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46" width="60" alt="hcptlogo" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td colspan="3" class="Hcap">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="3">This approval sheet declares that the <b>
                                    <asp:Literal ID="ltrDocname" runat="server"></asp:Literal></b> has been performed
                                at the below location.
                                
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="3">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px;">PO No
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="divPONO" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px;">Site Name
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="divSiteName" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">SiteID / LinkID
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="divSiteID" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">Scope
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="divScope" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="lblText">And the work has been considered as complete and satisfactory fulfil the standard
                                technical requirements in H3I project.
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="lblText" colspan="4">
                                    <div id="divReviewer" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="lblText">This approval sheet is automatically generated by system upon acceptance of all person
                    in charge and no more signatures on hard copy is required. This sheet is inseparable
                    from the
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        report and should be the first thing seen when opening the report.
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvPrintPOSVC" runat="server" style="width: 800px; height: 700px;">
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="height: 95px">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 97%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/nokia.png" height="36" width="104" alt="nsnlogo" />
                                </td>
                                <td colspan="4" align="center" class="lblBoldHeader" valign="top" style="width: 60%; text-align: center;">
                                    <b>
                                        <asp:Literal ID="ltrDocNameTitlePOSVC" runat="server"></asp:Literal></b>
                                    <br />
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46" width="60" alt="hcptlogo" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td class="lblText" style="width: 206px;">PO No :
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px">
                                    <div id="lblPONOSVC" runat="server" style="font-weight: bolder;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px;">Site Name
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvSitenameSvc" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">SiteID / LinkID
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvSiteIDSvc" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">Scope
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvScopeSvc" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Literal ID="ltrBOQListSVC" runat="server"></asp:Literal>
        </div>
        <div id="dvPrintPOEqp" runat="server" style="width: 800px; height: 700px;">
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="height: 95px">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 97%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/nokia.png" height="36" width="104" alt="nsnlogo" />
                                </td>
                                <td colspan="4" align="center" class="lblBoldHeader" valign="top" style="width: 60%; text-align: center;">
                                    <b>
                                        <asp:Literal ID="ltrDocTitleNameEqp" runat="server"></asp:Literal></b>
                                    <br />
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="https://sdmthree.nsnebast.com/Images/three-logo.png" height="46" width="60" alt="hcptlogo" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td class="lblText" style="width: 206px;">PO No :
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px">
                                    <div id="lblPONOEqp" runat="server" style="font-weight: bolder;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px;">Site Name
                                </td>
                                <td style="width: 1%;">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvSitenameEqp" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">SiteID / LinkID
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvSiteIDEqp" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">Scope
                                </td>
                                <td style="width: 1%">:
                                </td>
                                <td class="lblText" style="width: 1130px; font-weight: bolder;">
                                    <div id="dvScopeEqp" runat="server" style="width: 100%; text-align: left;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Literal ID="ltrBOQListEqp" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 10px; text-align: center; width: 100%;">
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClientClick="return confirm('Are you sure you want to Confirm this Doc was Valid?')" />
        </div>
    </form>
</body>
</html>
