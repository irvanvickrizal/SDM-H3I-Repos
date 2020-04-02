<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DashBoardAdmin.ascx.vb"
    Inherits="Include_DashBoardAdmin" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.Web.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<asp:ScriptManager ID="ScriptManager_Infra" runat="server">
</asp:ScriptManager>
<table style="width: 67%">
    <tr>
        <td style="width: 100%">
            <igmisc:WebPanel ID="WebPanel2" runat="server" BackColor="White" ExpandEffect="None"
                Height="204px" Style="vertical-align: top; text-align: left;" Width="850px" StyleSetName="">
                <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                    <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                    <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                        ColorTop="0, 45, 150" />
                </PanelStyle>
                <Header TextAlignment="Left">
                    <ExpandedAppearance>
                        <Styles BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Verdana" Font-Size="10pt" BackColor="InactiveCaptionText" Font-Bold="True">
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
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4">
                                        <table>
                                            <tr>
                                                <td style="width: 100px">
                                                    Month</td>
                                                <td style="width: 100px">
                                <asp:DropDownList ID="ddMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddMonth_SelectedIndexChanged">
                                   <asp:ListItem Value="0">--Select--</asp:ListItem>
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList></td>
                                                <td style="width: 100px">
                                                    Year</td>
                                                <td style="width: 100px">
                                <asp:DropDownList ID="ddYear" runat="server" Width="107px">
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        <tr>
                            <td colspan="4">
                                <igtbl:UltraWebGrid ID="UWD_Month" runat="server" Height="131px" Width="100%" OnInitializeLayout="UWD_Month_InitializeLayout">
                                    <Bands>
                                        <igtbl:UltraGridBand>
                                            <AddNewRow View="NotSet" Visible="NotSet">
                                            </AddNewRow>
                                        </igtbl:UltraGridBand>
                                    </Bands>
                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                    AllowSortingDefault="Yes" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                    Name="ctl00xctl58xUWDxMonth" RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                    Version="4.00">
                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                        Font-Size="8.25pt" Height="131px" Width="100%">
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
                                        <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
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
                                        <RowAlternateStyleDefault BackColor="InactiveCaptionText">
                                        </RowAlternateStyleDefault>
                                    </DisplayLayout>
                                </igtbl:UltraWebGrid></td>
                        </tr>
                    </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddMonth" />
                            <asp:AsyncPostBackTrigger ControlID="WebPanel2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </Template>
            </igmisc:WebPanel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <igmisc:WebPanel ID="WebPanel1" runat="server" BackColor="White" ExpandEffect="None"
                Height="195px" Style="vertical-align: top; text-align: left; width: 850px;" Width="132%" StyleSetName="">
                <PanelStyle BorderStyle="Solid" BorderWidth="1px">
                    <Padding Bottom="5px" Left="5px" Right="5px" Top="5px" />
                    <BorderDetails ColorBottom="0, 45, 150" ColorLeft="158, 190, 245" ColorRight="0, 45, 150"
                        ColorTop="0, 45, 150" />
                </PanelStyle>
                <Header Text="Over All Status" TextAlignment="Left">
                    <ExpandedAppearance>
                        <Styles BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Verdana" Font-Size="10pt" BackColor="InactiveCaptionText" Font-Bold="True">
                            <Padding Bottom="3px" Left="4px" Top="1px" />
                            <BorderDetails ColorLeft="158, 190, 245" ColorRight="0, 45, 150" ColorTop="158, 190, 245"
                                WidthBottom="0px" />
                        </Styles>
                    </ExpandedAppearance>
                    <HoverAppearance>
                        <Styles ForeColor="Blue">
                        </Styles>
                    </HoverAppearance>
                    <ExpansionIndicator Height="0px" Width="0px" />
                    <CollapsedAppearance>
                        <Styles Font-Bold="True" Font-Names="Verdana" Font-Size="10pt">
                        </Styles>
                    </CollapsedAppearance>
                </Header>
                <Template>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <igtbl:UltraWebGrid ID="UWD_EPM" runat="server" Height="131px" Width="100%" OnClick="UWD_EPM_Click1" OnInitializeLayout="UWD_EPM_InitializeLayout">
                                <Bands>
                                    <igtbl:UltraGridBand>
                                        <AddNewRow View="NotSet" Visible="NotSet">
                                        </AddNewRow>
                                    </igtbl:UltraGridBand>
                                </Bands>
                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                    AllowSortingDefault="Yes" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                    Name="ctl00xctl02xUWDxEPM" RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                    Version="4.00">
                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                        Font-Size="8.25pt" Height="131px" Width="100%">
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
                                    <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True">
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
                                    <RowAlternateStyleDefault BackColor="InactiveCaptionText">
                                    </RowAlternateStyleDefault>
                                </DisplayLayout>
                            </igtbl:UltraWebGrid>
                            <ig:WebDialogWindow ID="WDM_EPM_Main" runat="server" Height="346px" Width="955px"
                                InitialLocation="Centered" Modal="True" WindowState="Hidden">
                                <ContentPane>
                                    <Template>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100%; height: 73px;">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                    <asp:Button ID="Button1" runat="server" Text="Export" Width="81px" OnClick="Button1_Click" /></td>
                                                                    <td style="width: 100px">
                                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Close" Width="98px" /></td>
                                                                </tr>
                                                            </table>
                                                    <igtbl:UltraWebGrid ID="UWG_EPM_Detail_PO" runat="server" Height="254px" Width="100%">
                                                        <Bands>
                                                            <igtbl:UltraGridBand>
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                            HeaderClickActionDefault="SortMulti" Name="ctl00xctl33xUWGxEPMxDetailxPO" RowHeightDefault="20px"
                                                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                Font-Size="8.25pt" Height="254px" Width="100%">
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
                                                            <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Center" Wrap="True">
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
                                                            <RowAlternateStyleDefault BackColor="InactiveCaptionText">
                                                            </RowAlternateStyleDefault>
                                                            <SelectedRowStyleDefault BackColor="LightBlue">
                                                            </SelectedRowStyleDefault>
                                                        </DisplayLayout>
                                                    </igtbl:UltraWebGrid>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="Button1" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    </td>
                                            </tr>
                                        </table>
                                    </Template>
                                </ContentPane>
                                <Header CaptionText="Overall Status By Region" Font-Bold="True" Font-Size="10pt">
                                </Header>
                            </ig:WebDialogWindow>
                            <ig:WebDialogWindow ID="WDM_EPM_Detail_PopUp" runat="server" Height="427px" Width="947px"
                                InitialLocation="Centered" Modal="True" WindowState="Hidden">
                                <ContentPane>
                                    <Template>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 100%; vertical-align: top; text-align: left;">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                    <asp:Button ID="btnExport1" runat="server" Text="Export" OnClick="btnExport1_Click" Width="73px" /></td>
                                                                    <td style="width: 100px">
                                                    <asp:Button ID="btnClose1" runat="server" OnClick="btnClose1_Click" Text="Close" Width="70px" /></td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnExport1" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    &nbsp;
                                                    <asp:Panel ID="Panel1" runat="server" Height="279px" ScrollBars="Auto" Width="903px">
                                                    <igtbl:UltraWebGrid ID="WGD_EPM_Detail" runat="server" Height="296px" Width="100%">
                                                        <Bands>
                                                            <igtbl:UltraGridBand>
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                            HeaderClickActionDefault="SortMulti" Name="ctl00xctl33xWGDxEPMxDetail" RowHeightDefault="20px"
                                                            RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" Font-Names="Microsoft Sans Serif"
                                                                Font-Size="8.25pt" Height="296px" Width="100%">
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
                                                            <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Center" Wrap="True">
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
                                                            <RowAlternateStyleDefault BackColor="InactiveCaptionText">
                                                            </RowAlternateStyleDefault>
                                                            <SelectedRowStyleDefault BackColor="LightBlue">
                                                            </SelectedRowStyleDefault>
                                                        </DisplayLayout>
                                                    </igtbl:UltraWebGrid></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    </td>
                                            </tr>
                                        </table>
                                    </Template>
                                </ContentPane>
                                <Header CaptionText="Overall Status by PO" Font-Bold="True" Font-Size="10pt">
                                </Header>
                            </ig:WebDialogWindow>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UWD_EPM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="WDM_EPM_Detail_PopUp" EventName="Load" />
                        </Triggers>
                    </asp:UpdatePanel>
                </Template>
            </igmisc:WebPanel>
            &nbsp;
            <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
            </igtblexp:UltraWebGridExcelExporter>
        </td>
    </tr>
</table>
