<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashboardpopupBautNew.aspx.vb" Inherits="DashboardpopupBautNew" EnableEventValidation = "false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ready For Baut</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
   <SCRIPT LANGUAGE="JavaScript">
function WindowsClose() 
{ 
   window.location.href = '../frmdashboard.aspx';

 
} 
function Approved(siteno,version)
{
   
     window.open('../digital-sign/customer-Digital-signature.aspx?siteno='+siteno+'&version='+version,'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');

}
function Approve(id,siteno,version,pono)
{
   
     var pono=pono.replace(' ','^');
     //window.open('../digital-sign/Digital-signature-Baut.aspx?siteno='+siteno+'&version='+version,'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
     window.open('../BAUT/frmTI_BAUT.aspx?id='+id+'&siteno='+siteno+'&version='+version+'&pono='+pono,'welcome3','width=950,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');

}
</SCRIPT> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">     
            <tr>
                <td colspan="2" style="background-image: url(../Images/barbg.jpg) ;background-repeat: repeat-x; width:180px">
                <img alt="" src="../Images/ReadyforBaut.jpg" />
                </td>
            </tr>       
             <tr>
                <td class="hgap"></td>
            </tr>
            <tr>
                <td id="Td1" runat="server">
                       <asp:GridView ID="grddocuments" runat="server" AllowPaging="True" EmptyDataText="All documents approved" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="siteno" PageSize="100">
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
               <asp:BoundField DataField="Pono" HeaderText="PoNo" />  
                
                  <asp:TemplateField HeaderText="Site NO">
                        <ItemTemplate>
                            <a href="#" onclick="Approve(<%# DataBinder.Eval(Container.DataItem,"sw_id") %>,'<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>,'<%# DataBinder.Eval(Container.DataItem,"PONo") %>')">
                                <%# DataBinder.Eval(Container.DataItem,"siteno") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Site NO">
                        <ItemTemplate>
                            <a href="#" onclick="Approved(<%# DataBinder.Eval(Container.DataItem,"sw_id") %>,'<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>)">
                                <%# DataBinder.Eval(Container.DataItem,"siteno") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:BoundField DataField="Scope" HeaderText="Scope" /> 
                      <asp:BoundField DataField="subdate" HeaderText="Ready for BAUT Date" />
            </Columns>
        </asp:GridView></td>
            </tr>
            <tr>
                <td class="hgap"></td>
            </tr>
               <tr>
                <td align="right">
                    <input id="BtnClose" type="submit" value="Close" runat="server"  /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
