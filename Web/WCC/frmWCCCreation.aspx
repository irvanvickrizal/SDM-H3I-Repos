<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCCCreation.aspx.vb"
    Inherits="WCC_frmWCCCreation" %>

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
    .hidden
    {
        display:none;
    }
    .modalBackground
    {
        background-color: #CCCCFF;
        filter: alpha(opacity=40);
        opacity: 0.5;
    }
    .ModalWindow
    {
        border: solid1px#c0c0c0;
        background-color: #99ccff;
        padding: 0px10px10px10px;
        position: absolute;
        top: -1000px;
    }
    .popup_Titlebar
    {
        background: url(../Images/titlebar_bg.jpg);
	    height: 29px;
    }
    .TitlebarLeft 
    {
	    float:left;
	    padding-left:5px;
	    padding-top:5px;
	    font-family:Arial, Helvetica, sans-serif;
	    font-weight:bold;
	    font-size:12px;
	    color:#FFFFFF;
    }
    .TitlebarRight 
    {
	    background:url(../Images/cross_icon_normal.png);
	    background-position:right;
	    background-repeat:no-repeat;
	    height:15px;
	    width:16px;
	    float:right;
	    cursor:pointer;
	    margin-right:5px;
	    margin-top:5px;
    }
    .btnSearch
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtSearch_0.gif);
    }
    .btnSearch:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtSearch_1.gif);
    }
    .btnSearch:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtSearch_2.gif);
    }
    </style>

    <script type="text/javascript" src="../js/jquery.leanModal.min.js"></script>

    <title>WCC CREATION</title>

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

    <script type="text/javascript" language="javascript">
        function NoWPIDSearch() {
            alert('Please type Workpackage id First!');
            return false;
        }
        function WPIDNotFound() {
            alert('WPID Not Found!');
            return false;
        }
        function NoDocumentRequired(){
            alert('Please choose document Required First!');
            return false;
        }
        function NoScopeDetail(){
            alert('Please choose Scope Detail First!');
            return false;
        }
        function BautNotYetApproved(){
            var result = confirm('Are you sure you want to continue while BAUT Not Yet Approved!');
            if (result) {
            return true;
            
            }
            else {
                return false;
            }
        }
        function WCCDeleteSucceed(){
            alert('WCC successfully Deleted');
            return true;
        }
        function forbiddenCreation() {
            alert('You dont have any privileges to create WCC Online!');
            return false;
        }
    </script>

