<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_ATP.aspx.vb" Inherits="frmTI_ATP"
    EnableEventValidation="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript">
     function WindowsClose() 
    { 
  
       window.opener.location.href = '../dashboard/DashboardpopupBaut.aspx?time='+(new Date()).getTime();
        if (window.opener.progressWindow)
        {
            window.opener.progressWindow.close()
        }
        window.close();
    } 
  function getControlPostion()
  {
     var Total=document.getElementById('HDDgSignTotal').value; 
     for(intCount1=0;intCount1<Total;intCount1++)
     {
       var divctrl = document.getElementById('DLDigitalSign_ctl0'+ intCount1 +'_ImgPostion');
       eval("document.getElementById('DLDigitalSign_ctl0"+ intCount1 +"_hdXCoordinate')").value = findPosX(divctrl);
       eval("document.getElementById('DLDigitalSign_ctl0"+ intCount1 +"_hdYCoordinate')").value = findPosY(divctrl);  
     // alert(findPosX(divctrl) + " , " + findPosY(divctrl));
       divctrl = document.getElementById('dlWCTR_ctl0'+ intCount1 +'_ImgPostion');
       eval("document.getElementById('dlWCTR_ctl0"+ intCount1 +"_hdXCoordinate')").value = findPosX(divctrl);
       eval("document.getElementById('dlWCTR_ctl0"+ intCount1 +"_hdYCoordinate')").value = findPosY(divctrl);  
     // alert(findPosX(divctrl) + " , " + findPosY(divctrl));
    
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
          alert('Integration date not available');
      }
      if (type=='IntD')
      {
        alert('The document cannot be uploaded before the integration date');
      } 
      if (type=='2sta')
      {
         alert('This Document already processed for second stage so cannot upload again ');
      }
      if (type=='nop')
      {
         alert('No permission to upload this Document ');
      }
     
      window.location = '../PO/frmSiteDocUploadTree.aspx'
   }  
   
    function TotalD()
    {
        //actualDurationBaut();
        //document.getElementById('TxtActualExecBaut').value=datediff
        var msg="";  
        if (IsEmptyCheck(document.getElementById("TxtReasonDaysBaut1").value)==false) 
        {
           // msg = msg+ "The reason of delay 1 days  should not be Empty.\n"
            document.getElementById("TxtReasonDaysBaut1").value=0;
        }
        if (IsEmptyCheck(document.getElementById("TxtReasonDaysBaut2").value)==false) 
        {
            //msg = msg+ "The reason of delay 2 days should not be Empty.\n"
             document.getElementById("TxtReasonDaysBaut2").value=0;
        }

        if (IsEmptyCheck(document.getElementById("TxtReasonDaysBaut3").value)==false) 
        {
            document.getElementById("TxtReasonDaysBaut3").value=0;
            // msg = msg+ "The reason of delay 3 days on should not be Empty.\n"
        }
        if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            document.getElementById('TxtTotalC').value=(document.getElementById('TxtReasonDaysBaut1').value*1)+(document.getElementById('TxtReasonDaysBaut2').value*1)+(document.getElementById('TxtReasonDaysBaut3').value*1)
        }
        
    }
    function actualDurationBaut()
   {
   
         var msg="";  
        if (IsEmptyCheck(document.getElementById("TxtDurationExecBaut").value)==false) 
        {
            msg = msg+ "Duration of execution of work  should not be Empty.\n"
        }
        if (IsEmptyCheck(document.getElementById("TxtWrkStartedBaut").value)==false) 
        {
            msg = msg+ "Work is started on date should not be Empty.\n"
        }
        
         if (IsEmptyCheck(document.getElementById("TxtWrkshouldFinishedbaut").value)==false) 
        {
            msg = msg+ "Work should be finished date on should not be Empty.\n"
        }
         if (IsEmptyCheck(document.getElementById("TxtWorkhasbeenfinishedBaut").value)==false) 
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
            var strDate1 = document.getElementById('TxtWorkhasbeenfinishedBaut').value
            var strDate2 =document.getElementById('TxtWrkStartedBaut').value 
            var strDate3 =document.getElementById('TxtWrkshouldFinishedbaut').value 


            var dt1 = new Date(Date.parse(strDate1.replace('-', ' '))); 
            var dt2 = new Date(Date.parse(strDate2.replace('-', ' ')));
            var dt3 = new Date(Date.parse(strDate3.replace('-', ' ')));
            datediff = ((dt1-dt2)/(24*60*60*1000)) 
            document.getElementById('TxtActualExecBaut').value=datediff
            if(datediff<0)        
            {
                document.getElementById('TxtReasonBaut1').readOnly =false;
                document.getElementById('TxtReasonBaut2').readOnly =false;
                document.getElementById('TxtReasonBaut3').readOnly =false;
                document.getElementById('TxtReasonDaysBaut1').readOnly =false;
                document.getElementById('TxtReasonDaysBaut2').readOnly =false;
                document.getElementById('TxtReasonDaysBaut3').readOnly =false;
                document.getElementById('TxtTotalD').readOnly =false;
                document.getElementById('LblDelayBaut').innerHTML ="Delay (C)"
            }
            else
            {
                document.getElementById('TxtReasonBaut1').value='';
                document.getElementById('TxtReasonBaut1').readOnly =true;
                document.getElementById('TxtReasonBaut2').value='';
                document.getElementById('TxtReasonBaut2').readOnly =true;
                document.getElementById('TxtReasonBaut3').value='';
                document.getElementById('TxtReasonBaut3').readOnly =true;
                document.getElementById('TxtReasonDaysBaut1').value=0;
                document.getElementById('TxtReasonDaysBaut1').readOnly =true;
                document.getElementById('TxtReasonDaysBaut2').value=0;
                document.getElementById('TxtReasonDaysBaut2').readOnly =true;
                document.getElementById('TxtReasonDaysBaut3').value=0;
                document.getElementById('TxtReasonDaysBaut3').readOnly =true;
                document.getElementById('TxtTotalD').value=0;
                document.getElementById('TxtTotalD').readOnly =true;
                document.getElementById('LblDelayBaut').innerHTML ="Acceleration (C)"
            }
            if (dt1 > dt3)
            {
                document.getElementById('TxtTotalC').value=(document.getElementById('TxtActualExecBaut').value*1)-(document.getElementById('TxtDurationExecBaut').value*1)
            }
            else
            {
                document.getElementById('TxtTotalC').value=(document.getElementById('TxtDurationExecBaut').value*1)-(document.getElementById('TxtActualExecBaut').value*1)
            }
        }
    }  
    </script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <style type="text/css">
        tr
        {
            padding: 3px;
        }
        .PageBreak
        {
	        page-break-before:always;
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
        .LblBold
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
            font-family: Verdana;
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
    </style>
</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <table style="width: 100%;">
            <tr>
                <td style="height: 295px">
                    <div id="DivPrintWCTR" runat="server" style="width: 100%;">
                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                            <tr>
                                <td align="left" valign="top" style="width: 20%">
                                    <img src="http://www.nsndemo.nsnebast.com:520/Images/nsn-logo.gif" height="36px" width="104px" />
                                </td>
                                <td colspan="4" align="center" class="LblBold" valign="top" style="width: 60%">
                                    ATP Approval Sheet<br />
                                </td>
                                <td align="right" valign="top" style="width: 20%">
                                    <img src="http://www.nsndemo.nsnebast.com:520/Images/telko-logo.gif" />
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
                                <td class="lblText" colspan="3">
                                    This approval sheet declares that the Acceptance Test Procedure (ATP) has been performed
                                    at the below location.</td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="3">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px;">
                                    Site Name</td>
                                <td style="width: 1%;">
                                    :
                                </td>
                                <td class="lblText" style="width: 1130px">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">
                                    Site ID</td>
                                <td style="width: 1%">
                                    :</td>
                                <td class="lblText" style="width: 1130px">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" style="width: 206px">
                                    Scope</td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td class="lblTextSmall" style="width: 1130px; text-align: left;">
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="lblText">
                                    And the work has been considered as complete and satisfactory fulfil the standard technical requirements in Telkomsel project.</td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Hcap">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="lblText" colspan="4">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_Id"
                                        EmptyDataText="No Records Found" PageSize="2" Width="100%">
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <Columns>
                                            <asp:BoundField DataField="doc_id" HeaderText="Description" />
                                            <asp:BoundField DataField="docpath" HeaderText="Name" />
                                            <asp:BoundField DataField="docpath" HeaderText="Company" />
                                            <asp:BoundField DataField="docpath" HeaderText="Date Time" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="lblText">
                    This approval sheet is automatically generated by eBAST upon acceptance of all person
                    in charge and no more signatures on hard copy is required. This sheet is inseparable
                    from the ATP report and should be the first thing seen when opening the report.</td>
            </tr>
        </table>
    </form>
</body>
</html>
