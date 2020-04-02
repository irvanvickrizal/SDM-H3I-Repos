<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRCORejection.aspx.vb" Inherits="CR_CRCORejection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Rejection</title>
    <style type="text/css">
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
        font-size: 7.5pt;
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
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
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
    </style>
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('There is no data can be exported!');
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
                        Document Rejection
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GvListCRCORejection" runat="server" AutoGenerateColumns="false"
                HeaderStyle-CssClass="HeaderGrid" Width="100%" CellPadding="3" CellSpacing="2"
                EmptyDataText="No Record CR Found">
                <RowStyle CssClass="oddGrid" />
                <AlternatingRowStyle CssClass="evenGrid" />
                <Columns>
                    <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="HeaderGrid" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Font-Names="Verdana" ItemStyle-Font-Size="7.5pt">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                    <asp:BoundField DataField="PONO" HeaderText="PO.No" />
                    <asp:BoundField DataField="EOName" HeaderText="PO Name" />
                    <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                    <asp:BoundField DataField="SiteName" HeaderText="SiteName" />
                    <asp:BoundField DataField="PackageId" HeaderText="WorkpackageID" />
                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                    <asp:BoundField DataField="RejectionRemarks" HeaderText="Remarks" />
                    <asp:BoundField DataField="rejectionUser" HeaderText="Rejected By" />
                    <asp:BoundField DataField="RejectionDate" HeaderText="Rejection Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgGenerateCRFormCR" runat="server" CommandArgument='<%#Eval("CRID") & "-" & Eval("PackageId") %>'
                                CommandName="generatecr" OnClientClick="return confirm('Are you sure you want to Re-upload this CR ?')"
                                ImageUrl="~/images/doce.gif" ToolTip="Generate CR Form" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgGenerateCRFormCO" runat="server" CommandArgument='<%#Eval("CRID") & "-" & Eval("PackageId") %>'
                                CommandName="generateco" OnClientClick="return confirm('Are you sure you want to Re-upload this CO ?')"
                                ImageUrl="~/images/doce.gif" ToolTip="Generate CR Form" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
