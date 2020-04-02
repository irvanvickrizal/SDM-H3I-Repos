<%@ Page Language="VB" Trace="false" AutoEventWireup="false" CodeFile="frmTI_BAKP.aspx.vb"
    Inherits="BAUT_frmTI_BAKPFinal" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAKP form</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <script language="javascript" type="text/javascript">
    function WindowsClose() 
    { 
  
       window.opener.location.href = '../dashboard/DashboardpopupBakp.aspx?time='+(new Date()).getTime();
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
  
    function Calc()
         {          
                   var ValA = document.getElementById('txtPO').value;               
                   var ValB = document.getElementById('txtActual').value;  
                       if( ValA == '' && ValB != '') 
                           {                        
                            document.getElementById('txtDelta').value=''                                                              
                            alert("For Delta Value Please Fill The PO Value")                                                              
                           }
                        else if ( ValA != '' &&  ValB == '')
                           {    
                              document.getElementById('txtDelta').value=document.getElementById('txtPO').value                                                                                  
              
                           }   
                        else if ( ValA != '' &&  ValB != '')
                               {
                                   
                                    var ValC = ValA - ValB                                     
                                    if(ValC != 0)
                                     {
                                         document.getElementById('txtDelta').value=ValC                                    
                                     
                                     } 
                                    else
                                    {
                                        document.getElementById('txtDelta').value=ValC                                                                
                                    }                            
                  
                                }
                         else if( ValA == '' && ValB == '')
                           {                                
                                 document.getElementById('txtDelta').value=''                    
                           }                                                 
       }      
         
                        
   function Calc1()
     { 
     
               var ValA = document.getElementById('txtPOUSD').value;               
               var ValB = document.getElementById('txtActUSD').value;  
                   if( ValA == '' && ValB != '') 
                           {                         
                            document.getElementById('txtDelUSD').value=''                                                            
                            alert("For Delta Value Please Fill The PO Value")                                                              
                           }
                    else if ( ValA != '' &&  ValB == '')
                       {                               

                           document.getElementById('txtDelUSD').value=document.getElementById('txtPOUSD').value                                                                                       
          
                        }   
                    else if ( ValA != '' &&  ValB != '')
                       {                            
                            var ValC = ValA - ValB                                     
                            if(ValC != 0)
                             {
                                 document.getElementById('txtDelUSD').value=ValC                                 
                             } 
                            else
                            {
                                document.getElementById('txtDelUSD').value=ValC                                                                    
                            }                             
          
                        }
                     else if( ValA == '' && ValB == '')
                       {                                 
                           document.getElementById('txtDelUSD').value=''                    
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
     if (type=='Lnk')
       
         {
             alert('Not Allowed to upload this Document Now.');
         }
      window.location = '../PO/frmSiteDocUploadTree.aspx'
   }

    function transferValue(rw) {
        var csv = ""; 
        for (var i=1; i<=rw; i++) { 
          var valA = 0;
          if (document.getElementById('input' + i + 'a').value != "");
            var valA = document.getElementById('input' + i + 'a').value;
          var valB = 0;
          if (document.getElementById('input' + i + 'b').value != "");
            var valB = document.getElementById('input' + i + 'b').value;
          var valD = 0;
          if (document.getElementById('input' + i + 'd').value != "");
            var valD = document.getElementById('input' + i + 'd').value;
          var valE = 0;
          if (document.getElementById('input' + i + 'e').value != "");
            var valE = document.getElementById('input' + i + 'e').value;
          var valG = 0;
          if (document.getElementById('input' + i + 'g').value != "");
            var valG = document.getElementById('input' + i + 'g').value;
          var valH = 0;
          if (document.getElementById('input' + i + 'h').value != "");
            var valH = document.getElementById('input' + i + 'h').value;
          var valJ = valD - valG;
          var valK = valE - valH;
          if (i == 1) {
            csv = 
              valA + "!" + valB + "!" + "0!" + valD + "!" + valE + "!" + "0!" +
              valG + "!" + valH + "!" + "0!" + valJ + "!" + valK;
          }
          else {
            csv += "@" +
              valA + "!" + valB + "!" + "0!" + valD + "!" + valE + "!" + "0!" +
              valG + "!" + valH + "!" + "0!" + valJ + "!" + valK;
          }
        }
        document.getElementById('csv').value = csv;
    }
    
    function formatCurrency(num) 
    {
    
        num = num.toString().replace(/\$|\,/g,'');
        if(isNaN(num))
        num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num*100+0.50000000001);
        num = Math.floor(num/100).toString();
        for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
        num = num.substring(0,num.length-(4*i+3))+','+
        num.substring(num.length-(4*i+3));
        return (num);
    }
    
    function formatusdCurrency(num) 
    {
        num = num.toString().replace(/\$|\,/g,'');
        if(isNaN(num))
        num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num*100+0.50000000001);
        cents = num%100;
        num = Math.floor(num/100).toString();
        if(cents<10)
        cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
        num = num.substring(0,num.length-(4*i+3))+','+
        num.substring(num.length-(4*i+3));
        return (((sign)?'':'-') + num + '.' + cents);
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
        <input runat="server" id="hdnready4baut" type="hidden" />
        <input runat="server" id="hdnKeyVal" type="hidden" />
        <input runat="server" id="hdnDGBox" type="hidden" />
        <input runat="server" id="hdnScope" type="hidden" />
        <input runat="Server" id="hdndocId" type="hidden" />
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
                <td>
                    <div id="dvPrint" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left" valign="top" style="width: 20%">
                                                <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" /></td>
                                            <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                                                FINAL BERITA ACARA KEMAJUAN PROYEK
                                                <br />
                                                (“BAKP”)<br />
                                                or PROJECT PROGRESS CERTIFICATE
                                            </td>
                                            <td align="right" valign="top" style="width: 20%">
                                                <img src="http://www.telkomsel.ebast-portal.com/Images/logo_tsel.png" />&nbsp;<br />
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td style="width: 25%">
                                                Works
                                            </td>
                                            <td style="width: 12px">
                                                :
                                            </td>
                                            <td style="width: 320px">
                                                Civil-Mechanical-Electrical Works</td>
                                        </tr>
                                        <tr class="lblText">
                                            <td>
                                                Site ID/Site Name</td>
                                            <td style="width: 12px">
                                                :
                                            </td>
                                            <td class="lblText" style="height: 19px; width: 320px;">
                                                <asp:Label ID="lblSite" runat="server"></asp:Label>&nbsp;
                                                <asp:Label ID="lblsitenamepo" runat="server" Visible="False"></asp:Label>
                                                &nbsp;<asp:Label ID="lblsiteidpo" runat="server" Visible="False"></asp:Label></td>
                                        </tr>
                                        <tr class="lblText">
                                            <td style="height: 19px">
                                                Project ID
                                            </td>
                                            <td style="width: 12px; height: 19px;">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 320px; height: 19px;">
                                                <b>
                                                    <asp:Label ID="lblProj" runat="server" CssClass="lblText"></asp:Label></b>
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td align="center" colspan="3">
                                                Number :
                                                <asp:TextBox ID="txtbakprefno" runat="server" CssClass="textFieldStyle" Width="286px"
                                                    BorderColor="White"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3">
                                                On the date&nbsp;
                                                <input type="text" id="txtDate" runat="server" class="textFieldStyle" readonly="readonly">
                                                <asp:ImageButton ID="btnDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                                <b>
                                                    <asp:Label ID="lblDate" runat="server" CssClass="lblUText"></asp:Label></b>
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
                                        <tr class="lblText">
                                            <td colspan="3" style="height: 19px">
                                                By virtue of:
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3">
                                                1.&nbsp;&nbsp;2G BSS and 3G UTRAN Rollout Agreement Ref. No.
                                                <asp:TextBox ID="txtRef" runat="server" CssClass="textFieldStyle" Width="184px">008/BC/PROC-01/LOG/2009 </asp:TextBox><asp:Label
                                                    ID="lblRef" runat="server" CssClass="lblBText"></asp:Label>&nbsp;dated&nbsp;
                                                <input type="text" id="txtDated" runat="server" class="textFieldStyle" value="13-March-2009">&nbsp;<asp:ImageButton
                                                    ID="btnUTRAN" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /><asp:Label ID="lblDated" runat="server" CssClass="lblBText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td style="width: 30%">
                                                2.&nbsp; Purchase Order Ref No.
                                            </td>
                                            <td style="width: 12px">
                                                :
                                            </td>
                                            <td style="width: 320px">
                                                <asp:TextBox ID="txtPOServices" runat="server" BorderColor="White" CssClass="textFieldStyle"
                                                    Width="656px"></asp:TextBox>
                                                <asp:Label ID="lblPORef" class="lblBText" runat="server"></asp:Label>
                                                <asp:Label ID="lblPODated" class="lblBText" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr class="lblText">
                                            <td>
                                                3.&nbsp; Final Change Orders Ref No.
                                            </td>
                                            <td style="width: 12px">
                                                :
                                            </td>
                                            <td style="width: 320px">
                                                <asp:TextBox ID="txtCOR" runat="server" CssClass="textFieldStyle"></asp:TextBox><b><asp:Label
                                                    ID="lblCOR" runat="server" CssClass="lblText"></asp:Label>&nbsp;</b>dated on
                                                <input type="text" id="txtCORDated" runat="server" class="textFieldStyle" readonly="readonly">
                                                <asp:ImageButton ID="btnFCOR" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /><b><asp:Label ID="lblCORDated" runat="server" CssClass="lblText"></asp:Label>&nbsp;</b><span
                                                        style="color: #ff0033">(if any)</span>
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td>
                                                4.&nbsp; BAPKP Ref No.
                                            </td>
                                            <td style="width: 12px">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 320px">
                                                <asp:Label ID="lblBR" runat="server" CssClass="lblBText"></asp:Label>
                                                &nbsp;dated on&nbsp;
                                                <asp:Label ID="lblBRD" runat="server" CssClass="lblBText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap">
                                                <asp:TextBox ID="txtPOSoftware" runat="server" BorderColor="White" CssClass="textFieldStyle"
                                                    Width="656px" Visible="False"></asp:TextBox></td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3">
                                                Telkomsel and vendor hereby state the followings:
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3" style="height: 126px">
                                                <ol>
                                                    <li>Vendor has completed the Tower Foundation and deliver the Tower Material (the "Works")
                                                        at the location, in accordance with the Purchase Order referred to above;&nbsp;<br />
                                                    </li>
                                                    <li>Signing this certificate will not transfer the tittle of the Works from Vendor to
                                                        Telkomsel&nbsp;<br />
                                                    </li>
                                                    <li>Responsibility of the Works will remain with the Vendor until the signing of First
                                                        Hand Over Certificate (BAKP-1) document for the complete Civil-Mechanical-Electrical
                                                        Works as stated under the Purchase Order reffered above&nbsp;<br />
                                                    </li>
                                                    <li style="vertical-align: top">Total cost of the Works under this certificate will
                                                        be as follows :</li></ol>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap" style="height: 5px">
                                                <asp:Label ID="lblPONO" runat="server" CssClass="lblText" Visible="false"></asp:Label>
                                                <asp:Label ID="lblPONO2" runat="server" CssClass="lblText" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdTable" runat="server" align="center" class="lblText" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="Hcap" style="height: 6px">
                                                <asp:HiddenField ID="csv" runat="Server" />
                                                <asp:Button ID="btnPO" runat="server" Text="Get PO Detail" CssClass="buttonStyle"
                                                    Visible="False" Width="99px" />
                                                <asp:Button ID="btnAddRow" runat="server" Text="Add Row" CssClass="buttonStyle" Visible="False" />
                                                <asp:Button ID="btnAddRowComplete" runat="server" Text="Complete" CssClass="buttonStyle" /></td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr class="lblText">
                                            <td colspan="3">
                                                This certificate is made in two (2) original copies bearing sufficient stamp duties
                                                which shall have the same legal powers after being signed by their respective duly
                                                representatives</td>
                                        </tr>
                                        <tr>
                                            <td class="Hcap" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="3">
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
                                    <asp:TextBox ID="txtPOHardware" runat="server" BorderColor="White" CssClass="textFieldStyle"
                                        Width="656px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txtOAir" runat="server" CssClass="textFieldStyle" Visible="False"></asp:TextBox><asp:Label
                                        ID="lblOAir" runat="server" CssClass="lblText" Visible="False"></asp:Label><input
                                            type="text" runat="server" id="txtOAirD" class="textFieldStyle" readonly="readonly"
                                            visible="false"><br />
                                    <asp:ImageButton ID="btnOAir" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                        Width="18px" Visible="False" /><asp:Label ID="lblOAirD" runat="server" CssClass="lblText"
                                            Visible="False"></asp:Label><asp:TextBox ID="txtSACRNo" runat="server" CssClass="textFieldStyle"
                                                Visible="False"></asp:TextBox><asp:Label ID="lblSACR" runat="server" CssClass="lblText"
                                                    Visible="False"></asp:Label><input type="text" id="txtSACRD" runat="server" class="textFieldStyle"
                                                        readonly="readonly" visible="false"><asp:ImageButton ID="btnSAC" runat="server" Height="16px"
                                                            ImageUrl="~/Images/calendar_icon.jpg" Width="18px" Visible="False" /><asp:Label ID="lblSACRD"
                                                                runat="server" CssClass="lblText" Visible="False"></asp:Label><asp:TextBox ID="txtICRNo"
                                                                    runat="server" CssClass="textFieldStyle" Visible="False"></asp:TextBox><input type="text"
                                                                        runat="server" id="txtICRD" class="textFieldStyle" readonly="readonly" visible="false"><asp:ImageButton
                                                                            ID="btnICR" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                                            Width="18px" Visible="False" /><asp:Label ID="lblICRD" runat="server" CssClass="lblText"
                                                                                Visible="False"></asp:Label><asp:Label ID="lblICR" runat="server" CssClass="lblText"
                                                                                    Visible="False"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                                    EmptyDataText="No Records Found" DataKeyNames="Doc_Id" PageSize="2">
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Total ">
                                            <ItemStyle HorizontalAlign="Right" />
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
                                                <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                    OnSelectedIndexChanged="DOThis" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Approve</asp:ListItem>
                                                    <asp:ListItem Value="1">Reject</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox TextMode="MultiLine" Rows="5" Columns="40" ID="txtremarks" runat="server"
                                                    Visible="false" CssClass="textFieldStyle">
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnswid" runat="server" />
                            </td>
                        </tr>
                        <tr align="center" class="lblText">
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
