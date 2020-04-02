<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmATPReport.aspx.vb" Inherits="RPT_frmATPReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP REPORT</title>
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
           cursor:pointer;
        }
        .btnSubmit
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:pointer;
        }
        .btnRefresh
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bold;
            color:black;
            padding:1px;
            cursor:pointer;
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
        var ContentHeight = 100;
        var TimeToSlide = 250.0;

        var openAccordion = '';

        function runAccordion(index)
        {
          var nID = "Accordion" + index + "Content";
          if(openAccordion == nID)
            nID = '';
            
          setTimeout("animate(" + new Date().getTime() + "," + TimeToSlide + ",'" 
              + openAccordion + "','" + nID + "')", 33);
          
          openAccordion = nID;
        }
        function animate(lastTick, timeLeft, closingId, openingId)
        {  
          var curTick = new Date().getTime();
          var elapsedTicks = curTick - lastTick;
          
          var opening = (openingId == '') ? null : document.getElementById(openingId);
          var closing = (closingId == '') ? null : document.getElementById(closingId);
         
          if(timeLeft <= elapsedTicks)
          {
            if(opening != null)
              opening.style.height = ContentHeight + 'px';
            
            if(closing != null)
            {
              closing.style.display = 'none';
              closing.style.height = '0px';
            }
            return;
          }
         
          timeLeft -= elapsedTicks;
          var newClosedHeight = Math.round((timeLeft/TimeToSlide) * ContentHeight);

          if(opening != null)
          {
            if(opening.style.display != 'block')
              opening.style.display = 'block';
            opening.style.height = (ContentHeight - newClosedHeight) + 'px';
          }
          
          if(closing != null)
            closing.style.height = newClosedHeight + 'px';

          setTimeout("animate(" + curTick + "," + timeLeft + ",'" 
              + closingId + "','" + openingId + "')", 33);
        }
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

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div class="HeaderReport">
            <table width="100%">
                <tr>
                    <td style="width: 80%">
                        ATP Done Report
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportExcel2" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="border-style: solid; border-width: 1px; border-color: Gray; margin-top: 10px;
            text-align: center;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="LblATPStartDate" runat="server" Text="Start Date" CssClass="GridOddRows"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtATPStartDate" runat="server" Height="14px" CssClass="GridOddRows"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" />
                        <cc1:CalendarExtender ID="ceStartTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="BtnCalendar"
                            TargetControlID="TxtATPStartDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width: 100px;">
                    </td>
                    <td>
                        <asp:Label ID="LblEndDateTime" runat="server" Text="End Date" CssClass="GridOddRows"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEndDateTime" runat="server" Height="14px" CssClass="GridOddRows"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnEndDateTime" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" />
                        <cc1:CalendarExtender ID="ceEndDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="btnEndDateTime"
                            TargetControlID="TxtEndDateTime">
                        </cc1:CalendarExtender>
                    </td>
                    <td style="width: 20px;">
                    </td>
                    <td>
                        <asp:ImageButton ID="ImgRefresh" runat="server" ImageUrl="~/Images/btnrefreshnew.png" Height="22px" OnClientClick="return CheckFiltering();" />
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
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
        <div style="margin-top: 10px;">
            <asp:UpdatePanel ID="upATPReport" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="LblErrorMessage" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="red" 
                            Font-Bold="true" Text="You can not define end date time less than start date time!"></asp:Label>
                    </div>
                    <asp:GridView ID="GvATPReport" runat="server" AllowPaging="False" EmptyDataText="No Data Record Found"
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
                            <asp:BoundField DataField="Pono" HeaderText="PO Number" />
                            <asp:BoundField DataField="workpackageid" HeaderText="Package ID" />
                            <asp:BoundField DataField="Ara_Name" HeaderText="Area" />
                            <asp:BoundField DataField="Rgnname" HeaderText="Region" />
                            <asp:BoundField DataField="Initiator" HeaderText="Initiator Name" />
                            <asp:BoundField DataField="InitiatorUploadDate" HeaderText="Initiator Upload Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="H3IReview" HeaderText="H3I Approver" />
                            <asp:BoundField DataField="H3IReviewDate" HeaderText="H3I Approved Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a href='../PO/frmViewDocument.aspx?id=<%#Eval("SW_Id") %>' target="_blank" style="text-decoration: none;
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
                    <asp:AsyncPostBackTrigger ControlID="ImgRefresh" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:Panel ID="PnlViewPopUp" runat="server">
            </asp:Panel>
        </div>
    </form>
</body>
</html>
