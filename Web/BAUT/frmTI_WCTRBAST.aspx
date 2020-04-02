<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_WCTRBAST.aspx.vb"
Inherits="frmTI_WCTRBAST" EnableEventValidation="False" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>WCTR BAST</title>
        <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript">
            function WindowsClosenew(){
                window.opener.location.href = '../dashboard/DashboardpopupWCTRBast.aspx?time=' + (new Date()).getTime();
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function getControlPostion(){
                var Total = document.getElementById('HDDgSignTotal').value;
                for (intCount1 = 0; intCount1 < Total; intCount1++) {
                    var divctrl = document.getElementById('DLDigitalSign_ctl0' + intCount1 + '_ImgPostion');
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdXCoordinate')").value = findPosX(divctrl);
                    eval("document.getElementById('DLDigitalSign_ctl0" + intCount1 + "_hdYCoordinate')").value = findPosY(divctrl);
                    //alert(findPosX(divctrl) + " , " + findPosY(divctrl));
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
                    alert('Integration date not vailable');
                }
                if (type == 'IntD') {
                    alert('The document cannot be uploaded before the integration date');
                }
                if (type == '2sta') {
                    alert('This Document already processed for second stage so cannot upload again ');
                }
                if (type == 'Lnk') {
                    alert('Not Allowed to upload this Document Now.');
                }
            }
            
            function CalculateTotal(){
                document.getElementById('txtJobDelay').value = ((document.getElementById('txtTotalA').value * 1) + ((document.getElementById('txtTotalB').value * 1) + (document.getElementById('txtTotalC').value * 1)));
                if (document.getElementById('txtJobDelay').value >= 0) {
                    document.getElementById('tdaccelerate').innerHTML = "Total period of work completion has been <b>accelerated</b> :";
                }
                else {
                    document.getElementById('tdaccelerate').innerHTML = "Total period of work completion has been <b>delayed</b> :";
                }
            }
            
            function actualDurationBaut(){
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtWorkStarted").value) == false) {
                    msg = msg + "Work is started on date should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtWorkShouldFinished").value) == false) {
                    msg = msg + "Work should be finished date on should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtWorkHasBeenFinished").value) == false) {
                    msg = msg + " Work has been finished on date should not be Empty.\n";
                }
                if (msg != "") {
                    alert("Mandatory field information required \n\n" + msg);
                    document.getElementById("btnGenerate").disabled = true;
                    return false;
                }
                else {
                    document.getElementById("btnGenerate").disabled = false;
                    var strDate1 = document.getElementById('txtWorkHasBeenFinished').value;
                    var strDate2 = document.getElementById('txtWorkStarted').value;
                    var strDate3 = document.getElementById('txtWorkShouldFinished').value;
                    var dt1 = new Date(Date.parse(strDate1.replace('-', ' ')));
                    var dt2 = new Date(Date.parse(strDate2.replace('-', ' ')));
                    var dt3 = new Date(Date.parse(strDate3.replace('-', ' ')));
                    datediff = ((dt1 - dt2) / (24 * 60 * 60 * 1000));
                    document.getElementById('txtActualExec').value = datediff;
                    var var16 = (document.getElementById('txtDurationExec').value * 1);
                    if (datediff < var16) {
                        document.getElementById('txtTotalA').value = var16 - datediff;
                    }
                    else {
                        datediff = ((dt1 - dt3) / (24 * 60 * 60 * 1000));
                        if (datediff > 0) {
                            document.getElementById('txtTotalA').value = var16 - (document.getElementById('txtActualExec').value * 1);
                        }
                        else {
                            document.getElementById('txtTotalA').value = (document.getElementById('txtActualExec').value * 1) - var16;
                        }
                    }
                    if ((document.getElementById('txtTotalA').value * 1) < 0) {
                        document.getElementById('lblDelay').innerHTML = "Delay (A)";
                    }
                    else {
                        document.getElementById('lblDelay').innerHTML = "Acceleration (A)";
                    }
                }
            }
            
            function TotalD(){
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut1").value) == false) {
                    msg = msg + "The reason of delay 1 days  should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut2").value) == false) {
                    msg = msg + "The reason of delay 2 days should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut3").value) == false) {
                    msg = msg + "The reason of delay 3 days on should not be Empty.\n";
                }
                if (msg != "") {
                    alert("Mandatory field information required \n\n" + msg);
                    document.getElementById("btnGenerate").disabled = true;
                    return false;
                }
                else {
                    document.getElementById("btnGenerate").disabled = false;
                    document.getElementById('txtTotalC').value = (document.getElementById('txtReasonDaysBaut1').value * 1) + (document.getElementById('txtReasonDaysBaut2').value * 1) + (document.getElementById('txtReasonDaysBaut3').value * 1);
                    CalculateTotal();
                }
            }
            
            function TotalB(){
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtReasonDays1").value) == false) {
                    msg = msg + "The reason of delay 1 days  should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDays2").value) == false) {
                    msg = msg + "The reason of delay 2 days should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDays3").value) == false) {
                    msg = msg + "The reason of delay 3 days on should not be Empty.\n";
                }
                if (IsEmptyCheck(document.getElementById("txtWorkHasBeenFinished").value) == false) {
                    msg = msg + "Work has been finished on date should not be Empty.\n";
                }
                if (msg != "") {
                    alert("Mandatory field information required \n\n" + msg);
                    document.getElementById("btnGenerate").disabled = true;
                    return false;
                }
                else {
                    document.getElementById("btnGenerate").disabled = false;
                    document.getElementById('txtTotalB').value = (document.getElementById('txtReasonDays1').value * 1) + (document.getElementById('txtReasonDays2').value * 1) + (document.getElementById('txtReasonDays3').value * 1);
                    CalculateTotal();
                }
            }
            
            function WindowsClose(){
                var tskid = 3;
                window.opener.location.href = '../dashboard/frmdocapproved.aspx?id=' + tskid + '&time=' + (new Date()).getTime();
                //alert(window.opener.progressWindow);
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
        </script>
        <script language="javascript" type="text/javascript" src="../Include/Validation.js">
        </script>
        <script language="javascript" type="text/javascript" src="../Include/popcalendar.js">
        </script>
        <style type="text/css">
            tr {
                padding: 3px;
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
                font-family: verdana;
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
            
            .lblBBTextL {
                border-bottom: 1px #000 solid;
                border-left: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: bold;
                text-decoration: none;
            }
            
            .lblBBTextM {
                border-bottom: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: normal;
                text-decoration: none;
                width: 1%;
            }
            
            .lblBBTextR {
                border-right: 1px #000 solid;
                border-bottom: 1px #000 solid;
                border-top: 1px #000 solid;
                font-family: verdana;
                font-size: 8pt;
                color: #000000;
                text-align: left;
                font-weight: normal;
                text-decoration: none;
            }
            
            #lblTotalA {
                font-weight: bold;
            }
            
            #lblJobDelay {
                font-weight: bold;
            }
        </style>
    </head>
    <body class="MainCSS">
        <form id="form1" runat="server">
            <input type="hidden" runat="server" id="HDDgSignTotal" /><input type="hidden" runat="server" id="hdnDGBox" /><input type="hidden" runat="server" id="hdnready4baut" /><input type="hidden" runat="server" id="hdnKeyVal" /><input type="hidden" runat="server" id="hdnScope" /><input runat="Server" id="hdndocId" type="hidden" /><input runat="Server" id="hdnWfId" type="hidden" /><input runat="Server" id="hdnversion" type="hidden" /><input runat="Server" id="hdnsiteid" type="hidden" /><input id="hdnAdminRole" runat="Server" type="hidden" /><input id="hdnAdmin" runat="Server" type="hidden" /><input id="hdnSiteno" runat="Server" type="hidden" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
            </asp:ScriptManager>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div id="dvPrint" runat="server" style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                                <tr>
                                    <td align="left" valign="top" style="width: 20%">
                                        <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" alt="nsnlogo" />
                                    </td>
                                    <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                        WORK COMPLETION TIME REPORT (WCTR)
                                        <br/>
                                    </td>
                                    <td align="right" valign="top" style="width: 20%">
                                        <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" alt="tselLogo" />
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                                <tr>
                                    <td colspan="3" class="Hcap">
                                        <br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText" style="width: 14%;">
                                        Work
                                    </td>
                                    <td style="width: 1%;">
                                        :
                                    </td>
                                    <td class="lblText" style="width: 594px">
                                        <asp:Label ID="lblScope" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText" style="width: 19%">
                                        Project
                                    </td>
                                    <td style="width: 1%">
                                        :
                                    </td>
                                    <td class="lblText" style="width: 594px">
                                        Telkomsel 2G BSS &amp; 3G UTRAN (TINEM 3)
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText" style="width: 19%">
                                        Site ID / Site Name
                                    </td>
                                    <td style="width: 1%">
                                        :
                                    </td>
                                    <td class="lblText" style="width: 594px">
                                        <asp:Label ID="lblSite" CssClass="lblBText" runat="server">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText">
                                        PO Number
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td class="lblText" style="width: 594px">
                                        <asp:Label ID="lblPO" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lblText">
                                        Project Id
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td class="lblText" style="width: 594px">
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
                                        On this Day<input id="txtDay" type="text" runat="Server" class="textFieldStyle" style="background-color: gray" readonly="readOnly" />
                                        <asp:Label ID="lblDay" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                        , Date<input id="txtDate" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" style="background-color: gray" readonly="readOnly"/>
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                        , Month<input id="txtMonth" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" style="background-color: gray" readonly="readOnly"/>
                                        <asp:Label ID="lblMonth" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                        , Year<input id="txtYear" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" style="background-color: gray" readonly="readOnly"/>
                                        <asp:Label ID="lblYear" runat="server" CssClass="lblBText">
                                        </asp:Label>
                                        , the work at project location mentioned above has been successfully completed.
                                        <br/>
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
                                                <td class="lblText" colspan="3" style="height: 15px">
                                                    <strong>The result is as follows: </strong>
                                                    <asp:Label ID="bastsublabel" Visible="false" runat="server" Width="135px">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 60%" class="lblText">
                                                    Duration of execution of work based on the Purchase Order
                                                </td>
                                                <td style="width: 1%">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtDurationExec" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" value="0" />
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
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtWorkStarted" type="text" runat="Server" class="textFieldStyle" />
                                                    <asp:ImageButton ID="btnWS" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
                                                    <asp:Label ID="lblWrkStarted" runat="server">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 60%; height: 22px;">
                                                    &nbsp; - Work should be finished on
                                                </td>
                                                <td style="height: 22px">
                                                    :
                                                </td>
                                                <td class="lblText" style="height: 22px; width: 483px;">
                                                    <input id="txtWorkShouldFinished" type="text" runat="Server" class="textFieldStyle" tabindex="0" />
                                                    <asp:ImageButton ID="btnWF" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
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
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtWorkHasBeenFinished" type="text" runat="Server" class="textFieldStyle" onblur="javascript:actualDurationBaut();" tabindex="0" />
                                                    <asp:ImageButton ID="btnWHF" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
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
                                                <td class="lblText" style="height: 22px; width: 483px;">
                                                    <input id="txtActualExec" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onfocus="javascript:actualDurationBaut();" value="0" />
                                                    <asp:Label ID="lblActualExec" runat="server">
                                                    </asp:Label>&nbsp;calendar
                                                    days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblBBTextL" style="width: 60%">
                                                    &nbsp;
                                                    <asp:Label ID="lblDelay" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td class="lblBBTextM">
                                                    :
                                                </td>
                                                <td class="lblBBTextR" style="width: 483px">
                                                    <input id="txtTotalA" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" value="0" />
                                                    <asp:Label ID="lblTotalA" runat="server">
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
                                                    &nbsp; 1.<input id="txtReason1" type="text" runat="Server" class="textFieldStyle" style="width: 200px"/>
                                                    <asp:Label ID="lblReason1" runat="server">                                    </asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtReasonDays1" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" onblur="TotalB();" value="0" />
                                                    <asp:Label ID="lblReasonDays1" runat="server">
                                                    </asp:Label>&nbsp;calendar
                                                    days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 60%">
                                                    &nbsp; 2.<input id="txtReason2" type="text" runat="Server" class="textFieldStyle" style="width: 200px"/>
                                                    <asp:Label ID="lblReason2" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtReasonDays2" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" onblur="Total(B);" value="0" />
                                                    <asp:Label ID="lblReasonDays2" runat="server">
                                                    </asp:Label>&nbsp;calendar
                                                    days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 60%; height: 22px;">
                                                    &nbsp; 3.<input id="txtReason3" type="text" runat="Server" class="textFieldStyle" style="width: 200px"/>
                                                    <asp:Label ID="lblReason3" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td style="height: 22px">
                                                    :
                                                </td>
                                                <td class="lblText" style="height: 22px; width: 483px;">
                                                    <input id="txtReasonDays3" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('-10123456789.');" onblur="TotalB();" value="0" />
                                                    <asp:Label ID="lblReasonDays3" runat="server">
                                                    </asp:Label>&nbsp;calendar
                                                    days
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
                                                <td class="lblBBTextR" style="width: 483px">
                                                    <input id="txtTotalB" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="CalculateTotal();" value="0" />
                                                    <asp:Label ID="lblTotalB" runat="server" Font-Bold="True">
                                                    </asp:Label>&nbsp;calendar
                                                    days
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
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtWorkShouldFinishedbaut" type="text" runat="Server" class="textFieldStyle" style="background-color: gray" readonly="readOnly" />
                                                    <asp:ImageButton ID="btnWSBaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" Visible="False" />
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
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtKPIAccepted" type="text" runat="Server" class="textFieldStyle" style="background-color: gray" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnWFBaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" Visible="False" />
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
                                                <td class="lblText" style="width: 60%; height: 22px;">
                                                    &nbsp; 1.<input id="txtReasonBaut1" type="text" runat="Server" class="textFieldStyle" style="width: 200px; background-color: gray;" readonly="readOnly"/>
                                                    <asp:Label ID="lblReasonBaut1" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td style="height: 22px">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 483px; height: 22px">
                                                    <input id="txtReasonDaysBaut1" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="TotalD();" value="0" style="background-color: gray" readonly="readOnly" />
                                                    <asp:Label ID="lblReasonDaysBaut1" runat="server">
                                                    </asp:Label>&nbsp;calendar days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 60%">
                                                    &nbsp; 2.<input id="txtReasonBaut2" type="text" runat="Server" class="textFieldStyle" style="width: 200px; background-color: gray;" readonly="readOnly"/>
                                                    <asp:Label ID="lblReasonBaut2" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 483px">
                                                    <input id="txtReasonDaysBaut2" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="TotalD();" value="0" style="background-color: gray" readonly="readOnly" />
                                                    <asp:Label ID="lblReasonDaysBaut2" runat="server">
                                                    </asp:Label>&nbsp;calendar days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 60%; height: 22px;">
                                                    &nbsp; 3.<input id="txtReasonBaut3" type="text" runat="Server" class="textFieldStyle" style="width: 200px; background-color: gray;" readonly="readOnly"/>
                                                    <asp:Label ID="lblReasonBaut3" runat="server">
                                                    </asp:Label>
                                                </td>
                                                <td style="height: 22px">
                                                    :
                                                </td>
                                                <td class="lblText" style="height: 22px; width: 483px;">
                                                    <input id="txtReasonDaysBaut3" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="TotalD();" value="0" style="background-color: gray" readonly="readOnly" />
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
                                                <td class="lblBBTextR" style="width: 483px">
                                                    <input id="txtTotalC" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" value="0" style="background-color: gray" readonly="readOnly" />
                                                    <asp:Label ID="lblTotalC" runat="server" Font-Bold="True">
                                                    </asp:Label>&nbsp;calendar days
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Hcap" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" colspan="3" runat="server" id="tdaccelerate">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Hcap" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblBBTextL" style="width: 60%">
                                                    &nbsp;Max (A + (B + C))
                                                </td>
                                                <td class="lblBBTextM">
                                                    :
                                                </td>
                                                <td class="lblBBTextR" style="width: 483px">
                                                    <input id="txtJobDelay" type="text" runat="Server" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');CalculateTotal();" value="0" />
                                                    <asp:Label ID="lblJobDelay" runat="server" Font-Bold="True">
                                                    </asp:Label>&nbsp;calendar
                                                    days
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
                                    <td colspan="3" align="Left">
                                        <asp:DataList ID="DLDigitalSign" runat="server" Width="100%" RepeatColumns="3">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                                    <tr>
                                                        <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
 border-style: solid;  vertical-align: top;  text-align: center;">
                                                            <%#Container.DataItem("Description") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="DgSign" runat="server" style="height: 100px;">
                                                            <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" /><asp:HiddenField runat="Server" ID="hdXCoordinate" /><asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
 border-style: solid;  vertical-align: top;  text-align: center;">
                                                            <%#Container.DataItem("name") %>
                                                            <br/>
                                                            <%#Container.DataItem("roledesc") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:DataList ID="DDBindSign" runat="server" Width="100%" RepeatColumns="3">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="lblBText" style="vertical-align: top; text-align: center;">
                                                            Signed by<%#Container.DataItem("name") %>
                                                            <br/>
                                                            <%#Container.DataItem("roledesc") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="lblBText" style="vertical-align: top; text-align: center; font-style: italic">
                                                            Date:<%#Container.DataItem("apptime") %>
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
                    <td>
                        <asp:Label ID="lblLinks" runat="server">
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td class="lblText" style="height: 20px">
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