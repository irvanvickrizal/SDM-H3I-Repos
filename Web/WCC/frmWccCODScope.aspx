<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccCODScope.aspx.vb" Inherits="WCC_frmWccCODScope" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

    <script language="javascript" type="text/javascript">

    function checkIsEmpty()
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtScope").value) == false)
        {
            msg = msg + "Scope should not be Empty\n";
        }
        if (IsEmptyCheck(document.getElementById("txtAlias").value) == false)
        {
            msg = msg + "Alias should not be Empty\n";
        }
        if (msg != "")
        {
            alert("Mandatory field information : \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
   <div>
        <table id="tblScope" runat="server" cellpadding="0" cellspacing="0" width="100%"
            visible="false">
            <tr>
                <td colspan="2" class="pageTitle">
                    Wcc
                    Scope Master
                </td>
                <td align="right" runat="server" id="addrow" class="pageTitleSub">
                    Create
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Scope<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td>
                    <input type="text" id="txtScope" runat="server" class="textFieldStyle" maxlength="50"
                        style="width: 397px" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Alias<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td>
                    <input type="text" id="txtAlias" runat="server" class="textFieldStyle" maxlength="50"
                        style="width: 397px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td>
                    <br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" class="pageTitle" id="tdTitle" runat="server">
                    Scope
                </td>
                <td align="right" class="pageTitleSub">
                    List
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3">
                    <br />
                    <input type="hidden" runat="server" id="hdnSort" />
                    <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
                    <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="grdScope" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="True"
            EmptyDataText="No Records Found.." AllowSorting="True" Width="100%" AutoGenerateColumns="False">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText="&nbsp;Sno&nbsp;" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Scope" DataNavigateUrlFields="Scope_ID" DataNavigateUrlFormatString="frmWccCODScope.aspx?id={0}"
                    HeaderText="Scope" SortExpression="Scope" />
                <asp:BoundField DataField="Alias" HeaderText="Alias" SortExpression="Alias" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
