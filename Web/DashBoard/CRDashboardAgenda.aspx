<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRDashboardAgenda.aspx.vb"
    Inherits="DashBoard_CRDashboardAgenda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <title>CR Dashboard Agenda</title>
    <style type="text/css">
        .HeaderGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: White;
            background-color: #ffc90E;
            border-color:white;
            vertical-align:middle;
        }
        .oddGrid
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
        }
        .evenGrid
        {
            font-family: Arial Unicode MS;
            font-size: 9pt;
            background-color:#cfcfcf;
        }
        .HeaderPanel
        {
           width:99%;
           background-repeat: repeat-x;
           background-image: url(../Images/banner/BG_Banner.png);
           font-family:verdana;
           font-weight:bolder;
           font-size:10pt;
           color:white;
           padding-top:5px;
           padding-bottom:5px;
        }
        .lblSubHeader
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            font-weight:bolder;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:10pt;
            font-weight:bolder;
        }
         .lblBText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .evengrid2
        {
            background-color: #cfcfcf;
        }
    </style>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:Arial Unicode MS;
            font-size:13px;
            font-weight:bold;
            margin-top:15px;
            margin-bottom:10px;
            padding:3px;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:Arial Unicode MS;
           font-weight:bold;
           font-size:10pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .BtnExpt
        {
           border-style:solid;
           border-color:white;
           border-width:1px;
           font-family:Arial Unicode MS;
           font-size:11px;
           font-weight:bold;
           color:white;
           width:120px;
           height:25px;
           cursor:hand;
        }
        .btnSubmit
        {
            font-family:Arial Unicode MS;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:hand;
        }
        .btnRefresh
        {
            font-family:Arial Unicode MS;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:hand;
            height:25px;
        }
        
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align:center;
            height : 100px;
            width:100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%; margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color:#ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }

    </style>

    <script type="text/javascript">
         function invalidExportToExcel() {
            alert('Data is empty, please try again!');
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MvCorePanel" runat="server">
                <asp:View ID="vwGeneralTask" runat="server">
                    <div class="HeaderReport">
                        <table width="100%">
                            <tr>
                                <td style="width: 80%">
                                    <asp:Label ID="LblUserUnderSignature" runat="server"></asp:Label>
                                </td>
                                <td style="width: 15%; text-align: right;">
                                    <asp:Button ID="BtnExportUserUnderSignature" runat="server" Text="Export To Excel"
                                        CssClass="BtnExpt" BackColor="#7f7f7f" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="False" EmptyDataText="No CR Document Pending"
                            AllowSorting="True" AutoGenerateColumns="False">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CRNo" HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EOName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    ItemStyle-Width="160px" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="UserLocation" HeaderText="Pending Task" HeaderStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="VwSiteCRCOReadyCreation" runat="server">
                    <div class="HeaderReport">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%">
                                    <asp:Label ID="LblSiteReadyCreation" runat="server"></asp:Label>
                                </td>
                                <td style="width: 35%; text-align: right;">
                                    <asp:Button ID="BtnBackToDashboard_SiteReadyCreation" runat="server" Text="Go to Dashboard"
                                        CssClass="BtnExpt" BackColor="#7f7f7f" />
                                    <asp:Button ID="BtnExptSiteReadyCreation" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                        BackColor="#7f7f7f" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="GvSiteReadyCreation" runat="server" AllowPaging="False" EmptyDataText="No Data Ready Creation"
                            AllowSorting="True" AutoGenerateColumns="False">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site No" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtSiteNo" runat="server" Text='<%#Eval("SiteNo") %>' CommandName="checkdoc"
                                            CommandArgument='<%#Eval("PackageId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EOName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="VwReportingFinal" runat="server">
                    <div class="HeaderReport">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%">
                                    <asp:Label ID="LblCRCOReporting" runat="server"></asp:Label>
                                </td>
                                <td style="width: 35%; text-align: right;">
                                    <asp:Button ID="btnBackToDashboard" runat="server" Text="Go To Dashboard" CssClass="BtnExpt"
                                        BackColor="#7f7f7f" />
                                    <asp:Button ID="btnExptCRCOReporting" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                        BackColor="#7f7f7f" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="GvCRCOReporting" runat="server" AllowPaging="False" EmptyDataText="No Data Ready Creation"
                            AllowSorting="True" AutoGenerateColumns="False">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StatusDocument" HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EOName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href='../CR/frmViewCRFinal_Popup.aspx?wpid=<%#Eval("PackageId") %>' target="_blank"
                                            style="text-decoration: none;">
                                            <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                style="text-decoration: none; border-width: 0px;" runat="server" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="VwCODoneReporting" runat="server">
                    <div class="HeaderReport">
                        <table width="100%">
                            <tr>
                                <td style="width: 60%">
                                    <asp:Label ID="LblHeaderCODone" Text="CO Done" runat="server"></asp:Label>
                                </td>
                                <td style="width: 35%; text-align: right;">
                                    <asp:Button ID="BtnGoToDashboardCODone" runat="server" Text="Go To Dashboard" CssClass="BtnExpt"
                                        BackColor="#7f7f7f" />
                                    <asp:Button ID="BtnExportCODone" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                        BackColor="#7f7f7f" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="GvCODoneReport" runat="server" AllowPaging="False" EmptyDataText="No Data Found"
                            AllowSorting="True" AutoGenerateColumns="False">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EOName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TselApprover" HeaderText="Tsel Approver" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TselApproverDate" HeaderText="Approved Date" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href='../PO/frmViewCODocument.aspx?swid=<%#Eval("swid") %>' target="_blank" style="text-decoration: none;">
                                            <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                style="text-decoration: none; border-width: 0px;" runat="server" />
                                        </a><a href='../PO/frmCOViewLog.aspx?id=<%#Eval("COID") %>&docid=<%#Eval("DocId") %>&wpid=<%#Eval("PackageId") %>'
                                            target="_blank" style="text-decoration: none;">
                                            <img src="../Images/ViewLog.jpg" alt="pdficon" id="Img1" height="18" width="18" style="text-decoration: none;
                                                border-width: 0px;" runat="server" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
