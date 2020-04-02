<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRPTSiteApprovalStatus_SiteBases.aspx.vb" Inherits="RPT_frmRPTSiteApprovalStatus_SiteBases" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SITE APPROVAL STATUS [SITE BASE]</title>
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
           font-size:12px;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:12px;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:12px;
           
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width:70px;">
                    <span style="font-family:Verdana; font-size:11px;">Select User</span>
                </td>
                <td>
                    <asp:DropDownList ID="DdlApproverReviewer" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:DropDownList>        
                </td>
                <td>
                    <asp:Button ID="BtnLoadData" runat="server" Text="Submit" CssClass="btnSubmit" />        
                </td>
            </tr>
        </table>
    </div>
    <div class="HeaderReport">
       <table width="100%">
            <tr>
                <td style="width:80%">
                        Site Approval Status [Site Base]
                </td>
                <td style="width:15%; text-align:right;">
                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export to Excel" CssClass="BtnExpt" BackColor="#7f7f7f" />
                </td>
            </tr>
       </table>
    </div>
    <div>
        <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="False" EmptyDataText="All documents approved" Width="100%" Font-Names="Verdana" Font-Size="11px"
                            AllowSorting="True" AutoGenerateColumns="False">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Site_No" HeaderText="Site No" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" />
                                <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                            </Columns>
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
