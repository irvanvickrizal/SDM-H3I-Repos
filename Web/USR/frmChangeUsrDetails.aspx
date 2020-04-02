<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeUsrDetails.aspx.vb" Inherits="USR_frmChangeUsrDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
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
        function checkIsSetupEmpty()
        {
            var msg="";
            var e = document.getElementById("ddlLevel"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Level should be select\n"
            }
            var e = document.getElementById("ddlRole1"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Role should be select\n"
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

        function checkIsEmpty()
        {
            var msg="";
            var e = document.getElementById("ddlJava"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Java should be select\n"
            }
            else
            {
             var e = document.getElementById("ddlArea"); 
             var strUser = e.options[e.selectedIndex].value;
             if (strUser == 0)
             {
                 msg = msg + "Area should be select\n"
             }
             else
             {
                 var e = document.getElementById("ddlRegion"); 
                 var strUser = e.options[e.selectedIndex].value;
                 if (strUser == 0)
                 {
                     msg = msg + "Region should be select\n"
                 }
                 else
                 {
                  
                     var e = document.getElementById("ddlZone"); 
                     var strUser = e.options[e.selectedIndex].value;
                     if (strUser == 0)
                     {
                         msg = msg + "Zone should be select\n"
                     }
                     else
                     {
                        var e = document.getElementById("ddlSite"); 
                            var strUser = e.options[e.selectedIndex].value;
                             if(document.getElementById("trSite").style.display=="none")
                             {
                                msg="";
                             }
                             else
                             {
                                if (strUser == 0)
                                {
                                msg = msg + "Site should be select\n"
                                }
                            }
                        }
                    }
                }
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
    </script>
<body>
    <form id="form1" runat="server">
        <div class="row">
	        <div class="col-md-6" runat="server" id="tblSetup">
		        <div class="box box-info">
			        <div class="box-header with-border">
				        <h3 class="box-title">Change Role</h3>
			        </div>
			        <div class="box-body">
				        <div class="table table-responsive">
						    <div class="form-horizontal">
							    <div class="form-group">
								    <label class="col-sm-3 control-label">User Type</label>
								    <div class="col-sm-6">
                                        <div ID="lblUser" runat="Server" class="form-control" style="background-color:#cccccc">
                                            <asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="form-control" />
                                        </div>
								    </div>
							    </div>
							    <div class="form-group">
								    <label class="col-sm-3 control-label">Name</label>
								    <div class="col-sm-6">
                                        <label runat="server" id="lblName" class="form-control" style="background-color:#cccccc" />
								    </div>
							    </div>
							    <div class="form-group">
								    <label class="col-sm-3 control-label">Login ID</label>
								    <div class="col-sm-6">
                                        <label runat="server" id="lblLogin" class="form-control" style="background-color:#cccccc" />
								    </div>
							    </div>
                                <div class="form-group">
								    <label class="col-sm-3 control-label">
                                        Level
                                        <font style=" color:Red; font-size:16px">
                                            <sup>*</sup>
                                        </font>
								    </label>
								    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlLevel" runat="Server" AutoPostBack="True" CssClass="form-control" />
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
                                        <asp:DropDownList ID="ddlRole1" runat="Server" CssClass="form-control" />
								    </div>
							    </div>
							    <div class="form-group">
								    <div class="col-sm-3"></div>
								    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblEdit" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="R">Edit Role</asp:ListItem>
                                            <asp:ListItem Value="P">Edit Previlege</asp:ListItem>
                                        </asp:RadioButtonList>
								    </div>
							    </div>
                                <div class="form-group" runat="server" id="trTask">
								    <label class="col-sm-3 control-label"></label>
								    <div class="col-sm-3">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-primary" Text="Save"/>
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
				        <h3 class="box-title">Edit Privilage</h3>
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
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-block btn-primary" Text="Update" />
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
                    <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True" CssClass="table table-bordered table-condensed"
                        AutoGenerateColumns="False" DataKeyNames="sno" EmptyDataText="No Records Found" PageSize="5">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="customPagination" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText=" Total ">
                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblno" runat="Server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="JAVA" HeaderText="Java" />
                            <asp:BoundField DataField="AREA" HeaderText="Area" />
                            <asp:BoundField DataField="REGION" HeaderText="Region" />
                            <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                            <asp:BoundField DataField="SITE" HeaderText="Site" />
                        <asp:BoundField DataField="sno" HeaderText=" Total " />
                            <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" 
                                    CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete');"
                                    Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
		        </div>
	        </div>
        </div>
        <input type="hidden" runat="server" id="hdnUId"/>
        <input type="hidden" runat="server" id="hdnId"/>
        <input type="hidden" runat="server" id="hdnRole"/>
        <input type="hidden" runat="server" id="hdnUsertype"/>
        <input type="hidden" runat="server" id="hdnSRCID"/>
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <div style="width:100%" id="divWidth">
    <table id="tblSetup" runat="Server" cellpadding="1" border="0" cellspacing="1" width="100%" >
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd">Change Role</td>
            </tr>
            <tr id="lblUsertype" runat="Server">
                <td class="lblTitle">User Type</td>
                <td style="width: 1%">:</td>
                <td id="lblUser" runat="Server" colspan="2"  class="lblText"><asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="selectFieldStyle"></asp:DropDownList>&nbsp;                
                </td>
            </tr>
            <tr>
                <td class="lblTitle" style="height: 20px">Name</td>
                <td style="width: 1%; height: 20px;">:</td>
                <td id="lblName" runat="Server" colspan="2" style="width: 563px; height: 20px;"  class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle" style="height: 18px">Login ID</td>
                <td style="width: 1%; height: 18px;">:</td>
                <td id="lblLogin" runat="Server" colspan="2" style="width: 563px; height: 18px;" class="lblText"></td>
            </tr>
        <tr>
            <td class="lblTitle" style="height: 18px">Level<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
            <td style="width: 1%; height: 21px;">:</td>
            <td runat="Server" colspan="2" class="lblText" style="height: 21px">
                <asp:DropDownList ID="ddlLevel" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle">
                </asp:DropDownList></td>
        </tr>
            <tr>
                <td class="lblTitle" style="height: 21px">Role<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width: 1%; height: 21px;">:</td>
                <td id="lblRole" runat="Server" colspan="2" class="lblText" style="height: 21px"><asp:DropDownList ID="ddlRole1" runat="Server" CssClass="selectFieldStyle"></asp:DropDownList></td>
            </tr>  
        <tr>
            <td class="lblTitle" style="height: 22px">
            </td>
            <td style="width: 1%; height: 22px;">
            </td>
            <td runat="Server" class="lblText" colspan="2" style="height: 22px">
                <asp:RadioButtonList ID="rblEdit" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="R">Edit Role</asp:ListItem>
                    <asp:ListItem Value="P">Edit Previlege</asp:ListItem>
               
                </asp:RadioButtonList></td>
        </tr>
        <tr id="trTask" runat="Server">
            <td class="lblTitle">
            </td>
            <td style="width: 1%">
            </td>
            <td runat="Server" class="lblText" colspan="2"><br />
                <asp:Button ID="btnSave" runat="server" CssClass="buttonStyle" Text="Save"/>
                    <asp:Button ID="BtnCancel" runat="server" CssClass="buttonStyle" Text="Cancel"/></td>
        </tr>
        </table>
        <br />
        <table id="tblRole" runat="Server" border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td id="Td1" class="pageTitle" colspan="3">
                        </td>
                    </tr>
                    <tr id="trLevel" runat="Server">
                        <td class="lblTitle">Level<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlLvl" runat="server" AutoPostBack="True" CssClass="selectFieldStyle">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trRole" runat="Server">
                        <td class="lblTitle">Role<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="selectFieldStyle">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trJava" runat="Server">
                        <td class="lblTitle">Java<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlJava" runat="server" AutoPostBack="True" CssClass="selectFieldStyle">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trArea" runat="Server">
                        <td class="lblTitle">Area<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlArea" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trRegion" runat="Server">
                        <td class="lblTitle">Region<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlRegion" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle"
                                DataTextField="--Select--" DataValueField="0">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trZone" runat="Server">
                        <td class="lblTitle">Zone<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td>
                            <asp:DropDownList ID="ddlZone" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle"
                                DataTextField="--Select--" DataValueField="0">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trSite" runat="Server">
                        <td class="lblTitle">Site<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                        <td style="width: 1px">:</td>
                        <td><asp:DropDownList ID="ddlSite" runat="Server" CssClass="selectFieldStyle" DataTextField="--Select--"
                                DataValueField="0">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 4px; width: 167px;">
                        </td>
                        <td style="width: 1px; height: 4px;">
                        </td>
                        <td style="height: 4px"><br />
                            <asp:Button ID="btnAdd" runat="server" CssClass="buttonStyle" Text="Update" />
                            <asp:Button ID="btnBack" runat="server" CssClass="buttonStyle" Text="Back" />
                        </td>
                    </tr>
                </table><br />
                <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True" 
                                                AutoGenerateColumns="False" DataKeyNames="sno" EmptyDataText="No Records Found"
                                PageSize="5" Width="100%">
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
                                   <asp:BoundField DataField="JAVA" HeaderText="Java" />
                                   <asp:BoundField DataField="AREA" HeaderText="Area" />
                                    <asp:BoundField DataField="REGION" HeaderText="Region" />
                                    <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                    <asp:BoundField DataField="SITE" HeaderText="Site" />
                                <asp:BoundField DataField="sno" HeaderText=" Total " />
                                 <asp:TemplateField Visible ="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" OnClick="Go">Edit</asp:LinkButton>
                                </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" 
                                            CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete');"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
        <input type="hidden" runat="server" id="hdnUId"/>
        <input type="hidden" runat="server" id="hdnId"/>
        <input type="hidden" runat="server" id="hdnRole"/>
        <input type="hidden" runat="server" id="hdnUsertype"/>
        <input type="hidden" runat="server" id="hdnSRCID"/>
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
