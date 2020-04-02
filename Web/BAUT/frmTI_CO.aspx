<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTI_CO.aspx.vb" Inherits="BAUT_frmTI_CO"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Order Form</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <style type="text/css">
        .PageBreak {
                page-break-before: always;
            }
            .seenextpage
            {
                text-align:left;
                margin-left:3px;
                margin-top:5px;
                margin-bottom:5px;
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
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
         .lblBHText
        {
            font-family: Arial Unicode MS;font-size: 6.5pt;color: #000000;text-align: center;font-weight: bold;
        }
        .lblBTextPrice
        {
            font-family: Verdana;
            font-size: 6.5pt;
            color: #000000;
            text-align: right;
            font-weight: bold;
        }
        .lblTextC
        {
            font-family: Arial Unicode MS;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
        }
        .lblTextC1
        {
            font-family: Arial Unicode MS;
            font-size: 6.5pt;
            color: #000000;
            text-align: right;
        }
        .scheduleImpactField,.otherImpactField
        {
            height:20px;text-align:left;margin-top:5px;
        }
        .technicalImpactField
        {
            height:45px;text-align:left;margin-top:5px;
        }
        .descdetailfield
        {
            margin-top: 3px; width: 100%; margin-left: 3px;
        }
        .budgetImpactField
        {
            margin-top:3px;height:150px;
        }
        .subpanelheader
        {
           width: 790px;
           padding-left: 0px;
        }
        .borderConfig
        {
           border-width:1px;border-style:solid;border-color:black
        }
        .clearDiv
        {
            height:30px;
        }
        .btnEditCR
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnEditCO_0.gif);
        }
        .btnEditCR:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnEditCO_1.gif);
        }
        .btnEditCR:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnEditCO_2.gif);
        } 
        
        .GeneratePDFForm
        {
            height:26px;
            width:120px;
            background-image: url(../Images/button/GeneratePDFForm_0.gif);
        }
        .GeneratePDFForm:hover
        {
            height:26px;
            width:120px;
            background-image: url(../Images/button/GeneratePDFForm_1.gif);
        }
        .GeneratePDFForm:click
        {
            height:26px;
            width:120px;
            background-image: url(../Images/button/GeneratePDFForm_2.gif);
        }
        .signPanel
        {
            height:420px;
            text-align:left;
            margin-top:10px;
        }
        #AttributePanel
        {
            margin-top:5px;
            height:135px;
        }
    </style>

    <script type="text/javascript">
        function formatCurrency(num){
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num)) 
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                num = Math.floor(num / 100).toString();
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) 
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (num);
            }
        function formatusdCurrency(num){
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num)) 
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                num = Math.floor(num / 100).toString();
                if (cents < 10) 
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) 
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }
    </script>

