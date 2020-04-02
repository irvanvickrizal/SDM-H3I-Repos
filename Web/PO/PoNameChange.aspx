<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PoNameChange.aspx.vb" Inherits="PO_PoNameChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                <tr class="pageTitle">
                    <td colspan="3">
                        POName Change</td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 6%;">
                        Po No<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td style="width: 5px">
                        :
                    </td>
                    <td style="height: 21px">
                        <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                            Width="135px">
                        </asp:DropDownList>
                        </td>
                </tr>
         <tr>
             <td class="lblTitle" style="width: 6%">
                 Po Name</td>
             <td style="width: 5px">
             </td>
             <td style="height: 21px">
                 <asp:TextBox ID="txtponame" runat="server" Width="247px"></asp:TextBox>
                 <asp:Button ID="btnupdate" runat="server" Text="UPDATE" CssClass="buttonStyle" /></td>
         </tr>
                </table>
    </div>
    </form>
</body>
</html>
