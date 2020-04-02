<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sendsms2.aspx.vb" Inherits="sendsms2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;&nbsp;
            <table>
                <tr>
                    <td style="width: 100px">
                        MOBILE</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextRecipient" runat="server"></asp:TextBox></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        MESSAGE</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="TextMessage" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                        <asp:Button ID="Button1" runat="server" Text="Send" /></td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
