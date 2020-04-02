<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSysMenu.aspx.vb" Inherits="frmSysMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />    
  <script type="text/javascript">
    function OnCheckBoxCheckChanged(evt) 
    {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) 
        {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)
            {
                if (nxtSibling.tagName.toLowerCase() == "div")
                {                    
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }            
            CheckUncheckParents(src, src.checked);
        }
    }
    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }
    function CheckUncheckParents(srcChild, check) 
    {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) 
        {
            var checkUncheckSwitch;
            if (check)
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; 
            }
            else 
            {
                checkUncheckSwitch = true;
            }
            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;                
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }
    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) 
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];                 
                    if (!prevChkBox.checked) 
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return true;
    }    
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }
    function SelectUser()
    {
         var msg=""
        var a = document.getElementById("ddlUserGroup"); 
        var strUser = a.options[a.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Please Select User Role.\n"
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="71%" cellpadding="0" cellspacing="1">
                <tr class="pageTitle"><td colspan="2">Access Rights</td></tr>
                <tr><td class="lblTitle">Select User Role :</td>
                <td><asp:DropDownList id="ddlUserGroup" runat="server" cssclass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnSave" Text="Save" runat="server" CssClass="buttonStyle" /></td></tr>                
                <tr><td></td><td>
                        <asp:TreeView ID="TreeView1" runat="server" CssClass="tree" ExpandDepth="2" MaxDataBindDepth="3" 
                        AutoGenerateDataBindings="False" NodeIndent="10" ShowLines="True" ShowCheckBoxes="All" Height="100%" 
                        OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged">
                            <ParentNodeStyle Font-Bold="True" ForeColor="Blue" />
                            <RootNodeStyle Font-Bold="True" ForeColor="Blue" />            
                            <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="None" BorderWidth="1px"
                                Font-Underline="False" HorizontalPadding="1px" VerticalPadding="0px" />
                            <NodeStyle CssClass="instructionalMessage" HorizontalPadding="1px"
                                NodeSpacing="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                    </td></tr>            
                </table>          
    </div>
    </form>
</body>
</html>