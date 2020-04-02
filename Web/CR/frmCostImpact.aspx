<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCostImpact.aspx.vb" Inherits="CR_frmCostImpact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
     function ViewMOM(id,Type)
     {
     //alert(id);
        var url;
        url='frmMOM.aspx?id='+id + '&Type='+Type
        window.open(url,'welcome','width=1000,height=500,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');
     }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="6000" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="tblCountry" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">                    
                        <tr class="pageTitle">
                            <td colspan="2">Enter Cost Impact</td>
                        </tr>
                        
                  <tr runat="server" id="rowPOSelect">
                    <td>
                    <asp:Panel ID="pnlSite" runat="server"  width="100%">       
               <table  id="tblSiteDetails" runat="server" width="100%" >
            
                <tr>
                    <td class="SubPageTitle">Site Details</td>    
                </tr>
            <tr>
           
           <td valign="top" align="left" style="width:200px">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Description">
                  
                      <table id="Table3"  runat="server" >
                      
                      <tr style="height:20px;">
                      <td></td>
                      </tr>
                     
                  <tr style="height:20px;">
                        <td class="lblTitleMOM">Site Id</td>   
                  </tr>
                 <tr style="height:20px;">
                    <td class="lblTitleMOM">Site Name</td>                           
                  </tr>                 
                <tr style="height:20px;">                   
                        <td class="lblTitleMOM">WorkPackage ID</td>     
                  </tr>
                  <tr style="height:20px;">                    
                         <td class="lblTitleMOM">WorkPackage Name</td>                   
                  </tr>
                   <tr style="height:20px;">                   
                         <td class="lblTitleMOM">Scope</td>                  
                    </tr>
                     <tr style="height:20px; border-style: solid">                    
                       <td class="lblTitleMOM">Description</td>                            
                    </tr>
                    <tr style="height:20px; border-style: solid">                   
                            <td class="lblTitleMOM">Band Type</td>   
                      </tr>
                    <tr style="height:20px">                    
                        <td class="lblTitleMOM">Band</td>                              
                    </tr>
                       <tr style="height:20px">
                         <td class="lblTitleMOM">Configuration</td>                       
                    </tr>
                        <tr style="height:20px">
                             <td class="lblTitleMOM">Purchase 900</td>               
                    </tr>
                        <tr style="height:20px">                    
                             <td class="lblTitleMOM">Purchase1800</td>                   
                    </tr>                                           
                   <tr style="height:20px">                     
                           <td class="lblTitleMOM">Hard Ware</td>      
                    </tr>
                    <tr style="height:20px">
                            <td class="lblTitleMOM">HardWare Code</td>                                      
                    </tr>                                      
                    <tr style="height:20px">
                             <td class="lblTitleMOM">Quantity</td>     
                    </tr>               
                    <tr>
                           <td class="lblTitleMOM">Antenna Name</td>
                    </tr>                
                    <tr style="height:20px">  
                             <td class="lblTitleMOM">Antenna Quantity</td>        
                    </tr>
                     <tr style="height:20px">
                            <td class="lblTitleMOM">Feeder Length</td>              
                    </tr>
                
                    <tr style="height:20px">
                            <td class="lblTitleMOM">Feeder Type</td>
                    </tr>
                     <tr style="height:20px">
                
                     <td class="lblTitleMOM">Feeder Quantity</td>                    
                </tr>
               <tr style="height:20px">
                
                <td class="lblTitleMOM">Value in USD</td>                      
               </tr>
               <tr style="height:20px">
                    <td class="lblTitleMOM">Value in IDR</td>                       
                </tr>
              
                   </table>
                                 
                   </asp:Panel>
                   </td>
            <td valign="top" align="left" runat="server">
            <asp:DataList ID="DataList1" RepeatDirection="Horizontal" runat="server" Width="100%">
                 
                 <ItemTemplate>
                    <asp:Panel ID="Panel2" runat="server" GroupingText='<%#Container.DataItem("sta")%>' Width="100%">
                   
                      <table id="Table2" runat="server" >
                    <tr style="height:20px" id="MOMRefNo" runat="server"> 
                        <td><strong> <%#Container.DataItem("MOM")%> </strong><a href="#" class="link" onclick="ViewMOM('<%# DataBinder.Eval(Container.DataItem,"MOMID") %>','V');"> <strong> <%#Container.DataItem("MOMRef")%> </strong></a></td>
                    </tr> 
                  <tr style="height:20px">
                         <td>: <%#Container.DataItem("SiteNo")%> </td> 
                  </tr>
                 <tr style="height:20px">
                        <td>: <%#Container.DataItem("SiteName")%> </td>                    
                  </tr>                 
                <tr style="height:20px">                   
                        <td>: <%#Container.DataItem("WorkPkgId")%> </td>  
                  </tr>
                  <tr style="height:20px">                    
                        <td>: <%#Container.DataItem("workPKGName")%></td>                    
                  </tr>
                   <tr style="height:20px">                   
                       <td>: <%#Container.DataItem("Scope")%> </td>
                    </tr>
                     <tr style="height:20px">                    
                        <td>: <%#Container.DataItem("Description")%> </td>  
                    </tr>
                    <tr>                   
                            <td>: <%#Container.DataItem("Band_Type")%> </td>         
                       </tr>
                    <tr style="height:20px">                    
                        <td>: <%#Container.DataItem("Band")%> </td>                    
                    </tr>
                       <tr style="height:20px">
                          <td>: <%#Container.DataItem("Config")%> </td>
                    
                    </tr>
                        <tr style="height:20px">
                             <td>: <%#Container.DataItem("Purchase900")%> </td>     
                    </tr>
                        <tr style="height:20px">                    
                           <td>: <%#Container.DataItem("Purchase1800")%> </td>                
                    </tr>                                           
                   <tr style="height:20px">                     
                           <td>: <%#Container.DataItem("BSSHW")%> </td>             
                    </tr>
                    <tr style="height:20px">
                             <td>: <%#Container.DataItem("BSSCode")%> </td>             
                    </tr>                    
                  <tr style="height:20px">
                            <td>: <%#Container.DataItem("Qty")%> </td>
                </tr>
                    <tr style="height:20px">
                            <td>: <%#Container.DataItem("AntennaName")%> </td>
                    </tr>               
                    <tr style="height:20px">
                            <td>: <%#Container.DataItem("AntennaQty")%> </td>
                    </tr>                
                    <tr style="height:20px">  
                             <td>: <%#Container.DataItem("FeederLen")%> </td>
                    </tr>
                     <tr style="height:20px">
                           <td>: <%#Container.DataItem("FeederType")%> </td>
                    </tr>
                
                    <tr style="height:20px">
                             <td>: <%#Container.DataItem("FeederQty")%> </td>
                    </tr>
              
                <tr style="height:20px">                              
                       <td>: <%# DataBinder.Eval(Container.DataItem, "Value1", "{0:F}") %></td>                           
                    </tr>
                <tr style="height:20px">
                       <td>: <%# DataBinder.Eval(Container.DataItem, "Value2", "{0:F}") %></td>
                </tr>
                
                   </table>                 
               
                   </asp:Panel>
                    </ItemTemplate>
                 </asp:DataList>
                   </td>
            
             <td valign="top" align="right">
                    <asp:Panel ID="pnlchangereq" runat="server" GroupingText="New" Width="100%" HorizontalAlign="left">
                   
                      <table id="Table1" runat="server" >
                      <tr style="height:20px">
                        <td></td>
                      </tr>
                  <tr style="height:20px">
                       <td id="txtSiteNo" runat="server"></td>
                  </tr>
                 <tr style="height:20px">
                   <td id="txtSiteName" runat="server"></td>                     
                  </tr>                 
                <tr style="height:20px">                   
                       <td id="txtWorkPkgID" runat="server"></td>
                  </tr>
                  <tr style="height:20px">                    
                       <td id="txtWorkPkgName" runat="server"></td>                    
                  </tr>
                   <tr style="height:20px">                   
                       <td id="txtFldType" runat="server"></td>
                    </tr>
                     <tr style="height:20px">                    
                        <td id="txtDesc" runat="server"></td>                    
                    </tr>
                    <tr style="height:20px">                   
                        <td id="txtBandType1" runat="server"></td>
                    </tr>
                    <tr style="height:20px">                    
                        <td id="txtBand" runat="server"></td>                    
                    </tr>
                       <tr style="height:20px">
                         <td id="txtconfig" runat="server"></td>
                    
                    </tr>
                        <tr style="height:20px">
                           <td id="txtPurchase900" runat="server"></td>
                    </tr>
                        <tr style="height:20px">                    
                            <td id="txtPurchase1800" runat="server"></td>                     
                    </tr>                                           
                   <tr style="height:20px">                     
                            <td id="txtBSSHW" runat="server" ></td>             
                    </tr>
                    <tr style="height:20px">
                            <td id="txtBSSCode" runat="server"></td>                    
                    </tr>                    
                  <tr style="height:20px">
                            <td id="txtQty" runat="server"></td>
                </tr>
                    <tr style="height:20px">
                            <td id="txtAntName" runat="server"></td>                             
                    </tr>               
                    <tr style="height:20px">
                           <td id="txtAntennaQty" runat="server"></td>
                    </tr>                
                    <tr style="height:20px">  
                             <td id="txtFeederLength" runat="server"></td>
                            
                    </tr>
                     <tr style="height:20px">
                           <td id="txtFeederType" runat="server"></td>                            
                    </tr>                
                    <tr style="height:20px">
                            <td id="txtFeederQty" runat="server"></td>
                    </tr>              
                    <tr style="height:20px">                
                       <td><input type="text" id="txtValue1" runat="server"  maxlength="50" /></td>
                    </tr>
                    <tr style="height:20px">                    
                     <td><input type="text" id="txtValue2" runat="server"  maxlength="50" /></td>                     
                      </tr>
                   </table>               
                   </asp:Panel>
                   </td>
                   
                   
                   </tr>
                </table>
                </asp:Panel>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="buttonStyle" OnClick="btnSave_Click1" />
                    </td>
                </tr>
              
              </td>
              </tr>
              </table>     
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
