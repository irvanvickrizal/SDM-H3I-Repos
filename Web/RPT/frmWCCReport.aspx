<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCCReport.aspx.vb" Inherits="RPT_frmWCCReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Report</title>
    <style type="text/css">
         #panelHeader
        {
            background-color:black;
            padding:5px;
            font-family:verdana;
            font-size:15px;
            font-weight:bolder;
            color:#ffffff;
            border-style:solid;
            border-width:2px;
            border-color:#c3c3c3;
        }
        .btnExportExcel
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_0.gif);
        }
        .btnExportExcel:hover
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_1.gif);
        }
        .btnExportExcel:click
        {
            height:29px;
            width:120px;
            background-image: url(../Images/button/BtnExportExcel_2.gif);
        }
        .btnSearch
        {
            height:29px;
            width:80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }
        .btnSearch:hover
        {
            height:29px;
            width:80px;
            background-image: url(../Images/button/BtSearch_1.gif);
        }
        .btnSearch:click
        {
            height:29px;
            width:80px;
            background-image: url(../Images/button/BtSearch_2.gif);
        }
        .gridHeader_2
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
         .textLabel
        {
            font-family:verdana;
            font-size:11px;
        }
         .textBoldLabel
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
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
        function invalidExportToExcel() {
            alert('Data is empty!');
            return false;
        }
        function invalidDateSearch(){
            alert('Please define Start Date first!');
            return false;
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

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div id="panelHeader">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 80%;">
                        <span>WCC Report</span>
                    </td>
                    <td style="width: 19%; text-align: right;">
                        <asp:LinkButton ID="LbtExport" runat="server" Width="120px" Style="text-decoration: none;
                            cursor: pointer">
                            <div class="btnExportExcel"></div>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UP1">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <span class="textBoldLabel">Report Type</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlReportType" runat="server" AutoPostBack="true" CssClass="textLabel">
                                    <asp:ListItem Text="--Select Report Type--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="WCC Done" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="WCC Approval Status" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="WCC Preparation" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="WCC Rejection" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="WCC Historical Rejection" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <span class="textBoldLabel">Start Time</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtStartTime" runat="server" Width="180px"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceStartDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="TxtStartTime"
                                    TargetControlID="TxtStartTime">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <span class="textBoldLabel">End Time</span>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtEndTime" runat="server" Width="180px"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceEndDateTime" runat="server" Format="dd-MMMM-yyyy" PopupButtonID="TxtEndTime"
                                    TargetControlID="TxtEndTime">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                <asp:LinkButton ID="LbtSearch" runat="server" Width="80px" ValidationGroup="wpidsearch"
                                    Style="text-decoration: none; cursor: pointer;">
                        <div class="btnSearch"></div>  
                                </asp:LinkButton>
                                <asp:LinkButton ID="LbtReset" runat="server" Text="Reset" CssClass="textLabel"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div style="margin-top: 10px;">
                    <asp:GridView ID="GvWCCReportExport" runat="server" AutoGenerateColumns="false" CellPadding="2"
                        AllowPaging="false" EmptyDataText="No Record Found">
                        <RowStyle CssClass="gridOdd" />
                        <AlternatingRowStyle CssClass="gridEven" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                    .
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SubconName" HeaderText="Initiator" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="WCCStatus" HeaderText="WCC Status" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="IssuanceDate" HeaderText="Issuance Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                NullDisplayText="Not Yet" />
                            <asp:BoundField DataField="CertificateNo" HeaderText="CertificateNo." HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText"
                                NullDisplayText="Not Yet" ConvertEmptyStringToNull="true" />
                            <asp:BoundField DataField="PackageId" HeaderText="Package ID [EPM]" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="SiteNo" HeaderText="SiteNo [EPM]" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="SiteName" HeaderText="SiteName [EPM]" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="PONO" HeaderText="PO.Telkomsel" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="POSubcon" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                            <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="ActivityName" HeaderText="Type of Work" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                            <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                NullDisplayText="Not Yet" />
                            <asp:BoundField DataField="OnTaskPendingName" HeaderText="Waiting Approval" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="ApproverName" HeaderText="Approver Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="Not Yet" />
                            <asp:BoundField DataField="ApproverDate" HeaderText="Approve Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                NullDisplayText="Not Yet" />
                            <asp:BoundField DataField="RejectionName" HeaderText="Rejection Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="RejectionDate" HeaderText="Rejection Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                NullDisplayText="-" />
                            <asp:BoundField DataField="RejectionRemarks" HeaderText="Remarks" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="RejectionCategory" HeaderText="Category" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="ReuploadName" HeaderText="Re-Upload Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="ReuploadDate" HeaderText="Re-upload Date" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                NullDisplayText="-" />
                            <asp:BoundField DataField="RegionName" HeaderText="Region Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                            <asp:BoundField DataField="AreaName" HeaderText="Area Name" HeaderStyle-CssClass="gridHeader_2"
                                ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px"
                                ConvertEmptyStringToNull="true" NullDisplayText="-" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LbtSearch" />
                <asp:AsyncPostBackTrigger ControlID="LbtReset" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
