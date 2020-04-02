<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccViewLog.aspx.vb" Inherits="WCC_frmWccViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table cellpadding="1" border="0" cellspacing="1" width="100%">
            <tr>
                <td class="pageTitle" colspan="4" id="Td1">
                    Wcc ViewLog</td>                                
            </tr>
             <tr class="lblText">
                    <td style="width:20%">PoNo</td>
                    <td style="width:1%">:</td>                   
                  <td colspan="2" id="tdpono" runat="server"></td> 
                </tr>
                <tr class="lblText">
                    <td>SiteNo</td>
                    <td>:</td>
                      <td colspan="2" id="tdsiteno" runat="server"></td> 
                </tr>
                <tr class="lblText">
                    <td>SiteName</td>
                    <td>:</td>
                     <td colspan="2" id="tdsitename" runat="server"></td> 
                </tr>
             <tr class="lblText">
                 <td>Scope</td>
                 <td>
                     :</td>
                 <td runat="server" id="tdscope" colspan="2">
                 </td>
             </tr>
                <tr class="lblText">
                 <td>Work Pkg ID</td>
                 <td>:</td>
                   <td colspan="2" id="tdwpid" runat="server"></td> 
             </tr>
             <tr class="lblText">
                 <td>Work Pkg Name</td>
                 <td>:</td>
                  <td colspan="2" id="tdwpname" runat="server"></td> 
             </tr>
             
            <tr class="lblText">
                <td>Document</td>
                <td>:</td>
                  <td colspan="2" id="tddocument" runat="server"></td> 
            </tr>
            
             <tr class="lblText" valign="top" ><td>View Log</td><td>:</td>
                <%-- <td colspan="2" runat="server" id="tdaudit"></td>--%>
                 <td id="Td2" colspan="2" runat="server" align="left"> </td> 
             </tr>                                       
                   <tr align="left">
                   <td colspan="4" align="left">
                     <div style="overflow: auto; height: 250px">
                    <asp:GridView ID="gvSearch" runat="server" BackColor="White"  CssClass="GridOddRows" BorderWidth="1px" 
                            ForeColor="Black" GridLines="Vertical"  AutoGenerateColumns="false" EmptyDataText="No Records Found">
                            <FooterStyle BackColor="#CCCCCC" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />                            
                            <HeaderStyle CssClass="GridHeader" />                                                   
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " ItemStyle-BorderWidth="1">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>    
                                        </ItemTemplate>                    
                                 </asp:TemplateField>
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="AuditInfo" ItemStyle-BorderWidth="1" />
                                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="User"  ItemStyle-BorderWidth="1" />
                                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="UserType" ItemStyle-BorderWidth="1" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Role" DataField="UserRole" ItemStyle-BorderWidth="1" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Start Date Time" DataField="EventStart" ItemStyle-BorderWidth="1" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="End Date Time" DataField="EventEnd" ItemStyle-BorderWidth="1" />                                 
                                 <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Remarks" DataField="Remarks" ItemStyle-BorderWidth="1" />                                 
                            </Columns>
                        </asp:GridView>
                        </div>
                          </td>
                   </tr>       
            <tr><td colspan="4" align="right"><input type="button" runat="server" id="btnClose" class="buttonStyle" value="Close" onclick="javascript:window.close()"/></td>              
            </tr> 
        </table>
        
        </div>
    </form>
</body>
</html>
