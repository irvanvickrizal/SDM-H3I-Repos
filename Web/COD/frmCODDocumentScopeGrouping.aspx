<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODDocumentScopeGrouping.aspx.vb"
    Inherits="COD_frmCODDocumentScopeGrouping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scope Document Grouping</title>
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
        .emptyRowStyle
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:maroon;
            border-style:solid;
            padding:3px;
            border-width:1px;
            border-color:gray;
        }
        .GridHeader
        {
           background-color:#ffc90e;
           font-family:verdana;
           font-weight:bold;
           font-size:9pt;
           text-align:center;
           height:30px;
           color:white;
        }
        .GridOddRows
        {
           font-family:verdana;
           font-size:7.5pt;
        }
        .GridEvenRows
        {
           background-color:#cfcfcf;
           font-family:verdana;
           font-size:7.5pt;
        }
    </style>

    <script type="text/javascript">
        function FailedSaved() {
            alert('Data Failed to Save, Please Try Again!');
            return false;
        }
        function SucceedSaved() {
            alert('Data Successfully Save');
            return true;
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Document Grouping of Scope</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
                                <asp:GridView ID="GvDocumentGroupings" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="5"
                                    CssClass="table table-bordered table-condensed">
                                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <SelectedRowStyle CssClass="GridEvenRows" />
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle CssClass="customPagination" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" CommandName="deletedoc" runat="server" CommandArgument='<%#Eval("DocGroupId") %>'
                                                    ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px" Width="16px" OnClientClick="return confirm('Are you sure you want to delete this Activity ?')"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocName" HeaderText="Document" />
                                        <asp:BoundField DataField="DScopeName" HeaderText="Scope Name" />
                                        <asp:BoundField DataField="LMBY" HeaderText="Last Modified" />
                                        <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
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
				        <h3 class="box-title">New Document Grouping</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Scope Detail</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlScopeDetails" runat="server" CssClass="form-control" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Parent Document</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlParentDocument" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-3"></div>
								        <div class="col-sm-6">
                                            <asp:Button ID="BtnAddParentDocument" runat="server" Text="Add" CssClass="btn btn-block btn-primary" />
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
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">Document Grouping of Scope</span>
            </div>
            <div style="margin-top: 5px;">
                <asp:GridView ID="GvDocumentGroupings" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                    Width="600px">
                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                    <HeaderStyle CssClass="GridHeader" />
                    <RowStyle CssClass="GridOddRows" />
                    <SelectedRowStyle CssClass="GridEvenRows" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CommandName="deletedoc" runat="server" CommandArgument='<%#Eval("DocGroupId") %>'
                                    ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px" Width="16px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocName" HeaderText="Document" />
                        <asp:BoundField DataField="DScopeName" HeaderText="Scope Name" />
                        <asp:BoundField DataField="LMBY" HeaderText="Last Modified" />
                        <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="width: 360px; margin-top: 10px; border-style: solid; padding: 3px; border-color: #c3c3c3;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">New Document Grouping</span>
            </div>
            <div style="margin-top: 2px;">
                <table>
                    <tr>
                        <td>
                            <span class="ltrLabel">Scope Detail</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlScopeDetails" runat="server" CssClass="ltrLabel"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <span class="ltrLabel">Parent Document : </span><asp:DropDownList ID="DdlParentDocument" runat="server" CssClass="ltrLabel" AutoPostBack="true"></asp:DropDownList><asp:Button ID="BtnAddParentDocument" runat="server" Text="Add" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GvDocuments" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkDocumentID" runat="server" />
                                            <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("Doc_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:BoundField DataField="DocName" HeaderText="Document" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    
                </table>
            </div>
        </div>
    </form>--%>
</body>
</html>
