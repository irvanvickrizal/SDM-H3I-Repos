<%@ Page Language="VB" AutoEventWireup="false" CodeFile="POCancellation.aspx.vb"
    Inherits="PO_POCancellation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Cancelation</title>
    <link rel="stylesheet" type="text/css" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                <tr class="pageTitle">
                    <td colspan="3">
                        PO Delete</td>
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
                        <asp:Button ID="btndelete" runat="server" Text="Delete PO" CssClass="buttonStyle"
                            Width="84px" /></td>
                </tr>
                <tr>
                    <td style="height: 21px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 21px">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%" visible="false">
                <tr class="pageTitle">
                    <td colspan="3">
                        Delete Document Data (Related to Selected Site)</td>
                </tr>
                <tr style="height: 5">
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%">
                        Po No
                    </td>
                    <td colspan="2" style="height: 21px">
                        <asp:DropDownList ID="ddlpono1" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                            Width="135px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%">
                        Site</td>
                    <td colspan="2" style="height: 21px">
                        <asp:DropDownList ID="ddlsite" runat="server" CssClass="selectFieldStyle" Width="313px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%;">
                        &nbsp;</td>
                    <td colspan="2" style="height: 21px">
                        <asp:Button ID="btndoc" runat="server" CssClass="buttonStyle" Text="Delete Documents"
                            Width="169px" /></td>
                </tr>
                <tr>
                    <td style="height: 21px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 21px">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%" visible="false">
                <tr class="pageTitle">
                    <td colspan="3">
                        Delete Seleted Site (Not Document Checklisted)</td>
                </tr>
                <tr style="height: 5">
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%">
                        Po No</td>
                    <td colspan="2" style="height: 21px">
                        <asp:DropDownList ID="ddlpono2" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                            Width="135px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%">
                        Site</td>
                    <td colspan="2" style="height: 21px">
                        <asp:DropDownList ID="ddlsite2" runat="server" CssClass="selectFieldStyle" Width="313px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 26%;">
                        &nbsp;</td>
                    <td colspan="2" style="height: 21px">
                        <asp:Button ID="btndoc2" runat="server" CssClass="buttonStyle" Text="Delete Site"
                            Width="169px" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
