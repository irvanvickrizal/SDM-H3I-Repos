<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOUpload.aspx.vb" Inherits="frmPOUpload" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v9.1, Version=9.1.20091.1015, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ePM & PO Upload</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <table width="100%">
                <tr class="pageTitle">
                    <td colspan="2">
                        Po Upload
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:FileUpload ID="POUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnview"
                            runat="server" CssClass="buttonStyle" Text="Upload" Width="71px" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td runat="server" id="PrCount">
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=1','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A1" class="ASmall">Missing Workpackage Id</a>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=2','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A2" class="ASmall">Configuration Error (Band1800
                        - Purchased Shows in 900)</a>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=3','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A3" class="ASmall">Configuration Error (Band900
                        - Purchased Shows in 1800)</a>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=4','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A4" class="ASmall">Dual Band - Qty MisMatch</a>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <a href="#" onclick="window.open('frmPOMissingInfo.aspx?Type=5','','width=800,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes')"
                        runat="server" visible="false" id="A5" class="ASmall">Configuration Error (config
                        shows 333+444, But Quantity is not matching with Config Total)</a>
                </td>
            </tr>
            <tr>
                <td runat="server" id="DupSites">
                </td>
            </tr>
            <tr>
                <td runat="server" id="errrow">
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table width="100%">
            <tr class="pageTitle">
                <td colspan="3">
                    EPM Upload for validating Data:
                    <asp:Label Id="LblCheck" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="3">
                    <asp:FileUpload ID="EPMUpload" runat="server" Width="518px" />&nbsp;<asp:Button ID="btnEview"
                        runat="server" CssClass="buttonStyle" Text="Upload" Width="70px" />
                    <asp:Button ID="Button1" runat="server" CssClass="buttonStyle" Text="Backup" Width="70px" />
                    <asp:Button ID="Button2" runat="server" CssClass="buttonStyle" Text="Restore" Width="70px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" id="EPMcount" runat="server" style="width: 100%">
                </td>
            </tr>
            <tr>
                <td runat="server" id="nochangescount" colspan="3" style="width: 100%">
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        <table>
            <tr>
                <td runat="server" id="noissuescount" colspan="2" style="width: 10%">
                </td>
                <td style="width: 90%">
                    <asp:Button ID="btnproceed" runat="server" Text="Proceed to EPM" Visible="false"
                        Width="257px" />
                </td>
            </tr>
            <tr>
                <td runat="server" id="issuescount" colspan="2" style="width: 10%">
                </td>
                <td style="width: 90%">
                    <asp:Button ID="btnDownload" runat="server" Text="Download the Dispute records" Visible="False"
                        Width="257px" />
                </td>
            </tr>
            <tr>
                <td id="Td1" runat="server" colspan="2" style="width: 39%; background-color: lightgrey">
                </td>
                <td>
                </td>
            </tr>
        </table>
        <tr>
            <td colspan="3">
                <b>FTP Uploaded File Name:</b>
            </td>
        </tr>
        <tr>
            <td style="height: 17px">
                <asp:TextBox ID="fileUpload1" Enabled="true" runat="server" EnableTheming="True"
                    Width="456px" />
                <asp:Button ID="btnupload1" runat="server" Text="Process" CssClass="buttonStyle" />
            </td>
        </tr>
        <table width="100%">
            <tr class="pageTitle">
                <td colspan="2">
                    EPM Upload after validating i.e downloaded dispute records after correction:
                </td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                    <asp:FileUpload ID="EPMUploadnew" runat="server" Width="518px" />&nbsp;<asp:Button
                        ID="btnEviewnew" runat="server" CssClass="buttonStyle" Text="Upload new" Width="70px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" id="EPMcountnew" runat="server" style="width: 100%">
                </td>
            </tr>
        </table>
        <igtbl:UltraWebGrid ID="EPMAfterValidating" runat="server" Height="200px" Width="325px"
            Visible="False">
            <Bands>
                <igtbl:UltraGridBand>
                    <AddNewRow Visible="NotSet" View="NotSet">
                    </AddNewRow>
                </igtbl:UltraGridBand>
            </Bands>
            <DisplayLayout Version="4.00" SelectTypeRowDefault="Extended" Name="UltraWebGrid1"
                AllowSortingDefault="OnClient" AllowDeleteDefault="Yes" AllowUpdateDefault="Yes"
                AllowColSizingDefault="Free" RowHeightDefault="20px" TableLayout="Fixed" ViewType="OutlookGroupBy"
                RowSelectorsDefault="No" AllowColumnMovingDefault="OnServer" HeaderClickActionDefault="SortMulti"
                StationaryMargins="Header" BorderCollapseDefault="Separate" StationaryMarginsOutlookGroupBy="True">
                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid"
                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="200px" Width="325px">
                </FrameStyle>
                <Pager MinimumPagesForDisplay="2">
                    <PagerStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
                    </PagerStyle>
                </Pager>
                <EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
                </EditCellStyleDefault>
                <FooterStyleDefault BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid">
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
                </FooterStyleDefault>
                <HeaderStyleDefault HorizontalAlign="Left" BackColor="LightGray" BorderStyle="Solid">
                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
                </HeaderStyleDefault>
                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"
                    Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                    <Padding Left="3px"></Padding>
                    <BorderDetails ColorLeft="Window" ColorTop="Window"></BorderDetails>
                </RowStyleDefault>
                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                </GroupByRowStyleDefault>
                <GroupByBox>
                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                    </BoxStyle>
                </GroupByBox>
                <AddNewBox Hidden="False">
                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px" BorderStyle="Solid">
                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
                    </BoxStyle>
                </AddNewBox>
                <ActivationObject BorderColor="" BorderWidth="">
                </ActivationObject>
                <FilterOptionsDefault>
                    <FilterDropDownStyle CustomRules="overflow:auto;" BackColor="White" BorderColor="Silver"
                        BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                        Font-Size="11px" Height="300px" Width="200px">
                        <Padding Left="2px"></Padding>
                    </FilterDropDownStyle>
                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                    </FilterHighlightRowStyle>
                    <FilterOperandDropDownStyle CustomRules="overflow:auto;" BackColor="White" BorderColor="Silver"
                        BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                        Font-Size="11px">
                        <Padding Left="2px"></Padding>
                    </FilterOperandDropDownStyle>
                </FilterOptionsDefault>
            </DisplayLayout>
        </igtbl:UltraWebGrid>
        <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
        </igtblexp:UltraWebGridExcelExporter>
    </form>
</body>
</html>
