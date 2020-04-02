<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGrpUsers.aspx.vb" Inherits="frmGrpUsers" %>

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

  <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

  <script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg="";
        if (IsEmptyCheck(document.getElementById("txtGrpUsers_Name").value) == false)
        {
            msg = msg + "Name should not be Empty\n"
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
  </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-xs-12">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title" id="addrow" runat="server">Group Users Details</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
					        <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
								        <label class="col-sm-3 control-label">
                                            Distribution Group
                                            <font style=" color:Red; font-size:16px">
                                                <sup>*</sup>
                                            </font>
								        </label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlDS_Id" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
							        <div class="form-group">
								        <label class="col-sm-3 control-label">
                                            User Type
                                            Distribution Group
                                            <font style=" color:Red; font-size:16px">
                                                <sup>*</sup>
                                            </font>
								        </label>
								        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUSR_Id" runat="server" CssClass="form-control" AutoPostBack="true" />
								        </div>
							        </div>
							        <div class="form-group">
								        <div class="col-sm-3"></div>
								        <div class="col-sm-6">
                                            <asp:Button ID="btnAddUS" runat="server" CssClass="btn btn-block btn-success" Text="Attach More Users" />
								        </div>
							        </div>
						        </div>
					        </div>
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
        <div class="row">
            <div id="space" runat="server"><span style="display:none"></span></div>
	        <div class="col-sm-5" id="tblExistingUsers" runat="server">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Existing Users</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div style="margin-top: 10px; min-height: 100px; max-height: 300px; overflow-y: scroll;">
                                <asp:GridView ID="grdExistingUsers" runat="server" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="USR_Id"
                                    CssClass="table table-bordered table-condensed">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Total " Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblno" runat="Server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="*" Visible="False">
                                            <ItemTemplate>
                                                <input type="checkbox" id="chkUsers" name="chkUsers" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "USR_ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="User Name" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-3"><span style="display:none;height:50px"></span></div>
				        </div>
			        </div>
		        </div>
	        </div>
            <div class="col-sm-5" id="tblMoreUsers" runat="server">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">More Users</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div style="margin-top: 10px; min-height: 100px; max-height: 300px; overflow-y: scroll;">
                                <asp:GridView ID="grdGrpUsers" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="USR_Id"
                                    CssClass="table table-bordered table-condensed">
                                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Total ">
                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="*">
                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                            <ItemTemplate>
                                                <input type="checkbox" id="chkUsers" name="chkUsers" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "USR_ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="User Name" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-block btn-primary" />
				        </div>
			        </div>
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
  <%--<form id="form1" runat="server">
    <div>
      <table id="tblGrpUsers" runat="server" border="0" cellpadding="1" cellspacing="1"
        width="100%" visible="true">
        <tr class="pageTitle">
          <td colspan="3" id="addrow">
            Group Users Details</td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle">Distribution Group<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 11px">
            :</td>
          <td id="Td1" runat="server" class="lblText">
            <asp:DropDownList ID="ddlDS_Id" runat="server" CssClass="selectFieldStyle" Width="109px"
              AutoPostBack="true">
            </asp:DropDownList></td>
        </tr>
        <tr>
          <td class="lblTitle">User Type<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 11px">
            :</td>
          <td id="Td3" runat="server" class="lblText">
            <asp:DropDownList ID="ddlUSR_Id" runat="server" CssClass="selectFieldStyle" Width="109px"
              AutoPostBack="true">
            </asp:DropDownList></td>
        </tr>
          <tr>
              <td colspan="3" style="height: 57px">
                  <table id="tblExistingUsers" border="0" runat="server" cellpadding="0" cellspacing="0" style="width: 100%">
                      <tr class="pageTitle">
                          <td>
                              Existing Users</td>
                      </tr>
                      <tr>
                          <td style="height: 16px"><asp:GridView ID="grdExistingUsers" runat="server" CellPadding="1"
              AllowSorting="True" Width="100%" AutoGenerateColumns="False" DataKeyNames="USR_Id">
                              <PagerSettings Position="TopAndBottom" />
                              <RowStyle CssClass="GridOddRows" />
                              <Columns>
                                  <asp:TemplateField HeaderText=" Total " Visible="False">
                                      <ItemTemplate>
                                          <asp:Label ID="lblno" runat="Server"></asp:Label>
                                      </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Right" Width="1%" />
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="*" Visible="False">
                                      <ItemTemplate>
                                          <input type="checkbox" id="chkUsers" name="chkUsers" runat="server" value='<%#DataBinder.Eval(Container.DataItem,"USR_ID") %>' />
                                      </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Right" Width="1%" />
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Name" HeaderText="User Name" />
                              </Columns>
                              <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                              <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridEvenRows" />
                          </asp:GridView>
                              &nbsp;</td>
                      </tr>
                  </table>
                              <asp:Button ID="btnAddUS" runat="server" CssClass="buttonStyle" Text="Attach More Users"
                                  Width="158px" /><br />
                  <br />
                  <table runat="server" id="tblMoreUsers" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                      <tr class="pageTitle">
                          <td>
                              More Users</td>
                      </tr>
                      <tr>
                          <td><asp:GridView ID="grdGrpUsers" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="False"
              AllowSorting="True" Width="100%" AutoGenerateColumns="False" DataKeyNames="USR_Id">
                              <PagerSettings Position="TopAndBottom" />
                              <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                              <AlternatingRowStyle CssClass="GridEvenRows" />
                              <RowStyle CssClass="GridOddRows" />
                              <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                              <Columns>
                                  <asp:TemplateField HeaderText=" Total ">
                                      <ItemStyle HorizontalAlign="Right" Width="1%" />
                                      <ItemTemplate>
                                          <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="*">
                                      <ItemStyle HorizontalAlign="Right" Width="1%" />
                                      <ItemTemplate>
                                          <input type="checkbox" id="chkUsers" name="chkUsers" runat="server" value='<%#DataBinder.Eval(Container.DataItem,"USR_ID") %>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Name" HeaderText="User Name" />
                              </Columns>
                          </asp:GridView>
                          </td>
                      </tr>
                      <tr>
                          <td align="center">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
                      </tr>
                  </table>
              </td>
          </tr>
        <tr style="height: 5">
          <td colspan="3"><br />
              &nbsp;</td>
        </tr>
        <tr>
          <td colspan="2">
          </td>
          <td align="center" >
            <br />
            </td>
        </tr>
      </table>
    </div>
  </form>--%>
</body>
</html>
