<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Digital-signature.aspx.vb"
    Inherits="Digital_signature" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Approval/Reviewal</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function gopage(id) {
            var url = '../BAUT/frmTI_WCTRBAST.aspx?id=' + id + '&from=bast';
            window.location = url;
        }

        function WindowsCloseApprover() {
            alert('Signed Sucessfully');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0';
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

        function WindowATPOnSiteClose() {
            alert('This Site has been registered as ATP On Site Now');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid + '&TId=0&doctype=atp';
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
                    alert("Please describe in detail your reason of rejection");
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
            function CheckRemarksApprove() {
                var remarks = document.getElementById("<%=TxtApproveWithRemarks.ClientID %>");
                if (remarks.value.length < 10) {
                    alert("Please describe in detail your remarks");
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
                                <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                            </td>
                            <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%; text-align: center;">
                                <b>ATP Approval Sheet</b>
                                <br />
                            </td>
                            <td align="right" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" alt="logotsel" />
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
                            <td class="lblText" colspan="3">This approval sheet declares that the Acceptance Test Procedure (ATP) has been performed
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
                            <td colspan="3" class="Hcap">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="lblText">And the work has been considered as complete and satisfactory fulfil the standard
                                technical requirements in Telkomsel project.
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
                <td colspan="3" class="lblText">This approval sheet is automatically generated by eBAST upon acceptance of all person
                    in charge and no more signatures on hard copy is required. This sheet is inseparable
                    from the ATP report and should be the first thing seen when opening the report.
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" scrolling="auto"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblText" colspan="4">
                        <div id="divReviewer2" runat="server" style="width: 100%; text-align: left;">
                        </div>
                    </td>
                </tr>
                <tr id="listdocuments" runat="server">
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
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
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="HDPath" runat="server" />
                        <asp:HiddenField ID="hdnx" runat="server" />
                        <asp:HiddenField ID="hdny" runat="server" />
                        <asp:HiddenField ID="hdpageNo" runat="server" />
                        <asp:HiddenField ID="HDPono" runat="server" />
                        <asp:HiddenField ID="HDDocid" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments2" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id" OnRowDataBound="grddocuments2_RowDataBound">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnotwo" runat="Server">
                                        </asp:Label>
                                        <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 972px" id="dgrow" runat="server">
                        <div style="text-align: center; margin-right: 5px;">
                            <div style="border-style: solid; border-color: Gray; border-width: 1px; margin-left: 2px; margin-right: 2px; width: 350px; text-align: left;">
                                <div style="font-family: Verdana; font-size: 12px; font-weight: bold; background-color: #c3c3c3; padding: 3px; margin-bottom: 20px; text-align: center;">
                                    Digital Signature Login
                                </div>
                                <div>
                                    <table cellpadding="1" cellspacing="1">
                                        <tr valign="top">
                                            <td>
                                                <span style="font-family: Verdana; font-size: 8.5pt;">Username</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="txtField">
                                                </asp:TextBox><span style="color: Red;">*</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                    ErrorMessage="Enter User Name" ValidationGroup="vgSign">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                <span style="font-family: Verdana; font-size: 8.5pt;">Password</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txtField">
                                                </asp:TextBox><span style="color: Red;">*</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                    ErrorMessage="Enter Password" ValidationGroup="vgSign">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right; width: 325px;">
                                        <asp:Label ID="LblTestSign" runat="server"></asp:Label>
                                        <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="80px"
                                            CssClass="buttonStyle" OnClientClick="this.style.display = 'none'; dgdiv.style.display = '';" />&nbsp;<asp:Button
                                                ID="btnReject" runat="server" CssClass="buttonStyle" Text="Reject" Visible="False" /><asp:ValidationSummary
                                                    ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="vgSign" />
                                    </div>
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
                                <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;">
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="rerow" runat="server">
                    <td>
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
                                        <asp:Button ID="btnApproveWithRemarks" runat="server" CausesValidation="false" Text="Approve With Remarks"
                                            CssClass="buttonStyle" Width="180px" />
                                        <asp:Button ID="btnreview" runat="server" Text="Review" ValidationGroup="vgSign"
                                            Width="120px" CssClass="buttonStyle" />
                                        <asp:Button ID="BtnATPOnSite" runat="server" CausesValidation="false" Text="ATP On Site"
                                            Width="150px" CssClass="buttonStyle" OnClientClick="return confirm('Are you sure you want to ATP on Site?')" />
                                        <asp:Button ID="BtnRejectReviewNew" runat="server" Text="Reject" CausesValidation="false"
                                            Width="120px" CssClass="buttonStyle" />
                                        <asp:Button ID="btnrejectreview" Visible="false" runat="server" CssClass="buttonStyle"
                                            Text="Reject" />
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
                                                                CssClass="textFieldStyle" Width="400px"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="text-align: right; width: 460px;">
                                            <asp:Button ID="BtnSubmitReject" runat="server" Text="Submit" CssClass="buttonStyle"
                                                OnClientClick="return CheckItem()" CausesValidation="false" />
                                            <asp:Label ID="LblWPID" runat="server" Visible="false"></asp:Label>
                                            <asp:Button ID="BtnCancelSubmit" runat="server" Text="Cancel" CssClass="buttonStyle"
                                                CausesValidation="false" />
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwApproveWithRemarks" runat="server">
                                        <div>
                                            <table>
                                                <tr valign="top">
                                                    <td>
                                                        <span class="lblText">Remarks</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtApproveWithRemarks" runat="server" Width="400px" Height="80px"
                                                            TextMode="MultiLine" CssClass="textFieldStyle"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="text-align: right; width: 460px;">
                                            <asp:Button ID="BtnSubmitApproveWithRemarks" runat="server" Text="Submit" CssClass="buttonStyle"
                                                OnClientClick="return CheckRemarksApprove()" CausesValidation="false" />
                                            <asp:Button ID="BtnCancelApproveWithRemarks" runat="server" Text="Cancel" CssClass="buttonStyle"
                                                CausesValidation="false" />
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnRejectReviewNew" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelSubmit" />
                                <asp:AsyncPostBackTrigger ControlID="btnApproveWithRemarks" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelApproveWithRemarks" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="loadingdiv" runat="server" style="display: none; position: absolute; width: 95%; text-align: right; top: 91%; left: 18px;">
            <img src="../sendsms.GIF" runat="server" id="loading" />
        </div>
        <div id="dgdiv" runat="server" style="display: none; position: absolute; width: 95%; text-align: right; top: 100%; left: 18px;">
            <img src="../digital-signature.gif" runat="server" id="dgloading" />
        </div>
    </form>
</body>
</html>
