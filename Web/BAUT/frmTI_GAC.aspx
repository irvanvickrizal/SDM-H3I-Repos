<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_GAC.aspx.vb" Inherits="BAUT_frmTI_GAC"
    EnableEventValidation="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GAC</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

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
    </script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

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
        .lblCText
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000000;
            text-align: center;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td style="font-family: Verdana">
                <div id="dvPrint" runat="server" style="width: 100%; text-align: center;">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                            </td>
                            <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                GOODS ARRIVAL CERTIFICATE ("GAC")
                            </td>
                            <td align="right" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="3" class="Hcap">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" class="lblText">
                                Type of Works
                            </td>
                            <td style="width: 1%">
                                :
                            </td>
                            <td class="lblText" style="width: 74%">
                                Supply of BSS Equipment
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText">
                                Site ID/Name
                            </td>
                            <td>
                                :
                            </td>
                            <td class="lblBText">
                                <asp:Label ID="lblSite" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText">
                                Project ID
                            </td>
                            <td>
                                :
                            </td>
                            <td class="lblBText">
                                <input id="txtProjID" runat="Server" type="text" class="textFieldStyle" /><asp:Label
                                    ID="lblProjID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td class="lblCText" colspan="3">
                                Number :
                                <asp:Label ID="lblNumber" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Hcap" colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                On the date
                                <input id="txtDate" runat="Server" type="text" class="textFieldStyle" readonly="readOnly" />
                                <asp:ImageButton ID="btnDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" />
                                <asp:Label ID="lblDate" runat="server" CssClass="lblBText"></asp:Label>, we the
                                undersigned:
                            </td>
                        </tr>
                        <tr>
                            <td class="Hcap" colspan="3">
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
                                                    <%#Container.DataItem("name")%>
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
                        <tr>
                            <td colspan="3" class="lblText">
                                By virtue of:
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                1.&nbsp; 2G BSS and 3G UTRAN Rollout Agreement Ref. No.
                                <input id="txtAgreementRefNo" runat="server" class="textFieldStyle" maxlength="10"
                                    onblur="javascript:return Calc();" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                    type="text" /><asp:Label ID="lblAgreementRefNo" runat="server" CssClass="lblBText"></asp:Label>&nbsp;
                                dated on
                                <input id="txtAgreementRefDt" runat="server" class="textFieldStyle" maxlength="10"
                                    onblur="javascript:return Calc();" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                    type="text" readonly="readOnly" />
                                <asp:ImageButton ID="btnAgreementDt" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" />
                                <asp:Label ID="lblAgreementRefDt" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                2. &nbsp;Purchase Order Ref. No.
                                <asp:Label ID="lblPORefNO" runat="server" CssClass="lblBText"></asp:Label>, dated
                                on
                                <asp:Label ID="lblPODate" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                3.&nbsp; Shipment Invoice Ref. No.
                                <input id="txtShipmentRefNo" runat="server" class="textFieldStyle" maxlength="10"
                                    onblur="javascript:return Calc();" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                    type="text" /><asp:Label ID="lblShipmentRefNo" runat="server" CssClass="lblBText"></asp:Label>,
                                dated on
                                <input id="txtShipmentDt" runat="server" class="textFieldStyle" maxlength="10" onblur="javascript:return Calc();"
                                    type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnShipmentDt" runat="server"
                                        Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
                                <asp:Label ID="lblShipmentDt" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                4.&nbsp; Packing Slip Ref. No.
                                <input id="txtPackingRefNo" runat="server" class="textFieldStyle" maxlength="10"
                                    onblur="javascript:return Calc();" type="text" /><asp:Label ID="lblPackingRefNo"
                                        runat="server" CssClass="lblBText"></asp:Label>, dated on
                                <input id="txtPackingDt" runat="server" class="textFieldStyle" maxlength="10" onblur="javascript:return Calc();"
                                    type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnPackingDt" runat="server"
                                        Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
                                <asp:Label ID="lblPackingDt" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText" colspan="3">
                                5.&nbsp; Airway Bill Ref. No.
                                <input id="txtAirwayRefNo" runat="server" class="textFieldStyle" maxlength="10" onblur="javascript:return Calc();"
                                    type="text" /><asp:Label ID="lblAirwayRefNo" runat="server" CssClass="lblBText"></asp:Label>,
                                dated on
                                <input id="txtAirwayDt" runat="server" class="textFieldStyle" maxlength="10" onblur="javascript:return Calc();"
                                    type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnAirwayDt" runat="server"
                                        Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />
                                <asp:Label ID="lblAirwayDt" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="Hcap" colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="lblText" style="font-size: 8pt; height: 32px;">
                                Telkomsel and Vendor hereby state the followings:<br />
                                1.&nbsp; Vendor has conduct a standard Factory Tests and Quality Control procedure
                                for the Equipment stated under this certificate.<br />
                                2.&nbsp; Vendor has complete the delivery the Eqipment to the Vendor Warehouse satisfactorily
                                180 (one hundred and eighty) calender days before the date of&nbsp;<br />
                                &nbsp; &nbsp;&nbsp; this certificate.<br />
                                3.&nbsp; Payment of the Eqipment against this certificate will transfer the title
                                of the Eqipment from Vendor to Telkomsel.<br />
                                4.&nbsp; Total cost of the Equipment under this certificate will be as follows:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="Hcap">
                            </td>
                        </tr>
                        <tr style="font-family: Verdana">
                            <td colspan="3" style="height: 20px" align="left">
                                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="lblBText">
                                        <td align="center" colspan="2" rowspan="2" style="width: 25%; background-color: #ccccff;">
                                            PO Number
                                        </td>
                                        <td align="center" style="background-color: #ccccff;" colspan="4">
                                            Value
                                        </td>
                                    </tr>
                                    <tr class="lblBText">
                                        <td align="center" style="width: 17%; background-color: #ccccff;">
                                            Curr.
                                        </td>
                                        <td align="center" style="width: 19%; background-color: #ccccff;">
                                            As Per PO.
                                        </td>
                                        <td align="center" style="width: 19%; background-color: #ccccff;">
                                            GAC Value
                                        </td>
                                        <td align="center" style="width: 25%; background-color: #ccccff;">
                                        </td>
                                    </tr>
                                    <tr class="lblText">
                                        <td colspan="2" align="center" rowspan="2">
                                            <asp:Label ID="lblPONO" runat="server" CssClass="lblText"></asp:Label><b></b>
                                        </td>
                                        <td align="center" style="width: 17%">
                                            <b>IDR</b>
                                        </td>
                                        <td align="center" style="width: 19%">
                                            <input id="IDRPO" runat="server" class="textFieldStyle" maxlength="10" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" type="text" /><b><asp:Label
                                                    ID="lblPOIDR" runat="server" CssClass="lblText"></asp:Label></b>
                                        </td>
                                        <td align="center" style="width: 19%">
                                            <input id="IDRGAC" runat="server" class="textFieldStyle" maxlength="10" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"
                                                type="text" /><b><asp:Label ID="lblPOGAC" runat="server" CssClass="lblText"></asp:Label></b>
                                        </td>
                                        <td align="center" style="width: 25%">
                                        </td>
                                    </tr>
                                    <tr class="lblText">
                                        <td align="center" style="width: 17%">
                                            <b>USD</b>
                                        </td>
                                        <td align="center" style="width: 19%">
                                            <b>
                                                <input id="USDPO" runat="server" class="textFieldStyle" maxlength="10" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" type="text" />
                                                <asp:Label ID="lblPOValueTot" runat="server" CssClass="lblText"></asp:Label></b>
                                        </td>
                                        <td align="center" style="width: 19%">
                                            <b>
                                                <input id="USDGAC" runat="server" class="textFieldStyle" maxlength="10" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" type="text" />
                                                <asp:Label ID="lblGACValueTot" runat="server" CssClass="lblText"></asp:Label></b>
                                        </td>
                                        <td align="center" style="width: 15%">
                                        </td>
                                    </tr>
                                </table>
                                <em><span style="font-size: 7pt">Detailed Equipment quantity as specification as attached</span></em>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="lblText">
                                <br />
                                This certificate is made in two (2) original copies bearing sufficient stamp duties
                                which shall have the same legal powers after being signed and/or approved by their
                                respective duly representatives.
                            </td>
                        </tr>
                        <tr style="font-family: Verdana">
                            <td colspan="3" class="Hcap">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="font-family: Verdana">
                            <td align="left" colspan="3">
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
                                                    <asp:ImageButton ID="ImgPostion" runat="server" ImageUrl="http://www.telkomsel.nsnebast.com/Images/dgsign.JPG" />
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
                <table width="100%">
                    <tr>
                        <td class="lblText">
                            <asp:GridView ID="grddocuments" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_Id"
                                EmptyDataText="No Records Found" PageSize="2" Width="100%">
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
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
