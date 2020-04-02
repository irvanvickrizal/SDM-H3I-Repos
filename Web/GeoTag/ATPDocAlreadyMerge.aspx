<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATPDocAlreadyMerge.aspx.vb"
    Inherits="GeoTag_ATPDocAlreadyMerge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP Doc Already Merge</title>
    <style type="text/css">
        #HeaderPanel
        {
            width: 99%;
            background-repeat: repeat-x;
            background-color: #c5c3c3;
            font-family: verdana;
            font-weight: bolder;
            font-size: 10pt;
            color: white;
            padding-top: 8px;
            padding-bottom: 8px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            text-shadow: 0px 1px 1px #000;
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
        .gridHeader
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #5042cc;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: White;
            border-style: solid;
            border-width: 1px;
            border-color: #c3c3c3;
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
        .lblTextBold
        {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bolder;
        }
        .lblText
        {
            font-family: Verdana;
            font-size: 11px;
        }
        .btnStyle
        {
            background-color: #c3c3c3;
            color: White;
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
            cursor: pointer;
        }
        .btnStyle2
        {
            background-color: #c3c3c3;
            color: Black;
            font-weight: bolder;
            font-family: Verdana;
            font-size: 10px;
            padding: 2px;
            cursor: pointer;
            border-style: solid;
            border-color: Gray;
            border-width: 2px;
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
        <div id="HeaderPanel">
            <asp:HiddenField ID="HdnSubconId" runat="server" />
            <div style="margin-left: 10px;">
                Historical ATP + GeoTag Merge Document
            </div>
        </div>
        <div style="margin-top: 10px;">
            <asp:GridView ID="GvHistoricalMergeDoc" runat="server" AutoGenerateColumns="false"
                EmptyDataText="Document not found" Width="99%">
                <HeaderStyle CssClass="gridHeader" />
                <RowStyle CssClass="gridOdd" />
                <AlternatingRowStyle CssClass="gridEven" />
                <Columns>
                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                        ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3">
                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" />
                    <asp:BoundField DataField="PoNo" HeaderText="PONO" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" />
                    <asp:BoundField DataField="PackageId" HeaderText="WorkpackageID" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Scope" HeaderText="Scope" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Fullname" HeaderText="Initiator" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SignTitle" HeaderText="Role" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Photo Doc" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                        ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="LblATPPhotoDocId" runat="server" Text='<%#Eval("atpphotodocid") %>'
                                Visible="false"></asp:Label>
                            <a id="viewdoclink" runat="server" class="fancyboxViewLog fancybox.iframe" href="#">
                                <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="18" width="18" /></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
