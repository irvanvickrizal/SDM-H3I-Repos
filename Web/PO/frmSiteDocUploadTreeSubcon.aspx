<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocUploadTreeSubcon.aspx.vb"
    Inherits="frmSiteDocUploadTreeSubcon" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Styles.css" />
    <style type="text/css">
        .lblTextC
        {
        	font-family:Verdana;
            font-size:8pt;
            color:Green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <label id="lblError" runat="server"></label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblmsg" runat="server" Text="All Documents  Uploaded for this Site"
                            Font-Bold="True" Font-Names="Verdana" ForeColor="#004000" Visible="False"></asp:Label><br />
                        <asp:Button ID="btndone" runat="server" Text="Done" OnClick="btndone_Click" Visible="False"
                            CssClass="buttonStyle" />
                    </td>
                </tr>
                <tr class="pageTitle">
                    <td colspan="3">
                        Subcon
                        Site Document Upload
                    </td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Work Package ID<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        &nbsp;<br />
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" />
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top">
                        Required Documents<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top">
                        <asp:TreeView ID="TreeView1" runat="server" CssClass="tree" AutoGenerateDataBindings="false"
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
                <tr style="height: 5">
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;&nbsp;
    <asp:HiddenField ID="hdndocid" runat="server" />
    <asp:HiddenField ID="hdnsiteid" runat="server" />
    <asp:HiddenField ID="hdnpoId" runat="server" />
    <input type="hidden" runat="server" id="hdnBAUT" />
    <input type="hidden" runat="server" id="hdnBAST1" />
    <input type="hidden" runat="server" id="hdnBAST2" />
    </form>
</body>
</html>
