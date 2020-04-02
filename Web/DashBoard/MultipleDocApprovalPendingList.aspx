<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MultipleDocApprovalPendingList.aspx.vb"
    Inherits="DashBoard_MultipleDocApprovalPendingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple Approval Form</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
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
    var atLeast = 1
    function ValidateCheckList(){     
                var CHK = document.getElementById("<%=GvDocReview.ClientID%>"); 
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
                    alert("Please tick at least one document to be approved");
                    return false;
                }
                else
                {
                    var answer = confirm("Are you sure want to approve the document will be approved with Multiple approval ?");
                        if (answer){
		                    return true;
	                    }
	                    else{
		                    return false;
	                    }
                }
                return true;
            }
            function WindowsCloseReviewer(){
                alert('Multiple Approval document Process Sucessfully.');
                return true;
            }
            function WindowsCloseErrorReviewer(){
                alert('Multiple Approval document Process Failed.');
                return false;
            }
    </script>

    <script type="text/javascript">
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

    <style type="text/css">
                #PleaseWait
                {
                    z-index: 200;
                    position: absolute;
                    top: 0pt;
                    left: 0pt;
                    text-align:center;
                    height : 100px;
                    width:100px;
                    background-image: url(../Images/animation_processing.gif);
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
                    z-index: 1;
                    height: 100%;
                    position:fixed;
                    top: 0;
                    left: 0;
                }
            </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div class="HeaderPanel">
            <div style="margin: 5px;">
                Multiple Approval List
            </div>
        </div>
        <div style="margin-top: 10px;">
            <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div id="blur">
                        <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                            <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                                height="150" alt="Processing" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpDocReview" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:GridView ID="GvDocReview" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        Width="99%" PageSize="16" EmptyDataText="No documents to be approved">
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                        <HeaderStyle CssClass="headerGridPadding" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                            Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input id="ChkReview" runat="server" type="checkbox" />
                                    <asp:Label ID="LblSiteid" runat="server" Text='<%# Eval("Site_Id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="LblSiteVersion" runat="server" Text='<%# Eval("SiteVersion") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="LblSNO" runat="server" Text='<%#Eval("sno") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("docid") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="headerGridPadding">
                                <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="itemGridPadding" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
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
                            <asp:BoundField DataField="workpackageid" HeaderText="Package ID" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="submitDate" HeaderText="Submit Date" HeaderStyle-CssClass="headerGridPadding"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HtmlEncode="false" ItemStyle-CssClass="itemGridPadding"
                                ItemStyle-Width="100px" />
                            <asp:TemplateField ItemStyle-CssClass="itemGridPadding" ItemStyle-HorizontalAlign="Center" HeaderText="Remarks"
                                ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:Label ID="LblRemarks" runat="server"></asp:Label>
                                </ItemTemplate>    
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="itemGridPadding" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <a href='../PO/frmViewDocument.aspx?id=<%#Eval("sw_id") %>' target="_blank" style="text-decoration: none;
                                        border-style: none;">
                                        <img src="../Images/Pdf_Icon.png" alt="pdficon" id="pdfIcon" height="18" width="18"
                                            style="text-decoration: none; border-style: none;" />
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="LbtReview" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="margin-top: 10px; width: 99%; text-align: right;">
            <asp:HiddenField ID="HdnIsAuthorized" runat="server" />
            <asp:MultiView ID="MvDigitalSignatureLogin" runat="server">
                <asp:View ID="VwNotLogin" runat="server">
                    <div style="width: 100%;">
                        <table align="center" class="pageTitle" width="350px">
                            <tr>
                                <td colspan="2" align="center">
                                    <div style="background-color: #c3c3c3; padding: 4px;">
                                        <span style="font-family: Verdana; font-size: 13px; color: #000000;">Digital Signature
                                            Login</span>
                                    </div>
                                </td>
                            </tr>
                            <tr valign="middle" align="left">
                                <td class="lblTitle">
                                    User Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" Width="250px" CssClass="textFieldStyle"
                                        ReadOnly="True" Style="padding-top: 3px; padding-left: 2px; padding-bottom: 3px;"></asp:TextBox>
                                </td>
                                <td>
                                    <span style="font-size: 16px; color: red">*</span>
                                </td>
                            </tr>
                            <tr valign="middle">
                                <td class="lblTitle">
                                    Password
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" Width="250px" runat="server" CssClass="textFieldStyle"
                                        TextMode="Password" Style="padding-top: 3px; padding-left: 2px; padding-bottom: 3px;"></asp:TextBox>
                                </td>
                                <td>
                                    <span style="font-size: 16px; color: red">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkrequest" runat="server" OnClientClick="this.style.display = 'none'; loadingdiv.style.display = '';">Request Password</asp:LinkButton>
                                </td>
                                <td style="height: 40px">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="buttonStyle" Text="Login Signature"
                                        Width="144px" />&nbsp;
                                    <asp:Label ID="LblErrorMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="VwLogin" runat="server">
                    <asp:LinkButton ID="LbtReview" Width="55px" runat="server" CssClass="dnnPrimaryAction"
                        Text="Approve" OnClientClick="return ValidateCheckList()" Font-Names="Verdana"
                        Font-Size="11px">
                    </asp:LinkButton>
                    <asp:LinkButton ID="LbtBackDashboard" Width="115px" runat="server" CssClass="dnnPrimaryAction"
                        Text="Go To Dashboard" Font-Names="Verdana" Font-Size="11px">
                    </asp:LinkButton>
                </asp:View>
                <asp:View ID="VwEmptyDataRow" runat="server">
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
