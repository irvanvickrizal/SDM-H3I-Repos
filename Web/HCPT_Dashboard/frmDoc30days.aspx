<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDoc30days.aspx.vb" Inherits="HCPT_Dashboard_frmDoc30days" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Signed Last 30 days</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left: 10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }

        .BtnExpt {
            border-style: solid;
            border-color: white;
            border-width: 1px;
            font-family: verdana;
            font-size: 11px;
            font-weight: bold;
            color: black;
            width: 120px;
            height: 25px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 450px;">
            <div class="headerpanel">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 80%">Doc Signed Last 30days
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <input id="btnExport" runat="server" type="button" value="Export to Excel" class="BtnExpt" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 5px; width: 100%;">
                <asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False" Width="99%"
                    AllowPaging="true" EmptyDataText="No Record Found" PageSize="30">
                    <PagerSettings Position="TopAndBottom" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText=" No. ">
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                            <ItemTemplate>
                                <asp:Label ID="lblno" runat="Server"></asp:Label>
                                <asp:Label ID="LblSWID" runat="server" Visible="false" Text='<%#Eval("SW_Id")%>'></asp:Label>
                                <asp:Label ID="LblDocId" runat="server" Visible="false" Text='<%#Eval("Doc_Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocName" HeaderText="Docname" />
                        <asp:BoundField DataField="pono" HeaderText="PO Number" />
                        <asp:BoundField DataField="siteno" HeaderText="Site No" />
                        <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                        <asp:BoundField DataField="Scope" HeaderText="Scope" />
                        <asp:BoundField DataField="workpackageid" HeaderText="Work Package Id" />
                        <asp:BoundField DataField="rgnname" HeaderText="Region" />
                        <asp:BoundField DataField="enddatetime" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy}"
                            ConvertEmptyStringToNull="true" HtmlEncode="false" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
