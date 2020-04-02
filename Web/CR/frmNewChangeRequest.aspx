<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNewChangeRequest.aspx.vb"
    Inherits="CR_frmNewChangeRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Form Change Request</title>
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
           width:100%;
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
        .lblCheckText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
        }
        
        .evengrid2
        {
            background-color: #cfcfcf;
        }
        .btnSave
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_0.gif);
        }
        .btnSave:hover
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_1.gif);
        }
        .btnSave:click
        {
            height:26px;
            width:60px;
            background-image: url(../Images/button/BtnSave_2.gif);
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
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%;">
            <div class="HeaderPanel">
                <div style="margin-left: 10px;">
                    Change Request Form
                </div>
            </div>
            <div id="AttributePanel" style="margin-top: 10px; border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: Black; padding-bottom: 5px;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">General Information</span>
                </div>
                <div>
                    <table width="100%">
                        <tr valign="top">
                            <td style="border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Change Request Number</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblChangeRequestNumber" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Site ID</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSiteID" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Site Name</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSiteName" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Area</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblArea" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border-style: solid; border-color: Black; border-width: 1px;">
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Contract Number</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblPONo" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">PO Name</span>
                                        </td>
                                        <td>
                                            <span class="lblText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblEOName" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Project Type</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblProjectType" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Project ID</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
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
                                            <span class="lblCheckText">Date Submitted</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="LblDateSubmitted" runat="server" CssClass="lblBText"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="lblCheckText">Project Category</span>
                                        </td>
                                        <td>
                                            <span class="lblCheckText">:</span>
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
            <div id="InitiatorPanel" style="margin-top: 5px; border-bottom-width: 1px; padding-bottom: 5px;
                border-bottom-style: solid; border-bottom-color: Black;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Initiator</span>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <span class="lblCheckText">Name</span>
                            </td>
                            <td>
                                <span class="lblText">:</span>
                            </td>
                            <td>
                                <asp:Label ID="LblInitiatorName" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblCheckText">Department</span>
                            </td>
                            <td>
                                <span class="lblCheckText">:</span>
                            </td>
                            <td>
                                <asp:Label ID="LblInitiatorDepartment" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblCheckText">Area</span>
                            </td>
                            <td>
                                <span class="lblCheckText">:</span>
                            </td>
                            <td>
                                <asp:Label ID="LblInitiatorArea" runat="server" CssClass="lblBText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="ProposedChangedPanel" style="margin-top: 5px; border-bottom-width: 1px;
                border-bottom-style: solid; border-bottom-color: Black;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Description of Proposed Change and Justification </span>
                </div>
                <div style="margin-left: 2px; margin-top: 2px;">
                    <asp:TextBox ID="TxtDescriptionOfChange" runat="server" Style="overflow: hidden;"
                        CssClass="lblText" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox>
                </div>
                <div style="margin-top: 5px; border-top-width: 1px; border-top-style: solid; border-top-color: Black;">
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
                                                <span class="lblCheckText">Regulatory Requirement</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkRegulatoryRequirement" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Site Condition</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkSiteCondition" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Design Change</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkDesignChange" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Technical Error/Omission</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkTechnicalError" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Other</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkOther" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 60%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px; margin-top: 5px;">
                                    <span class="lblBText">Description / Comment</span>
                                </div>
                                <div style="margin-left: 2px;">
                                    <asp:TextBox ID="TxtDescription_JustificationComments" Style="overflow: hidden;"
                                        runat="server" TextMode="MultiLine" Width="99%" Height="85px" CssClass="lblText"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="ImpactPanel" style="margin-top: 5px; border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: Black;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Impacts</span>
                </div>
                <div>
                    <table width="100%">
                        <tr valign="top">
                            <td style="width: 40%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Design Impact</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkDesignImpact" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">Budget Impact</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkBudgetImpact" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="lblCheckText">No Design and Budget Impact</span>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ChkNoImpact" runat="server" CssClass="lblText" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="width: 60%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 2px; margin-top: 5px;">
                                    <span class="lblBText">Indicative Budget / Cost Impacts :</span>
                                </div>
                                <div style="margin-left: 2px;">
                                    <table border="2" style="border-color: Black;">
                                        <tr style="border-color: Black;">
                                            <td style="border-style: none; border-width: 0px;">
                                            </td>
                                            <td style="border-color: Black;">
                                                <span class="lblCheckText">USD</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <span class="lblCheckText">IDR</span>
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblCheckText">Contract</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtContractUSD" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtContractIDR" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblCheckText">Implementation</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtImplementationUSD" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtImplementationIDR" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black;">
                                                <span class="lblCheckText">Indicative Price Cost</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtIndicativePriceCostUSD" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtIndicativePriceCostIDR" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                        </tr>
                                        <tr style="border-color: Black;">
                                            <td style="border-color: Black; width: 170px;">
                                                <span class="lblCheckText">Percentage Price Change</span>
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtPercentagePriceChangeUSD" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                            <td style="border-color: Black;">
                                                <input type="text" class="lblCheckText" onblur="this.value=formatusdCurrency(this.value);"
                                                    id="TxtPercentagePriceChangeIDR" runat="server" style="height: 14px; width: 150px;" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td style="width: 10%;">
                                <span class="lblCheckText">Schedule Impacts</span>
                            </td>
                            <td style="width: 1%;">
                                :</td>
                            <td style="width: 89%;">
                                <asp:TextBox ID="TxtScheduleImpacts" runat="server" CssClass="lblText" Height="14px"
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblCheckText">Other Impacts</span>
                            </td>
                            <td>
                                :</td>
                            <td>
                                <asp:Label ID="LblOtherImpacts" runat="server" CssClass="lblText"></asp:Label>
                                <asp:TextBox ID="TxtOtherImpacts" runat="server" CssClass="lblText" Height="14px"
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdateProgress DynamicLayout="true" AssociatedUpdatePanelID="UpSignPanel" runat="server">
                <ProgressTemplate>
                    <img src="~/images/animation_processing.gif" runat="server" id="ImgProcessing" alt="Loading" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpSignPanel" runat="server">
                <ContentTemplate>
                    <div id="DvWorkFlow" style="margin-top: 5px; padding-bottom: 5px; border-bottom-width: 1px;
                        border-bottom-style: solid; border-bottom-color: Black;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblWorkFlowType" runat="server" Text="WorkFlow Type" CssClass="lblCheckText"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlWorkflowType" runat="server" CssClass="lblCheckText" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="LblWorkFlowID" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="signPanel">
                        <div id="TselSignaturePanel" style="margin-top: 5px;">
                            <div class="subpanelheader">
                                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                                    padding-left: 10px;">
                                    <span class="lblSubHeader">Telkomsel Authorization</span>
                                </div>
                            </div>
                            <div>
                                <table cellpadding="0" cellspacing="1" width="100%" border="1">
                                    <asp:Repeater ID="RptDigitalSignTelkomsel" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="width: 300px; text-align: center;">
                                                    <span class="lblCheckText">Title</span>
                                                </td>
                                                <td style="width: 250px; text-align: center;">
                                                    <span class="lblCheckText">Name</span>
                                                </td>
                                                <td style="width: 200px; text-align: center;">
                                                    <span class="lblCheckText">Sign</span>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="lblCheckText" style="vertical-align: middle;">
                                                    &nbsp;
                                                    <%#Container.DataItem("SignTitle")%>
                                                </td>
                                                <td class="lblCheckText" style="vertical-align: middle;">
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
                        <div id="NSNSignaturePanel" style="margin-top: 5px; border-bottom-width: 0px; border-bottom-style: none;
                            border-bottom-color: Black;">
                            <div class="subpanelheader">
                                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                                    padding-left: 10px;">
                                    <span class="lblSubHeader">Vendor Authorization</span>
                                </div>
                            </div>
                            <div>
                                <table cellpadding="0" cellspacing="1" width="100%" border="1">
                                    <asp:Repeater ID="RptDigitalSignNSN" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="width: 300px; text-align: center;">
                                                    <span class="lblCheckText">Title</span>
                                                </td>
                                                <td style="width: 250px; text-align: center;">
                                                    <span class="lblCheckText">Name</span>
                                                </td>
                                                <td style="width: 200px; text-align: center;">
                                                    <span class="lblCheckText">Sign</span>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="lblCheckText" style="vertical-align: middle;">
                                                    &nbsp;
                                                    <%#Container.DataItem("SignTitle")%>
                                                </td>
                                                <td class="lblCheckText" style="vertical-align: middle;">
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DdlWorkflowType" />
                </Triggers>
            </asp:UpdatePanel>
            <div>
                <div style="width: 100%; text-align: right; margin-top: 5px;">
                    <asp:LinkButton ID="LbtSave" Width="60px" runat="server" OnClientClick="return confirm('Do you  want to Save and Generate to PDF this CR Form ?')"
                        Style="text-decoration: none;">
                                    <div class="btnSave"></div>                    
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtCancel" Width="60px" runat="server" Style="text-decoration: none;">
                                    <div class="btnCancel"></div>                    
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
