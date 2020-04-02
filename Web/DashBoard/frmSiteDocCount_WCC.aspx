<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocCount_WCC.aspx.vb"
    Inherits="DashBoard_frmSiteDocCount_WCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard Site Doc Count [WCC]</title>
    <style type="text/css">
        .PnlHeader
        {
            padding: 3px;
            background-color: #7f7f7f;
            color: #ffffff;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bolder;
        }
        .PnlHeader2
        {
            padding: 7px;
            background-color: #7f7f7f;
            color: #ffffff;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bolder;
        }
        .BtnATPOnline
        {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            border-width: 1px;
            border-color: #ffffff;
            background-color: #cccccc;
            color: #3F48CC;
            height: 20px;
            cursor: hand;
        }
        .BtnATPViewAll
        {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            border-width: 1px;
            border-color: black;
            background-color: #cccccc;
            color: #3F48CC;
            height: 20px;
            cursor: hand;
            width: 60px;
        }
        .gridHeader
        {
            font-family: Verdana;
            font-size: 10pt;
            background-color: Maroon;
            padding: 5px;
            color: white;
        }
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
        }
        .fancybox-title-inside
        {
            text-align: center;
            font-family: verdana;
            font-size: 18px;
        }
        .gridHeader_2
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 2px;
            border-color: gray;
        }
        .gridOdd
        {
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
        }
        .gridEven
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            padding: 5px;
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
    			
    			$('.fancyboxViewLog').fancybox({
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
    <div style="width: 99%;">
        <div id="ATPHeader" class="PnlHeader">
            <table>
                <tr>
                    <td style="width: 90%">
                        WCC Document Pending
                    </td>
                    <td style="width: 9%; text-align: right;">
                        <div style="text-align: right; width: 100%;">
                            <asp:Button ID="BtnGoToDashboard" runat="server" Text="Go To Dashboard" CssClass="BtnATPOnline" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="WCCDocument" style="margin-top: 10px; margin-left: 1px;">
        <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="False" EmptyDataText="No WCC Document Pending"
            Width="99%" AllowSorting="True" AutoGenerateColumns="False">
            <PagerSettings Position="TopAndBottom" />
            <AlternatingRowStyle CssClass="gridEven" />
            <RowStyle CssClass="gridOdd" />
            <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText">
                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="PONO" HeaderText="PO.Number" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="POName" HeaderText="PO.Name" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="Scope" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="ScopeName" HeaderText="Detail Scope" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:BoundField DataField="IssuanceDate" HeaderText="Issuance Date" HeaderStyle-CssClass="gridHeader_2"
                    HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                    ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="LblSNO" runat="server" Text='<%#Eval("sno") %>' Visible="false"></asp:Label>
                        <asp:LinkButton ID="LbtCheckDocument" runat="server" Text="Check Document" CommandName="checkdoc"
                            CommandArgument='<%#Eval("wccid") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderStyle="solid"
                    ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="LblWCCID" runat="server" Text='<%#Eval("wccid") %>' Visible="false"></asp:Label>
                        <a id="viewlog" runat="server" class="fancyboxViewLog fancybox.iframe" href="#">View
                            Log</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
