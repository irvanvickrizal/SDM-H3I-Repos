<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewCancelSiteDetails.aspx.vb" Inherits="ViewCancelSiteDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Site details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="width: 100%" >
    <table id="PO" border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td align="left" colspan="2" class="pageTitle">EPM Details</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 13px">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr><td class="lblTitle">Purchase Order No</td>
                <td style="width:1%">:</td>
                <td id="lblPONo" runat="server"></td>
                </tr>
                    <tr>
                        <td class="lblTitle">SiteNo</td>
                        <td style="width:1%">:</td>                          
                        <td id="txtSiteno" runat="server"></td>
                    </tr>
                    <tr>
                        <td class="lblTitle">SiteName</td>
                        <td style="width:1%">:</td>
                        <td id="txtSiteName" runat="server">
                        </td>
                    </tr>
                    <tr>
                        <td class="lblTitle">
                            Contract Date</td>
                        <td style="width:1%">
                            :</td>
                        <td id="txtContractDt" runat="server">
                        </td>
                    </tr>
                </table>
                </td>
            </tr> 
            <tr>
                <td width="50%" valign="top">
        <asp:Panel ID="Panel15" runat="server" GroupingText="Site Info">
         <table border="0" cellpadding="1" cellspacing="1">
                        <tr><td class="lblTitle">PhaseTI</td>
                <td style="width:1%">:</td>
                <td id="txtephaseti" runat="server"></td>               
            </tr>
            <tr>
                <td class="lblTitle">
                    TI SubCon</td>
                <td style="width:1%">:</td>
                <td id="txtetisubcon" runat="server"></td>
            </tr>
             <tr>
                 <td class="lblTitle">
                     Work Package ID</td>
                 <td style="width:1%">:</td>
                 <td id="txteWorkPackageID" runat="server"></td>
             </tr>
            <tr>
                 <td class="lblTitle">
                     Site Integration Date</td>
                 <td style="width:1%">:</td>
                 <td id="txteSiteIntegration" runat="server"></td>
             </tr>
            <tr>
                 <td class="lblTitle">
                     Acceptance On Air</td>
                 <td style="width:1%">:</td>
                 <td id="txteSiteAcpOnAir" runat="server"></td>
             </tr>
            <tr>
                 <td class="lblTitle">
                     Acceptance On &nbsp;Bast</td>
                 <td style="width:1%">:</td>
                 <td id="txteSiteAcpOnBast" runat="server"></td>
             </tr>   
        </table>
        </asp:Panel>
        </td>
        <td valign="top" width="50%">
     <asp:Panel ID="Panel16" runat="server" GroupingText="Package Info">
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Package Type</td>
                <td style="width:1%">:</td>
                <td id="txtePackageType" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Package Name</td>
                <td style="width:1%">:</td>
                <td id="txtePackageName" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Package Status</td>
                <td style="width:1%">:</td>
                <td id="txtePackageStatus" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Customer PONo</td>
                <td style="width:1%">:</td>
                <td id="txteCustomerPONO" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">
                    Customer PO Received Date</td>
                <td style="width:1%">:</td>
                <td id="txteCustomerPORecDate" runat="server" ></td>
            </tr>
        </table>        
        </asp:Panel>
        </td>
            </tr>
               <tr>
                <td align="left" colspan="2" class="pageTitle">Original Configuration</td>
            </tr>         
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel17" runat="server" GroupingText="WorkPackage Info">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="lblTitle">Work Package Id</td>
                        <td style="width:1%">:</td>
                        <td id="txtWPKGId" runat="server" >
                        </td>
                    </tr>
                    <tr>
                        <td class="lblTitle">Work Package Name</td>
                        <td style="width:1%">:</td>
                        <td id="txtWPName" runat="server"></td>
                    </tr>
                </table>
            </asp:Panel>
    </td>  
         <td valign="top" runat="server">
        <asp:Panel ID="Panel5" runat="server" GroupingText="Hardware Info"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Hardware</td>
                <td style="width:1%">:</td>
                <td  id="txtHW" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Hardware Code</td>
                <td style="width:1%">:</td>
                <td  id="txtCode" runat="server" ></td>
            </tr>
        </table>
       </asp:Panel>
        </td></tr>   
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel3" runat="server" GroupingText="Configuration Info">
         <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">Band </td>
                <td style="width:1%">:</td>
                <td  id="txtBandType" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Band Type</td>
                <td style="width:1%">:</td>
                <td id="txtBand" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Configuration</td>
                <td style="width:1%">:</td>
                <td  id="txtConfig" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 900</td>
                <td style="width:1%">:</td>
                <td  id="txtP900" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 1800</td>
                <td style="width:1%">:</td>
                <td  id="txtP1800" runat="server" ></td>
            </tr>      
        </table>
        </asp:Panel>
        </td>
        <td valign="top">
            <asp:Panel ID="Panel4" runat="server" GroupingText="Antenna Info" > 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Antenna Name</td>
                <td style="width:1%">:</td>
                <td id="txtAntName" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Antenna Quantity</td>
                <td>:</td>
                <td id="txtAntQty" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Length</td>
                <td style="width:1%">:</td>
                <td id="txtFedLen" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Type</td>
                <td>:</td>
                <td id="txtFedType" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Quantity</td>
                <td style="width:1%">:</td>
                <td id="txtFedQty" runat="server"></td>
            </tr>                        
        </table>
        </asp:Panel>
    </td>           
                   </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" GroupingText="Scope Info">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">
                    Scope</td>
                <td style="width:1%">:</td>
                <td id="txtFldType" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Description</td>
                <td style="width:1%">:</td>
                <td id="txtDesc" runat="server"></td>
            </tr>
        </table>
        </asp:Panel>
        </td>
                <td valign="top">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="Value Info"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr id="Tr4" runat="Server">
                <td class="lblTitle">Value in USD</td>
                <td style="width:1%">:</td>
                <td id="txtValue1" runat="server"></td>
            </tr>            
            <tr id="Tr5" runat="Server">
                <td class="lblTitle">Value in IDR</td>
                <td style="width:1%">:</td>
                <td  id="txtValue2" runat="server"></td>
            </tr>  
        </table>
        </asp:Panel>
        </td>
            </tr>
           <tr>       
            <td colspan="2" valign="top">
                <asp:Panel ID="Panel8" runat="server" GroupingText="Remarks">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr id="Tr1" runat="Server">
                        <td class="lblTitle">Remarks</td>
                        <td style="width:1%">:</td>
                        <td id="txtRemarks" runat="server">
                </td>
                    </tr>
                </table>
            </asp:Panel>
                </td>
        </tr>
               </table> 
              <table id="tblMom" runat="Server" border="0" cellpadding="1" cellspacing="1" width="100%">
               <tr>
                <td align="left" colspan="2" class="pageTitle">Changed Configuration</td>
            </tr>         
            <tr>
                <td style="width:50%" valign="top">
     <asp:Panel ID="Panel1" runat="server" GroupingText="WorkPackage Info">
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Work Package Id</td>
                <td style="width:1%">:</td>
                <td id="txtmwpkgid" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Work Package Name</td>
                <td style="width:1%">:</td>
                <td id="txtmwpname" runat="server" ></td>
            </tr>
        </table>        
        </asp:Panel>
        </td>  
         <td width="50%" valign="top">
        <asp:Panel ID="Panel9" runat="server" GroupingText="Hardware Info"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Hardware</td>
                <td style="width:1%">:</td>
                <td  id="txtmhw" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Hardware Code</td>
                <td>:</td>
                <td  id="txtmcode" runat="server" ></td>
            </tr>
        </table>
       </asp:Panel>
        </td></tr>              
            <tr>
                <td valign="top">
        <asp:Panel ID="Panel12" runat="server" GroupingText="Configuration Info">
         <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">Band Type</td>
                <td style="width:1%">:</td>
                <td  id="txtmbandtype" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Band</td>
                <td>:</td>
                <td id="txtmband" runat="server" ></td>
            </tr>
            <tr>
                <td class="lblTitle">Configuration</td>
                <td style="width:1%">:</td>
                <td  id="txtmConfig" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 900</td>
                <td>:</td>
                <td  id="txtmp900" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 1800</td>
                <td style="width:1%">:</td>
                <td  id="txtmp1800" runat="server" ></td>
            </tr>      
        </table>
        </asp:Panel>
        </td>
                <td valign="top">
        <asp:Panel ID="Panel13" runat="server" GroupingText="Antenna Info" > 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle">Antenna Name</td>
                <td style="width:1%">:</td>
                <td id="txtmantname" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle" style="height: 18px">Antenna &nbsp;Quantity</td>
                <td>:</td>
                <td id="txtmantqty" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Length</td>
                <td style="width:1%">:</td>
                <td id="txtmfedlen" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Type</td>
                <td style="width:1%">:</td>
                <td id="txtmfedtype" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Quantity</td>
                <td style="width:1%">:</td>
                <td id="txtmfedqty" runat="server"></td>
            </tr>            
        </table>
        </asp:Panel>
        </td>
            </tr>
            <tr>
            <td valign="top">
        <asp:Panel ID="Panel10" runat="server" GroupingText="Scope Info">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">
                    Scope</td>
                <td style="width:1%">:</td>
                <td id="txtmfldtype" runat="server"></td>
            </tr>
            <tr>
                <td class="lblTitle">Description</td>
                <td style="width:1%">:</td>
                <td id="txtmdesc" runat="server"></td>
            </tr>
        </table>
        </asp:Panel>
        </td>
            <td valign="top">
        <asp:Panel ID="Panel11" runat="server" GroupingText="Value Info"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr id="tr2" runat="Server">
                <td class="lblTitle">
                    Value in &nbsp;USD</td>
                <td style="width:1%">:</td>
                <td id="txtmvalue1" runat="server"></td>
            </tr>            
            <tr id="tr3" runat="Server">
                <td class="lblTitle">
                    Value in &nbsp;IDR</td>
                <td style="width:1%">:</td>
                <td  id="txtmvalue2" runat="server"></td>
            </tr>  
        </table>
        </asp:Panel>
    </td>
            </tr>
           <tr>
            <td colspan="2" valign="top"><asp:Panel ID="Panel14" runat="server" GroupingText="Remarks">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td class="lblTitle">Remarks</td>
                        <td style="width:1%">:</td>
                        <td id="txteRemarks" runat="server"></td>
                    </tr>
                </table>
            </asp:Panel>
            </td>          
        </tr>         
               </table> 
               <table width="100%"><tr><td align="right">  <input id="btnClose" runat="server" class="buttonStyle"
            type="button" value="Close" onclick="window.close();" /></td></tr>
               </table>
       </div>        
        </form>
</body>
</html>
