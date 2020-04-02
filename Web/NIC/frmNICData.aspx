<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmNICData.aspx.vb" Inherits="MSD_frmNICData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
     
     <script language="javascript" type="text/jscript">
    
    function checkKey()
    {
        if(window.event.keyCode == 32) //48 57
        {
        window.event.keyCode = 0;
        return false;
        }
    }
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <table width="100%">
            <tr class="pageTitle">
                <td colspan="2" style="width: 1159px">
                    Mile Stone Details</td>
            </tr>
            <tr valign="top">
                <td colspan="2" style="width: 1159px">
                </td>
            </tr>
            <tr valign="top">
                <td colspan="2" style="width: 1159px">
                    <asp:Label ID="lblMSG" runat="server" ForeColor="Blue" Text="Label" Visible="False"></asp:Label></td>
            </tr>
            <tr valign="top">
                <td colspan="2" style="width: 1159px">
                </td>
            </tr>
            <tr>
                <td id="EPMcount" runat="server" colspan="2" style="width: 1159px">
                    <table border="0" style="width: 534px">
                        <tr>
                            <td style="width: 100px">
                                Data</td>
                            <td colspan="2">
                                <asp:TextBox ID="txtFieldName" runat="server" CssClass="textFieldStyle"  Height="17px"
                                    MaxLength="100" Width="394px" onkeypress="checkKey()" EnableViewState="False" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                Type</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlDataType" runat="server" AutoPostBack="True" CssClass="selectFieldStyle"
                                    Width="98px">
                                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Text</asp:ListItem>
                                    <asp:ListItem Value="2">DateTime</asp:ListItem>
                                    <asp:ListItem Value="3">Integer</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="1">
                            </td>
                            <td colspan="2">
                                <table style="width: 374px">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Button ID="btnSave"
                        runat="server" CssClass="buttonStyle" Text="Add" Width="86px" /></td>
                                        <td style="width: 100px">
                                            <asp:Button ID="btnUpdateFields"
                        runat="server" CssClass="buttonStyle" Text="Update Fields" Width="101px" Enabled="False" /></td>
                                        <td style="width: 100px">
                                            <asp:Button ID="btnDelete"
                        runat="server" CssClass="buttonStyle" Text="Delete Fields" Width="98px" Enabled="False" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="Td1" runat="server" colspan="2" style="width: 1159px">
                    </td>
            </tr>
            <tr>
                <td id="Td2" runat="server" colspan="2" style="width: 1159px">
                    <input id="hdnSLNO" runat="server" type="hidden" />
                    <input id="hdnFieldName" runat="server" type="hidden" /></td>
            </tr>
            <tr>
                <td id="Td3" runat="server" colspan="2" style="width: 1159px">
                    <asp:GridView ID="grdNICDATA" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="1"
                        EmptyDataText="No Records Found.." Width="30%">
                        <PagerSettings Position="TopAndBottom" />
                        <RowStyle CssClass="GridOddRows" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="SLNo" DataNavigateUrlFormatString="frmNICData.aspx?SLNo={0}"
                                DataTextField="FieldName" HeaderText="FieldName" SortExpression="FieldName" />
                        </Columns>
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                    </asp:GridView>
                    &nbsp;
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
