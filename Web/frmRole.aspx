<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRole.aspx.vb" Inherits="NSN_frmRole" %>

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
            var e = document.getElementById("ddlGroup"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Group should not be empty\n";
            }
            var e = document.getElementById("ddlLevel"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Level should not be empty\n";
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
          $('#ddlGroup').val(0);
          $('#ddlLevel').val(0);
      }

      function modalCreate() {
          clearModalFields();
          $('#usrRoleId').val("");
          $('#modalHeader').text("Create New User Role");
          $('#modalCreateUpdate').modal('show');
      }

      function modalEdit(code, desc, groupId, lvlId) {
          clearModalFields();
          $('#modalHeader').text("User Role Details");
          $('#txtCode').val(code);
          $('#txtDesc').val(desc);
          $('#ddlGroup').val(groupId);
          $('#ddlLevel').val(lvlId);
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
				        <h3 class="box-title">User Role List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-1">Search</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="R.Rolecode" Text="Code"></asp:ListItem>
                                                <asp:ListItem Value="R.RoleDesc" Text="Description"></asp:ListItem>
                                                <asp:ListItem Value="UT.GrpDesc" Text="Group"></asp:ListItem>
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
                            <asp:GridView ID="grdUsrRole" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" DataKeyNames="RoleID" 
                                 AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <RowStyle CssClass="GridEvenRows" /> 
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" Font-Names="Verdana" Font-Size="8pt" Height="5px" VerticalAlign="Middle"/>
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" SortExpression="Rolecode">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="UsrRoleCode" runat="server" 
                                                Text='<%# Eval("Rolecode") %>'
                                                CommandName="GetDetails"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="hiddenId" runat="server" 
                                                Value='<%#Eval("RoleID")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RoleDesc" HeaderText="Descrption" SortExpression="RoleDesc" />
                                    <asp:BoundField DataField="GrpDesc" HeaderText="Group" SortExpression="GrpDesc" />
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
                                <label>
                                    Code
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <input id="txtCode" runat="Server" type="text" maxlength="2" class="form-control"/>
                            </div>
                            <div class="form-group">
                                <label>
                                    Description
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <input id="txtDesc" runat="Server" type="text" maxlength="50" class="form-control"/>
                            </div>
                            <div class="form-group">
                                <label>
                                    Group
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Level
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                    <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control">
                                       <asp:ListItem Value="0">--Select--</asp:ListItem>
                                       <asp:ListItem Value="N">National</asp:ListItem>
                                       <asp:ListItem Value="A">Area</asp:ListItem>
                                       <asp:ListItem Value="R">Region</asp:ListItem>
                                       <asp:ListItem Value="Z">Zone</asp:ListItem>
                                       <asp:ListItem Value="S">Site</asp:ListItem>
                                   </asp:DropDownList>
                                </label>
                            </div>
                            <div class="form-group">
                                <asp:RadioButtonList ID="RBL" runat="server"  CssClass="selectedFieldStyle" RepeatLayout="Flow " RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Value="1">Single</asp:ListItem>
                                   <asp:ListItem Value="0">Multiple</asp:ListItem>
                               </asp:RadioButtonList>
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
        <input type="hidden" id="usrRoleId" runat="server" />
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <div>
        <div>
            <table id="tblSetup" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td align="left" colspan="4">User Role Details</td>
            </tr>
                      
            <tr>
                <td class="lblTitle">Code<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width:1%">:</td>
                <td>
                    <input id="txtCode" runat="Server" type="text" maxlength="2" class="textFieldStyle" /></td>
            </tr>
            <tr>
                <td class="lblTitle">Description<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td>:</td>
                <td>
                    <input id="txtDesc" runat="Server" type="text" maxlength="50" class="textFieldStyle" size="50" />
                    </td>
            </tr>
                       <tr>
                            <td class="lblTitle">Group<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                            <td>:</td>
                           <td><asp:DropDownList ID="ddlGroup" runat="server" CssClass="selectFieldStyle">
                           </asp:DropDownList></td>
                       </tr>
                       <tr>
                            <td class="lblTitle">Level<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                            <td>:</td>
                           <td><asp:DropDownList ID="ddlLevel" runat="server" CssClass="selectFieldStyle">
                               <asp:ListItem Value="0">--Select--</asp:ListItem>
                               <asp:ListItem Value="N">National</asp:ListItem>
                               <asp:ListItem Value="A">Area</asp:ListItem>
                               <asp:ListItem Value="R">Region</asp:ListItem>
                               <asp:ListItem Value="Z">Zone</asp:ListItem>
                               <asp:ListItem Value="S">Site</asp:ListItem>
                           </asp:DropDownList></td>
                       </tr>
                       <tr>
                           <td class="lblTitle" colspan="2">
                           </td>
                           <td>
                               <asp:RadioButtonList ID="RBL" runat="server"  CssClass="selectedFieldStyle" RepeatLayout="Flow " RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Value="1">Single</asp:ListItem>
                                   <asp:ListItem Value="0">Multiple</asp:ListItem>
                               </asp:RadioButtonList></td>
                       </tr>
                       <tr>
                           <td class="lblTitle" colspan="2">
                           </td>
                           <td style="height: 21px">
                               <asp:Button ID="btnInsert" runat="server" Text="Save" CssClass="buttonStyle" Width="75px" />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle" Width="75px" /></td>
                       </tr>
        </table>
            <table cellpadding="1" cellspacing="1" style="font: 10pt verdana" width="100%">
                <tr>
                    <td align="right" colspan="3">
                        </td>
                </tr>
                
                <tr class="pageTitle">
                    <td colspan="3">
                        <strong>User Role List</strong></td>
                </tr>
                <tr>
                    <td class="lblTitle">Search</td>
                    <td><asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="R.Rolecode" Text="Code"></asp:ListItem>
                        <asp:ListItem Value="R.RoleDesc" Text="Description"></asp:ListItem>
                        <asp:ListItem Value="UT.GrpDesc" Text="Group"></asp:ListItem>
                    </asp:DropDownList>&nbsp;<input type="text" id="txtSearch" runat="server" class="textFieldStyle" />&nbsp;<asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="goButtonStyle" />
                        </td>
                        <td align="right"><asp:Button ID="btnNew" runat="server" Text="Create" CssClass="buttonStyle" Width="75px"/></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdUsrRole" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" DataKeyNames="RoleID" 
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
                                <asp:HyperLinkField HeaderText="Code" DataTextField="Rolecode" DataNavigateUrlFields="RoleID" DataNavigateUrlFormatString = "frmRole.aspx?ID={0}" SortExpression="Rolecode"/>                                   
                                <asp:BoundField DataField="RoleDesc" HeaderText="Descrption" SortExpression="RoleDesc" />
                                <asp:BoundField DataField="GrpDesc" HeaderText="Group" SortExpression="GrpDesc" />
                                <asp:TemplateField>
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
