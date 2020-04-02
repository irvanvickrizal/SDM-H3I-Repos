<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardC.aspx.vb" Inherits="newRPT_frmdashboardC" %>

<%@ Register Src="DashBoard/DashBoardAdmin.ascx" TagName="DashBoardAdmin" TagPrefix="uc1" %>
<%@ Register Src="DashBoard/GraphicalReport.ascx" TagName="GraphicalReport" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="Refresh" content="1200" />
    <link href="CSS/styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>

    <script language="javascript" type="text/javascript">
    
    var vardes=0;

    function popwindowDashBoard(id)
{
    if(id==1)
    {
     window.open('DashBoard/dashboardpopupbast.aspx','welcome','width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
    else if(id==2)
    {
    
     window.open('DashBoard/dashboardpopupbaut.aspx','welcome','width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
     
    }
    else if(id==3)
    {
     window.open('DashBoard/DocumentWrokFlow.aspx','welcome','width=500,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
     else if(id==4)//Pending upload docs
    {
        window.open('DashBoard/PendingUploadDocument.aspx?id=1','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
     else if(id==5)
    {
     window.open('DashBoard/frmDocApproved.aspx','welcome','width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }    
    else if(id==6) //sls time
    {
     window.open('DashBoard/PendingUploadDocument.aspx?id=0','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
    else if(id==7) //Rejected docs
    {
     window.open('DashBoard/PendingUploadDocument.aspx?id=2','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
     else if(id==8) //Missing WPId
    {
     window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=0','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
    else if(id==9) //Missing EPM
    {
     window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=1','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
    else if(id==10) //Duplicate Sites
    {
        window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=2','welcome','width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
     else if(id==11) //EBAST Done
    {
        window.open('DashBoard/EBastDoneDetails.aspx','welcome','width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }
     else
    {
     window.open('DashBoard/frmsitestatus.aspx','welcome','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    }

}

function popSitesDetails(id)
{
        window.open('DashBoard/EBastDoneDetails.aspx?id='+id,'welcome','width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');

    
}
function popwindowMissingSites(pono)
{
     window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=0&pono='+pono,'welcome','width=550,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
    
}
function popwindowDashBoardpending(id)
{
    
     window.open('DashBoard/frmDocApproved.aspx?id='+id,'welcome','width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
}
function DashBoard(id)
{
     window.open('DashBoard/dashboard.aspx?pono='+id,'welcome','width=750,height=325,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

}
function SiteDetails()
{
     window.open('DashBoard/SiteDetails.aspx','welcome','width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

}
function SiteStatus()
{
    
   if(vardes==0)
   {
        for(intcount=0;intcount<document.getElementById("hdTotal").value;intcount++)
        {
           document.getElementById("TRDesc"+intcount).style.display="none";
           document.getElementById("TRAsc"+intcount).style.display="";
        }
       document.getElementById("idUpArrow").src="Images/arrow2.png"
       vardes=1;
   }
   else
   {
        for(intcount=0;intcount<document.getElementById("hdTotal").value;intcount++)
        {
           document.getElementById("TRDesc"+intcount).style.display="";
           document.getElementById("TRAsc"+intcount).style.display="none";
       }
       document.getElementById("idUpArrow").src="Images/arrow1.png"
        vardes=0;
   }

}
function OverAllStatus(pono,id)
{      
    window.open('DashBoard/frmDashBoardDetails.aspx?P='+ pono +'&id='+id,'welcome','width=475,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');

}
function popSitesSLA(id)
{
        window.open('DashBoard/SLADetails.aspx?id='+id,'welcomeSLA','width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');

    
}
    </script>

</head>
<body topmargin="0">
    <form id="form1" runat="server">
        <div style="width: 100%; vertical-align: top">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td>
                        <uc1:DashBoardAdmin ID="DashBoardAdmin1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td valign="top" style="border: 0px; background-image: url(Images/agenda1px.jpg);
                                    background-repeat: repeat-x; height: 33px; width: 40%">
                                    <img alt="" src="Images/agendabarimg.jpg" /></td>
                                <td style="width:1%">
                                <%--</td>
                                <td style="border: 0px; background-image: url(Images/sitestatus1px.jpg); background-repeat: repeat-x;
                                    height: 33px; width: 59%">
                                    <img alt="" src="Images/sla.png" />
                                </td>--%>
                            </tr>
                            <tr>
                                <td runat="server" id="tdAgenda" valign="top">
                                </td>
                                <td valign="top">
                                    &nbsp;</td>
                                <td valign="top" >
                                    <%--<table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                        <tr class="dashboard">
                                            <td style="width: 50%">
                                                Sites nearing Contract SLA's</td>
                                            <td style="width: 50%">
                                                Sites exceeded Contract SLA's</td>
                                        </tr>
                                        <tr><td colspan="2" class="hgap"></td></tr>
                                        <tr>
                                            <td valign="top" style="height: 45px">
                                            <div runat="server" id="TdStatusNew" class="dashboardNew">
                                            </div>
                                            </td>
                                            <td valign="top" style="height: 45px">
                                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                                    <tr class="dashboard">
                                                        <td>Complete
                                                        </td>
                                                        <td>Not Complete
                                                        </td>
                                                    </tr>
                                                    <tr class="dashboardNew">
                                                        <td runat="server" id="TdComplete">
                                                        </td>
                                                        <td runat="server" id="TdNotComplete">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>--%>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="3">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="background-image: url(Images/gra1px.jpg); background-repeat: repeat-x">
                                                <img alt="" src="Images/RBAST.jpg" /></td>
                                            <td width="5px">
                                            </td>
                                            <td style="background-image: url(Images/gra1px.jpg); background-repeat: repeat-x">
                                                <img alt="" src="Images/grareport.jpg" /></td>
                                            <td width="5px">
                                            </td>
                                            <td style="background-image: url(Images/gra1px.jpg); background-repeat: repeat-x">
                                                <img alt="" src="Images/sitestatus.jpg" /></td>
                                        </tr>
                                        <tr>
                                            <td runat="server" id="TdBaut" valign="top" class="dashboard">
                                            </td>
                                            <td width="5px">
                                            </td>
                                            <td align="center">
                                                <div class="hgap">
                                                </div>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="dashboard/DashboardImage.ashx" />
                                            </td>
                                            <td width="5px">
                                            </td>
                                            <td valign="top" class="dashboard">
                                                <div runat="server" id="TdStatus">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
