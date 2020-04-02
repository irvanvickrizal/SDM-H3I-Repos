<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCOManualDoneUpload.aspx.vb"
    Inherits="CO_frmCOManualDoneUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Manual CO Done</title>
    <link href="../CSS/ValidationMessage.css" rel="stylesheet" type="text/css" />
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
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left:5px;
        }
        .gridHeader
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 1px;
            border-color: GrayText;
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
        .lblText
        {
            font-family: Verdana;
            font-size: 11px;
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
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
			Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
            function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Up1">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div id="HeaderPanel">
                CO Done With Manual Process
            </div>
            <div style="width: 99%; border-bottom-color: Black; border-bottom-style: solid; border-bottom-width: 1px;
                padding-bottom: 10px; margin-top: 10px;">
                <div>
                    <span class="lblText">Package ID[EPM]</span>
                    <asp:TextBox ID="TxtPackageId" CssClass="lblText" runat="server" Width="200px" ValidationGroup="wpidsearch"></asp:TextBox>
                </div>
                <div style="width: 315px; text-align: right; margin-top: 5px;">
                    <asp:LinkButton ID="LbtSearch" runat="server" Font-Names="Verdana" Font-Size="11px"
                        Text="Search" ValidationGroup="wpidsearch"></asp:LinkButton>
                </div>
            </div>
            <div style="margin-top: 10px;">
                <asp:GridView ID="GvCOTransaction" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    PageSize="15" EmptyDataText="No Record Found" Width="99%">
                    <HeaderStyle CssClass="gridHeader" />
                    <RowStyle CssClass="gridOdd" />
                    <AlternatingRowStyle CssClass="gridEven" />
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                            ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Pono" HeaderText="Po.No" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="SiteName" HeaderText="SiteName" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="PackageId" HeaderText="PackageId" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="Scope" HeaderText="Scope" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="InitiatorUser" HeaderText="Creator" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="WFDesc" HeaderText="Workflow" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:BoundField DataField="PendingRole" HeaderText="Pending User Group" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px" />
                        <asp:TemplateField HeaderText="CO Document" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridHeader"
                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="Solid" ItemStyle-BorderWidth="1px">
                            <ItemTemplate>
                                <asp:Label ID="LblDocPathStatus" runat="server" Text='<%#Eval("DocPathStatus") %>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LblDocStatus" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="12px"
                                    Font-Names="Verdana"></asp:Label>
                                <asp:Label ID="LblSWID" runat="server" Text='<%#Eval("swid") %>' Visible="false"></asp:Label>
                                <a id="viewdoclink" runat="server" class="fancyboxViewLog fancybox.iframe" href="#"
                                    style="border-style: none;">
                                    <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="24" width="24" style="border-style: none;" />
                                </a>
                                <asp:ImageButton ID="ImgUpdateTransaction" ImageUrl="~/images/Upload_File.png" runat="server"
                                    Style="border-style: none;" Height="24px" Width="25px" CommandName="updatetran"
                                    CommandArgument='<%#Eval("COID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LbtSearch" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
