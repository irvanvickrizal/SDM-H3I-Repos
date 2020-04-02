<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccDigital-Signature.aspx.vb" Inherits="WCC_frmWccDigital_Signature" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function gopage(id)
    {
        var url='../BAUT/frmTI_WCTRBAST.aspx?id=' + id + '&from=bast'
       window.location= url;
    
    }
      
    
function WindowsClose() 
{ 
    alert('Signed Sucessfully.');
    var tskid=getQueryVariable('tskid');
         window.opener.location.href = '../dashboard/frmdocapproved.aspx?id='+tskid+'&time='+(new Date()).getTime();
         //alert(window.opener.progressWindow);
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();

 
} 
function WindowsClosenew() 
{ 
    alert('Reviewed Sucessfully.');
    var tskid=getQueryVariable('tskid');
         window.opener.location.href = '../dashboard/frmdocapproved.aspx?id='+tskid+'&time='+(new Date()).getTime();
         //alert(window.opener.progressWindow);
  if (window.opener.progressWindow)
 {
    window.opener.progressWindow.close()
  }
  window.close();

 
} 
function WindowsClose2() 
{ 
  var tskid=getQueryVariable('tskid');
    window.opener.location.href = '../dashboard/frmdocapproved.aspx?id='+tskid+'&time='+(new Date()).getTime();
  if (window.opener.progressWindow)
    window.opener.progressWindow.close()
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
function waitPreloadPage() 
{ //DOM
document.getElementById('prepage').style.display='';
    if (document.getElementById)
    {
        document.getElementById('prepage').style.visibility='hidden';
    }
    else{
        if (document.layers)
        { //NS4
            document.prepage.visibility = 'hidden';
        }
        else { //IE4
            document.all.prepage.style.visibility = 'hidden';
        }
    }
}
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
   <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td align="center" style="height: 510px">
                        <iframe runat="server" id="PDFViwer" width="99%" height="500px" scrolling="auto"></iframe>
                    </td>
                </tr>
                <tr>
                    <td class="hgap" style="width: 972px">
                        <asp:GridView ID="grdDocuments" runat="server" Width="50%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
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
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">Approve</asp:ListItem>
                                            <asp:ListItem Value="1">Reject</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
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
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="hgap" style="width: 972px; height: 160px;">
                        <asp:GridView ID="grdDocuments2" runat="server" Width="50%" AutoGenerateColumns="False"
                            EmptyDataText="No Records Found" DataKeyNames="Doc_Id" OnRowDataBound="grdDocuments2_RowDataBound" >
                            <PagerSettings Position="TopAndBottom"  />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblnotwo" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                <asp:BoundField DataField="docpath" HeaderText="Path" />
                                <asp:BoundField DataField="DocName" HeaderText="Document" ItemStyle-HorizontalAlign="left" />
                                <asp:BoundField DataField="SW_Id" />
                                <asp:BoundField DataField="site_id" />
                                <asp:BoundField DataField="pono" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rdbstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DOThis"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">Approve</asp:ListItem>
                                            <asp:ListItem Value="1">Reject</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" Columns="40" CssClass="textFieldStyle"
                                            Rows="5" TextMode="MultiLine" Visible="false">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td style="height: 160px">
                    </td>
                </tr>
               <tr id="reviewerid" runat="server" visible="false" >
                    <td class="hgap" style="width: 972px">
                        <asp:Label ID="lblreviewer" runat="server" Font-Names="Arial" ForeColor="Red"></asp:Label>
                       </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 972px" id="dgrow" runat="server">
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 50%">
                            <tr>
                                <td colspan="2" style="height: 20px;" class="pageTitle">
                                    Digital Signature Login</td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="height: 34px">
                                    User Name<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                                </td>
                                <td style="height: 34px">
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="textFieldStyle" ReadOnly="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                        ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator><br />
                                    <asp:LinkButton ID="lnkrequest" OnClientClick="this.style.display = 'none'; loadingdiv.style.display = '';"
                                        runat="server">Request Password</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td class="lblTitle">
                                    Password <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textFieldStyle"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="150px"
                                        CssClass="buttonStyle" OnClientClick="this.style.display = 'none'; dgdiv.style.display = '';" />&nbsp;<asp:Button ID="btnReject" runat="server" CssClass="buttonStyle"
                                            Text="Reject" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSign" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="rerow" runat="server">
                    <td>
                        <asp:Button ID="btnreview" runat="server" Text="Review" ValidationGroup="vgSign"
                            Width="150px" CssClass="buttonStyle" OnClick="btnreview_Click" /></td>
                </tr>
            </table>
        </div>
        <div id="loadingdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 91%; left: 18px;">
            <img src="../sendsms.GIF" runat="server" id="loading" />
        </div>
           <div id="dgdiv" runat="server" style="display: none; position: absolute; width: 95%;
            text-align: right; top: 100%; left: 18px;">
            <img src="../digital-signature.gif" runat="server" id="dgloading" />
        </div>
    </form>
</body>
</html>
