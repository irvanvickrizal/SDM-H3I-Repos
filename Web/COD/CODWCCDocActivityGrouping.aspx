<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CODWCCDocActivityGrouping.aspx.vb"
    Inherits="COD_CODWCCDocActivityGrouping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Document Activity Grouping</title>
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
        .lblText
        {
            font-family:verdana;
            font-size:11px;
        }
        .lblBoldText
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
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
        .btnstyle
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            background-color:#c3c3c3;
            color:#fff;
            padding:8px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
    </style>

    <script type="text/javascript">
        function ErrorSave()  
        {
            alert("Error While Saving the Data, Please Try Again!");
            return false;
        }
        function SucceedSave()  
        {
            alert("Data Successful saved");
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
				        <h3 class="box-title">WCC Activity Document Grouping</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
                                <asp:GridView ID="GvDocGrouping" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="5"
                                    CssClass="table table-bordered table-condensed">
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle CssClass="customPagination" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                                    CommandName="DeleteGrouping" Width="20px" Height="20px" CommandArgument='<%#Eval("DocActivityId") %>'
                                                    OnClientClick="return confirm('Are you sure you want to delete this Activity ?')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DocName" HeaderText="Document" />
                                        <asp:BoundField DataField="ActivityName" HeaderText="Subcon Type" />
                                        <asp:BoundField DataField="CDT" HeaderText="Create Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                            ConvertEmptyStringToNull="true" />
                                        <asp:BoundField DataField="ModifiedUser" HeaderText="Create User" />
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
				        <h3 class="box-title">Add New WCC Activity Document Grouping</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Parent Doc Type</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlParentDocType" runat="server" CssClass="form-control" AutoPostBack="true">
                                                <asp:ListItem Text="--Select Parent Doc Type--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="BAUT" Value="BAUT"></asp:ListItem>
                                                <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                            </asp:DropDownList>
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Doc Type</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlDocType" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-3 control-label">Subcon Type</label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="DdlActivities" runat="server" CssClass="form-control" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-3 control-label"></div>
								        <div class="col-sm-3">
                                            <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn btn-block btn-primary" />
								        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-block btn-warning" />
                                        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
			        <div class="box-footer">
                        <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText" ForeColor="red" />
			        </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div>
            <asp:MultiView ID="MvCorePanel" runat="server">
                <asp:View ID="vwListDocs" runat="server">
                    <div style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: Gray;
                        padding-bottom: 10px; width: 99%;">
                        <asp:GridView ID="GvDocGrouping" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                            Width="99%">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                            CommandName="DeleteGrouping" Width="20px" Height="20px" CommandArgument='<%#Eval("ActivityId") %>'
                                            OnClientClick="return confirm('Are you sure you want to delete this Activity ?')" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocName" HeaderText="Document" />
                                <asp:BoundField DataField="ActivityName" HeaderText="Subcon Type" />
                                <asp:BoundField DataField="CDT" HeaderText="Create Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    ConvertEmptyStringToNull="true" />
                                <asp:BoundField DataField="ModifiedUser" HeaderText="Create User" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="margin-top: 10px; border-style: solid; border-color: Gray; border-width: 2px;
                        padding: 5px; width: 35%;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblParentDocType" runat="server" Text="Parent Doc Type" CssClass="lblBoldText"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlParentDocType" runat="server" CssClass="lblText" AutoPostBack="true">
                                        <asp:ListItem Text="--Select Parent Doc Type--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="BAUT" Value="BAUT"></asp:ListItem>
                                        <asp:ListItem Text="WCC" Value="WCC"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblDocType" runat="server" Text="Doc Type" CssClass="lblBoldText"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlDocType" runat="server" CssClass="lblText" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblSubconActivity" runat="server" Text="Subcon Type" CssClass="lblBoldText"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlActivities" runat="server" CssClass="lblText">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top: 10px; text-align: right; width: 400px;">
                            <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btnstyle" />
                            <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btnstyle" />
                        </div>
                        <div style="margin-top: 5px;" id="panelWarningMessage" runat="server">
                            <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText" ForeColor="red"></asp:Label>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vwModifiedUsers" runat="server">
                </asp:View>
            </asp:MultiView>
        </div>
    </form>--%>
</body>
</html>
