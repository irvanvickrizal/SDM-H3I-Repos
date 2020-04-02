<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCustomerList.aspx.vb" Inherits="USR_frmCustomerList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer List</title>
    <base target="_self" />
     <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
   <div style="width:100%" id="divWidth">
        <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd">Customer List</td>
            </tr>            
            <tr>
            <td class="lblTitle" style="width:18%">Type</td>
            <td style="width: 1%">:</td>
            <td><asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>           
            <tr>
                <td class="lblTitle">Search by Role</td>
                <td style="width: 4px">:</td>
                <td><asp:DropDownList ID="ddlRole" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="lblTitle">Search User</td>
                <td style="width: 4px">:</td>
                <td><asp:DropDownList ID="ddlSelect" runat="Server" CssClass="selectFieldStyle">
                    <asp:ListItem Value="[NAME]">Name</asp:ListItem>
                    <asp:ListItem Value="GRPDESC">User Type</asp:ListItem>
                    <asp:ListItem Value="USRLOGIN">Login ID</asp:ListItem>
                    <asp:ListItem Value="EMAIL">Email</asp:ListItem>
                </asp:DropDownList>
                <input id="txtSearch" runat="server" type="text" class="textFieldStyle" maxlength="30" onkeypress="javascript:return allowKeyAcceptsData('.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');"/>&nbsp;<asp:Button ID="btnGo" runat="Server" Text="GO" CssClass="goButtonStyle" />
                </td>
            </tr>
            <tr><td colspan="4" align="right"><input type="button" runat="server" id="btnCreate" value="Create" class="buttonStyle" /></td></tr>
        <tr>
            <td colspan="4"><br />
             <asp:GridView ID="grdUserlist" runat="server" AllowPaging="True" AllowSorting="True" Width="100%" DataKeyNames="USR_ID"
                AutoGenerateColumns="False" PageSize="5" EmptyDataText="No Records Found">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" /> 
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle"/>
            <Columns>
                    <asp:TemplateField HeaderText=" Total ">
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server"></asp:Label>    
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID" DataNavigateUrlFormatString="frmUserDetails.aspx?id={0}" HeaderText="Name" SortExpression="NAME">
                    <ItemStyle Width="25%" />
                </asp:HyperLinkField>
                <asp:HyperLinkField DataTextField="NAME" DataNavigateUrlFields="USR_ID,Name" DataNavigateUrlFormatString="frmCODSubConList.aspx?id={0}&SS={1}&SelMode=True" HeaderText="Name" SortExpression="NAME">
                    <ItemStyle Width="25%" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="GRPDESC" HeaderText="User Type" SortExpression="GRPDESC" />
                <asp:BoundField DataField="ROLEDESC" HeaderText="Role" SortExpression="ROLEDESC" />   
                <asp:BoundField DataField="USRLOGIN" HeaderText="Login ID" SortExpression="USRLOGIN" />  
                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />   
                <asp:BoundField DataField="Approved" HeaderText="Status" SortExpression="Approved" />             
                <asp:BoundField DataField="Acc_Status" HeaderText="Account Status" SortExpression="Acc_Status" />
                     </Columns>
        </asp:GridView>
            </td>
        </tr>
              
        </table>
        <input id="hdnSort" type="hidden" runat="server" />
        <input id="hdnusrType" type="hidden" runat="server" />
    </div>
    </form>
</body>
</html>
