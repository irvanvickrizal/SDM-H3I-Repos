<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWFDoc.aspx.vb" Inherits="frmWFDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Flow Mapping</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Process Flow Mapping List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-9">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Select Process Flow</label>
								        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlWF" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
                                        <div class="col-sm-4"><span style="display:none"></span></div>
                                        <div class="col-sm-2">
                                            <input class="btn btn-block btn-info" type="button" runat="server" value="Change" id="btnChange" />
                                        </div>
							        </div>
						        </div>
					        </div>
                            <asp:GridView ID="grdWF" runat="server" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No Records Found"
                                CssClass="table table-bordered table-condensed">
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <RowStyle CssClass="GridOddRows" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DocCode" HeaderText="Code" SortExpression="DocCode" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="DocName" HeaderText="Name" SortExpression="DocName" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            </asp:GridView>
				        </div>
			        </div>
			        <div class="box-footer"></div>
		        </div>
	        </div>
        </div>

        <table cellpadding="1" cellspacing="1" width="100%">
            
            <tr>
                <td colspan="3">
                    
                </td>
            </tr>
        </table>
        <input type="hidden" id="hdnSort" runat="server" />
    </form>
    <%--<form id="form1" runat="server">
        <table cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td colspan="3" class="pageTitle">
                    Process Flow Mapping List</td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Select Process Flow</td>
                <td>
                    :
                    <asp:DropDownList ID="ddlWF" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                    </asp:DropDownList></td>
                <td align="right">
                    <input class="buttonStyle" type="button" runat="server" value="Change" id="btnChange" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdWF" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True"
                        Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5">
                        <PagerSettings Position="TopAndBottom" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DocCode" HeaderText="Code" SortExpression="DocCode" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="DocName" HeaderText="Name" SortExpression="DocName" />
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <input type="hidden" id="hdnSort" runat="server" />
    </form>--%>
</body>
</html>
