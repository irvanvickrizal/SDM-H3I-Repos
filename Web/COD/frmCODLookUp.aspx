<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODLookUp.aspx.vb" Inherits="frmLookUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>LookUp Details</title>
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />
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
    
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function checkIsEmpty()
        {
            var msg = "";
            var e = document.getElementById("ddlGroup"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Group should be select\n"
            }
            if (IsEmptyCheck(document.getElementById("txtCCode").value) == false)
            {
                msg = msg + "Code should not be Empty\n"
            }
            if (IsEmptyCheck(document.getElementById("txtCName").value) == false)
            {
                msg = msg + "Description should not be Empty\n"
            }
            if (msg != "")
            {
                alert("Mandatory field information :\n\n" + msg);
                return false;
            }
            else
            {
                return true;
            }           
        }        

        function clearModalFields() {
            $('#txtLKPCode').val("");
            $('#txtLKPDesc').val("");
            $('#btnSave').prop('value', 'Save');
            $('#btnDelete').attr("style", "display:none");
            $('#DDLGRP').val(0);
        }

        function modalCreate() {
            clearModalFields();
            $('#lookupId').val("");
            $('#modalHeader').text("Create New Lookup");
            $('#modalCreateUpdate').modal('show');
        }

        function modalEdit(code, desc, group) {
            clearModalFields();
            $('#modalHeader').text("Lookup Details");
            $('#txtLKPCode').val(code);
            $('#txtLKPDesc').val(desc);
            $('#DDLGRP').val(group);
            $('#btnSave').prop('value', 'Update');
            $('#btnDelete').attr("style", "display:block");
            $('#modalCreateUpdate').modal('show');
        }

        function modalClose() {
            clearModalFields();
            $('#modalCreateUpdate').modal('hide');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">LookUp List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-2">Search LookUp</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList CssClass="form-control" ID="ddlSelect" runat="server">
                                                <asp:ListItem Value="LKPCode">Code</asp:ListItem>
                                                <asp:ListItem Value="LKPDesc">Description</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtSearch" runat="server" class="form-control" />
							            </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Groups
                                        </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="DDLGroup" runat="server" AutoPostBack="True" CssClass="form-control"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-2"></div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="btn btn-block btn-success" />
                                        </div>
                                    </div>
						        </div>
					        </div>
                            <div class="col-md-3">
                                <div class="form-inline">
                                    <div class="col-xs-5">
                                        <input id="btnNewGroup"  value="Create" class="btn btn-block btn-primary" onclick="modalCreate()" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <span style="display:block;height:10px"></span>
                            </div>
                           <asp:GridView ID="grdLookup" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="10" 
                                CssClass="table table-bordered table-condensed">
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
                                <RowStyle CssClass="GridOddRows" /> 
                                <AlternatingRowStyle CssClass="GridEvenRows" />          
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle"/>
                                <Columns>
                                    <asp:BoundField DataField="LKP_ID" HeaderText="LKP_ID" Visible="False" />
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code" SortExpression="LKPCode">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="code" runat="server" 
                                                Text='<%# Eval("LKPCode") %>'
                                                CommandName="GetDetails"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="hdnId" runat="server" 
                                                Value='<%#Eval("LKP_ID")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LKPDesc" SortExpression="LKPDESC" HeaderText="Description" >
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>            
                                </Columns>
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
                                <input type="text" id="txtLKPCode" runat="server" class="form-control" maxlength="10"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Description
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <input type="text" id="txtLKPDesc" class="form-control" runat="server" maxlength="50" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Group
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <asp:DropDownList ID="DDLGRP" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-block btn-primary" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="btn btn-block btn-danger" />
                        <input type="button" value="Cancel" onclick="modalClose()" class="btn btn-block btn-warning" />
                    </div>
                </div>
            </div>
        </div>

        <input id="hdnSort" type="hidden" runat="server" />
        <input type="hidden" id="lookupId" runat="server" />
    </form>
    <%--OLD--%>
    <%--<form id="frmLookup" runat="server" >
      <table id="tblLookUp" runat="server" border="0" cellpadding="1" cellspacing="1" width="75%" visible="false">
    <tr class="pageTitle"><td colspan="3" align="left" id="rowadd">LookUp Details</td></tr> 
    <tr style="height:5"></tr>
    <tr><td class="lblTitle">Code<font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td>
    <td><input type="text" id="txtLKPCode" runat="server" class="textFieldStyle" style="text-transform:uppercase" maxlength="10"  onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" /></td></tr>
    <tr><td class="lblTitle">Description<font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td>:</td>
    <td align="left"><input type="text" id="txtLKPDesc" class="textFieldStyle" runat="server" maxlength="50" /></td></tr>
        <tr><td class="lblTitle">Group<font style=" color:Red; font-size:16px"><sup> * </sup></font></td><td style="width:1%">:</td>
    <td><asp:DropDownList ID="DDLGRP" runat="server" CssClass="selectFieldStyle"></asp:DropDownList></td></tr> 
    
    <tr><td colspan="2"></td>
        <td>
            &nbsp;<br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
        </td></tr>    
       
    </table> 
      <br />
    <table width="75%" cellpadding="1" cellspacing="1">
    <tr class="pageTitle"><td colspan="3">LookUp List</td></tr>
    <tr><td class="lblTitle">Search LookUp</td><td style="width:1%">:</td>
        <td align="left"><asp:DropDownList CssClass="selectFieldStyle" ID="ddlSelect" runat="server">
            <asp:ListItem Value="LKPCode">Code</asp:ListItem>
            <asp:ListItem Value="LKPDesc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" CssClass="textFieldStyle" ID="txtSearch" Height="17px"></asp:TextBox>
            <asp:Button runat="server" Text="Go" ID="btnSearch" CssClass="goButtonStyle"/></td>
    </tr>
    <tr>
        <td class="lblTitle">Groups</td><td>:</td>
        <td><asp:DropDownList ID="DDLGroup" runat="server" AutoPostBack="True" ></asp:DropDownList></td>
        </tr>
        <tr><td colspan="3" align="right">
             <input type="hidden" runat="server" id="hdnSort" /> 
            <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp; 
            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="buttonStyle" />
            </td></tr>
    </table> 
   <br /> 
       <asp:GridView ID="grdLookup" runat="server" Width="75%" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true">
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
        <RowStyle CssClass="GridOddRows" /> 
        <AlternatingRowStyle CssClass="GridEvenRows" />          
        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle"/>
        <Columns>
            <asp:BoundField DataField="LKP_ID" HeaderText="LKP_ID" Visible="False" />
            <asp:TemplateField HeaderText=" Total ">
                <ItemTemplate>
                <asp:Label ID="lblNo" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Width="2%" />
            </asp:TemplateField>            
            <asp:HyperLinkField DataNavigateUrlFields="LKP_ID" DataNavigateUrlFormatString="frmCODLookup.aspx?id={0}&Mode=E" SortExpression="LKPCode" DataTextField= "LKPCode" HeaderText="Code" >
                <ItemStyle Width="25%" />
            </asp:HyperLinkField>
            <asp:BoundField DataField="LKPDesc" SortExpression="LKPDESC" HeaderText="Description" >
                <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>            
        </Columns>
        </asp:GridView>
       </form>--%>
</body>
</html>
