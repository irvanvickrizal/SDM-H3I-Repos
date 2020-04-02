<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODWCCDocument.aspx.vb"
    Inherits="COD_frmCODWCCDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Document Configuration</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family:verdana;
            font-size:8pt;
            color:#000;
        }
        .lblField
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:#000;
        }
        .panelLeft
        {
            float:left;
            width:35%;
        }
        .panelRight
        {
            float:right;
            width:62%;
        }
        .HeaderGrid
        {
        font-family: Arial Unicode MS;
        font-size: 8pt;
        font-weight: bold;
        color:white;
        background-color:Orange;
        border-color:black;
        border-style:solid;
        border-width:1px;
        vertical-align:middle;
        }
        .oddGrid
        {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color: White;
        border-color:black;
        border-style:solid;
        border-width:1px;
        }
        .evenGrid
        {
        font-family: Arial Unicode MS;
        font-size: 7.5pt;
        background-color:#cfcfcf;
        border-color:black;
        border-style:solid;
        border-width:1px;
        }
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:11px;
            font-weight:bold;
            margin-bottom:10px;
            padding-left:3px;
            padding-top:3px;
            padding-bottom:3px;
        }
    </style>
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
				        <h3 class="box-title" id="LblHeaderDocumentGrouping" runat="server">Additional Document Grouping</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-10">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-1 control-label">Detail of Scope</label>
								        <div class="col-sm-3">
                                            <asp:DropDownList ID="DdlScopeDetail" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="DdlParentDocumentIndexChanged" />
								        </div>
								        <label class="col-sm-1 control-label">Document Parent</label>
								        <div class="col-sm-3">
                                            <asp:DropDownList ID="DdlParentDocument" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlParentDocumentIndexChanged"
                                                CssClass="form-control">
                                                <asp:ListItem Text="-- Select Parent Doc --" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                                <asp:ListItem Text="BAUT" Value="BAUT"></asp:ListItem>
                                            </asp:DropDownList>
								        </div>
								        <label class="col-sm-1 control-label">Subcon Activity</label>
								        <div class="col-sm-3">
                                            <asp:DropDownList ID="DdlActivities" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
							        <div class="form-group">
							        </div>
							        <div class="form-group">
							        </div>
						        </div>
					        </div>
                            <div class="col-md-6">
                                <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid" AllowPaging="true" PageSize="5"
                                    CssClass="table table-condensed table-bordered">
                                    <RowStyle CssClass="oddGrid" />
                                    <AlternatingRowStyle CssClass="evenGrid" />
                                    <PagerSettings Position="Bottom" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                    CommandName="AddNewDoc" CommandArgument='<%#Eval("Doc_Id") %>' Width="20px" Height="20px"
                                                    ToolTip="Add new document" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                        <asp:TemplateField HeaderText="Is Mandatory">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlMandatoryStatus" runat="server" CssClass="ltrLabel">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-md-6">
                                <asp:GridView ID="GvDocumentGrouping" runat="server" AutoGenerateColumns="false" EmptyDataText="No Document Grouping" HeaderStyle-CssClass="HeaderGrid"
                                    AllowPaging="true" PageSize="5" CssClass="table table-condensed table-bordered">
                                    <RowStyle CssClass="oddGrid" />
                                    <AlternatingRowStyle CssClass="evenGrid" />
                                    <EmptyDataRowStyle CssClass="evenGrid" />
                                    <PagerSettings Position="Bottom" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" CommandName="DeleteDoc" CommandArgument='<%#Eval("WCCDOCId") %>'
                                                    runat="server" ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px"
                                                    Width="16px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                        <asp:CheckBoxField DataField="IsMandatory" HeaderText="Is Mandatory" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DOCWCCLMBY" HeaderText="LMBY" />
                                        <asp:BoundField DataField="DOCWCCLMDT" HeaderText="LMDT" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                            ConvertEmptyStringToNull="true" />
                                    </Columns>
                                </asp:GridView>
                            </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div style="width: 100%;">
            <div class="panelLeft">
                <div>
                    <table>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrScope" runat="server">Detail of Scope</asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlScopeDetail" runat="server" CssClass="ltrLabel" AutoPostBack="true"
                                    OnSelectedIndexChanged="DdlParentDocumentIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrDocument" runat="server">Document Parent</asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlParentDocument" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlParentDocumentIndexChanged"
                                    CssClass="ltrLabel">
                                    <asp:ListItem Text="-- Select Parent Doc --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                    <asp:ListItem Text="BAUT" Value="BAUT"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="panelRight">
                <div style="padding-left: 3px;">
                    <div class="HeaderReport" style="width: 70%;">
                        <asp:Label ID="LblHeaderDocumentGrouping" runat="server" Text="Additional Document Grouping"></asp:Label>
                    </div>
                    <div style="border-left-color: Black; border-left-style: dashed; border-left-width: 1px;">
                        <div style="margin-left:10px;">
                            <asp:Label ID="LblSubconType" runat="server" Text="Subcon Activity" CssClass="lblField"></asp:Label>
                            <asp:DropDownList ID="DdlActivities" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div style="margin-top: 5px;margin-left:10px;">
                            <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="HeaderGrid"
                                Width="99%">
                                <RowStyle CssClass="oddGrid" />
                                <AlternatingRowStyle CssClass="evenGrid" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                CommandName="AddNewDoc" CommandArgument='<%#Eval("Doc_Id") %>' Width="20px" Height="20px"
                                                ToolTip="Add new document" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                    <asp:TemplateField HeaderText="Is Mandatory">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DdlMandatoryStatus" runat="server" CssClass="ltrLabel">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div>
                        <asp:GridView ID="GvDocumentGrouping" runat="server" AutoGenerateColumns="false"
                            CellPadding="3" CellSpacing="3" EmptyDataText="No Document Grouping" HeaderStyle-CssClass="HeaderGrid">
                            <RowStyle CssClass="oddGrid" />
                            <AlternatingRowStyle CssClass="evenGrid" />
                            <EmptyDataRowStyle CssClass="evenGrid" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="DeleteDoc" CommandArgument='<%#Eval("WCCDOCId") %>'
                                            runat="server" ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px"
                                            Width="16px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocName" HeaderText="Document Name" />
                                <asp:CheckBoxField DataField="IsMandatory" HeaderText="Is Mandatory" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DOCWCCLMBY" HeaderText="LMBY" />
                                <asp:BoundField DataField="DOCWCCLMDT" HeaderText="LMDT" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    ConvertEmptyStringToNull="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>--%>
</body>
</html>
