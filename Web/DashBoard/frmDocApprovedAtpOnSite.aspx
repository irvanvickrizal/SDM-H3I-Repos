<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocApprovedAtpOnSite.aspx.vb" Inherits="DashBoard_frmDocApprovedAtpOnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Document View</title>
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
                window.open('../digital-sign/Digital-signature.aspx?tskid=' + tskid + '&id=' + id + '&docname=' + docname + '&version=' + version + '&siteno=' + siteno + '&pono=' + pono + '&swid=' + swid + '&wpid=' + wpid + '&time=' + (new Date()).getTime(), 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
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
    <style type="text/css">
        .BtnATPOnline
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            border-style:solid;
            border-width:1px;
            border-color:#ffffff;
            background-color:#cccccc; 
            color:#3F48CC;
            height:25px;
            cursor:hand;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:99%">
        <div style="font-family:Verdana; font-weight:bolder; color:White; background-color:#7f7f7f; padding:3px">
            <table>
                <tr>
                    <td style="width:90%">
                        DOCUMENT REVIEW        
                    </td>
                    <td style="width:9%; text-align:right;">
                        <div style="width:100%; text-align:right;">
                            <asp:Button ID="BtnBackToATPPending" Text="Go to ATP Task Pending" runat="server" CssClass="BtnATPOnline" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top:10px; margin-left:2px; width:100%;">
            <input id="hdnSort" type="hidden" runat="server" />
            <asp:GridView ID="gvDocumentATPReviewed" runat="server" AllowPaging="True" EmptyDataText="no documents to review" Width="100%"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="No." HeaderStyle-HorizontalAlign="Center" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="doc_id" HeaderText="doc" Visible="false" />
                            <asp:BoundField DataField="docpath" HeaderText="Path" Visible="false" />
                            <asp:BoundField DataField="DocName" HeaderText="Document" HeaderStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="doc_id" HeaderText="Document" Visible="false" />
                            <asp:BoundField DataField="siteno" HeaderText="Site No" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Pono" HeaderText="PoNo" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SW_Id" Visible="false" />
                            <asp:BoundField DataField="siteversion" HeaderText="version" Visible="false" />
                            <asp:BoundField DataField="Scope" HeaderText="Scope" Visible="false" />
                            <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" Visible="false" />
                            <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="LblSW_Id" runat="server" Text='<%#Eval("SW_Id") %>' Visible="false"></asp:Label>
                                    <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">
                                        Review Document</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('../po/frmViewDocument.aspx?id=<%# DataBinder.Eval(Container.DataItem,"SW_id") %>','','Width=500,height=500');">
                                        View Document </a>
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
        </div>
    </div>
    </form>
</body>
</html>
