<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODSubSection.aspx.vb"
  Inherits="frmCODSubSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
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
    
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

      <script language="javascript" type="text/javascript">
          function checkIsEmpty()    
        {
            var msg = "";
            if (IsEmptyCheck(document.getElementById("txtSubSec_Name").value) == false)
            {
                msg = msg + "Name should not be Empty\n";
            }       
            var e = document.getElementById("ddlSection"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Section should not be Empty\n"
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
              $('#txtSubSec_Name').val("");
              $('#txtSubSec_Desc').val("");
              $('#btnSave').prop('value', 'Save');
              $('#btnDelete').attr("style", "display:none");
              $('#ddlSection').val(0);
          }

          function modalCreate() {
              clearModalFields();
              $('#subSectionId').val("");
              $('#modalHeader').text("Create New Sub Section");
              $('#modalCreateUpdate').modal('show');
          }

          function modalEdit(code, desc, section) {
              clearModalFields();
              $('#modalHeader').text("Sub Section Details");
              $('#txtSubSec_Name').val(code);
              $('#txtSubSec_Desc').val(desc);
              $('#ddlSection').val(section);
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
				        <h3 class="box-title">Sub Section</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-2">Search Sub Section</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control">
                                              <asp:ListItem Value="SubSec_Name">Name</asp:ListItem>
                                              <asp:ListItem Value="SubSec_Desc">Description</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtSearch" runat="server" class="form-control" />
							            </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-2">
                                            Section
                                        </label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlSectionSrc" runat="server" CssClass="form-control" DataTextField="txt" DataValueField="VAL" AutoPostBack="true" />
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
                            <asp:GridView ID="grdSubSection" runat="server" AllowPaging="True" PageSize="10" CssClass="table table-bordered table-condensed"
                                AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="SUBSEC_Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="subSecName" runat="server" 
                                                Text='<%# Eval("SUBSEC_Name") %>'
                                                CommandName="GetDetails"
                                                CommandArgument='<%# Container.DisplayIndex %>'>
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="hdnId" runat="server" 
                                                Value='<%#Eval("SUBSEC_Id")%>'/>
                                            <asp:HiddenField ID="hdnDesc" runat="server" 
                                                Value='<%#Eval("SubSec_Desc")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Sec_Name" HeaderText="Section" SortExpression="Sec_Name" ItemStyle-Width="35%" />
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
                                    Name
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <input type="text" id="txtSubSec_Name" runat="server" class="form-control" maxlength="100" size="75" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Description
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <textarea id="txtSubSec_Desc" runat="server" rows="5" cols="50" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label>
                                    Section
                                    <font style=" color:Red; font-size:16px">
                                        <sup>*</sup>
                                    </font>
                                </label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" DataTextField="txt" DataValueField="VAL" />
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
        <input type="hidden" id="subSectionId" runat="server" />
    </form>
    <%--OLD--%>
  <%--<form id="form1" runat="server">
    <div>
      <table id="tblSubSection" runat="server" border="0" cellpadding="0" cellspacing="0"
        width="100%" visible="false">
        <tr >
          <td colspan="2" class="pageTitle">Sub Section</td><td id="addrow" runat="server" align="right" class="pageTitleSub">Create</td>
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
            <input type="text" id="txtSubSec_Name" runat="server" class="textFieldStyle" maxlength="100" size="75" /></td>
        </tr>
        <tr>
          <td class="lblTitle" valign="top">
            Description<font style=" color:Red; font-size:16px"><sup>&nbsp; </sup></font></td>
          <td style="width: 1%" valign="top">
            :</td>
          <td>
            <textarea id="txtSubSec_Desc" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
        </tr>
         <tr>
          <td class="lblTitle" valign="top">
            Section<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%" valign="top">
            :</td>
          <td>
            <asp:DropDownList ID="ddlSection" runat="server" CssClass="selectFieldStyle" DataTextField="txt"
              DataValueField="VAL">
            </asp:DropDownList>
          </td>
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
      <table cellpadding="1" cellspacing="1" width="100%">
        <tr class="pageTitle">
          <td colspan="2"  id="tdTitle" runat="server">Sub Section</td><td align="right"class="pageTitleSub">List </td>
        </tr>
        <tr>
          <td class="lblTitle">
            Search SubSection</td>
          <td style="width: 1%">
            :</td>
          <td>
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
              <asp:ListItem Value="SubSec_Name">Name</asp:ListItem>
              <asp:ListItem Value="SubSec_Desc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
        <tr>
          <td class="lblTitle" style="height: 21px">
            Section</td>
          <td style="width: 1%">
            :
          </td>
          <td>
            <asp:DropDownList ID="ddlSectionSrc" runat="server" CssClass="selectFieldStyle" DataTextField="txt"
              DataValueField="VAL" AutoPostBack="true">
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td align="right" colspan="3">
            <input type="hidden" runat="server" id="hdnSort" />
            <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
            <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
            </td>
        </tr>
      </table>
      <br />
      <asp:GridView ID="grdSubSection" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="True"
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
          <asp:HyperLinkField DataTextField="SUBSEC_Name" DataNavigateUrlFields="SUBSEC_Id"
            DataNavigateUrlFormatString="frmCODSubSection.aspx?id={0}&Mode=E" HeaderText="Name"
            SortExpression="SUBSEC_Name" />          
          <asp:BoundField DataField="Sec_Name" HeaderText="Section" SortExpression="Sec_Name" ItemStyle-Width="35%" />
        </Columns>
      </asp:GridView>
    </div>
  </form>--%>
</body>
</html>
