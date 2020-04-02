<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TI_drilldown.aspx.vb" Inherits="MDB_TI_drilldown" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
    <%@ Register Assembly="Infragistics2.WebUI.Misc.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/styles.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
            function OverAllStatus(Process,pono,id)
            {      
                window.open('frmManagementDashBoardDetails.aspx?Process='+ Process +'&P='+ pono +'&id='+id,'ManagementDashboard','width=790,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');

            }
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width ="100%" border="0" cellpadding="0" cellspacing="0" >
        <tr style="border-right: 0px; border-top: 0px; background-image: url(../Images/newpixal.jpg);
             border-left: 0px; border-bottom: 0px; background-repeat: repeat-x; height: 33px">
            <td colspan="2">
             <img alt="" src="../Images/ov.jpg" />
            </td>
        </tr>  
        <tr>
            <td style="width:65%" valign="top">
                <table border="0" width="100%"> 
                   <tr>
                        <td class="lblTitle" id="Process" runat="Server" align="left">
                            </td>
                   </tr>
                   <tr>
                   <td style="text-align: center">
                        <asp:GridView ID="GVEPM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="PoNo" EmptyDataText="No Records Found" Width="98%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Po&#160;Date">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPoRecordDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Po&#160;Name">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPoName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO&#160;Number">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPono" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoNo") %>'></asp:Label>
                                        <asp:HiddenField ID="hdPono" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PoNew") %>'>
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContract" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractDT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancel">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCancel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cancel") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActive" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Active") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Air">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDRM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DRM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BAST Done">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBastDone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReadyForBast") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Air Remaining">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="Red" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDRMRemaining" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DRMRemaining") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BAST Remaining">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="Red" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBASTRemaining" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BASTRemaining") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%&#160;BAST">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReadyForBaut1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BASTPrecentage") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReadyForBaut" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                   </tr>
                    <tr>
                        <td style="text-align: center">
                        <igchart:ultrachart id="chartEPM" runat="server" backgroundimagefilename="" bordercolor="Black"
                            borderwidth="1px" charttype="StackColumnChart" emptycharttext="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource"
                            version="9.1">
<Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></Tooltips>

<ColorModel ModelStyle="CustomLinear" ColorBegin="Pink" ColorEnd="DarkRed" AlphaLevel="150"></ColorModel>

<Effects><Effects>
<igchartprop:GradientEffect></igchartprop:GradientEffect>
</Effects>
</Effects>

<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" Extent="91">
<MajorGridLines Visible="False" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalRightFacing">
<SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalRightFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200" Extent="44">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="DimGray" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Far" Orientation="VerticalLeftFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X2>

<Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z>

<Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z2>
</Axis>

<ColumnChart><ChartText>
    <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="0" ItemFormatString="&lt;DATA_VALUE:#&gt;"
        Row="-2" VerticalAlign="Near" Visible="True">
    </igchartprop:ChartTextAppearance>
</ChartText>
</ColumnChart>

