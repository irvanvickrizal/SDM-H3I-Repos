<%@ Page Language="VB" AutoEventWireup="true" CodeFile="frmTI_QC.aspx.vb" Inherits="BAUT_frmTI_QC"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QC</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

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
       function showErrRejection(type,docname)
       {
        if (type=='rmkem')
          {
            alert('Please fill reason of first');
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
            margin-left: 10px;
            margin-top: 0px;
            width: 800px;
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
        .lblRef
        {
            font-family: verdana;
            font-size: 10pt;
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
        .checkDocumentPanel
        {
            margin-left: 40px;
        }
        .GrdDocPanelRows
        {
            font-family: verdana;
            font-size: 8pt;
        }
        a:link
        {
            font-family: verdana;
            font-size: 8pt;
        }
        a:visited
        {
            font-family: verdana;
            font-size: 8pt;
        }
        a:active
        {
            font-family: verdana;
            font-size: 8pt;
        }
        
    </style>

    <script type="text/javascript">
        function CheckEmptyMandatory()
        {
            var onAirDate = document.getElementById("TxtOnAirDate");
            var integrationDate = document.getElementById("TxtIntDate");
            var sitename = document.getElementById("lblSiteName");
            var typeofwork = document.getElementById("txtTWork");
            var bandtype = document.getElementById("lblBand");
            var config = document.getElementById("TxtExtConfig");
            var onConfig = document.getElementById("txtOnAirCon");
            
            if (integrationDate.value.length < 1)
            {
                alert("Please define integration date first!");
                return false;
            }
            
            if (onAirDate.value.length < 1)
            {
                alert("Please define on air date first!");
                return false;
            }
            
            if (sitename.value.length < 1)
            {
                alert("Please insert sitename first!");
                return false;
            }
            
            if (typeofwork.value.length < 1)
            {
                alert("Please input type of work first!");
                return false;
            }
            
            if (bandtype.value.length < 1)
            {
                alert("Please input band first!");
                return false;
            }
            
            if (config.value.length < 1)
            {
                alert("Please input existing config first!");
                return false;
            }
            
            if (onConfig.value.length < 1)
            {
                alert("Please input on air config first!");
                return false;
            }
            
        }
    </script>

</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="hdnRole" />
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
        <input id="hdSno" runat="server" type="hidden" value="0" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="dvPrint" runat="server" style="width: 800px; height: 800px;">
            <div id="headerPanel" style="margin-top: 15px;">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/nsn-logo.gif" height="36px" width="104px"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                            SITE QUALITY ACCEPTANCE CERTIFICATE
                        </td>
                        <td align="right" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/logo_tsel.png" alt="tsellogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="secondHeaderPanel">
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td colspan="2" class="lblText">
                            <b style="border-right: black 2px solid; border-top: black 2px solid; border-left: black 2px solid;
                                border-bottom: black 2px solid">RECAPITULATION</b><br />
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-bottom: 10px; width: 100%;">
                <div style="float: left; width: 30%; text-align: left;">
                    <span class="lblText">Reference No :</span>
                    <asp:Label ID="lblPO" runat="server" CssClass="lblText" Visible="false"></asp:Label>
                </div>
                <div style="float: right; width: 70%; text-align: right;">
                    <div style="border-style: solid; border-color: Black; border-width: 1px; width: 300px;
                        text-align: center; height: 20px; padding: 2px;">
                        <b>
                            <asp:Label ID="lblRefNO" runat="server" CssClass="lblRef"></asp:Label></b>
                    </div>
                </div>
            </div>
            <div id="siteATTPanel" style="margin-top: 10px;  height:200px;">
                <table width="100%" cellpadding="0" cellspacing="0" border="1">
                    <tr class="lblText">
                        <td style="width: 25%">
                            PO No
                        </td>
                        <td style="width: 25%">
                            <b>
                                <asp:Label ID="LblPono" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td style="width: 25%">
                            OSS Name
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="TxtOSSName" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="LblOSSName" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td style="width: 25%">
                            Site ID
                        </td>
                        <td style="width: 25%">
                            <b>
                                <asp:Label ID="lblSiteID" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td style="width: 25%">
                            BSC Name
                        </td>
                        <td style="width: 25%">
                            <asp:TextBox ID="txtBSCName" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblBSCName" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Site Name
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            New Site ID
                        </td>
                        <td>
                            <asp:TextBox ID="txtNSiteID" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblNSiteID" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            BTSE Type
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBTSType" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblBTSType" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            Clutter Type
                        </td>
                        <td>
                            <asp:TextBox ID="TxtClutterType" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblClutterType" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Type of Work
                        </td>
                        <td>
                            <asp:TextBox ID="txtTWork" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblTWork" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            LAC
                        </td>
                        <td>
                            <asp:TextBox ID="txtLAC" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblLAC" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            NE Type
                        </td>
                        <td>
                            <asp:TextBox ID="txtNEType" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblNEType" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            CI
                        </td>
                        <td>
                            <asp:TextBox ID="txtCI" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblCI" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Band
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblBand" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            Existing config
                        </td>
                        <td>
                            <asp:TextBox ID="TxtExtConfig" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblExtConfig" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            BCF/ET
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBCFET" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="LblBCFET" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                        <td>
                            On Air Config
                        </td>
                        <td>
                            <asp:TextBox ID="txtOnAirCon" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblOnAirCon" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="siteIntegrationDatePanel" style="margin-top: 10px;">
                <table cellpadding="0" cellspacing="0" border="1" width="100%">
                    <tr class="lblText">
                        <td>
                            <span class="lblText">Connected Date </span>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblIntDate" runat="server" CssClass="lblText"></asp:Label>
                            <asp:TextBox ID="TxtIntDate" runat="server" CssClass="lblText"></asp:TextBox>
                            <asp:ImageButton ID="BtnIntDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
                        </td>
                        <td>
                            <span class="lblText">On-Air Date</span>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblOnAirDate" runat="server" CssClass="lblText"></asp:Label>
                            <asp:TextBox ID="TxtOnAirDate" runat="server" CssClass="lblText"></asp:TextBox>
                            <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                Width="18px" />
                        </td>
                        <td>
                            <span class="lblText">Acceptance Date</span>
                        </td>
                        <td align="center">
                            <asp:Label ID="lblAcpDate" runat="server" CssClass="lblText"  Width="120px"></asp:Label>
                            <asp:TextBox ID="TxtAcpDate" runat="server" CssClass="lblText"></asp:TextBox>
                            <asp:ImageButton ID="ImgCalAcpDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                Width="18px" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="remarksPanel1" style="margin-top: 10px;">
                <div class="lblText" style="width: 100%;">
                    this Site Quality Acceptance Certificate is a legal note that Telkomsel has approved
                    the integration of the aforementioned type of work to the Telkomsel Network and
                    has accepted the KPI values.
                </div>
                <div class="lblText" style="width: 100%; margin-top: 5px;">
                    this certificate is automatically generated by e-BAST application upon the acceptance
                    of all person in charge. no signature is required for the following documents :
                </div>
            </div>
            <div id="checkDocumentPanel" style="margin-top: 10px; width: 100%; text-align: left; height:200px;">
                <asp:GridView ID="GrdDocPanel" runat="server" Width="150px" AutoGenerateColumns="False"
                    ShowHeader="false" GridLines="None" EmptyDataText="No Records Found" DataKeyNames="Doc_Id"
                    PageSize="2">
                    <AlternatingRowStyle CssClass="GrdDocPanelRows" />
                    <RowStyle CssClass="GrdDocPanelRows" />
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="20px">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkChecked" runat="server" />
                                <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblDocPath" runat="server" Text='<%#Eval("docpath") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblSWId" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocName" HeaderText="Document">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="GrdDocPanelPrint" runat="server" Width="150px" AutoGenerateColumns="False"
                    ShowHeader="false" GridLines="None" EmptyDataText="No Records Found" DataKeyNames="Doc_Id"
                    PageSize="2">
                    <AlternatingRowStyle CssClass="GrdDocPanelRows" />
                    <RowStyle CssClass="GrdDocPanelRows" />
                    <Columns>
                        <asp:TemplateField ControlStyle-Width="20px">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkChecked" runat="server" />
                                <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblDocPath" runat="server" Text='<%#Eval("docpath") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblSWId" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocName" HeaderText="Document">
                            <ItemStyle HorizontalAlign="Left" CssClass="GrdDocPanelRows" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <div style="margin-left: 20px; display: none;">
                    <asp:CheckBox ID="ChkDrive" runat="server" Text="Drive test report" CssClass="lblText" />
                </div>
                <div style="margin-top: 5px; margin-left: 20px; display: none;">
                    <asp:CheckBox ID="ChkKPI" runat="server" Text="KPI Statistic" CssClass="lblText" />
                </div>
                <div style="margin-top: 5px; margin-left: 20px; display: none;">
                    <asp:CheckBox ID="ChkFAlarm" runat="server" Text="Free Alarm" CssClass="lblText" />
                </div>
            </div>
            <div id="footerPanel" style="margin-top: 20px; height: 200px;">
                <div style="width: 100%; text-align: left; display: none;">
                    <span class="lblText">NOTE :</span><br />
                    <br />
                </div>
                <div style="height: 120px; display:none;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 18%;">
                                <span class="lblText">QC Submission Date</span>
                            </td>
                            <td style="width: 1%;">
                                :</td>
                            <td style="width: 80%;">
                                <asp:Label ID="LblSubmissionDate" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblText">Reason of Review Delay</span>
                            </td>
                            <td>
                                :</td>
                            <td>
                                <asp:TextBox ID="TxtReasonOfReviewDelay" runat="server" Style="width: 99%;"></asp:TextBox>
                                <asp:Label ID="LblReasonofReviewDelay" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="signaturePanel" style="text-align: center; width: 100%; margin-top: 10px;">
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
                                    <td id="DgSign" runat="server" style="height: 100px; text-align: left;">
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
                </div>
            </div>
            <div style="display: none;">
                <table cellspacing="0" cellpadding="0" width="100%" border="1">
                    <tr class="lblText">
                        <td style="width: 24%">
                            Company
                        </td>
                        <td style="width: 1%">
                            :
                        </td>
                        <td style="width: 25%">
                            Nokia Siemens Networks
                        </td>
                        <td rowspan="3" style="width: 25%">
                            Site Quality Acceptance Certificate
                        </td>
                        <td style="width: 25%" colspan="4">
                            Quality Certificate Form
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Prepare By
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblPreBy" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                        <td style="width: 12%">
                            Last Update
                        </td>
                        <td style="width: 1%">
                            :
                        </td>
                        <td colspan="2">
                            <input runat="server" id="txtLUpdate" class="textFieldStyle" readonly="readonly" />&nbsp;<asp:ImageButton
                                ID="btnDate" runat="server" Width="18px" ImageUrl="~/Images/calendar_icon.jpg"
                                Height="16px"></asp:ImageButton>
                            <b>
                                <asp:Label ID="lblLUpdate" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                    <tr class="lblText">
                        <td>
                            Author
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblAuthor" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                        <td>
                            Page : 1/1
                        </td>
                        <td colspan="2">
                            &nbsp;Version :
                        </td>
                        <td>
                            <asp:TextBox ID="txtVer" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                            <b>
                                <asp:Label ID="lblVer" runat="server" CssClass="lblText"></asp:Label></b>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="documentPanel" style="margin-top: 15px;">
            <table width="100%">
                <tr>
                    <td class="lblText">
                        <asp:GridView ID="grdDocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="DoThis" EmptyDataText="No Records Found" DataKeyNames="Doc_Id"
                            PageSize="2">
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        <asp:Label ID="LblDocName" runat="server" Text='<%#Eval("DocName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblSWId" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblDocid" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document" ControlStyle-Width="100px">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:TemplateField ControlStyle-Width="180px">
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
                                        <asp:TextBox TextMode="MultiLine" Rows="5" Columns="40" ID="txtremarks" runat="server" Visible="false"
                                            CssClass="textFieldStyle">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr class="lblText">
                    <td align="center">
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle"
                            Width="120px" OnClientClick="return CheckEmptyMandatory();" />
                        <asp:Button ID="btnSubmitReject" runat="server" Text="Submit" CssClass="buttonStyle" Width="120px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>