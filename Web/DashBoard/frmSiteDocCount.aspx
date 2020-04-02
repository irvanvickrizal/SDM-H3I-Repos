<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocCount.aspx.vb"
    Inherits="DashBoard_frmSiteDocCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sites Document Count</title>
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
    </style>
    
    <script type="text/javascript" src="../js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="~/dist/css/skins/_all-skins.min.css" />
    <link rel="stylesheet" href="~/plugins/iCheck/flat/blue.css" />
    <link rel="stylesheet" href="~/plugins/morris/morris.css" />
    <link rel="stylesheet" href="~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    <link rel="stylesheet" href="~/dist/css/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link href="../css/Pagination.css" rel="stylesheet" />
</head>
<body>
    <%--Modified by Fauzan, 9 Nov 2018. ReDesign SSV submit file--%>
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">Site Document Pending</h3>
                </div>
                <form id="form1" runat="server" class="form-horizontal">
                    <div class="box-body">
                        <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="true" EmptyDataText="All documents approved"
                            AllowSorting="True" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-condensed">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No. ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" />
                                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                <asp:BoundField DataField="wpid" HeaderText="Work Package ID" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HtmlEncode="false" />
                                <asp:HyperLinkField DataTextField="CountUsrType" DataNavigateUrlFields="SiteNo,tsk_id,wpid"
                                    DataNavigateUrlFormatString="frmDocApproved.aspx?Id={0}&amp;TId={1}&amp;wpid={2}&amp;doctype=common"
                                    HeaderText="Document Count" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="box-footer">
                        <input id="hdnSort" type="hidden" runat="server" />
                        <input id="btnViewAll" type="submit" size="50" value="View All" runat="server"
                            class="buttonStyle" style="width: 100pt" visible="false" />
                        <input id="BtnClose" type="submit" size="50" value="Go to Dashboard" runat="server"
                            class="btn btn-primary pull-right" style="width: 100pt" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    
    <%--OLD--%>
    <%--<form id="form2" runat="server">
        <div style="width: 99%; height: 450px;">
            <div class="headerpanel">
                Site Document Pending
            </div>
            <div style="margin-top: 5px;">
                <div>
                    <table cellpadding="1" border="0" cellspacing="1" width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="false" EmptyDataText="All documents approved"
                                    AllowSorting="True" AutoGenerateColumns="False" Width="100%">
                                    <PagerSettings Position="TopAndBottom" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" No. ">
                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                            <ItemTemplate>
                                                <%# container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PoNo" HeaderText="PO Number" />
                                        <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                        <asp:BoundField DataField="SiteName" HeaderText="Site Name" />
                                        <asp:BoundField DataField="wpid" HeaderText="Work Package ID" />
                                        <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                        <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HtmlEncode="false" />
                                        <asp:HyperLinkField DataTextField="CountUsrType" DataNavigateUrlFields="SiteNo,tsk_id,wpid"
                                            DataNavigateUrlFormatString="frmDocApproved.aspx?Id={0}&amp;TId={1}&amp;wpid={2}&amp;doctype=common"
                                            HeaderText="Document Count" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" id="total" runat="server"></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <input id="Submit1" type="submit" size="50" value="View All" runat="server"
                                    class="buttonStyle" style="width: 100pt" visible="false" />
                                <input id="Submit2" type="submit" size="50" value="Go to Dashboard" runat="server"
                                    class="buttonStyle" style="width: 100pt" />
                            </td>
                        </tr>
                    </table>
                    <input id="Hidden1" type="hidden" runat="server" />
                </div>
            </div>
        </div>
        <div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>
    </form>--%>
</body>
</html>
