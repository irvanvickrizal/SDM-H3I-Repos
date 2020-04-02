<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TaskPendingMACRCOProcess.aspx.vb"
    Inherits="DashBoard_TaskPendingMACRCOProcess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Multiple Approval CR/CO Process</title>
    <style type="text/css">
        .headerpanel
        {
            width:99%;
            padding:8px;
            font-family:verdana;
            font-size:13px;
            color:black;
            border-width:1px;
            border-color:gray;
            border-style:solid;
            background-color:#cfcfcf;
            margin-bottom:15px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:12px;
            font-weight:bold;
        }
        .lblText
        {
            font-family:verdana;
            font-size:12px;
        }
        .headerGridPadding
        {
            font-family:Verdana;
	        font-size:11px;
	        background-color:#ffc727;
	        font-weight:bolder;
	        text-align:center;
	        padding:5px;
	        color:white;
	        border-style:solid;
	        border-width:2px;
	        border-color:gray;
        }
        .itemGridPadding
        {
            Padding:5px;
            border-style:solid;
	        border-width:2px;
	        border-color:gray;
	        font-family:verdana;
	        font-size:11px;
        }
        .EmptyDataRowStyle
        {
            padding:5px;
            font-family:verdana;
            font-size:11px;
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
                    background-image: url(../Images/animation_processing.gif);
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
                    z-index: 1;
                    height: 100%;
                    position:fixed;
                    top: 0;
                    left: 0;
                }
        
    </style>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%">
            <div class="headerpanel">
                <span>Pending Multiple Approval CR/CO Document Process</span>
            </div>
            <div style="margin-top: 10px;">
                <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UP1">
                    <ProgressTemplate>
                        <div id="blur">
                            <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                                <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                    height="150" alt="Processing" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" EmptyDataText="No Document Pending"
                            AllowPaging="true" PageSize="10">
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                            <HeaderStyle CssClass="headerGridPadding" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="headerGridPadding">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="itemGridPadding" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocType" HeaderText="Document" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="SiteNo" HeaderText="Site No." ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="Sitename" HeaderText="Sitename" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="PONO" HeaderText="PoNumber" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="PackageId" HeaderText="Workpackageid" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy}"
                                    HtmlEncode="false" ConvertEmptyStringToNull="true" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="ModifiedTransactionDate" HeaderText="Approval Date Process"
                                    DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" ConvertEmptyStringToNull="true"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerGridPadding"
                                    ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="IndicativePriceCostIDR" HeaderText="Indicative Price Cost IDR"
                                    DataFormatString="{0:#,##0}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                                <asp:BoundField DataField="IndicativePriceCostUSD" HeaderText="Indicative Price Cost USD"
                                    DataFormatString="{0:###,##.#0}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                                <asp:TemplateField ItemStyle-CssClass="itemGridPadding" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSNO" runat="server" Text='<%#Eval("sno") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblDocType" runat="server" Visible="false" Text='<%#Eval("DocType") %>'></asp:Label>
                                        <div id="pnlCRDocView" runat="server">
                                            <a href='../ViewDocument/frmViewCRDocument.aspx?id=<%#Eval("sno") %>' target="_blank"
                                                style="text-decoration: none; border-style: none;">
                                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                                    style="text-decoration: none; border-style: none;" />
                                            </a>
                                        </div>
                                        <div id="pnlCODocView" runat="server">
                                            <a href='../ViewDocument/frmViewCODocument.aspx?id=<%#Eval("sno") %>' target="_blank"
                                                style="text-decoration: none; border-style: none;">
                                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="Img1" height="18" width="18"
                                                    style="text-decoration: none; border-style: none;" />
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>
