<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODLookUp2.aspx.vb" Inherits="frmLookUp2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LookUp Details</title>
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

    <script language="javascript" type="text/javascript">
    function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("ddlGroup"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Group should be select\n"
        }
        if (IsEmptyCheck(document.getElementById("txtCCode").value) == false)
        {
            msg = msg + "Code should not be Empty\n"
        }
        if (IsEmptyCheck(document.getElementById("txtCName").value) == false)
        {
            msg = msg + "Description should not be Empty\n"
        }
        if (msg != "")
        {
            alert("Mandatory field information :\n\n" + msg);
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
    <form id="frmLookup" runat="server">
        <table id="tblLookUp" runat="server" border="0" cellpadding="1" cellspacing="1" width="75%"
            visible="false">
            <tr class="pageTitle">
                <td colspan="3" align="left" id="rowadd">
                    Dashboard Milestone Names</td>
            </tr>
            <tr style="height: 5">
            </tr>
            <tr>
                <td class="lblTitle">
                    Code<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td>
                    :</td>
                <td>
                    <input type="text" id="txtLKPCode" runat="server" class="textFieldStyle" style="text-transform: uppercase"
                        maxlength="10" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz');" /></td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Description<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td>
                    :</td>
                <td align="left">
                    <input type="text" id="txtLKPDesc" class="textFieldStyle" runat="server" maxlength="50" /></td>
            </tr>
            <!-- 
            <tr visible="false">
                <td class="lblTitle">
                    Group<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td style="width: 1%">
                    :</td>
                <td>
                    <asp:DropDownList ID="DDLGRP" runat="server" CssClass="selectFieldStyle">
                    </asp:DropDownList></td>
            </tr>
            -->
            <tr>
                <td colspan="2">
                </td>
                <td>
                    &nbsp;<br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <table width="75%" cellpadding="1" cellspacing="1">
            <tr class="pageTitle">
                <td colspan="3">
                    Dashboard Milestone Names</td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Search LookUp</td>
                <td style="width: 1%">
                    :</td>
                <td align="left">
                    <asp:DropDownList CssClass="selectFieldStyle" ID="ddlSelect" runat="server">
                        <asp:ListItem Value="LKPCode">Code</asp:ListItem>
                        <asp:ListItem Value="LKPDesc">Description</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="textFieldStyle" ID="txtSearch" Height="17px"></asp:TextBox>
                    <asp:Button runat="server" Text="Go" ID="btnSearch" CssClass="goButtonStyle" /></td>
            </tr>
            <!--
            <tr>
                <td class="lblTitle">
                    Groups</td>
                <td>
                    :</td>
                <td>
                    <asp:DropDownList ID="DDLGroup" runat="server" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            -->
            <tr>
                <td colspan="3" align="right">
                    <input type="hidden" runat="server" id="hdnSort" />
                    <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="grdLookup" runat="server" Width="75%" AutoGenerateColumns="false"
            AllowPaging="true" AllowSorting="true">
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
            <RowStyle CssClass="GridOddRows" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="LKP_ID" HeaderText="LKP_ID" Visible="False" />
                <asp:TemplateField HeaderText=" Total ">
                    <ItemTemplate>
                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="LKP_ID" DataNavigateUrlFormatString="frmCODLookup2.aspx?id={0}&Mode=E"
                    SortExpression="LKPCode" DataTextField="LKPCode" HeaderText="Code">
                    <ItemStyle Width="25%" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="LKPDesc" SortExpression="LKPDESC" HeaderText="Description">
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
