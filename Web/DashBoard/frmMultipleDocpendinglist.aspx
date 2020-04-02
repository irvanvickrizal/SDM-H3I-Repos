<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMultipleDocpendinglist.aspx.vb"
    Inherits="frmMultipleDocpendinglist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Multiple Doc's pending list</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="tblDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc; text-align: left">
                        Multiple Approval List
                    </td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                    </td>
                </tr>
            </table>
            <input id="hdnSort" runat="server" type="hidden" /><br />
            <br />
            <asp:GridView ID="grdSite" runat="server" AllowPaging="False" EmptyDataText="No documents to  approve"
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="wftsno">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input id="CheckBox1" runat="server" type="checkbox" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SNo">
                        <ItemTemplate>
                            <asp:Label ID="lblno" Text='<%# Container.DataItemIndex + 1 %>' runat="Server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="site_no" HeaderText="Site No" />
                    <asp:BoundField DataField="siteversion" HeaderText="Version" />
                    <asp:BoundField DataField="docID" HeaderText="Document ID" />
                    <asp:BoundField DataField="docname" HeaderText="Document Name" />
                    <asp:BoundField DataField="siteid" HeaderText="Site id" />
                    <asp:BoundField DataField="taskid" HeaderText="Task" />
                    <asp:BoundField DataField="docpath" HeaderText="docpath" />
                    <asp:BoundField DataField="wftsno" HeaderText="wftsno" />
                    <asp:BoundField DataField="XVAL" HeaderText="XVAL" />
                    <asp:BoundField DataField="YVAL" HeaderText="YVAL" />
                    <asp:BoundField DataField="PAGENO" HeaderText="PAGENO" />
                    <asp:BoundField DataField="SiteDocID" HeaderText="SiteDocID" />
                    <asp:BoundField DataField="pono" HeaderText="Po No" />
                    <asp:BoundField DataField="fldtype" HeaderText="Scope" />
                    <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                </Columns>
            </asp:GridView>
            <br />
            <table border="0" cellpadding="1" cellspacing="2" style="width: 50%">
                <tr>
                    <td class="pageTitle" colspan="2" style="height: 20px">
                        Digital Signature Login</td>
                </tr>
                <tr>
                    <td class="lblTitle" style="height: 34px">
                        User Name<font style="font-size: 16px; color: red"><sup> * </sup></font>
                    </td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textFieldStyle" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                            ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator><br />
                        <asp:LinkButton ID="lnkrequest" runat="server" OnClientClick="this.style.display = 'none'; loadingdiv.style.display = '';">Request Password</asp:LinkButton></td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Password <font style="font-size: 16px; color: red"><sup>* </sup></font>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textFieldStyle" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="height: 40px">
                    </td>
                    <td style="height: 40px">
                        <asp:Button ID="btnProceed" runat="server" CssClass="buttonStyle" Text="Proceed for Sign"
                            Width="144px" />&nbsp;
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSign" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="loadingdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 120%; left: 40px;">
            <img src="../sendsms.GIF" runat="server" id="loading" />
        </div>
        <div id="dgdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 120%; left: 16px;">
            &nbsp;
        </div>
    </form>
</body>
</html>
