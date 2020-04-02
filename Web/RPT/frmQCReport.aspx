<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmQCReport.aspx.vb" Inherits="RPT_frmQCReport"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>QC Done Report</title>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
            margin-top:15px;
            margin-bottom:10px;
            padding:3px;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:10pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:8pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:8pt;
        }
        .BtnExpt
        {
           border-style:solid;
           border-color:white;
           border-width:1px;
           font-family:verdana;
           font-size:11px;
           font-weight:bold;
           color:white;
           width:120px;
           height:25px;
           cursor:hand;
        }
        .btnSubmit
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:hand;
        }
        .btnRefresh
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:hand;
            height:25px;
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
          background-color:gray;
          -moz-user-select:none;
        }

        .AccordionContent
        {
          height:0px;
          overflow:auto;
          display:none; 
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

    <script type="text/javascript">
        function CheckFiltering()
        {
            var startdate = document.getElementById("TxtATPStartDate");
            var enddate = document.getElementById("TxtEndDateTime");
            if (startdate.value.length < 1)
            {
                alert("Please define start date first!");   
                return false;
            }
            else if (enddate.value.length < 1)
            {
                alert("Please define end date!");   
                return false;
            }              
        }
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>

    <style type="text/css">
        #accordion { list-style: none; margin: 0px 0; padding: 0; height: 150px; overflow: hidden; background: transparent;}   
        #accordion li { float: left; border-left: display: block; height: 150px; width: 20px; padding: 15px 0; overflow: hidden; color: #fff; text-decoration: none; font-size: 16px; line-height: 1.5em; border-left: 1px solid #fff;}   
        #accordion li img { border: none; border-right: 1px solid #fff; float: left; margin: -15px 15px 0 0; }   
        #accordion li.active { width: 410px; } 
        #docHeader,#logHeader {cursor:hand;}
        .dvPanelSearch
        {
            width:400px;
            font-family:verdana;
        }
        .chkText
        {
            font-family:verdana;
            font-size:8pt;
            color:#000000;
        }
        .txtSearch
        {
            font-family:verdana;
            font-size:8pt;
            color:#000000;
            
        }
        .buttonSearch
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnSearch_0.gif);
            text-align:right;
        }
        .buttonSearch:hover
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnSearch_1.gif);
        }
        .buttonSearch:click
        {
            height:26px;
            width:80px;
            background-image: url(../Images/button/BtnSearch_2.gif);
        }
    </style>

    <script type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>

    <script type="text/javascript" src="../Scripts/jquery.uniform.js"></script>

    <link rel="stylesheet" href="../CSS/uniform.default.css" type="text/css" />

    <script type="text/javascript">
        $(function(){ $(".txtSearch").uniform(); });
    </script>

    <script type="text/javascript">
        $(document).ready(function(){       
            activeItem = $("#accordion li:first");     
            $(activeItem).addClass('active');       
            $("#accordion li").click(function(){         
            $(activeItem).animate({width: "20px"}, 
            {duration:300, queue:false});         
            $(this).animate({width: "410px"}, 
            {duration:300, queue:false});         
            activeItem = this;     });   }); 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div class="HeaderReport">
            <table width="100%">
                <tr>
                    <td style="width: 80%">
                        QC Done Report
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportExcel2" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: 150px; width: 100%; margin-top: -10px;">
            <div style="width: 41%; float: left; padding-left: 2px; border-style: outset; border-width: 1px;
                border-color: #cfcfcf;">
                <ul id="accordion">
                    <li>
                        <img src="~/images/accordion/PanelSearchAdvance.png?id=x" id="pnlSearchHeaderAdvance"
                            style="cursor: pointer" runat="server" alt="docHeader" />
                        <div class="dvPanelSearch">
                            <div>
                                <table>
                                    <tr>
                                        <td style="width: 20px;">
                                            <input type="checkbox" id="ChkStartDate" runat="server" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="LblATPStartDate" runat="server" Text="Start Date" CssClass="chkText"
                                                Width="60px"></asp:Label>
                                            <asp:TextBox ID="TxtATPStartDate" runat="server" Height="14px" CssClass="txtSearch"
                                                BackColor="white"></asp:TextBox>
                                        </td>
                                        <td style="width: 60px;" align="left">
                                            <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                Width="18px" />
                                            <cc1:CalendarExtender ID="ceStartTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="BtnCalendar"
                                                TargetControlID="TxtATPStartDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20px;">
                                            <input type="checkbox" id="ChkEndDateTime" runat="server" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="LblEndDateTime" runat="server" Text="End Date" CssClass="chkText"
                                                Width="60px"></asp:Label>
                                            <asp:TextBox ID="TxtEndDateTime" runat="server" Height="14px" CssClass="txtSearch"
                                                BackColor="white"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnEndDateTime" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                Width="18px" />
                                            <cc1:CalendarExtender ID="ceEndDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="btnEndDateTime"
                                                TargetControlID="TxtEndDateTime">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr style="display:none;">
                                        <td style="width: 20px;">
                                            <input type="checkbox" id="ChkRegion" runat="server" />
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Label ID="LblRegion" runat="server" Text="Region" CssClass="chkText" Width="60px"></asp:Label>
                                            <asp:DropDownList ID="DdlRegion" runat="server" CssClass="GridOddRows">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="padding-left: 320px;">
                                <asp:LinkButton ID="LbtRefresh" runat="server" Style="text-decoration: none;">
                                    <div class="buttonSearch"></div>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </li>
                    <li>
                        <img src="~/images/accordion/PanelSearch_Search.png" id="pnlSearchHeader" runat="server"
                            style="cursor: pointer" alt="docHeader" />
                        <div class="dvPanelSearch">
                            <div style="margin-top: 20px;">
                                <span class="chkText">Search</span>
                                <asp:TextBox ID="TxtSearch" runat="server" CssClass="txtSearch" Width="280px" BackColor="white"></asp:TextBox>
                            </div>
                            <div style="padding-left: 290px; margin-top: 10px;">
                                <asp:LinkButton ID="LbtSearch" runat="server" Style="text-decoration: none; text-align: right;">
                                    <div class="buttonSearch"></div>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div style="float: left; width: 59%;">
            </div>
        </div>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="margin-top: 10px; width: 100%; overflow: scroll;">
            <asp:UpdatePanel ID="upATPReport" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="LblErrorMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                            ForeColor="red" Font-Bold="true" Text="You can not define end date time less than start date time!"></asp:Label>
                    </div>
                    <asp:GridView ID="GvQCReport" runat="server" AllowPaging="False" EmptyDataText="No Data Record Found"
                        CellPadding="1" CellSpacing="2" Width="100%" Font-Names="Verdana" Font-Size="11px"
                        AllowSorting="True" AutoGenerateColumns="False">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Siteno" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_name" HeaderText="Site Name" />
                            <asp:BoundField DataField="OSSName" HeaderText="OSS Name" NullDisplayText="Null" />
                            <asp:BoundField DataField="Pono" HeaderText="PO Number" />
                            <asp:BoundField DataField="workpackageid" HeaderText="Package ID" />
                            <asp:BoundField DataField="QCRefNo" HeaderText="Reference No" />
                            <asp:BoundField DataField="NEType" HeaderText="NE Type" />
                            <asp:BoundField DataField="BTSType" HeaderText="BTSE Type" />
                            <asp:BoundField DataField="BSCName" HeaderText="BSC Name" />
                            <asp:BoundField DataField="NewSiteID" HeaderText="New Site ID" />
                            <asp:BoundField DataField="LAC" HeaderText="LAC" />
                            <asp:BoundField DataField="CI" HeaderText="CI" />
                            <asp:BoundField DataField="BCFET" HeaderText="BCF/ET" />
                            <asp:BoundField DataField="ClutterType" HeaderText="Clutter Type" />
                            <asp:BoundField DataField="existingcon" HeaderText="config" />
                            <asp:BoundField DataField="typeofwork" HeaderText="Scope" />
                            <asp:BoundField DataField="scopedesc" HeaderText="Scope description" />
                            <asp:BoundField DataField="integrationdate" HeaderText="Integration Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="AcceptanceDate" HeaderText="Acceptance Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="Ara_Name" HeaderText="Area" />
                            <asp:BoundField DataField="Rgnname" HeaderText="Region" />
                            <asp:BoundField DataField="NSNReviewer" HeaderText="NSN User" />
                            <asp:BoundField DataField="NSNReviewDate" HeaderText="NSN ExecuteDate" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="TSelApprover" HeaderText="Tsel User" />
                            <asp:BoundField DataField="TSelApproveDate" HeaderText="TSel Execute Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:CheckBoxField DataField="ChkKPI" HeaderText="KPI Doc" ReadOnly="true" />
                            <asp:CheckBoxField DataField="ChkDrive" HeaderText="Drive Test Doc" ReadOnly="true" />
                            <asp:CheckBoxField DataField="ChkAlarm" HeaderText="Alarm Doc" ReadOnly="true" />
                            <asp:CheckBoxField DataField="ChkOther" HeaderText="Other Doc" ReadOnly="true" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='../PO/frmViewDocumentQC.aspx?id=<%#Eval("SW_Id") %>' target="_blank" style="text-decoration: none;
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
                    <asp:AsyncPostBackTrigger ControlID="LbtRefresh" />
                    <asp:AsyncPostBackTrigger ControlID="LbtSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
