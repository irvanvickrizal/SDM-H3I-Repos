<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dashboard.aspx.vb" Inherits="RPT_dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>   
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">            
            <tr style="border:0px; background-image: url(../Images/newpixal.jpg) ;background-repeat: repeat-x; height:33px">
                
                <td valign="top"><img alt="" src="../Images/ov.jpg"/>
                </td>    
                <td align="right">
                    <asp:HiddenField ID="HdView" runat="server" Value="0" />
                    <asp:Button ID="btnSection" runat="server" Text="View Section Document Count " /></td>          
            </tr>
               <tr><td colspan="2" class="hgap"></td> </tr>
            <tr>
                <td colspan="2">
                         <asp:GridView ID="grdDashBoard" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="PoNo" EmptyDataText="No Records Found"
                Width="100%">
                <PagerSettings Position="TopAndBottom" />
                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="GridEvenRows" />
                <RowStyle CssClass="GridOddRows" />
                <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            
                                <%#DataBinder.Eval(Container.DataItem, "PoNo")%>
                                                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Site">
                      <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <asp:Label ID="lblTotalSite" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalSite") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Total Req Docs">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblTotalSection" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalDocument") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblUploaded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalUploadDocument") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NSN Approved">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblNSNApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NsnApproved") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Approved">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustomerApproved") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompleted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CompleteDocument") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remaining">
                        <ItemStyle HorizontalAlign="center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:Label ID="lblRemainingApproved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RemainingDocument") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle"/>
                        <ItemTemplate>
                            <asp:Label ID="lblPrentageTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Precentage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                </td>
            </tr>            
             <tr><td colspan="2" class="hgap"></td> </tr>
        </table>
      <input id="hdnSort" type="hidden" runat="server" />
        
    </div>
    </form>
</body>
</html>
