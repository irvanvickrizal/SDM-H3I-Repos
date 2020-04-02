<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListATPPhotoDoc.aspx.vb"
    Inherits="GeoTag_frmListATPPhotoDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP Geo Tagging</title>
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
         #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align:center;
            height : 100px;
            width:100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%; margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color:#ffffff;
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
    </script>

    <script type="text/javascript">
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
        <div>
            <div id="HeaderPanel">
                <asp:HiddenField ID="HdnSubconId" runat="server" />
                <div style="margin-left: 10px;">
                    View Geo tagging ATP Photo Document
                </div>
            </div>
            <asp:MultiView ID="MvCorePanel" runat="server">
                <asp:View ID="VwPhotoListDoc" runat="server">
                    <div style="margin-top: 10px;">
                        <asp:GridView ID="GvPhotoListDoc" runat="server" AutoGenerateColumns="false" EmptyDataText="Document not found"
                            Width="99%">
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
                                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" HeaderStyle-CssClass="gridHeader"
                                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" />
                                <asp:BoundField DataField="UserUploadName" HeaderText="Engineer Name" HeaderStyle-CssClass="gridHeader"
                                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" />
                                <asp:TemplateField HeaderText="Photo Doc" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                    ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a id="viewdoclink" runat="server">
                                            <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="18" width="18" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Creation Status"
                                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="1px" ItemStyle-BorderColor="#c3c3c3"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSWID" runat="server" Text='<%#Eval("SWID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblATPDocPhotoId" runat="server" Text='<%#Eval("ATPDocPhotoId") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="LblChecklistStatus" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                                        <asp:Label ID="LblWorkPackageId" runat="server" Visible="false" Text='<%#Eval("PackageId") %>'></asp:Label>
                                        <asp:ImageButton ID="imgPDFUpload" runat="server" ToolTip="Upload ATP Doc Checklist"
                                            Width="25px" Height="25px" ImageUrl="~/images/UploadPDF.gif" CommandName="uploaddoc"
                                            CommandArgument='<%#Eval("ATPDocPhotoId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div style="width: 99%; margin-top: 5px; text-align: right;">
                            <asp:LinkButton ID="LbtViewHistoricalMergeDoc" runat="server" Text="View Doc Already Merged"
                                Font-Names="verdana" Font-Size="11px"></asp:LinkButton>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="VwDocMergeDetail" runat="server">
                    <asp:HiddenField ID="hdnATPPhotoDocId" runat="server" />
                    <asp:HiddenField ID="hdnready4baut" runat="server" />
                    <div style="margin-top: 10px;">
                        <asp:UpdateProgress ID="upgATPReport" runat="server" AssociatedUpdatePanelID="Up1"
                            DisplayAfter="0">
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
                                <div id="detaildocmerge">
                                    <div style="padding: 5px; border-style: solid; border-width: 2px; border-color: Gray;
                                        width: 50%;">
                                        <table cellpadding="2">
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">SiteNo</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSiteNo" runat="server" CssClass="lblText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">SiteName</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblSitename" runat="server" CssClass="lblText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">PoNo</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblPoNo" runat="server" CssClass="lblText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">Scope</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblScope" runat="server" CssClass="lblText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">Workpackageid</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblWPID" runat="server" CssClass="lblText"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="lblTextBold">ATP Photo Doc</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <a id="viewatpphotodoclink" runat="server" class="fancyboxViewLog fancybox.iframe"
                                                        href="#">
                                                        <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="25" width="25" />
                                                    </a>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    <span class="lblTextBold">ATP Doc Checklist</span>
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblATPDocChecklistStatus" runat="server" CssClass="lblText" ForeColor="Red"
                                                        Font-Italic="true" Font-Bold="true"></asp:Label>
                                                    <a id="viewatpdocchecklistlink" runat="server" class="fancyboxViewLog fancybox.iframe"
                                                        href="#">
                                                        <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="25" width="25" /></a>
                                                    <asp:ImageButton ID="ImgRemove" runat="server" ImageUrl="~/images/deldoc.gif" CausesValidation="false"
                                                        OnClientClick="return confirm('Are you sure you want to delete this ATP Doc Checklist?');" />
                                                    <br />
                                                    <asp:FileUpload ID="FUDocChecklist" runat="server" />
                                                    <asp:RegularExpressionValidator ID="revDocUpload" runat="server" ControlToValidate="FUDocChecklist"
                                                        ValidationGroup="UploadDocChecklist" ErrorMessage="Please select a .pdf file Only"
                                                        Display="None" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F))|((d|D)(o|O)(c|C)))$"></asp:RegularExpressionValidator>
                                                    <asp:Button ID="BtnUploadChecklist" runat="server" Text="Upload Doc" CssClass="btnStyle2"
                                                        ValidationGroup="UploadDocChecklist" />
                                                    <asp:ValidationSummary ID="VsDocChecklist" runat="server" DisplayMode="SingleParagraph"
                                                        HeaderText="Warning Message" ValidationGroup="UploadDocChecklist" ShowMessageBox="true"
                                                        ShowSummary="false" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="margin-top: 5px; text-align: right; width: 99%;">
                                            <asp:Label ID="LblMergeStatus" runat="server" Font-Names="verdana" Font-Size="11px"
                                                Font-Bold="true"></asp:Label>
                                            <a id="viewmergelog" runat="server" class="fancyboxViewLog fancybox.iframe" href="#">
                                                <span class="lblText" style="color: Blue; cursor:pointer;">View Log</span></a>
                                            <asp:Button ID="BtnMergeDoc" runat="server" Text="Merge Document" CssClass="btnStyle2"
                                                OnClientClick="return confirm('Are you sure you want to merge these document?');" />
                                        </div>
                                    </div>
                                    <div style="margin-top: 10px; text-align: right; width: 99%;">
                                        <hr />
                                        <asp:Button ID="BtnBackToList" runat="server" Text="Back To list" CssClass="btnStyle" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnMergeDoc" />
                                <asp:PostBackTrigger ControlID="BtnUploadChecklist" />
                                <asp:PostBackTrigger ControlID="BtnBackToList" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div id="detailmergelog">
                            <asp:GridView ID="GvATPDocMergeLog" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                            </asp:GridView>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
