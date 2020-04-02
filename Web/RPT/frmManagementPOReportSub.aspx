<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmManagementPOReportSub.aspx.vb"
    Inherits="RPT_frmManagementPOReportSub" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
  Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Management PO Report</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table><tr><td><asp:Button ID="btnBack" runat="server" Text="Back to Main" CssClass="buttonStyle" Width="100px" /></td></tr>
    <tr><td>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
        EnableParameterPrompt="False" Height="50px" Width="350px" HasToggleGroupTreeButton="False"
        HasViewList="False" HasCrystalLogo="False" HasSearchButton="False" DisplayGroupTree="False" />
    </td></tr></table>
    
    </form>
</body>
</html>
