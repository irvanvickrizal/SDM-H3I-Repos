<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestModem.aspx.vb" Inherits="TestModem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test Modem</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table>
            <tr>
                <td>
                    PhoneNo
                </td>
                <td>
                    <asp:TextBox ID="TxtPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Message
                </td>
                <td>
                    <asp:TextBox ID="TxtMessage" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnTest" runat="server" Text="Send SMS" />
        <asp:Label ID="LblErrorMessage" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
