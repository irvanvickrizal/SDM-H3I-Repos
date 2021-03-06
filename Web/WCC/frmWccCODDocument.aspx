<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccCODDocument.aspx.vb" Inherits="WCC_frmWccCODDocument" %>

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
        if (IsEmptyCheck(document.getElementById("txtDoc_Name").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }
        if (IsEmptyCheck(document.getElementById("txtSOrder").value) == false)
        {
            msg = msg + "Serial No should not be Empty\n";
        }
        var e = document.getElementById("ddlSection"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Section should not be empty\n";
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
        <table id="tblSection" runat="server" border="0" cellpadding="0" cellspacing="1"
            width="100%" visible="false">
            <tr>
                <td colspan="3" class="pageTitle">
                    Wcc
                    Document Template
                </td>
                <td align="right" id="addrow" runat="server" class="pageTitleSub">
                    Create
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="2">
                    <asp:TreeView ID="TVDoc" runat="server" CssClass="tree" ShowCheckBoxes="All" AutoGenerateDataBindings="false"
                        NodeIndent="10" ExpandDepth="3" MaxDataBindDepth="4" ShowLines="True">
                        <ParentNodeStyle Font-Bold="True" ForeColor="Blue" />
                        <RootNodeStyle Font-Bold="True" ForeColor="Blue" />
                        <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="None" BorderWidth="1px"
                            Font-Underline="False" HorizontalPadding="1px" VerticalPadding="0px" />
                        <NodeStyle CssClass="instructionalMessage" HorizontalPadding="1px" NodeSpacing="0px"
                            VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Document Type<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2">
                    <asp:RadioButtonList ID="rdoDoc" AutoPostBack="true" CssClass="lblText" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="flow">
                        <asp:ListItem Text="Document  " Value="D" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Online Form" Value="O"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Document Name<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2">
                    <input type="text" id="txtDoc_Name" runat="server" class="textFieldStyle" maxlength="75"
                        size="75" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle" valign="top">
                    Serial Order
                </td>
                <td style="width: 1%" valign="top">
                    :
                </td>
                <td colspan="2">
                    <input type="text" runat="server" id="txtSOrder" class="textFieldStyle" maxlength="2"
                        onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789');" />
                </td>
            </tr>
            <tr id="scoperow" runat="server">
                <td class="lblTitle" style="height: 19px">
                    Scope
                </td>
                <td style="height: 19px">
                    :
                </td>
                <td style="height: 19px">
                    <asp:DropDownList ID="ddlScope" runat="server" CssClass="selectFieldStyle">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="lblTitle" valign="top">
                    Online Approval Required
                </td>
                <td style="width: 1%" valign="top">
                    :
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="chkRequired" runat="server" Checked="True" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle" valign="top">
                    Allow Before Integration
                </td>
                <td style="width: 1%" valign="top">
                    :
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="chkAllow" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Digital Signature
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox ID="chkDG" runat="server" CssClass="lblText" AutoPostBack="True" Text="Doument Avilable with Box" />
                </td>
            </tr>
            <tr visible="false" id="rowForm" runat="server">
                <td class="lblTitle">
                    Online Form Name
                </td>
                <td>
                    :
                </td>
                <td colspan="2">
                    <input type="text" runat="server" id="txtOnlineForm" class="textFieldStyle" style="width: 359px" />
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                <td colspan="2">
                </td>
                <td class="lblText" colspan="2" style="font-weight: bold">
                    <asp:GridView ID="GVXY" runat="server" CellPadding="1" AllowPaging="True" Width="75%"
                        AutoGenerateColumns="False" EmptyDataText="No Records Found">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                        <Columns>
                            <asp:TemplateField HeaderText="X Position">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtX" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "X_Coordinate") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Y Position">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtY" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Y_Coordinate") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Page No">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPageno" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PageNo") %>'></asp:TextBox>
                                    <asp:HiddenField ID="HDSno" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Sno") %>' />
                                    <asp:HiddenField ID="HDDGId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "DGS_Id") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Select" Text="Add" />
                            <asp:ButtonField CommandName="Delete" Text="Remove" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="2">
                    &nbsp;<br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="pageTitle">
                <td colspan="3" id="tdTitle" runat="server">
                    WCC
                    Document Template
                </td>
                <td align="right" class="pageTitleSub">
                    List
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Search Document
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                        <asp:ListItem Value="DocName">Name</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" />
                </td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Parent Document
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlSectionSrc" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="Tr2" runat="server">
                <td class="lblTitle">
                    Scope
                </td>
                <td style="width: 1%">
                    :
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlScopeSrc" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <input type="hidden" runat="server" id="hdnSort" />
                    <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
                    <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="grdSection" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True"
            Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total ">
                    <ItemStyle HorizontalAlign="Right" Width="1%" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="DOCName" DataNavigateUrlFields="DOC_ID" DataNavigateUrlFormatString="frmWccCODDocument.aspx?id={0}&amp;Mode=E"
                    HeaderText="Name" SortExpression="DOCName"></asp:HyperLinkField>
                <asp:BoundField DataField="Scope" HeaderText="Scope" SortExpression="Scope" ItemStyle-Width="15%" />
                <asp:BoundField DataField="Parent" HeaderText="Parent Document" SortExpression="Parent"
                    ItemStyle-Width="25%" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
