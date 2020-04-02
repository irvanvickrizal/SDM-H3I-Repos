<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_FormWCCDone.aspx.vb"
    Inherits="fancybox_Form_fb_FormWCCDone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form WCC</title>
    <style type="text/css">
        .headerTitle
        {
            font-family:Verdana;
            font-size:18px;
            font-weight:bolder;
        }
        .headerPanel
        {
            padding:3px;
            border-bottom-color:#000;
            border-bottom-width:2px;
            border-bottom-style:Solid;
            width:100%;
        }
        .labelText
        {
            font-family:verdana;
            font-size:11px;
            text-align:left;
        }
        .labelFieldText
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            text-align:left;
        }
        .formPanel
        {
            width:800px;
            text-align:center;
            margin-top:10px;
        }
        .panelDetail
        {
            margin-top:20px;
            width: 95%;
            padding: 30px; 
            background: #FFF;
            border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
            box-shadow: 0px 0px 4px rgba(0,0,0,0.7); -webkit-box-shadow: 0 0 4px rgba(0,0,0,0.7); -moz-box-shadow: 0 0px 4px rgba(0,0,0,0.7);
            z-index:13000;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panelDetail" style="width: 820px;">
            <div class="headerPanel">
                <table width="100%;">
                    <tr>
                        <td style="width: 30%;">
                            <img src="http://localhost/images/NSNLogo_New.png" id="NSNLogoImg" alt="NSNLogo" />
                        </td>
                        <td style="width: 69%;">
                            <span class="headerTitle">Work Completion Certificate</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 5px; text-align: center;">
                <p class="labelText" style="text-align: center; font-family:Verdana; font-size:11px;">
                    This is to officially certify that the subcontractor has accomplished the assigned
                    works according to the purchase order completely / in full</p>
                <p class="labelText" style="text-align: center; font-family:Verdana; font-size:11px;">
                    all required documentations have been submitted and approved by respective NSN PIC(Person
                    In Charge)</p>
                <asp:ScriptManager ID="SM1" runat="server">
                </asp:ScriptManager>
                <asp:HiddenField ID="HdnPackageId" runat="server" />
                <asp:HiddenField ID="HdnIsRejected" runat="server" />
            </div>
            <div class="formPanel">
                <table cellpadding="2" border="2" cellspacing="2" width="70%" style="margin: 0 auto;">
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Subcontractor Name</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplaySubcontractorName" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">WCC Issuance Date</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayWCCIssuanceDate" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Certificate Number</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayCertificateNumber" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Site ID (EPM)</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplaySiteIDEPM" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Site Name (EPM)</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplaySiteNameEPM" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Work Package ID (EPM)</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayWorkpackageID" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Work Description</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayWorkDescription" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">PO Subcontractor</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayPOSubcontractor" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">PO Telkomsel</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayPOTelkomsel" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">Telkomsel BAUT/BAST Date</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayBAUTBASTDate" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span class="labelFieldText">WCTR Date</span>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="DisplayWCTRDate" CssClass="labelText" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 10px; border-bottom-color: #000; border-bottom-style: dashed;
                border-bottom-width: 1px; padding-bottom: 5px;">
                <table>
                    <tr valign="top">
                        <td>
                            <div>
                                <span class="labelFieldText">SIS/SITAC</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkSIS_Sitac" runat="server" Enabled="false" /><span class="labelText">SIS/SES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkPKS_AJB_50Perc" runat="server" Enabled="false" /><span class="labelText">50%
                                        PKS/AJB</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkIMB_50Perc" runat="server" Enabled="false" /><span class="labelText">50%
                                        IMB</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkCA_LC_100Perc" runat="server" Enabled="false" /><span class="labelText">100%
                                        CA/LC</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSITAC_Permitting" runat="server" Enabled="false" /><span class="labelText">SITAC/Permitting</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">CME</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="Chk2G_3G_BAUT" runat="server" Enabled="false" /><span class="labelText">2G/3G
                                        BAUT</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSDH_PDH" runat="server" Enabled="false" /><span class="labelText">SDH/PDH</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="Chk2G_CME_BAST" runat="server" Enabled="false" /><span class="labelText">2G
                                        CME BAST</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkAdditional" runat="server" Enabled="false" /><span class="labelText">ADDITIONAL</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">TI</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkSurvey" runat="server" Enabled="false" /><span class="labelText">SURVEY</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDismantling" runat="server" Enabled="false" /><span class="labelText">DISMANTLING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkReconfig" runat="server" Enabled="false" /><span class="labelText">RECONFIG</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkEnclosure" runat="server" Enabled="false" /><span class="labelText">ENCLOSURE</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkServices" runat="server" Enabled="false" /><span class="labelText">SERVICES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkFreq_License" runat="server" Enabled="false" /><span class="labelText">FREQ.LICENSE</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">NPO</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkInitial_Tuning" runat="server" Enabled="false" /><span class="labelText">INITIAL
                                        TUNING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkCluster_Tuning" runat="server" Enabled="false" /><span class="labelText">CLUSTER
                                        TUNING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkIBC" runat="server" Enabled="false" /><span class="labelText">IBC
                                        (In Building)</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkOptimization" runat="server" Enabled="false" /><span class="labelText">OPTIMIZATION</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSiteVerification" runat="server" Enabled="false" /><span class="labelText">SITE
                                        VERIFICATION</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">NPO</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkChange_Request" runat="server" Enabled="false" /><span class="labelText">CHANGE
                                        REQUEST</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDesign_For_MW_Access" runat="server" Enabled="false" /><span
                                        class="labelText">DESIGN FOR MW ACCESS MW</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkNPO_SDH_PDH" runat="server" Enabled="false" /><span class="labelText">SDH/PDH</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkNPO_SIS_SES" runat="server" Enabled="false" /><span class="labelText">SIS/SES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkHICAP_BSC_COLO_DCS" runat="server" Enabled="false" /><span class="labelText">HICAP/BSC/COLO/DCS</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDetailed_RF_Covered" runat="server" Enabled="false" /><span
                                        class="labelText">DETAILED RF COVERED AND CAPACITY NWP</span>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 5px;">
                    <p style="font-family: Verdana; font-size: 11px; font-weight: bolder; text-align: center;">
                        this WCC has been generated electronically from the eBAST-system and thus requires
                        no additional signatures from NSN person in charge. It serves as main supporting
                        document for invoicing of the respective purchase order.
                    </p>
                </div>
            </div>
            <div style="margin-top: 10px;" id="divApproval" runat="server" class="labelText">
            </div>
        </div>
    </form>
</body>
</html>
