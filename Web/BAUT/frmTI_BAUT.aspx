<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_BAUT.aspx.vb" Inherits="BAUT_frmTI_BAUTNEW"
    EnableEventValidation="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BAUT</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

    <script language="javascript" type="text/javascript">
            function WindowsClose(){
                window.opener.location.href = '../dashboard/DashboardpopupBaut.aspx?time=' + (new Date()).getTime();
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function getControlPosition(){
                var Total = document.getElementById('HDDgSignTotal').value;
                for (intCount1 = 0; intCount1 < Total; intCount1++) {
                    var divctrl = document.getElementById('DLDigitalSign_ctl0' + intCount1 + '_ImgPostion');
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                    // alert(findPosX(divctrl) + " , " + findPosY(divctrl));
                    divctrl = document.getElementById('dlWCTR_ctl0' + intCount1 + '_ImgPostion');
                    eval("document.getElementById('dlWCTR_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                    eval("document.getElementById('dlWCTR_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                    // alert(findPosX(divctrl) + " , " + findPosY(divctrl));
                
                }
            }
            
            function findPosX(imgElem){
                xPos = eval(imgElem).offsetLeft;
                tempEl = eval(imgElem).offsetParent;
                while (tempEl != null) {
                    xPos += tempEl.offsetLeft;
                    tempEl = tempEl.offsetParent;
                }
                return xPos;
            }
            
            function findPosY(imgElem){
                yPos = eval(imgElem).offsetTop;
                tempEl = eval(imgElem).offsetParent;
                while (tempEl != null) {
                    yPos += tempEl.offsetTop;
                    tempEl = tempEl.offsetParent;
                }
                return yPos;
            }
            
            function Showmain(type){
                if (type == 'Int') {
                    alert('Integration date not available');
                }
                if (type == 'IntD') {
                    alert('The document cannot be uploaded before the integration date');
                }
                if (type == '2sta') {
                    alert('This Document already processed for second stage so cannot upload again ');
                }
                if (type == 'nop') {
                    alert('No permission to upload this Document ');
                }
                if(type == 'nscope'){
                    alert('Master Scope not defined, please go to Management PO List to define Master Scope');
                }
                if(type == 'nwork'){
                    alert('TypeofWork not defined, please go to Management PO List to define TypeofWork');
                }
                if(type == 'napodate'){
                    alert('PO Date Not yet available');
                }
                if(type == 'naprojectid'){
                    alert('Project ID is Empty, Please fill Project ID Field in PO Management');
                }
                window.location = '../PO/frmSiteDocUploadTree.aspx';
            }
            
            function TotalD(){
                //actualDurationBaut();
                //document.getElementById('txtActualExecBaut').value=datediff;
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut1").value) == false) {
                    // msg = msg+ "The reason of delay 1 days  should not be Empty.\n";
                    document.getElementById("txtReasonDaysBaut1").value = 0;
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut2").value) == false) {
                    //msg = msg+ "The reason of delay 2 days should not be Empty.\n";
                    document.getElementById("txtReasonDaysBaut2").value = 0;
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut3").value) == false) {
                    // msg = msg+ "The reason of delay 3 days on should not be Empty.\n";
                    document.getElementById("txtReasonDaysBaut3").value = 0;
                }
                if (msg != "") {
                    alert("Mandatory field information required \n\n" + msg);
                    return false;
                }
                else {
                    document.getElementById('txtTotalC').value = (document.getElementById('txtReasonDaysBaut1').value * 1) + (document.getElementById('txtReasonDaysBaut2').value * 1) + (document.getElementById('txtReasonDaysBaut3').value * 1)
                }
            }
            
            function actualDurationBaut(){
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtDurationExecBaut").value) == false) {
                    msg = msg + "Duration of execution of work  should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtKPIAccepted").value) == false) {
                    msg = msg + "Work is started on date should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtWorkShouldFinishedbaut").value) == false) {
                    msg = msg + "Work should be finished date on should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtWorkHasBeenFinishedBaut").value) == false) {
                    msg = msg + " Work has been finished on date should not be Empty.\n";
                }
                if (msg != "") {
                    alert("Mandatory field information required \n\n" + msg);
                    return false;
                }
                else {
                    var strDate1 = document.getElementById('txtWorkHasBeenFinishedBaut').value;
                    var strDate2 = document.getElementById('txtKPIAccepted').value;
                    var strDate3 = document.getElementById('txtWorkShouldFinishedbaut').value;
                    var dt1 = new Date(Date.parse(strDate1.replace('-', ' ')));
                    var dt2 = new Date(Date.parse(strDate2.replace('-', ' ')));
                    var dt3 = new Date(Date.parse(strDate3.replace('-', ' ')));
                    datediff = ((dt1 - dt2) / (24 * 60 * 60 * 1000));
                    document.getElementById('txtActualExecBaut').value = datediff;
                    if (datediff < 0) {
                        document.getElementById('txtReasonBaut1').readOnly = false;
                        document.getElementById('txtReasonBaut2').readOnly = false;
                        document.getElementById('txtReasonBaut3').readOnly = false;
                        document.getElementById('txtReasonDaysBaut1').readOnly = false;
                        document.getElementById('txtReasonDaysBaut2').readOnly = false;
                        document.getElementById('txtReasonDaysBaut3').readOnly = false;
                        document.getElementById('txtTotalD').readOnly = false;
                        document.getElementById('lblDelayBaut').innerHTML = "Delay (C)";
                    }
                    else {
                        document.getElementById('txtReasonBaut1').value = '';
                        document.getElementById('txtReasonBaut1').readOnly = true;
                        document.getElementById('txtReasonBaut2').value = '';
                        document.getElementById('txtReasonBaut2').readOnly = true;
                        document.getElementById('txtReasonBaut3').value = '';
                        document.getElementById('txtReasonBaut3').readOnly = true;
                        document.getElementById('txtReasonDaysBaut1').value = 0;
                        document.getElementById('txtReasonDaysBaut1').readOnly = true;
                        document.getElementById('txtReasonDaysBaut2').value = 0;
                        document.getElementById('txtReasonDaysBaut2').readOnly = true;
                        document.getElementById('txtReasonDaysBaut3').value = 0;
                        document.getElementById('txtReasonDaysBaut3').readOnly = true;
                        document.getElementById('txtTotalD').value = 0;
                        document.getElementById('txtTotalD').readOnly = true;
                        document.getElementById('lblDelayBaut').innerHTML = "Acceleration (C)";
                    }
                    if (dt1 > dt3) {
                        document.getElementById('txtTotalC').value = (document.getElementById('txtActualExecBaut').value * 1) - (document.getElementById('txtDurationExecBaut').value * 1);
                    }
                    else {
                        document.getElementById('txtTotalC').value = (document.getElementById('txtDurationExecBaut').value * 1) - (document.getElementById('txtActualExecBaut').value * 1);
                    }
                }
            }
    </script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js">
    </script>

    <style type="text/css">
            tr {
                padding: 3px;
            }
            
            .PageBreak {
                page-break-before: always;
            }
            
            .MainCSS {
                margin-bottom: 0px;
                margin-left: 20px;
                margin-right: 20px;
                margin-top: 0px;
                width: 800px;
                height: 700px;
                text-align: center;
            }
            
            .lblText {
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
            }
            
            .lblBText {
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: bold;
            }
            
            .lblBold {
                font-family: verdana;
                font-size: 12pt;
                color: #000000;
                font-weight: bold;
            }
            
            .textFieldStyle {
                background-color: white;
                border: 1px solid;
                color: black;
                font-family: verdana;
                font-size: 9pt;
            }
            
            .GridHeader {
                color: #0e1b42;
                background-color: Orange;
                font-size: 9pt;
                font-family: Verdana;
                text-align: Left;
                vertical-align: bottom;
                font-weight: bold;
            }
            
            .GridEvenRows {
                background-color: #e3e3e3;
                vertical-align: top;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
            }
            
            .GridOddRows {
                background-color: white;
                vertical-align: top;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
            }
            
            .PagerTitle {
                font-size: 8pt;
                background-color: #cddbbf;
                text-align: Right;
                vertical-align: middle;
                color: #25375b;
                font-weight: bold;
            }
            
            .Hcap {
                height: 5px;
            }
            
            .VCap {
                width: 10px;
            }
        </style>
