<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpositereports.aspx.vb" Inherits="RPT_frmpositereports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table cellpadding="4" cellspacing="4" width="75%">
        <tr>
            <td align="center">
                <table style="width: 70%">
                    <tr>
                        <td >
                            Select PoNo</td>
                        <td align="left" >
                            &nbsp;<asp:DropDownList ID="DDPoDetails" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDPoDetails"
                                ErrorMessage="Please Select the PoNo" InitialValue="0" ValidationGroup="FinanicialReport">*</asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" >
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" ValidationGroup="FinanicialReport" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ValidationGroup="FinanicialReport" ShowMessageBox="True" ShowSummary="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="hgap">
            </td>
        </tr>
        <tr class="pageTitle">
          <td align="center">PO Report 
          </td>
           
        </tr>
        <tr>
        <td id="tdTitle" runat="server" align="center"  ></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
