<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEBASTDone.aspx.vb" Inherits="RPT_frmEBASTDone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FAC DONE</title>
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
           font-size:8pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:7pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:7pt;
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
            cursor:hand;
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

        .btnRefresh
        {
            height:25px;
            width:90px;
            background-image: url(../Images/button/btnRefresh.gif);
            text-decoration:none;
        }
        .btnRefresh:hover
        {
            height:25px;
            width:90px;
            background-image: url(../Images/button/btnRefreshHOver.gif);
            text-decoration:none;
        }
        .btnRefresh:visited
        {
             height:25px;
             width:90px;
             background-image:url(../Images/button/btnRefreshActive.gif);
             text-decoration:none;
        }

    </style>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '800px';
                    
                }
            }
        )
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
    #leftpanelheader
    {
        width:85%;
        float:left;
        background-image: url(../Images/banner/BG_Banner.png);
        height:30px;
    }
    #rightpanelheader
    {
        width:15%;
        float:right;
        background-image: url(../Images/banner/BG_Banner.png);
        height:30px;
        text-align:right;
        
    }
    .lblBASTTotal
    {
      font-family:verdana;
      font-size:9pt;
      color:red;
      font-weight:bolder;
     
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="width: 100%;">
            <div class="HeaderReport">
                <table width="100%">
                    <tr>
                        <td style="width: 85%">
                            FAC DONE REPORT
                        </td>
                        <td style="width: 15%">
                            <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border-style: solid; border-width: 1px; border-color: Gray; margin-top: 10px;">
                <table cellpadding="1" cellspacing="2">
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
                            <asp:LinkButton ID="LbtRefresh" runat="server" OnClientClick="return CheckFiltering()"
                                Style="text-decoration: none">
                            <div class="btnRefresh"></div>
                            </asp:LinkButton>
                        </td>
                        <td style="width: 50px;">
                        </td>
                        <td>
                            <asp:Label ID="LblBASTDoneTotal" runat="server" CssClass="lblBASTTotal">
                            </asp:Label>
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
            <div style="margin-top: 10px; width: 100%;">
                <asp:Panel ID="PnlGetReport" runat="server" ScrollBars="Auto" Width="100%" BorderStyle="None">
                    <asp:UpdatePanel ID="upBASTReport" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:Label ID="LblWarningMessage" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    ForeColor="red" Text="For the first time, please define period of date the document already approved"></asp:Label>
                                <asp:Label ID="LblErrorMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="red" Font-Bold="true" Text="You can not define end date time less than start date time!"></asp:Label>
                            </div>
                            <asp:GridView ID="GvBASTReport" runat="server" AllowPaging="False" CellPadding="1"
                                CellSpacing="2" Width="100%" Font-Names="Verdana" AllowSorting="True" AutoGenerateColumns="False">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <EmptyDataTemplate>
                                    <div style="width: 99%; padding: 2px; border-style: none;">
                                        <span style="font-family: Verdana; font-size: 8pt; color: Red;">No Record Data found</span>
                                    </div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="GridOddRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Region" HeaderText="Region" />
                                    <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                                    <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                    <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID" />
                                    <asp:BoundField DataField="TSELPROJECTID" HeaderText="TSEL ID" />
                                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                    <asp:BoundField DataField="POName" HeaderText="Po Name" />
                                    <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                                    <asp:BoundField DataField="BAUTRefNo" HeaderText="BAUT Ref. No." />
                                    <asp:BoundField DataField="BAUTNSN" HeaderText="BAUT Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="BUnsnuser" HeaderText="BAUT User(NSN)" />
                                    <asp:BoundField DataField="BAUTTELKOMSEL" HeaderText="BAUT Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="BUtelkomseluser" HeaderText="BAUT User(Telkomsel)" />
                                    <asp:BoundField DataField="BASTNSN" HeaderText="BAST Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="BSnsnuser" HeaderText="BAST User(NSN)" />
                                    <asp:BoundField DataField="BASTTELKOMSEL" HeaderText="BAST Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="BStelkomseluser" HeaderText="BAST User(Telkomsel)" />
                                    <asp:BoundField DataField="totald" HeaderText="WCTR BAST" />
                                    <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LbtRefresh" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
