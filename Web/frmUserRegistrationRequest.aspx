<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUserRegistrationRequest.aspx.vb" Inherits="frmUserRegistrationRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registration Form</title>
    <style type="text/css">
        .lblStyle
        {
            font-family:verdana;
            font-size:8pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr valign="middle">
                <td class="lblStyle">
                    <asp:Literal ID="LtrFullname" runat="server" Text="Fullname"></asp:Literal>
                </td>
                <td>
                    <div style="width:300px; text-align:right; background-color:Maroon;">
                        <asp:TextBox ID="TxtFullname" runat="server" Width="280px" Height="16px" CssClass="lblStyle" ></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr valign="middle">
                <td class="lblStyle">
                    <asp:Literal ID="LtrUsername" runat="server" Text="Username"></asp:Literal>
                </td>
                <td>
                    <div style="width:300px; text-align:right; background-color:Maroon;">
                        <asp:TextBox ID="TxtUsername" runat="server" Width="280px" Height="16px" CssClass="lblStyle" ></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr valign="middle">
                <td class="lblStyle">
                    <asp:Literal ID="LtrUserType" runat="server" Text="User Type"></asp:Literal>
                </td>
                <td>
                    <div style="width:300px; text-align:right; background-color:Maroon;">
                        <asp:DropDownList ID="DdlUserType" runat="server" CssClass="lblStyle">
                            <asp:ListItem Text="-- User Type --" Value="0"></asp:ListItem>
                            <asp:ListItem Text="NSN" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Subcon" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
