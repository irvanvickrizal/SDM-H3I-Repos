<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODDocumentSubtitute.aspx.vb"
    Inherits="COD_frmCODDocumentSubtitute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document Subtitution</title>
    <link href="../CSS/ValidationMessage.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
    .gridHeader
    {
	    font-family:Verdana;
	    font-size:11px;
	    background-color:#ffc727;
	    font-weight:bolder;
	    text-align:center;
	    padding:5px;
	    color:white;
	    border-style:solid;
	    border-width:2px;
	    border-color:gray;
    }
    .gridOdd
    {
        font-family:Verdana;
	    font-size:11px;
	    padding:5px;
    }
    .gridEven
    {
        font-family:Verdana;
	    font-size:11px;
	    background-color:#cfcfcf;
	    padding:5px;
    }
    .emptyRow
    {
        background-color:maroon;
        width:98%;
        padding:5px;
        font-size:11px;
        font-weight:bolder;
        font-family:verdana;
        color:#fff;
    }
    .lblText
    {
        font-family:Verdana;
	    font-size:11px;
    }
    .buttonStyle
    {
        border-style:solid;
        border-width:2px;
        border-color:#c3c3c3;
        padding:4px;
        color:#000;
        background-color:#b5e61d; 
        font-family:verdana;
        font-size:11px;
        font-weight:bolder;
        cursor:pointer;
    }
    .panelSaveForm
    {
        width:550px;
        padding: 3px;
        background: #f0f0f0;
        border-style: outset;
        border-width: 1px;
        border-color: #c3c3c3;
        box-shadow: 0 0 2px #000;
        background: -webkit-gradient(linear, left top, left bottom, 
                    color-stop(0%, white), color-stop(15%, white), color-stop(100%, #f0f0f0));
        background: -moz-linear-gradient(top, white 0%, white 55%, #f0f0f0 130%);
    }
    .panelGrid
    {
        width:98%;
        padding: 5px;
        background: #f0f0f0;
        border-style: outset;
        border-width: 1px;
        border-color: #c3c3c3;
        box-shadow: 0 0 2px #222;
        background: -webkit-gradient(linear, left top, left bottom, 
                    color-stop(0%, white), color-stop(15%, white), color-stop(100%, #f0f0f0));
        background: -moz-linear-gradient(top, white 0%, white 55%, #f0f0f0 130%);
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Document Substitude List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Document</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlDocuments" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
						        </div>
                                <asp:GridView ID="GvDocumentSubtitutes" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                    AllowPaging="true" PageSize="5" CssClass="table table-bordered table-condensed" DataKeyNames="DocSubtituteId">
                                    <RowStyle CssClass="gridOdd" />
                                    <AlternatingRowStyle CssClass="gridEven" />
                                    <%--<EmptyDataRowStyle CssClass="emptyRow" />--%>
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                                            ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/delete.jpg" CommandArgument='<%#Eval("DocSubtituteId") %>'
                                                    CommandName="DeleteDoc" OnClientClick="return confirm('Are you sure you want to delete this Subtitute of document ?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocName" HeaderText="Document Name" HeaderStyle-CssClass="gridHeader"
                                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                        <asp:BoundField DataField="DocNameChange" HeaderText="Subtitute Doc Name" HeaderStyle-CssClass="gridHeader"
                                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                        <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By" HeaderStyle-CssClass="gridHeader"
                                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                        <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified Date" HtmlEncode="false"
                                            HeaderStyle-CssClass="gridHeader" DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true"
                                            ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                                    </Columns>
                                </asp:GridView>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Add New Document Substitude</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Document</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlDocumentsI" runat="server" CssClass="form-control" ValidationGroup="subtitutedoc" />
                                            <asp:CompareValidator ID="CvDocumentsI" runat="server" ControlToValidate="DdlDocumentsI" SetFocusOnError="true" Operator="GreaterThan" 
                                                ValueToCompare="0" ErrorMessage="Required" Font-Names="Verdana" Font-Size="11px" ValidationGroup="subtitutedoc" CssClass="dnnFormMessage dnnFormError" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Substitude Document</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlSubtituteDocumentsI" runat="server" CssClass="form-control" />
                                            <asp:CompareValidator ID="CvSubtituteDocumentsI" runat="server" ControlToValidate="DdlSubtituteDocumentsI" SetFocusOnError="true" Operator="GreaterThan" ValueToCompare="0" 
                                                ErrorMessage="Required" Font-Names="Verdana" Font-Size="11px" ValidationGroup="subtitutedoc" CssClass="dnnFormMessage dnnFormError" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-3"></div>
								        <div class="col-sm-6">
                                            <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn btn-block btn-primary" ValidationGroup="subtitutedoc" />
								        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div>
            <asp:Label ID="LblDocument" runat="server" Text="Document" CssClass="lblText"></asp:Label>
            <asp:DropDownList ID="DdlDocuments" runat="server" CssClass="lblText" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div style="margin-top: 10px;" class="panelGrid">
            <asp:GridView ID="GvDocumentSubtitutes" runat="server" AutoGenerateColumns="false"
                Width="100%" EmptyDataText="No Record Found">
                <RowStyle CssClass="gridOdd" />
                <AlternatingRowStyle CssClass="gridEven" />
                <EmptyDataRowStyle CssClass="emptyRow" />
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="gridHeader" ItemStyle-BorderColor="GrayText"
                        ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/delete.jpg"
                                Width="18px" Height="18px" CommandName="DeleteDoc" OnClientClick="return confirm('Are you sure you want to delete this Subtitute of document ?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DocName" HeaderText="Document Name" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="DocNameChange" HeaderText="Subtitute Doc Name" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By" HeaderStyle-CssClass="gridHeader"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                    <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified Date" HtmlEncode="false"
                        HeaderStyle-CssClass="gridHeader" DataFormatString="{0:dd-MMM-yyyy}" ConvertEmptyStringToNull="true"
                        ItemStyle-BorderColor="GrayText" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                </Columns>
            </asp:GridView>
        </div>
        <div style="width: 99%;">
            <div class="panelSaveForm" style="margin-top: 10px;">
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LblDocName" runat="server" Text="Document" CssClass="lblText"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlDocumentsI" runat="server" CssClass="lblText" ValidationGroup="subtitutedoc" Width="300px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CvDocumentsI" runat="server" ControlToValidate="DdlDocumentsI"
                                    SetFocusOnError="true" Operator="GreaterThan" ValueToCompare="0" ErrorMessage="Required"
                                    Font-Names="Verdana" Font-Size="11px" ValidationGroup="subtitutedoc" CssClass="dnnFormMessage dnnFormError"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDocNameSubtitute" runat="server" Text="Subtitute Document" CssClass="lblText"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlSubtituteDocumentsI" runat="server" CssClass="lblText" Width="300px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CvSubtituteDocumentsI" runat="server" ControlToValidate="DdlSubtituteDocumentsI" 
                                    SetFocusOnError="true" Operator="GreaterThan" ValueToCompare="0" ErrorMessage="Required"
                                    Font-Names="Verdana" Font-Size="11px" ValidationGroup="subtitutedoc" CssClass="dnnFormMessage dnnFormError"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="margin-top: 5px; text-align: right;">
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="buttonStyle" Width="80px"
                        ValidationGroup="subtitutedoc" />
                </div>
            </div>
        </div>
    </form>--%>
</body>
</html>
