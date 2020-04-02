<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewCRDocument.aspx.vb"
    Inherits="ViewDocument_frmViewCRDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CR Document</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lblBRText
        {
            font-family: Arial Unicode MS;
            font-size: 9pt;
            background-color:#cfcfcf;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnDocPath" runat="server" />
        <asp:HiddenField ID="hdnwpid" runat="server" />
        <asp:HiddenField ID="hdncrid" runat="server" />
        <asp:HiddenField ID="hdnpackageid" runat="server" />
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" scrolling="auto"></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblBRText" colspan="4">
                        <div id="divReviewer" runat="server" style="width: 100%; text-align: left; margin-left: 5px;">
                        </div>
                    </td>
                </tr>
                <tr id="listdocuments" runat="server">
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grddocuments" runat="server" Width="100%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="CRSiteDocId">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:Label ID="LblSubDocid" runat="server" Text='<%#Eval("CRSiteDocId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
