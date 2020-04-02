<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManagementDashobardProcess.aspx.vb"
    Inherits="DashBoard_ManagementDashobardProcess" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Management Dashboard Process</title>
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
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr style="border-right: 0px; border-top: 0px; background-image: url(../Images/newpixal.jpg);
                    border-left: 0px; border-bottom: 0px; background-repeat: repeat-x; height: 33px">
                    <td valign="top">
                        <img alt="" src="../Images/ov.jpg" />
                    </td>
                </tr>
                <tr>
                    <td class="hgap">
                    </td>
                </tr>
                  <tr>
                <td class="lblTitle" id="Process" runat="Server" align="left">
                 
                </td>
             
                </tr> 
                  <tr>
                    <td class="hgap">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GVEPM" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="PoNo" EmptyDataText="No Records Found" Width="100%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="center" VerticalAlign="Middle" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Po&nbsp;Date" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPoRecordDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Po&nbsp;Name" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPoName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO&nbsp;Number" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerPono" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PoNo") %>'></asp:Label>
                                        <asp:HiddenField ID="hdPono" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PoNew") %>'>
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblContract" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractDT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cancel" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCancel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cancel") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActive" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Active") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Air" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDRM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DRM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BAST Done" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBastDone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ReadyForBast") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Air Remaining" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Font-Bold="true" ForeColor="red" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDRMRemaining" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DRMRemaining") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BAST Remaining" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" Font-Bold="true" ForeColor="red" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBASTRemaining" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BASTRemaining") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%&nbsp;BAST" ItemStyle-Width="65px" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReadyForBaut1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BASTPrecentage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="center"
                                    HeaderStyle-VerticalAlign="Middle">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblReadyForBaut" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="hgap">
                       </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <igchart:ultrachart id="chart" runat="server" backgroundimagefilename="" bordercolor="Black"
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
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" CssClass="buttonStyle" Width="250px" Text="Back To Management Dashboard <<" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
