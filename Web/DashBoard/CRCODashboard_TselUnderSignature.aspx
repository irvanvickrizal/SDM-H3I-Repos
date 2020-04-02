<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRCODashboard_TselUnderSignature.aspx.vb"
    Inherits="DashBoard_CRCODashboard_TselUnderSignature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TASK PENDING YOUR STAFF</title>
    <style type="text/css">
        .HeaderGrid
        {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: White;
            background-color: #ffc90E;
            border-color:white;
            vertical-align:middle;
        }
        .oddGrid
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
        }
        .evenGrid
        {
            font-family: Arial Unicode MS;
            font-size: 9pt;
            background-color:#cfcfcf;
        }
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
        .lblSubHeader
        {
            font-family:Arial Unicode MS;
            font-size:8pt;
            font-weight:bolder;
        }
        .lblTitle
        {
            font-family:Arial Unicode MS;
            font-size:10pt;
            font-weight:bolder;
        }
         .lblBText
        {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            color: #000000;
            text-align: left;
            font-weight: bold;
        }
        .evengrid2
        {
            background-color: #cfcfcf;
        }
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:Arial Unicode MS;
            font-size:13px;
            font-weight:bold;
            margin-top:15px;
            margin-bottom:10px;
            padding:3px;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:Arial Unicode MS;
           font-weight:bold;
           font-size:10pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .BtnExpt
        {
           border-style:solid;
           border-color:white;
           border-width:1px;
           font-family:Arial Unicode MS;
           font-size:11px;
           font-weight:bold;
           color:white;
           width:120px;
           height:25px;
           cursor:hand;
        }
    </style>
     <script type="text/javascript">
         function invalidExportToExcel() {
            alert('Data is empty, please try again!');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="HeaderReport">
            <table width="100%">
                <tr>
                    <td style="width: 80%">
                        <asp:Label ID="LblUserUnderSignature" runat="server"></asp:Label>
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportUserUnderSignature" runat="server" Text="Export To Excel"
                            CssClass="BtnExpt" BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="False" EmptyDataText="No CR Document Pending"
                AllowSorting="True" AutoGenerateColumns="False">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                    Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total " HeaderStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CRNo" HeaderText="Doc Name" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="POName" HeaderText="PO Name" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SiteName" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                        ItemStyle-Width="160px" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="UserLocation" HeaderText="Pending Task" HeaderStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
