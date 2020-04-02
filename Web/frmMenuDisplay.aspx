<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMenuDisplay.aspx.vb"
    Inherits="frmMenuDisplay" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Display</title>
    <link href="CSS/Styles.css" type="text/css" rel="stylesheet" />
	<style type="text/css">
        .logoPanel {
            position: fixed;
            bottom: 0px;
            right: 0px;
        }
    </style>
    <script language="JavaScript" src="http://seal.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>

</head>
<body style="background-color: #528cd6">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" style="background-color: #528cd6; height: 100%" cellpadding="0"
            cellspacing="0">
            <tr>
                <td valign="top">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
                            <asp:TreeView ID="TreeView1" runat="server" CssClass="tree" ExpandDepth="0" MaxDataBindDepth="3"
                                AutoGenerateDataBindings="False" CollapseImageToolTip="" ExpandImageToolTip=""
                                ImageSet="Msdn" Target="frame1" NodeIndent="10" ForeColor="Navy" ShowLines="True"
                                OnTreeNodePopulate="TreeView1_TreeNodePopulate">
                                <ParentNodeStyle Font-Bold="True" />
                                <RootNodeStyle Font-Bold="True" Font-Underline="False" />
                                <HoverNodeStyle Font-Italic="True" Font-Underline="True" />
                                <SelectedNodeStyle BorderStyle="None" BorderWidth="1px" Font-Underline="False" HorizontalPadding="3px"
                                    VerticalPadding="2px" ForeColor="White" />
                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" HorizontalPadding="3px"
                                    NodeSpacing="1px" VerticalPadding="1px" />
                            </asp:TreeView>
                        </contenttemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top"></td>
            </tr>
        </table>
    </form>
    <!--
    SiteSeal Html Builder Code:
    Shows the logo at URL http://seal.networksolutions.com/images/basicsqgreen.gif
    Logo type is  ("NETSB")
    //-->

    <script language="JavaScript" type="text/javascript"> SiteSeal("http://seal.networksolutions.com/images/basicsqgreen.gif", "NETSB", "none");</script>	
</body>
</html>