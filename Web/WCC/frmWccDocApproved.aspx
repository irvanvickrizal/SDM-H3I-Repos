<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccDocApproved.aspx.vb" Inherits="WCC_frmWccDocApproved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Doc View</title>
            <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 

   <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
   <script language="javascript" type="text/javascript">
function WindowsClose() 
{ 
         window.location.href = '../frmdashboard.aspx?time='+(new Date()).getTime();
  

 
} 
function Reject(id)
{
    var tskid=getQueryVariable('id');
     window.open('Reject.aspx?tskid='+tskid+'&id='+id,'welcome2','width=550,height=200,resizable=yes,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

}
function Approve(id,docname,siteno,version,pono,swid)
{
    var tskid=getQueryVariable('id');
 //alert(docname)
     if(docname=='BAUT')
     {
                   window.open('../digital-sign/Digital-signature-baut.aspx?tskid='+tskid+'&id='+id+'&docname='+docname+'&version='+version+'&siteno='+siteno+'&pono='+pono+'&swid='+swid+'&time='+(new Date()).getTime(),'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
 
     }
     else
     {
            window.open('../WCC/frmWccDigital-Signature.aspx?tskid='+tskid+'&id='+id+'&docname='+docname+'&version='+version+'&siteno='+siteno+'&pono='+pono+'&swid='+swid+'&time='+(new Date()).getTime(),'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
           // window.open('../digital-sign/Digitalsignaturemain.aspx?tskid='+tskid+'&id='+id+'&docname='+docname+'&version='+version+'&siteno='+siteno+'&pono='+pono+'&swid='+swid+'&time='+(new Date()).getTime(),'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
          
        }
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
    <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                    Wcc Approval Document</td>
            </tr>            
        <tr>
            <td colspan="4">
             <asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" EmptyDataText="All documents approved" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
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
                <asp:BoundField DataField="DocName" HeaderText="Document" /> 
              
                <asp:BoundField DataField="doc_id" HeaderText="Document" />  
                <asp:BoundField DataField="siteno" HeaderText="Site No" />
                <asp:BoundField DataField="Pono" HeaderText="PoNo" />    
                 
                <asp:ButtonField CommandName="Delete" Text="Approve" ValidationGroup="grdValid" HeaderText="Approve" CausesValidation="True" />
         
                  <asp:TemplateField HeaderText="Approve">
                        <ItemTemplate>
                            <a href="#" onclick="Approve('<%# DataBinder.Eval(Container.DataItem,"sno") %>','<%# DataBinder.Eval(Container.DataItem,"DocName") %>','<%# DataBinder.Eval(Container.DataItem,"siteno") %>','<%# DataBinder.Eval(Container.DataItem,"siteversion") %>','<%# DataBinder.Eval(Container.DataItem,"pono") %>',<%# DataBinder.Eval(Container.DataItem,"sw_id") %>)">
                                Approve
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Reject">
                        <ItemTemplate>
                            <a href="#" onclick="Reject('<%# DataBinder.Eval(Container.DataItem,"sno") %>')">
                                Reject
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SW_Id" />
                        <asp:BoundField DataField="siteversion" HeaderText="version" /> 
                           <asp:BoundField DataField="Scope" HeaderText="Scope" /> 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="#" onclick="window.open('../po/frmViewLog.aspx?id=<%# DataBinder.Eval(Container.DataItem,"doc_id") %>&sid=<%# DataBinder.Eval(Container.DataItem,"siteid") %>-<%# DataBinder.Eval(Container.DataItem,"Scope") %>&PONo=<%# DataBinder.Eval(Container.DataItem,"PONo") %>','','Width=500,height=500');">
                                ViewLog
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>               
            </Columns>
        </asp:GridView>
            </td>
           
        </tr>
                  <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" size="50px"  value="Close" runat="server" class="buttonStyle" style="width: 100pt"  /></td>
            </tr>
        </table>
          <input id="hdnSort" type="hidden" runat="server" />
    </div>
    </form>
</body>
</html>