</head>
<body class="mainCSS">
    <form id="form1" runat="server">
        <asp:HiddenField ID="HdnSiteId" runat="server" />
        <asp:HiddenField ID="HdnVersion" runat="server" />
        <asp:HiddenField ID="HdnPackageId" runat="server" />
        <asp:HiddenField ID="HdnWFID" runat="server" />
        <div id="dvPrint" runat="server" style="width: 800px; height: 700px; border-style: solid;
            border-width: 0px;">
            <div id="headerPanel" style="margin-top: 15px; width: 100%; border-bottom-style: solid;
                border-bottom-color: Black; border-bottom-width: 2px; padding-bottom: 2px;">
                <table cellpadding="0" cellspacing="0" width="99%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/nsn-logo.gif" alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="bottom" style="width: 55%">
                            <span class="lblTitle">Change Order Form</span>
                        </td>
                        <td align="right" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/logo_tsel.png" alt="tsellogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="AttributePanel" style="margin-top: 5px; height: 135px; border-bottom-width: 0px;
                border-bottom-style: solid; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/GeneralInformation.png"
                        alt="generalInformation" width="800px" />
                </div>
                <div>
                    <table width="800px">
                        <tr valign="top">
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblTextC">Change Order Number</span>
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
                                            <span class="lblTextC">EO Name</span>
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
            <div id="InitiatorPanel" style="margin-top: 5px; height: 50px; border-bottom-width: 0px;
                border-bottom-style: solid; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/Initiator.png" width="800px"
                        alt="Initiator" />
                </div>
                <div>
                    <table width="800px">
                        <tr valign="top">
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div>
                                    <span class="lblTextC">Name</span>
                                </div>
                                <div style="margin-top: 3px;">
                                    <asp:Label ID="LblInitiatorName" runat="server" CssClass="lblBText"></asp:Label>
                                </div>
                            </td>
                            <td style="width: 300px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div>
                                    <span class="lblTextC">Department</span>
                                </div>
                                <div style="margin-top: 3px;">
                                    <asp:Label ID="LblInitiatorDepartment" runat="server" CssClass="lblBText"></asp:Label>
                                </div>
                            </td>
                            <td style="width: 200px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div>
                                    <span class="lblTextC">Area</span>
                                </div>
                                <div style="margin-top: 3px;">
                                    <asp:Label ID="LblInitiatorArea" runat="server" CssClass="lblBText"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="DescriptionChangePanel" style="margin-top: 10px; border-bottom-width: 0px;
                border-bottom-style: solid; border-bottom-color: Black; padding-bottom: 5px;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/Description.png"
                        width="800px" alt="DescofChange" />
                </div>
                <div class="subpanelheader">
                    <div class="seenextpage">
                        <span class="lblBText"><b>Refer to Page 2 as detail of Description Changed ...</b></span>
                    </div>
                </div>
            </div>
            <div id="ImpactPanel" style="margin-top: 5px; border-bottom-width: 0px; border-bottom-style: solid;
                border-bottom-color: Black; margin-bottom: 5px;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/Impact.png" width="800px"
                        alt="impact" />
                </div>
                <div style="margin-left: 0px;">
                    <div class="subpanelheader">
                        <span class="lblBText"><b>Technical Impact</b></span>
                    </div>
                    <div class="technicalImpactField">
                        <asp:Label ID="TechnicalImpact" runat="server" CssClass="lblTextC" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtTechnicalImpact" runat="server" CssClass="textFieldStyle" Width="795px"
                            Height="30px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="subpanelheader">
                        <span class="lblBText"><b>Budget Impact</b></span>
                    </div>
                    <div class="budgetImpactField">
                        <asp:GridView ID="GvBudgetImpact" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                            ShowFooter="true" HeaderStyle-Font-Bold="true" BorderColor="black" BorderStyle="Solid"
                            GridLines="Both" Width="795px" EmptyDataText="No Data Found">
                            <RowStyle CssClass="oddGrid" />
                            <AlternatingRowStyle CssClass="evenGrid" />
                            <Columns>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-Width="60px"
                                    HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid"
                                    ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid"
                                    HeaderStyle-BorderWidth="1px" HeaderText="HOT/HOG" HeaderStyle-CssClass="lblBHText">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemPONO" runat="server" Text='<%#Eval("PONO") %>' CssClass="lblTextC"
                                            Width="80px" />
                                    </ItemTemplate>
                                     <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle; height:14px;">
                                            
                                        </div>
                                        <div>
                                            <asp:Label ID="LblBlank" runat="server" CssClass="lblTextC1">&nbsp;</asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white" ItemStyle-Width="60px"
                                    HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid"
                                    ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid"
                                    HeaderStyle-BorderWidth="1px" HeaderText="Detail" HeaderStyle-CssClass="lblBHText">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblTextC"
                                            Width="190px" />
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" HorizontalAlign="Right"
                                        Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black;padding-right:5px;height:14px; vertical-align:middle;">
                                            <asp:Label ID="LblPercentageTotal" runat="server" Text="Percentage of Change" CssClass="lblTextC1"></asp:Label>
                                        </div>
                                        <div style="padding-right:5px;">
                                            <asp:Label ID="LblTotal" runat="server" Text="Total" CssClass="lblTextC1"></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    FooterStyle-BorderColor="black" FooterStyle-BorderStyle="solid" FooterStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BorderColor="black"
                                    HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">USD</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblContractUSD" Text='<%#Eval("ContractUSD") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle; height:14px;">
                                            <asp:Label ID="LblPercentageContractUSD" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercContractUSD) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblContractTotalUSD" runat="server" CssClass="lblTextC1">
                                            <%#GetDetailTotal("totalContractUSD")%>
                                            </asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">IDR</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblContractIDR" Text='<%#Eval("ContractIDR") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle;height:14px;">
                                            <asp:Label ID="LblPercentageContractIDR" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercContractIDR) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblContractTotalIDR" runat="server" CssClass="lblTextC1">
                                            <%#GetDetailTotal("totalContractIDR")%>
                                            </asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">USD</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblCRUSD" Text='<%#Eval("CRUSD") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle;height:14px;">
                                            <asp:Label ID="LblPercentageCRUSD" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercImplementationUSD) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblCRTotalUSD" runat="server" CssClass="lblTextC" Text='<%#GetDetailTotal("totalCRUSD")%>'></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">IDR</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblCRIDR" Text='<%#Eval("CRIDR") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle;height:14px;">
                                            <asp:Label ID="LblPercentageCRIDR" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercImplementationIDR) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblCRTotalIDR" runat="server" CssClass="lblTextC1" Text='<%#GetDetailTotal("totalCRIDR")%>'></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">USD</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblDeltaUSD" Text='<%#Eval("DeltaUSD") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle;height:14px;">
                                            <asp:Label ID="LblPercentageDeltaUSD" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercDeltaUSD) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblDeltaTotalUSD" runat="server" CssClass="lblTextC1" Text='<%#GetDetailTotal("totalDeltaUSD")%>'></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                    HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderText="Detail"
                                    HeaderStyle-CssClass="lblBHText">
                                    <HeaderTemplate>
                                        <span class="lblBText">IDR</span>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblDeltaIDR" Text='<%#Eval("DeltaIDR") %>' runat="server" CssClass="lblTextC1"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="black" BorderStyle="solid" BorderWidth="1px" Font-Bold="true" />
                                    <FooterTemplate>
                                        <div style="border-bottom: solid 1px black; vertical-align: middle;height:14px;">
                                            <asp:Label ID="LblPercentageDeltaIDR" runat="server" CssClass="lblTextC1" Font-Bold="false">
                                            <%#Convert.ToDecimal(GetDetailPercentage().PercDeltaIDR) %>
                                            </asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="LblDeltaTotalIDR" runat="server" CssClass="lblTextC1" Text='<%#GetDetailTotal("totalDeltaIDR")%>'></asp:Label>
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="scheduleImpactField">
                        <span class="lblBText"><b>Schedule Impact</b></span>
                        <br />
                        <asp:Label ID="ScheduleImpacts" runat="server" CssClass="lblTextC" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtScheduleImpacts" runat="server" CssClass="textFieldStyle" Width="795px"
                            Height="18px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="otherImpactField">
                        <span class="lblBText"><b>Other Impact</b></span>
                        <br />
                        <asp:Label ID="OtherImpact" runat="server" CssClass="lblTextC" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtOtherImpact" runat="server" CssClass="textFieldStyle" Width="795px"
                            Height="18px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="clearDiv" />
                </div>
            </div>
            <div class="signPanel">
                <div id="TselSignaturePanel" style="margin-top: 5px;">
                    <div class="subpanelheader">
                        <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/TselAuthorization.png"
                            width="800px" alt="tselAuthorization" />
                    </div>
                    <div>
                        <table cellpadding="0" cellspacing="1" width="100%" border="1">
                            <asp:Repeater ID="RptDigitalSignTelkomsel" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <td style="width: 300px; text-align: center;">
                                            <span class="lblTextC">Title</span>
                                        </td>
                                        <td style="width: 250px; text-align: center;">
                                            <span class="lblTextC">Name</span>
                                        </td>
                                        <td style="width: 200px; text-align: center;">
                                            <span class="lblTextC">Sign</span>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="lblTextC" style="vertical-align: middle;">
                                            &nbsp;
                                            <%#Container.DataItem("SignTitle")%>
                                        </td>
                                        <td class="lblTextC" style="vertical-align: middle;">
                                            &nbsp;
                                            <%#Container.DataItem("name")%>
                                        </td>
                                        <td id="DgSign" runat="server" style="height: 35px; text-align: left;">
                                            <div class="clearSpace">
                                                &nbsp;</div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
                <div id="NSNSignaturePanel" style="margin-top: 5px;">
                    <div class="subpanelheader">
                        <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/VendorAuthorization.png"
                            width="800px" alt="tselAuthorization" />
                    </div>
                    <div>
                        <table cellpadding="0" cellspacing="1" width="100%" border="1">
                            <asp:Repeater ID="RptDigitalSignNSN" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <td style="width: 300px; text-align: center;">
                                            <span class="lblTextC">Title</span>
                                        </td>
                                        <td style="width: 250px; text-align: center;">
                                            <span class="lblTextC">Name</span>
                                        </td>
                                        <td style="width: 200px; text-align: center;">
                                            <span class="lblTextC">Sign</span>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="lblTextC" style="vertical-align: middle;">
                                            &nbsp;
                                            <%#Container.DataItem("SignTitle")%>
                                        </td>
                                        <td class="lblTextC" style="vertical-align: middle;">
                                            &nbsp;
                                            <%#Container.DataItem("name")%>
                                        </td>
                                        <td id="DgSign" runat="server" style="height: 35px; text-align: left;">
                                            <div class="clearSpace">
                                                &nbsp;</div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="PageBreak">
        </div>
        <div id="dvPrintDescriptionofChange" runat="server" style="width: 800px; border-style: solid;
            border-width: 0px;">
            <div id="headerPanel2" style="margin-top: 15px; width: 100%; border-bottom-style: solid;
                border-bottom-color: Black; border-bottom-width: 2px; padding-bottom: 2px;">
                <table cellpadding="0" cellspacing="0" width="99%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/nsn-logo.gif" alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="bottom" style="width: 55%">
                            <span class="lblTitle">Form Change Order</span>
                        </td>
                        <td align="right" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/logo_tsel.png" alt="tsellogo" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Div1" style="margin-top: 10px; border-bottom-width: 0px; border-bottom-style: solid;
                border-bottom-color: Black; padding-bottom: 5px;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/Images/CRCOSubHeader/Description.png"
                        width="795px" alt="DescofChange" />
                </div>
                <div class="descdetailfield">
                    <asp:GridView ID="gvDetails" DataKeyNames="COList_Id" runat="server" AutoGenerateColumns="false"
                        HeaderStyle-CssClass="HeaderGrid" ShowFooter="false" HeaderStyle-Font-Bold="true"
                        BorderColor="black" BorderStyle="Solid" Width="795px">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderStyle-CssClass="lblBHText" ItemStyle-CssClass="lblBTextPrice"
                                ItemStyle-Width="195px" HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="white"
                                HeaderStyle-BorderStyle="none" HeaderStyle-BorderWidth="0px" />
                            <asp:BoundField DataField="Contract_Type" HeaderStyle-CssClass="lblBHText" ItemStyle-CssClass="lblTextC1"
                                ItemStyle-Width="150px" HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="black"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px" HeaderText="Type" />
                            <asp:BoundField DataField="Contract_Configuration" HeaderStyle-CssClass="lblBHText"
                                ItemStyle-CssClass="lblTextC1" ItemStyle-Width="150px" HeaderStyle-VerticalAlign="Top"
                                ItemStyle-BorderColor="black" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px"
                                HeaderStyle-BorderColor="black" HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px"
                                HeaderText="Configuration" />
                            <asp:BoundField DataField="CR_Type" HeaderStyle-CssClass="lblBHText" ItemStyle-CssClass="lblTextC1"
                                ItemStyle-Width="150px" HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="black"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px" HeaderText="Type" />
                            <asp:BoundField DataField="CR_Configuration" HeaderStyle-CssClass="lblBHText" ItemStyle-CssClass="lblTextC1"
                                ItemStyle-Width="150px" HeaderStyle-VerticalAlign="Top" ItemStyle-BorderColor="black"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" HeaderStyle-BorderColor="black"
                                HeaderStyle-BorderStyle="solid" HeaderStyle-BorderWidth="1px" HeaderText="Configuration" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="JustificationChangePanel" style="margin-top: 5px; border-top-width: 0px;
                    border-top-style: solid; border-top-color: Black;">
                    <table width="100%">
                        <tr valign="top">
                            <td style="width: 40%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px; margin-top: 5px;">
                                    <span class="lblBText">Justification / Reason for change :</span>
                                </div>
                                <div style="margin-left: 15px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Regulatory Requirement</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" id="ChkRegulatoryRequirement" runat="server" disabled="disabled"
                                                    class="lblTextC" /></td>
                                            <td>
                                                <span class="lblTextC">Site Condition</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" id="ChkSiteCondition" runat="server" disabled="disabled" class="lblTextC" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Design Change</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" id="ChkDesignChange" runat="server" disabled="disabled" class="lblTextC" /></td>
                                            <td>
                                                <span class="lblTextC">Technical Error/Omission</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" id="ChkTechnicalError" runat="server" disabled="disabled"
                                                    class="lblTextC" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Other</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" id="ChkOther" runat="server" disabled="disabled" class="lblTextC" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 60%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px; margin-top: 5px;">
                                    <span class="lblBText">Description / Comment</span>
                                </div>
                                <div style="margin-left: 2px; height: 85px;">
                                    <asp:Label ID="Description_JustificationComments" runat="server" CssClass="lblText"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="dvGeneratePanel" runat="server" style="margin-top: 5px; width: 800px; text-align: center;">
            <asp:Label ID="LblErrorMessage" runat="server"></asp:Label>
            <asp:LinkButton ID="LbtEditCR" Width="60px" runat="server" OnClientClick="return confirm('Do you  want to Edit this CR Form ?')"
                Style="text-decoration: none;">
                                    <div class="btnEditCR"></div>                    
            </asp:LinkButton>
            <asp:LinkButton ID="lbtGeneratePDFForm" Width="120px" runat="server" Style="text-decoration: none;"
                OnClientClick="return confirm('Are you sure you want to generate to PDF ?')">
                                    <div class="GeneratePDFForm"></div>                    
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
