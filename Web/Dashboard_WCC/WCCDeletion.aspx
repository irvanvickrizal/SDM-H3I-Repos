<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WCCDeletion.aspx.vb" Inherits="Dashboard_WCC_WCCDeletion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Deletion</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="LblRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRemarks" runat="server" TextMode="MultiLine" Width="250px" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="BtnDelete" runat="server" Text="Submit" OnClick="BtnDelete_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