</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="HDDgSignTotal" />
        <input type="hidden" runat="server" id="hdnDGBox" />
        <input type="hidden" runat="server" id="hdnready4baut" />
        <input type="hidden" runat="server" id="hdnKeyVal" />
        <input type="hidden" runat="server" id="hdnScope" />
        <input type="hidden" runat="server" id="hdnMasterScope" />
        <input type="hidden" runat="server" id="hdnTypeofWork" />
        <input runat="Server" id="hdndocid" type="hidden" />
        <input runat="Server" id="hdnWfId" type="hidden" />
        <input runat="Server" id="hdnversion" type="hidden" />
        <input runat="Server" id="hdnsiteid" type="hidden" />
        <input runat="Server" id="hdnAdminRole" type="hidden" />
        <input runat="Server" id="hdnAdmin" type="hidden" />
        <input runat="Server" id="hdnSiteno" type="hidden" />
        <input runat="Server" id="hdnPono" type="hidden" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div id="DivPrintWCTR" runat="server" style="width: 100%;">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                                </td>
                                <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                    WORK COMPLETION TIME REPORT (WCTR)
                                    <br />
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" />
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
                                <td class="lblText" style="width: 206px;">
                                    Work
                                </td>
                                <td style="width: 1%;">
                                    :
                                </td>
                                <td class="lblText" style="width: 1101px">
                                    <asp:Label ID="lblScope" runat="server" CssClass="lblBText">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">
                                    Project
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td class="lblText" style="width: 1101px">
                                    Telkomsel 2G BSS &amp; 3G UTRAN (TINEM 3)
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">
                                    Site ID / Site Name
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td class="lblTextSmall" style="width: 1101px; text-align: left;">
                                    <asp:Label ID="lblSiteWCTR" CssClass="lblTextSmall" runat="server">
                                    </asp:Label>
                                    &nbsp; SiteID(PO):
                                    <asp:Label ID="lblsiteidpo" runat="server" CssClass="lblTextSmall"></asp:Label><asp:TextBox ID="txtsiteidpo" runat="server" CssClass="textFieldStyle" Width="146px">
                                    </asp:TextBox>
                                    Site Name(PO):
                                    <asp:Label ID="lblsitenamepo" runat="server" CssClass="lblTextSmall"></asp:Label><asp:TextBox ID="txtsitenamepo" runat="server" CssClass="textFieldStyle" Width="184px">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">
                                    PO Number
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="lblText" style="width: 1101px">
                                    <asp:TextBox ID="txtPoWCTR" runat="server" CssClass="textFieldStyle" Width="316px"></asp:TextBox>
                                    <asp:Label ID="lblPoWCTR" runat="server" CssClass="lblBText">
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td class="lblText" style="height: 21px; width: 206px;">
                                    Project ID
                                </td>
                                <td style="height: 21px">
                                    :
                                </td>
                                <td class="lblText" style="height: 21px; width: 1101px;">
                                    <asp:Label ID="lblProjectidwctr" runat="server" Font-Bold="True">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="lblText">
                                    On this Day<input id="txtDay" type="text" runat="Server" class="textFieldStyle" style="background-color: gray"
                                        readonly="readOnly" /><asp:Label ID="lblDay" runat="server" CssClass="lblBText">
                                        </asp:Label>, Date<input id="txtDateWCTR" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                        style="background-color: gray" readonly="readOnly" /><asp:Label ID="lblDateWCTR"
                                            runat="server" CssClass="lblBText">
                                        </asp:Label>, Month<input id="txtMonth" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                        style="background-color: gray" readonly="readOnly" /><asp:Label ID="lblMonth" runat="server"
                                            CssClass="lblBText">
                                        </asp:Label>, Year<input id="txtYear" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                        style="background-color: gray" readonly="readOnly" /><asp:Label ID="lblYear" runat="server"
                                            CssClass="lblBText">
                                        </asp:Label>
                                    , the work at project location mentioned above has been successfully completed.
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="lblText" colspan="3">
                                                <strong>The result is as follows: (based on BAST Submission)</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%" class="lblText">
                                                Duration of execution of work based on the Purchase Order
                                            </td>
                                            <td style="width: 1%">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtDurationExec" type="text" runat="Server" class="textFieldStyle" value="0" />
                                                <asp:Label ID="lblDurationExec" runat="server">
                                                </asp:Label>
                                                &nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; - Work is started on (based on Purchase Order)
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtWorkStarted" type="text" runat="Server" class="textFieldStyle" />
                                                <asp:ImageButton ID="imgWrkStarted" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label ID="lblWrkStarted" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 23px;">
                                                &nbsp; - Work should be finished on
                                            </td>
                                            <td style="height: 23px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 23px; width: 353px;">
                                                <input id="txtWorkShouldFinished" type="text" runat="Server" class="textFieldStyle" />
                                                <asp:ImageButton ID="imgWorkShouldFinished" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label ID="lblWorkShouldFinished" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; - Work has been finished on
                                            </td>
                                            <td valign="Top">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtWorkHasBeenFinished" type="text" runat="Server" class="textFieldStyle"
                                                    onblur="actualDuration()" style="background-color: gray" readonly="readOnly" />
                                                <asp:ImageButton ID="imgWorkhasbeenFinished" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" Visible="False" />
                                                <asp:Label ID="lblWorkhasbeenFinished" runat="server">
                                                </asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 22px">
                                                The actual duration of execution of work
                                            </td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px; width: 353px;">
                                                <input id="txtActualExec" type="text" runat="Server" class="textFieldStyle" readonly="readOnly"
                                                    value="0" />
                                                <asp:Label ID="lblActualExec" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp;Delay / Acceleration ( A )
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtTotalA" type="text" runat="Server" class="textFieldStyle" value="0" />
                                                <asp:Label ID="lblTotalA" runat="server" Font-Bold="True">
                                                </asp:Label>&nbsp;calendar days (*)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                <b>The reason of delay in the execution of work are : </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 1.<input id="txtReason1" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px; background-color: gray;" readonly="readOnly" />
                                                <asp:Label ID="lblReason1" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtReasonDays1" type="text" runat="Server" class="textFieldStyle" onclick="return txtReasonDays1_onclick()"
                                                    value="0" style="background-color: gray" readonly="readOnly" />
                                                <asp:Label ID="lblReasonDays1" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 2.<input id="txtReason2" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px; background-color: gray;" readonly="readOnly" />
                                                <asp:Label ID="lblReason2" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtReasonDays2" type="text" runat="Server" class="textFieldStyle" value="0"
                                                    style="background-color: gray" readonly="readOnly" />
                                                <asp:Label ID="lblReasonDays2" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 22px;">
                                                &nbsp; 3.<input id="txtReason3" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px; background-color: gray;" readonly="readOnly" />
                                                <asp:Label ID="lblReason3" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px; width: 353px;">
                                                <input id="txtReasonDays3" type="text" runat="Server" class="textFieldStyle" value="0"
                                                    style="background-color: gray" readonly="readOnly" />
                                                <asp:Label ID="lblReasonDays3" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp; Total ( B )
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtTotalB" type="text" runat="Server" class="textFieldStyle" value="0"
                                                    style="background-color: gray" readonly="readOnly" />
                                                <asp:Label ID="lblTotalB" runat="server" Font-Bold="True">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" colspan="3">
                                                <strong>The result is as follows: (based on project implementation)</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; - Work has been finished on (BAUT)
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtWorkShouldFinishedbaut" type="text" runat="Server" class="textFieldStyle"
                                                    style="background-color: gray" readonly="readOnly" />
                                                <asp:ImageButton ID="imgWorkShouldFinishedbaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" Visible="False" />
                                                <asp:Label ID="lblWorkShouldFinishedbaut" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp; - KPI accepted
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtKPIAccepted" type="text" runat="Server" class="textFieldStyle" />
                                                <asp:ImageButton ID="imgKPIAccepted" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label ID="lblKPIAccepted" runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                <b>The reason of delay in the execution of work are : </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 1.<input id="txtReasonBaut1" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px" />
                                                <asp:Label ID="lblReasonBaut1" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtReasonDaysBaut1" type="text" runat="Server" class="textFieldStyle"
                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onchange="TotalD();"
                                                    value="0" />
                                                <asp:Label ID="lblReasonDaysBaut1" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 2.<input id="txtReasonBaut2" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px" />
                                                <asp:Label ID="lblReasonBaut2" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtReasonDaysBaut2" type="text" runat="Server" class="textFieldStyle"
                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onchange="TotalD();"
                                                    value="0" />
                                                <asp:Label ID="lblReasonDaysBaut2" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 22px;">
                                                &nbsp; 3.<input id="txtReasonBaut3" type="text" runat="Server" class="textFieldStyle"
                                                    style="width: 366px" />
                                                <asp:Label ID="lblReasonBaut3" runat="server">
                                                </asp:Label>
                                            </td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px; width: 353px;">
                                                <input id="txtReasonDaysBaut3" type="text" runat="Server" class="textFieldStyle"
                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onchange="TotalD();"
                                                    value="0" />
                                                <asp:Label ID="lblReasonDaysBaut3" runat="server">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp; Total ( C )
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtTotalC" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                                    value="0" />
                                                <asp:Label ID="lblTotalC" runat="server" Font-Bold="True">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3" style="height: 5px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" colspan="3" style="height: 15px">
                                                Total period of work completion has been delayed / accelerated :
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp;Max (A – (B + C))
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 353px">
                                                <input id="txtJobDelay" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                                    value="0" />
                                                <asp:Label ID="lblJobDelay" runat="server" Font-Bold="True">
                                                </asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="3">
                                    This work completion time report is made truthfully and released to serve its appropriate
                                    purpose.
                                </td>
                            </tr>
                            <tr>
                                <td class="Hcap" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left">
                                    <asp:DataList ID="dlWCTR" runat="server" Width="100%" RepeatColumns="3">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                                <tr>
                                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                        border-style: solid; vertical-align: top; text-align: center;">
                                                        <%#Container.DataItem("Description") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="DgSign" runat="server" style="height: 100px;">
                                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" /><asp:HiddenField
                                                            runat="Server" ID="hdXCoordinate" />
                                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                        border-style: solid; vertical-align: top; text-align: center;">
                                                        <%#Container.DataItem("name") %>
                                                        <br />
                                                        <%#Container.DataItem("roledesc") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="PageBreak">
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvPrint" runat="server" style="width: 100%; text-align: center;">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                                </td>
                                <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                    <asp:Label ID="LblHeaderBAUT" runat="server" Text="BERITA ACARA UJI TERIMA (BAUT)" CssClass="lblBold"></asp:Label>
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="3" class="Hcap">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="text-align: right">
                                    <asp:Label ID="LblBautRefNumber" runat="server" Text="BAUT Ref Number" CssClass="lblText"></asp:Label>
                                </td>
                                <td valign="Top">
                                    :
                                </td>
                                <td class="lblText" style="width: 894px">
                                    <input id="txtBautNo" runat="Server" type="text" class="textFieldStyle" style="width: 294px" />
                                    <asp:Label ID="lblBautNo" runat="server" CssClass="lblText" Font-Bold="True" Width="332px">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%" class="lblText">
                                    Type of Works
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td class="lblText" style="width: 894px">
                                    <asp:Label ID="lblScope2" runat="server" CssClass="lblBText">
                                    </asp:Label>, Site Configuration:<input id="txtConfig" runat="Server" type="text"
                                        class="textFieldStyle" style="width: 294px" />
                                    <asp:Label ID="lblConfig" runat="server" CssClass="lblText" Font-Bold="True">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText">
                                    Site ID/Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="lblText" id="txtBsiteidpo" style="width: 894px">
                                    <asp:Label ID="lblSite" runat="server" Font-Bold="True">
                                    </asp:Label>&nbsp; SiteID(PO):
                                    <asp:Label ID="lblBsiteidpo" runat="server" Font-Bold="True">
                                    </asp:Label>
                                    &nbsp;Sitename(PO):
                                    <asp:Label ID="lblBsitenamepo" runat="server" Font-Bold="True">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr visible="false">
                                <td class="lblText">
                                    Purchase Order
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="lblText" style="width: 894px">
                                    <asp:TextBox ID="txtPO" runat="server" CssClass="textFieldStyle" Width="316px"></asp:TextBox>
                                    <asp:Label ID="lblPO" runat="server" Font-Bold="True">
                                    </asp:Label></td>
                            </tr>
                            <tr>
                                <td class="lblText">
                                    Project ID
                                </td>
                                <td>
                                    :
                                </td>
                                <td class="lblText" style="width: 894px">
                                    <asp:Label ID="lblProjectId" runat="server" Font-Bold="True">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Hcap" colspan="3">
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td class="lblText">
                                    On this date
                                </td>
                                <td valign="Top">
                                    :
                                </td>
                                <td class="lblText" style="width: 894px">
                                    <input id="txtDate" runat="Server" type="text" class="textFieldStyle" readonly="readOnly" />
                                    <asp:ImageButton ID="imgDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                        Width="18px" />
                                    <asp:Label ID="lblDate" runat="server" CssClass="lblText" Font-Bold="True">
                                    </asp:Label>
                                    , we the undersigned:
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:DataList ID="dtList" Width="100%" runat="server">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td class="lblText" style="width: 25%">
                                                        Name
                                                    </td>
                                                    <td style="width: 1%">
                                                        :
                                                    </td>
                                                    <td style="width: 74%" class="lblBText">
                                                        <%#Container.DataItem("name") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lblText" style="width: 25%">
                                                        Title
                                                    </td>
                                                    <td style="width: 1%">
                                                        :
                                                    </td>
                                                    <td style="width: 74%" class="lblBText">
                                                        <%#Container.DataItem("roledesc") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lblText" colspan="3">
                                                        <%#Container.DataItem("NewDescription") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" />
                                    </asp:DataList>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="lblText" style="width: 25%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                By virtue of
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                a. Purchase Order Ref No.
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <asp:TextBox ID="txtPONo" runat="server" CssClass="textFieldStyle" Width="316px"></asp:TextBox>
                                                <asp:Label ID="lblPONo" runat="server" Font-Bold="True" CssClass="lblText">
                                                </asp:Label>&nbsp; dated<input id="txtPODated" runat="Server" class="textFieldStyle"
                                                    type="text" />&nbsp;
                                                <asp:Label ID="lblPODate" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                <asp:ImageButton ID="imgPODate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                b. ATP dated
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtATPDated" runat="Server" type="text" class="textFieldStyle" />
                                                <asp:Label ID="lblATPDated" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                dated<input id="txtATPDate" runat="Server" type="text" class="textFieldStyle" />
                                                <asp:Label ID="lblATPDate" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                <asp:ImageButton ID="ImgATPDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                c. On Air dated
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtOADate" runat="Server" type="text" class="textFieldStyle" />
                                                <asp:Label ID="lblOADate" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                dated<input id="txtOADated" runat="Server" type="text" class="textFieldStyle" />
                                                <asp:Label ID="lblOADated" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                <asp:ImageButton ID="imgOADated" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                d. KPI Achievement Date
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtkpidate" runat="Server" type="text" class="textFieldStyle" />
                                                <asp:Label ID="lblkpidate" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                                <asp:ImageButton ID="imgkpidate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                Telkomsel and Vendor hereby state the followings :
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                1&nbsp;&nbsp;Vendor has completed the works with the detail result as attached
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                2&nbsp;&nbsp;Telkomsel stated that the works qualification is GOOD. Therefore, Telkomsel
                                                has accept the works accordingly
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText" style="height: 19px">
                                                3&nbsp;&nbsp;There is/there is no minor pending item(s) of the works as refer to
                                                the Purchase Order and its changes if applicable
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                &nbsp;&nbsp;&nbsp;&nbsp;The detail of pending item(s) as follows :
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                &nbsp;&nbsp;&nbsp;&nbsp;a.&nbsp;&nbsp;
                                                <textarea id="txtd1" class="textFieldStyle" cols="100" rows="1" runat="server">
                                                    </textarea>&nbsp;
                                                <asp:Label runat="server" CssClass="lblText" ID="lbld1">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                &nbsp;&nbsp;&nbsp;&nbsp;b.&nbsp;&nbsp;
                                                <textarea id="txtd2" class="textFieldStyle" cols="100" rows="1" runat="server">
                                                    </textarea>&nbsp;
                                                <asp:Label runat="server" CssClass="lblText" ID="lbld2">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                &nbsp;&nbsp;&nbsp;&nbsp;c.&nbsp;&nbsp;
                                                <textarea id="txtd3" class="textFieldStyle" cols="100" rows="1" runat="server">
                                                    </textarea>&nbsp;
                                                <asp:Label runat="server" CssClass="lblText" ID="lbld3">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                4&nbsp;&nbsp;Vendor hereby state and committed that the pending item(s) mentioned
                                                above will be completed on &nbsp;<input id="txtd4" runat="Server" type="text" class="textFieldStyle"
                                                    readonly="readOnly" /><asp:ImageButton ID="imgd4" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                        Width="18px" />
                                                <asp:Label ID="lbld4" runat="server" CssClass="lblText" Font-Bold="True">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="lblText">
                                                Telkomsel and Vendor has done Acceptance Test with following result :
                                                <br />
                                                <br />
                                                <i>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; The Vendor has been 100% succesfully
                                                    fulfill the obligation as mentioned in the Purchase Order</i>
                                                <br />
                                                <i>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; and Telkomsel accepted the work as
                                                    mentioned in the Purchase Order</i>
                                                <br />
                                                <br />
                                                This certificate is made in three(3) original copies which shall have the same legal
                                                powers after being signed
                                                <br />
                                                by their respective duly representatives.
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="left">
                                                <asp:DataList ID="DLDigitalSign" runat="server" Width="100%" RepeatColumns="3">
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                                            <tr>
                                                                <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                                    border-style: solid; vertical-align: top; text-align: center;">
                                                                    <%#Container.DataItem("Description") %>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="DgSign" runat="server" style="height: 100px;">
                                                                    <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" /><asp:HiddenField
                                                                        runat="Server" ID="hdXCoordinate" />
                                                                    <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                                    border-style: solid; vertical-align: top; text-align: center;">
                                                                    <%#Container.DataItem("name") %>
                                                                    <br />
                                                                    <%#Container.DataItem("roledesc") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center" class="lblText">
                                <asp:GridView ID="grddocuments" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_Id"
                                    EmptyDataText="No Records Found" PageSize="2" Width="100%">
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
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
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
