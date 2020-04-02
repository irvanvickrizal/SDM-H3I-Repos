<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRole.aspx.vb" Inherits="NSN_frmRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Type</title>
   <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>      
</head>
  <script language="javascript" type="text/javascript">
        function checkIsEmpty()    
        {
            var msg="";                       
            if (IsEmptyCheck(document.getElementById("txtCode").value) == false)
            {
                msg = msg + "Code should not be empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtDesc").value) == false)
            {
                msg = msg + "Desc should not be empty\n"
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
    <div>
        <div>
                   <table id="tblSetup" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td align="left" colspan="4">User Role Details</td>
            </tr>
                      
            <tr>
                <td class="lblTitle" style="width: 169px">
                    Code</td>
                <td>
                    <input id="txtCode" runat="Server" type="text" maxlength="2" class="textFieldStyle" /></td>
            </tr>
            <tr>
                <td class="lblTitle" style="width: 169px">
                    Description</td>
                <td style="height: 21px">
                    <input id="txtDesc" runat="Server" type="text" maxlength="20" class="textFieldStyle" />
                    </td>
            </tr>
                       <tr>
                            <td class="lblTitle" style="width: 169px">Group</td>
                           <td><asp:DropDownList ID="ddlGroup" runat="server" CssClass="selectFieldStyle">
                           </asp:DropDownList></td>
                       </tr>
                       <tr>
                            <td class="lblTitle" style="width: 169px">Level</td>
                           <td><asp:DropDownList ID="ddlLevel" runat="server" CssClass="selectFieldStyle">
                               <asp:ListItem Value="0">--Select--</asp:ListItem>
                               <asp:ListItem Value="N">National</asp:ListItem>
                               <asp:ListItem Value="A">Area</asp:ListItem>
                               <asp:ListItem Value="R">Region</asp:ListItem>
                               <asp:ListItem Value="Z">Zone</asp:ListItem>
                               <asp:ListItem Value="S">Site</asp:ListItem>
                           </asp:DropDownList></td>
                       </tr>
                       <tr>
                           <td class="lblTitle" style="width: 169px">
                           </td>
                           <td>
                               <asp:RadioButtonList ID="RBL" runat="server"  CssClass="selectedFieldStyle" RepeatLayout="Flow " RepeatDirection="Horizontal">
                                   <asp:ListItem Selected="True" Value="1">Single</asp:ListItem>
                                   <asp:ListItem Value="0">Multiple</asp:ListItem>
                               </asp:RadioButtonList></td>
                       </tr>
                       <tr>
                           <td class="lblTitle" style="width: 169px">
                           </td>
                           <td style="height: 21px"><br />
                               <asp:Button ID="btnInsert" runat="server" Text="Save" CssClass="buttonStyle" Width="75px" />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle" Width="75px" /></td>
                       </tr>
        </table><br />
            <table cellpadding="1" cellspacing="1" style="font: 10pt verdana" width="100%">                 
                <tr class="pageTitle">
                    <td colspan="3">
                        <strong>User Role List</strong></td>
                </tr>
               <tr>
                    <td class="lblTitle" style="width: 169px">Search</td>
                    <td><asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="R.Rolecode" Text="Code"></asp:ListItem>
                        <asp:ListItem Value="R.RoleDesc" Text="Description"></asp:ListItem>
                        <asp:ListItem Value="UT.GrpDesc" Text="Group"></asp:ListItem>
                    </asp:DropDownList>&nbsp;<input type="text" id="txtSearch" runat="server" class="textFieldStyle" />&nbsp;<asp:Button ID="btnSearch" runat="Server" Text="GO" CssClass="goButtonStyle" />
                        </td>
                        <td align="right"><asp:Button ID="btnNew" runat="server" Text="Create" CssClass="buttonStyle" Width="75px"/></td>
                </tr> 
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdUsrRole" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" DataKeyNames="RoleID" 
                             AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" >
                            <PagerSettings Position="TopAndBottom" />
                            <AlternatingRowStyle CssClass="GridOddRows" />
                            <RowStyle CssClass="GridEvenRows" /> 
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px" VerticalAlign="Middle"/>
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Code" DataTextField="Rolecode" DataNavigateUrlFields="RoleID" DataNavigateUrlFormatString = "frmRole.aspx?ID={0}" SortExpression="Rolecode"/>                                   
                                <asp:BoundField DataField="RoleDesc" HeaderText="Descrption" SortExpression="RoleDesc" />
                                <asp:BoundField DataField="GrpDesc" HeaderText="Group" SortExpression="GrpDesc" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" CommandArgument='<%#Container.DataItem("GrpId")%>"'
                                            CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete');"
                                            Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
        <input id="hdnSort" type="hidden" runat="server" />
    </form>
</body>
</html>
