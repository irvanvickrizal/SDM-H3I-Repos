<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteReport.aspx.vb" Inherits="RPT_frmSiteReport" %>

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

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

  <script language="javascript" type="text/javascript">
    function viewUser()
    {
        var aa;
        aa = window.showModalDialog('../USR/frmUserList.aspx?SelMode=true','','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');        
        if(typeof aa != 'undefined')
        {
            document.getElementById('hdnSSId').value = aa
            var bb = aa.split('####')        
            document.getElementById('hdnSupId').value = bb[0];
            document.getElementById('txtSSName').value = bb[1];
            //document.getElementById('hdnUserType').value = bb[2];    
            document.getElementById('hdName').value= bb[1]; 
             
        }
    }    
    function checkIsEmpty()
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtSSName").value) == false)
        {
            msg = msg + "Supervisor should not be Empty\n";
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
                <td class="pageTitle" colspan="3">Supervisor Site Report</td>
            </tr>            
            <tr id="trSup" runat="Server">
                <td class="lblTitle">Select Supervisor</td>
                <td style="width:1%">:</td>
                <td>
                   <input type="text" runat="server" id="txtSSName" class="textFieldStyle" disabled="disabled" />&nbsp;
            <a id="A1" runat="server" href="#" onclick="viewUser();" class="ASmall">Select Supervisor</a></td>
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
        <input type="hidden" runat="server" id="hdnSSId" />
        <input type="hidden" runat="server" id="hdName" />
        <input type="hidden" runat="server" id="hdnSupId" value="0" />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasSearchButton="False" HasViewList="False" DisplayGroupTree="False" />
    </div>
    </form>
</body>
</html>
