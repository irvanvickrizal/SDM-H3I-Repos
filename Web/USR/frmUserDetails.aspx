<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserDetails.aspx.vb"
    Inherits="USR_frmUserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
    <script language="javascript" type="text/javascript">
        function viewUser()
        {
            var uid=document.getElementById("hdnUsrId").value;
            window.open('frmchangeRole.aspx?id='+ uid,'','width=800;height=600;scrollbars=yes;statusbar=no');
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
				        <h3 class="box-title">User Details</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-3"><span style="display:none"></span></div>
                            <div class="col-md-6">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-3 control-label">User Type</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" id="lblUser" CssClass="form-control" BackColor="#cccccc" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Name</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" id="lblName" CssClass="form-control" BackColor="#cccccc" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Login ID</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" id="lblLogin" CssClass="form-control" BackColor="#cccccc" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Email</label>
                                        <div class="col-sm-6">
                                            <asp:Label runat="server" ID="lblEmail" class="form-control" BackColor="#cccccc" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Sign Title</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="TxtTitle" runat="server" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="BtnUpdateTitle" runat="server" Text="Update" CssClass="btn btn-block btn-success" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Phone Number</label>
                                        <div class="col-sm-6">
                                            <input id="txtPhnumber" runat="server" class="form-control" type="text" maxlength="20" onkeypress="javascript:return allowKeyAcceptsData('0123456789-');" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-block btn-success" Text="Update" />
                                        </div>
							        </div>
                                    <div class="form-group" id="trstatus" runat="server">
                                        <label class="col-sm-3 control-label">Status</label>
                                        <div class="col-sm-6">
                                            <asp:RadioButtonList ID="rblistApproved" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="0">Rejected</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-block btn-success" Text="Update" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Account Status</label>
                                        <div class="col-sm-6">
                                            <asp:RadioButtonList ID="rblistStatus" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="A">Active</asp:ListItem>
                                                <asp:ListItem Value="I">Inactive</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-block btn-success" Text="Update" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <asp:Button ID="btnPwdreset" runat="server" CssClass="btn btn-block btn-primary" Text="Reset Password" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnPwdresetEBOQ" runat="server" CssClass="btn btn-block btn-primary" Text="Reset E-BOQ Password"/>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnPwdResetEMORE" runat="server" CssClass="btn btn-block btn-primary" Text="Reset EMORE Password"/>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-block btn-danger" Text="Delete" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-block btn-warning" Text="Cancel" />
                                        </div>
                                    </div>
						        </div>
					        </div>
                            <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" PageSize="10" Width="100%" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle CssClass="customPagination" HorizontalAlign="Right" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ROLEDESC" HeaderText="Role" />
                                    <asp:BoundField DataField="AREA" HeaderText="Area" />
                                    <asp:BoundField DataField="REGION" HeaderText="Region" />
                                    <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                    <asp:BoundField DataField="SITE" HeaderText="Site" />
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
			        <input type="hidden" runat="server" id="hdnUsrId" />
		        </div>
	        </div>
        </div>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
        <div style="width: 100%" id="divWidth">
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd">
                        User Details</td>
                </tr>
                <tr id="lblUsertype" runat="Server">
                    <td class="lblTitle" style="width: 15%">
                        User Type</td>
                    <td style="width: 1%">
                        :</td>
                    <td id="lblUser" runat="Server" colspan="2">
                        <asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="selectFieldStyle">
                        </asp:DropDownList>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Name</td>
                    <td style="width: 1%">
                        :</td>
                    <td id="lblName" runat="Server" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Login ID</td>
                    <td style="width: 1%">
                        :</td>
                    <td id="lblLogin" runat="Server" colspan="2">
                    </td>
                </tr>--%>
               <%-- <tr visible="false">
                    <td class="lblTitle">
                        Role1</td>
                    <td style="width: 1%">
                        :</td>
                    <td id="lblRole" runat="Server" colspan="2">
                        <asp:DropDownList ID="ddlRole" runat="Server" CssClass="selectFieldStyle">
                        </asp:DropDownList></td>
                </tr>--%>
                <%--<tr>
                    <td class="lblTitle">
                        E-mail</td>
                    <td style="width: 1%">
                        :</td>
                    <td id="lblEmail" runat="Server" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Sign Title
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTitle" runat="server" CssClass="textFieldStyle" Width="229px" MaxLength="60"></asp:TextBox>
                        <asp:Button ID="BtnUpdateTitle" runat="server" Text="Update" CssClass="buttonStyle" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="height: 18px">
                        Phone Number</td>
                    <td style="width: 1%; height: 18px;">
                        :</td>
                 <td> <input id="txtPhnumber" runat="server" class="textFieldStyle" type="text" maxlength="20" onkeypress="javascript:return allowKeyAcceptsData('0123456789-');" style="width: 229px" />
                     <asp:Button ID="Button3" runat="server" CssClass="buttonStyle" Text="Update" /></td>
                                  
                </tr>
                <tr id="trstatus" runat="Server">
                    <td class="lblTitle">
                        Status</td>
                    <td style="width: 1%">
                        :</td>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rblistApproved" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="0">Rejected</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button2" runat="server" CssClass="buttonStyle" Text="Update" /></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Account Status</td>
                    <td style="width: 1%">
                        :</td>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rblistStatus" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="A">Active</asp:ListItem>
                            <asp:ListItem Value="I">Inactive</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" CssClass="buttonStyle" Text="Update" /></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td colspan="2">
                        <br />
                        <asp:Button ID="btnPwdreset" runat="server" CssClass="buttonStyle" Text="Reset Password"
                            Width="13%" />&nbsp;
						<asp:Button ID="btnPwdresetEBOQ" runat="server" CssClass="buttonStyle" Text="Reset E-BOQ Password"
                            Width="16%" />&nbsp;
                        <asp:Button ID="btnDelete" runat="server" CssClass="buttonStyle" Text="Delete" />
                        <asp:Button ID="BtnCancel" runat="server" CssClass="buttonStyle" Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 18px">
                        &nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" PageSize="5" Width="100%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ROLEDESC" HeaderText="Role" />
                                <asp:BoundField DataField="AREA" HeaderText="Area" />
                                <asp:BoundField DataField="REGION" HeaderText="Region" />
                                <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                <asp:BoundField DataField="SITE" HeaderText="Site" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <input type="hidden" runat="server" id="hdnUsrId" />
        </div>

    <script type="text/javascript">
        if(screen.width==1440)
        {
     
            document.getElementById("divWidth").style.width=screen.width-(232+416);
        }
        else if(screen.width==1024)
        {
     
            document.getElementById("divWidth").style.width=screen.width-(257+64);
        }
        else
        {
            document.getElementById("divWidth").style.width=screen.width-230;

        }
    </script>

    </form>--%>
</body>
</html>
