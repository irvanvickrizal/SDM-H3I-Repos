<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_WCCOnTaskPending.aspx.vb"
    Inherits="fancybox_Form_fb_WCCOnTaskPending" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC On TaskPending</title>
    <style type="text/css">
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
        #panelHeader
        {
            background-color:#cfcfcf;
            padding:5px;
            font-family:verdana;
            font-size:15px;
            font-weight:bolder;
            color:#ffffff;
            border-radius: 5px; -moz-border-radius: 5px; -webkit-border-radius: 5px;
        }
        a.ButtonExport, a.ButtonExport:link, a.ButtonExport:visited, a.dnnSecondaryAction, a.dnnSecondaryAction:link, a.dnnSecondaryAction:visited
        {
	        display: inline-block;
        }
        a.ButtonExport, a.ButtonExport:link, a.ButtonExport:visited, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only
        {
	         background: #6348CC;
	        background: -moz-linear-gradient(top, #6348CC 0%, #6348CC 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#0080C0), color-stop(100%,#0080C0));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=    '#0080C0' , endColorstr= '#0080C0' ,GradientType=0 );
	        -moz-border-radius: 3px;
	        border-radius: 3px;
	        text-shadow: 0px 1px 1px #000;
	        color: #fff;
	        text-decoration: none;
	        font-weight: bold;
	        border-color: #fff;
	        padding: 8px;
        }
        a[disabled].ButtonExport, a[disabled].ButtonExport:link, a[disabled].ButtonExport:visited, a[disabled].ButtonExport:hover, a[disabled].ButtonExport:visited:hover, dnnForm.ui-widget-content a[disabled].ButtonExport
        {
	        text-decoration: none;
	        color: #bbb;
	        background: #6348CC;
	        background: -moz-linear-gradient(top, #6348CC 0%, #6348CC 100%);
	        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#6348CC), color-stop(100%,#6348CC));
	        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr=   '#6348CC' , endColorstr= '#6348CC' ,GradientType=0 );
	        -ms-filter: "progid:DXImageTransform.Microsoft.gradient( startColorstr='#6348CC', endColorstr='#6348CC',GradientType=0 )";
	        cursor: default;
	        padding: 8px;
        }
        a.ButtonExport:hover, .ui-button.ui-widget.ui-state-default.ui-corner-all.ui-button-text-only:hover
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
        function invalidExportToExcel() {
            alert('Data is empty!');
            return false;
        }
        function invalidDateSearch(){
            alert('Please define Start Date first!');
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="panelHeader">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 80%;">
                        <span>WCC On Task Pending</span>
                    </td>
                    <td style="width: 19%; text-align: right;">
                        <asp:LinkButton ID="LbtExport" Text="Export to Excel" runat="server" CssClass="ButtonExport"
                            Font-Names="verdana" Font-Size="11px"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 5px;">
            <asp:GridView ID="GvWCCList" runat="server" AutoGenerateColumns="false" CellPadding="2"
                Width="100%">
                <RowStyle CssClass="gridOdd" />
                <AlternatingRowStyle CssClass="gridEven" />
                <Columns>
                    <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SubconName" HeaderText="Subcon" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="PackageId" HeaderText="Package ID [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName [EPM]" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="PONO" HeaderText="PO.Telkomsel" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="POSubcontractor" HeaderText="PO Subcontractor" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-BorderColor="GrayText" />
                    <asp:BoundField DataField="ScopeName" HeaderText="Scope" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="UserLocation" HeaderText="Pending Location" HeaderStyle-CssClass="gridHeader_2"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
