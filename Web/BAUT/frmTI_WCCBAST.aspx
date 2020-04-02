<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_WCCBAST.aspx.vb" Inherits="frmTI_WCCBAST"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <script language="javascript" type="text/javascript">
    function WindowsClose() 
    { 
  
       window.opener.location.href = '../dashboard/DashboardpopupBast.aspx?time='+(new Date()).getTime();
        if (window.opener.progressWindow)
        {
            window.opener.progressWindow.close()
        }
        window.close();
    }
     
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
        <input runat="Server" id="hdndocid" type="hidden" />
        <input runat="Server" id="hdnWfId" type="hidden" />
        <input runat="Server" id="hdnversion" type="hidden" />
        <input runat="Server" id="hdnsiteid" type="hidden" />
        <input runat="Server" id="hdnAdminRole" type="hidden" />
        <input runat="Server" id="hdnAdmin" type="hidden" />
        <input runat="Server" id="hdnSiteno" type="hidden" />
        <input runat="Server" id="hdnPono" type="hidden" />
        <asp:Label ID="lblPO" runat="server" Font-Bold="True" Visible="False"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table border="0" style="width: 100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 908px">
                        <tr>
                            <td style="height: 671px">
                                <div id="DivWCC" runat="server" style="width: 100%;">
                                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td align="left" style="width: 10%" valign="top">
                                                <img height="37" src="../Images/nsn-logo.gif" width="105" />
                                            </td>
                                            <td align="center" class="lblBold" colspan="4" style="width: 67%" valign="top">
                                                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Purple">Work Completion Certificate</asp:Label>
                                            </td>
                                            <td align="right" style="width: 10%" valign="top">
                                                <img src="../Images/logo_tsel.png" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td class="Hcap" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="lblText" style="width: 25%">
                                                &nbsp;Subcontractor Name</td>
                                            <td style="width: 9px">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtSubconractorName" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 200px" />
                                            </td>
                                            <td class="lblText" style="width: 74%">
                                                <asp:Label ID="lblSubcontractorName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 21px">
                                                &nbsp;Date Issued</td>
                                            <td style="width: 9px; height: 21px;" valign="top">
                                                :</td>
                                            <td class="lblText" style="width: 433px; height: 21px;">
                                                <input id="txtDateIssued" runat="Server" class="textFieldStyle" type="text" style="width: 127px" />
                                                <asp:ImageButton ID="imgdateissue" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                            <td class="lblText" style="height: 21px">
                                                <asp:Label ID="lblDateIssued" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;Certificate Number</td>
                                            <td valign="top" style="width: 9px">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtCertificateNumber" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 100px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblCertificateNumber" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                &nbsp;Site ID (EPM)</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                <span style="font-size: 8pt; font-family: Arial Unicode MS">:</span></td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtSiteId" runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblSiteID" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                &nbsp;Site Name (EPM)</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtSiteName" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblSiteName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;Work Package ID (EPM)</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtWorkPackageID" runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                                                &nbsp;
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblWorkPackageId" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;Work Description</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtWorkDescription" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblWorkDescription" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 21px">
                                                &nbsp;Start Date</td>
                                            <td valign="top" style="width: 9px; height: 21px;">
                                                :</td>
                                            <td class="lblText" style="width: 433px; height: 21px;">
                                                <input id="txtStartDate" runat="Server" class="textFieldStyle" type="text" style="width: 125px" />
                                                <asp:ImageButton ID="imgstdate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                            <td class="lblText" style="height: 21px">
                                                <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;Completion Date</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtCompletionDate" runat="Server" class="textFieldStyle" type="text" style="width: 125px" />
                                                <asp:ImageButton ID="imgcmpdate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                            <td class="lblText">
                                                <asp:Label ID="lblCompletionDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                &nbsp;Delay Date from PO</td>
                                            <td valign="top" style="width: 9px; height: 20px;">
                                                :</td>
                                            <td class="lblText" style="width: 433px; height: 20px">
                                                <input id="txtDelayDateFromPo" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 125px" />
                                                <asp:ImageButton ID="imgdelaydate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblDelayDateFromPo" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;PO Partner</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtPoPartner" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblPOPartner" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                &nbsp;PO Telkomsel</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtPoTelkomsel" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblPoTelkomsel" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                &nbsp;Telkomsel BAUT/BAST Date</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtTelcomeSelBAUTDate" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 100px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblTelkomselBaut_BastDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="5" style="height: 16px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <strong>Note:</strong></td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="5" style="height: 6px">
                                                <em>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 559pt; border-collapse: collapse;
                                                        height: 24px">
                                                        <colgroup>
                                                            <col style="width: 42pt; mso-width-source: userset; mso-width-alt: 2048" width="56" />
                                                            <col span="2" style="width: 58pt; mso-width-source: userset; mso-width-alt: 2816"
                                                                width="77" />
                                                            <col style="width: 48pt" width="64" />
                                                            <col style="width: 51pt; mso-width-source: userset; mso-width-alt: 2486" width="68" />
                                                            <col style="width: 86pt; mso-width-source: userset; mso-width-alt: 4205" width="115" />
                                                            <col style="width: 48pt" width="64" />
                                                            <col style="width: 179pt; mso-width-source: userset; mso-width-alt: 8704" width="238" />
                                                        </colgroup>
                                                        <tr height="17" style="height: 12.75pt">
                                                            <td class="xl66" colspan="8" height="34" rowspan="2" style="border-right: #ece9d8;
                                                                border-top: #ece9d8; border-left: #ece9d8; width: 570pt; border-bottom: #ece9d8;
                                                                height: 25.5pt; background-color: transparent" width="759">
                                                                <em><span style="font-size: 8pt; font-family: Arial">NSN reserved the right to disclaim
                                                                    and request the assigned Subcontractor Company above to resolve and/or provide required
                                                                    action within certain time given if in case for any reasons there are incomplete
                                                                    issues/obligation or deficiencies</span></em></td>
                                                        </tr>
                                                        <tr height="17" style="height: 12.75pt">
                                                        </tr>
                                                    </table>
                                                </em>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="5" style="height: 8px">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 557pt; border-collapse: collapse">
                                                    <colgroup>
                                                        <col style="width: 42pt; mso-width-source: userset; mso-width-alt: 2048" width="56" />
                                                        <col span="2" style="width: 58pt; mso-width-source: userset; mso-width-alt: 2816"
                                                            width="77" />
                                                        <col style="width: 48pt" width="64" />
                                                        <col style="width: 51pt; mso-width-source: userset; mso-width-alt: 2486" width="68" />
                                                        <col style="width: 86pt; mso-width-source: userset; mso-width-alt: 4205" width="115" />
                                                        <col style="width: 48pt" width="64" />
                                                        <col style="width: 179pt; mso-width-source: userset; mso-width-alt: 8704" width="238" />
                                                        <col style="width: 55pt; mso-width-source: userset; mso-width-alt: 2669" width="73" />
                                                    </colgroup>
                                                    <tr height="17" style="height: 12.75pt">
                                                        <td class="xl66" colspan="9" height="114" rowspan="2" style="border-right: #ece9d8;
                                                            border-top: #ece9d8; border-left: #ece9d8; width: 625pt; border-bottom: #ece9d8;
                                                            height: 85.5pt; background-color: transparent" width="832">
                                                            <strong><span style="font-size: 8pt; color: #ff0000; font-family: Arial">* Important
                                                                Note</span></strong><font class="font7"><br />
                                                                    <span style="font-size: 8pt; font-family: Arial">* Partner is required to prepare the
                                                                        WCC and submit for approval together in the Site Folder<br />
                                                                        * WCC approval should attach with PO &amp; Justification letter from NSN<br />
                                                                        * NO additional work will be approved after the BAK / BAN is signed by Telkomsel<br />
                                                                        * 3G WCC will be approved after site approved<span style="mso-spacerun: yes">&nbsp;
                                                                        </span>by Manage Service Department<br />
                                                                        * WCC is approved after ATP (Acceptance Test Protocol) on site approved by NSN ATP
                                                                        Inspector with 0 defect.<br />
                                                                        * WCC will be approved with Guaranty letter approval by Respective National Manager
                                                                        of each department.(Only on special case)</span></font></td>
                                                    </tr>
                                                    <tr height="97" style="height: 72.75pt; mso-height-source: userset">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="5" style="height: 8px">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
                                                <asp:DataList ID="DLDigitalSign" runat="server" RepeatColumns="3" Width="100%">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="200px">
                                                            <tr>
                                                                <td class="lblBText" style="border-width: 1px; border-color: Black; border-collapse: collapse;
                                                                    border-style: solid; vertical-align: top; text-align: center;">
                                                                    <%#Container.DataItem("Description")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="DgSign" runat="server" style="height: 100px;">
                                                                    <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" />
                                                                    <asp:HiddenField ID="hdXCoordinate" runat="Server" />
                                                                    <asp:HiddenField ID="hdYCoordinate" runat="Server" />
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
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 18px">
                                <asp:Button ID="btnGenerate" runat="server" CssClass="buttonStyle" Text="Generate" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
