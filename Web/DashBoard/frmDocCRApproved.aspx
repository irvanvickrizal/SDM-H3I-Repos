<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocCRApproved.aspx.vb"
    Inherits="DashBoard_frmDocCRApproved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CR Document View</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
            function WindowsClose(){
                window.location.href = '../frmdashboard.aspx?time=' + (new Date()).getTime();
            }
            
            function Approve(id,wpid,type){
                window.open('../digital-sign/CR-Digital-signature.aspx?id=' + id + '&wpid=' + wpid + '&type=' + type + '&time=' + (new Date()).getTime(), 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            function ApproveCO(id,wpid,type){
                window.open('../digital-sign/CO-Digital-signature.aspx?id=' + id + '&wpid=' + wpid + '&type=' + type + '&time=' + (new Date()).getTime(), 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function getQueryVariable(variable){
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    if (pair[0] == variable) {
                        return pair[1];
                    }
                }
            }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Button ID="BtnViewCRPending" runat="server" Text="CR Document Pending" Style="border-style: solid;
                    border-color: White; height: 25px; padding: 3px;" BackColor="Gray" CssClass="lblText" />
                <asp:Button ID="BtnViewCOPending" runat="server" Text="CO Document Pending" Style="border-style: solid;
                    border-color: White; height: 25px; padding: 3px;" BackColor="Gray" CssClass="lblText" />
            </div>
            <div style="border-width: 1px; border-color: Black; border-style: solid;">
                <asp:MultiView ID="MvCorePanel" runat="server">
                    <asp:View ID="VwCRDocPending" runat="server">
                        <div style="background-color: Gray; padding: 5px;">
                            <span style="font-family: Arial Unicode MS; font-size: 10.5pt; font-weight: bolder;
                                color: White">CR Task Pending </span>
                        </div>
                        <div>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td colspan="4" id="rowadd" style="background-color: #c2c2c2;">
                                        <div style="margin-left:10px;">
                                            <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;">
                                                Approval Document</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="grdapproverdocuments" runat="server" AllowPaging="True" EmptyDataText="No Pending documents to be approved"
                                            Width="99%" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <EmptyDataRowStyle ForeColor="green" BorderWidth="0px" BorderStyle="none" BorderColor="transparent" Width="99%" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No. ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:Label>
                                                        <asp:Label ID="LblCRID" Visible="false" runat="server" Text='<%#Eval("CRID") %>'></asp:Label>
                                                        <asp:Label ID="LblDocPath" Visible="false" runat="server" Text='<%#Eval("DocPath") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="CR No." />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"SNOWFCR") %>','<%# DataBinder.Eval(Container.DataItem,"PackageId") %>','app');">
                                                            Check Document</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../PO/frmViewCRDocument.aspx?crid=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&subdocid=0','','Width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');">
                                                            View Document </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCRViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=700,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <input id="hdnSort" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td colspan="4" id="Td1" style="background-color: #c2c2c2">
                                        <div style="margin-left:10px;">
                                            <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;">
                                                Review Document</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="grdreviewerdocument" runat="server" AllowPaging="True" EmptyDataText="No Pending documents to be reviewed"
                                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR" Width="99%">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <EmptyDataRowStyle ForeColor="green" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No. ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:Label>
                                                        <asp:Label ID="LblCRID" Visible="false" runat="server" Text='<%#Eval("CRID") %>'></asp:Label>
                                                        <asp:Label ID="LblDocPath" Visible="false" runat="server" Text='<%#Eval("DocPath") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="CR No." />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"SNOWFCR") %>','<%# DataBinder.Eval(Container.DataItem,"PackageId") %>','rev');">
                                                            Check Document</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../PO/frmViewCRDocument.aspx?crid=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&subdocid=0','','Width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');">
                                                            View Document </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCRViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td colspan="4" id="Td2" style="background-color: #c2c2c2">
                                        <div style="margin-left:10px;">
                                            <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;">
                                                Document has been approved/reviewed</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="grddocumentapproved" runat="server" AllowPaging="True" EmptyDataText="No Documents already approved/Reviewed"
                                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <EmptyDataRowStyle ForeColor="green" />
                                            <Columns>
                                                <asp:TemplateField HeaderText=" Total ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="CR No." />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../PO/frmViewCRDocument.aspx?crid=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&subdocid=0','','Width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');">
                                                            View Document </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCRViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <input id="Hidden1" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="VwCODocPending" runat="server">
                        <div style="background-color: Gray; padding: 5px;">
                            <span style="font-family: Arial Unicode MS; font-size: 10.5pt; font-weight: bolder;
                                color: White">CO Task Pending </span>
                        </div>
                        <div>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td colspan="4" id="Td3" style="background-color: #c2c2c2">
                                        <div style="margin-left:10px;">
                                            <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;">
                                                Approval Document</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GvCoApproval" runat="server" AllowPaging="True" EmptyDataText="No Pending Documents to be Approved"
                                            Width="100%" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <EmptyDataRowStyle ForeColor="green" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No. ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:Label>
                                                        <asp:Label ID="LblCRID" Visible="false" runat="server" Text='<%#Eval("CRID") %>'></asp:Label>
                                                        <asp:Label ID="LblDocPath" Visible="false" runat="server" Text='<%#Eval("DocPath") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="docname" />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:BoundField DataField="lmdt" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    HtmlEncode="false" ItemStyle-CssClass="itemGridPadding" />
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="ApproveCO('<%# DataBinder.Eval(Container.DataItem,"SNOWFCR") %>','<%# DataBinder.Eval(Container.DataItem,"PackageId") %>','app');">
                                                            Check Document</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCOViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&docid=<%# DataBinder.Eval(Container.DataItem,"docid") %>','','Width=700,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <input id="Hidden2" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td colspan="4" id="Td4" style="background-color: #c2c2c2">
                                        <div style="margin-left:10px;">
                                            <span style="font-family: Arial Unicode MS; font-size: 9.5pt; font-weight: bolder;">
                                                Review Document</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GvCOReview" runat="server" AllowPaging="True" EmptyDataText="No Pending Documents to be Reviewed"
                                            Width="100%" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <EmptyDataRowStyle ForeColor="green" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No. ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server" Text='<%# Container.DataItemIndex + 1 %>'>
                                                        </asp:Label>
                                                        <asp:Label ID="LblCRID" Visible="false" runat="server" Text='<%#Eval("CRID") %>'></asp:Label>
                                                        <asp:Label ID="LblDocPath" Visible="false" runat="server" Text='<%#Eval("DocPath") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="docname" />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:BoundField DataField="lmdt" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    HtmlEncode="false" ItemStyle-CssClass="itemGridPadding" />
                                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="ApproveCO('<%# DataBinder.Eval(Container.DataItem,"SNOWFCR") %>','<%# DataBinder.Eval(Container.DataItem,"PackageId") %>','rev');">
                                                            Check Document</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCOViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&docid=<%# DataBinder.Eval(Container.DataItem,"docid") %>','','Width=500,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td class="pageTitle" colspan="4" id="Td5" style="background-color: #cccccc">
                                        Document has been approved
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="GvCODocApproved" runat="server" AllowPaging="True" EmptyDataText="No documents Approved"
                                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNOWFCR">
                                            <PagerSettings Position="TopAndBottom" />
                                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                            <RowStyle CssClass="GridOddRows" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                            <Columns>
                                                <asp:TemplateField HeaderText=" Total ">
                                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblno" runat="Server">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CRNo" HeaderText="docname" />
                                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                                <asp:BoundField DataField="PackageId" HeaderText="Work Package ID" />
                                                <asp:BoundField DataField="lmdt" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    HtmlEncode="false" ItemStyle-CssClass="itemGridPadding" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="window.open('../po/frmCOViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"CRID") %>&wpid=<%# DataBinder.Eval(Container.DataItem,"PackageId") %>&docid=<%# DataBinder.Eval(Container.DataItem,"docid") %>','','Width=700,height=500');">
                                                            ViewLog </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <input id="Hidden3" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </form>
</body>
</html>
