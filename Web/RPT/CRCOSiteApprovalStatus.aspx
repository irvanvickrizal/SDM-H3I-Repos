<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRCOSiteApprovalStatus.aspx.vb"
    Inherits="RPT_CRCOSiteApprovalStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CR / CO Approval Status</title>
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
           font-weight:bolder;
           font-size:9pt;
           text-align:center;
           height:30px;
           color:black;
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
         function invalidExportToExcel() {
            alert('Data is empty!');
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
                        CR CO Approval Status
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
            <asp:GridView ID="GvCRCOApprovalReport" runat="server" AllowPaging="False" EmptyDataText="No Data Record Found"
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
                    <asp:BoundField DataField="DocumentName" HeaderText="Document Name" />
                    <asp:BoundField DataField="Siteno" HeaderText="Site No" />
                    <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                    <asp:BoundField DataField="Pono" HeaderText="PO Number" />
                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                    <asp:BoundField DataField="Package_Id" HeaderText="Package ID" />
                    <asp:BoundField DataField="AreaName" HeaderText="Area" />
                    <asp:BoundField DataField="RgnName" HeaderText="Region" />
                    <asp:BoundField DataField="TaskDesc" HeaderText="TaskDesc" />
                    <asp:BoundField DataField="UserTask" HeaderText="PIC" />
                    <asp:BoundField DataField="userType" HeaderText="Company Group" />
                    <asp:BoundField DataField="PendingTaskDateFrom" HeaderText="date of document submitted"
                        HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
