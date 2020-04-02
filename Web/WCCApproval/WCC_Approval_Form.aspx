<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WCC_Approval_Form.aspx.vb"
    EnableEventValidation="false" Inherits="WCCApproval_WCC_Approval_Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Document</title>
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
            width:960px;
            text-align:center;
            margin-top:10px;
        }
        .formPanel2
        {
            width:960px; 
            margin-top: 10px; 
            border-bottom-color: #000; 
            border-bottom-style: dashed;
            border-bottom-width: 1px; 
            padding-bottom: 5px;
        }
        .panelDetail
        {
            margin-top:20px;
            padding: 30px; 
            background: #FFF;
            border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
            box-shadow: 0px 0px 4px rgba(0,0,0,0.7); -webkit-box-shadow: 0 0 4px rgba(0,0,0,0.7); -moz-box-shadow: 0 0px 4px rgba(0,0,0,0.7);
            z-index:13000;
            width:960px;
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
        .divCenter
        {   
            margin-top:5px;
            text-align:center;
        }
        .lblRemarks
        {
            font-family: Verdana; 
            font-size: 11px; 
            font-weight: bolder; 
            text-align: center;
        }
        .tableinfo
        {
            margin:0 auto;
            width:70%;
            border-style:solid;
            border-color:black;
            border-width:2px;
            padding:2px; 
        }
        #PleaseWait
                {
                    z-index: 200;
                    position: fixed;
                    top: 0pt;
                    left: 0pt;
                    text-align:center;
                    height : 100px;
                    width:100px;
                    background-image: url(../Images/animation_processing.gif);
                    background-repeat: no-repeat;
                    margin: 0 50%; margin-top: 10px;
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
    .btnApprove
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_0.gif);
    }
    .btnApprove:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_1.gif);
    }
    .btnApprove:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnApprove_2.gif);
    }
    .btnReject
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_0.gif);
    }
    .btnReject:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_1.gif);
    }
    .btnReject:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnReject_2.gif);
    }
    .btnSubmit
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_0.gif);
    }
    .btnSubmit:hover
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_1.gif);
    }
    .btnSubmit:click
    {
        height:29px;
        width:80px;
        background-image: url(../Images/button/BtnSubmit_2.gif);
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
        function WindowsCloseWCCApprover(){
                alert('WCC approved Sucessfully');
                location.href = '../dashboard/frmSiteDocCount_WCC.aspx';
        }
        function WindowsCloseCRReviewer(){
                alert('WCC Reviewed Sucessfully.');
                location.href = '../dashboard/frmSiteDocCount_WCC.aspx';
        }
        function WindowsCloseWCCRejection(){
                alert('WCC Rejected Sucessfully');
                location.href = '../dashboard/frmSiteDocCount_WCC.aspx';
        }
        
        var atLeast = 1
            function CheckItem()
            {
                var chk = ValidateCheckList();
                var remrks = ValidateRemarks();
                if (chk == false)
                {
                    alert("Please tick at least one Reason");
                    return false;
                }
                else
                {
                    if(remrks == false)
                    {
                        alert("Please describe in detail your reason of rejection");
                        return false;
                    }
                    else
                    {
                        var answer = confirm("Are you sure want to reject this document?");
                        if (answer){
		                    return true;
	                    }
	                    else{
		                    return false;
	                    }
                    }
                }
                return true;
            }
            
            function ValidateRemarks()
            {
                var remarks = document.getElementById("<%=TxtRemarksReject.ClientID %>");
                if (remarks.value.length < 10)
                {
                    return false;
                }
                return true;
            }
            
            function ValidateCheckList(){     
                var CHK = document.getElementById("<%=CbList.ClientID%>"); 
                var checkbox = CHK.getElementsByTagName("input");
                var counter=0;
                for (var i=0;i<checkbox.length;i++)
                {
                    if (checkbox[i].checked)
                    {
                        counter++;
                    }
                }
                if(atLeast>counter)
                {
                    return false;
                }
                return true;
            }
            function BautNotYetApproved(){
            var result = confirm('Are You sure you want to approve this document before BAUT Approved ?');
            if (result) {
                return true;
                
                }
                else {
                    return false;
                }
            }
            function BautApproved(){
            var result = confirm('Are You sure you want to approve this WCC Document ?');
            if (result) {
                return true;
                
                }
                else {
                    return false;
                }
            }
    </script>

    <script language="javascript" type="text/javascript">
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
    <div id="divPrint" runat="server" class="panelDetail">
        <div class="headerPanel">
            <table width="100%;">
                <tr>
                    <td style="width: 30%;">
                        <img src="http://localhost/images/NSNLogo_New.png" id="Img1" alt="NSNLogo" />
                    </td>
                    <td style="width: 69%;">
                        <span class="headerTitle">Work Completion Certificate</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divCenter">
            <p class="labelText" style="text-align: center;">
                This is to officially certify that the subcontractor has accomplished the assigned
                works according to the purchase order completely / in full, all required</p>
            <p class="labelText" style="text-align: center;">
                all required documentations have been submitted and approved by respective NSN PIC(Person
                In Charge)</p>
        </div>
        <div class="formPanel">
            <table cellpadding="2" border="2" cellspacing="2" width="70%" style="margin: 0 auto;">
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Subcontractor Name</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblSubconNamePrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">WCC Issuance Date</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblWCCIssuanceDatePrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Certificate Number</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblCertificateNoPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Site ID (EPM)</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblSiteIDPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Site Name (EPM)</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblSiteNamePrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Work Package ID (EPM)</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblWPIDPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">Work Description</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblWorkDescriptionPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">PO Subcontractor</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblPOSubconPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">PO Telkomsel</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblPOTselPrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="LblDocAcceptanceDateTypePrint" CssClass="labelFieldText" runat="server"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblDocAcceptanceDatePrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <span class="labelFieldText">WCTR Date</span>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="LblWCTRDatePrint" CssClass="labelText" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formPanel2">
            <table>
                <tr valign="top">
                    <td>
                        <div>
                            <span class="labelFieldText">SIS/SITAC</span>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <input type="checkbox" runat="server" id="ChkSISorSESPrint" />
                                <span class="labelText">SIS/SES</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkPKSorAJBPrint" /><span class="labelText">50%PKS/AJB</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="Chk50PercIMBPrint" /><span class="labelText">50%
                                    IMB</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="Chk100PercCAorLCPrint" /><span class="labelText">100%
                                    CA/LC</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkSitacorPermittingPrint" /><span class="labelText">SITAC/Permitting</span>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <span class="labelFieldText">CME</span>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <input type="checkbox" id="Chk2Gor3GBAUTPrint" runat="server" /><span class="labelText">2G/3G
                                    BAUT</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkSDHorPDHPrint" runat="server" /><span class="labelText">SDH/PDH</span>
                            </div>
                            <div>
                                <input type="checkbox" id="Chk2GCMEBastPrint" runat="server" /><span class="labelText">2G
                                    CME BAST</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkAdditionalPrint" runat="server" /><span class="labelText">ADDITIONAL</span>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <span class="labelFieldText">TI</span>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <input type="checkbox" id="ChkSurveyPrint_TI" runat="server" /><span class="labelText">SURVEY</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkDismantlingPrint_TI" runat="server" /><span class="labelText">DISMANTLING</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkReconfigPrint_TI" runat="server" /><span class="labelText">RECONFIG</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkEnclosurePrint_TI" runat="server" /><span class="labelText">ENCLOSURE</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkServicesPrint_TI" runat="server" /><span class="labelText">SERVICES</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkFreqLicensePrint_TI" runat="server" /><span class="labelText">FREQ.LICENSE</span>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <span class="labelFieldText">NPO</span>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <input type="checkbox" id="ChkInitialTuningPrint_NPO" runat="server" /><span class="labelText">INITIAL
                                    TUNING</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkClusterPrint_NPO" runat="server" /><span class="labelText">CLUSTER
                                    TUNING</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkIBCPrint_NPO" runat="server" /><span class="labelText">IBC
                                    (In Building)</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkOptimizationPrint_NPO" /><span class="labelText">OPTIMIZATION</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkSiteVerificationPrint_NPO" /><span class="labelText">SITE
                                    VERIFICATION</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkDetailRFCovAndCapNWPPrint_NPO" />
                                <span class="labelText">DETAILED RF COVERED AND CAPACITY NWP</span>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div>
                            <span class="labelFieldText">NPO</span>
                        </div>
                        <div style="margin-top: 5px;">
                            <div>
                                <input type="checkbox" runat="server" id="ChkChangeReqPrint" />
                                <span class="labelText">CHANGE REQUEST</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkDesignForMWAccessPrint" /><span class="labelText">DESIGN
                                    FOR MW ACCESS MW</span>
                            </div>
                            <div>
                                <input type="checkbox" runat="server" id="ChkSDHorPDHPrint_NPO" /><span class="labelText">SDH/PDH</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkSISorSESPrint_NPO" runat="server" /><span class="labelText">SIS/SES</span>
                            </div>
                            <div>
                                <input type="checkbox" id="ChkHICAPorBSCorCOLOorDCSPrint_NPO" runat="server" /><span
                                    class="labelText">HICAP/BSC/COLO/DCS</span>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="margin-top: 5px;">
                <p class="lblRemarks">
                    This WCC has been generated electronically from the eBAST-system and thus requires
                    no additional signatures from NSN person in charge. It serves as main supporting
                    document for invoicing of the respective purchase order.
                </p>
            </div>
        </div>
        <div style="margin-top: 10px;" id="dvListApprovalPrint" runat="server" class="labelText">
        </div>
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div class="panelDetail" id="divChecking" runat="server">
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
                            <asp:Label ID="DisplayDocAcceptanceLabel" CssClass="labelFieldText" runat="server"></asp:Label>
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
            <div class="formPanel2">
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
                                <div>
                                    <asp:CheckBox ID="ChkDetailed_RF_Covered" runat="server" Enabled="false" /><span
                                        class="labelText">DETAILED RF COVERED AND CAPACITY NWP</span>
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
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 5px;">
                    <p class="lblRemarks">
                        This WCC has been generated electronically from the eBAST-system and thus requires
                        no additional signatures from NSN person in charge. It serves as main supporting
                        document for invoicing of the respective purchase order.
                    </p>
                </div>
            </div>
            <div style="margin-top: 10px;" id="divApproval" runat="server" class="labelText">
            </div>
            <hr />
            <asp:UpdatePanel ID="upPanelReview" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="UploadDocumentPanel">
                        <div>
                            <h3>
                                Additional Document
                            </h3>
                        </div>
                        <div>
                            <asp:GridView ID="GvSupportingDocuments" runat="server" AutoGenerateColumns="false"
                                Width="50%">
                                <RowStyle CssClass="gridOdd" />
                                <AlternatingRowStyle CssClass="gridEven" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Document" HeaderStyle-CssClass="gridHeader_2">
                                        <ItemTemplate>
                                            <a href='#' id="ViewDocLink" class="fancyboxViewDocument fancybox.iframe" runat="server"
                                                visible='<%#Eval("IsUploaded") %>'>
                                                <%#Eval("DocName") %>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" HeaderText="Is Rejected" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDocId" runat="server" Visible="false" Text='<%#Eval("DocId") %>'></asp:Label>
                                            <asp:Label ID="LblDocName" runat="server" Visible="false" Text='<%#Eval("DocName") %>'></asp:Label>
                                            <asp:Label ID="LblParentDocType" runat="server" Visible="false" Text='<%#Eval("ParentDocType") %>'></asp:Label>
                                            <asp:Label ID="LblSWId" runat="server" Visible="false" Text='<%#Eval("WCCSiteDocId") %>'></asp:Label>
                                            <input id="ChkRejected" runat="server" type="checkbox" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <hr />
                    <asp:MultiView ID="MvCorePanel" runat="server">
                        <asp:View ID="VwApprovalDocument" runat="server">
                            <div style="margin-top: 5px; width: 100%; text-align: right;">
                                <asp:LinkButton ID="LbtBackApproval" runat="server" Text="Back" Font-Names="Verdana"
                                    Font-Size="11px" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to back to List of task pending ?')"></asp:LinkButton>
                                <asp:LinkButton ID="LbtApprove" runat="server" Width="80px" Style="text-decoration: none;
                                    cursor: pointer;">
                                    <div class="btnApprove"></div>
                                </asp:LinkButton>
                                <asp:LinkButton ID="LbtReject" runat="server" Width="80px" Style="text-decoration: none;
                                    cursor: pointer;">
                                    <div class="btnReject"></div>
                                </asp:LinkButton>
                            </div>
                        </asp:View>
                        <asp:View ID="vwRejectPanel" runat="server">
                            <div style="width: 100%;">
                                <table>
                                    <tr valign="top">
                                        <td>
                                            <div style="padding-top: 5px;">
                                                <span class="lblText">Reason </span>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:CheckBoxList ID="CbList" runat="server" CssClass="lblText">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <span class="lblText">Remarks </span>
                                        </td>
                                        <td>
                                            <div style="padding-left: 8px;">
                                                <asp:TextBox ID="TxtRemarksReject" runat="server" TextMode="MultiLine" Height="80px"
                                                    CssClass="textFieldStyle" Width="400px"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="margin-top: 5px; width: 100%; text-align: right;">
                                <asp:LinkButton ID="LbtSubmitReject" runat="server" Text="Submit" OnClientClick="return CheckItem()"
                                    CausesValidation="false" Width="80px" Style="text-decoration: none; cursor: pointer;">
                                    <div class="btnSubmit"></div>
                                </asp:LinkButton>
                                <asp:LinkButton ID="LbtCancelSubmit" runat="server" Text="Cancel"
                                    Font-Names="Verdana" Font-Size="11px" CausesValidation="false"></asp:LinkButton>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LbtReject" />
                    <asp:AsyncPostBackTrigger ControlID="LbtCancelSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="LbtSubmitReject" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:UpdateProgress ID="upgATPReport" AssociatedUpdatePanelID="upPanelReview" runat="server"
            DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                            height="150" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
