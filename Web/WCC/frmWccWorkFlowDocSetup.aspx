<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccWorkFlowDocSetup.aspx.vb" Inherits="WCC_frmWccWorkFlowDocSetup" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript">
        function checkIsEmpty()
        {
            var msg="";
            var e = document.getElementById("ddlWF"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Workflow should be select\n"
            }
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return accessConfirm();
            }          
        }
        function accessConfirm()
        {
            var r = confirm("Are you sure you want to save the details?");
            if (r == true)
            {
                return true;
            }
            else{return false;}
        }
        
        /*function myPostBack()
        {    
          var o = window.event.srcElement;
          if (o.tagName == "INPUT" && o.type == "checkbox")
              {__doPostBack("","");
              } 
        }
        function TreeNodeCheckChanged(event, control) 
        {    // Valid for IE and Firefox/Safari/Chrome.    
            var obj = window.event ? window.event.srcElement : event.target;    
            var source = window.event ? window.event.srcElement.id : event.target.id;    
            source = source.replace(control.id + "t", control.id + "n");    
            source = source.replace("CheckBox", "");    
            var checkbox = document.getElementById(source);    
            if (checkbox != null && obj.tagName == "INPUT" && obj.type == "checkbox") 
            {        __doPostBack(checkbox.id, "");    
            }
        } */ 
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
            <ContentTemplate>        
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr class="pageTitle">
                        <td colspan="3">
                            Wcc
                            Business Process Flow</td>
                    </tr>
                    <tr style="height:5"><td colspan="3"></td></tr>
                                <tr>
                        <td class="lblTitleM">
                            Process Flow</td>
                        <td style="width: 1px">:</td>
                        <td><asp:DropDownList ID="ddlWF" runat="server" CssClass="selectFieldStyle" AutoPostBack="true"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="lblTitleM">Documents</td>
                        <td valign="top">:</td>
                        <td><asp:TreeView ID="TreeView1" runat="server" CssClass="tree" ShowCheckBoxes="All" AutoGenerateDataBindings="false" NodeIndent="10" ExpandDepth="3" ShowLines="True">
                            <ParentNodeStyle Font-Bold="True" ForeColor="Blue" />
                                    <RootNodeStyle Font-Bold="True" ForeColor="Blue" />            
                                    <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="None" BorderWidth="1px"
                                        Font-Underline="False" HorizontalPadding="1px" VerticalPadding="0px" />
                                    <NodeStyle CssClass="instructionalMessage" HorizontalPadding="1px"
                                        NodeSpacing="0px" VerticalPadding="0px" />
                            </asp:TreeView>
                            
                        </td>
                    </tr>
                    <tr style="height:5"><td colspan="3"></td></tr>
                    <tr>
                        <td colspan="2"></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass ="buttonStyle" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass ="buttonStyle" /></td>
                    </tr>        
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>           
    </form>
</body>
</html>
