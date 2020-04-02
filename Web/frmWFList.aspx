<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWFList.aspx.vb" Inherits="frmWFList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Process Flow List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-12">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-1 control-label">Search</label>
								        <div class="col-xs-2">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="WFcode" Text="Code"></asp:ListItem>
                                                <asp:ListItem Value="WFDesc" Text="Description"></asp:ListItem>
                                            </asp:DropDownList>
								        </div>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtSearch" runat="server" class="form-control" />
                                        </div>
                                        <div class="col-xs-1">
                                            <asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="btn btn-block btn-success" />
                                        </div>
                                        <div class="col-sm-3"></div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnNew" runat="server" Text="Create" CssClass="btn btn-block btn-primary"/>
                                        </div>
							        </div>
						        </div>
					        </div>
                            <asp:GridView ID="grdWF" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" AutoGenerateColumns="False" 
                                EmptyDataText="No Records Found" PageSize="5" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataNavigateUrlFields="WFId" DataTextField="WFCode" HeaderText="Code" DataNavigateUrlFormatString="frmWFSetup.aspx?id={0}" SortExpression="WFCode" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="WFDesc" HeaderText="Name" SortExpression="WFDesc" />
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
			        <div class="box-footer"></div>
		        </div>
	        </div>
        </div>
        <input type="hidden" id="hdnSort" runat="server" />
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td colspan="3">Process Flow List</td>
            </tr>
            <tr>
                <td class="lblTitle" style="width: 169px">Search</td>
                <td>
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="WFcode" Text="Code"></asp:ListItem>
                        <asp:ListItem Value="WFDesc" Text="Description"></asp:ListItem>
                    </asp:DropDownList>&nbsp;<input type="text" id="txtSearch" runat="server" class="textFieldStyle" />&nbsp;<asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="goButtonStyle" />
                </td>
                <td align="right">
                    <asp:Button ID="btnNew" runat="server" Text="Create" CssClass="buttonStyle" Width="75px" /></td>
            </tr>

            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdWF" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="WFId" DataTextField="WFCode" HeaderText="Code" DataNavigateUrlFormatString="frmWFSetup.aspx?id={0}" SortExpression="WFCode" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="WFDesc" HeaderText="Name" SortExpression="WFDesc" />
                        </Columns>

                    </asp:GridView>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hdnSort" runat="server" />
    </form>--%>
</body>
</html>
