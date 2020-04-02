<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODReason.aspx.vb" Inherits="COD_frmCODReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reason WCTR</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

    <script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";       
        var e = document.getElementById("ddlReasonCategory"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Reason Category should not be empty\n";
        } 
         if (IsEmptyCheck(document.getElementById("txtReason").value) == false)
        {
            msg = msg + "Reason should not be Empty\n";
        }  
         if (IsEmptyCheck(document.getElementById("txtRemark").value) == false)
        {
            msg = msg + "Remark should not be Empty\n";
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
            <table id="tblReason" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
                visible="false">
                <tr>
                    <td colspan="2" class="pageTitle">
                        Reason</td>
                    <td align="right" runat="server" id="addrow" class="pageTitleSub">
                        Create</td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="height: 15px; text-align: right;" valign="top">
                        Reason Category<font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%; height: 15px" valign="top">
                        :</td>
                    <td style="height: 15px">
                        <asp:DropDownList ID="ddlReasonCategory" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Reason<font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <input type="text" id="txtReason" runat="server" class="textFieldStyle" style="width: 319px" /></td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top">
                        Remark <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                    </td>
                    <td style="width: 1%" valign="top">
                        :</td>
                    <td>
                        <textarea id="txtRemark" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top">
                        Additional Remarks</td>
                    <td style="width: 1%" valign="top">
                        :</td>
                    <td>
                        <textarea id="txtAddRemark" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td>
                        &nbsp;<br />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
                </tr>
            </table>
            <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" class="pageTitle" id="tdTitle" runat="server">
                        Reason
                    </td>
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
            <asp:GridView ID="grdReason" runat="server" AllowPaging="True" AllowSorting="True"
                Width="100%" AutoGenerateColumns="False" PageSize="5" EmptyDataText="No Records Found">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText=" Total ">
                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                        <ItemTemplate>
                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="RCCode" HeaderText="Code" SortExpression="Remark" />
                    <asp:HyperLinkField DataTextField="Reason" DataNavigateUrlFields="PK_Reason" DataNavigateUrlFormatString="frmCODReason.aspx?id={0}&amp;Mode=E"
                        HeaderText="Reason" SortExpression="Reason">
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                    <asp:BoundField DataField="AddRemarks" HeaderText="Additional Remarks" SortExpression="AddRemarks" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
