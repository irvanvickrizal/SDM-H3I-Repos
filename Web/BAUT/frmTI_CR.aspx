<%@ Page Language="VB" AutoEventWireup="true" CodeFile="frmTI_CR.aspx.vb" Inherits="BAUT_frmTI_CR"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Request Form</title>
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <style type="text/css">
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
        .lblTextC
        {
            font-family: Arial Unicode MS;
            font-size: 6.5pt;
            color: #000000;
            text-align: left;
        }
        .btnEditCR
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/btnEditCR_0.gif);
        }
        .btnEditCR:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/btnEditCR_1.gif);
        }
        .btnEditCR:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/btnEditCR_2.gif);
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
            height:300px;
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
    <form id="form1" runat="server" method="post">
        <div id="dvPrint" runat="server" style="width: 800px; height: 1100px; border-style: none;
            border-color: Black; border-width: 0px;">
            <div id="headerPanel" style="margin-top: 15px; width: 100%; border-bottom-style: none;
                border-bottom-color: Black; border-bottom-width: 2px; padding-bottom: 2px;">
                <table cellpadding="0" cellspacing="0" width="99%" border="0">
                    <tr>
                        <td align="left" valign="top" style="width: 20%">
                            <img src="http://nsndemo.nsnebast.com:1300/Images/nsn-logo.gif"
                                alt="nsnLogo" />
                        </td>
                        <td colspan="4" align="center" class="lblBold" valign="bottom" style="width: 55%">
                            <span class="lblTitle">Change Request Form</span>
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
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/GeneralInformation.png" alt="generalInformation" width="800px" />
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
            <div id="InitiatorPanel" style="margin-top: 5px; height: 55px; border-bottom-width: 0px;
                border-bottom-style: none; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/Initiator.png" alt="Initiator" width="800px" />
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
            <div id="ProposedChangedPanel" style="margin-top: 5px; height: 200px; border-bottom-width: 0px;
                border-bottom-style: none; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/Description.png" alt="Descofchange" width="800px" />
                </div>
                <div class="lblTextC" style="margin-left: 2px; margin-top: 2px; height: 90px">
                    <asp:TextBox ID="TxtDescriptionOfChange" runat="server" Style="overflow: hidden;"
                        CssClass="lblTextC" TextMode="MultiLine" Width="790px" Height="60px" Visible="false"></asp:TextBox>
                    <asp:Label ID="LblDescriptionofChange" runat="server" CssClass="lblTextC"></asp:Label>
                </div>
                <div style="margin-top: 5px; border-top-width: 0px; border-top-style: none; border-top-color: Black;">
                    <table width="800px">
                        <tr valign="top">
                            <td style="width: 350px; border-style: solid; border-color: Black; border-width: 1px;">
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
                                                <input type="checkbox" runat="server" id="ChkRegulatoryRequirement" disabled="disabled" />
                                            </td>
                                            <td>
                                                <span class="lblTextC">Site Condition</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkSiteCondition" disabled="disabled" />
                                            </td>
                                            <td>
                                                <span class="lblTextC">Design Change</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkDesignChange" disabled="disabled" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblTextC">Technical Error/Omission</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkTechnicalError" disabled="disabled" />
                                            </td>
                                            <td>
                                                <span class="lblTextC">Other</span>
                                            </td>
                                            <td>
                                                <input type="checkbox" runat="server" id="ChkOther" disabled="disabled" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 450px; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px; margin-top: 5px;">
                                    <span class="lblBText">Description / Comment</span>
                                </div>
                                <div style="margin-left: 2px; height: 55px;">
                                    <asp:TextBox ID="TxtDescription" Style="overflow: hidden;" runat="server" TextMode="MultiLine"
                                        Width="290px" CssClass="lblTextC" Visible="false"></asp:TextBox>
                                    <asp:Label ID="LblJustificationComments" runat="server" CssClass="lblTextC"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="ImpactPanel" style="margin-top: 5px; height: 200px; border-bottom-width: 0px; border-bottom-style: none;
                border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/Impact.png" alt="impacts" width="800px" />
                </div>
                <div>
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
            <div class="signPanel">
            <div id="TselSignaturePanel" style="margin-top: 5px;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/TselAuthorization.png" alt="TselAuthorization" width="800px" />
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
            <div id="NSNSignaturePanel" style="margin-top: 5px;border-bottom-width: 0px;
                border-bottom-style: none; border-bottom-color: Black;">
                <div class="subpanelheader">
                    <img src="http://nsndemo.nsnebast.com:1300/images/CRCOSubHeader/VendorAuthorization.png" alt="VendorAuthorization"  width="800px"/>
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
        <asp:Label ID="LblErrorMessage" runat="server"></asp:Label>
        <div id="dvGeneratePanel" runat="server" style="margin-top: 5px; width: 800px; text-align: center;">
            <asp:HiddenField ID="hdnWFID" runat="server" />
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
