<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSSVL0RC.aspx.vb" Inherits="DashBoard_frmSSVL0RC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SSVO L0 Need to Submit</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .PnlHeader {
            Padding: 3px;
            background-color: #7f7f7f;
            Color: #ffffff;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bolder;
        }

        .PnlHeader2 {
            Padding: 7px;
            background-color: #7f7f7f;
            Color: #ffffff;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bolder;
        }

        .BtnATPOnline {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            border-width: 1px;
            border-color: #ffffff;
            background-color: #cccccc;
            color: #3F48CC;
            height: 20px;
            cursor: pointer;
        }

        .BtnATPViewAll {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            border-width: 1px;
            border-color: black;
            background-color: #cccccc;
            color: #3F48CC;
            height: 20px;
            cursor: pointer;
            width: 60px;
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
</head>
<body>
    <%--Modified by Fauzan, 9 Nov 2018. ReDesign SSV submit file--%>
    <div class="row">
        <div class="col-xs-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title"><asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
                </div>
                <form id="form1" runat="server" class="form-horizontal">
                    <div class="box-body">
                        <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                            <asp:GridView ID="gvSSVL0RC" runat="server" AllowPaging="False" EmptyDataText="SSV L0 Report need to submit"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed" Width="99%">
                            <RowStyle CssClass="GridOddRows" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="custpono" HeaderText="Cust. PONO" />
                                    <asp:BoundField DataField="siteno" HeaderText="SiteID Impl" />
                                    <asp:BoundField DataField="sitename" HeaderText="Sitename Impl" />
                                    <asp:BoundField DataField="region" HeaderText="Region" />
                                    <asp:BoundField DataField="zone" HeaderText="Zone" />
                                    <asp:BoundField DataField="Workpackageid" HeaderText="Workpackageid" />
                                    <asp:BoundField DataField="PackageName" HeaderText="PackageName" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                <asp:Label ID="lblWPID" Visible="false" runat="server" Text='<%#Eval("workpackageid")%>'></asp:Label>
                                                <asp:Label ID="lblFldtype" Visible="false" runat="server" Text='<%#Eval("fldtype")%>'></asp:Label>
                                                <asp:Label ID="lblSiteID" Visible="false" runat="server" Text='<%#Eval("site_id")%>'></asp:Label>
                                                <asp:Label ID="lblpoid" Visible="false" runat="server" Text='<%#Eval("po_id")%>'></asp:Label>
                                                <asp:Label ID="lblSiteno" Visible="false" runat="server" Text='<%#Eval("siteno")%>'></asp:Label>
                                                <asp:ImageButton ID="imgSubmitDoc" runat="server" ImageUrl="~/images/doc.gif" CommandName="submitform" CommandArgument='<%#Eval("sw_id")%>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:Button ID="BtnGoToDashboard" runat="server" Text="Go To Dashboard" CssClass="btn btn-primary pull-right" />
                    </div>
                </form>
            </div>
        </div>
    </div>
    
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div>
            <div class="PnlHeader">
                <table>
                    <tr>
                        <td style="width: 90%">SSV L0 Report Ready to Submit
                        </td>
                        <td style="width: 9%; text-align: right;">
                            <div style="text-align: right; width: 100%;">
                                <asp:Button ID="BtnGoToDashboard" runat="server" Text="Go To Dashboard" CssClass="BtnATPOnline" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 10px; min-height: 200px; max-height: 650px; overflow-y: scroll;">
                <asp:GridView ID="gvSSVL0RC" runat="server" AllowPaging="False" EmptyDataText="SSV L0 Report need to submit"
                    AllowSorting="True" AutoGenerateColumns="False" HeaderStyle-CssClass="GridHeader" Width="99%">
                    <RowStyle CssClass="GridOddRows" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="custpono" HeaderText="Cust. PONO" />
                        <asp:BoundField DataField="siteno" HeaderText="SiteID Impl" />
                        <asp:BoundField DataField="sitename" HeaderText="Sitename Impl" />
                        <asp:BoundField DataField="region" HeaderText="Region" />
                        <asp:BoundField DataField="zone" HeaderText="Zone" />
                        <asp:BoundField DataField="Workpackageid" HeaderText="Workpackageid" />
                        <asp:BoundField DataField="PackageName" HeaderText="PackageName" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="width: 100%; text-align: center;">
                                    <asp:Label ID="lblWPID" Visible="false" runat="server" Text='<%#Eval("workpackageid")%>'></asp:Label>
                                    <asp:Label ID="lblFldtype" Visible="false" runat="server" Text='<%#Eval("fldtype")%>'></asp:Label>
                                    <asp:Label ID="lblSiteID" Visible="false" runat="server" Text='<%#Eval("site_id")%>'></asp:Label>
                                    <asp:Label ID="lblpoid" Visible="false" runat="server" Text='<%#Eval("po_id")%>'></asp:Label>
                                    <asp:Label ID="lblSiteno" Visible="false" runat="server" Text='<%#Eval("siteno")%>'></asp:Label>
                                    <asp:ImageButton ID="imgSubmitDoc" runat="server" ImageUrl="~/images/doc.gif" CommandName="submitform" CommandArgument='<%#Eval("sw_id")%>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>--%>
</body>
</html>
