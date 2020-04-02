<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocQCApproved.aspx.vb"
    Inherits="DashBoard_frmDocQCApproved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doc View</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

    <script language="javascript" type="text/javascript">
            function WindowsClose(){
                window.location.href = '../frmdashboard.aspx?time=' + (new Date()).getTime();
            }
            
            function Reject(id){
                var tskid = getQueryVariable('id');
                window.open('Reject.aspx?tskid=' + tskid + '&id=' + id, 'welcome2', 'width=550,height=200,resizable=yes,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function Approve(id, docname, siteno, version, pono, swid){
                var tskid = getQueryVariable('id');
                var wpid = getQueryVariable('wpid');
                window.open('../digital-sign/QC-Digital-signature.aspx?tskid=' + tskid + '&id=' + id + '&docname=' + docname + '&pono=' + pono + '&swid=' + swid + '&wpid=' + wpid + '&time=' + (new Date()).getTime(), 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
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
        <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                    Approval Document
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" EmptyDataText="No documents to approve" Width="99%"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" No. ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="doc_id" HeaderText="doc" />
                            <asp:BoundField DataField="docpath" HeaderText="Path" />
                            <asp:BoundField DataField="DocName" HeaderText="Document" />
                            <asp:BoundField DataField="doc_id" HeaderText="Document" />
                            <asp:BoundField DataField="siteno" HeaderText="Site No" />
                            <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                            <asp:BoundField DataField="SW_Id" />
                            <asp:BoundField DataField="siteversion" HeaderText="version" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">
                                        Check Document</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
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
                <td class="pageTitle" colspan="4" id="Td1" style="background-color: #cccccc">
                    Review Document
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="grddocuments2" runat="server" AllowPaging="True" EmptyDataText="no documents to review"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
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
                                    <asp:Label ID="LblSWId" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="doc_id" HeaderText="doc" />
                            <asp:BoundField DataField="docpath" HeaderText="Path" />
                            <asp:BoundField DataField="DocName" HeaderText="Document" />
                            <asp:BoundField DataField="doc_id" HeaderText="Document" />
                            <asp:BoundField DataField="siteno" HeaderText="Site No" />
                            <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                            <asp:BoundField DataField="SW_Id" />
                            <asp:BoundField DataField="siteversion" HeaderText="version" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">
                                        Review Document</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
                                        ViewLog </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PnlDocumentHasBeenApproved" runat="server">
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="Td2" style="background-color: #cccccc">
                        Document has been approved
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="grddocuments3" runat="server" AllowPaging="True" EmptyDataText="No documents to  approve"
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
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
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document" />
                                <asp:BoundField DataField="doc_id" HeaderText="Document" />
                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                <asp:BoundField DataField="Pono" HeaderText="PoNo" />
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="siteversion" HeaderText="version" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
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
        </asp:Panel>
        <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" size="50px" value="Close" runat="server" class="buttonStyle"
                        style="width: 100pt" visible="false" />&nbsp;</td>
            </tr>
        </table>
        <input id="hdnSort1" type="hidden" runat="server" />
    </form>
</body>
</html>
