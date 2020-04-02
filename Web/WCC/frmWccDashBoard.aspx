<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccDashBoard.aspx.vb" Inherits="WCC_frmWccDashBoard" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics2.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
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
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="2" style="width: 66%">
            <tr>
                <td colspan="1" style="vertical-align: top; height: 21px; text-align: left">
                    &nbsp;<panelstyle borderstyle="Solid" borderwidth="1px"></panelstyle><padding bottom="5px"
                        left="5px" right="5px" top="5px"></padding><borderdetails colorbottom="0, 45, 150"
                            colorleft="158, 190, 245" colorright="0, 45, 150" colortop="0, 45, 150"></borderdetails>&nbsp;<panelstyle borderstyle="Solid" borderwidth="1px"></panelstyle><padding bottom="5px"
                        left="5px" right="5px" top="5px"></padding><borderdetails colorbottom="0, 45, 150"
                            colorleft="158, 190, 245" colorright="0, 45, 150" colortop="0, 45, 150"></borderdetails></td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 96px; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" ExpandEffect="None"
                        Height="222px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="421px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Agenda" TextAlignment="Left">
                            <ExpandedAppearance>
                                <Styles BackColor="InactiveCaptionText" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                    Font-Names="Verdana" Font-Size="9pt" ForeColor="#000040">
                                    <Padding Bottom="3px" Left="4px" Top="1px" />
                                    <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                        WidthBottom="0px" />
                                </Styles>
                            </ExpandedAppearance>
                            <HoverAppearance>
                                <Styles ForeColor="Blue">
                                </Styles>
                            </HoverAppearance>
                            <CollapsedAppearance>
                                <Styles Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                                </Styles>
                            </CollapsedAppearance>
                            <ExpansionIndicator Height="0px" Width="0px" />
                        </Header>
                        <Template>
                            <table style="width: 100%">
                                <tr>
                                    <td id="tdAgenda" runat="server" valign="top" style="width: 100%; height: 18px; font-weight: normal;
                                        font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left;">
                                    </td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                </td>
            </tr>
            <tr>
                <td colspan="1" style="vertical-align: top; height: 21px; text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="1" style="vertical-align: top; height: 21px; text-align: left">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
