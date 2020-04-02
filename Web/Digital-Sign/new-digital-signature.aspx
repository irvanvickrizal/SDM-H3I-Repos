<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false" CodeFile="new-digital-signature.aspx.vb" Inherits="Digital_Sign_new_digital_signature" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doc Review Panel</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" /> 
    <style type="text/css">
        .lblBoldHeader {
            font-family: Verdana;
            font-size: 15px;
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
        var atLeast = 1
        function CheckItem() {
            var chk = ValidateCheckList();
            var remrks = ValidateRemarks();
            if (chk == false) {
                alert("Please tick at least one Reason");
                return false;
            }
            else {
                if (remrks == false) {
                    alert("Please describe in detail your reason of rejection (min.10 Chars)");
                    return false;
                }
            }
            return true;
        }

        function ValidateRemarks() {
            var remarks = document.getElementById("<%=TxtRemarksReject.ClientID %>");
            if (remarks.value.length < 10) {
                return false;
            }
            return true;
        }

        function ValidateCheckList() {
            var CHK = document.getElementById("<%=CbList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                return false;
            }
            return true;
        }

        var atleastsign = 1
        function CheckItemSignReject() {
            var chk = ValidateSignCheckList();
            var remrks = ValidateSignRemarks();
            if (chk == false) {
                alert("Please tick at least one Reason");
                return false;
            }
            else {
                if (remrks == false) {
                    alert("Please describe in detail your reason of rejection (min.10 Chars)");
                    return false;
                }
            }
            return true;
        }

        function ValidateSignRemarks() {
            var remarks = document.getElementById("<%=TxtRemarks_SignRejectPanel.ClientID %>");
            if (remarks.value.length < 10) {
                return false;
            }
            return true;
        }

        function ValidateSignCheckList() {
            var CHK = document.getElementById("<%=CbReasonLists.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                return false;
            }
            return true;
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

    <style type="text/css">
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
    </style>
</head>
<body>
    <div id="dvPrint" runat="server" style="width: 100%;">
        <table style="width: 100%;">
            <tr>
                <td style="height: 295px">
                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
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
                            <td class="lblText" style="width: 1130px">
                                <div id="divPONO" runat="server" style="width: 100%; text-align: left;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" style="width: 206px;">Site Name
                            </td>
                            <td style="width: 1%;">:
                            </td>
                            <td class="lblText" style="width: 1130px">
                                <div id="divSiteName" runat="server" style="width: 100%; text-align: left;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" style="width: 206px">Site ID
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td class="lblText" style="width: 1130px">
                                <div id="divSiteID" runat="server" style="width: 100%; text-align: left;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" style="width: 206px">Scope
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td class="lblText" style="width: 1130px;">
                                <div id="divScope" runat="server" style="width: 100%; text-align: left;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" style="width: 206px">BTS Type
                            </td>
                            <td style="width: 1%">:
                            </td>
                            <td class="lblText" style="width: 1130px;">
                                <div id="divBTSType" runat="server" style="width: 100%; text-align: left;">
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
                    from the ATP report and should be the first thing seen when opening the report.
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="HdWFID" runat="server" />
        <asp:HiddenField ID="HdTskId" runat="server" />
        <asp:HiddenField ID="HdGRPID" runat="server" />

        <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" style="overflow: scroll;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblText" colspan="4">
                        <asp:Label ID="LblDocPath" runat="server" Visible="false"></asp:Label>
                        <div id="divReviewer2" runat="server" style="width: 100%; text-align: left; background-color: gray; padding: 4px;">
                        </div>
                    </td>
                </tr>
                <tr id="listdocuments" runat="server">
                    <td class="hgap" style="width: 972px">
                        <asp:UpdatePanel ID="updocchild" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table>
                                        <tr valign="top">
                                            <td>
                                                <asp:Label ID="lblacceptRemarks" runat="Server">Remarks</asp:Label>
                                            </td>
                                            <td>
                                                <div style="padding-left: 8px;">
                                                    <asp:TextBox ID="txtRemarksAccept" runat="server" TextMode="MultiLine"
                                                        Height="80px" CssClass="lblText" Width="380px"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                                        EmptyDataText="No Attachment doc" DataKeyNames="Doc_Id">
                                        <PagerSettings Position="TopAndBottom" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="Server">
                                                    </asp:Label>
                                                    <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                            <asp:BoundField DataField="docpath" HeaderText="Path" />
                                            <asp:BoundField DataField="DocName" HeaderText="Document" ItemStyle-Width="250px">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SW_Id" />
                                            <asp:BoundField DataField="site_id" />
                                            <asp:BoundField DataField="pono" />
                                            <asp:TemplateField ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="150px">
                                                        <asp:ListItem Selected="True" Value="0">
                                                    Approve
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="1">
                                                    Reject
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle" Width="98%"
                                                        Rows="3" TextMode="MultiLine" ToolTip="Please put remarks here.." Visible="false">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="margin-top: 5px;">
                                    <asp:Label ID="LblWarningMessageChildDoc" runat="server" ForeColor="Red" Font-Names="Verdana"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grddocuments" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSubmitSignReject" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:HiddenField ID="HDPath" runat="server" />
                        <asp:HiddenField ID="hdnx" runat="server" />
                        <asp:HiddenField ID="hdny" runat="server" />
                        <asp:HiddenField ID="hdpageNo" runat="server" />
                        <asp:HiddenField ID="HDPono" runat="server" />
                        <asp:HiddenField ID="HDDocid" runat="server" />
                        <asp:HiddenField ID="hdDocAliasName" runat="server" />
                    </td>
                    <td></td>
                </tr>

            </table>
        </div>
        <div style="margin-top: 5px;">
            <asp:Panel ID="pnlapproval" runat="server">
                <div style="border-style: solid; border-color: Gray; border-width: 1px; margin-left: 2px; margin-right: 2px; width: 450px; text-align: left;">
                    <div style="font-family: Verdana; font-size: 12px; font-weight: bold; background-color: #c3c3c3; padding: 3px; margin-bottom: 10px; text-align: center;">
                        Digital Signature Login
                    </div>
                    <asp:Panel ID="pnlDGSign" runat="server">
                        <div>
                            <div style="text-align: left; width: 450px;">
                                <asp:UpdateProgress ID="UpgPanelSign" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="blur">
                                            <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                                                <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                                    height="150" alt="Processing" />
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="UpPanelSign" runat="server">
                                    <ContentTemplate>
                                        <asp:MultiView ID="MvPanelSign" runat="server">
                                            <asp:View ID="defaultSign" runat="server">
                                                <table cellpadding="1" cellspacing="0" width="100%">
                                                    <tr valign="top" id="trpassfield" runat="server">
                                                        <td style="width: 25%">
                                                            <asp:Label ID="LblPassword" runat="server" Text="Password" CssClass="lblText"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="HdnUsername" runat="server" />
                                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="lblText"
                                                                Width="95%">
                                                            </asp:TextBox><span style="color: Red;">*</span>
                                                            <asp:Label ID="LblWarningSignMessage" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr valign="top">
                                                        <td colspan="2" style="width: 98%; text-align: right; padding-right: 10px; padding-top: 10px;">
                                                            <asp:Button ID="BtnSign" runat="server" Text="Sign" Width="80px" CssClass="btnstyle"
                                                                OnClientClick="return confirm('Are you sure you want to Approve this document?')" />
                                                            <asp:Button ID="BtnSignReject" runat="server" Text="Reject" CssClass="btnstylegray"
                                                                Width="80px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                            <asp:View ID="defaultSignReject" runat="server">
                                                <div>
                                                    <table>
                                                        <tr valign="top">
                                                            <td>
                                                                <div style="padding-top: 5px;">
                                                                    <span class="lblText">Reason </span>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList ID="CbReasonLists" runat="server" CssClass="lblText">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr valign="top">
                                                            <td>
                                                                <span class="lblText">Remarks </span>
                                                            </td>
                                                            <td>
                                                                <div style="padding-left: 8px;">
                                                                    <asp:TextBox ID="TxtRemarks_SignRejectPanel" runat="server" TextMode="MultiLine"
                                                                        Height="80px" CssClass="lblText" Width="380px"></asp:TextBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                </div>
                                                <div style="margin-top: 5px;">
                                                    <asp:Label ID="LblErrorSignReject" runat="server"></asp:Label>
                                                </div>
                                                <div style="text-align: right; width: 450px;">
                                                    <asp:Button ID="BtnSubmitSignReject" runat="server" Text="Submit" CssClass="btnstylegray"
                                                        OnClientClick="return CheckItemSignReject();" CausesValidation="false" />
                                                    <asp:Button ID="BtnCancelSignReject" runat="server" Text="Cancel" CssClass="btnstylegray"
                                                        CausesValidation="false" />
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BtnSignReject" />
                                        <asp:AsyncPostBackTrigger ControlID="BtnCancelSignReject" />
                                        <asp:AsyncPostBackTrigger ControlID="grddocuments" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                        </table>

                    </asp:Panel>
                    <asp:Panel ID="PnlRequestPass" runat="server">
                        <div>
                            <div style="width: 325px; margin-top: 10px; text-align: left;">
                                <asp:LinkButton ID="lnkrequest" OnClientClick="this.style.display = 'none'; loadingdiv.style.display = '';"
                                    runat="server">
                                                                    <div style="border-style:none; padding:2px; width:160px; vertical-align:middle;">
                                                                        <img src="../Images/reqPassword.png" alt="reqPassicon" style="border-style:none;" width="18" height="18" /> 
                                                                        <span style="font-family:Verdana; font-size:9pt; font-weight:bold; text-decoration:underline; color:Maroon;">
                                                                            Request Password
                                                                        </span>
                                                                    </div>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlreviewer" runat="server">
                <div>
                    <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
                        <ProgressTemplate>
                            <div id="blur">
                                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                                    <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                        height="150" alt="Processing" />
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="upPanelReview" runat="server">
                        <ContentTemplate>
                            <asp:MultiView ID="MvApprovalButtonPanel" runat="server">
                                <asp:View ID="vwDefaultPanel" runat="server">
                                    <div class="col-xs-2">
                                        <asp:Button ID="btnreview" runat="server" Text="Review" ValidationGroup="vgSign"
                                         CssClass="btn btn-block btn-success pull-left" OnClientClick="return confirm('Are you sure you want to Approve this document?')" />
                                    </div>
                                    <div class="col-xs-2">
                                        <asp:Button ID="BtnRejectReviewNew" runat="server" Text="Reject" CausesValidation="false"
                                         CssClass="btn btn-block btn-danger pull-left" />
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <asp:Label ID="LblWarningMessageReview" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                    </div>
                                </asp:View>
                                <asp:View ID="vwRejectPanel" runat="server">
                                    <div>
                                        <table>
                                            <tr valign="top">
                                                <td>
                                                    <div style="padding-top: 5px;">
                                                        <span class="lblText">Reason </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="CbList" runat="server" CssClass="lblText">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    <span class="lblText">Remarks </span>
                                                </td>
                                                <td>
                                                    <div style="padding-left: 8px;">
                                                        <asp:TextBox ID="TxtRemarksReject" runat="server" TextMode="MultiLine" Height="80px"
                                                            CssClass="lblText" Width="400px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <br />
                                            <div>
                                                <table id="tblFileUploadOtherDocument" runat="server" cellpadding="0" cellspacing="0" visible="true">
                                                    <tr>
                                                        <td>
                                                            <span class="lblText">File Upload</span>
                                                        </td>
                                                        <td>&nbsp;
                                    <asp:FileUpload ID="fuOtherDocument" runat="server" />
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="fuOtherDocument"
                                                                ValidationGroup="UploadRejectDocument" ErrorMessage="Please select a .pdf file only" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">
                                                            </asp:RegularExpressionValidator>
                                                            &nbsp;
                                                        </td>

                                                        <td>
                                                            <asp:LinkButton ID="lbtUploadRejectDocument" runat="server" CssClass="labelFieldText" Text="Upload" ValidationGroup="UploadRejectDocument" ForeColor="blue" OnClick="lbtUploadRejectDocument_Click"></asp:LinkButton>
                                                            &nbsp; &nbsp;
                                    <asp:LinkButton ID="lbtCancelRejectDocument" runat="server" CssClass="labelFieldText" Text="Cancel" ForeColor="blue"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <asp:Label ID="LblErrorReviewReject" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                    </div>
                                    <div style="text-align: right; width: 470px;">
                                        <div class="col-xs-4">
                                            <asp:Button ID="BtnSubmitReject" runat="server" Text="Submit" CssClass="btn btn-block btn-info pull-left"
                                            OnClientClick="return CheckItem()" CausesValidation="false" Width="120px" />
                                        </div>
                                        <asp:Label ID="LblWPID" runat="server" Visible="false"></asp:Label>
                                        <div class="col-xs-2">
                                            <asp:Button ID="BtnCancelSubmit" runat="server" Text="Cancel" CssClass="btn btn-block btn-danger pull-left"
                                            CausesValidation="false" Width="120px" />
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnRejectReviewNew" />
                            <asp:AsyncPostBackTrigger ControlID="BtnCancelSubmit" />
                            <asp:PostBackTrigger ControlID="lbtUploadRejectDocument" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HdnGuid" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <asp:Label ID="lblSiteID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPONumber" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblWorkPackageID" runat="server" Visible="false"></asp:Label>
    
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
</body>
</html>
