<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserList.aspx.vb" Inherits="USR_frmUserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User List</title>
    <base target="_self" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>

    <link type="text/css" rel="Stylesheet" href="../CSS/css/smoothness/jquery-ui-1.8.17.custom.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('a#popup').live('click', function (e) {
                
                var page = $(this).attr("href")
                var titleDoc = $(this).attr("title")
                var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="' + page + '" scrolling="AUTO" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 250,
                    width: 300,
                    title: titleDoc,
                     buttons: {
                        "Close": function () { $dialog.dialog('close'); }
                                },
                    close: function (event, ui) {
                        
                       //__doPostBack('<%= btnRefresh.ClientID %>', '');
                    }
                });
                $dialog.dialog('open');
                e.preventDefault();
            });
        });
        
        function modelOpen(userName) {
            $('#modalHeader').text(userName);
            $('#modalScope').modal('show');
        }

        function modalClose() {
            $('#modalScope').modal('hide');
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
				        <h3 class="box-title">User List</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
                            <div class="col-md-2">
                                <span style="display:none;"></span>
                            </div>
					        <div class="col-md-7">
						        <div class="form-horizontal">
							        <div class="form-group">
                                        <label class="col-sm-3 control-label">Type</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="form-control" AutoPostBack="True" />
                                        </div>
							        </div>
                                    <div class="form-group" runat="server" id="lblsrcname">
                                        <label class="col-sm-3 control-label">Name</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlSrcname" runat="Server" CssClass="form-control" AutoPostBack="True" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Search by Role</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="True" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Search by  User</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlSelect" runat="Server" CssClass="form-control">
                                                <asp:ListItem Value="[NAME]">Name</asp:ListItem>
                                                <asp:ListItem Value="GRPDESC">User Type</asp:ListItem>
                                                <asp:ListItem Value="USRLOGIN">Login ID</asp:ListItem>
                                                <asp:ListItem Value="EMAIL">Email</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <input id="txtSearch" runat="server" type="text" class="form-control" maxlength="30"
                                                onkeypress="javascript:return allowKeyAcceptsData('.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" placeholder="Enter Text" />
                                        </div>
							        </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <span style="display:none"></span>
                                        </div>
                                        <div class="col-sm-6" >
                                            <asp:Button ID="btnGo" runat="Server" Text="GO" CssClass="btn btn-block btn-success" />
                                        </div>
                                    </div>
						        </div>
					        </div>
                            <div class="col-lg-7">
                                <span style="display:none"></span>
                            </div>
                            <div class="col-md-5">
                                <div class="form-inline pull-right">
                                    <div class="form-group">
                                        <input type="button" runat="server" id="btnCreate" value="Create New User" class="btn btn-block btn-primary" />
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="grdUserlist" runat="server" AllowPaging="True" AllowSorting="True"
                                 DataKeyNames="USR_ID" AutoGenerateColumns="False" PageSize="10" EmptyDataText="No Records Found" CssClass="table table-bordered table-condensed">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmUserDetails.aspx?id={0}"
                                        HeaderText="Name" SortExpression="NAME">
                                        <ItemStyle Width="25%" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmChangeUsrDetails.aspx?id={0}"
                                        HeaderText="Name" SortExpression="NAME">
                                        <ItemStyle Width="25%" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID,Name" DataNavigateUrlFormatString="frmUserList.aspx?id={0}&SS={1}&SelMode=True"
                                        HeaderText="Name" SortExpression="NAME">
                                        <ItemStyle Width="25%" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="GRPDESC" HeaderText="User Type" SortExpression="GRPDESC" />
                                    <asp:BoundField DataField="USRTYPE" HeaderText="CSName" SortExpression="USRTYPE" />
                                    <asp:BoundField DataField="ROLEDESC" HeaderText="Role" SortExpression="ROLEDESC" />
                                    <asp:BoundField DataField="USRLOGIN" HeaderText="Login ID" SortExpression="USRLOGIN" />
                                    <asp:BoundField DataField="usrpassword" HeaderText="Password" SortExpression="usrpassword" />
                                    <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />
                                    <asp:BoundField DataField="Approved" HeaderText="Status" SortExpression="Approved" />
                                    <asp:BoundField DataField="Acc_Status" HeaderText="Account Status" SortExpression="Acc_Status" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%--<a id="popup" href='frmUsrScopeAccess.aspx?id=<%#Eval("USR_ID") %>' title='<%#Eval("Name") %>'
                                                style="text-decoration: none; border: none;">
                                                <img src="../Images/folder_locked.png" id="iconAuth" alt="authIcon" width="20"
                                                    height="20" style="text-decoration: none; border: none;" />
                                            </a>--%>
                                            <asp:HiddenField ID="hiddenId" Value='<%#Eval("USR_ID")%>' runat="server"/>
                                            <asp:ImageButton ID="ImgSelect" runat="server" ImageUrl="../Images/folder_locked.png" height="20" style="text-decoration: none; border: none;" alt="authIcon" 
                                                width="20" CommandName="ScopeUser" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
				        </div>
			        </div>
			        <div class="box-footer">
                        <input id="hdnSort" type="hidden" runat="server" />
                        <asp:Button ID="btnRefresh" runat="server" Style="display: none" OnClick="btnRefresh_Click" />
			        </div>
		        </div>
	        </div>
        </div>

        <%--Modal Scope User--%>
        <div class="modal fade" id="modalScope" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label id="modalHeader"></label>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-3">
                            <span style="display:none"></span>
                        </div>
                        <div class="col-md-6">
                            <asp:HiddenField ID="modalUsrId" runat="server"/>
                            <div class="form-group">
                                <asp:CheckBox ID="ChkTIScope" runat="server" Text="TI Scope" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="ChkCMEScope" runat="server" Text="CME Scope" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="ChkSISScope" runat="server" Text="SIS Scope" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="ChkSitacScope" runat="server" Text="Sitac Scope" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="LbtSave" runat="server" Text="Update Scope Access" CssClass="btn btn-block btn-success"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <input type="button" value="Close" onclick="modalClose()" class="btn btn-block btn-danger" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <%--OLD--%>
    <%--<form id="form2" runat="server">
        <div style="width: 100%" id="divWidth">
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd">
                        User List</td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 18%">
                        Type</td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="lblsrcname" runat="Server">
                    <td class="lblTitle">
                        Name</td>
                    <td style="width: 4px">
                        :</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Search by Role</td>
                    <td style="width: 4px">
                        :</td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Search User</td>
                    <td style="width: 4px">
                        :</td>
                    <td>
                        <asp:DropDownList ID="DropDownList4" runat="Server" CssClass="selectFieldStyle">
                            <asp:ListItem Value="[NAME]">Name</asp:ListItem>
                            <asp:ListItem Value="GRPDESC">User Type</asp:ListItem>
                            <asp:ListItem Value="USRLOGIN">Login ID</asp:ListItem>
                            <asp:ListItem Value="EMAIL">Email</asp:ListItem>
                        </asp:DropDownList>
                        <input id="Text1" runat="server" type="text" class="textFieldStyle" maxlength="30"
                            onkeypress="javascript:return allowKeyAcceptsData('.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" />&nbsp;<asp:Button
                                ID="Button1" runat="Server" Text="GO" CssClass="goButtonStyle" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <input type="button" runat="server" id="btnCreate" value="Create" class="buttonStyle" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                            Width="100%" DataKeyNames="USR_ID" AutoGenerateColumns="False" PageSize="5" EmptyDataText="No Records Found">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmUserDetails.aspx?id={0}"
                                    HeaderText="Name" SortExpression="NAME">
                                    <ItemStyle Width="25%" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmChangeUsrDetails.aspx?id={0}"
                                    HeaderText="Name" SortExpression="NAME">
                                    <ItemStyle Width="25%" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID,Name" DataNavigateUrlFormatString="frmUserList.aspx?id={0}&SS={1}&SelMode=True"
                                    HeaderText="Name" SortExpression="NAME">
                                    <ItemStyle Width="25%" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="GRPDESC" HeaderText="User Type" SortExpression="GRPDESC" />
                                <asp:BoundField DataField="USRTYPE" HeaderText="CSName" SortExpression="USRTYPE" />
                                <asp:BoundField DataField="ROLEDESC" HeaderText="Role" SortExpression="ROLEDESC" />
                                <asp:BoundField DataField="USRLOGIN" HeaderText="Login ID" SortExpression="USRLOGIN" />
                                <asp:BoundField DataField="usrpassword" HeaderText="Password" SortExpression="usrpassword" />
                                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />
                                <asp:BoundField DataField="Approved" HeaderText="Status" SortExpression="Approved" />
                                <asp:BoundField DataField="Acc_Status" HeaderText="Account Status" SortExpression="Acc_Status" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a id="popup" href='frmUsrScopeAccess.aspx?id=<%#Eval("USR_ID") %>' title='<%#Eval("Name") %>'
                                            style="text-decoration: none; border: none;">
                                            <img src="../Images/folder_locked.png" id="iconAuth" alt="authIcon" width="20"
                                                height="20" style="text-decoration: none; border: none;" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <input id="Hidden1" type="hidden" runat="server" />
            <asp:Button ID="Button2" runat="server" Style="display: none" OnClick="btnRefresh_Click" />
        </div>

        <script type="text/javascript">
    /*if(screen.width==1440)
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

    }*/
        </script>

    </form>--%>
</body>
</html>
