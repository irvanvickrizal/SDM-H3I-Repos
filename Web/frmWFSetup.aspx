<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWFSetup.aspx.vb" Inherits="frmWFSetup" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
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
    <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>
    <script language="javascript" type="text/javascript">          
        function swapOptions(obj,i,j) 
        {
            var e = document.getElementById(obj);
	        var o = e.options;
	        var i_selected = o[i].selected;
	        var j_selected = o[j].selected;
	        var temp = new Option(o[i].text, o[i].value,o[i].selectedIndex, o[i].defaultSelected, o[i].selected);
	        var temp2= new Option(o[j].text, o[j].value,o[j].selectedIndex, o[j].defaultSelected, o[j].selected);
	        o[i] = temp2;
	        o[j] = temp;
	        o[i].selected = j_selected;
	        o[j].selected = i_selected;
	    }   
	    function moveOptionUp(obj) 
	    {
	        if (!hasOptions(obj)) { return; }
	        var e = document.getElementById(obj);
	        var i = e.options.length;	       
	        for (i=0; i < e.options.length; i++) {
		       if (e.options[i].selected) {
			        if (i != 0 && !e.options[i-1].selected) {
				        swapOptions(obj,i,i-1);
				        e.options[i-1].selected = true;
				        e.options[i-1].index=i-1;
				    }
			    }
		    }
	    }
        function moveOptionDown(obj) 
        {
	        if (!hasOptions(obj)) { return; }
	        var e = document.getElementById(obj);
	        var i = e.options.length;
	        //alert(i);
	        for (i=e.options.length-1; i>=0; i--) {
		        if (e.options[i].selected) {
			        if (i != (e.options.length-1) && ! e.options[i+1].selected) {
				        swapOptions(obj,i,i+1);
				        e.options[i+1].selected = true;
				        e.options[i+1].index=i+1;
				    }
			    }
		    }
	    }
	    function hasOptions(obj) 
	    {        
	        var e = document.getElementById(obj); // select element
            var strUser = e.options[e.selectedIndex].value; //(Value Field) or             var strUser = e.options[e.selectedIndex].text; (Text Field)
            if (strUser != -1)
            {
                return true;
            }
            return false;
	    } 
        function checkIsEmpty()
        {   var msg = '';        
	        var e = document.getElementById('lstUnSelected'); 
            var strUser = e.options.length; 
            if (strUser == 0)
            {
                msg = 'Please select group items to Priority\n';
            }
            if (document.getElementById('txtWFCode').value == '')
            {
                msg = msg + 'Code should not be Empty\n';
            }
            if (document.getElementById('txtWFName').value == '')
            {
                msg = msg + 'Name should not be Empty\n';
            }
            //msg = ddltimecheck();
            if (msg == '')
            {
                return true;
            }
            else
            {
                alert('Mandatory Information required\n' + msg);
                return false;
            }
	    }	    
	    function ConfirmDelete()
        {
            var r = confirm("Are you sure want to Delete?");
            if (r == true)
            {
                return true;
            }
            else{return false;}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
                <ContentTemplate>
                    <div class="row">
	                    <div class="col-xs-12">
		                    <div class="box box-info">
			                    <div class="box-header with-border">
				                    <h3 class="box-title">Process Flow Setup Details</h3>
			                    </div>
			                    <div class="box-body">
				                    <div class="table table-responsive">
					                    <div class="col-md-11">
						                    <div class="form-horizontal">
							                    <div class="form-group">
								                    <label class="col-sm-3 control-label">
                                                        Process Flow Code
                                                        <font style="Color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
								                    </label>
								                    <div class="col-sm-3">
                                                        <input id="txtWFCode" type="text" maxlength="2" class="form-control" runat="server" />
								                    </div>
							                    </div>
							                    <div class="form-group">
								                    <label class="col-sm-3 control-label">
                                                        ProcessFlow Name
                                                        <font style="Color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
								                    </label>
								                    <div class="col-sm-3">
                                                        <input id="txtWFName" type="text" maxlength="20" class="form-control" runat="server" />
								                    </div>
							                    </div>
                                                <div class="form-group">
								                    <label class="col-sm-3 control-label">
                                                        SLA
                                                        <font style="Color:Red; font-size:16px">
                                                            <sup>*</sup>
                                                        </font>
								                    </label>
								                    <div class="col-sm-3">
                                                        <input id="txtTime" type="text" runat="server" maxlength="3" class="form-control" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789');" />
								                    </div>
                                                    <div class="col-sm-3">
                                                        <label>Hrs</label>
                                                    </div>
							                    </div>
                                                <div class="form-group">
								                    <label class="col-sm-3 control-label">
                                                        Distribution Group
								                    </label>
								                    <div class="col-sm-2">
                                                        <asp:DropDownList CssClass ="form-control" runat="server" id="ddlGrp" />
								                    </div>
							                    </div>
                                                <div class="form-group">
								                    <label class="col-sm-3 control-label">Heirarchy</label>
								                    <div class="col-sm-9">
                                                        <table class="table table-condensed">
                                                            <tr>
                                                                <th colspan="2">Roles</th>
                                                                <th colspan="2">Route</th>
                                                            </tr>
                                                            <tr>
                                                                <td style="width:300px">
                                                                    <asp:ListBox ID="lstSelected" runat="server" Rows="10" CssClass="form-control" style="overflow-x:auto;"/>
                                                                </td>
                                                                <td align="center" valign="middle">
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnAddAll" value=">>" /> <br />
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnAdd" value=">" /> <br />
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnRemove" value="<" /> <br />
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnRemoveAll" value="<<" />
                                                                </td>
                                                                <td style="width:300px">
                                                                    <asp:ListBox ID="lstUnSelected" CssClass="form-control" runat="server" Rows="10" style="overflow-x:auto;"/>
                                                                </td>
                                                                <td valign="middle">
                                                                    <br />
                                                                    <input type="button" class="btn btn-block btn-info" runat="server" id="btnUpdate" value="Edit" disabled="disabled" onserverclick="btnUpdate_ServerClick" /> <br />
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnUp" value="Up" disabled="disabled" /><br />
                                                                    <input type="button" class="btn btn-block btn-default" runat="server" id="btnDown" value="Down" disabled="disabled"/>
                                                                </td>
                                                            </tr>
                                                        </table>
								                    </div>
							                    </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3"><span style="display:none;height:10px"></span></div>
                                                    <div class="col-sm-9">
                                                        <asp:GridView ID="grdWf" runat="server" AutoGenerateColumns="False"  AllowPaging="true" PageSize="5"
                                                            CssClass="table table-condensed table-condensed">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <AlternatingRowStyle CssClass="table table-condensed table-condensed" />
                                                            <%--<RowStyle CssClass="GridOddRows" />--%>
                                                            <PagerSettings Position="TopAndBottom" />
                                                            <PagerStyle HorizontalAlign="Right" CssClass="customPagination" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText=" Total "><ItemTemplate><%#Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                                                                <asp:BoundField DataField="Route" HeaderText="Route" />
                                                                <asp:TemplateField HeaderText="Task">
                                                                    <ItemTemplate><asp:DropDownList ID="ddlTask"  AutoPostBack="true" runat="server" CssClass="form-control" DataSource='<%# dt1 %>' DataTextField="txt" DataValueField="val" OnSelectedIndexChanged="DoDIS"></asp:DropDownList></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TSK_Id" />
                                                                <asp:TemplateField HeaderText="Escalate Role">
                                                                     <ItemTemplate>
                                                                       <asp:DropDownList Visible="false" ID="ddlRole" runat="server" CssClass="form-control" DataSource='<%# dt2 %>' DataTextField="txt" DataValueField="val"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Time in Hrs">
                                                                    <ItemTemplate>
                                                                       <input type="text" visible="false" id="textesc" runat="server" maxlength="5" class="form-control" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" value='' />                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="grpid" />
                                                                <asp:BoundField DataField="EscRoleid" />
                                                                <asp:BoundField DataField="EscTime" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="form-group">
								                    <div class="col-sm-3"></div>
								                    <div class="col-sm-2">
                                                        <asp:Button ID="btnSave" Cssclass="btn btn-block btn-primary" runat="server" Text="Proceed" />
								                    </div>
                                                    <div class="col-sm-2">
                                                        <input type="button" class="btn btn-block btn-warning" runat="server" value="Cancel" id="btnCancel" />
								                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="btnDelete" Cssclass="btn btn-block btn-danger" runat="server" Text="Delete" Visible="false" />
								                    </div>
                                                    <div class="col-sm-2">
                                                        <input type="button" id="btnEdit" runat="server" visible="false" value="Edit" class="btn btn-block btn-info" />
								                    </div>
							                    </div>
						                    </div>
					                    </div>
				                    </div>
			                    </div>
		                    </div>
	                    </div>
                    </div>
            </ContentTemplate> 
        </asp:UpdatePanel> 
    </form>
    <%--OLD--%>
    <%--<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
            <ContentTemplate>     
    <table width="100%" cellpadding="1" cellspacing="1">
        <tr class="pageTitle"><td colspan="7">Process Flow Setup Details</td></tr>
        <tr><td class="lblTitle">ProcessFlow Code<font style="Color:Red; font-size:16px"><sup> * </sup></font></td><td style="width:1%">:</td><td colspan="4"><input id="txtWFCode" type="text" maxlength="2" class="textFieldStyle" runat="server" /></td></tr>        
        <tr>
            <td class="lblTitle">ProcessFlow Name<font style="Color:Red; font-size:16px"><sup> * </sup></font></td>
            <td>:</td><td colspan="4"><input id="txtWFName" type="text" maxlength="20" class="textFieldStyle" runat="server" /></td>
        </tr>
        <tr>
            <td class="lblTitle">SLA<font style="Color:Red; font-size:16px"><sup> * </sup></font></td>
            <td>:</td><td colspan="4"><input id="txtTime" type="text" runat="server" maxlength="3" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789');" />&nbsp;Hrs</td>
        </tr>
        <tr>
            <td class="lblTitle">Distribution Group</td>
            <td>:</td>
            <td colspan="4"><asp:DropDownList CssClass ="selectFieldStyle" runat="server" id="ddlGrp"></asp:DropDownList>
            </td>
        </tr>
        <tr><td class="lblTitle">Heirarchy</td><td>:</td><td class="lblText" style="width:10%">
            <strong>Roles</strong></td>
        <td class="lblText"></td>
        <td style="width:15%"><strong>Route</strong></td></tr>
        <tr><td colspan="2" style="height: 138px"><asp:ListBox ID="lstGroups" runat="server" Rows="10" CssClass="selectFieldStyle" AutoPostBack="True" Visible="False"></asp:ListBox></td>
        <td style="width:10%; height: 138px;"><asp:ListBox ID="lstSelected" runat="server" Rows="10" CssClass="selectFieldStyle"></asp:ListBox></td>
        <td align="center" valign="middle" style="height: 138px;width:5%">
            <input type="button" class="buttonStyle" runat="server" id="btnAddAll" value=">>" /><br /><br />
            <input type="button" class="buttonStyle" runat="server" id="btnAdd" value=">" /><br /><br />
            <input type="button" class="buttonStyle" runat="server" id="btnRemove" value="<" /><br /><br />
            <input type="button" class="buttonStyle" runat="server" id="btnRemoveAll" value="<<" /></td>
         <td style="height: 138px">
            <asp:ListBox ID="lstUnSelected" CssClass="selectFieldStyle" runat="server" Rows="10"></asp:ListBox></td>
            <td valign="middle" style="height: 138px">
                <input type="button" class="buttonStyle" runat="server" id="btnUpdate" value="Edit" disabled="disabled" onserverclick="btnUpdate_ServerClick" /><br /><br />
                <input type="button" class="buttonStyle" runat="server" id="btnUp" value="Up" disabled="disabled" /><br /><br />
                <input type="button" class="buttonStyle" runat="server" id="btnDown" value="Down" disabled="disabled"/>
            </td></tr>        
        <tr></tr>
        <tr><td colspan="2"></td>
            <td colspan="5">
            <asp:GridView ID="grdWf" runat="server" AutoGenerateColumns="False">
                <HeaderStyle CssClass="GridHeader" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total "><ItemTemplate><%# container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="Route" HeaderText="Route" />
                    <asp:TemplateField HeaderText="Task">
                        <ItemTemplate><asp:DropDownList ID="ddlTask"  AutoPostBack="true" runat="server" CssClass="selectFieldStyle" DataSource='<%# dt1 %>' DataTextField="txt" DataValueField="val" OnSelectedIndexChanged="DoDIS"></asp:DropDownList></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TSK_Id" />
                    <asp:TemplateField HeaderText="Escalate Role">
                         <ItemTemplate>
                           <asp:DropDownList Visible="false" ID="ddlRole" runat="server" CssClass="selectFieldStyle" DataSource='<%# dt2 %>' DataTextField="txt" DataValueField="val"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time in Hrs">
                                                <ItemTemplate>
                           <input type="text" visible="false" id="textesc" runat="server" maxlength="5" class="textFieldStyle" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" value='' />                        
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="grpid" />
                    <asp:BoundField DataField="EscRoleid" />
                    <asp:BoundField DataField="EscTime" />
                    
                </Columns>
            </asp:GridView>
        
            </td></tr>
            <tr><td colspan="2" style="height: 14px"></td><td colspan="4" style="height: 14px">
            <asp:Button ID="btnSave" Cssclass="buttonStyle" runat="server" Text="Proceed" />&nbsp;
            <input type="button" class="buttonStyle" runat="server" value="Cancel" id="btnCancel" />&nbsp;
            <asp:Button ID="btnDelete" Cssclass="buttonStyle" runat="server" Text="Delete" Visible="false" />&nbsp;
            <input type="button" id="btnEdit" runat="server" visible="false" value="Edit" class="buttonStyle" />
            
            </td></tr>
    </table>
    </ContentTemplate> 
    </asp:UpdatePanel> 
         </form>--%>
</body>
</html>
