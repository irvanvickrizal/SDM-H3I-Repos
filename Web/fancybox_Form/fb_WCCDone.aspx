<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_WCCDone.aspx.vb" Inherits="fancybox_Form_fb_WCCDone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC DONE</title>

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
        #panelHeader
        {
            background-color:#cfcfcf;
            padding:5px;
            font-family:verdana;
            font-size:15px;
            font-weight:bolder;
            color:#ffffff;
            border-top-left-radius: 5px; 
            border-top-right-radius: 5px; 
            -moz-border-radius-topright: 5px; 
            -moz-border-radius-topleft: 5px; 
            -webkit-border-radius-topright: 5px;
            -webkit-border-radius-topleft: 5px;
            border-bottom-right-radius: 5px; 
            -moz-border-radius-bottomright: 5px;
            -webkit-border-radius-bottomright: 5px; 
            border-style:solid;
            border-width:2px;
            border-color:#c3c3c3;
        }
        .btnExportExcel
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_0.gif);
        }
        .btnExportExcel:hover
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_1.gif);
        }
        .btnExportExcel:click
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_2.gif);
        }
        #advanceSearchPanel
        {
            background-color:#c3c3c3;
	        padding:0px;
	        width:740px;
	        -moz-border-radius-bottomright: 5px; 
            -webkit-border-radius-bottomright: 5px;
            border-bottom-right-radius: 5px; 
            border-bottom-left-radius: 5px; 
            -moz-border-radius-bottomleft: 5px; 
            -webkit-border-radius-bottomleft: 5px;
            color:maroon;
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            text-align:center;
            cursor:pointer;
        }
        .AccordionTitle, .AccordionContent, .AccordionContainer
        {
          position:relative;
          width:745px;
        }
        .AccordionTitle
        {
          overflow:hidden;
          cursor:pointer;
          font-family:Arial;
          font-size:8pt;
          font-weight:bold;
          vertical-align:middle;
          text-align:center;
          background-repeat:repeat-x;
          display:table-cell;
          background-image:url('title_repeater.jpg');
          -moz-user-select:none;
        }
        .AccordionContent
        {
          overflow:auto;
          display:none; 
          background-color:#cfcfcf;
          -moz-border-radius: 5px;
	        border-radius: 5px;
        }

        .AccordionContainer
        {
          border-top: solid 1px #ffffff;
          border-bottom: solid 1px #ffffff;
          border-left: solid 2px #ffffff;
          border-right: solid 2px #ffffff;
          -moz-border-radius: 5px;
	        border-radius: 5px;
        }
        .pagingStyle
        {
            
        }
        .textLabel
        {
            font-family:verdana;
            font-size:11px;
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

    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty!');
            return false;
        }
        function invalidDateSearch(){
            alert('Please define Start Date first!');
            return false;
        }
    </script>

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
    			    height:780,
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
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div id="panelHeader">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 80%;">
                        <span>WCC Done</span>
                    </td>
                    <td style="width: 19%; text-align: right;">
                        <asp:LinkButton ID="LbtExport" runat="server" Width="120px" Style="text-decoration: none;
                            cursor: pointer">
                            <div class="btnExportExcel"></div>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div id="advanceSearchPanel">
            <div style="margin-left: 5px;">
                <div>
                    <table>
                        <tr>
                            <td valign="middle">
                                <asp:CheckBox ID="ChkScope" runat="server" />
                                <asp:Label ID="LblScope" CssClass="textLabel" runat="server" Text="Scope" ForeColor="black"></asp:Label>
                                <asp:DropDownList ID="DdlScope" runat="server" CssClass="textLabel">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkStartDate" runat="server" />
                                <asp:Label ID="LblStartDate" runat="server" Text="StartDate" CssClass="textLabel"
                                    ForeColor="black"></asp:Label>
                                <asp:TextBox ID="TxtStartDate" runat="server" Width="150px" CssClass="textLabel"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceStartDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="TxtStartDate"
                                    TargetControlID="TxtStartDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkEndDate" runat="server" />
                                <asp:Label ID="Label1" runat="server" Text="EndDate" CssClass="textLabel" ForeColor="black"></asp:Label>
                                <asp:TextBox ID="TxtEndDate" runat="server" Width="150px" CssClass="textLabel"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceEndDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="TxtEndDate"
                                    TargetControlID="TxtEndDate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <asp:LinkButton ID="LbtSearch" runat="server" Text="Search" CssClass="textLabel"></asp:LinkButton>
                                <asp:HiddenField ID="HdnSearchId" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div style="margin-top: 5px; width: 100%;">
            <asp:GridView ID="GvWCCList" runat="server" AutoGenerateColumns="false" CellPadding="2"
                AllowPaging="true" PageSize="5">
                <RowStyle CssClass="gridOdd" />
                <AlternatingRowStyle CssClass="gridEven" />
                <PagerStyle CssClass="pagingStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            .
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SubconName" HeaderText="Subcon" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="CertificateNumber" HeaderText="CertificateNo." HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="PackageId" HeaderText="Package ID [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcont" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="LMBY" HeaderText="Initiator Name" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="ApproverName" HeaderText="Approver Name" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="ApproverDate" HeaderText="Approve Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="GrayText"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LblWCCID" runat="server" Text='<%#Eval("wccid") %>' Visible="false"></asp:Label>
                            <a href="#" runat="server" id="ViewWCCDone" class="fancyboxViewDashboard fancybox.iframe">
                                <img src="~/images/Pdf_Icon.png" alt="viewdocicon" runat="server" id="viewdocicon" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="display: none;">
            <asp:GridView ID="GvWCCDoneExport" runat="server" AutoGenerateColumns="false" CellPadding="2"
                AllowPaging="false">
                <RowStyle CssClass="gridOdd" />
                <AlternatingRowStyle CssClass="gridEven" />
                <Columns>
                    <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            .
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SubconName" HeaderText="Subcon" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="IssuanceDate" HeaderText="Issuance Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="CertificateNumber" HeaderText="CertificateNo." HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="PackageId" HeaderText="Package ID [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="PONO" HeaderText="PO.Telkomsel" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="LMBY" HeaderText="Initiator Name" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="ApproverName" HeaderText="Approver Name" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="ApproverDate" HeaderText="Approve Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
