<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultipleApprovalProcess_BP.aspx.vb"
    Inherits="Admin_frmMultipleApprovalProcess_BP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple Approval Process</title>
    <style type="text/css">
        .HeaderPanel
        {
           width:98.5%;
           background-color: #c3c3c3;
           font-family:verdana;
           font-weight:bolder;
           font-size:14px;
           color:white;
           padding:3px;
           border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
        }
        .lblSearch
        {
            font-family:verdana;
            font-size:10px;
        }
        .TxtSearch
        {
            font-family:verdana;
            font-size:11px;
            height:14px;
        }
        .AccordionTitle, .AccordionContent, .AccordionContainer
        {
          position:relative;
          width:280px;
        }

        .AccordionTitle
        {
          height:20px;
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
          height:0px;
          overflow:auto;
          display:none; 
        }

        .AccordionContainer
        {
          border-top: solid 1px #cfcfcf;
          border-bottom: solid 1px #cfcfcf;
          border-left: solid 2px #cfcfcf;
          border-right: solid 2px #cfcfcf;
          
        }
        .headerGridPadding
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
        .itemGridPadding
        {
            Padding:5px;
            border-style:solid;
	        border-width:2px;
	        border-color:gray;
        }
        .EmptyDataRowStyle
        {
            padding:5px;
            font-family:verdana;
            font-size:11px;
        }
        .btnReview
        {
            height:25px;
            width:80px;
            background-image: url(../Images/button/btnRev.gif);
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
        
    </style>
    <script type="text/javascript">
        function WindowsCloseApprover(){
                alert('Signed Sucessfully');
                return true;
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="HeaderPanel">
            <div style="margin: 5px;">
                Pending Multiple Approval Process
            </div>
        </div>
        <div style="margin-top: 10px; width: 99%;">
            <asp:GridView ID="GvDocReview" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                Width="99%" PageSize="16" EmptyDataText="No documents Found">
                <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                <HeaderStyle CssClass="headerGridPadding" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                    Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="headerGridPadding">
                        <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="itemGridPadding" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LbtApprove" runat="server" CommandName="approve" CommandArgument='<%#Eval("SNO") %>' Text="Approve"></asp:LinkButton>
                            <asp:Label ID="LblUserLogin" runat="server" Text='<%#Eval("UserLogin") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblUsername" runat="server" Text='<%#Eval("Username") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblRoleId" runat="server" Text='<%#Eval("RoleId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("DocId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblDocName" runat="server" Text='<%#Eval("docname") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblDocPath" runat="server" Text='<%#Eval("DocPath") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblPageNo" runat="server" Text='<%#Eval("PageNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblXVAL" runat="server" Text='<%#Eval("XVal") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblYVAL" runat="server" Text='<%#Eval("YVal") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblSiteNo" runat="server" Text='<%#Eval("SiteNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblPoNo" runat="server" Text='<%#Eval("PoNo") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblUserID" runat="server" Text='<%#Eval("USERID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblSiteID" runat="server" Text='<%#Eval("SiteId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblSiteVersion" runat="server" Text='<%#Eval("SiteVersion") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LblPackageId" runat="server" Text='<%#Eval("PackageId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="siteno" HeaderText="Site No" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" />
                    <asp:BoundField DataField="sitename" HeaderText="Site name" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" />
                    <asp:BoundField DataField="docname" HeaderText="Document Name" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="pono" HeaderText="Po No" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Scope" HeaderText="Scope" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" />
                    <asp:BoundField DataField="PackageId" HeaderText="Package ID" HeaderStyle-CssClass="headerGridPadding"
                        ItemStyle-CssClass="itemGridPadding" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="submitDate" HeaderText="Submit Date" HeaderStyle-CssClass="headerGridPadding"
                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HtmlEncode="false" ItemStyle-CssClass="itemGridPadding"
                        ItemStyle-Width="100px" />
                    <asp:TemplateField ItemStyle-CssClass="itemGridPadding" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <a href='../PO/frmViewDocument.aspx?id=<%#Eval("SWID") %>' target="_blank" style="text-decoration: none;
                                border-style: none;">
                                <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                    style="text-decoration: none; border-style: none;" />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Label ID="LblDocTest" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
