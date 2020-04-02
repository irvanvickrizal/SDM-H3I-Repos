<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WCCDashboard_Prep.aspx.vb" EnableEventValidation="false"
    Inherits="Dashboard_WCC_WCCDashboard_Prep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Preparation</title>

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
	    width: 98%;
	    background-repeat: repeat-x;
	    background-color:#c5c3c3;
	    font-family: verdana;
	    font-weight: bolder;
	    font-size: 14px;
	    color: white;
	    padding-top: 4px;
	    padding-bottom: 4px;
	    padding-left:10px;
	    -moz-border-radius: 3px;
	    border-radius: 3px;
	    margin-bottom:10px;
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
	    padding:5px;
    }
    .fontLink
    {
        font-family:verdana;
        font-size:11px;
        color:blue;
        text-decoration:underline;
        cursor:pointer;
    }
     a.ButtonExport, a.ButtonExport:link, a.ButtonExport:visited, a.dnnSecondaryAction, a.dnnSecondaryAction:link, a.dnnSecondaryAction:visited
        {
	        display: inline-block;
        }
        a.ButtonExport, a.ButtonExport:link, a.ButtonExport:visited, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only
        {
	        background: #6348CC;
	        background: -moz-linear-gradient(top, #6348CC 0%, #6348CC 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#0080C0), color-stop(100%,#0080C0));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=    '#0080C0' , endColorstr= '#0080C0' ,GradientType=0 );
	        -moz-border-radius: 3px;
	        border-radius: 3px;
	        text-shadow: 0px 1px 1px #000;
	        color: #fff;
	        text-decoration: none;
	        font-weight: bold;
	        border-color: #fff;
	        padding: 8px;
        }
        a[disabled].ButtonExport, a[disabled].ButtonExport:link, a[disabled].ButtonExport:visited, a[disabled].ButtonExport:hover, a[disabled].ButtonExport:visited:hover, dnnForm.ui-widget-content a[disabled].ButtonExport
        {
	        text-decoration: none;
	        color: #bbb;
	        background: #6348CC;
	        background: -moz-linear-gradient(top, #6348CC 0%, #6348CC 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#6348CC), color-stop(100%,#6348CC));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=   '#6348CC' , endColorstr= '#6348CC' ,GradientType=0 );
	        -ms-filter: "progid:DXImageTransform.Microsoft.gradient( startColorstr='#6348CC', endColorstr='#6348CC',GradientType=0 )";
	        cursor: default;
	        padding: 8px;
        }
        a.ButtonExport:hover, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only:hover
        {
	        background: #4E4E4E;
	        background: -moz-linear-gradient(top, #4E4E4E 0%, #282828 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#4E4E4E), color-stop(100%,#656565));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=   '#4E4E4E' , endColorstr= '#282828' ,GradientType=0 );
	        color: #fff;
	        padding: 8px;
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
        <div id="HeaderPanel">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 78%;">
                        <span>WCC Preparation</span>
                    </td>
                    <td style="width: 18%; text-align: right;">
                        <asp:LinkButton ID="LbtExport" Text="Export to Excel" runat="server" CssClass="ButtonExport"
                            Font-Names="verdana" Font-Size="11px"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GvWCCList" runat="server" AutoGenerateColumns="false" CellPadding="2"
                Width="99%">
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
                    <asp:BoundField DataField="PackageId" HeaderText="Package ID [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="WCTRDate" HeaderText="WCTR Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                        ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="BAUTDate" HeaderText="BAUT Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                        ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid" NullDisplayText="Not Yet Approved"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderWidth="2px" />
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
                            <a id="opennewframe" runat="server" class="fancybox fancybox.iframe"><span class="fontLink">
                                Site information</span></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
