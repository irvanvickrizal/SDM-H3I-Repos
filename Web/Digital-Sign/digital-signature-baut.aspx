<%@ Page Language="VB" AutoEventWireup="false" CodeFile="digital-signature-baut.aspx.vb" Inherits="Digital_Sign_digital_signature_baut" EnableEventValidation = "false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Digital Sign</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>

<script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("DDLZN"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Zone should be select\n";
        }
        if (IsEmptyCheck(document.getElementById("txtNo").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }    
        if (IsEmptyCheck(document.getElementById("txtName").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }        
        if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }   
    }    
    function viewUser()
    {
        var aa;
        aa = window.showModalDialog('../USR/frmUserList.aspx?SelMode=true','','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');        
        if(typeof aa != 'undefined')
        {
            document.getElementById('hdnSSId').value = aa
            var bb = aa.split('####')        
            document.getElementById('hdnSupId').value = bb[0];
            document.getElementById('txtSSName').value = bb[1];
            //document.getElementById('hdnUserType').value = bb[2];      
        }    
    }
    function WindowsClose() 
    { 
        alert('Signed Sucessfully.');

        var tskid=getQueryVariable('tskid');
        window.opener.location.href = '../dashboard/frmdocapproved.aspx?id='+tskid+'&time='+(new Date()).getTime();;
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

<body>
  <form id="frmCODSiteSetup" runat="server">
    <div style="text-align:center ">
       <table border="0" cellpadding="1" cellspacing="2" style="width: 100%;text-align:left;">
           <tr>
               <td colspan="2"><iframe runat="server" id="PDFViwer" width="99%" height="500px" scrolling="auto"></iframe>
               
               </td>
           </tr>
           <tr>
               <td colspan="2">
           <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" Width="99%" AutoGenerateColumns="False" EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
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
                <asp:BoundField DataField="doc_id" HeaderText="doc"  />
                <asp:BoundField DataField="docpath" HeaderText="Path" />
                <asp:BoundField DataField="DocName" HeaderText="Document" >
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField> 
               <asp:BoundField DataField="SW_Id" />
                <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"  OnSelectedIndexChanged="DOThis" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value=0>Approve</asp:ListItem>
                                            <asp:ListItem Value=1>Reject</asp:ListItem>
                                            </asp:RadioButtonList>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField >
              <ItemTemplate>
              <asp:TextBox TextMode="MultiLine" ID="txtremarks" runat="server" Visible="false" CssClass="textFieldStyle" >
              </asp:TextBox>
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
           </tr>
           <tr>
               <td align="center" colspan="2">
                   <asp:Label ID="lblLinks" runat="server"></asp:Label></td>
           </tr>
           <tr>
               <td colspan="2">
                   <table border="0" cellpadding="1" cellspacing="2" style="width: 90%">
                       <tr>
                           <td class="pageTitle" colspan="2" style="height: 20px">
                               Digital Signature Login</td>
                       </tr>
                       <tr>
                           <td class="lblTitle">
                                User Name<font style="font-size: 16px; color: red"><sup> * </sup></font>
                           </td>
                           <td>
                               <asp:TextBox ID="txtUserName" runat="server" CssClass="textFieldStyle" ReadOnly="True" ></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                   ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator> <asp:LinkButton ID="lnkrequest" runat="server">Request Password</asp:LinkButton></td>
                       </tr>
                       <tr>
                           <td class="lblTitle">
                               
                                Password <font style="font-size: 16px; color: red"><sup><span style="color: #000000">* </span></sup>
                               </font>
                           </td>
                           <td>
                               <asp:TextBox ID="txtPassword" runat="server" CssClass="textFieldStyle" TextMode="Password"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                   ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator></td>
                       </tr>
                       <tr>
                           <td>
                           </td>
                           <td>
                               <asp:Button ID="BtnSign" runat="server" CssClass="buttonStyle" Text="Sign" ValidationGroup="vgSign"
                                   Width="150px" />
                               <asp:Button ID="BtnReject" runat="server" CssClass="buttonStyle" Text="Reject" ValidationGroup="vgSign"
                                   Width="150px" CausesValidation="False" />&nbsp;
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
