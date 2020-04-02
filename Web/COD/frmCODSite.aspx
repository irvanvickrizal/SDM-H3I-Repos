<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODSite.aspx.vb" Inherits="frmCODSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Site Details</title>
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js">
    </script>

</head>

<script language="javascript" type="text/javascript">
        function checkIsEmpty(){
            var msg = "";
            var e = document.getElementById("DDLZN");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Zone should be select\n";
            }
            if (IsEmptyCheck(document.getElementById("txtNo").value) == false) {
                msg = msg + "Name should not be Empty\n";
            }
            if (IsEmptyCheck(document.getElementById("txtName").value) == false) {
                msg = msg + "Name should not be Empty\n";
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }
        
        function viewUser(){
            var aa;
            aa = window.showModalDialog('../USR/frmUserList.aspx?SelMode=true', '', 'width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
            if (typeof aa != 'undefined') {
                document.getElementById('hdnSSId').value = aa;
                var bb = aa.split('####');
                document.getElementById('hdnSupId').value = bb[0];
                document.getElementById('txtSSName').value = bb[1];
                //document.getElementById('hdnUserType').value = bb[2];      
            }
        }
</script>

<body>
    <form id="frmCODSiteSetup" runat="server">
        <table id="tblSite" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%"
            visible="false">
            <tr>
                <td align="left" class="pageTitle" colspan="2">
                    Site
                </td>
                <td align="right" id="rowadd" runat="server" class="pageTitleSub">
                    Create
                </td>
            </tr>
            <tr style="height: 5">
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    No <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                </td>
                <td>
                    :
                </td>
                <td>
                    <input type="text" runat="Server" id="txtNo" class="textFieldStyle" maxlength="8" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Name <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                </td>
                <td>
                    :
                </td>
                <td>
                    <input type="text" runat="Server" id="txtName" class="textFieldStyle" maxlength="50"
                        size="75" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle" style="height: 22px">
                    Description <font style="color: Red; font-size: 16px"><sup></sup></font>
                </td>
                <td style="height: 22px">
                    :
                </td>
                <td style="height: 22px">
                    <input type="text" id="txtSTDesc" runat="server" class="textFieldStyle" maxlength="50"
                        size="75" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Select Zone <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DDLZN" runat="server" CssClass="selectFieldStyle">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Add Supervisor
                </td>
                <td>
                    :
                </td>
                <td>
                    <input type="text" runat="server" id="txtSSName" class="textFieldStyle" disabled="disabled" />&nbsp;<a
                        id="A1" runat="server" href="#" onclick="viewUser();" class="ASmall">Select Supervisor</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td>
                    &nbsp;
                    <br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" /><asp:Button
                        ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="1" width="100%">
            <tr class="pageTitle">
                <td colspan="2" id="tdTitle" runat="server">
                    Site
                </td>
                <td align="right" class="pageTitleSub">
                    List
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Search Site
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="Site_No">
                                No
                        </asp:ListItem>
                        <asp:ListItem Value="Site_Name">
                                Name
                        </asp:ListItem>
                        <asp:ListItem Value="Site_Desc">
                                Description
                        </asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle">
                    </asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Zone
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DDLZone" CssClass="selectFieldStyle" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="right" style="height: 24px">
                    <input type="hidden" runat="server" id="hdnSort" /><input id="btnNewGroup" type="button"
                        value="Create" runat="server" class="buttonStyle" disabled="disabled" />&nbsp;<input
                            id="btnCancel" type="button" value="Cancel" runat="server" class="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="grdSt" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
            AutoGenerateColumns="False" EmptyDataText="No Records Found">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total ">
                    <ItemStyle HorizontalAlign="Right" Width="1%" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="Site_No" DataNavigateUrlFields="Site_ID" DataNavigateUrlFormatString="frmCODSite.aspx?id={0}&amp;Mode=E"
                    HeaderText="Site ID" SortExpression="Site_No" ItemStyle-Width="10%"></asp:HyperLinkField>
                <asp:BoundField DataField="Site_Name" HeaderText="Name" SortExpression="Site_Name" />
                <asp:BoundField DataField="Site_Desc" HeaderText="Description" SortExpression="Site_Desc"
                    ItemStyle-Width="30%" Visible="false" />
                <asp:BoundField DataField="ZNName" HeaderText=" Zone " ItemStyle-Width="30%" SortExpression="ZNName" />
                <asp:BoundField DataField="SName" HeaderText="Supervisor" ItemStyle-Width="15%" SortExpression="SName" />
            </Columns>
        </asp:GridView>
        <input type="hidden" runat="server" id="hdnSSId" /><input type="hidden" runat="server"
            id="hdnSupId" value="0" /><input type="hidden" runat="server" id="hdnUserType" value="0" />
    </form>
</body>
</html>
