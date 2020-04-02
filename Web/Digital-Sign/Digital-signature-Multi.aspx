<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Digital-signature-Multi.aspx.vb" Inherits="Digital_signature_multi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 
<script language="javascript" type="text/javascript">
function WindowsClose() 
{ 
    alert('Signed Sucessfully.');
    window.opener.location.href = '../dashboard/Approve-Documents.aspx?time='+(new Date()).getTime();
  
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();

 
} 
function getQueryVariable(variable)
{
var query = window.location.search.substring(1);
var vars = query.split("&");
for (var i=0;i<vars.length;i++)  {
var pair = vars[i].split("=");
if (pair[0] == variable)
{
return pair[1];
}
}
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center"><iframe runat="server" id="PDFViwer" width="99%" height="500px" scrolling="auto"></iframe>
                </td>
            </tr>
            <tr>
                <td class="hgap">
               <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" Width="50%" AutoGenerateColumns="False" EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" /> 
            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle"/>
            <Columns>
                    <asp:TemplateField HeaderText=" Total ">
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                    <ItemTemplate>
                        <asp:Label ID="lblno" runat="Server"></asp:Label>    
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                <asp:BoundField DataField="docpath" HeaderText="Path" />
                <asp:BoundField DataField="DocName" HeaderText="Document" ItemStyle-HorizontalAlign="left" /> 
               <asp:BoundField DataField="SW_Id" />
                <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                            <asp:ListItem Selected="True">Approve</asp:ListItem>
                                            <asp:ListItem>Reject</asp:ListItem>
                                            </asp:RadioButtonList>  
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView> 
                    <asp:HiddenField ID="HDPath" runat="server" />
                    <asp:HiddenField ID="hdnx" runat="server" />
                    <asp:HiddenField ID="hdny" runat="server" />
                    <asp:HiddenField ID="hdpageNo" runat="server" />
                    <asp:HiddenField ID="HDPono" runat="server" />
                    <asp:HiddenField ID="HDDocid" runat="server" />
                </td>
               <td>
              
               </td> 
            </tr>
            <tr>
                <td align="center" class="hgap">
                    <asp:Label ID="lblLinks" runat="server"></asp:Label></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                   <table border="0" cellpadding="1" cellspacing="2" style="width: 50%">
                             <tr>
                        <td colspan="2" style="height:20px;" class="pageTitle">
                            Digital Signature Login</td>
                     </tr>
                        <tr>
                            <td class="lblTitle">
                                User Name<font style=" color:Red; font-size:16px"><sup> * </sup></font> </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textFieldStyle" ReadOnly="True" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator> <asp:LinkButton ID="lnkrequest" runat="server">Request Password</asp:LinkButton></td>
                        </tr>
                        <tr >
                            <td class="lblTitle">
                                Password <font style=" color:Red; font-size:16px"><sup> * </sup></font> </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textFieldStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                    ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="150px" CssClass="buttonStyle" />&nbsp;
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSign" />
                            </td>
                        </tr>
                            </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
