<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocReport.aspx.vb" Inherits="RPT_frmDocReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    
    </head>
    <script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("ddlPO"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "PO should be select\n";
        }
        if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }   
    }    
  </script>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="3">
                    PO Site Status Report</td>
            </tr>            
            <tr id="trPono" runat="Server">
                <td class="lblTitle">Po No</td>
                <td style="width:1%">:</td>
                <td><asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList></td>
                </tr>
       <tr id="trRpt" runat="Server">
           <td>
           </td>
           <td style="width: 1%">
           </td>
           <td>
            <asp:Button ID="btnReport" runat="server" Text="View Report" CssClass="buttonStyle" Width="99px"/>            

           </td>
       </tr>
        <tr id="trBack" runat="Server">
            <td>
            </td>
            <td style="width: 1%">
            </td>
            <td>
                <asp:Button ID="btnBack" runat="server" CssClass="buttonStyle" Text="Back To Report"
                    Width="117px" /></td>
        </tr>
        </table>  
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasSearchButton="False" HasViewList="False" DisplayGroupTree="False"/>
    </div>
    </form>
</body>
</html>
