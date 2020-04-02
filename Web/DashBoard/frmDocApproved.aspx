<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocApproved.aspx.vb"
    Inherits="frmDocApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doc View</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" />    
    <link href="../css/Pagination.css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

    <script language="javascript" type="text/javascript">
        function WindowsClose() {
            window.location.href = '../frmdashboard.aspx?time=' + (new Date()).getTime();
        }

        function Reject(id) {
            var tskid = getQueryVariable('id');
            window.open('Reject.aspx?tskid=' + tskid + '&id=' + id, 'welcome2', 'width=550,height=200,resizable=yes,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
        }

        function Approve(tskid,id,wfid,siteno, version, pono, swid) {
            var wpid = getQueryVariable('wpid');
            window.open('../digital-sign/new-digital-signature.aspx?tskid=' + tskid + '&id=' + id + '&wfid=' + wfid + '&version=' + version + '&siteno=' + siteno + '&pono=' + pono + '&swid=' + swid + '&wpid=' + wpid + '&time=' + (new Date()).getTime(), 'welcome3', 'width=850,height=600,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
        }

        function getQueryVariable(variable) {
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
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left: 10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <%--Modified by Fauzan, 9 Nov 2018. ReDesign Approved file--%>
    <div class="row">
        <form id="form1" runat="server" class="form-horizontal">
            <div class="col-xs-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                      <h3 class="box-title">Approval Document</h3>
                      <div class="box-tools pull-right">
	                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
	                    </button>
                      </div>
                    </div>
                    <div class="box-body">
                        <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" EmptyDataText="No documents to  approve"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" DataKeyNames="sno" Width="99%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
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
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem, "sno") %>','<%# DataBinder.Eval(Container.DataItem, "DocName") %>','<%# DataBinder.Eval(Container.DataItem, "siteno") %>','<%# DataBinder.Eval(Container.DataItem, "siteversion") %>','<%# DataBinder.Eval(Container.DataItem, "pono") %>',<%# DataBinder.Eval(Container.DataItem, "sw_id") %>);">Approve</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <ItemTemplate>
                                        <a href="#" onclick="Reject('<%# DataBinder.Eval(Container.DataItem, "sno") %>');">Reject</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="siteversion" HeaderText="version" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem, "tsk_id") %>','<%# DataBinder.Eval(Container.DataItem, "sno") %>','<%# DataBinder.Eval(Container.DataItem, "wf_id") %>','<%# DataBinder.Eval(Container.DataItem, "siteno") %>','<%# DataBinder.Eval(Container.DataItem, "siteversion") %>','<%# DataBinder.Eval(Container.DataItem, "pono") %>',<%# DataBinder.Eval(Container.DataItem, "sw_id") %>);">Check Document</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem, "doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem, "siteid") %>-<%# DataBinder.Eval(Container.DataItem, "Scope") %>-<%# DataBinder.Eval(Container.DataItem, "workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem, "PONo") %>','','Width=500,height=500');">ViewLog </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>          
            </div>
            <div class="col-xs-12"> 
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Review Document</h3>
                        <div class="box-tools pull-right">
	                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
	                    </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <asp:GridView ID="grddocuments2" runat="server" AllowPaging="True" EmptyDataText="no documents to review"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" DataKeyNames="sno" Width="99%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. ">
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
                                <asp:TemplateField HeaderText="Approve">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem, "sno") %>','<%# DataBinder.Eval(Container.DataItem, "DocName") %>','<%# DataBinder.Eval(Container.DataItem, "siteno") %>','<%# DataBinder.Eval(Container.DataItem, "siteversion") %>','<%# DataBinder.Eval(Container.DataItem, "pono") %>',<%# DataBinder.Eval(Container.DataItem, "sw_id") %>);">Review</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <ItemTemplate>
                                        <a href="#" onclick="Reject('<%# DataBinder.Eval(Container.DataItem, "sno") %>');">Reject</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="siteversion" HeaderText="version" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem, "tsk_id") %>','<%# DataBinder.Eval(Container.DataItem, "sno") %>','<%# DataBinder.Eval(Container.DataItem, "wf_id") %>','<%# DataBinder.Eval(Container.DataItem, "siteno") %>','<%# DataBinder.Eval(Container.DataItem, "siteversion") %>','<%# DataBinder.Eval(Container.DataItem, "pono") %>',<%# DataBinder.Eval(Container.DataItem, "sw_id") %>);">Check Document</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem, "doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem, "siteid") %>-<%# DataBinder.Eval(Container.DataItem, "Scope") %>-<%# DataBinder.Eval(Container.DataItem, "workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem, "PONo") %>','','Width=800,height=500');">ViewLog </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>    
            </div>
            <div class="col-xs-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Document has been approved</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <asp:GridView ID="grddocuments3" runat="server" AllowPaging="True" EmptyDataText="No documents to  approve"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" DataKeyNames="sno">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
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
                                        <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem, "doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem, "siteid") %>-<%# DataBinder.Eval(Container.DataItem, "Scope") %>-<%# DataBinder.Eval(Container.DataItem, "workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem, "PONo") %>','','Width=500,height=500');">ViewLog </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>             
            </div>
            <div class="col-xs-12" style="display:none">
                <div class="box-body">
                    <asp:Label ID="lblWarningDelegationUser" runat="server" Font-Bold="true" Font-Names="verdana" Text="Your Task Pending already delegated to your supervisor"
                        Font-Size="11px" ForeColor="red" Visible="false"></asp:Label>
                </div>
                <div class="box-footer">
                    <input id="BtnClose" type="submit" size="50" value="Close" runat="server" class="btn btn-block btn-default pull-left"
                        style="width: 100pt" visible="false" />
                </div>
            </div>
            <input id="hdnSort" type="hidden" runat="server" />
        </form>
    </div>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div style="width: 99%; height: 450px;">
            <div class="headerpanel">
                Approval Document
            </div>
            <div style="margin-top: 5px;">
                <table cellpadding="1" border="0" cellspacing="1" width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" EmptyDataText="No documents to  approve"
                                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno" Width="99%">
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
                                    <asp:TemplateField HeaderText="Approve">
                                        <ItemTemplate>
                                            <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">Approve</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject">
                                        <ItemTemplate>
                                            <a href="#" onclick="Reject('<%# DataBinder.Eval(Container.DataItem,"sno") %>');">Reject</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SW_Id" />
                                    <asp:BoundField DataField="siteversion" HeaderText="version" />
                                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                    <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                    <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"tsk_id") %>','<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"wf_id") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">Check Document</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">ViewLog </a>
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
            </div>
            <div class="headerpanel" style="margin-top: 10px;">
                Review Document
            </div>
            <div style="margin-top: 5px;">
                <table cellpadding="1" border="0" cellspacing="1" width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grddocuments2" runat="server" AllowPaging="True" EmptyDataText="no documents to review"
                                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno" Width="99%">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" No. ">
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
                                    <asp:TemplateField HeaderText="Approve">
                                        <ItemTemplate>
                                            <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">Review</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reject">
                                        <ItemTemplate>
                                            <a href="#" onclick="Reject('<%# DataBinder.Eval(Container.DataItem,"sno") %>');">Reject</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SW_Id" />
                                    <asp:BoundField DataField="siteversion" HeaderText="version" />
                                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                    <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                    <asp:TemplateField ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                           <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"tsk_id") %>','<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"wf_id") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>);">Check Document</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=800,height=500');">ViewLog </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="headerpanel" style="margin-top: 10px;">
                Document has been approved
            </div>
            <div style="margin-top: 10px;">
                <table cellpadding="1" border="0" cellspacing="1" width="100%">
                    <tr>
                        <td>
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
                                            <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>-<%# DataBinder.Eval(Container.DataItem,"workpkgid") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">ViewLog </a>
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
        </div>
        Modified by Fauzan, 7 Nov 2018. Remove 3 Logo
        <div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>

        <div id="pnlWarningDelegationUserApprover" runat="server" style="margin-bottom: 10px;">
            <asp:Label ID="lblWarningDelegationUser" runat="server" Font-Bold="true" Font-Names="verdana" Text="Your Task Pending already delegated to your supervisor"
                Font-Size="11px" ForeColor="red"></asp:Label>
        </div>
        <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" size="50" value="Close" runat="server" class="buttonStyle"
                        style="width: 100pt" visible="false" />&nbsp;</td>
            </tr>
        </table>
        <input id="hdnSort1" type="hidden" runat="server" />
    </form>--%>
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
</body>

</html>
