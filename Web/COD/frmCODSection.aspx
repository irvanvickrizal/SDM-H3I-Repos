<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODSection.aspx.vb" Inherits="frmCODSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

    <script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtSec_Name").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }        
        if (msg != "")
        {
            alert("Mandatory field information : \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }
        }


        function clearModalFields() {
            $('#txtSec_Name').val("");
            $('#txtSec_Desc').val("");
            $('#btnSave').prop('value', 'Save');
            $('#btnDelete').attr("style", "display:none");
        }

        function modelCreate() {
            clearModalFields();
            $('#sectionId').val("");
            $('#modalHeader').text("Create New Section");
            $('#modalCreateUpdate').modal('show');
        }

        function modalEdit(name, address) {
            clearModalFields();
            $('#modalHeader').text("Section Details");
            $('#txtSec_Name').val(name);
            $('#txtSec_Desc').val(address);
            $('#btnSave').prop('value', 'Update');
            $('#btnDelete').attr("style", "display:block");
            $('#modalCreateUpdate').modal('show');
        }

        function modalClose() {
            clearModalFields();
            $('#modalCreateUpdate').modal('hide');
        }
    </script>
    
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
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Section</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-1">Search Section</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                              <asp:ListItem Value="Sec_Name">Name</asp:ListItem>
                                              <asp:ListItem Value="Sec_Desc">Description</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn btn-block btn-success" />
                                        </div>
							        </div>
						        </div>
					        </div>
                            
                            <div class="col-md-3">
                                <div class="form-inline">
                                    <input type="hidden" runat="server" id="Hidden1" />
                                    <div class="col-xs-5">
                                        <input id="btnNewGroup" type="button" value="Create" class="btn btn-block btn-primary" onclick="modelCreate(true)"/>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="grdSection" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="5"
                                EmptyDataText="No Records Found" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="SEC_Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="SEC_Name" runat="server" 
                                                Text='<%# Eval("SEC_Name") %>'
                                                CommandName="GetDetails"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="hdnId" runat="server" 
                                                Value='<%#Eval("SEC_Id")%>'/>
                                            <asp:HiddenField ID="hdnDesc" runat="server" 
                                                Value='<%#Eval("SEC_Desc")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                            <input type="hidden" runat="server" id="hdnSort" />
				        </div>
			        </div>
		        </div>
	        </div>
        </div>

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
                                    Name
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <input type="text" id="txtSec_Name" runat="server" class="form-control" maxlength="30"
                                    style="text-transform: uppercase; width: 316px" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" />
                            </div>
                            <div class="form-group">
                                <label>Description</label>
                                <textarea id="txtSec_Desc" runat="server" rows="5" cols="50" class="form-control"></textarea>
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

        <input type="hidden" id="sectionId" runat="server" />
  </form>
    <%--OLD--%>
  <%--<form id="form1" runat="server">
    <div>
      <table id="tblSection" runat="server" border="0" cellpadding="0" cellspacing="0"
        width="100%" visible="false">
        <tr >
          <td colspan="2" class="pageTitle">Section</td><td align="right" id="addrow" runat="server" class="pageTitleSub">Create </td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle">
            Name<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">
            :</td>
          <td>
            <input type="text" id="txtSec_Name" runat="server" class="textFieldStyle" maxlength="50" size="75" /></td>
        </tr>
        <tr>
          <td class="lblTitle" valign="top">
            Description<font style=" color:Red; font-size:16px"><sup> </sup></font></td>
          <td style="width: 1%" valign="top">
            :</td>
          <td>
            <textarea id="txtSec_Desc" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
        </tr>        
        <tr>
          <td colspan="2">
          </td>
          <td>
            &nbsp;<br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
        </tr>        
      </table>
      <br />
      <table cellpadding="1" cellspacing="0" width="100%">
        <tr class="pageTitle">
          <td colspan="2"  id="tdTitle" runat="server">Section</td><td align="right"class="pageTitleSub"> List</td>
        </tr>
        <tr>
          <td class="lblTitle">
            Search Section</td>
          <td style="width: 1%">
            :</td>
          <td>
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
              <asp:ListItem Value="Sec_Name">Name</asp:ListItem>
              <asp:ListItem Value="Sec_Desc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
        <tr>
          <td align="right" colspan="3">
            <input type="hidden" runat="server" id="hdnSort" />
            <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
            <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
            </td>
        </tr>
      </table><br />
      <asp:GridView ID="grdSection" runat="server" CellSpacing="0" CellPadding="0" AllowPaging="True"
        AllowSorting="True" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found">
        <PagerSettings Position="TopAndBottom" />
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
        <AlternatingRowStyle CssClass="GridEvenRows" />
        <RowStyle CssClass="GridOddRows" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
        <Columns>
          <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
              <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
          </asp:TemplateField>
          <asp:HyperLinkField DataTextField="SEC_Name" DataNavigateUrlFields="SEC_Id"
            DataNavigateUrlFormatString="frmCODSection.aspx?id={0}&Mode=E" HeaderText="Name"
            SortExpression="SEC_Name" />
          <asp:BoundField DataField="SEC_Desc" HeaderText="Description" SortExpression="Sec_Desc" ItemStyle-Width="25%" />
        </Columns>
      </asp:GridView>
    </div>
  </form>--%>
</body>
</html>
