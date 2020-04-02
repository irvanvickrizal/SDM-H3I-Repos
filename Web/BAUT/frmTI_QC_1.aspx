<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_QC.aspx.vb" Inherits="BAUT_frmTI_QC"
    EnableEventValidation="false" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td>
                <div id="dvPrint" runat="server">
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td align="left" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                            </td>
                            <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                QC CERTIFICATE
                            </td>
                            <td align="right" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" />
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td colspan="3" class="Hcap">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="lblText">
                                <b style="border-right: black 2px solid; border-top: black 2px solid; border-left: black 2px solid;
                                    border-bottom: black 2px solid">RECAPITULATION</b><br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" class="lblText">
                                Reference No:<br />
                                <b>
                                    <asp:Label ID="lblPO" runat="server" CssClass="lblText" Visible="false"></asp:Label>
                                </b>
                                <br />
                            </td>
                            <td class="lblText" style="width: 75%" colspan="2">
                                <asp:Label ID="lblRefNO" runat="server" CssClass="lblText"></asp:Label><br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table width="100%" cellpadding="0" cellspacing="0" border="1">
                                    <tr class="lblText">
                                        <td style="width: 25%">
                                            Site ID
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblSiteID" runat="server" CssClass="lblText"></asp:Label>
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
                                            <asp:Label ID="lblSiteName" runat="server" CssClass="lblText"></asp:Label>
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
                                            <asp:Label ID="lblBand" runat="server" CssClass="lblText"></asp:Label>
                                        </td>
                                        <td>
                                            Existing config
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExtConfig" runat="server" CssClass="lblText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="lblText">
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 50px">
                                <table cellpadding="0" cellspacing="0" border="1" width="100%">
                                    <tr class="lblText">
                                        <td style="width: 14%">
                                            Integration Date
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td style="width: 18%">
                                            <asp:Label ID="lblIntDate" runat="server" CssClass="lblText"></asp:Label>
                                        </td>
                                        <td style="width: 14%">
                                            On-Air Date
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td style="width: 18%">
                                            <asp:Label ID="lblOnAirDate" runat="server" CssClass="lblText"></asp:Label>
                                        </td>
                                        <td style="width: 14%">
                                            Acceptance Date
                                        </td>
                                        <td style="width: 1%">
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAcpDate" runat="server" CssClass="lblText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td colspan="3">
                                This quality certificate is a legal note that Telkomsel's SQA department in regional
                                office has approved the integration quality of mentioned type of work to the Telkomsel
                                network and accepting reached KPI integration values.<br />
                                <br />
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td colspan="3">
                                The quality certificate is printed in three identical copies and its content is
                                approved by Nokia Project, Telkomsel SQA in regional office.
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td align="right">
                                <asp:CheckBox ID="ChkDrive" runat="server" />
                            </td>
                            <td colspan="2" class="lblText" style="width: 75%">
                                Drive test report
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td style="width: 25%" align="right">
                                <asp:CheckBox ID="ChkKPI" runat="server" />
                            </td>
                            <td colspan="2">
                                KPI Statistic
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td colspan="1" style="width: 25%" align="right">
                                <asp:CheckBox ID="ChkFAlarm" runat="server" />
                            </td>
                            <td colspan="2">
                                Free Alarm
                            </td>
                        </tr>
                        <tr class="lblText">
                            <td colspan="1">
                                NOTE
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
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
                        <tr>
                            <td colspan="3">
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
                                            <input runat="server" id="txtLUpdate" CssClass="textFieldStyle" readonly="readonly" />&nbsp;<asp:ImageButton
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="lblText">
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
                                    <tr class="lblText">
                                        <td align="center">
                                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="buttonStyle" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
