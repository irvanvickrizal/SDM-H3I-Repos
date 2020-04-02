<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLastSign.aspx.vb" Inherits="DashBoard_ViewLastSign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Last 30 Days Signed Document</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript"> 
    function ViewDocument(id)
    {

     window.open('../po/frmViewDocument.aspx?id='+id,'LastSignDocument','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');


    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                        View Sign Document</td>
                </tr>
                <tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <td>
                        <asp:GridView ID="grdDocuments" runat="server" DataKeyNames="sno" AutoGenerateColumns="False"
                            AllowSorting="True" EmptyDataText="View sign document" AllowPaging="True">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoSec" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document">
                                    <ItemTemplate>
                                        <a href="#" onclick='ViewDocument(<%# DataBinder.Eval(Container.DataItem,"sno") %>)'>
                                            <%# DataBinder.Eval(Container.DataItem,"DocumentName") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Pono" HeaderText="Pono"></asp:BoundField>
                                <asp:BoundField DataField="Siteno" HeaderText="Site NO"></asp:BoundField>
                                <asp:BoundField DataField="Scope" HeaderText="Scope"></asp:BoundField>
                                <asp:BoundField DataField="WorkPackageId" HeaderText="Work Package ID"></asp:BoundField>
                                <asp:BoundField DataField="UploadedDate" HeaderText="Sign Date"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
