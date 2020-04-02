<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_SICEQP.aspx.vb" Inherits="BAUT_frmTI_SICEQP"
    EnableEventValidation="False" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIC EQP</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <script type="text/javascript" language="javascript">  
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
  
  function Calc() { 
      var ValA = document.getElementById('txtIDRPO').value;
      var ValB = document.getElementById('txtIDRActual').value;
      
      if (ValA == '' && ValB != '') {
        alert("For Delta Value Please Fill The PO Value")
      }
      else if (ValA != '' &&  ValB == '') {
        document.getElementById('txtIDRDelta').value = document.getElementById('txtIDRPO').value 
      }   
      else if (ValA != '' &&  ValB != '') {
        var ValC = ValA - ValB
        
        if (ValC != 0) {
          document.getElementById('txtIDRDelta').value = ValC
        } 
        else {
          document.getElementById('txtIDRDelta').value = ValC
        }                              
      }
      else if( ValA == '' && ValB == '') {
        document.getElementById('lblPO').innerText =''
        document.getElementById('lblImplt').innerText ='' 
        document.getElementById('lblDelta').innerText =''
      }                                                 
    }

    function Calc0() { 
      var ValA0 = document.getElementById('txtUSDPO').value;
      var ValB0 = document.getElementById('txtUSDActual').value;
      
      if (ValA0 == '' && ValB0 != '') {
        alert("For Delta Value Please Fill The PO Value")
      }
      else if (ValA0 != '' &&  ValB0 == '') {
        document.getElementById('txtUSDDelta').value = document.getElementById('txtUSDPO').value 
      }   
      else if (ValA0 != '' &&  ValB0 != '') {
        var ValC = ValA0 - ValB0
        
        if (ValC != 0) {
          document.getElementById('txtUSDDelta').value = ValC
        } 
        else {
          document.getElementById('txtUSDDelta').value = ValC
        }                              
      }
      else if( ValA0 == '' && ValB0 == '') {
        document.getElementById('lblPO0').innerText =''
        document.getElementById('lblImplt0').innerText ='' 
        document.getElementById('lblDelta0').innerText =''
      }                                                 
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <div id="dvPrint" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" valign="top" style="width: 20%">
                                                    <img src="../Images/nsn-logo.gif" height="36px" width="104px" />
                                                </td>
                                                <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                                    SITE INTEGRATION CERTIFICATE ("SIC")
                                                </td>
                                                <td align="right" valign="top" style="width: 20%">
                                                    <img src="../Images/logo_tsel.png" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td class="lblText">
                                                    Works
                                                </td>
                                                <td style="width: 1%">
                                                    :
                                                </td>
                                                <td style="width: 74%">
                                                    Supply of Equipment
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td class="lblText">
                                                    Site ID/Site Name
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td class="lblText">
                                                    <asp:Label ID="lblSite" CssClass="lblBText" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td class="lblText">
                                                    Project ID
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td class="lblText">
                                                    <asp:TextBox ID="txtProj" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                                    <asp:Label ID="lblProj" runat="server" CssClass="lblBText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3" align="center">
                                                    Number :
                                                    <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    On the date
                                                    <input id="txtDate" runat="Server" type="text" class="textFieldStyle" readonly="readOnly" />
                                                    <asp:ImageButton ID="btnDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                        Width="18px" />
                                                    <asp:Label ID="lblDate" CssClass="lblBText" runat="server"></asp:Label>
                                                    , we the undersigned:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:DataList ID="dtList" Width="100%" runat="server">
                                                        <ItemTemplate>
                                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                <tr>
                                                                    <td class="lblText">
                                                                        Name
                                                                    </td>
                                                                    <td style="width: 1%">
                                                                        :
                                                                    </td>
                                                                    <td style="width: 74%" class="lblBText">
                                                                        <%#Container.DataItem("name")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lblText">
                                                                        Title
                                                                    </td>
                                                                    <td style="width: 1%">
                                                                        :
                                                                    </td>
                                                                    <td style="width: 74%" class="lblBText">
                                                                        <%#Container.DataItem("roledesc")%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="lblText" colspan="3">
                                                                        <%#Container.DataItem("NewDescription")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    By virtue of:
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    1.&nbsp;&nbsp;2G BSS and 3G UTRAN Rollout Agreement Ref. No.
                                                    <asp:TextBox ID="txtRef" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                                    <asp:Label ID="lblRef" runat="server" CssClass="lblBText"></asp:Label>
                                                    ,&nbsp;dated on
                                                    <asp:TextBox ID="txtAgreeDate" runat="server" CssClass="textFieldStyle" MaxLength="10"
                                                        ReadOnly="true"></asp:TextBox>
                                                    <asp:Label ID="lblAgreeDate" runat="server" CssClass="lblBText"></asp:Label>
                                                    &nbsp;<asp:ImageButton ID="btnCal1" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                        Width="18px" />
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    2.&nbsp;&nbsp;Purchase Order Ref. No.
                                                    <asp:Label ID="lblPORef" runat="server" CssClass="lblBText"></asp:Label>
                                                    , dated on
                                                    <asp:Label ID="lblPODated" runat="server" CssClass="lblBText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    3.&nbsp;&nbsp;Final Change Orders Ref. No.
                                                    <asp:TextBox ID="txtCOR" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                                    <b>
                                                        <asp:Label ID="lblCOR" runat="server" CssClass="lblBText"></asp:Label></b>,<b>&nbsp;</b>dated
                                                    on
                                                    <asp:TextBox ID="txtCORDated" runat="server" CssClass="textFieldStyle" ReadOnly="true"></asp:TextBox>
                                                    <b>
                                                        <asp:Label ID="lblCORDated" runat="server" CssClass="lblBText"></asp:Label></b>
                                                    &nbsp;(if any)
                                                    <asp:ImageButton ID="btnCal2" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                        Width="18px" />
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    4.&nbsp;&nbsp;BAUT Ref. No.
                                                    <asp:Label ID="lblBNo" runat="server" CssClass="lblBText"></asp:Label>
                                                    , dated on
                                                    <asp:Label ID="lblBNDated" runat="server" CssClass="lblBText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    Telkomsel and Vendor hereby state the followings:
                                                </td>
                                            </tr>
                                            <tr class="lblText">
                                                <td colspan="3">
                                                    <ol>
                                                        <li>Vendor has transferred the works and the title thereof to Telkomsel at the location,
                                                            in accordance with the Purchase Order referred to above;</li><li>Telkomsel has accepted the works and the title thereof satisfactorily, provided
                                                            that:
                                                            <ol type="a">
                                                                <li>Completion of the Works [has / has no] delay,</li><li>
                                                                    <table>
                                                                        <tr class="lblText">
                                                                            <td>
                                                                                - For Equipment Hardware: 12 (twelve) months as of the date of this certificate.
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="lblText">
                                                                            <td>
                                                                                - For Equipment Software: 3 (three) months as of the date of this certificate.
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </li>
                                                            </ol>
                                                        </li>
                                                        <li>
                                                            <table>
                                                                <tr class="lblText">
                                                                    <td>
                                                                        Any insufficiency or defect to the Works encountered during such period due to workmanship
                                                                        or quality thereof shall become the responsibility of Vendor to rectify or replace
                                                                        such insufficiency or defect.
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </li>
                                                        <li>Total cost of the Equipment under this certificate will be as follows:</li></ol>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left">
                                                    <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                                        <tr class="lblBText">
                                                            <td align="center" rowspan="2" style="width: 25%">
                                                                PO Number
                                                            </td>
                                                            <td align="center" colspan="4">
                                                                Value
                                                            </td>
                                                        </tr>
                                                        <tr class="lblBText">
                                                            <td style="width: 25%" align="center">
                                                                Curr.
                                                            </td>
                                                            <td style="width: 25%" align="center">
                                                                As per PO
                                                            </td>
                                                            <td style="width: 25%" align="center">
                                                                Actual
                                                            </td>
                                                            <td style="width: 25%" align="center">
                                                                Delta
                                                            </td>
                                                        </tr>
                                                        <tr class="lblText">
                                                            <td rowspan="2">
                                                                <asp:Label ID="lblPONO" runat="server" CssClass="lblText"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 25%">
                                                                &nbsp; USD
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtIDRPO" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="javascript:return Calc();" /><b><asp:Label
                                                                        ID="lblPO" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtIDRActual" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="javascript:return Calc();" /><b><asp:Label
                                                                        ID="lblImplt" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtIDRDelta" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" /><b><asp:Label
                                                                        ID="lblDelta" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                        <tr class="lblText">
                                                            <td style="width: 25%">
                                                                &nbsp; IDR
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtUSDPO" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="javascript:return Calc0();" /><b><asp:Label
                                                                        ID="lblPO0" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtUSDActual" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" onblur="javascript:return Calc0();" /><b><asp:Label
                                                                        ID="lblImplt0" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <input type="text" id="txtUSDDelta" runat="server" class="textFieldStyle" maxlength="10"
                                                                    onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" /><b><asp:Label
                                                                        ID="lblDelta0" runat="server" CssClass="lblText"></asp:Label></b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="lblText">
                                                    This certificate is made in two (2) original copies bearing sufficient stamp duties
                                                    which shall have the same legal powers after being signed and/or approved by their
                                                    respective duly representatives.
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                         <asp:DataList ID="DLDigitalSign" runat="server" Width="100%" RepeatColumns="3">
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" width="200px" border="0">
                                            <tr>
                                                <td class="lblBText" style="border-width:1px;border-color: Black;
                                            border-collapse: collapse; border-style: solid;vertical-align: top; text-align: center;">
                                                    <%#Container.DataItem("Description")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td id="DgSign" runat="server" style="height: 100px;">
                                                    <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="../Images/dgsign.JPG" />
                                                    <asp:HiddenField runat="Server" ID="hdXCoordinate" />
                                                    <asp:HiddenField runat="Server" ID="hdYCoordinate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblBText"  style="border-width:1px;border-color: Black;
                                            border-collapse: collapse; border-style: solid;vertical-align: top; text-align: center;">
                                                    <%#Container.DataItem("name")%><br />
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
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                                        EmptyDataText="No Records Found" DataKeyNames="Doc_Id" PageSize="2">
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
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
                                                    <asp:TextBox TextMode="MultiLine" Rows="5" Columns="40" ID="txtremarks" runat="server"
                                                        Visible="false" CssClass="textFieldStyle">
                                                    </asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr align="center" class="lblText">
                                <td align="center">
                                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblText" colspan="2">
                                    <input runat="Server" id="hdnSiteno" type="hidden" />
                                    <input runat="Server" id="hdnAdmin" type="hidden" />
                                    <input runat="Server" id="hdnAdminRole" type="hidden" />
                                    <input runat="Server" id="hdnsiteid" type="hidden" />
                                    <input runat="Server" id="hdnversion" type="hidden" />
                                    <input runat="Server" id="hdnWfId" type="hidden" />
                                    <input runat="Server" id="hdndocId" type="hidden" />
                                    <input runat="server" id="hdnScope" type="hidden" />
                                    <input runat="server" id="hdnDGBox" type="hidden" />
                                    <input runat="server" id="hdnKeyVal" type="hidden" />
                                    <input runat="server" id="hdnready4baut" type="hidden" />
                                    <input type="hidden" runat="server" id="HDDgSignTotal" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