<Legend Visible="True" SpanPercentage="32" Font="Arial, 10px" BorderThickness="0"></Legend>
</igchart:ultrachart>
                        </td>
                    </tr>
                </table>  
            </td>
            <td style="width:35%" valign="top">
                 <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server" Width="100%" LinkedRefreshControlID="WebAsyncRefreshPanel2">
                <table border="0" width="100%" id="tblGrid">
                    <tr>
                        <td class="lblTitle" style="width: 49%;">Select PONo</td>
                        <td style="width: 1%;"> :</td>
                        <td><asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList></td>
                    </tr>
                    <tr>
                       <td colspan="3" style="text-align: center">
                       <%--<asp:GridView ID="gdTI" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        EmptyDataText="No Records Found" Width="100%">
                        <PagerSettings Position="TopAndBottom" />
                        <RowStyle CssClass="GridOddRows" />
                        <Columns>
                            <asp:BoundField DataField="Total Site" HeaderText="PO No" />
                            <asp:BoundField DataField="On Air" HeaderText="On Air" />
                            <asp:BoundField DataField="KPI MET" HeaderText="KPI Met" />
                            <asp:BoundField DataField="Folder To NSN" HeaderText="Folder To NSN" />
                            <asp:BoundField DataField="BAUT Submitted" HeaderText="BAUT Submitted" />
                            <asp:BoundField DataField="BAUT Approved" HeaderText="BAUT Approved" />
                            <asp:BoundField DataField="BAST Submitted" HeaderText="BAST Submitted" />
                            <asp:BoundField DataField="BAST Approved" HeaderText="BAST Approved" />
                        </Columns>
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                    </asp:GridView>--%>
                    <asp:GridView ID="gdTI" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        EmptyDataText="No Records Found" Width="95%">
                        <PagerSettings Position="TopAndBottom" />
                        <RowStyle CssClass="GridOddRows" />
                        <Columns>
                            <asp:BoundField DataField="txt" HeaderText="Type" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="val" HeaderText="Total" >
                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                    </asp:GridView>
                       </td>
                    </tr>
                    </table>
                 </igmisc:WebAsyncRefreshPanel>
                <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server" Width="100%">
                <table border="0" width="100%" id="tblChart">
                    <tr>
                        <td style="text-align: center">
                            <igchart:UltraChart ID="chartTI" runat="server" BackgroundImageFileName="" BorderColor="Black"
                                BorderWidth="1px" ChartType="BarChart" Version="9.1" Width="100%">
                                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" FormatString="&lt;DATA_VALUE:#&gt;" />
                                <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" ModelStyle="CustomLinear">
                                </ColorModel>
                                <Effects>
                                    <Effects>
                                        <igchartprop:GradientEffect>
                                        </igchartprop:GradientEffect>
                                    </Effects>
                                </Effects>
                                <Axis>
                                    <PE ElementType="None" Fill="Cornsilk" />
                                    <X Extent="39" LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="True">
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
                                    <X2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
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
                                <BarChart>
                                    <ChartText>
                                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" ItemFormatString="&lt;DATA_VALUE:0&gt;"
                                            Row="-2" Visible="True" HorizontalAlign="Far">
                                        </igchartprop:ChartTextAppearance>
                                    </ChartText>
                                </BarChart>
                                <Data ZeroAligned="True">
                                </Data>
                            </igchart:UltraChart>
                        </td>
                    </tr>
                    </table>
                 </igmisc:WebAsyncRefreshPanel></td>
        </tr>
        <tr>
            <td style="text-align: center;width:65%" valign="top">
                &nbsp;</td>
            <td style="width:35%" valign="top">
            <%--<igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel2" runat="server" Width="100%">
                <table border="0" width="100%" id="tblChart">
                    <tr>
                        <td style="text-align: center">
                            <igchart:UltraChart ID="chartTI" runat="server" BackgroundImageFileName="" BorderColor="Black"
                                BorderWidth="1px" ChartType="BarChart" Version="9.1" Width="100%">
                                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" FormatString="&lt;DATA_VALUE:#&gt;" />
                                <ColorModel AlphaLevel="150" ColorBegin="Pink" ColorEnd="DarkRed" ModelStyle="CustomLinear">
                                </ColorModel>
                                <Effects>
                                    <Effects>
                                        <igchartprop:GradientEffect>
                                        </igchartprop:GradientEffect>
                                    </Effects>
                                </Effects>
                                <Axis>
                                    <PE ElementType="None" Fill="Cornsilk" />
                                    <X Extent="39" LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" Visible="True">
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
                                    <X2 LineThickness="1" TickmarkInterval="50" TickmarkStyle="Smart" Visible="False">
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
                                <BarChart>
                                    <ChartText>
                                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" ItemFormatString="&lt;DATA_VALUE:0&gt;"
                                            Row="-2" Visible="True" HorizontalAlign="Far">
                                        </igchartprop:ChartTextAppearance>
                                    </ChartText>
                                </BarChart>
                            </igchart:UltraChart>
                        </td>
                    </tr>
                    </table>
                 </igmisc:WebAsyncRefreshPanel>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnBack" runat="server" CssClass="buttonStyle" Text="Back To Management Dashboard <<"
                    Width="250px" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
