<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewDocumentQC.aspx.vb"
    Inherits="PO_frmViewDocumentQC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    <title>View Document</title>
    <style type="text/css">
        a:link
        {
            font-family:verdana;
            font-size:8pt;
        }
        .btnRefresh
        {
            height:25px;
            width:90px;
            background-image: url(../Images/button/btnRefresh.gif);
            text-decoration:none;
        }
        .btnRefresh:hover
        {
            height:25px;
            width:90px;
            background-image: url(../Images/button/btnRefreshHOver.gif);
            text-decoration:none;
        }
        .btnRefresh:visited
        {
             height:25px;
             width:90px;
             background-image:url(../Images/button/btnRefreshActive.gif);
             text-decoration:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%;">
            <div style="height: 100%; width: 100%">
                <iframe runat="server" id="docView" width="100%" height="575px" scrolling="auto"></iframe>
            </div>
            <div>
                <asp:GridView ID="grdDocuments" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1"
                    EmptyDataText="No Records Found" DataKeyNames="Doc_Id" PageSize="2">
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <Columns>
                        <asp:TemplateField HeaderText="Additional Document">
                            <ItemTemplate>
                                <a href='../PO/frmViewDocumentQC.aspx?id=<%#Eval("SW_Id") %>' target="_blank" style="border-style: none;">
                                    <span style="font-family: Verdana; font-size: 9pt;">
                                        <%#Eval("DocName")%>
                                    </span></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Visible="false" />
            <div style="width: 100%; text-align: right; margin-right: 10px;">
                <asp:LinkButton ID="LbtRefresh" runat="server"
                    Style="text-decoration: none">
                            <div class="btnRefresh"></div>
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
