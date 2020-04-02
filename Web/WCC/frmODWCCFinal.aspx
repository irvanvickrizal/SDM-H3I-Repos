<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmODWCCFinal.aspx.vb" Inherits="WCC_frmODWCCFinal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../Scripts/Styles/global.css" />

    <script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.mousewheel-3.0.6.pack.js"></script>

    <script src="../Scripts/UI/minified/jquery.ui.core.min.js" type="text/javascript"></script>

    <script src="../Scripts/UI/minified/jquery.ui.widget.min.js" type="text/javascript"></script>

    <script src="../Scripts/UI/minified/jquery.ui.accordion.min.js" type="text/javascript"></script>

    <!-- Add fancyBox main JS and CSS files -->

    <script type="text/javascript" src="../fancybox/jquery.fancybox.js?v=2.1.3"></script>

    <link rel="stylesheet" type="text/css" href="../fancybox/jquery.fancybox.css?v=2.1.2"
        media="screen" />
    <title>Work Completion Certificate</title>
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
            font-size:12px;
            text-align:left;
        }
        .labelFieldText
        {
            font-family:verdana;
            font-size:12px;
            font-weight:bolder;
            text-align:left;
        }
        .formPanel
        {
            width:100%;
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
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
        }
        .fancybox-title-inside {
            text-align: center;
            font-family:verdana;
            font-size:18px;
        }
        .gridHeader
        {
	        font-family:Verdana;
	        font-size:11px;
	        background-color:maroon;
	        font-weight:bolder;
	        text-align:center;
	        padding:5px;
	        color:white;
	        border-style:solid;
	        border-width:2px;
	        border-color:gray;
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
	        padding:5px;
        }
        a.dnnPrimaryAction, a.dnnPrimaryAction:link, a.dnnPrimaryAction:visited, a.dnnSecondaryAction, a.dnnSecondaryAction:link, a.dnnSecondaryAction:visited
        {
	        display: inline-block;
        }
        a.dnnPrimaryAction, a.dnnPrimaryAction:link, a.dnnPrimaryAction:visited, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only
        {
	        background: #818181;
	        background: -moz-linear-gradient(top, #818181 0%, #656565 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#818181), color-stop(100%,#656565));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=    '#818181' , endColorstr= '#656565' ,GradientType=0 );
	        -moz-border-radius: 3px;
	        border-radius: 3px;
	        text-shadow: 0px 1px 1px #000;
	        color: #fff;
	        text-decoration: none;
	        font-weight: bold;
	        border-color: #fff;
	        padding: 8px;
        }
        a[disabled].dnnPrimaryAction, a[disabled].dnnPrimaryAction:link, a[disabled].dnnPrimaryAction:visited, a[disabled].dnnPrimaryAction:hover, a[disabled].dnnPrimaryAction:visited:hover, dnnForm.ui-widget-content a[disabled].dnnPrimaryAction
        {
	        text-decoration: none;
	        color: #bbb;
	        background: #818181;
	        background: -moz-linear-gradient(top, #818181 0%, #656565 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#818181), color-stop(100%,#656565));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=   '#818181' , endColorstr= '#656565' ,GradientType=0 );
	        -ms-filter: "progid:DXImageTransform.Microsoft.gradient( startColorstr='#818181', endColorstr='#656565',GradientType=0 )";
	        cursor: default;
	        padding: 8px;
        }
        ul.dnnActions a.dnnPrimaryAction:hover, ul.dnnActions a.dnnPrimaryAction:visited:hover, a.dnnPrimaryAction:hover, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only:hover
        {
	        background: #4E4E4E;
	        background: -moz-linear-gradient(top, #4E4E4E 0%, #282828 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#4E4E4E), color-stop(100%,#656565));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=   '#4E4E4E' , endColorstr= '#282828' ,GradientType=0 );
	        color: #fff;
	        padding: 8px;
        }
        .dnnFormMessage {
            -moz-border-radius: 3px;
            border-radius: 3px;
            padding: 10px 10px 10px 40px;
            line-height: 1.4;
            margin: 0.5em 1em;
        }
        .dnnFormMessage span {
            float: none;
            padding: 0;
            width: 100%;
            text-align: left;
            text-shadow: 0px 1px 1px #fff;
        }
        .dnnFormError {
            color: #fff !important;
            background: url(../images/errorbg.gif) no-repeat left center;
            text-shadow: 0px 1px 1px #000;
            padding: 5px 20px;
            z-index:13000;
            margin-left:-10px;
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

    <script type="text/javascript">
        $(function(){
            $("#divItems").accordion({
                fillSpace: true
            });
            $(function() {
		        $( "#accordionResizer" ).resizable({
			        minHeight: 140,
			        resize: function() {
				        $( "#accordion" ).accordion( "resize" );
			        }
		        });
	        });
        });
    </script>

    <script type="text/javascript">
        function WCCSubmitSucceed(){
                alert('WCC Submit Sucessfully');
                location.href = '../dashboard/frmDashboardWCC_Subcon.aspx';
        }
        function WCCSubmitFailed() {
            alert('WCC Submit Failed during transaction, please try again!');
            return false;
        }
        function DocNYC() {
            alert('Additional Doc Not Yet Complete is Uploaded!');
            return false;
        }
        function WFNYD(){
            alert('Please defined Workflow Type First!');
            return false;
        }
        function POSubconAlreadyExist(posub,pkgid)
        {
            alert('PO Subcontractor ' + posub + ' is already used by workpackageid ' + pkgid );
            //alert('PO Subcontractor already exist');
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="panelDetail" style="width: 960px;">
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
                <p class="labelText" style="text-align: center;">
                    This is to officially certify that the subcontractor has accomplished the assigned
                    works according to the purchase order completely / in full, all required</p>
                <p class="labelText" style="text-align: center;">
                    all required documentations have been submitted and approved by respective NSN PIC(Person
                    In Charge)</p>
                <asp:ScriptManager ID="SM1" runat="server">
                </asp:ScriptManager>
                <asp:HiddenField ID="HdnPackageId" runat="server" />
                <asp:HiddenField ID="HdnDScopeId" runat="server" />
            </div>
            <div class="formPanel">
                <table width="99%">
                    <tr valign="top">
                        <td style="width: 50%;">
                            <table cellpadding="2" border="2" cellspacing="2" width="99%">
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
                                        <asp:UpdatePanel ID="UpPOSubcon" runat="server">
                                            <ContentTemplate>
                                                <asp:MultiView ID="MvPOSubcon" runat="server">
                                                    <asp:View ID="VwPOSubconDisplay" runat="server">
                                                        <asp:Label ID="DisplayPOSubcontractor" CssClass="labelText" runat="server"></asp:Label>
                                                        <asp:LinkButton ID="LbtEditPoSubcon" runat="server" Text="Edit" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue"></asp:LinkButton>
                                                    </asp:View>
                                                    <asp:View ID="VwPOSubconEdit" runat="server">
                                                        <asp:TextBox ID="TxtPOSubcontractorEdit" runat="server" CssClass="labelText">
                                                        </asp:TextBox>
                                                        <asp:LinkButton ID="LbtUpdatePOSubcon" runat="server" Text="Update" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue" OnClientClick="return confirm('Are you sure you want to update this PO Subcon?')"></asp:LinkButton>
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:LinkButton ID="LbtCancelEditPOSubcon" runat="server" Text="Cancel" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue"></asp:LinkButton>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="LbtEditPoSubcon" />
                                                <asp:PostBackTrigger ControlID="LbtUpdatePOSubcon" />
                                                <asp:AsyncPostBackTrigger ControlID="LbtCancelEditPOSubcon" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
                                        <asp:UpdatePanel ID="UpWCTRDate" runat="server">
                                            <ContentTemplate>
                                                <asp:MultiView ID="MvWCTRDate" runat="server">
                                                    <asp:View ID="VwWCTRDateDisplay" runat="server">
                                                        <asp:Label ID="DisplayWCTRDate" CssClass="labelText" runat="server"></asp:Label>
                                                        <asp:LinkButton ID="LbtEditWCTRDate" runat="server" Text="Edit" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue"></asp:LinkButton>
                                                    </asp:View>
                                                    <asp:View ID="VwWCTRDateEdit" runat="server">
                                                        <asp:TextBox ID="TxtWCTRDate" runat="server" CssClass="lblText" Width="250px"></asp:TextBox>
                                                        <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                            Width="18px" />
                                                        <cc1:CalendarExtender ID="ceStartTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="BtnCalendar"
                                                            TargetControlID="TxtWCTRDate">
                                                        </cc1:CalendarExtender>
                                                        <asp:LinkButton ID="LbtUpdateWCTRDate" runat="server" Text="Update" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue" OnClientClick="return confirm('Are you sure you want to update Date of WCTR?')"></asp:LinkButton>
                                                        <asp:LinkButton ID="LbtCancelEditWCTRDate" runat="server" Text="Cancel" CssClass="labelText"
                                                            Font-Underline="true" ForeColor="blue"></asp:LinkButton>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="LbtEditWCTRDate" />
                                                <asp:AsyncPostBackTrigger ControlID="LbtUpdateWCTRDate" />
                                                <asp:AsyncPostBackTrigger ControlID="LbtCancelEditWCTRDate" />
                                                <asp:PostBackTrigger ControlID="BtnCalendar" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 45%;">
                            <div id="accordionResizer" style="padding: 10px; width: 400px; height: 250px;" class="ui-widget-content">
                                <div id="divItems" style="width: 400px; height: 150px;">
                                    <h3>
                                        <a href="#">Additional Document</a>
                                    </h3>
                                    <div id="UploadDocumentPanel">
                                        <asp:MultiView ID="MvCoreDocPanel" runat="server">
                                            <asp:View ID="VwListSupportingDocuments" runat="server">
                                                <asp:GridView ID="GvSupportingDocuments" runat="server" AutoGenerateColumns="false"
                                                    Width="99%">
                                                    <RowStyle CssClass="gridOdd" />
                                                    <AlternatingRowStyle CssClass="gridEven" />
                                                    <Columns>
                                                        <asp:BoundField DataField="DocName" HeaderText="Document" HeaderStyle-CssClass="gridHeader_2" />
                                                        <asp:TemplateField HeaderStyle-CssClass="gridHeader_2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblDocName" runat="server" Visible="false" Text='<%#Eval("DocName") %>'></asp:Label>
                                                                <asp:Label ID="LblParentDocType" runat="server" Visible="false" Text='<%#Eval("ParentDocType") %>'></asp:Label>
                                                                <asp:Label ID="LblSWId" runat="server" Visible="false" Text='<%#Eval("WCCSiteDocId") %>'></asp:Label>
                                                                <asp:Label ID="LblDocId" runat="server" Visible="false" Text='<%#Eval("DocId") %>'></asp:Label>
                                                                <asp:ImageButton ID="ImgUpload" runat="server" ImageUrl="~/Images/file.gif" CommandName="uploaddoc"
                                                                    CommandArgument='<%#Eval("WCCSiteDocId") %>' />
                                                                <asp:ImageButton ID="ImgDeleteDocument" runat="server" ImageUrl="~/Images/gridview/Cancel.jpg"
                                                                    Width="20px" Height="20px" Visible='<%#Eval("IsUploaded") %>' CommandName="deletedoc"
                                                                    CommandArgument='<%#Eval("WCCSiteDocId") %>' OnClientClick="return confirm('Are you sure want to delete this Document ?')" />
                                                                <a href='#' id="ViewDocLink" class="fancyboxViewDocument fancybox.iframe" runat="server"
                                                                    visible='<%#Eval("IsUploaded") %>'>
                                                                    <img id="Img1" src="~/Images/ViewDoc.jpg" alt="vdoc" runat="server" width="20" height="20" /></a>
                                                                <asp:ImageButton ID="ImgDeleteSiteDoc" runat="server" ImageUrl="~/Images/deldoc.gif"
                                                                    ToolTip="Delete Attachment Folder Doc" CommandArgument='<%#Eval("WCCSiteDocId") %>'
                                                                    CommandName="deletesitedoc" OnClientClick="return confirm('Are you sure want to delete this attachment document folder ?')" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div style="margin-top: 5px; width: 100%; text-align: right;">
                                                    <asp:LinkButton ID="LbtAddDocument" runat="server" Text="Add Document"></asp:LinkButton>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="VwUploadDocument" runat="server">
                                                <asp:HiddenField ID="HdnSiteDocId" runat="server" />
                                                <div style="width: 340px;">
                                                    <div style="height: 120px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <span class="labelFieldText">Document</span>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="DisplayDocName" runat="server" CssClass="labelText"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="labelFieldText">File Upload</span>
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload runat="server" ID="FUAdditionalDoc" />
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FUAdditionalDoc"
                                                                        ValidationGroup="uploaddoc" ErrorMessage="Please select a .pdf file only" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">
                                                                    </asp:RegularExpressionValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div style="width: 99%;">
                                                            <asp:LinkButton ID="LbtUploadDocument" runat="server" CssClass="labelFieldText" Text="Upload"
                                                                ValidationGroup="uploaddoc" ForeColor="blue"></asp:LinkButton>
                                                            <br />
                                                            <asp:Label ID="errorMessage" runat="server" CssClass="labelText"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div style="width: 100%; text-align: right;">
                                                        <asp:LinkButton ID="LbtBackListDoc" runat="server" Text="Back" CssClass="dnnPrimaryAction"
                                                            Font-Names="Verdana" Font-Size="11px"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="VwAdditionalDocument" runat="server">
                                                <asp:UpdatePanel ID="UPAdditionalDoc" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvAdditionalDocument" runat="server" AutoGenerateColumns="false"
                                                            Width="99%">
                                                            <RowStyle CssClass="gridOdd" />
                                                            <AlternatingRowStyle CssClass="gridEven" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="gridHeader_2">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/images/gridview/AddNewItem.jpg"
                                                                            CommandName="adddoc" CommandArgument='<%#Eval("docid") %>' Width="20px" Height="20px" />
                                                                        <asp:Label ID="LblParentDocType" runat="server" Text='<%#Eval("ParentDocType") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="DocName" HeaderText="Document" HeaderStyle-CssClass="gridHeader_2" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="GvAdditionalDocument" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div style="text-align: right; margin-top: 5px">
                                                    <asp:LinkButton ID="LbtClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="11px"
                                                        Font-Bold="true" ForeColor="Red"></asp:LinkButton>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                    <h3>
                                        <a href="#">Document Flow Type</a>
                                    </h3>
                                    <div>
                                        <div style="text-align: left;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <span class="labelFieldText">WorkFlow Type</span>
                                                    <asp:DropDownList ID="DdlWorkflow" runat="server" AutoPostBack="true" ValidationGroup="wfoption">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CvWorkflow" runat="server" ErrorMessage="Please Choose Workflow"
                                                        SetFocusOnError="true" ValidationGroup="wfoption" ControlToValidate="DdlWorkflow"
                                                        CssClass="dnnFormMessage dnnFormError" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                                    <div style="text-align: left; margin-top: 5px; height: 100px;">
                                                        <asp:GridView ID="GvApprovers" runat="server" AutoGenerateColumns="false" Width="99%">
                                                            <RowStyle CssClass="gridOdd" />
                                                            <RowStyle CssClass="gridEven" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="Solid"
                                                                    ItemStyle-BorderColor="graytext" ItemStyle-BorderWidth="2px">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Username" HeaderStyle-CssClass="gridHeader" HeaderText="Name"
                                                                    ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="graytext" ItemStyle-BorderWidth="2px" />
                                                                <asp:BoundField DataField="SignTitle" HeaderStyle-CssClass="gridHeader" HeaderText="Title"
                                                                    ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="graytext" ItemStyle-BorderWidth="2px" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div style="width: 100%; text-align: right; margin-top: 5px;">
                                                        <asp:LinkButton ID="LbtEditWorkflow" runat="server" Text="Edit" Font-Names="Verdana"
                                                            CausesValidation="false" Font-Size="11px"></asp:LinkButton>
                                                        <asp:LinkButton ID="LbtCancelWorkflow" runat="server" Text="Cancel" Font-Names="Verdana"
                                                            CausesValidation="false" Font-Size="11px"></asp:LinkButton>
                                                        <asp:LinkButton ID="LbtUpdateWorkflow" runat="server" Text="Update" CssClass="dnnPrimaryAction"
                                                            Font-Names="Verdana" Font-Size="11px" ValidationGroup="wfotpion"></asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="LbtEditWorkflow" />
                                                    <asp:AsyncPostBackTrigger ControlID="LbtUpdateWorkflow" />
                                                    <asp:AsyncPostBackTrigger ControlID="LbtCancelWorkflow" />
                                                    <asp:AsyncPostBackTrigger ControlID="DdlWorkflow" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 10px;">
                <table>
                    <tr valign="top">
                        <td>
                            <div>
                                <span class="labelFieldText">SIS/SITAC</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkSIS_Sitac" runat="server" /><span class="labelText">SIS/SES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkPKS_AJB_50Perc" runat="server" /><span class="labelText">50% PKS/AJB</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkIMB_50Perc" runat="server" /><span class="labelText">50% IMB</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkCA_LC_100Perc" runat="server" /><span class="labelText">100% CA/LC</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSITAC_Permitting" runat="server" /><span class="labelText">SITAC/Permitting</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">CME</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="Chk2G_3G_BAUT" runat="server" /><span class="labelText">2G/3G BAUT</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSDH_PDH" runat="server" /><span class="labelText">SDH/PDH</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="Chk2G_CME_BAST" runat="server" /><span class="labelText">2G CME BAST</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkAdditional" runat="server" /><span class="labelText">ADDITIONAL</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">TI</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkSurvey" runat="server" /><span class="labelText">SURVEY</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDismantling" runat="server" /><span class="labelText">DISMANTLING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkReconfig" runat="server" /><span class="labelText">RECONFIG</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkEnclosure" runat="server" /><span class="labelText">ENCLOSURE</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkServices" runat="server" /><span class="labelText">SERVICES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkFreq_License" runat="server" /><span class="labelText">FREQ.LICENSE</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">NPO</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkInitial_Tuning" runat="server" /><span class="labelText">INITIAL
                                        TUNING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkCluster_Tuning" runat="server" /><span class="labelText">CLUSTER
                                        TUNING</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkIBC" runat="server" /><span class="labelText">IBC (In Building)</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkOptimization" runat="server" /><span class="labelText">OPTIMIZATION</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkSiteVerification" runat="server" /><span class="labelText">SITE
                                        VERIFICATION</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDetailed_RF_Covered" runat="server" /><span class="labelText">DETAILED
                                        RF COVERED AND CAPACITY NWP</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div>
                                <span class="labelFieldText">NPO</span>
                            </div>
                            <div style="margin-top: 5px;">
                                <div>
                                    <asp:CheckBox ID="ChkChange_Request" runat="server" /><span class="labelText">CHANGE
                                        REQUEST</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkDesign_For_MW_Access" runat="server" /><span class="labelText">DESIGN
                                        FOR MW ACCESS MW</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkNPO_SDH_PDH" runat="server" /><span class="labelText">SDH/PDH</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkNPO_SIS_SES" runat="server" /><span class="labelText">SIS/SES</span>
                                </div>
                                <div>
                                    <asp:CheckBox ID="ChkHICAP_BSC_COLO_DCS" runat="server" /><span class="labelText">HICAP/BSC/COLO/DCS</span>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 5px;">
                <p style="font-family: Verdana; font-size: 11px; font-weight: bolder; text-align: center;">
                    this WCC has been generated electronically from the eBAST-system and thus requires
                    no additional signatures from NSN person in charge. It serves as main supporting
                    document for invoicing of the respective purchase order.
                </p>
            </div>
            <hr />
            <div style="margin-top: 5px; width: 100%; text-align: right;">
                <asp:LinkButton ID="LbtSubmit" runat="server" CssClass="dnnPrimaryAction" Font-Names="Verdana"
                    Text="Submit" Font-Size="11px"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
