<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBCCConfiguration.aspx.vb"
    Inherits="Admin_frmBCCConfiguration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BCC Configuration [EBAST]</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: #000;
        }
        .HeaderPanel
        {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top: -5px;
            margin-bottom: 10px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-left: 8px;
            border-style: solid;
            border-color: Gray;
            border-width: 2px;
            color: White;
        }
        .HeaderGrid
        {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bold;
            color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            height: 30px;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
        }
        .oddGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .emptyrow
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        .btnSearch
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }
        .btnSearch:hover
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_1.gif);
        }
        .btnSearch:click
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_2.gif);
        }
        .warningMessage
        {
            font-family: Verdana;
            font-size: 13px;
            font-weight: bolder;
            color: Red;
            font-style: italic;
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }
        #blur
        {
            /*width: 100%;*/
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            /*height: 200%;*/
            position: absolute;
            top: 0;
            left: 0;
        }
        .fancybox-custom .fancybox-skin
        {
            box-shadow: 0 0 50px #222;
        }
        .fancybox-title-inside
        {
            text-align: center;
            font-family: verdana;
            font-size: 18px;
        }
    </style>
    
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../Scripts/SDMController.js"></script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    progress.style.width = '300px';
                    progress.style.height = '30px';
                    blur.style.height = document.documentElement.clientHeight;
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
                }
            }
        )

        function adjustSize() {
            adjustBlurScreen();
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
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="col-xs-12">
            <asp:UpdatePanel ID="UP1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">User BCC Email Configuration</h3>
                            </div>
                            <div class="box-body">
                                <div class="table table-responsive">
                                    <div class="col-xs-6">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                <asp:DropDownList ID="DdlEmailDocType_Short" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC" Text="SOAC"></asp:ListItem>
                                                    <asp:ListItem Value="ATP" Text="ATP"></asp:ListItem>
                                                    <asp:ListItem Value="QC" Text="QC"></asp:ListItem>
                                                    <asp:ListItem Value="BAUT" Text="BAUT"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC_ReadyCreation" Text="SOAC-ReadyCreation"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC_Done" Text="SOAC-DONE"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <span style="display:block; height:50px;"></span>
                                    </div>
                                    <asp:GridView ID="GvBCCConfiguration" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed"
                                        EmptyDataText="No Record Found" Width="100%" BorderColor="Black" AllowPaging="true" PageSize="15">
                                        <RowStyle CssClass="oddGrid" />
                                        <AlternatingRowStyle CssClass="evenGrid" />
                                        <HeaderStyle CssClass="HeaderGrid" />
                                        <EmptyDataRowStyle CssClass="emptyrow" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BCC Type" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEmailDocType" runat="server" Text='<%#Eval("EmailDocType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblFullname" runat="server" Text='<%#Eval("UserInfo.Fullname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEmail" runat="server" Text='<%#Eval("UserInfo.Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PhoneNo" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPhoneNo" runat="server" Text='<%#Eval("UserInfo.PhoneNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRole" runat="server" Text='<%#Eval("RoleInf.RoleName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created User" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTselApprover" runat="server" Text='<%#Eval("ModifiedUser") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Created Date" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCreatedDate" runat="server" Text='<%#Eval("CDT", "{0:dd-MMMM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Delete BCC Configuration" OnClientClick="return confirm('Are you sure you want to delete this user?');"
                                                        ImageUrl="~/images/gridview/cancel.jpg" CommandName="DeleteConfig" CommandArgument='<%#Eval("SNO") %>' Width="18px" Height="18px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="box-footer">
                                <div style="margin-top:5px;">
                                    <asp:Label ID="LblGridviewWarningMessage" runat="server" CssClass="lblText"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="box box-info">
                            <div class="box-header with-border"></div>
                            <div class="box-body">
                                <div class="table table-responsive">
                                    <div class="col-xs-6">
                                        <form id="form2" class="form-inline">
                                            <div class="form-group">
                                                <label>Email Doc Type</label>
                                                <asp:DropDownList ID="DdlEmailDocType" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC" Text="SOAC"></asp:ListItem>
                                                    <asp:ListItem Value="ATP" Text="ATP"></asp:ListItem>
                                                    <asp:ListItem Value="QC" Text="QC"></asp:ListItem>
                                                    <asp:ListItem Value="BAUT" Text="BAUT"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC_ReadyCreation" Text="SOAC-ReadyCreation"></asp:ListItem>
                                                    <asp:ListItem Value="SOAC_Done" Text="SOAC-DONE"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="TxtSearchName" runat="server" CssClass="form-control" placeholder="Search By Name"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label>Search By Role</label>
                                                <asp:DropDownList ID="DdlRole" runat="server" CssClass="form-control"> </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="LbtSearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"></asp:Button>
                                            </div>
                                        </form>
                                    </div>
                                    <asp:GridView ID="GvUsers" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="10" Width="100%">
                                        <AlternatingRowStyle CssClass="table table-bordered table-condensed" />
                                        <RowStyle CssClass="oddGrid" />
                                        <AlternatingRowStyle CssClass="evenGrid" />
                                        <HeaderStyle CssClass="HeaderGrid" />
                                        <EmptyDataRowStyle CssClass="emptyrow" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="customPagination" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add User" OnClientClick="return confirm('Are you sure you want to add this user?');"
                                                        ImageUrl="~/images/gridview/AddNewitem.jpg" CommandName="AddUser" CommandArgument='<%#Eval("UserID") %>' Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Fullname" HeaderText="User Name" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" />
                                            <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRole" runat="server" Text='<%#Eval("RoleInf.RoleName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="box-footer">
                                <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script type="text/javascript" src="../js/jquery-1.9.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../dist/js/adminlte.min.js"></script>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        USer BCC Email Configuration
    </div>
    <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div id="blur">
                <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                    <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="margin-top: 5px;">
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:DropDownList ID="DdlEmailDocType_Short" runat="server" CssClass="lblText" AutoPostBack="true">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="SOAC" Text="SOAC"></asp:ListItem>
                        <asp:ListItem Value="ATP" Text="ATP"></asp:ListItem>
                        <asp:ListItem Value="QC" Text="QC"></asp:ListItem>
                        <asp:ListItem Value="BAUT" Text="BAUT"></asp:ListItem>
                        <asp:ListItem Value="SOAC_ReadyCreation" Text="SOAC-ReadyCreation"></asp:ListItem>
                        <asp:ListItem Value="SOAC_Done" Text="SOAC-DONE"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="margin-top: 10px;">
                    <asp:GridView ID="GvBCCConfiguration" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No Record Found" Width="100%" BorderColor="Black" AllowPaging="true" PageSize="15">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <HeaderStyle CssClass="HeaderGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="BCC Type" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblEmailDocType" runat="server" Text='<%#Eval("EmailDocType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Name" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblFullname" runat="server" Text='<%#Eval("UserInfo.Fullname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblEmail" runat="server" Text='<%#Eval("UserInfo.Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PhoneNo" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblPhoneNo" runat="server" Text='<%#Eval("UserInfo.PhoneNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblRole" runat="server" Text='<%#Eval("RoleInf.RoleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created User" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblTselApprover" runat="server" Text='<%#Eval("ModifiedUser") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created Date" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="LblCreatedDate" runat="server" Text='<%#Eval("CDT","{0:dd-MMMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="Black" HeaderStyle-BorderColor="Black">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Delete BCC Configuration" OnClientClick="return confirm('Are you sure you want to delete this user?');"
                                        ImageUrl="~/images/gridview/cancel.jpg" CommandName="DeleteConfig" CommandArgument='<%#Eval("SNO") %>' Width="18px" Height="18px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="margin-top:5px;">
                        <asp:Label ID="LblGridviewWarningMessage" runat="server" CssClass="lblText"></asp:Label>
                    </div>
                    <hr />
                </div>
                <div style="margin-top: 10px; border-style: solid; border-color: Gray; border-width: 1px;
                    padding: 5px; width: 700px;">
                    <table>
                        <tr>
                            <td>
                                <span class="lblText">Email Doc Type</span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlEmailDocType" runat="server" CssClass="lblText" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="SOAC" Text="SOAC"></asp:ListItem>
                                    <asp:ListItem Value="ATP" Text="ATP"></asp:ListItem>
                                    <asp:ListItem Value="QC" Text="QC"></asp:ListItem>
                                    <asp:ListItem Value="BAUT" Text="BAUT"></asp:ListItem>
                                    <asp:ListItem Value="SOAC_ReadyCreation" Text="SOAC-ReadyCreation"></asp:ListItem>
                                    <asp:ListItem Value="SOAC_Done" Text="SOAC-DONE"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="border-style: solid; border-color: Gray; border-width: 1px; padding: 4px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <span class="lblText">Search By Name</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtSearchName" runat="server" CssClass="lblText"></asp:TextBox>
                                            </td>
                                            <td>
                                                <span class="lblText">Search By Role</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlRole" runat="server" CssClass="lblText">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LbtSearch" runat="server" Text="Search" CssClass="lblText" ForeColor="Blue"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top:5px;">
                                    <asp:GridView ID="GvUsers" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found" AllowPaging="true" PageSize="10" Width="100%">
                                        <AlternatingRowStyle CssClass="evenGrid" />
                                        <HeaderStyle CssClass="HeaderGrid" />
                                        <RowStyle CssClass="oddGrid" />
                                        <EmptyDataRowStyle CssClass="emptyrow" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAdd" runat="server" ToolTip="Add User" OnClientClick="return confirm('Are you sure you want to add this user?');"
                                                        ImageUrl="~/images/gridview/AddNewitem.jpg" CommandName="AddUser" CommandArgument='<%#Eval("UserID") %>' Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Fullname" HeaderText="User Name" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" />
                                            <asp:BoundField DataField="UserType" HeaderText="User Type" />
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRole" runat="server" Text='<%#Eval("RoleInf.RoleName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="margin-top:5px;">
                                    <asp:Label ID="LblWarningMessage" runat="server" CssClass="lblText"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>--%>
</body>
</html>