</head>
<body>

    <script type="text/javascript">
        function ValidateRemarks()
        {
            var remarks = document.getElementById("<%=TxtRemarks.ClientID %>");
                if (remarks.value.length < 10)
                {
                    return false;
                }
                return true;
        }
        function validateDeletion()
        {
            var remarksDeletion = ValidateRemarks();
            if (remarksDeletion == false)
            {
                alert("Please Input Remarks of Deletion min.10 Character");
                return false;
            }
            else
            {
                var answer = confirm("Are you sure want to delete this wcc?");
                if (answer)
                {return true;}
                else
                {return false;}
                     
                
            }
        }
    </script>

    <form id="form1" runat="server">
        <div id="panelInformation">
            <div id="panelHeader">
                <h2>
                    Site Information</h2>
                <a class="modal_close" href="#"></a>
            </div>
            <div style="margin-left: 20px; margin-top: 5px;">
                <span class="lblTextTitle">Your Company</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img7" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayCompanyName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">PO Number</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img1" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayPoNo" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">PO Name</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img2" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayPOName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Site No/Site Name refer to PO</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img8" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplaySiteNoPO" runat="server" CssClass="lblText"></asp:Label>/<asp:Label
                                ID="DisplaySiteNamePO" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Site No/Site Name refer to EPM</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img3" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplaySiteNo" runat="server" CssClass="lblText"></asp:Label>/<asp:Label
                                ID="DisplaySiteName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Scope</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img5" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayScope" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">WorkpackageId</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img4" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayWPID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Project Id</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img6" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayProjectID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div>
            <div id="HeaderPanel">
                <asp:HiddenField ID="HdnSubconId" runat="server" />
                <div style="margin-left: 10px;">
                    WCC Creation
                </div>
            </div>
            <div style="width: 99%; border-bottom-color: Black; border-bottom-style: solid; border-bottom-width: 1px;
                padding-bottom: 10px; margin-top: 10px;">
                <div>
                    <span class="lblText">Package ID[EPM]</span>
                    <asp:TextBox ID="TxtPackageId" CssClass="lblText" runat="server" Width="200px" ValidationGroup="wpidsearch"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RfvPackageId" runat="server" CssClass="dnnFormMessage dnnFormError"
                        SetFocusOnError="true" Font-Names="Verdana" Font-Size="11px" ControlToValidate="TxtPackageId"
                        ErrorMessage="Package ID is Required" ValidationGroup="wpidsearch"></asp:RequiredFieldValidator>
                </div>
                <div style="width: 315px; text-align: right; margin-top: 5px;">
                    <asp:LinkButton ID="LbtSearch" runat="server" Width="80px" ValidationGroup="wpidsearch"
                        Style="text-decoration: none; cursor: pointer;">
                        <div class="btnSearch"></div>  
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <asp:MultiView ID="MvCorePanel" runat="server">
            <asp:View ID="VwWCCConfig" runat="server">
                <div style="margin-top: 10px; width: 100%;">
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
                                        <asp:Label ID="LblAcceptanceDate" runat="server" CssClass="lblField" Text="Telkomsel BAUT/BAST Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:MultiView ID="MvCorePanelAcceptanceDate" runat="server">
                                            <asp:View ID="vwAvailableAcceptanceDate" runat="server">
                                                <asp:TextBox ID="TxtBAUTBASTDate" runat="server" CssClass="lblText" ReadOnly="true"
                                                    Width="250px"></asp:TextBox>
                                            </asp:View>
                                            <asp:View ID="vwNotAvailableAcceptanceDate" runat="server">
                                                <asp:TextBox ID="TxtAcceptanceDateNY" runat="server" CssClass="lblText" Width="250px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CEAcceptanceDate" runat="server" Format="dd-MMMM-yyyy"
                                                    PopupButtonID="TxtAcceptanceDateNY" TargetControlID="TxtAcceptanceDateNY">
                                                </cc1:CalendarExtender>
                                                <asp:Label ID="LblWarningMessageAcceptanceDate" runat="server" ForeColor="red" Font-Size="11px"
                                                    Font-Names="Verdana"></asp:Label>
                                            </asp:View>
                                        </asp:MultiView>
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
                                        <asp:Literal ID="LtrScopeDetail" runat="server">Type of Work </asp:Literal>
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
                            <asp:LinkButton ID="LbtSave" runat="server" Text="Submit" CssClass="dnnPrimaryAction"
                                ValidationGroup="wcccreation"></asp:LinkButton>
                            <asp:LinkButton ID="LbtSaveWithoutBAUT" runat="server" Text="Submit" CssClass="dnnPrimaryAction"
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
            </asp:View>
            <asp:View ID="VwAdditionalDocUpload" runat="server">
            </asp:View>
            <asp:View ID="VwNotFound" runat="server">
                <asp:Label ID="LblWarningMessage" runat="server" Font-Names="Verdana" Font-Size="7pt"
                    Font-Bold="true" ForeColor="red" Text="Workpackage ID not found"></asp:Label>
            </asp:View>
            <asp:View ID="VwBAUTNotApproved" runat="server">
                <span style="font-family: Verdana; font-size: 7pt; font-weight: bolder; color: Red">
                    BAUT Not Yet Approved</span>
            </asp:View>
            <asp:View ID="VwWCCList" runat="server">
                <div style="margin-top: 15px;">
                    <asp:GridView ID="GvWCCList" runat="server" AutoGenerateColumns="false" CellPadding="2"
                        Width="98%">
                        <RowStyle CssClass="gridOdd" />
                        <AlternatingRowStyle CssClass="gridEven" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgEdit" runat="server" ImageUrl="~/images/gridview/Edit.jpg"
                                        CommandName="EditWCC" CommandArgument='<%#Eval("WCCID") %>' Width="20px" Height="20px" />
                                    <asp:Label ID="LblPackageId" runat="server" Text='<%#Eval("PackageId") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WCCStatus" HeaderText="Status" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="SubconName" HeaderText="Subcon" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="WCTRDate" HeaderText="WCTR Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="BAUTDate" HeaderText="BAUT Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                                ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                                NullDisplayText="Not Yet Approved" ItemStyle-BorderColor="GrayText" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="LMBY" HeaderText="Initiator Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="GrayText"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a id="opennewframe" runat="server" class="fancybox fancybox.iframe"><span class="gridOdd">
                                        Site information</span></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="GrayText"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="LblWCCID" runat="server" Text='<%#Eval("WCCID") %>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="LbtDeleteWCC" runat="server" CommandArgument='<%#Eval("WCCID") %>'
                                        CommandName="deletewcc">
                                        <img src="~/images/gridview/Cancel.jpg" alt="deleteicon" id="imgDelete" runat="server" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:View>
            <asp:View ID="VwDeleteWCCForm" runat="server">
                <asp:HiddenField ID="HdnWCCId_Del" runat="server" />
                <div style="margin-top: 10px; width: 100%; vertical-align: top;">
                    <div style="height: 300px;">
                        <div>
                            <table width="99%">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="lblField" Text="Subcontractor Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSubcontractorName_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="lblField" Text="WCC Issuance Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtWCCIssuanceDate_Del" runat="server" CssClass="lblText" ReadOnly="true"
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
                                        <asp:Label ID="Label3" runat="server" CssClass="lblField" Text="Certificate Number"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtCertificateNo_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" CssClass="lblField" Text="Site ID(EPM)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSiteID_Del" runat="server" CssClass="lblText" ReadOnly="true"
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
                                        <asp:Label ID="Label5" runat="server" CssClass="lblField" Text="Site Name(EPM)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSiteName_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="lblField" Text="Work Package ID(EPM)"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPackageId_Del" runat="server" CssClass="lblText" ReadOnly="true"
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
                                        <asp:Label ID="Label7" runat="server" CssClass="lblField" Text="Work Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtWorkDesc_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="lblField" Text="PO Subcontractor"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPOSubcontractor_Del" runat="server" CssClass="lblText" Width="250px"
                                            ReadOnly="true"></asp:TextBox>
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
                                        <asp:Label ID="Label9" runat="server" CssClass="lblField" Text="PO Telkomsel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPOTelkomsel_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                            Width="250px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblAcceptanceDate_Del" runat="server" CssClass="lblField" Text="Telkomsel BAUT/BAST Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:MultiView ID="MvCorePanelSignDate_Del" runat="server">
                                            <asp:View ID="vwSignDateRO_Del" runat="server">
                                                <asp:TextBox ID="TxtSignDate_Del" runat="server" CssClass="lblText" ReadOnly="true"
                                                    Width="250px"></asp:TextBox>
                                            </asp:View>
                                            <asp:View ID="vwSignDateET_Del" runat="server">
                                                <asp:TextBox ID="TxtSignDateET_Del" runat="server" CssClass="lblText" Width="250px"></asp:TextBox>
                                            </asp:View>
                                        </asp:MultiView>
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
                                        <asp:Label ID="Label12" runat="server" CssClass="lblField" Text="WCTR Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtWCTRDate_Del" runat="server" CssClass="lblText" Width="250px"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="lblText">
                                        <asp:Literal ID="Literal1" runat="server">Detail Scope</asp:Literal>
                                    </td>
                                    <td>
                                        <div class="dnnFormItem">
                                            <asp:DropDownList ID="DdlMasterScope_Del" runat="server" CssClass="lblText" Enabled="false">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DdlTypeofWork_Del" runat="server" CssClass="lblText" ValidationGroup="wcccreation"
                                                Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div style="height: 3px;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 95%; text-align: right;">
                            <hr />
                            <div>
                                <div style="margin-top: 20px; text-align: right; width: 74%;">
                                    <span class="lblText"><b>Put Your Remarks:</b></span>
                                </div>
                                <div style="margin-top: 5px;">
                                    <asp:TextBox ID="TxtRemarks" runat="server" TextMode="MultiLine" Width="400px" Height="60px"></asp:TextBox>
                                </div>
                                <div style="text-align: right; width: 100%; margin-top: 10px; margin-right: 20px;">
                                    <asp:LinkButton ID="LbtDeleteWCC" runat="server" Text="Delete" CssClass="dnnPrimaryAction"
                                        OnClientClick="return validateDeletion();"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
