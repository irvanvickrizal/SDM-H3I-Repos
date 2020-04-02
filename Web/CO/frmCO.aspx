<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCO.aspx.vb" Inherits="CO_frmCO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <title>CO Form</title>
    <style type="text/css">
     .HeaderGrid
    {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        font-weight: bold;
        color:white;
        background-color:Orange;
        border-color:black;
        border-style:solid;
        border-width:1px;
        vertical-align:middle;
    }
    .oddGrid
    {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color: White;
        border-color:black;
        border-style:solid;
        border-width:1px;
    }
    .evenGrid
    {
        font-family: Arial Unicode MS;
        font-size: 9pt;
        background-color:#cfcfcf;
        border-color:black;
        border-style:solid;
        border-width:1px;
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
        .clearDiv
        {
            height:5px;
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

    <script type="text/javascript">
    function ConfirmationBox(desc) {
            var result = confirm('Are you sure you want to delete '+desc+' ?' );
            if (result) {
            return true;
            }
            else {
            return false;
        }
    }
    function ConfirmationPDFGenerate()
    {
        var totalBalanceIDR = document.getElementById("<%=LblTotalBalanceIDR.ClientID %>");
        var totalBalanceUSD = document.getElementById("<%=LblTotalBalanceUSD.ClientID %>");
        var desc = "";
        if (totalBalanceIDR.value != 0 && totalBalanceUSD.value != 0)
        {
            desc = " With different Indicative Price (IDR,USD) ";
        }
        if (totalBalanceIDR.value != 0 && totalBalanceUSD.value == 0)
        {
            desc = " With different Indicative Price (IDR) ";
        }
        if (totalBalanceIDR.value == 0 && totalBalanceUSD.value != 0)
        {
            desc = " With different Indicative Price (USD) ";
        }
        var result = confirm('Save and Proceed this CO' + desc + ' as PDF Form ?');
        if (result) {
            return true;
            }
            else {
            return false;}
    }
    function ConfirmationTest()
    {
        alert("Test");
        return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="HdnCOID" runat="server" />
        <asp:HiddenField ID="HdnSiteId" runat="server" />
        <asp:HiddenField ID="HdnVersion" runat="server" />
        <div style="width: 100%;">
            <div class="HeaderPanel">
                <div style="margin-left: 10px;">
                    Change Order Form
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
                                            <span class="lblCheckText">Change Order Number</span>
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
            <div id="DescriptionChangePanel" style="margin-top: 10px; border-bottom-width: 1px;
                border-bottom-style: solid; border-bottom-color: Black; padding-bottom: 5px;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Description of Proposed Change and Justification</span>
                </div>
                <div style="margin-top: 10px; width: 100%; margin-left: 30px;">
                    <asp:UpdatePanel ID="UpDetails" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvDetails" DataKeyNames="COList_Id" runat="server" AutoGenerateColumns="false"
                                HeaderStyle-CssClass="HeaderGrid" ShowFooter="true" HeaderStyle-Font-Bold="true"
                                OnRowCancelingEdit="gvDetails_RowCancelingEdit" EmptyDataText="No Data Found"
                                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing" OnRowUpdating="gvDetails_RowUpdating"
                                OnRowCommand="gvDetails_RowCommand">
                                <RowStyle CssClass="oddGrid" />
                                <AlternatingRowStyle CssClass="evenGrid" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                ToolTip="Update" Height="16px" Width="16px" />
                                            <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                ToolTip="Cancel" Height="16px" Width="16px" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                                ToolTip="Edit" Height="16px" Width="16px" />
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                                ToolTip="Delete" Height="16px" Width="16px" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new User" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("Description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblBText" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtftDescription" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvftDescription" runat="server" ControlToValidate="txtftDescription"
                                                Text="*" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-ForeColor="black">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtContractType" runat="server" Text='<%#Eval("Contract_Type") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractType" runat="server" Text='<%#Eval("Contract_Type") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtftContractType" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvftContractType" runat="server" ControlToValidate="txtftContractType"
                                                Text="*" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Configuration" HeaderStyle-ForeColor="black">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtContractConfiguration" runat="server" Text='<%#Eval("Contract_Configuration") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblContractConfiguration" runat="server" Text='<%#Eval("Contract_Configuration") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtftContractConfiguration" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvContractConfiguration" runat="server" ControlToValidate="txtftContractConfiguration"
                                                Text="*" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" HeaderStyle-ForeColor="black">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCRType" runat="server" Text='<%#Eval("CR_Type") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRType" runat="server" Text='<%#Eval("CR_Type") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtftCRType" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvftCRType" runat="server" ControlToValidate="txtftCRType"
                                                Text="*" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Configuration" HeaderStyle-ForeColor="black">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCRConfiguration" runat="server" Text='<%#Eval("CR_Configuration") %>' />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCRConfiguration" runat="server" Text='<%#Eval("CR_Configuration") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtftCRConfiguration" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvCRConfiguration" runat="server" ControlToValidate="txtftCRConfiguration"
                                                Text="*" ValidationGroup="validation" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="LblResult" runat="server" CssClass="lblText" ForeColor="red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvDetails" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="JustificationChangePanel" style="margin-top: 5px; border-top-width: 1px;
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
            <div id="ImpactPanel" style="margin-top: 5px; border-bottom-width: 1px; border-bottom-style: solid;
                border-bottom-color: Black; margin-bottom: 5px;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Impacts</span>
                </div>
                <div>
                    <table width="100%">
                        <tr valign="top">
                            <td style="width: 40%; border-style: solid; border-color: Black; border-width: 1px;">
                                <div style="margin-left: 15px;">
                                    <div>
                                        <span class="lblCheckText"><b>Technical Impact</b></span>
                                        <br />
                                        <asp:TextBox ID="txtTechnicalImpact" runat="server" Width="99%" Height="45px" CssClass="lblText"></asp:TextBox>
                                    </div>
                                    <div style="margin-bottom: 20px;">
                                        <span class="lblCheckText"><b>Budget Impact</b></span>
                                        <br />
                                        <asp:UpdatePanel ID="upBudgetImpact" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBudgetImpact" DataKeyNames="COBudget_Id" runat="server" AutoGenerateColumns="false"
                                                    HeaderStyle-CssClass="HeaderGrid" ShowFooter="true" HeaderStyle-Font-Bold="true"
                                                    OnRowCancelingEdit="GvBudgetImpact_RowCancelingEdit" EmptyDataText="No Data Found"
                                                    OnRowDeleting="GvBudgetImpact_RowDeleting" OnRowEditing="GvBudgetImpact_RowEditing"
                                                    OnRowUpdating="GvBudgetImpact_RowUpdating" OnRowCommand="GvBudgetImpact_RowCommand">
                                                    <RowStyle CssClass="oddGrid" />
                                                    <AlternatingRowStyle CssClass="evenGrid" />
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white">
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                                    ToolTip="Update" Height="16px" Width="16px" />
                                                                <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                                    ToolTip="Cancel" Height="16px" Width="16px" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                                                    ToolTip="Edit" Height="16px" Width="16px" />
                                                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/gridview/delete.jpg"
                                                                    ToolTip="Delete" Height="16px" Width="16px" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                                    CommandName="AddNew" Width="20px" Height="20px" ToolTip="Add new User" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HOT/HOG Number" HeaderStyle-ForeColor="black">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtPONO" runat="server" Text='<%#Eval("PONO") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemPONO" runat="server" Text='<%#Eval("PONO") %>' CssClass="lblBText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtftPONO" runat="server" />
                                                                <asp:RequiredFieldValidator ID="rfvftPONO" runat="server" ControlToValidate="txtftPONO"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short Description" HeaderStyle-ForeColor="black">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDescription" runat="server" Text='<%#Eval("Description") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemDesc" runat="server" Text='<%#Eval("Description") %>' CssClass="lblBText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtftDescription" runat="server" />
                                                                <asp:RequiredFieldValidator ID="rfvftDescription" runat="server" ControlToValidate="txtftDescription"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtContractUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Contract_USD") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblContractUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Contract_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <input type="text" id="txtftContractUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    style="height: 14px; width: 100px; text-align: right;" />
                                                                <asp:RequiredFieldValidator ID="rfvftContractUSD" runat="server" ControlToValidate="txtftContractUSD"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtContractIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Contract_IDR") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblContractIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Contract_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <input type="text" id="txtftContractIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    style="height: 14px; width: 100px; text-align: right;" />
                                                                <asp:RequiredFieldValidator ID="rfvContractIDR" runat="server" ControlToValidate="txtftContractIDR"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtCRUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("CR_USD") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblCRUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("CR_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <input type="text" id="txtftCRUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    style="height: 14px; width: 100px; text-align: right;" />
                                                                <asp:RequiredFieldValidator ID="rfvftCRUSD" runat="server" ControlToValidate="txtftCRUSD"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtCRIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("CR_IDR") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblCRIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("CR_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <input type="text" id="txtftCRIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    style="height: 14px; width: 100px; text-align: right;" />
                                                                <asp:RequiredFieldValidator ID="rfvCRIDR" runat="server" ControlToValidate="txtftCRIDR"
                                                                    Text="*" ValidationGroup="validation2" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtDeltaUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Delta_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                    border-style: none; background-color: Transparent; text-align: right;" class="lblText" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblDeltaUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Delta_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                    border-style: none; background-color: Transparent; text-align: right;" class="lblText" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="LblftDeltaUSD" runat="server" CssClass="lblText" Width="100px"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Right">
                                                            <EditItemTemplate>
                                                                <input type="text" id="txtDeltaIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Delta_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <input type="text" id="lblDeltaIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                    value='<%#Eval("Delta_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                    text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                <asp:Label ID="lbDeltaUSD" runat="server" Text='<%#Eval("Delta_USD") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbDeltaIDR" runat="server" Text='<%#Eval("Delta_IDR") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="LblftDeltaIDR" runat="server" CssClass="lblText" Width="100px"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblResultBudgetImpact" runat="server" CssClass="lblText"></asp:Label>
                                                <div>
                                                    <div style="display: none;">
                                                        <table border="1" cellpadding="1" cellspacing="2" width="60%">
                                                            <tr>
                                                                <td colspan="2" align="center" style="background-color: orange; color: White">
                                                                    <span class="lblBText" style="color: White">Indicative Price</span>
                                                                </td>
                                                                <td colspan="2" align="center" style="background-color: orange; color: White">
                                                                    <span class="lblBText" style="color: White">Total Delta</span>
                                                                </td>
                                                                <td colspan="2" align="center" style="background-color: orange; color: White">
                                                                    <span class="lblBText" style="color: White">Balance</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 10%; background-color: orange;">
                                                                    <span class="lblBText">USD</span>
                                                                </td>
                                                                <td align="center" style="width: 15%; background-color: orange;">
                                                                    <span class="lblBText">IDR</span>
                                                                </td>
                                                                <td align="center" style="width: 10%; background-color: orange;">
                                                                    <span class="lblBText">USD</span>
                                                                </td>
                                                                <td align="center" style="width: 15%; background-color: orange;">
                                                                    <span class="lblBText">IDR</span>
                                                                </td>
                                                                <td align="center" style="width: 10%; background-color: orange;">
                                                                    <span class="lblBText">USD</span>
                                                                </td>
                                                                <td align="center" style="width: 15%; background-color: orange;">
                                                                    <span class="lblBText">IDR</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="LblIndicativePriceUSD" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblIndicativePriceIDR" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="LblTotalDeltaUSD" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="LblTotalDeltaIDR" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="LblTotalBalanceUSD" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="LblTotalBalanceIDR" runat="server" CssClass="lblBText"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvBudgetImpact" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpPercentagePanel" runat="server">
                                            <ContentTemplate>
                                                <div style="margin-left: 190px;">
                                                    <asp:GridView ID="GvPercentageImpact" DataKeyNames="PercChange_Id" runat="server"
                                                        AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid" ShowFooter="False"
                                                        HeaderStyle-Font-Bold="true" OnRowCancelingEdit="GvPercentageImpact_RowCancelingEdit"
                                                        EmptyDataText="No Data Found" OnRowEditing="GvPercentageImpact_RowEditing" OnRowUpdating="GvPercentageImpact_RowUpdating">
                                                        <RowStyle CssClass="oddGrid" />
                                                        <AlternatingRowStyle CssClass="evenGrid" />
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="false" HeaderStyle-BackColor="white">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Images/gridview/update.jpg"
                                                                        ToolTip="Update" Height="16px" Width="16px" />
                                                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                                        ToolTip="Cancel" Height="16px" Width="16px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/gridview/Edit.jpg"
                                                                        ToolTip="Edit" Height="16px" Width="16px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="black">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtftDescPercentage" Text="Percentage Price Change" Style="border-style: none;
                                                                        border-color: #ffffff;" runat="server" ReadOnly="true" CssClass="lblBText" Width="159px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtContractUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Contract_USD") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblContractUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Contract_USD") %>' style="height: 14px; width: 120px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent; text-align: right;"
                                                                        class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtContractIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Contract_IDR") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblContractIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Contract_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtCRUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Implementation_USD") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblCRUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Implementation_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtCRIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Implementation_IDR") %>' style="height: 14px; width: 100px; text-align: right;" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblCRIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Implementation_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="USD" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtDeltaUSD" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Delta_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right;" class="lblText" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblDeltaUSD" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Delta_USD") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="IDR" HeaderStyle-ForeColor="black">
                                                                <EditItemTemplate>
                                                                    <input type="text" id="txtDeltaIDR" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Delta_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right;" class="lblText" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <input type="text" id="lblDeltaIDR" readonly="readonly" runat="server" onblur="this.value=formatusdCurrency(this.value);"
                                                                        value='<%#Eval("Delta_IDR") %>' style="height: 14px; width: 100px; color: Black;
                                                                        text-align: right; border-style: none; background-color: Transparent;" class="lblText" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvPercentageImpact" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div>
                                        <span class="lblCheckText"><b>Schedule Impact</b></span>
                                        <br />
                                        <asp:TextBox ID="TxtScheduleImpact" runat="server" Width="99%" Height="45px" CssClass="lblText"></asp:TextBox>
                                    </div>
                                    <div>
                                        <span class="lblCheckText"><b>Other Impact</b></span>
                                        <br />
                                        <asp:TextBox ID="TxtOtherImpact" runat="server" Width="99%" Height="45px" CssClass="lblText"></asp:TextBox>
                                    </div>
                                    <div class="clearDiv" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="AdditionalDocumentPanel" style="margin-top: 5px; border-bottom-width: 1px;
                border-bottom-style: solid; border-bottom-color: Black; margin-bottom: 5px;">
                <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                    padding-left: 10px;">
                    <span class="lblSubHeader">Additional Document Support</span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpDocAttachment" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table width="100%">
                                <tr valign="top">
                                    <td style="width: 35%;">
                                        <div style="margin-top: 5px; margin-bottom: 5px;">
                                            <asp:GridView ID="GvDocAttachment" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid">
                                                <RowStyle CssClass="oddGrid" />
                                                <AlternatingRowStyle CssClass="evenGrid" Font-Size="7.5pt" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="7.5pt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                                CommandName="AddDoc" Width="20px" Height="20px" ToolTip="Add Supporting Document"
                                                                OnClientClick="return confirm('Are you sure want to add this attachment Document ?')"
                                                                CommandArgument='<%#Eval("docregid") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td style="width: 65%;">
                                        <div style="margin-top: 5px; margin-bottom: 5px;">
                                            <asp:MultiView ID="MvCoreUploadDocAttachment" runat="server">
                                                <asp:View ID="VwListDocUpload" runat="server">
                                                    <asp:GridView ID="GvUploadDocAttachment" runat="server" AutoGenerateColumns="false"
                                                        HeaderStyle-CssClass="HeaderGrid" EmptyDataText="No Need Supporting Doc Upload">
                                                        <RowStyle CssClass="oddGrid" />
                                                        <AlternatingRowStyle CssClass="evenGrid" Font-Size="7.5pt" />
                                                        <EmptyDataRowStyle CssClass="evenGrid" Font-Size="7.5pt" ForeColor="black" Font-Bold="true" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No." ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="7.5pt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="LblDocName" runat="server" Text='<%#Eval("DocName") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:Label ID="Label1" runat="server" Text="Document Not Yet Uploaded" Visible='<%# IIf(Boolean.Parse(Eval("IsUploaded")),"false","true") %>'
                                                                            CssClass="lblTextC" ForeColor="green"></asp:Label>
                                                                        <asp:Label ID="LblWarningMessage" runat="server" Text="Document Already Uploaded"
                                                                            Visible='<%#Eval("IsUploaded") %>' CssClass="lblTextC" ForeColor="green"></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href='../PO/frmViewCODocument.aspx?swid=<%#Eval("docregid") %> &doctype=supportdoc'
                                                                        target="_blank" style="text-decoration: none; border-style: none;" title="View Document">
                                                                        <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="16" width="16"
                                                                            runat="server" visible='<%#IIf(Boolean.Parse(Eval("IsUploaded")),"true","false") %>'
                                                                            style="text-decoration: none; border-style: none;" />
                                                                    </a>
                                                                    <asp:ImageButton ID="ImgUploadDoc" CommandName="uploaddoc" CommandArgument='<%#Eval("docregid") %>'
                                                                        runat="server" ImageUrl="~/Images/file.gif" ToolTip="Upload Document" Height="16px"
                                                                        Width="16px" />
                                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="deletedoc" CommandArgument='<%#Eval("docregid") %>'
                                                                        runat="server" ImageUrl="~/Images/gridview/delete.jpg" OnClientClick="return confirm('Are you sure want to delete this attachment Document ?')"
                                                                        ToolTip="Delete" Height="16px" Width="16px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:View>
                                                <asp:View ID="vwDocUpload" runat="server">
                                                    <div>
                                                        <asp:HiddenField ID="HdnSWID" runat="server" />
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblDocName" runat="server" CssClass="lblTextC"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="FuDocument" runat="server" /><asp:LinkButton ID="LbtUploadDocSupport"
                                                                        runat="server" Text="Upload"></asp:LinkButton>
                                                                    <br />
                                                                    <asp:Label ID="LblErrorMessageUpload" runat="server" CssClass="lblTextC"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvUploadDocAttachment" />
                            <asp:AsyncPostBackTrigger ControlID="GvDocAttachment" />
                            <asp:PostBackTrigger ControlID="LbtUploadDocSupport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <asp:UpdatePanel ID="UpAuthorization" runat="server">
                <ContentTemplate>
                    <div id="AuthorizationPanel" style="margin-top: 5px; border-bottom-width: 1px; border-bottom-style: solid;
                        border-bottom-color: Black; margin-bottom: 5px;">
                        <div style="background-color: #c3c3c3; padding-top: 3px; padding-bottom: 3px; width: 99%;
                            padding-left: 10px;">
                            <span class="lblSubHeader">Authorization</span>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <span class="lblCheckText"><b>Workflow Type</b></span></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DdlWorkflowType" CssClass="lblCheckText" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="LblErrorMessageWorkflow" runat="server" CssClass="lblCheckText" ForeColor="red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="TselSignaturePanel" style="margin-top: 5px;">
                            <div class="subpanelheader">
                                <span class="lblSubHeader">Telkomsel Authorization</span>
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
                                                <td id="DgSign" runat="server" style="height: 40px; text-align: left;">
                                                    <div class="clearDiv">
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
                                <span class="lblSubHeader">Vendor Authorization</span>
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
                                                <td id="DgSign" runat="server" style="height: 40px; text-align: left;">
                                                    <div class="clearDiv">
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
        </div>
        <div>
            <div style="width: 100%; text-align: right; margin-top: 5px;">
                <asp:LinkButton ID="LbtSave" Width="60px" runat="server" OnClientClick="return ConfirmationPDFGenerate()"
                    Style="text-decoration: none;">
                                    <div class="btnSave"></div>                    
                </asp:LinkButton>
                <asp:LinkButton ID="lbtCancel" Width="60px" runat="server" Style="text-decoration: none;">
                                    <div class="btnCancel"></div>                    
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
