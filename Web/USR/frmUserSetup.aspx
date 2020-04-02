<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserSetup.aspx.vb" Inherits="USR_frmUserSetup" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>User Setup</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript" src="../Include/Validation.js">
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
        <style type="text/css">
            .disabledColumnForm {
                pointer-events: none;
                opacity: 0.4;
            }
        </style>
    </head>
    <script language="javascript" type="text/javascript">
        function checkIsEmpty(){
            var msg = "";
            var f = document.getElementById("ddlUsertype");
            var strUser1 = f.options[f.selectedIndex].value;
            if (strUser1 != 1) {
                var g = document.getElementById("ddlSelect");
                var strUser2 = g.options[g.selectedIndex].value;
                if (strUser2 == 0) {
                    msg = msg + "User Type Name Should be Select\n";
                }
            }
            if (IsEmptyCheck(document.getElementById("txtName").value) == false) {
                msg = msg + "Name should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtLodinId").value) == false) {
                msg = msg + "LoginID should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtEmail").value) == false) {
                msg = msg + "Email should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtEmail").value) == true) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var address = document.getElementById("txtEmail").value;
                if (reg.test(address) == false) {
                    msg = msg + "Invalid Email Address\n";
                }
            }
            var e = document.getElementById("ddlRole1");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Role should be select\n";
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }
        
        function checkEmail(){
            var msg = "";
            if (IsEmptyCheck(document.getElementById("txtLodinId").value) == false) {
                msg = msg + "Login ID should not be Empty\n";
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }
        
        function checkRole(){
            var msg = "";
            var e = document.getElementById("ddlJava");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Java should be select\n";
            }
            else {
                var e = document.getElementById("ddlArea");
                var strUser = e.options[e.selectedIndex].value;
                if (strUser == 0) {
                    msg = msg + "Area should be select\n";
                }
                else {
                    var e = document.getElementById("ddlRegion");
                    var strUser = e.options[e.selectedIndex].value;
                    if (strUser == 0) {
                        msg = msg + "Region should be select\n";
                    }
                    else {
                    
                        var e = document.getElementById("ddlZone");
                        var strUser = e.options[e.selectedIndex].value;
                        if (strUser == 0) {
                            msg = msg + "Zone should be select\n";
                        }
                        else {
                            var e = document.getElementById("ddlSite");
                            var strUser = e.options[e.selectedIndex].value;
                            if (document.getElementById("trSite").style.display == "none") {
                                msg = "";
                            }
                            else {
                                if (strUser == 0) {
                                    msg = msg + "Site should be select\n";
                                }
                            }
                        }
                    }
                }
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-6" runat="server" id="tblSetup">
                            <div class="box box-info">
			                    <div class="box-header with-border">
				                    <h3 class="box-title">User Setup</h3>
			                    </div>
			                    <div class="box-body">
				                    <div class="table table-responsive">
						                <div class="form-horizontal">
							                    <div class="form-group">
                                                    <label class="col-sm-3 control-label">User Type</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlUsertype" runat="Server" AutoPostBack="True" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" id="name">
                                                    <label runat="server" ID="lblName" class="col-sm-3 control-label"></label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlSelect" runat="Server" CssClass="form-control" AutoPostBack="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Name
                                                        <font style=" color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <input id="txtName" runat="server" type="text" class="form-control" maxlength="30"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Title</label>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="TxtTitle" runat="server" CssClass="form-control" MaxLength="60" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Login ID 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <input id="txtLodinId" runat="Server" type="text" class="form-control" maxlength="20"/>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btnCheck" runat="Server" Text="Check For Availability" ForeColor="orange" />
                                                    </div>
                                                </div>
                                                <div class="form-group" id="lblStatusForm" runat="server">
                                                    <div class="col-sm-3"></div>
                                                    <div class="col-sm-6">
                                                        <label ID="lblStatus" runat="server" style="color:red" EnableViewState="False" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">DG Sign Required</label>
                                                    <div class="col-sm-6">
                                                        <asp:RadioButtonList ID="rblsign" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                            <asp:ListItem Value="0">
                                                                Yes
                                                            </asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="1">
                                                                No
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        E-mail 
                                                        <font style=" color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <input id="txtEmail" runat="server" type="text" class="form-control" maxlength="50" size="50" 
                                                            onkeypress="javascript:return allowKeyAcceptsData('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@_');"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Phone Number</label>
                                                    <div class="col-sm-6">
                                                        <input id="txtPhoneOff" runat="server" type="text" class="form-control" maxlength="20" onkeypress="javascript:return allowKeyAcceptsData('0123456789-');"/>
                                                    </div>
                                                </div>
                                                <div class="form-group" runat="server" id="trLevel">
                                                    <label class="col-sm-3 control-label">
                                                        Level 
                                                        <font style=" color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlLevel" runat="Server" AutoPostBack="True" CssClass ="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Role 
                                                        <font style=" color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlRole1" runat="server" AutoPostBack="True" CssClass ="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3"></div>
                                                    <div class="col-sm-3">
                                                        <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-block btn-primary" Text="Proceed" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-block btn-danger" Text="Cancel"/>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-6" id="tblRole" runat="server">
                            <div class="box box-info">
			                    <div class="box-header with-border">
				                    <h3 class="box-title">User Role Setup</h3>
			                    </div>
			                    <div class="box-body">
				                    <div class="table table-responsive">
						                <div class="form-horizontal">
							                <div class="form-group">
								                <label class="col-sm-3 control-label">
                                                    Level 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlLvl" runat="server" AutoPostBack="True" CssClass ="form-control" />
								                </div>
							                </div>
                                            <div class="form-group">
								                <label class="col-sm-3 control-label">
                                                    Role 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" CssClass ="form-control" />
								                </div>
							                </div>
                                            <div class="form-group" runat="server" id="trJava">
								                <label class="col-sm-3 control-label">
                                                    Java 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlJava" runat="server" CssClass="form-control" AutoPostBack="True" />
								                </div>
							                </div>
                                            <div class="form-group" runat="server" id="trArea">
								                <label class="col-sm-3 control-label">
                                                    Area 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlArea" runat="Server" CssClass="form-control" AutoPostBack="True" />
								                </div>
							                </div>
                                            <div class="form-group" runat="server" id="trRegion">
								                <label class="col-sm-3 control-label">
                                                    Region 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlRegion" runat="Server" CssClass="form-control" AutoPostBack="True" DataTextField="--Select--" DataValueField="0" />
								                </div>
							                </div>
                                            <div class="form-group" id="trZone" runat="server">
								                <label class="col-sm-3 control-label">
                                                    Zone 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlZone" runat="Server" CssClass="form-control" AutoPostBack="True" DataTextField="--Select--" DataValueField="0" />
								                </div>
							                </div>
                                            <div class="form-group" runat="server" id="trSite">
								                <label class="col-sm-3 control-label">
                                                    Site 
                                                    <font style=" color:Red; font-size:16px">
                                                        <sup>*</sup>
                                                    </font>
								                </label>
								                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSite" runat="Server" CssClass="form-control" DataTextField="--Select--" DataValueField="0" />
								                </div>
							                </div>
                                            <div class="form-group">
								                <div class="col-sm-3"></div>
								                <div class="col-sm-3">
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-block btn-primary" Text="Add" />
								                </div>
                                                <div class="col-sm-3">
                                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-danger" Text="Back" />
								                </div>
							                </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="USR_ID" 
                                        EmptyDataText="No Records Found" PageSize="5" CssClass="table table-bordered table-condensed">
                                        <PagerSettings Position="TopAndBottom" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <PagerStyle CssClass="customPagination" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="Server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JAVA" HeaderText="Java"/><asp:BoundField DataField="AREA" HeaderText="Area"/><asp:BoundField DataField="REGION" HeaderText="Region"/><asp:BoundField DataField="ZONE" HeaderText="Zone"/><asp:BoundField DataField="SITE" HeaderText="Site"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <input id="hdnID" runat="Server" type="hidden" />
                    <input id="hdnUID" runat="Server" type="hidden" />
                    <input id="hdnSRCID" runat="Server" type="hidden" />
                    <input id="isUserSetupAble" value="yes" runat="Server" type="hidden" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        
        <script language="javascript" type="text/javascript" src="../js/jquery-1.9.min.js"></script>
        <script type="text/javascript" src="../bootstrap/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="../Scripts/SDMController.js"></script>
        <%--OLD--%>
        <%--<form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="width:100%" id="divWidth">
                        <table id="tblSetup" runat="Server" cellpadding="1" border="0" cellspacing="1" width="100%">
                            <tr>
                                <td class="pageTitle" colspan="3" id="rowadd">
                                    User Setup
                                </td>
                            </tr>
                            <tr id="lblUsertype" runat="Server">
                                <td class="lblTitle">
                                    User Type
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUsertype" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="name" runat="Server">
                                <td id="lblName" runat="Server" class="lblTitle">
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSelect" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Name 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <input id="txtName" runat="server" type="text" class="textFieldStyle" maxlength="30"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Title
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTitle" runat="server" CssClass="textFieldStyle" MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Login ID 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <input id="txtLodinId" runat="Server" type="text" class="textFieldStyle" maxlength="20"/>&nbsp;
                                    <asp:LinkButton ID="btnCheck" runat="Server" Text="Check For Availability" ForeColor="orange">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" EnableViewState="False">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    DG Sign Required
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rblsign" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                        <asp:ListItem Value="0">
                                            Yes
                                        </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">
                                            No
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    E-mail 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <input id="txtEmail" runat="server" type="text" class="textFieldStyle" maxlength="50" size="50" onkeypress="javascript:return allowKeyAcceptsData('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@_');"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Phone Number
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <input id="txtPhoneOff" runat="server" type="text" class="textFieldStyle" maxlength="20" onkeypress="javascript:return allowKeyAcceptsData('0123456789-');"/>
                                </td>
                            </tr>
                            <tr id="trLevel" runat="server">
                                <td class="lblTitle">
                                    Level 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLevel" runat="Server" AutoPostBack="True" CssClass ="selectFieldStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="Tr1" runat="server">
                                <td class="lblTitle">
                                    Role 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 4px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRole1" runat="server" AutoPostBack="True" CssClass ="selectFieldStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 4px">
                                </td>
                                <td>
                                    <br/>
                                    <asp:Button ID="BtnSave" runat="server" CssClass="buttonStyle" Text="Proceed" /><asp:Button ID="BtnCancel" runat="server" CssClass="buttonStyle" Text="Cancel"/>
                                </td>
                            </tr>
                        </table>
                        <table id="tblRole" runat="Server" cellpadding="1" border="0" cellspacing="1" style="width:100%">
                            <tr>
                                <td class="pageTitle" colspan="3" id="Td1">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Level 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLvl" runat="server" AutoPostBack="True" CssClass ="selectFieldStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Role 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" CssClass ="selectFieldStyle">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trJava" runat="Server">
                                <td class="lblTitle">
                                    Java 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJava" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trArea" runat="Server">
                                <td class="lblTitle">
                                    Area 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlArea" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trRegion" runat="Server">
                                <td class="lblTitle">
                                    Region 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRegion" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True" DataTextField="--Select--" DataValueField="0">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trZone" runat="Server">
                                <td class="lblTitle">
                                    Zone 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlZone" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True" DataTextField="--Select--" DataValueField="0">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trSite" runat="Server">
                                <td class="lblTitle">
                                    Site 
                                    <font style=" color:Red; font-size:16px">
                                        <sup>
                                            * 
                                        </sup>
                                    </font>
                                </td>
                                <td style="width: 1px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSite" runat="Server" CssClass="selectFieldStyle" DataTextField="--Select--" DataValueField="0">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 1px">
                                </td>
                                <td>
                                    <br/>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="buttonStyle" Text="Add" /><asp:Button ID="btnBack" runat="server" CssClass="buttonStyle" Text="Back" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="USR_ID" EmptyDataText="No Records Found" PageSize="5" Width="100%">
                                        <PagerSettings Position="TopAndBottom" /><HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" /><AlternatingRowStyle CssClass="GridEvenRows" /><RowStyle CssClass="GridOddRows" /><PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="Server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JAVA" HeaderText="Java"/><asp:BoundField DataField="AREA" HeaderText="Area"/><asp:BoundField DataField="REGION" HeaderText="Region"/><asp:BoundField DataField="ZONE" HeaderText="Zone"/><asp:BoundField DataField="SITE" HeaderText="Site"/>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table><input id="hdnID" runat="Server" type="hidden" /><input id="hdnUID" runat="Server" type="hidden" /><input id="hdnSRCID" runat="Server" type="hidden" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <script type="text/javascript">
                if (screen.width == 1440) {
                
                    document.getElementById("divWidth").style.width = screen.width - (232 + 416);
                }
                else 
                    if (screen.width == 1024) {
                    
                        document.getElementById("divWidth").style.width = screen.width - (257 + 64);
                    }
                    else {
                        document.getElementById("divWidth").style.width = screen.width - 230;
                        
                    }
            </script>
        </form>--%>
    </body>
</html>
