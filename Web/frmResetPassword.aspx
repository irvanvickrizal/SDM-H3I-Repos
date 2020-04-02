<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResetPassword.aspx.vb"
    Inherits="frmResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RESET PASSWORD</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:600px; border-style:solid; border-width:1px; border-color:Black;">
            <div style="font-family: Verdana; font-size: 12px; font-weight: bold; color: White;
                background-color: #cfcfcf; padding: 5px;">
                Password Reminder
            </div>
            <div>
                <asp:Panel ID="PnlCorrectUsername" runat="server">
                    <table>
                        <tr>
                            <td>
                                <img src="Images/Correct_Icon.jpg" alt="Correct Icon" id="imgCorrectIcon" />
                            </td>
                            <td>
                                Please see your password reset in your email.
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PnlIncorrectUsername" runat="server">
                    <table>
                        <tr>
                            <td>
                                <img src="Images/Incorrect_Icon.jpg" alt="Incorrect Icon" id="imgIncorrectIcon" />
                            </td>
                            <td>
                                Username can't found in this system, please try again or contact administrator.
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="margin-top: 15px; margin-left: 5px; margin-bottom: 5px;">
                <table>
                    <tr>
                        <td>
                            <span style="font-family: Verdana; font-size: 11px;">username </span>
                        </td>
                        <td style="width: 265px;">
                            <asp:TextBox ID="TxtUsername" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RfvUsername" runat="server" ControlToValidate="TxtUsername"
                                ErrorMessage="Please, Fill username Field First!" Display="None" ValidationGroup="SendPassword"></asp:RequiredFieldValidator>
                            <asp:Button ID="BtnSendPassword" runat="server" Text="Send Password" Font-Names="Verdana"
                                OnClick="BtnSendPasswordClick" ValidationGroup="SendPassword" />
                            <asp:Label ID="LblGeneratePassword" runat="server" Visible="false"></asp:Label>
                            <asp:HiddenField ID="HdfPassword" runat="server" />
                            <asp:ValidationSummary ID="VsSendPassword" runat="server" DisplayMode="List" ValidationGroup="SendPassword"
                                ShowMessageBox="true" ShowSummary="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-bottom: 5px; margin-left: 10px;">
                <span style="color: Red; font-family: Verdana; font-size: 11px;">Note : Your password
                    will be delivered to your email address that has been registered in eBast. </span>
            </div>
            <div style="margin-bottom: 5px; margin-left: 10px; text-align: right; margin-top: 20px;
                margin-right: 5px; font-family: Verdana; font-size: 11px;">
                <asp:LinkButton ID="LbtGoToSignInForm" runat="server" Text="Back to Sign in Page"
                    OnClick="LbtGoToSignInFormClick" CausesValidation="false"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
