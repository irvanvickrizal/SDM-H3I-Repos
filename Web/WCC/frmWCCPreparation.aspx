<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCCPreparation.aspx.vb"
    Inherits="WCC_frmWCCPreparation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.mousewheel-3.0.6.pack.js"></script>

    <!-- Add fancyBox main JS and CSS files -->

    <script type="text/javascript" src="../fancybox/jquery.fancybox.js?v=2.1.3"></script>

    <link rel="stylesheet" type="text/css" href="../fancybox/jquery.fancybox.css?v=2.1.2"
        media="screen" />
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/ValidationMessage.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/WCCForm.css" rel="stylesheet" type="text/css" />
    <title>WCC Preparation</title>
    <style type="text/css">
    #HeaderPanel
    {
	    width: 99%;
	    background-repeat: repeat-x;
	    background-color:#c5c3c3;
	    font-family: verdana;
	    font-weight: bolder;
	    font-size: 10pt;
	    color: white;
	    padding-top: 5px;
	    padding-bottom: 5px;
	    -moz-border-radius: 3px;
	    border-radius: 3px;
	    text-shadow: 0px 1px 1px #000;
    }
    .subheaderpanel
    {
        width: 99%;
	    background-color:#c5c3c3;
	    font-family: verdana;
	    font-weight: bolder;
	    font-size: 10pt;
	    color: #000;
	    padding-top: 5px;
	    padding-bottom: 5px;
	    text-shadow: 0px 1px 1px #000;
    }
    .gridHeader
    {
	    font-family:Verdana;
	    font-size:10pt;
	    background-color:Maroon;
	    padding:5px;
	    color:white;
    }
    .fancybox-custom .fancybox-skin
    {
        box-shadow: 0 0 50px #222;
    }
    .fancybox-title-inside {
        text-align: center;
        font-family:verdana;
        font-size:18px;
    }
    .gridHeader_2
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
    .gridOdd
    {
        font-family:Verdana;
	    font-size:11px;
	    padding:5px;
    }
    .gridEven
    {
        font-family:Verdana;
	    font-size:11px;
	    background-color:#cfcfcf;
	    font-weight:bolder;
	    text-align:center;
	    padding:5px;
    }
    </style>

    <script type="text/javascript">
			$(function() {
    			$('.fancybox').fancybox({
    			    width:500,
    			    height:800,   
    			    scrolling : 'No',     
    			    helpers: {
					    title : {
						    type : 'inside'
					    },
					    overlay : {
					        transitionOut :'elastic',
					        speedIn : 600,
						    speedOut : 300
					    }
				    }
    			});
    			
    			$('.fancyboxViewDocument').fancybox({
    			    width:850,
    			    height:900,   
    			    helpers: {
					    title : {
						    type : 'inside'
					    },
					    overlay : {
					        transitionOut :'elastic',
					        speedIn : 600,
						    speedOut : 300
					    }
				    }
    			});
			});
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div id="HeaderPanel">
            <asp:HiddenField ID="HdnSubconId" runat="server" />
            <div style="margin-left: 10px;">
                WCC Preparation
            </div>
        </div>
        <div style="margin-top: 20px;">
            <div style="float: left; width: 48%; height: 300px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblSubconCompany" runat="server" CssClass="lblField" Text="Subcontractor Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSubconCompany" runat="server" CssClass="lblText" ReadOnly="true"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblWCCIssuanceDate" runat="server" CssClass="lblField" Text="WCC Issuance Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtWccIssuanceDate" runat="server" CssClass="lblText" Enabled="false"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblCertificateNumber" runat="server" CssClass="lblField" Text="Certificate Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCertificateNumber" runat="server" CssClass="lblText" Enabled="false"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblSiteID" runat="server" CssClass="lblField" Text="Site ID(EPM)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSiteID" runat="server" CssClass="lblText" ReadOnly="true" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblSiteName" runat="server" CssClass="lblField" Text="Site Name(EPM)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSiteName" runat="server" CssClass="lblText" ReadOnly="true" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblWorkPackageId" runat="server" CssClass="lblField" Text="Work Package ID(EPM)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtWorkpackageId" runat="server" CssClass="lblText" ReadOnly="true"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblWorkDescription" runat="server" CssClass="lblField" Text="Work Description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtWorkDescription" runat="server" CssClass="lblText" ReadOnly="true"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPOSubcontractor" runat="server" CssClass="lblField" Text="PO Subcontractor"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPOSubcontractor" runat="server" CssClass="lblText" Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RfvPOSubcontractor" runat="server" CssClass="dnnFormMessage dnnFormError"
                                    SetFocusOnError="true" Font-Names="Verdana" Font-Size="11px" ControlToValidate="TxtPOSubcontractor"
                                    ErrorMessage="Required" ValidationGroup="wcccreation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblPOTelkomsel" runat="server" CssClass="lblField" Text="PO Telkomsel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtPOTelkomsel" runat="server" CssClass="lblText" ReadOnly="true"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblBAUTDate" runat="server" CssClass="lblField" Text="Telkomsel BAUT/BAST Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBAUTBASTDate" runat="server" CssClass="lblText" ReadOnly="true"
                                    Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblWCTRDate" runat="server" CssClass="lblField" Text="WCTR Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtWCTRDate" runat="server" CssClass="lblText" Width="250px"></asp:TextBox>
                                <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" Visible="false" />
                                <cc1:CalendarExtender ID="ceStartTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="TxtWCTRDate"
                                    TargetControlID="TxtWCTRDate">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RfvWCTRDate" runat="server" CssClass="dnnFormMessage dnnFormError"
                                    SetFocusOnError="true" Font-Names="Verdana" Font-Size="11px" ControlToValidate="TxtWCTRDate"
                                    ErrorMessage="Required" ValidationGroup="wcccreation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="lblText">
                                <asp:Literal ID="LtrGeneralScope" runat="server">Master Scope</asp:Literal>
                            </td>
                            <td>
                                <div class="dnnFormItem">
                                    <asp:DropDownList ID="DdlGeneralScope" runat="server" CssClass="lblText" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:CompareValidator ID="CvGeneralScope" runat="server" ControlToValidate="DdlGeneralScope"
                                        Font-Names="Verdana" Font-Size="10px" CssClass="dnnFormMessage dnnFormError"
                                        SetFocusOnError="true" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required"
                                        ValidationGroup="wcccreation"></asp:CompareValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 3px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="lblText">
                                <asp:Literal ID="LtrScopeDetail" runat="server">Type of Work</asp:Literal>
                            </td>
                            <td>
                                <div class="dnnFormItem">
                                    <asp:UpdatePanel ID="UpScopeDetail" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DdlScopeDetail" runat="server" CssClass="lblText" ValidationGroup="wcccreation">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CvScopeDetail" runat="server" ControlToValidate="DdlScopeDetail"
                                                Font-Names="Verdana" Font-Size="10px" CssClass="dnnFormMessage dnnFormError"
                                                SetFocusOnError="true" ValueToCompare="0" Operator="GreaterThan" ErrorMessage="Required"
                                                ValidationGroup="wcccreation"></asp:CompareValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="DdlGeneralScope" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; width: 99%; text-align: right; margin-top: 5px;">
                    <hr />
                    <a id="aSiteInformation" runat="server" href="#" class="fancybox fancybox.iframe">Site
                        Information</a>
                    <asp:LinkButton ID="LbtUpdate" runat="server" Text="Update" CssClass="dnnPrimaryAction"
                        ValidationGroup="wcccreation"></asp:LinkButton>
                </div>
            </div>
            <div style="float: right; width: 45%">
                <div id="panelUploadDocument" runat="server">
                    <asp:HiddenField ID="hdnWCCID" runat="server" />
                    <asp:MultiView ID="MvCorePanelDocInitiatilation" runat="server">
                        <asp:View ID="VwBAUTDocInitialize" runat="server">
                            <div style="width: 99%;">
                                <div class="subheaderpanel">
                                    <div style="margin-left: 5px;">
                                        WCC Supporting Document
                                    </div>
                                </div>
                                <div style="margin-top: 5px; padding: 5px;">
                                    <asp:GridView ID="GvSupportingDocuments" runat="server" AutoGenerateColumns="false"
                                        Width="98%">
                                        <HeaderStyle CssClass="gridHeader" ForeColor="white" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ChkChecked" runat="server" Enabled='<%#Eval("CanDeleted") %>' Checked="true" />
                                                    <asp:Label ID="LblDocPath" runat="server" Text='<%#Eval("DocPath") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="LblOrgDocPath" runat="server" Text='<%#Eval("OrgDocPath") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="LblParentDocType" runat="server" Text='<%#Eval("ParentDocType") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("DocId") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DocName" HeaderText="Document Name" HeaderStyle-CssClass="gridHeader" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <a href="#" id="ViewDocLink" class="fancyboxViewDocument fancybox.iframe" runat="server"
                                                        visible='<%#Eval("IsUploaded") %>'>View</a>
                                                    <asp:Label ID="LblViewDocLink" runat="server" Visible='<%#(Not(Convert.ToBoolean(Eval("IsUploaded")))) %>'
                                                        Text="Not Yet"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="margin-top: 5px; text-align: right;">
                                    <asp:LinkButton ID="LbtEdit" runat="server" Text="Edit" OnClientClick="return confirm('Are you sure you want to edit this WCC');"></asp:LinkButton>
                                    <asp:LinkButton ID="LbtCreateFolder" runat="server" Text="Create Folder" CssClass="dnnPrimaryAction"
                                        OnClientClick="return confirm('Are you sure you want create folder site');"></asp:LinkButton>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="VwWCCDocInitiatilze" runat="server">
                        </asp:View>
                    </asp:MultiView>
                    <div>
                        <asp:Label ID="LblBAUTDocumentHeader" runat="server"></asp:Label>
                    </div>
                    <div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
