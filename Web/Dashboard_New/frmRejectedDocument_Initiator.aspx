﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRejectedDocument_Initiator.aspx.vb" Inherits="Dashboard_New_frmRejectedDocument_Initiator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rejected Documents</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            color: black;
            font-family: Trebuchet MS;
            font-size: 14px;
            font-weight: bold;
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
        <div class="headerpanel">
            <table style="width: 100%">
                <tr>
                    <td style="width: 60%">Rejected Documents
                    </td>
                    <td style="width: 35%; text-align: right;">
                        <asp:Button ID="BtnExport" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                            BackColor="#cfcfcf" ForeColor="Black" Font-Names="Trebuchet MS;" Font-Size="12px" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 10px;">
            <asp:GridView ID="GvRejectedDocuments" runat="server" AutoGenerateColumns="False" Width="100%"
                AllowPaging="true" PageSize="15">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" Height="20px" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                    HorizontalAlign="Right" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total " ItemStyle-HorizontalAlign="right" ItemStyle-Width="2%">
                        <ItemTemplate>
                            <%# container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Docname" HeaderText="Document" />
                    <asp:BoundField DataField="PoNo" HeaderText="PoNo" />
                    <asp:BoundField DataField="SiteNo" HeaderText="Site ID" />
                    <asp:BoundField DataField="Sitename" HeaderText="Sitename" />
                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                    <asp:BoundField DataField="Workpackageid" HeaderText="WorkPackageID" SortExpression="WorkPkgId" />
                    <asp:BoundField DataField="tselprojectid" HeaderText="BTS Type" />
                    <asp:BoundField DataField="RejectedUser" HeaderText="Rejected User" />
                    <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                    <asp:BoundField DataField="RejectedDate" HeaderText="Rejected Date" HtmlEncode="false"
                        DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                                <asp:LinkButton ID="lbtEditDoc" CssClass="textLabel" runat="server" Text="Upload Doc" CommandName="EditDoc" CommandArgument='<%#Eval("PoNo") %>'></asp:LinkButton>
                                <asp:Label ID="Lblsite" runat="server" Text='<%#Eval("siteno") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("version") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblpoid" runat="server" Text='<%#Eval("po_id") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
