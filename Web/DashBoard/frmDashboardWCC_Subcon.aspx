<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDashboardWCC_Subcon.aspx.vb"
    Inherits="DashBoard_frmDashboard_WCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Agenda Subcon</title>
    <style type="text/css">
        #box
    {
		border-style: solid;
        border-width: 1px;
        border-color: #cfcfcf;
        position: relative;
        width: 790px;
        background: #ddd;
        padding-left: 3px;
		padding-right: 3px;
        color: rgba(0,0,0, .8);
        text-shadow: 0 1px 0 #fff;
        line-height: 1.5;
        background: -webkit-gradient(linear, left top, left bottom, 
color-stop(0%, white), color-stop(15%, white), color-stop(100%, #f0f0f0));
        background: -moz-linear-gradient(top, white 0%, white 55%, #f0f0f0 130%);
        margin: 0px auto;
        border-radius: 8px; -moz-border-radius: 8px; -webkit-border-radius: 8px;
        padding-top:3px;
        padding-bottom:15px;
    }
    #box:before, #box:after
    {
        z-index: -1;
        position: absolute;
        content: "";
        bottom: 15px;
        left: 10px;
        width: 785px;
        top: 80%;
        max-width: 500px;
        background: #ddd;;
        -webkit-box-shadow: 0 15px 10px rgba(0,0,0, 0.7);
        -moz-box-shadow: 0 15px 10px rgba(0, 0, 0, 0.7);
        box-shadow: 0 15px 10px rgba(0, 0, 0, 0.7);
        -webkit-transform: rotate(-3deg);
        -moz-transform: rotate(-3deg);
        -o-transform: rotate(-3deg);
        -ms-transform: rotate(-3deg);
        transform: rotate(-3deg);
		background: -webkit-gradient(linear, left top, left bottom, 
color-stop(0%, white), color-stop(15%, white), color-stop(100%, #f0f0f0));
        background: -moz-linear-gradient(top, white 0%, white 55%, #f0f0f0 130%);
        
    }
    
    #box:after
    {
        -webkit-transform: rotate(3deg);
        -moz-transform: rotate(3deg);
        -o-transform: rotate(3deg);
        -ms-transform: rotate(3deg);
        transform: rotate(3deg);
        right: 10px;
        left: auto;
    }
    .lblText
    {
        font-family:verdana;
        font-size:12px;
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
    </style>

    <script type="text/javascript" src="../js/jquery-1.8.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.mousewheel-3.0.6.pack.js"></script>

    <!-- Add fancyBox main JS and CSS files -->

    <script type="text/javascript" src="../fancybox/jquery.fancybox.js?v=2.1.3"></script>

    <link rel="stylesheet" type="text/css" href="../fancybox/jquery.fancybox.css?v=2.1.2"
        media="screen" />

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
    			
    			$('.fancyboxViewDashboard').fancybox({
    			    width:900,
    			    height:450,
    			    fitToView : false,
                    autoSize : false,
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
        <div style="width: 100%;">
            <div style="width: 100%">
                <div id="box">
                    <table cellpadding="2px" width="100%;">
                        <tr>
                            <td colspan="2">
                                <div style="border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
                                    background-color: #cfcfcf; padding: 5px; padding-left: 10px;">
                                    <span style="font-family: Verdana; font-size: 15px; color: White">Task Agenda</span>
                                </div>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <img src="../images/green_checkmark20.jpg" alt="RptIcon" height="15" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblWCCDoneNull" runat="server" Text="WCC Done (0)" CssClass="lblText"></asp:Label>
                                            <a href="../fancybox_form/fb_WCCDone.aspx?dtype=subcon" runat="server" id="CRFinalLink">
                                                <asp:Label ID="LblWCCDone" runat="server" CssClass="lblText"></asp:Label>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10">
                                            <div style="border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #cfcfcf;
                                                width: 100%; margin-top: 3px;">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="../images/RptIcon.png" alt="RptIcon" height="15" width="13" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblWCCinPipelineNull" runat="server" Text="WCC On Task Pending (0)"
                                                CssClass="lblText"></asp:Label>
                                            <a href="../fancybox_form/fb_WCCOnTaskPending_Subcon.aspx" class="fancyboxViewDashboard fancybox.iframe">
                                                <asp:Label ID="LblOnTaskPending" runat="server" CssClass="lblText"></asp:Label></a>
                                        </td>
                                        <td>
                                            <img src="../images/arrow_left.png" alt="leftarrow" height="30" width="25" />
                                        </td>
                                        <td>
                                            <img src="../images/Red_icon20.png" alt="RptIcon" height="15" width="13" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblWCCReadyCreationNull" runat="server" Text="WCC Ready Creation (0)"
                                                CssClass="lblTaskPending"></asp:Label>
                                            <asp:LinkButton ID="LbtWCCReadyCreation" runat="server" CssClass="lblText"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <img src="../images/arrow_left.png" alt="leftarrow" height="30" width="25" />
                                        </td>
                                        <td>
                                            <img src="../images/Red_icon20.png" alt="RptIcon" height="15" width="13" />
                                        </td>
                                        <td>
                                            <asp:Label ID="LblSiteNeedWCCNull" runat="server" Text="WCC Preparation (0)" CssClass="lblTaskPending"></asp:Label>
                                            <asp:LinkButton ID="LbtSiteNeedWCC" runat="server" CssClass="lblText"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <img src="../images/arrow_left.png" alt="leftarrow" height="30" width="25" />
                                        </td>
                                        <td>
                                            <img src="../images/Red_icon20.png" alt="RptIcon" height="15" width="13" />
                                        </td>
                                        <td colspan="4">
                                            <asp:Label ID="LblWCCRejectionNull" runat="server" Text="WCC Rejection (0)" CssClass="lblText"></asp:Label>
                                            <asp:LinkButton ID="LbtWCCRejection" runat="server" CssClass="lblText"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <div style="background-color: #cfcfcf; border-radius: 5px; -moz-border-radius: 5px;
                        -webkit-border-radius: 5px; background-color: #cfcfcf; padding-left: 10px;">
                        <table cellpadding="2px" width="99%;">
                            <tr>
                                <td colspan="2" style="background-color: #cfcfcf; padding: 3px;">
                                    <span style="font-family: Verdana; font-size: 15px; color: White">eBAST Mutual Link</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin-top: 10px;">
                        <table cellpadding="2px">
                            <tr valign="top">
                                <td>
                                    <img src="../images/green_checkmark20.jpg" alt="RptIcon" height="15" />
                                </td>
                                <td>
                                    <a class="popup" href="../RPT/frmEBAUTDone.aspx" title="BAUT DONE"><span style="font-family: Verdana;
                                        font-size: 12px;">BAUT Done Report</span></a>
                                </td>
                                <td>
                                    <img src="../images/green_checkmark20.jpg" alt="RptIcon" height="15" />
                                </td>
                                <td>
                                    <a class="popup" title="ATP DONE" href="../Rpt/frmQCReport.aspx"><span style="font-family: Verdana;
                                        font-size: 12px;">QC Done Report</span></a>
                                </td>
                                <td>
                                    <img src="../images/green_checkmark20.jpg" alt="RptIcon" height="15" />
                                </td>
                                <td>
                                    <a class="popup" title="ATP DONE" href="../Rpt/frmATPReport.aspx"><span style="font-family: Verdana;
                                        font-size: 12px;">ATP Done Report</span></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
