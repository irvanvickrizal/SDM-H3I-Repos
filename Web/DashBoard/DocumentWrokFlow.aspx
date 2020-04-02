<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentWrokFlow.aspx.vb" Inherits="DocumentWrokFlow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" >
function WindowsClose() { 
    window.opener.location.href = '../frmdashboard.aspx';
    
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();
} 
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">     
            <tr>
                <td colspan="2" style="background-image: url(../Images/barbg.jpg) ;background-repeat: repeat-x; width:180px">
                    &nbsp;</td>
            </tr>       
            <tr>
                <td style="width:10px;" >
                </td>
            </tr>
            <tr>
                <td id="tdBast" runat="server">
                 <asp:GridView ID="grdDocument" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="doc_Id" EmptyDataText="Work flow set all the document"
                        PageSize="5" Width="100%" AllowPaging="True">
                        <PagerSettings Position="TopAndBottom" />
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                        <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                        <Columns>                            
                             <asp:TemplateField HeaderText="Document Name">
                                <ItemTemplate>
                                   <asp:Label ID="lblDocName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"docname") %>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="Section & Subsection Name">
                                <ItemTemplate>
                                   <asp:Label ID="lblSecName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SecName") %>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Work Flow">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddWF" runat="server" CssClass="selectFieldStyle" ValidationGroup='grdValid'>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddWF"
                                        ErrorMessage="Please select WorkFlow" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup='grdValid'>*</asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup='grdValid' DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                </ItemTemplate>
                             </asp:TemplateField> 
                            <asp:ButtonField CommandName="Delete" Text="Save" ValidationGroup="grdValid"& CausesValidation="false" />
                           
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" value="Close & Update" onclick="WindowsClose();"  /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
