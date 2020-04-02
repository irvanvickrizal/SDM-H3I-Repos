<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewDocument.aspx.vb"
    Inherits="WCC_frmViewDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC View Document</title>
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
    .fancybox-title-inside {
            text-align: center;
            font-family:verdana;
            font-size:18px;
        }
         .gridHeader
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
        <div>
            <div id="HeaderPanel">
                <asp:HiddenField ID="HdnSubconId" runat="server" />
                <div style="margin-left: 10px;">
                    View Document
                </div>
            </div>
            <div style="width: 99%; border-bottom-color: Black; border-bottom-style: solid; border-bottom-width: 1px;
                padding-bottom: 10px; margin-top: 10px;">
                <div>
                    <asp:DropDownList ID="DdlSearchType" runat="server" CssClass="lblText">
                        <asp:ListItem Text="Package ID[EPMD]" Value="1"></asp:ListItem>
                        <asp:ListItem Text="PO Subcon" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:TextBox ID="TxtPackageId" CssClass="lblText" runat="server" Width="200px" ValidationGroup="wpidsearch"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RfvPackageId" runat="server" CssClass="dnnFormMessage dnnFormError"
                        SetFocusOnError="true" Font-Names="Verdana" Font-Size="11px" ControlToValidate="TxtPackageId"
                        ErrorMessage="Package ID is Required" ValidationGroup="wpidsearch"></asp:RequiredFieldValidator>
                </div>
                <div style="width: 315px; text-align: right; margin-top: 5px;">
                    <asp:LinkButton ID="LbtSearch" runat="server" CssClass="dnnPrimaryAction" Font-Names="Verdana"
                        Font-Size="11px" Text="Search" ValidationGroup="wpidsearch"></asp:LinkButton>
                </div>
            </div>
            <div style="margin-top: 10px;">
                <asp:MultiView ID="MvDisplayResult" runat="server">
                    <asp:View ID="VwWPIDNoResult" runat="server">
                        <asp:Label ID="LblNotFound" runat="server" Text="WorkpackageId not found" CssClass="lblText"
                            Font-Bold="true" Font-Italic="true" ForeColor="red"></asp:Label>
                    </asp:View>
                    <asp:View ID="VwSubconFilterSearch" runat="server">
                        <span class="lblText">Subcon</span>&nbsp;<asp:DropDownList ID="DdlSubconFilter" runat="server"
                            CssClass="lblText" AutoPostBack="true">
                        </asp:DropDownList>
                    </asp:View>
                    <asp:View ID="VwSubconView" runat="server">
                    </asp:View>
                </asp:MultiView>
            </div>
            <div style="margin-top: 10px;">
                <asp:GridView ID="GvViewDocuments" runat="server" AutoGenerateColumns="false" EmptyDataText="Document not found"
                    Width="99%">
                    <HeaderStyle CssClass="gridHeader" />
                    <RowStyle CssClass="gridOdd" />
                    <AlternatingRowStyle CssClass="gridEven" />
                    <Columns>
                        <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Docname" HeaderText="Document" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="LMDT" HeaderText="UploadDate" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy}"
                            ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="SiteName" HeaderText="SiteName" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="PONO" HeaderText="PO Telkomsel" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:BoundField DataField="POSubcon" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                        <asp:TemplateField HeaderStyle-CssClass="gridHeader" ItemStyle-BorderStyle="solid"
                                ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="LblSWID" runat="server" Text='<%#Eval("WCCSiteDocId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblWCCID" runat="server" Text='<%#Eval("wccid") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("docid") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblDocType" runat="server" Text='<%#Eval("ParentDocType") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblPackageId" runat="server" Text='<%#Eval("PackageId") %>' Visible="false"></asp:Label>
                                <a id="viewdoclink" runat="server" class="fancyboxViewLog fancybox.iframe" href="#"
                                    visible='<%#Eval("IsUploaded") %>'>
                                    <img src="../Images/ViewDoc.jpg" alt="viewdoc" height="18" width="18" /></a> <a id="viewlog" runat="server"
                                        class="fancyboxViewLog fancybox.iframe" href="#">
                                        <img src="../Images/ViewLog.jpg" alt="viewlog" height="18" width="18" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div style="margin-top:5px; width:99%; text-align:right;">
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
