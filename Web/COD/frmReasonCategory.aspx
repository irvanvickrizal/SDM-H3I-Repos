<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReasonCategory.aspx.vb"
    Inherits="COD_frmReasonCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reason Category BAST</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
</head>
<script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtRCCode").value) == false)
        {
            msg = msg + "Code should not be Empty\n";
        }
          if (IsEmptyCheck(document.getElementById("txtRCDesc").value) == false)
        {
            msg = msg + "Description should not be Empty\n";
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

<body>
    <form id="form1" runat="server">
        <div>
            <table id="tblReasonCategory" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%" visible="false">
                <tr>
                    <td colspan="2" class="pageTitle">
                        Reason Category</td>
                    <td align="right" runat="server" id="addrow" class="pageTitleSub">
                        Create</td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 200px">
                        &nbsp;Code<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <input type="text" id="txtRCCode" runat="server" class="textFieldStyle" maxlength="10" /></td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top" style="width: 200px">
                        Description<font style="color: Red; font-size: 16px"><sup> * </sup>
                    </td>
                    <td style="width: 1%" valign="top">
                        :</td>
                    <td>
                        <input type="text" id="txtRCDesc" runat="server" class="textFieldStyle" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 18px">
                    </td>
                    <td style="height: 18px">
                        &nbsp;<br />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" class="pageTitle" id="tdTitle" runat="server">
                        Reason Category</td>
                    <td align="right" class="pageTitleSub">
                        List
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <input type="hidden" runat="server" id="hdnSort" />
                        <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
                        <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdReasonCategory" runat="server" CellSpacing="0" CellPadding="1"
                AllowPaging="True" AllowSorting="True" Width="100%" AutoGenerateColumns="False"
                EmptyDataText="No Records Found">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField ItemStyle-Width="25%" DataTextField="RCCode" DataNavigateUrlFields="PK_ReasonCategory"
                        DataNavigateUrlFormatString="frmReasonCategory.aspx?id={0}&Mode=E" HeaderText="Code"
                        SortExpression="RCCode" />
                    <asp:BoundField DataField="RCDesc" HeaderText="Description" SortExpression="RCDesc" />
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </form>
</body>
</html>
