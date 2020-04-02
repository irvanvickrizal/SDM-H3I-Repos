<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteCompletionReport.aspx.vb" Inherits="frmSiteCompletionReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>View Site Process Report</title>
<link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
<script language="javascript" type="text/javascript">
   function checkIsEmpty()
    {
        var msg = "";
        var bool = dtCompare();
          if (bool == false)
            {
                msg = msg + "From Date Must Be Less Than To Date";                
            }
            if (msg != "")
            {
            alert("Selected Dates Information \n\n" + msg);
            return false;
            }
        
          else
           {
            return true;
            }    
      }    
    function dtCompare()
    {
        if ((document.getElementById('txtSiteCreateF').value != '') && (document.getElementById('txtSiteCreateT').value != ''))
        {
              var dt1 = document.getElementById('txtSiteCreateF').value;
              var dt2 = document.getElementById('txtSiteCreateT').value;
              var s1 = dt1.split('/');
              s1 = s1[2] + s1[1] + s1[0];
              var s2 = dt2.split('/');
              s2 = s2[2] + s2[1] + s2[0]; 
//         if ((document.getElementById('txtDocChkF').value != '') && (document.getElementById('txtDocChkT').value != ''))
//        {
//              var dt1 = document.getElementById('txtDocChkF').value;
//              var dt2 = document.getElementById('txtDocChkT').value;
//              var s1 = dt1.split('/');
//              s1 = s1[2] + s1[1] + s1[0];
//              var s2 = dt2.split('/');
//              s2 = s2[2] + s2[1] + s2[0];   
//         if ((document.getElementById('txtSiteIntF').value != '') && (document.getElementById('txtSiteIntT').value != ''))
//        {
//              var dt1 = document.getElementById('txtSiteIntF').value;
//              var dt2 = document.getElementById('txtSiteIntT').value;
//              var s1 = dt1.split('/');
//              s1 = s1[2] + s1[1] + s1[0];
//              var s2 = dt2.split('/');
////              s2 = s2[2] + s2[1] + s2[0];                      
              if (s1 > s2) 
              {  
                return false;
              }
              else
              {
                return true;
              }
        }
     }  
    </script>  

</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr id="title" runat="server" >
                <td class="pageTitle" colspan="4" >Site Activity Monitoring Report</td>
            </tr>            
            <tr id="porpt" runat="server" class="lblTitle" >
                <td style="width:23%">Po No<font style="color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width:1%" align="center">:</td>
                <td colspan="2" align="left">
                    <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList></td>
                </tr>
            <tr id="siterpt" runat="Server" class="lblTitle">
            <td >Site</td>
            <td style="width: 1%" align="center">:</td>
            <td colspan="2"  align="left"><asp:DropDownList ID="ddlSite" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
             <tr id="siteselect" runat="Server" class="lblTitle">
                 <td >Site Creation Date
                 </td>
                 <td style="width: 1%" align="center">
                     :</td>
                 <td align="left" style="width:18%">From&nbsp;<input id="txtSiteCreateF" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteCreateFDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px"/></td>               
                 <td align="left">To&nbsp;<input id="txtSiteCreateT" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteCreateTDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />                 
                 </td>                 
             </tr>
             <tr id="sitedocselect" runat="Server" class="lblTitle">
                 <td>
                     Check List Date
                 </td>
                 <td style="width: 1%" align="center">
                     :</td>
                 <td align="left">From&nbsp;<input id="txtDocChkF" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteChkFDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px"/></td>               
                 <td align="left">To&nbsp;<input id="txtDocChkT" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteChkTDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />                 
                 </td>
             </tr>
             <tr id="siteintselect" runat="Server" class="lblTitle">
                 <td >Site Integration Date
                 </td>
                 <td style="width: 1%" align="center">
                     :</td>
                 <td align="left">From&nbsp;<input id="txtSiteIntF" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteIntFDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px"/></td>               
                 <td align="left">To&nbsp;<input id="txtSiteIntT" runat="server" class="textFieldStyle"
                         maxlength="10" type="text" readonly="readOnly" />&nbsp;<asp:ImageButton ID="btnSiteIntTDate" runat="server"
                             Height="16px" ImageUrl="~/Images/calendar_icon.jpg" Width="18px" />                 
                 </td>
             </tr>
       <tr runat="Server">
           <td class="lblTitle">
           </td>
           <td style="width: 1%">
           </td>
           <td colspan="2">
            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="buttonStyle" />
            <asp:Button ID="btnBack" runat="server" Text="Back To Report Selection" CssClass="buttonStyle" Width="33%" />
           </td>
       </tr>        
       </table>      
    </div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" CssFilename="~/CSS/styles.css" HasCrystalLogo="False" HasToggleGroupTreeButton="False" DisplayGroupTree="False" HasSearchButton="False" />
    </form>
</body>
</html>
