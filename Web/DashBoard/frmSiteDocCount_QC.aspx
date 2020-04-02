<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocCount_QC.aspx.vb"
    Inherits="DashBoard_frmSiteDocCount_QC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <title>QC Document</title>
    <style type="text/css">
        .PnlHeader
        {
            Padding:3px;
            background-color:#7f7f7f;
            Color:#ffffff;
            font-family:Verdana;
            font-size:12px;
            font-weight:bolder;
        }
        
        .PnlHeader2
        {
            Padding:7px;
            background-color:#7f7f7f;
            Color:#ffffff;
            font-family:Verdana;
            font-size:12px;
            font-weight:bolder;
        }
        
        .BtnATPOnline
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bolder;
            border-style:solid;
            border-width:1px;
            border-color:#ffffff;
            background-color:#cccccc;
            color:#3F48CC;
            height:20px;
            cursor:hand;
        }
        .BtnATPViewAll
        {
            font-family:verdana;
            font-size:10px;
            font-weight:bolder;
            border-style:solid;
            border-width:1px;
            border-color:black;
            background-color:#cccccc;
            color:#3F48CC;
            height:20px;
            cursor:hand;
            width:60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 99%;">
            <div id="ATPHeader" class="PnlHeader">
                <table>
                    <tr>
                        <td style="width: 90%">
                            QC Document Pending
                        </td>
                        <td style="width: 9%; text-align: right;">
                            <div style="text-align: right; width: 100%;">
                                <asp:Button ID="BtnGoToDashboard" runat="server" Text="Go To Dashboard" CssClass="BtnATPOnline" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="QCDocument" style="margin-top: 10px; margin-left: 1px;">
                <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="False" EmptyDataText="No QC Document Pending"
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
                        <asp:BoundField DataField="Site_No" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Site_Name" HeaderText="Site Name" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                            HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                        <asp:HyperLinkField DataTextField="CountUsrType" DataNavigateUrlFields="Site_No,tsk_id,workpkgid"
                            DataNavigateUrlFormatString="frmDocQCApproved.aspx?Id={0}&amp;TId={1}&amp;wpid={2}&amp;doctype=qcol"
                            HeaderText="Document Count" />
                    </Columns>
                </asp:GridView>
            </div>
            <div style="text-align: right; margin-top: 10px;">
                <asp:Button ID="BtnViewAll" runat="server" Text="View All" CssClass="BtnATPViewAll" />
            </div>
        </div>
    </form>
</body>
</html>
