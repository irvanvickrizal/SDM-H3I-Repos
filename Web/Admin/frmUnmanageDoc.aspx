﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUnmanageDoc.aspx.vb"
    Inherits="Admin_frmUnmanageDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unmanage Document</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }
        .HeaderPanel
        {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top: -5px;
            margin-bottom: 10px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-left: 8px;
            border-style: solid;
            border-color: Gray;
            border-width: 2px;
            color: White;
        }
        .HeaderGrid
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
            height: 25px;
        }
        .oddGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .emptyrow
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Green;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
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

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Unmanage Management</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Parent Document Type</label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DdlFilterParentDocType" runat="server" CssClass="form-control"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="-- select Parent Doc Type --" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="General" Value="GENERAL"></asp:ListItem>
                                                        <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                                                        <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                                    </asp:DropDownList>
								                </div>
							                </div>
						                </div>

                                        <asp:GridView ID="GvUnManageDocs" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                                            PageSize="10" AllowPaging="true" CssClass="table table-bordered table-condensed">
                                            <RowStyle CssClass="oddGrid" />
                                            <AlternatingRowStyle CssClass="evenGrid" />
                                            <HeaderStyle CssClass="HeaderGrid" />
                                            <EmptyDataRowStyle CssClass="emptyrow" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DocName" HeaderText="Document" />
                                                <asp:BoundField DataField="ParentDocType" HeaderText="Parent Type" />
                                                <asp:BoundField DataField="Modifieduser" HeaderText="Modified User" />
                                                <asp:BoundField DataField="LMDT" HeaderText="Modified Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                                    ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                                            ToolTip="Delete Workflow" Width="20px" Height="20px" CommandName="deletedoc"
                                                            CommandArgument='<%#Eval("undocid") %>' OnClientClick="return confirm('Are you sure you want to delete this document?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
					                </div>
				                </div>
			                </div>
			                <div class="box-footer">
                                <asp:Label ID="LblGvWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px" CssClass="form-control"
                                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;" />
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row">
	                <div class="col-xs-12">
		                <div class="box box-info">
			                <div class="box-header with-border">
				                <h3 class="box-title">Add Unmange Document</h3>
			                </div>
			                <div class="box-body">
				                <div class="table table-responsive">
					                <div class="col-md-6">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">Form Type</label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DdlParentType" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Text="-- select Parent Doc Type --" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="General" Value="GENERAL"></asp:ListItem>
                                                        <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                                                        <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                                    </asp:DropDownList>
								                </div>
							                </div>
                                        </div>
                                        <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                                            PageSize="5" AllowPaging="true" CssClass="table table-bordered table-condensed">
                                            <RowStyle CssClass="oddGrid" />
                                            <AlternatingRowStyle CssClass="evenGrid" />
                                            <HeaderStyle CssClass="HeaderGrid" />
                                            <EmptyDataRowStyle CssClass="emptyrow" />
                                            <PagerSettings Position="TopAndBottom" />
                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DocName" HeaderText="DocName" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                            CommandName="adddoc" CommandArgument='<%#Eval("docid") %>' Width="25px" Height="25px"
                                                            ToolTip="Add Document" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
					                </div>
				                </div>
			                </div>
			                <div class="box-footer">
                                <asp:Label ID="LblWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px" CssClass="form-control"
                                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;" />
			                </div>
		                </div>
	                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        <span>Unmanage Management</span>
    </div>
    <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>
            <div style="border-bottom-style: solid; border-bottom-color: Gray; border-bottom-width: 2px;
                padding-bottom: 10px; min-height: 300px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <span class="ltrLabel">Parent Document Type</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlFilterParentDocType" runat="server" CssClass="ltrLabel"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="-- select Parent Doc Type --" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="General" Value="GENERAL"></asp:ListItem>
                                    <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                                    <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px;">
                    <asp:GridView ID="GvUnManageDocs" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                        PageSize="10" AllowPaging="true" Width="99%">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <HeaderStyle CssClass="HeaderGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DocName" HeaderText="Document" />
                            <asp:BoundField DataField="ParentDocType" HeaderText="Parent Type" />
                            <asp:BoundField DataField="Modifieduser" HeaderText="Modified User" />
                            <asp:BoundField DataField="LMDT" HeaderText="Modified Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                        ToolTip="Delete Workflow" Width="20px" Height="20px" CommandName="deletedoc"
                                        CommandArgument='<%#Eval("undocid") %>' OnClientClick="return confirm('Are you sure you want to delete this document?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <asp:Label ID="LblGvWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div style="width: 45%; margin-top: 10px; border-style: solid; border-width: 1px;
                border-color: Gray;">
                <div style="background-color: Black; padding: 5px; margin-bottom: 8px;">
                    <span class="lblField" style="color: White;">Add Unmanage Document</span>
                </div>
                <table>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrFormType" runat="server" Text="Form Type"></asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlParentType" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                                <asp:ListItem Text="-- select Parent Doc Type --" Value="0"></asp:ListItem>
                                <asp:ListItem Text="General" Value="GENERAL"></asp:ListItem>
                                <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                                <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 350px;">
                            <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                                PageSize="5" AllowPaging="true" Width="99%">
                                <RowStyle CssClass="oddGrid" />
                                <AlternatingRowStyle CssClass="evenGrid" />
                                <HeaderStyle CssClass="HeaderGrid" />
                                <EmptyDataRowStyle CssClass="emptyrow" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DocName" HeaderText="DocName" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Images/gridview/AddNewitem.jpg"
                                                CommandName="adddoc" CommandArgument='<%#Eval("docid") %>' Width="25px" Height="25px"
                                                ToolTip="Add Document" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LblWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                                ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>--%>
</body>
</html>
