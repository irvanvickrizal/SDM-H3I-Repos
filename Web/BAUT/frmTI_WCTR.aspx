<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_WCTR.aspx.vb" Inherits="frmTI_WCTR"
    EnableEventValidation="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WCTR</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <script language="javascript" type="text/javascript">
  function getControlPosition()
  {
     var Total=document.getElementById('HDDgSignTotal').value; 
     for(intCount1=0;intCount1<Total;intCount1++)
     {
       var divctrl = document.getElementById('DLDigitalSign_ctl0'+ intCount1 +'_ImgPostion');
       eval("document.getElementById('DLDigitalSign_ctl0"+ intCount1 +"_hdXCoordinate')").value = findPosX(divctrl);
       eval("document.getElementById('DLDigitalSign_ctl0"+ intCount1 +"_hdYCoordinate')").value = findPosY(divctrl);  
      //alert(findPosX(divctrl) + " , " + findPosY(divctrl));
     }
  }
    
  function findPosX(imgElem)
  {
    xPos = eval(imgElem).offsetLeft;
	tempEl = eval(imgElem).offsetParent;
  	while (tempEl != null) {
  		xPos += tempEl.offsetLeft;
  		tempEl = tempEl.offsetParent;
  	}
	return xPos;
  }

  function findPosY(imgElem)
  {
    yPos = eval(imgElem).offsetTop;
	tempEl = eval(imgElem).offsetParent;
	while (tempEl != null) {
  		yPos += tempEl.offsetTop;
  		tempEl = tempEl.offsetParent;
  	}
	return yPos;
  } 
  
     function Showmain(type) 
   { 
      if (type=='Int')
      {
          alert('Integration date not vailable');
      }
      if (type=='IntD')
      {
        alert('The document cannot be uploaded before the integration date');
      } 
      if (type=='2sta')
      {
         alert('This Document already processed for second stage so cannot upload again ');
      }
   }
   function actualDurationBaut()
   {
   
         var msg="";  
        if (IsEmptyCheck(document.getElementById("txtDurationExecBaut").value)==false) 
        {
            msg = msg+ "Duration of execution of work  should not be Empty.\n"
        }
        if (IsEmptyCheck(document.getElementById("txtWorkStartedBaut").value)==false) 
        {
            msg = msg+ "Work is started on date should not be Empty.\n"
        }
        
         if (IsEmptyCheck(document.getElementById("txtWorkShouldFinishedbaut").value)==false) 
        {
            msg = msg+ "Work should be finished date on should not be Empty.\n"
        }
         if (IsEmptyCheck(document.getElementById("txtWorkHasBeenFinishedBaut").value)==false) 
        {
            msg = msg+ " Work has been finished on date should not be Empty.\n"
        }
          if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            var strDate1 = document.getElementById('txtWorkHasBeenFinishedBaut').value
            var strDate2 =document.getElementById('txtWorkStartedBaut').value 
            var strDate3 =document.getElementById('txtWorkShouldFinishedbaut').value 


            var dt1 = new Date(Date.parse(strDate1.replace('-', ' '))); 
            var dt2 = new Date(Date.parse(strDate2.replace('-', ' ')));
            var dt3 = new Date(Date.parse(strDate3.replace('-', ' ')));
            datediff = ((dt1-dt2)/(24*60*60*1000)) 
            document.getElementById('txtActualExecBaut').value=datediff
            if(datediff<0)        
            {
                document.getElementById('txtReasonBaut1').readOnly =false;
                document.getElementById('txtReasonBaut2').readOnly =false;
                document.getElementById('txtReasonBaut3').readOnly =false;
                document.getElementById('txtReasonDaysBaut1').readOnly =false;
                document.getElementById('txtReasonDaysBaut2').readOnly =false;
                document.getElementById('txtReasonDaysBaut3').readOnly =false;
                document.getElementById('txtTotalD').readOnly =false;
                document.getElementById('lblDelayBaut').innerHTML ="Delay (C)"
            }
            else
            {
                document.getElementById('txtReasonBaut1').value='';
                document.getElementById('txtReasonBaut1').readOnly =true;
                document.getElementById('txtReasonBaut2').value='';
                document.getElementById('txtReasonBaut2').readOnly =true;
                document.getElementById('txtReasonBaut3').value='';
                document.getElementById('txtReasonBaut3').readOnly =true;
                document.getElementById('txtReasonDaysBaut1').value=0;
                document.getElementById('txtReasonDaysBaut1').readOnly =true;
                document.getElementById('txtReasonDaysBaut2').value=0;
                document.getElementById('txtReasonDaysBaut2').readOnly =true;
                document.getElementById('txtReasonDaysBaut3').value=0;
                document.getElementById('txtReasonDaysBaut3').readOnly =true;
                document.getElementById('txtTotalD').value=0;
                document.getElementById('txtTotalD').readOnly =true;
                document.getElementById('lblDelayBaut').innerHTML ="Acceleration (C)"
            }
            if (dt1 > dt3)
            {
                document.getElementById('txtTotalC').value=(document.getElementById('txtActualExecBaut').value*1)-(document.getElementById('txtDurationExecBaut').value*1)
            }
            else
            {
                document.getElementById('txtTotalC').value=(document.getElementById('txtDurationExecBaut').value*1)-(document.getElementById('txtActualExecBaut').value*1)
            }
        }


    }
    function TotalD()
    {
        actualDurationBaut();
         document.getElementById('txtActualExecBaut').value=datediff
            if(datediff>0)        
            {
                 var msg="";  
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut1").value)==false) 
                {
                    msg = msg+ "The reason of delay 1 days  should not be Empty.\n"
                }
                if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut2").value)==false) 
                {
                    msg = msg+ "The reason of delay 2 days should not be Empty.\n"
                }
                
                 if (IsEmptyCheck(document.getElementById("txtReasonDaysBaut3").value)==false) 
                {
                    msg = msg+ "The reason of delay 3 days on should not be Empty.\n"
                }
                 if (IsEmptyCheck(document.getElementById("txtWorkHasBeenFinishedBaut").value)==false) 
                {
                    msg = msg+ " Work has been finished on date should not be Empty.\n"
                }
                  if (msg != "")
                {
                    alert("Mandatory field information required \n\n" + msg);
                    return false;
                }
                else
                {
                    document.getElementById('txtTotalD').value=(document.getElementById('txtReasonDaysBaut1').value*1)+(document.getElementById('txtReasonDaysBaut2').value*1)+(document.getElementById('txtReasonDaysBaut3').value*1)
                }
        }
    }
    </script>

    <style type="text/css">
        tr
        {
            padding: 3px;
        }
        .MainCSS
        {
            margin-bottom: 0px;
            margin-left: 20px;
            margin-right: 20px;
            margin-top: 0px;
            width: 800px;
            height: 700px;
            text-align: center;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
        }
        .lblBText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblBold
        {
            font-family: verdana;
            font-size: 12pt;
            color: #000000;
            font-weight: bold;
        }
        .textFieldStyle
        {
            background-color: white;
            border: 1px solid;
            color: black;
            font-family: verdana;
            font-size: 9pt;
        }
        .GridHeader
        {
            color: #0e1b42;
            background-color: Orange;
            font-size: 9pt;
            font-family: verdana;
            text-align: Left;
            vertical-align: bottom;
            font-weight: bold;
        }
        .GridEvenRows
        {
            background-color: #e3e3e3;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
        }
        .GridOddRows
        {
            background-color: white;
            vertical-align: top;
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
        }
        .PagerTitle
        {
            font-size: 8pt;
            background-color: #cddbbf;
            text-align: Right;
            vertical-align: middle;
            color: #25375b;
            font-weight: bold;
        }
        .Hcap
        {
            height: 5px;
        }
        .VCap
        {
            width: 10px;
        }
        .lblBBTextL
        {
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
        .lblBBTextM
        {
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
        .lblBBTextR
        {
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
        #lblTotalA
        {
            font-weight: bold;
        }
        #lblJobDelay
        {
            font-weight: bold;
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
        <input runat="Server" id="hdndocId" type="hidden" />
        <input runat="Server" id="hdnWfId" type="hidden" />
        <input runat="Server" id="hdnversion" type="hidden" />
        <input runat="Server" id="hdnsiteid" type="hidden" />
        <input id="hdnAdminRole" runat="Server" type="hidden" />
        <input id="hdnAdmin" runat="Server" type="hidden" />
        <input id="hdnSiteno" runat="Server" type="hidden" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ScriptManager>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div id="dvPrint" runat="server" style="width: 100%;">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                                </td>
                                <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                    WORK COMPLETION TIME REPORT (WCTR)<br />
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
                                <td class="lblText" style="width: 14%;">
                                    Work
                                </td>
                                <td style="width: 1%;">
                                    :
                                </td>
                                <td class="lblText" style="width: 85%">
                                    <asp:Label ID="lblScope" runat="server" CssClass="lblBText"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 19%">
                                    Project</td>
                                <td style="width: 1%">
                                    :</td>
                                <td class="lblText" style="width: 80%">
                                    Telkomsel 2G BSS &amp; 3G UTRAN (TINEM 3)</td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 19%">
                                    Site ID / Site Name</td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td class="lblText" style="width: 80%">
                                    <asp:Label ID="lblSite" CssClass="lblBText" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText">
                                    PO Number</td>
                                <td>
                                    :
                                </td>
                                <td class="lblText">
                                    <asp:Label ID="lblPO" runat="server" CssClass="lblBText"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="lblText">
                                    On this Day
                                    <input id="txtDay" type="text" runat="Server" class="textFieldStyle" /><asp:Label
                                        ID="lblDay" runat="server" CssClass="lblBText"></asp:Label>
                                    , Date
                                    <input id="txtDate" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                        ID="lblDate" runat="server" CssClass="lblBText"></asp:Label>
                                    , Month
                                    <input id="txtMonth" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                        ID="lblMonth" runat="server" CssClass="lblBText"></asp:Label>
                                    , Year
                                    <input id="txtYear" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                        ID="lblYear" runat="server" CssClass="lblBText"></asp:Label>
                                    , the work at project location mentioned above has been successfully completed.<br />
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
                                                <strong>
                                    The result is as follows: (based on BAST Submission)</strong></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%" class="lblText">
                                                Duration of execution of work based on the Purchase Order
                                            </td>
                                            <td style="width: 1%">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 40%">
                                                <input id="txtDurationExec" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblDurationExec" runat="server"></asp:Label>
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
                                            <td class="lblText">
                                                <input id="txtWorkStarted" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWS" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" Visible="False" />
                                                <asp:Label
                                                    ID="lblWrkStarted" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 23px;">
                                                &nbsp; - Work should be finished on
                                            </td>
                                            <td style="height: 23px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 23px">
                                                <input id="txtWorkShouldFinished" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWF" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" Visible="False" />
                                                <asp:Label
                                                    ID="lblWorkShouldFinished" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; - Work has been finished on
                                            </td>
                                            <td valign="Top">
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtWorkHasBeenFinished" type="text" runat="Server" class="textFieldStyle"
                                                    onblur="actualDuration()" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWHF" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" Visible="False" />
                                                <asp:Label ID="lblWorkhasbeenFinished" runat="server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 22px">
                                                The actual duration of execution of work</td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px">
                                                <input id="txtActualExec" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblActualExec" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp;Delay / Acceleration ( A )
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblBBTextR">
                                                <input id="txtTotalA" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblTotalA" runat="server"></asp:Label>&nbsp;calendar days (*)
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
                                                &nbsp; 1.
                                                <input id="txtReason1" type="text" runat="Server" class="textFieldStyle" style="width: 200px" readonly="readOnly" /><asp:Label
                                                    ID="lblReason1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtReasonDays1" type="text" runat="Server" class="textFieldStyle" onclick="return txtReasonDays1_onclick()" readonly="readOnly" /><asp:Label
                                                    ID="lblReasonDays1" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 2.
                                                <input id="txtReason2" type="text" runat="Server" class="textFieldStyle" style="width: 200px" readonly="readOnly" /><asp:Label
                                                    ID="lblReason2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtReasonDays2" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblReasonDays2" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 22px;">
                                                &nbsp; 3.
                                                <input id="txtReason3" type="text" runat="Server" class="textFieldStyle" style="width: 200px" readonly="readOnly" /><asp:Label
                                                    ID="lblReason3" runat="server"></asp:Label>
                                            </td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px">
                                                <input id="txtReasonDays3" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblReasonDays3" runat="server"></asp:Label>&nbsp;calendar days
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
                                            <td class="lblBBTextR">
                                                <input id="txtTotalB" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" /><asp:Label
                                                    ID="lblTotalB" runat="server" Font-Bold="True"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" colspan="3">
                                                <strong>
                                                The result is as follows: (based on BAUT Submission)</strong></td>
                                        </tr>
                                              <tr>
                                            <td style="width: 60%" class="lblText">
                                                Duration of execution of work based on the Purchase Order
                                            </td>
                                            <td style="width: 1%">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 40%">
                                                <input id="txtDurationExecBaut" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblDurationExecBaut" runat="server"></asp:Label>
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
                                            <td class="lblText">
                                                <input id="txtWorkStartedBaut" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWSBaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label
                                                    ID="lblWrkStartedBaut" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp; - Work should be finished on
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtWorkShouldFinishedbaut" type="text" runat="Server" class="textFieldStyle" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWFBaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label
                                                    ID="lblWorkShouldFinishedbaut" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; - Work has been finished on
                                            </td>
                                            <td valign="Top">
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtWorkHasBeenFinishedBaut" type="text" runat="Server" class="textFieldStyle" onfocus="actualDurationBaut();"
                                                    onchange="actualDurationBaut();" readonly="readOnly" />
                                                <asp:ImageButton ID="btnWHFBaut" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <asp:Label ID="lblWorkhasbeenfinishedBaut" runat="server"></asp:Label>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 22px">
                                                The actual duration of execution of work</td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px">
                                                <input id="txtActualExecBaut" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onfocus="actualDurationBaut();"/><asp:Label
                                                    ID="lblActualExecBaut" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp;<asp:Label ID="lblDelayBaut" runat="server"></asp:Label></td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblBBTextR">
                                                <input id="txtTotalC" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblTotalC" runat="server" Font-Bold="True"></asp:Label>&nbsp;calendar days (*)
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
                                                &nbsp; 1.
                                                <input id="txtReasonBaut1" type="text" runat="Server" class="textFieldStyle" style="width: 200px"/><asp:Label
                                                    ID="lblReasonBaut1" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtReasonDaysBaut1" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblReasonDaysBaut1" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%">
                                                &nbsp; 2.
                                                <input id="txtReasonBaut2" type="text" runat="Server" class="textFieldStyle" style="width: 200px" /><asp:Label
                                                    ID="lblReasonBaut2" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td class="lblText">
                                                <input id="txtReasonDaysBaut2" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblReasonDaysBaut2" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="width: 60%; height: 22px;">
                                                &nbsp; 3.
                                                <input id="txtReasonBaut3" type="text" runat="Server" class="textFieldStyle" style="width: 200px" /><asp:Label
                                                    ID="lblReasonBaut3" runat="server"></asp:Label>
                                            </td>
                                            <td style="height: 22px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 22px">
                                                <input id="txtReasonDaysBaut3" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="TotalD();"/><asp:Label
                                                    ID="lblReasonDaysBaut3" runat="server"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp; Total ( D )
                                            </td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblBBTextR">
                                                <input id="txtTotalD" type="text" runat="Server" class="textFieldStyle"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblTotalD" runat="server" Font-Bold="True"></asp:Label>&nbsp;calendar days
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" colspan="3">
                                               otal period of work completion has been delayed / accelerated :
                                            </td>
                                        </tr>
                                                                               <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblBBTextL" style="width: 60%">
                                                &nbsp;Max ((A – B) + (C - D))</td>
                                            <td class="lblBBTextM">
                                                :
                                            </td>
                                            <td class="lblBBTextR">
                                                <input id="txtJobDelay" type="text" runat="Server" class="textFieldStyle" readonly="readOnly"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/><asp:Label
                                                    ID="lblJobDelay" runat="server"></asp:Label>&nbsp;calendar days
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
                                <td colspan="3">
                                    <asp:DataList ID="DLDigitalSign" runat="server" Width="100%" RepeatColumns="3">
                                        <ItemTemplate>
                                            <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                                <tr>
                                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                        border-style: solid; vertical-align: top; text-align: center;">
                                                        <%#Container.DataItem("Description")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="DgSign" runat="server" style="height: 100px;">
                                                        <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" />
                                                        <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                                        <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                        border-style: solid; vertical-align: top; text-align: center;">
                                                        <%#Container.DataItem("name")%>
                                                        <br />
                                                        <%#Container.DataItem("roledesc")%>
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
                    </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="lblText">
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
