<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MultipleDocReviewPendingList.aspx.vb"
    Inherits="DashBoard_MultipleDocReviewPendingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple Doc Review</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderPanel
        {
           width:99%;
           background-repeat: repeat-x;
           background-image: url(../Images/banner/BG_Banner.png);
           font-family:verdana;
           font-weight:bolder;
           font-size:10pt;
           color:white;
           padding-top:5px;
           padding-bottom:5px;
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
            Padding:5px;
            text-align:center;
            color:#ffffff;
        }
        .itemGridPadding
        {
            Padding:3px;
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
        .btnReview:hover
        {
            height:25px;
            width:80px;
            background-image: url(../Images/button/btnRevHOver.gif);
        }
        .btnReview:hover
        {
            height:25px;
            width:80px;
            background-image: url(../Images/button/btnRevHOver.gif);
        }
        .btnReview:click
        {
            height:25px;
            width:80px;
            background-image: url(../Images/button/btnRevClick.gif);
        }
        .btnDashboard
        {
            height:25px;
            width:100px;
            background-image: url(../Images/button/btnGoDashboard.gif);
        }
        .btnDashboard:hover
        {
            height:25px;
            width:100px;
            background-image: url(../Images/button/btnGoDashboardHOver.gif);
        }
        .btnDashboard:clicked
        {
            height:25px;
            width:100px;
            background-image: url(../Images/button/btnGoDashboardClick.gif);
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
                    alert("Please tick at least one document to be reviewed");
                    return false;
                }
                return true;
            }
            function WindowsCloseReviewer(){
                alert('Reviewed Sucessfully.');
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
            <div style="margin-left: 10px;">
                Multiple Review List
            </div>
        </div>
        <div>
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
            <asp:UpdatePanel ID="UpDocReview" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GvDocReview" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        Width="99%" PageSize="16" EmptyDataText="No documents to be reviewed">
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                            Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
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
                            <asp:TemplateField HeaderText="No.">
                                <HeaderStyle CssClass="headerGridPadding" />
                                <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="itemGridPadding" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="site_no" HeaderText="Site No" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" />
                            <asp:BoundField DataField="docname" HeaderText="Document Name" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" />
                            <asp:BoundField DataField="pono" HeaderText="Po No" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Package ID" HeaderStyle-CssClass="headerGridPadding"
                                ItemStyle-CssClass="itemGridPadding" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="submitDate" HeaderText="Submit Date" HeaderStyle-CssClass="headerGridPadding"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HtmlEncode="false" ItemStyle-CssClass="itemGridPadding"
                                ItemStyle-Width="100px" />
                            <asp:TemplateField>
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
                    <asp:AsyncPostBackTrigger ControlID="LbtReview" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div style="margin-top: 10px; width: 99%; text-align: right;">
            <asp:LinkButton ID="LbtReview" Width="80px" runat="server" Style="text-decoration: none;"
                OnClientClick="return ValidateCheckList()">
                <div class="btnReview"></div>
            </asp:LinkButton>
            <asp:LinkButton ID="LbtBackDashboard" Width="100px" runat="server" Style="text-decoration: none">
                <div class="btnDashboard"></div>
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
