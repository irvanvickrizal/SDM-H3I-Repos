<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboard.aspx.vb" Inherits="frmDashboard" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>

    <script language="javascript" type="text/javascript">
            
            var vardes = 0;
            
            function popwindowDashBoard(id){
                if (id == 1) {
                    window.open('DashBoard/dashboardpopupbast.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
                else 
                    if (id == 2) {
                        window.open('DashBoard/dashboardpopupbaut.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                    }
                    else 
                        if (id == 3) {
                            window.open('DashBoard/DocumentWrokFlow.aspx', 'welcome', 'width=500,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                        }
                        else 
                            if (id == 4) {//Pending upload docs
                                window.open('DashBoard/PendingUploadDocument.aspx?id=1', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                            }
                            else 
                                if (id == 5) {
                                    window.open('DashBoard/frmDocApproved.aspx', 'welcome', 'width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                }
                                else 
                                    if (id == 6) {//sls time
                                        window.open('DashBoard/PendingUploadDocument.aspx?id=0', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                    }
                                    else 
                                        if (id == 7) {//Rejected docs
                                            window.open('DashBoard/PendingUploadDocument.aspx?id=2', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                        }
                                        else 
                                            if (id == 8) {//Missing WPId
                                                window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=0', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                            }
                                            else 
                                                if (id == 9) {//Missing EPM
                                                    window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=1', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                }
                                                else 
                                                    if (id == 10) {//Duplicate Sites
                                                        window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=2', 'welcome', 'width=350,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                    }
                                                    else 
                                                        if (id == 11) {//EBAST Done
                                                            window.open('DashBoard/EBastDoneDetails.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                        }
                                                        else {
                                                            window.open('DashBoard/frmsitestatus.aspx', 'welcome', 'width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                                                        }
            }
            
            function popSitesDetails(id){
                if (id == 3) {
                    window.open('DashBoard/dashboardpopupbast.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                }
                else 
                    if (id == 8) {
                        window.open('DashBoard/dashboardpopupbaut.aspx', 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                    }
                    else {
                        window.open('DashBoard/EBastDoneDetails.aspx?id=' + id, 'welcome', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
                    }
            }
            
            function popwindowMissingSites(pono){
                window.open('PO/frmPOMissingInfo.aspx?Type=1&DB=0&pono=' + pono, 'welcome', 'width=550,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            
            function popwindowDashBoardpending(id){
                window.open('DashBoard/frmDocApproved.aspx?id=' + id, 'welcome', 'width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            
            function DashBoard(id){
                window.open('DashBoard/dashboard.aspx?pono=' + id, 'welcome', 'width=750,height=325,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function SiteDetails(){
                window.open('DashBoard/SiteDetails.aspx', 'welcome', 'width=400,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function SiteStatus(){
                if (vardes == 0) {
                    for (intcount = 0; intcount < document.getElementById("hdTotal").value; intcount++) {
                        document.getElementById("TRDesc" + intcount).style.display = "none";
                        document.getElementById("TRAsc" + intcount).style.display = "";
                    }
                    document.getElementById("idUpArrow").src = "Images/arrow2.png";
                    vardes = 1;
                }
                else {
                    for (intcount = 0; intcount < document.getElementById("hdTotal").value; intcount++) {
                        document.getElementById("TRDesc" + intcount).style.display = "";
                        document.getElementById("TRAsc" + intcount).style.display = "none";
                    }
                    document.getElementById("idUpArrow").src = "Images/arrow1.png";
                    vardes = 0;
                }
            }
            
            function OverAllStatus(pono, id){
                window.open('DashBoard/frmDashBoardDetails.aspx?P=' + pono + '&id=' + id, 'welcome', 'width=475,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
            
            function popSitesSLA(id){
                window.open('DashBoard/SLADetails.aspx?id=' + id, 'welcomeSLA', 'width=700,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
            }
    </script>

</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="2" style="width: 66%">
            <tr>
                <td colspan="2" style="vertical-align: top; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel4" runat="server" BackColor="White" ExpandEffect="None"
                        Height="195px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="795px" BorderStyle="None">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Monthly Milestone Achievements" TextAlignment="Left">
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
                            <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Height="100%"
                                Width="100%">
                                <table style="width: 100%" border="0" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td style="width: 100px; height: 36px;">
                                            <table style="width: 504px">
                                                <tr>
                                                    <td style="width: 30px; height: 19px; font-weight: bold; font-size: 8pt; vertical-align: middle;
                                                        font-family: Verdana; text-align: left;">
                                                        Month :
                                                    </td>
                                                    <td style="width: 100px; height: 19px">
                                                        <asp:DropDownList ID="ddMonth" runat="server" Width="90px" AutoPostBack="True">
                                                            <asp:ListItem Value="0">
                                                                    --Select--
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="1">
                                                                    January
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="2">
                                                                    February
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="3">
                                                                    March
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="4">
                                                                    April
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="5">
                                                                    May
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="6">
                                                                    June
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="7">
                                                                    July
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="8">
                                                                    August
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="9">
                                                                    September
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="10">
                                                                    October
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="11">
                                                                    November
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="12">
                                                                    December
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 30px; height: 19px; font-weight: bold; font-size: 8pt; vertical-align: middle;
                                                        font-family: Verdana; text-align: left;">
                                                        Year :
                                                    </td>
                                                    <td style="width: 70px; height: 19px">
                                                        <asp:DropDownList ID="ddYear" runat="server" Width="75px" AutoPostBack="True">
                                                            <asp:ListItem>
                                                                    2009
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2010
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2011
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2012
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2013
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2014
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2015
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2016
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2017
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2018
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2019
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                                    2020
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 108px; height: 19px; font-weight: bold; font-size: 8pt; vertical-align: middle;
                                                        font-family: Verdana; text-align: left;">
                                                        EPMData As of:
                                                    </td>
                                                    <td style="width: 100px; height: 19px; text-align: left">
                                                        <asp:Label ID="lblepmdate" runat="server" Text="Label" Font-Size="X-Small" Font-Bold="true"
                                                            Width="110px">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100%">
                                            <igtbl:UltraWebGrid ID="UWD_Month" runat="server" Height="115px" Width="100%">
                                                <Bands>
                                                    <igtbl:UltraGridBand>
                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                        </AddNewRow>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                    AllowSortingDefault="Yes" BorderCollapseDefault="Separate" Name="UWDxMonth" RowHeightDefault="20px"
                                                    RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                    StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00">
                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                        Font-Size="8.25pt" Height="115px" Width="100%">
                                                    </FrameStyle>
                                                    <Pager MinimumPagesForDisplay="2">
                                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </PagerStyle>
                                                    </Pager>
                                                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                    </EditCellStyleDefault>
                                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </FooterStyleDefault>
                                                    <HeaderStyleDefault BackColor="MediumPurple" BorderStyle="Solid" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" Wrap="True" ForeColor="White">
                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                    </HeaderStyleDefault>
                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                        <Padding Left="3px" />
                                                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                    </RowStyleDefault>
                                                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                    </GroupByRowStyleDefault>
                                                    <GroupByBox Hidden="True">
                                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                        </BoxStyle>
                                                    </GroupByBox>
                                                    <AddNewBox Hidden="False">
                                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                        </BoxStyle>
                                                    </AddNewBox>
                                                    <ActivationObject BorderColor="" BorderWidth="">
                                                    </ActivationObject>
                                                    <FilterOptionsDefault>
                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                            Font-Size="11px" Height="300px" Width="200px">
                                                            <Padding Left="2px" />
                                                        </FilterDropDownStyle>
                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                        </FilterHighlightRowStyle>
                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                            Font-Size="11px">
                                                            <Padding Left="2px" />
                                                        </FilterOperandDropDownStyle>
                                                    </FilterOptionsDefault>
                                                    <RowAlternateStyleDefault BackColor="LavenderBlush">
                                                    </RowAlternateStyleDefault>
                                                </DisplayLayout>
                                            </igtbl:UltraWebGrid>
                                        </td>
                                    </tr>
                                </table>
                            </igmisc:WebAsyncRefreshPanel>
                        </Template>
                    </igmisc:WebPanel>
                </td>
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
                                    <td style="width: 50%">
                                        <b>Process-TI:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink
                                            ID="lnkall" runat="server" NavigateUrl="~/DashBoard/frmagendaallscopes.aspx">View All Scopes</asp:HyperLink></td>
                                </tr>
                                <tr>
                                    <td id="tdAgenda" runat="server" valign="top" style="width: 100%; height: 18px; font-weight: normal;
                                        font-size: 8pt; vertical-align: top; font-family: Verdana; text-align: left;">
                                    </td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                </td>
                <td style="vertical-align: top; width: 100px; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" ExpandEffect="None"
                        Height="222px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="372px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Site Acceptance Status" TextAlignment="Left">
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
                                    <td style="font-family: Arial; background-color: InactiveCaptionText; font-weight: bold;
                                        font-size: 10pt; width: 50%" colspan="3">
                                        BAST</td>
                                    <td style="width: 50%; background-color: InactiveCaptionText;" align="center">
                                        Oldest Task</td>
                                </tr>
                                <tr>
                                    <td class="dashboard" valign="top" style="width: 3%">
                                        <asp:Image ID="Image1" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                            Width="14px" /></td>
                                    <td valign="top" class="dashboard" style="width: 57%">
                                        <div runat="server" id="Tdbastdone" style="font-weight: normal; font-size: 8pt; vertical-align: top;
                                            font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="TdbastdoneNO" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="Tdbastdonedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%; height: 21px;">
                                        <div runat="server" id="tdTbastsignature" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%; height: 21px;">
                                        <div runat="server" id="tdTbastsignatureno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%; height: 21px;" valign="top">
                                        <div runat="server" id="tdTbastsignaturedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdNbastsignature" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdNbastsignatureNO" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdNbastsignaturedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="Tdready4bast" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="Tdready4bastno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="Tdready4bastdays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdbastprocessing" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdbastprocessingno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdbastprocessingdays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial; background-color: InactiveCaptionText; font-weight: bold;
                                        font-size: 10pt; width: 60%" colspan="3">
                                        BAUT</td>
                                    <td style="width: 40%; background-color: InactiveCaptionText;" align="center">
                                        Oldest Task</td>
                                </tr>
                                <tr>
                                    <td class="dashboard" valign="top" style="width: 3%">
                                        <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/Images/tick.jpg"
                                            Width="14px" /></td>
                                    <td valign="top" class="dashboard" style="width: 57%">
                                        <div runat="server" id="tdbautdone" style="font-weight: normal; font-size: 8pt; vertical-align: top;
                                            font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdbautdoneno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdbautdonedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdTbautsiganture" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdTbautsigantureno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdTbautsiganturedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdNbautsiganture" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdNbautsigantureNo" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdNbautsiganturedays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdready4baut" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdready4bautno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 30%" valign="top">
                                        <div runat="server" id="tdready4bautdays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="dashboard" colspan="2" style="width: 60%">
                                        <div runat="server" id="tdbautprocessing" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" valign="top" style="width: 10%">
                                        <div runat="server" id="tdbautprocessingno" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: left">
                                        </div>
                                    </td>
                                    <td class="dashboard" style="width: 10%" valign="top">
                                        <div runat="server" id="tdbautprocessingdays" style="font-weight: normal; font-size: 8pt;
                                            vertical-align: top; font-family: Verdana; text-align: center">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </Template>
                    </igmisc:WebPanel>
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel5" runat="server" BackColor="White" ExpandEffect="None"
                        Height="195px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="795px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Over All Status" TextAlignment="Left">
                            <ExpandedAppearance>
                                <Styles BackColor="InactiveCaptionText" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                    Font-Names="Verdana" Font-Size="10pt" ForeColor="#000040">
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
                            <asp:ScriptManager ID="ScriptManager2" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <igtbl:UltraWebGrid ID="UWD_Over_All_Status" runat="server" Height="151px" Width="100%">
                                        <Bands>
                                            <igtbl:UltraGridBand>
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                            </igtbl:UltraGridBand>
                                        </Bands>
                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                            AllowSortingDefault="Yes" BorderCollapseDefault="Separate" Name="ctl10xUWDxOverxAllxStatus"
                                            RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                            Version="4.00">
                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                Font-Size="8.25pt" Height="151px" Width="100%">
                                            </FrameStyle>
                                            <RowAlternateStyleDefault BackColor="LavenderBlush">
                                            </RowAlternateStyleDefault>
                                            <Pager MinimumPagesForDisplay="2">
                                                <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </PagerStyle>
                                            </Pager>
                                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                            </EditCellStyleDefault>
                                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                            </FooterStyleDefault>
                                            <HeaderStyleDefault BackColor="MediumPurple" BorderStyle="Solid" HorizontalAlign="Center"
                                                VerticalAlign="Middle" Wrap="True">
                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                            </HeaderStyleDefault>
                                            <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                <Padding Left="3px" />
                                                <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                            </RowStyleDefault>
                                            <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                            </GroupByRowStyleDefault>
                                            <GroupByBox Hidden="True">
                                                <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                </BoxStyle>
                                            </GroupByBox>
                                            <AddNewBox Hidden="False">
                                                <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                </BoxStyle>
                                            </AddNewBox>
                                            <ActivationObject BorderColor="" BorderWidth="">
                                            </ActivationObject>
                                            <FilterOptionsDefault>
                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px" Height="300px" Width="200px">
                                                    <Padding Left="2px" />
                                                </FilterDropDownStyle>
                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                </FilterHighlightRowStyle>
                                                <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                    BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                    Font-Size="11px">
                                                    <Padding Left="2px" />
                                                </FilterOperandDropDownStyle>
                                            </FilterOptionsDefault>
                                        </DisplayLayout>
                                    </igtbl:UltraWebGrid>
                                    <ig:WebDialogWindow ID="WDM_EPM_Main" runat="server" Height="283px" InitialLocation="Centered"
                                        Width="723px" WindowState="Hidden" Style="vertical-align: top; text-align: center"
                                        Modal="True">
                                        <Header CaptionText="Overall Status By Region">
                                        </Header>
                                        <ContentPane>
                                            <Template>
                                                &nbsp;
                                                <asp:Panel ID="Panel1" runat="server" Height="230px" ScrollBars="Auto" Style="vertical-align: top;
                                                    text-align: center" Width="703px">
                                                    <table style="width: 96%">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 100%; text-align: left">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <table border="0" cellpadding="0" cellspacing="1" style="width: 137px">
                                                                            <tr>
                                                                                <td style="width: 100px">
                                                                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export" />
                                                                                </td>
                                                                                <td style="width: 100px">
                                                                                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <igtbl:UltraWebGrid ID="UWG_EPM_Detail_PO" runat="server" Height="151px" Width="100%"
                                                                            OnClick="UWG_EPM_Detail_PO_Click" OnInitializeLayout="UWG_EPM_Detail_PO_InitializeLayout">
                                                                            <Bands>
                                                                                <igtbl:UltraGridBand>
                                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                                    </AddNewRow>
                                                                                </igtbl:UltraGridBand>
                                                                            </Bands>
                                                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                                AllowSortingDefault="Yes" BorderCollapseDefault="Separate" Name="ctl182xUWGxEPMxDetailxPO"
                                                                                RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                                StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                                Version="4.00">
                                                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                                    Font-Size="8.25pt" Height="151px" Width="100%">
                                                                                </FrameStyle>
                                                                                <RowAlternateStyleDefault BackColor="LavenderBlush">
                                                                                </RowAlternateStyleDefault>
                                                                                <Pager MinimumPagesForDisplay="2">
                                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                                    </PagerStyle>
                                                                                </Pager>
                                                                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                                </EditCellStyleDefault>
                                                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                                </FooterStyleDefault>
                                                                                <HeaderStyleDefault BackColor="MediumPurple" BorderStyle="Solid" HorizontalAlign="Center"
                                                                                    VerticalAlign="Middle" Wrap="True" CustomRules="font-weight:normal;">
                                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                                </HeaderStyleDefault>
                                                                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                                                    <Padding Left="3px" />
                                                                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                                                </RowStyleDefault>
                                                                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                                                </GroupByRowStyleDefault>
                                                                                <GroupByBox Hidden="True">
                                                                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                                    </BoxStyle>
                                                                                </GroupByBox>
                                                                                <AddNewBox Hidden="False">
                                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                                    </BoxStyle>
                                                                                </AddNewBox>
                                                                                <ActivationObject BorderColor="" BorderWidth="">
                                                                                </ActivationObject>
                                                                                <FilterOptionsDefault>
                                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                        Font-Size="11px" Height="300px" Width="200px">
                                                                                        <Padding Left="2px" />
                                                                                    </FilterDropDownStyle>
                                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                                    </FilterHighlightRowStyle>
                                                                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                        Font-Size="11px">
                                                                                        <Padding Left="2px" />
                                                                                    </FilterOperandDropDownStyle>
                                                                                </FilterOptionsDefault>
                                                                            </DisplayLayout>
                                                                        </igtbl:UltraWebGrid>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 14px">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </Template>
                                        </ContentPane>
                                    </ig:WebDialogWindow>
                                    <ig:WebDialogWindow ID="WDM_EPM_Detail_PopUp" runat="server" Height="382px" InitialLocation="Centered"
                                        Modal="True" Width="775px" WindowState="Hidden">
                                        <Header CaptionText="Overall Status by PO">
                                        </Header>
                                        <ContentPane>
                                            <Template>
                                                <asp:Panel ID="Panel2" runat="server" Height="100%" ScrollBars="Auto" Style="vertical-align: top;
                                                    text-align: center" Width="100%">
                                                    <table style="width: 97%; height: 317px">
                                                        <tr>
                                                            <td style="vertical-align: top; width: 100%; height: 241px; text-align: left">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <table style="width: 121px">
                                                                            <tr>
                                                                                <td style="width: 100px; height: 26px">
                                                                                    <asp:Button ID="btnExport1" runat="server" OnClick="btnExport1_Click" Text="Export" />
                                                                                </td>
                                                                                <td style="width: 100px; height: 26px">
                                                                                    <asp:Button ID="btnClose1" runat="server" OnClick="btnClose1_Click" Text="Close" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                &nbsp;
                                                                <igtbl:UltraWebGrid ID="WGD_EPM_Detail" runat="server" Height="237px" Width="100%"
                                                                    OnInitializeLayout="WGD_EPM_Detail_InitializeLayout">
                                                                    <Bands>
                                                                        <igtbl:UltraGridBand>
                                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                                            </AddNewRow>
                                                                        </igtbl:UltraGridBand>
                                                                    </Bands>
                                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                        AllowSortingDefault="Yes" BorderCollapseDefault="Separate" Name="ctl10xWGDxEPMxDetail"
                                                                        RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                        Version="4.00">
                                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                            Font-Size="8.25pt" Height="237px" Width="100%">
                                                                        </FrameStyle>
                                                                        <RowAlternateStyleDefault BackColor="LavenderBlush">
                                                                        </RowAlternateStyleDefault>
                                                                        <Pager MinimumPagesForDisplay="2">
                                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </PagerStyle>
                                                                        </Pager>
                                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                        </EditCellStyleDefault>
                                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                        </FooterStyleDefault>
                                                                        <HeaderStyleDefault BackColor="MediumPurple" BorderStyle="Solid" HorizontalAlign="Center"
                                                                            VerticalAlign="Middle" Wrap="True">
                                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                        </HeaderStyleDefault>
                                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                                                            <Padding Left="3px" />
                                                                            <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                                        </RowStyleDefault>
                                                                        <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                                        </GroupByRowStyleDefault>
                                                                        <GroupByBox Hidden="True">
                                                                            <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                            </BoxStyle>
                                                                        </GroupByBox>
                                                                        <AddNewBox Hidden="False">
                                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                            </BoxStyle>
                                                                        </AddNewBox>
                                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                                        </ActivationObject>
                                                                        <FilterOptionsDefault>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Height="300px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                                BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px">
                                                                                <Padding Left="2px" />
                                                                            </FilterOperandDropDownStyle>
                                                                        </FilterOptionsDefault>
                                                                    </DisplayLayout>
                                                                </igtbl:UltraWebGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </Template>
                                        </ContentPane>
                                    </ig:WebDialogWindow>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="UWD_Over_All_Status" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="WDM_EPM_Main" EventName="Load" />
                                </Triggers>
                            </asp:UpdatePanel>
                            &nbsp;&nbsp;
                            <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
                            </igtblexp:UltraWebGridExcelExporter>
                        </Template>
                    </igmisc:WebPanel>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 96px; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WP_Graph_PO" runat="server" BackColor="White" ExpandEffect="None"
                        Height="300px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="404px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Graph Report - Gap Analysis by PO" TextAlignment="Left">
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
                            <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server" Height="114px"
                                Width="100%">
                                <table border="0" cellpadding="0" cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <table style="width: 260px">
                                                <tr>
                                                    <td style="width: 100px; font-weight: bold; font-size: 8pt; vertical-align: middle;
                                                        font-family: Verdana; text-align: left;">
                                                        Select PO :
                                                    </td>
                                                    <td style="width: 100px">
                                                        <asp:DropDownList ID="ddlPO" runat="server" Width="172px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <igchart:UltraChart ID="chartTI" runat="server" BackgroundImageFileName="" BorderColor="Black"
                                                BorderWidth="1px" ChartType="BarChart" Height="250px" Version="9.1" Width="94%">
                                                <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" ModelStyle="CustomLinear">
                                                </ColorModel>
                                                <Axis>
                                                    <PE ElementType="None" Fill="Cornsilk" />
                                                    <X Extent="39" LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="True">
                                                        <MajorGridLines AlphaLevel="255" Color="Black" DrawStyle="Dot" Thickness="1" Visible="True" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                                            Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                                            <SeriesLabels Font="Verdana, 7pt" FormatString="" HorizontalAlign="Far" Orientation="VerticalLeftFacing"
                                                                VerticalAlign="Center">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto">
                                                            </Layout>
                                                        </Labels>
                                                    </X>
                                                    <Y Extent="96" LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                                                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                                            Orientation="Horizontal" VerticalAlign="Center">
                                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Center" Orientation="VerticalRightFacing"
                                                                OrientationAngle="20" VerticalAlign="Center" Visible="False">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto" Padding="10">
                                                            </Layout>
                                                        </Labels>
                                                    </Y>
                                                    <Y2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                                            Visible="True" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                                                            Orientation="Horizontal" VerticalAlign="Center" Visible="False">
                                                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" Orientation="VerticalLeftFacing"
                                                                VerticalAlign="Center">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto">
                                                            </Layout>
                                                        </Labels>
                                                    </Y2>
                                                    <X2 LineThickness="1" TickmarkInterval="40" TickmarkStyle="Smart" Visible="False">
                                                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                                            Visible="True" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                                                            Orientation="VerticalLeftFacing" VerticalAlign="Center" Visible="False">
                                                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" HorizontalAlign="Far"
                                                                Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto">
                                                            </Layout>
                                                        </Labels>
                                                    </X2>
                                                    <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                                            Visible="True" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                                                            Orientation="Horizontal" VerticalAlign="Center" Visible="False">
                                                            <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                                                VerticalAlign="Center">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto">
                                                            </Layout>
                                                        </Labels>
                                                    </Z>
                                                    <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                                                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                                                            Visible="True" />
                                                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                                                            Visible="False" />
                                                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                                                            Orientation="Horizontal" VerticalAlign="Center" Visible="False">
                                                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="VerticalLeftFacing"
                                                                VerticalAlign="Center">
                                                                <Layout Behavior="Auto">
                                                                </Layout>
                                                            </SeriesLabels>
                                                            <Layout Behavior="Auto">
                                                            </Layout>
                                                        </Labels>
                                                    </Z2>
                                                </Axis>
                                                <Effects>
                                                    <Effects>
                                                        <igchartprop:GradientEffect>
                                                        </igchartprop:GradientEffect>
                                                    </Effects>
                                                </Effects>
                                                <Data ZeroAligned="True">
                                                </Data>
                                                <BarChart>
                                                    <ChartText>
                                                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" HorizontalAlign="Far"
                                                            ItemFormatString="&lt;DATA_VALUE:0&gt;" Row="-2" Visible="True">
                                                        </igchartprop:ChartTextAppearance>
                                                    </ChartText>
                                                </BarChart>
                                                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" FormatString="&lt;DATA_VALUE:#&gt;" />
                                            </igchart:UltraChart>
                                        </td>
                                    </tr>
                                </table>
                            </igmisc:WebAsyncRefreshPanel>
                        </Template>
                    </igmisc:WebPanel>
                </td>
                <td style="vertical-align: top; width: 100px; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel3" runat="server" BackColor="White" ExpandEffect="None"
                        Height="300px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="372px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Document Graph - Future" TextAlignment="Left">
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
                        </Template>
                    </igmisc:WebPanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top; height: 21px; text-align: left">
                    <igmisc:WebPanel ID="WebPanel6" runat="server" BackColor="White" ExpandEffect="None"
                        Height="195px" Style="vertical-align: top; text-align: left" StyleSetName=""
                        Width="795px">
                        <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                            <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                            <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                                ColorTop="0, 45, 150" />
                        </PanelStyle>
                        <Header Text="Site List Document" TextAlignment="Left">
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
                                <Styles Font-Bold="True" Font-Names="Verdana" Font-Size="10pt" ForeColor="#000040">
                                </Styles>
                            </CollapsedAppearance>
                            <ExpansionIndicator Height="0px" Width="0px" />
                        </Header>
                        <Template>
                            <igtbl:UltraWebGrid ID="uwd_Site_List_doc" runat="server" Height="151px" Width="100%">
                                <Bands>
                                    <igtbl:UltraGridBand>
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
                                    </igtbl:UltraGridBand>
                                </Bands>
                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                    AllowSortingDefault="Yes" BorderCollapseDefault="Separate" Name="uwdxSitexListxdoc"
                                    RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                    Version="4.00">
                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                        Font-Size="8.25pt" Height="151px" Width="100%">
                                    </FrameStyle>
                                    <RowAlternateStyleDefault BackColor="LavenderBlush">
                                    </RowAlternateStyleDefault>
                                    <Pager MinimumPagesForDisplay="2">
                                        <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </PagerStyle>
                                    </Pager>
                                    <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                    </EditCellStyleDefault>
                                    <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </FooterStyleDefault>
                                    <HeaderStyleDefault BackColor="MediumPurple" BorderStyle="Solid" HorizontalAlign="Center"
                                        VerticalAlign="Middle" Wrap="True">
                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                    </HeaderStyleDefault>
                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                                        <Padding Left="3px" />
                                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                    </RowStyleDefault>
                                    <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                    </GroupByRowStyleDefault>
                                    <GroupByBox Hidden="True">
                                        <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                        </BoxStyle>
                                    </GroupByBox>
                                    <AddNewBox Hidden="False">
                                        <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                        </BoxStyle>
                                    </AddNewBox>
                                    <ActivationObject BorderColor="" BorderWidth="">
                                    </ActivationObject>
                                    <FilterOptionsDefault>
                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                            Font-Size="11px" Height="300px" Width="200px">
                                            <Padding Left="2px" />
                                        </FilterDropDownStyle>
                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                        </FilterHighlightRowStyle>
                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                            Font-Size="11px">
                                            <Padding Left="2px" />
                                        </FilterOperandDropDownStyle>
                                    </FilterOptionsDefault>
                                </DisplayLayout>
                            </igtbl:UltraWebGrid>
                        </Template>
                    </igmisc:WebPanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
