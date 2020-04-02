<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RejectedDocument.aspx.vb"
    Inherits="Dashboard_New_RejectedDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rejected Documents</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
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
        .GridEvenRowsNew
        {
            font-family:verdana;
            font-size:9pt;
            background-color:#cfcfcf;
            
        }
        .GridOddRowsNew
        {
            font-family:verdana;
            font-size:9pt;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="HeaderReport">
                <table width="100%">
                    <tr>
                        <td style="width: 60%">
                            Rejected Documents
                        </td>
                        <td style="width: 35%; text-align: right;">
                            <asp:Button ID="BtnExport" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                            <asp:Button ID="BtnDashboard" runat="server" Text="Go to Dashboard" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="GvRejectedDocuments" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="GridHeader">
                    <RowStyle CssClass="GridEvenRowsNew" />
                    <AlternatingRowStyle CssClass="GridOddRowsNew" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="docname" HeaderText="Document Name" />
                        <asp:BoundField DataField="site_no" HeaderText="Site No" />
                        <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                        <asp:BoundField DataField="workpkgid" HeaderText="Workpackageid" />
                        <asp:BoundField DataField="pono" HeaderText="PoNo" />
                        <asp:BoundField DataField="rejectdate" HeaderText="Rejected Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
