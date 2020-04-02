<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFinanicalReports.aspx.vb"
    Inherits="RPT_frmFinancialReports" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
  Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Financial Report</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="4" cellspacing="4" width="100%">
            <tr>
                <td align="left">
                    <table style="width: 70%">
                        <tr>
                            <td class="lblTitle" style="width: 10%">
                                Month
                            </td>
                            <td style="width: 20%">
                                <asp:DropDownList ID="DDMonth" CssClass="selectFieldStyle" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDMonth"
                                    ErrorMessage="Please Select the month" InitialValue="0" ValidationGroup="FinanicialReport">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="lblTitle" style="width: 10%">
                                Year
                            </td>
                            <td style="width: 20%">
                                <asp:DropDownList ID="DDYear" CssClass="selectFieldStyle" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDYear"
                                    ErrorMessage="Please Select the Year" InitialValue="0" ValidationGroup="FinanicialReport">*</asp:RequiredFieldValidator>
                            </td>
                            <td class="lblTitle" style="width: 10%;">
                                Po No
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="buttonStyle" ValidationGroup="FinanicialReport" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ValidationGroup="FinanicialReport" ShowMessageBox="True" ShowSummary="False" />
                            </td>
                        </tr>
            </table> </td> </tr>
            <tr>
                <td align="center" class="hgap">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="left" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                            </td>
                            <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%">
                            </td>
                            <td align="right" valign="top" style="width: 20%">
                                <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="pageTitle">
                <td align="center">
                    Financial Report
                </td>
            </tr>
            <tr>
                <td id="tdTitle" runat="server" align="center">
                </td>
            </tr>
            <tr>
                <td runat="server" align="center">
                    <font style="color: red; font-size: 16px">* </font>(Value IDR (Euro Rate) + Value
                    USD (Euro Rate))
                </td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
            EnableParameterPrompt="False" Height="50px" Width="350px" HasToggleGroupTreeButton="False"
            HasViewList="False" HasCrystalLogo="False" HasSearchButton="False" DisplayGroupTree="False" />
    </div>
    </form>
</body>
</html>
