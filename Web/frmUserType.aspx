<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserType.aspx.vb" Inherits="NSN_frmUserType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Type</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
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
  <script language="javascript" type="text/javascript">
        function checkIsEmpty()    
        {
            var msg=""; 
                               
            if (IsEmptyCheck(document.getElementById("txtCode").value) == false)
            {
                msg = msg + "Code should not be empty\n"
            } 
              
            if (IsEmptyCheck(document.getElementById("txtDesc").value) == false)
            {
                msg = msg + "Description should not be empty\n"
            } 
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return true;
            }           
      } 

      function clearModalFields() {
          $('#txtCode').val("");
          $('#txtDesc').val("");
          $('#btnInsert').prop('value', 'Save');
          $('#btnDelete').attr("style", "display:none");
      }

      function modalCreate() {
          clearModalFields();
          $('#usrTypeId').val("");
          $('#modalHeader').text("Create New User Type");
          $('#modalCreateUpdate').modal('show');
      }

      function modalEdit(code, desc) {
          clearModalFields();
          $('#modalHeader').text("User Type Details");
          $('#txtCode').val(code);
          $('#txtDesc').val(desc);
          $('#btnInsert').prop('value', 'Update');
          $('#btnDelete').attr("style", "display:block");
          $('#modalCreateUpdate').modal('show');
      }

      function modalClose() {
          clearModalFields();
          $('#modalCreateUpdate').modal('hide');
      }
  </script>  
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">User Type List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-1">Search</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="grpCode" Text="Code"></asp:ListItem>
                                                <asp:ListItem Value="grpDesc" Text="Description"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtSearch" runat="server" class="form-control" />
							            </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="btn btn-block btn-success" />
                                        </div>
							        </div>
						        </div>
					        </div>
                            <div class="col-md-3">
                                <div class="form-inline">
                                    <div class="col-xs-5">
                                        <input id="btnNew"  value="Create" class="btn btn-block btn-primary" onclick="modalCreate()" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <span style="display:block;height:10px"></span>
                            </div>
                            <asp:GridView ID="grdUsrGrp" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" DataKeyNames="GrpId" 
                                 AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <RowStyle CssClass="GridEvenRows" /> 
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" SortExpression="GrpCode">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="UsrTypeCode" runat="server" 
                                                Text='<%# Eval("GrpCode") %>'
                                                CommandName="GetDetails"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="hiddenId" runat="server" 
                                                Value='<%#Eval("GrpId")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="GrpDesc" HeaderText="Descrption"
                                        SortExpression="GrpDesc" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            </asp:GridView>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>

        <%--Modal--%>
        <div class="modal fade" id="modalCreateUpdate" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label id="modalHeader"></label>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="form-group">
                                <label>Code</label>
                                <input id="txtCode" runat="Server" type="text" maxlength="1" class="form-control" onkeypress="javascript:return allowKeyAcceptsData('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');"/>
                            </div>
                            <div class="form-group">
                                <label>Description</label>
                                <input id="txtDesc" runat="Server" type="text" maxlength="15" class="form-control" onkeypress="javascript:return allowKeyAcceptsData('0123456789. ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');"/>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="btnInsert" runat="server" Text="Save" CssClass="btn btn-block btn-primary" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="btn btn-block btn-danger" />
                        <input type="button" value="Cancel" onclick="modalClose()" class="btn btn-block btn-warning" />
                    </div>
                </div>
            </div>
        </div>
        <input id="hdnSort" type="hidden" runat="server" />
        <input type="hidden" id="usrTypeId" runat="server" />
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <div>
        <div>
                   <table id="tblSetup" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td align="left" colspan="4" style="height: 26px">
                    User Type Details</td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Code<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                    <td style="width:1%">:</td>
                <td>
                    <input id="txtCode" runat="Server" type="text" maxlength="1" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsData('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');"/></td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Description<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                    <td>:</td>
                <td style="height: 21px">
                    <input id="txtDesc" runat="Server" type="text" maxlength="15" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsData('0123456789. ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');"/>
                    </td>
            </tr>
                       <tr>
                           <td class="lblTitle" colspan="2">
                           </td>
                           <td style="height: 21px">
                               <asp:Button ID="btnInsert" runat="server" Text="Save" CssClass="buttonStyle" Width="75px" />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle" Width="75px" /></td>
                       </tr>
        </table>
            <table cellpadding="1" cellspacing="1" width="100%">
                
                
                <tr class="pageTitle">
                    <td colspan="4">
                        <strong>User Type List</strong></td>
                </tr>
                <tr>
                    <td class="lblTitle">Search</td>
                    <td style="width:1%">:</td>
                    <td><asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="grpCode" Text="Code"></asp:ListItem>
                        <asp:ListItem Value="grpDesc" Text="Description"></asp:ListItem>
                    </asp:DropDownList>&nbsp;<input type="text" id="txtSearch" runat="server" class="textFieldStyle" />&nbsp;<asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="goButtonStyle" />
                        </td><td align="right"><asp:Button ID="btnNew" runat="server" Text="Create" CssClass="buttonStyle" Width="75px"/></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="grdUsrGrp" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" DataKeyNames="GrpId" 
                             AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" >
                            <PagerSettings Position="TopAndBottom" />
                            <AlternatingRowStyle CssClass="GridOddRows" />
                            <RowStyle CssClass="GridEvenRows" /> 
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px" VerticalAlign="Middle"/>
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Code" DataTextField="GrpCode" DataNavigateUrlFields="GrpId" DataNavigateUrlFormatString = "frmUserType.aspx?ID={0}" SortExpression="GrpCode"/>                                   
                                <asp:BoundField DataField="GrpDesc" HeaderText="Descrption"
                                    SortExpression="GrpDesc" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" CommandArgument='<%#Container.DataItem("GrpId")%>"'
                                            CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete');"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
        <input id="hdnSort" type="hidden" runat="server" />
    </form>--%>
</body>
</html>
