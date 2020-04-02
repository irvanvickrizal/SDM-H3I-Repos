<%@ Page Language="VB" AutoEventWireup="true" CodeFile="frmTI_FinalCR.aspx.vb" Inherits="BAUT_frmTI_FinalCR"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Request</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <style type="text/css">
        .lblSubHeader
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            font-weight:bolder;
        }
        .lblSubHeader2
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            color:black;
            font-weight:bold;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:10pt;
            font-weight:bolder;
        }
        .subpanelheader{text-align:left;}
         .lblBText
        {
            font-family: Arial Unicode MS;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblBTextPrice
        {
            font-family: Verdana;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblBTextDisclaimer
        {
            font-family: Verdana;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .lblTextC
        {
            font-family: Arial Unicode MS;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
        }
        .btnReview
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnReview_0.gif);
        }
        .btnReview:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnReview_1.gif);
        }
        .btnReview:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnReview_2.gif);
        } 
       .btnCancel
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_0.gif);
        }
        .btnCancel:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_1.gif);
        }
        .btnCancel:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnCancel_2.gif);
        } 
    </style>
    <style type="text/css">
         .HeaderGrid2
    {
        font-family: Arial Unicode MS;
        font-size: 9.5pt;
        font-weight: bold;
        color: White;
        background-color: #ffc90E;
        border-color:white;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        background-color: White;
    }
    .evenGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        background-color:#cfcfcf;
    }
    </style>

    <script type="text/javascript">
        function WindowsClose(wpid){
                window.opener.location.href = '../CR/frmListCR.aspx?wpid=' + wpid + '&time=' + (new Date()).getTime();
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
      
    </script>

</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <asp:HiddenField ID="HdnScope" runat="server" />
        <asp:HiddenField ID="HdnSiteId" runat="server" />
        <asp:HiddenField ID="HdnVersion" runat="server" />
        <asp:HiddenField ID="HdnCRID" runat="server" />
        <div id="dvPrint" runat="server" style="width: 800px; height: 840px; border-style: none;
            border-color: Black; border-width: 0px;">
            <div id="headerPanel" style="margin-top: 15px; width: 100%; border-bottom-style: none;
                border-bottom-color: Black; border-bottom-width: 2px; padding-bottom: 2px;">
                <table cellpadding="0" cellspacing="0" width="99%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/nsn-logo.gif" alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="bottom" style="width: 55%">
                            <span class="lblTitle">Summary CR Approved</span>
                        </td>
                        <td align="right" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/logo_tsel.png" alt="tsellogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="AttributePanel" style="margin-top: 5px; height: 120px; border-bottom-width: 0px;
                border-bottom-style: none; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/GeneralInformation.png" alt="generalInformation"
                        width="800px" />
                </div>
                <div>
                    <table width="800px">
                        <tr valign="top">
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Change Request Number</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblChangeRequestNumber" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Site ID</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSiteID" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Site Name</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSiteName" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Area</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblArea" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Contract Number</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPONo" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">PO Name</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblEOName" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Project Type</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblProjectType" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Project ID</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblProjectID" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Date Submitted</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblDateSubmitted" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Project Category</span>
                                        </td>
                                        <td>
                                            <span class="lblTextC">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblProjectCategory" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="ProposedChangedPanel" style="margin-top: 5px; border-bottom-width: 0px;
                border-bottom-style: none; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/Description.png" alt="Description"
                        width="800px" />
                </div>
                <div class="lblTextC" style="margin-left: 2px; margin-top: 2px; margin-bottom: 5px;">
                    <asp:TextBox ID="TxtDescriptionOfChange" runat="server" Style="overflow: hidden;"
                        CssClass="lblTextC" TextMode="MultiLine" Width="790px" Height="60px" Visible="false"></asp:TextBox>
                    <asp:GridView ID="GvFinalDescription" runat="server" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="HeaderGrid" ShowFooter="false" HeaderStyle-Font-Bold="true"
                        BorderColor="black" BorderStyle="Solid" GridLines="Both" Width="795px" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="400px">
                                <HeaderStyle BorderColor="black" BorderWidth="1px" BorderStyle="solid" HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LblHeadDesc" runat="server" Text="Description" CssClass="lblSubHeader2"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle BorderColor="black" BorderWidth="1px" BorderStyle="solid" />
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="LblCRNo" runat="server" Text='<%#Eval("CRNo") %>' CssClass="lblBText"></asp:Label>
                                    </div>
                                    <div style="margin-top: 2px">
                                        <span class="lblBText">Description of Change :</span>
                                    </div>
                                    <div style="margin-top: 2px;">
                                        <asp:Label ID="LblDescriptionofChange" runat="server" CssClass="lblTextC" Text='<%#Eval("DescriptionofChange") %>'></asp:Label>
                                    </div>
                                    <div style="margin-top: 4px">
                                        <span class="lblBText">Comments :</span>
                                    </div>
                                    <div style="margin-top: 2px;">
                                        <span class="lblTextC">
                                            <%#Eval("JustificationComments") %>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-VerticalAlign="Top" ItemStyle-Width="400px">
                                <HeaderStyle BorderColor="black" BorderWidth="1px" BorderStyle="solid" HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LblHeadApproval" runat="server" Text="Approval Person" CssClass="lblSubHeader2"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle BorderColor="black" BorderWidth="1px" BorderStyle="solid" />
                                <ItemTemplate>
                                    <div id="DvSignaturePanel" style="margin-left: 10px; margin-top: 5px;">
                                        <asp:Label ID="ListofApproval" runat="server" CssClass="lblTextC">
                                            <%#GetApprovalListString(Eval("CRID"))%>
                                        </asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="Div1" style="margin-top: 5px; border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/FinalImpact.png" alt="FinalImpact"
                        width="800px" />
                </div>
                <div style="margin-bottom: 5px;">
                    <table width="800px">
                        <tr valign="top">
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Design Impact</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkDesignImpact" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Budget Impact</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkBudgetImpact" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">No Design and Budget Impact</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="chkNoImpact" disabled="disabled" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 500px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 2px; margin-top: 5px;">
                                    <span class="lblBText">Indicative Budget / Cost Impacts :</span>
                                </div>
                                <div style="margin-left: 2px;">
                                    <table border="2" style="border-color: Black;">
                                        <tr style="border-color: Black;">
                                            <td style="border-style: none; border-width: 0px;">
                                            </td>
                                            <td style="border-color: Black;">
                                                <span class="lblTextC">USD</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <span class="lblTextC">IDR</span>
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblTextC">Contract</span>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Label ID="LblContractUSD" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Label ID="LblContractIDR" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblTextC">Implementation</span>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Label ID="LblImplementationUSD" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:Label ID="LblImplementationIDR" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblTextC">Indicative Price Cost</span>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:TextBox ID="TxtIndicativePriceCostUSD" runat="server" CssClass="lblTextC" Height="14px"
                                                    Visible="false" Width="150px"></asp:TextBox>
                                                <asp:Label ID="LblIndicativePriceCostUSD" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:TextBox ID="TxtIndicativePriceCostIDR" runat="server" CssClass="lblTextC" Height="14px"
                                                    Visible="false" Width="150px"></asp:TextBox>
                                                <asp:Label ID="LblIndicativePriceCostIDR" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black; width: 170px;">
                                                <span class="lblTextC">Percentage Price Change</span>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:TextBox ID="TxtPercentagePriceChangeUSD" runat="server" CssClass="lblBText"
                                                    Height="14px" Width="150px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="LblPercentagePriceChangeUSD" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                            <td style="border-color: Black; width: 150px; height: 15px; text-align: right; vertical-align: middle;">
                                                <asp:TextBox ID="TxtPercentagePriceChangeIDR" runat="server" CssClass="lblTextC"
                                                    Height="14px" Width="150px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="LblPercentagePriceChangeIDR" runat="server" CssClass="lblBTextPrice"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="lblTextC">
                    <table class="lblTextC">
                        <tr>
                            <td class="lblTextC">
                                <span class="lblTextC">Schedule Impacts</span>
                            </td>
                            <td>
                                :</td>
                            <td class="lblTextC">
                                <asp:Label ID="LblScheduleImpacts" runat="server" CssClass="lblTextC"></asp:Label>
                                <asp:TextBox ID="TxtScheduleImpacts" runat="server" CssClass="lblTextC" Height="14px"
                                    Width="680px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblTextC">
                                <span class="lblTextC">Other Impacts</span>
                            </td>
                            <td class="lblTextC">
                                :</td>
                            <td>
                                <asp:Label ID="LblOtherImpacts" runat="server" CssClass="lblTextC"></asp:Label>
                                <asp:TextBox ID="TxtOtherImpacts" runat="server" CssClass="lblTextC" Height="14px"
                                    Width="680px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div style="margin-top: 2px; border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: Black;">
                <span class="lblBTextDisclaimer">Disclaimer : This summary contains all changes extracted
                    from all approved CRs for the aforementioned site and requires no signatures. It
                    is used as main reference to create the CO and trigger its process.</span>
            </div>
        </div>
        <div id="ListCRApprovedDocPanel" runat="server" style="width: 310px; border-style: solid;
            border-width: 1px; border-color: Black;">
            <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 97%;
                padding-left: 10px;">
                <span style="font-family: Arial Unicode MS; font-size: 8pt; font-weight: bolder;">List
                    of CR Document Approved</span>
            </div>
            <div style="margin-top: 5px; margin-left: 5px;">
                <asp:GridView ID="GvCRDocApproved" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid2">
                    <RowStyle CssClass="evenGrid" />
                    <AlternatingRowStyle CssClass="oddGrid" />
                    <Columns>
                        <asp:BoundField DataField="CRNo" HeaderText="Document Name" />
                        <asp:BoundField DataField="InitiatorName" HeaderText="Inititator Name" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="#" style="text-decoration: none; border-width: 0px;" onclick="window.open('../po/frmViewCRDocument.aspx?crid=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>','','Width=850,height=500');">
                                    <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                        style="text-decoration: none; border-width: 0px;" runat="server" />
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="ButtonPanelControl" runat="server" style="margin-top: 10px; width: 300px;
                text-align: right;">
                <asp:HiddenField ID="HdnWPId" runat="server" />
                <asp:LinkButton ID="LbtReviewCRFinal" Width="60px" runat="server" OnClientClick="return confirm('Are you sure you want to confirm this CR as final?')"
                    Style="text-decoration: none;">
                     <div class="btnReview"></div>                    
                </asp:LinkButton>
                <asp:LinkButton ID="LbtCancelCRFinal" Width="60px" runat="server" Style="text-decoration: none;">
                     <div class="btnCancel"></div>                    
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
