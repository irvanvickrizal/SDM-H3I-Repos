<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BastUnderSignature.aspx.vb"
    Inherits="Dashboard_New_BastUnderSignature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BAST UNDER SIGNATURE</title>
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
                            <asp:Label ID="LblHeaderBastUnderSignature" runat="server"></asp:Label>
                        </td>
                        <td style="width: 35%; text-align: right;">
                            <asp:Button ID="BtnRefresh" runat="server" Text="Refresh" CssClass="BtnExpt" BackColor="#7f7f7f" />
                            <asp:Button ID="BtnExport" runat="server" Text="Export To Excel" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                            <asp:Button ID="BtnDashboard" runat="server" Text="Go to Dashboard" CssClass="BtnExpt"
                                BackColor="#7f7f7f" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow:scroll;">
                <div>
                    <asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False" Width="100%"
                        AllowPaging="True">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID" />
                            <asp:BoundField DataField="TSELPROJECTID" HeaderText="TSEL ID" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="ACT_9350" HeaderText="Work Completed" />
                            <asp:BoundField DataField="BAUTRefNo" HeaderText="BAUT Ref. No." />
                            <asp:BoundField DataField="BAUTNSN" HeaderText="BAUT Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BUnsnuser" HeaderText="BAUT User(NSN)" />
                            <asp:BoundField DataField="BAUTTELKOMSEL" HeaderText="BAUT Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BUtelkomseluser" HeaderText="BAUT User(Telkomsel)" />
                            <asp:BoundField DataField="BASTNSN" HeaderText="BAST Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BSnsnuser" HeaderText="BAST User(NSN)" />
                            <asp:BoundField DataField="BASTTELKOMSEL" HeaderText="BAST Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BStelkomseluser" HeaderText="BAST User(Telkomsel)" />
                            <asp:BoundField DataField="totald" HeaderText="WCTR BAST" />
                            <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div style="display: none;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Region" HeaderText="Region" />
                            <asp:BoundField DataField="SiteNo" HeaderText="Site No" />
                            <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                            <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID" />
                            <asp:BoundField DataField="TSELPROJECTID" HeaderText="TSEL ID" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="POName" HeaderText="Po Name" />
                            <asp:BoundField DataField="PONo" HeaderText="Customer Po" />
                            <asp:BoundField DataField="ACT_9350" HeaderText="Work Completed" />
                            <asp:BoundField DataField="BAUTRefNo" HeaderText="BAUT Ref. No." />
                            <asp:BoundField DataField="BAUTNSN" HeaderText="BAUT Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BUnsnuser" HeaderText="BAUT User(NSN)" />
                            <asp:BoundField DataField="BAUTTELKOMSEL" HeaderText="BAUT Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BUtelkomseluser" HeaderText="BAUT User(Telkomsel)" />
                            <asp:BoundField DataField="BASTNSN" HeaderText="BAST Date(NSN)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BSnsnuser" HeaderText="BAST User(NSN)" />
                            <asp:BoundField DataField="BASTTELKOMSEL" HeaderText="BAST Date(Telkomsel)" DataFormatString="{0:dd-MMM-yyyy}"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="BStelkomseluser" HeaderText="BAST User(Telkomsel)" />
                            <asp:BoundField DataField="totald" HeaderText="WCTR BAST" />
                            <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
