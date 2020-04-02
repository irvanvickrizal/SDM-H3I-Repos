<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SiteDetails.aspx.vb" Inherits="PO_SiteDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>sitedetailsforcancelation</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="width: 100%" >
    <table border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td align="left" colspan="2" class="pageTitle">
                    Site Details</td>
            </tr>
            <tr>
                <td colspan="2"   style="height: 13px">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr><td class="lblTitle" style="height: 18px">Purchase Order No</td>
                <td style="width: 3px; height: 18px;">:</td>
                <td id="lblPONo" runat="server" style="height: 18px" class="lblText"></td>
                </tr>
                </table>
                </td>
            </tr> 
            <tr>
                <td valign="top">
        <asp:Panel ID="Panel1" runat="server" GroupingText="Site Info" CssClass="lblText">
         <table border="0" cellpadding="1" cellspacing="1">
                        <tr>
                <td class="lblTitle">Site No</td>
                <td>:</td>
                <td id="txtSiteNo" runat="server" class="lblText"></td>
               
            </tr>
            <tr>
                <td class="lblTitle">Site Name</td>
                <td>:</td>
                <td id="txtSiteName" runat="server" class="lblText"></td>
            </tr>
             <tr>
                 <td class="lblTitle">
                     Contract Date</td>
                 <td>:</td>
                 <td id="txtContractDt" runat="server" class="lblText">
                    </td>
             </tr>
        </table>
        </asp:Panel>
        </td>
        <td valign="top">
     <asp:Panel ID="Panel6" runat="server" GroupingText="WorkPackage Info" Width="100%" CssClass="lblText">
        <table border="0" cellpadding="1" cellspacing="1" width="100%">
        <tr>
                <td class="lblTitle" style="width:35%">Work Package Id</td>
                <td style="width:1%">:</td>
                <td id="txtWPKGId" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Work Package Name</td>
                <td>:</td>
                <td id="txtWPName" runat="server" class="lblText"></td>
            </tr>
            <tr><td colspan="3"></td></tr>
        </table>        
        </asp:Panel>
        </td>
            </tr>
            <tr>
                <td valign="top">
        <asp:Panel ID="Panel2" runat="server" GroupingText="Scope Info" CssClass="lblText">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">Scope</td>
                <td>:</td>
                <td id="txtFldType" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Description</td>
                <td>:</td>
                <td id="txtDesc" runat="server" class="lblText"></td>
            </tr>
        </table>
        </asp:Panel>
        </td>
        <td>
        <asp:Panel ID="Panel5" runat="server" GroupingText="Hardware Info" Width="100%" CssClass="lblText"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle" style="width:35%">Hardware</td>
                <td style="width: 1%">:</td>
                <td  id="txtHW" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Hardware Code</td>
                <td style="width: 8px">:</td>
                <td  id="txtCode" runat="server" class="lblText"></td>
            </tr>
        </table>
       </asp:Panel>
        </td>
            </tr>
            <tr>
                <td valign="top">
        <asp:Panel ID="Panel3" runat="server" GroupingText="Configuration Info" CssClass="lblText">
         <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td class="lblTitle">Band Type</td>
                <td>:</td>
                <td  id="txtBandType" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Band</td>
                <td>:</td>
                <td id="txtBand" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Configuration</td>
                <td>:</td>
                <td  id="txtConfig" runat="server" style="height: 18px" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 900</td>
                <td>:</td>
                <td  id="txtP900" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Purchase 1800</td>
                <td>:</td>
                <td  id="txtP1800" runat="server" class="lblText"></td>
            </tr>      
        </table>
        </asp:Panel>
        </td>
                <td>
        <asp:Panel ID="Panel4" runat="server" GroupingText="Antenna Info" Width="100%" CssClass="lblText"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr>
                <td class="lblTitle" style="width:35%">Antenna Name</td>
                <td style="width:1%">:</td>
                <td id="txtAntName" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Antenna Qty</td>
                <td>:</td>
                <td id="txtAntQty" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Length</td>
                <td>:</td>
                <td id="txtFedLen" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Type</td>
                <td>:</td>
                <td id="txtFedType" runat="server" class="lblText"></td>
            </tr>
            <tr>
                <td class="lblTitle">Feeder Qty</td>
                <td>:</td>
                <td id="txtFedQty" runat="server" class="lblText"></td>
            </tr>            
        </table>
        </asp:Panel>
        </td>
            </tr>
            <tr>
                <td>
        <asp:Panel ID="Panel7" runat="server" GroupingText="Value Info" CssClass="lblText"> 
        <table border="0" cellpadding="1" cellspacing="1">
        <tr id="trValue1" runat="Server">
                <td class="lblTitle">
                    Value in USD</td>
                <td>:</td>
                <td id="txtValue1" runat="server" class="lblText"></td>
            </tr>            
            <tr id="trValue2" runat="Server">
                <td class="lblTitle">
                    Value in IDR</td>
                <td>:</td>
                <td  id="txtValue2" runat="server" class="lblText"></td>
            </tr>  
        </table>
        </asp:Panel>
    </td>
                <td></td>
            </tr>
        <tr>
            <td style="height: 40px"><asp:Panel ID="Panel8" runat="server" GroupingText="Remarks" CssClass="lblText">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr id="Tr1" runat="Server">
                        <td class="lblTitle">
                        </td>
                        <td>
                        </td>
                        <td id="Td1" runat="server" style="width: 3px">
                <asp:TextBox ID="txtremarks" runat="server" CssClass="textFieldStyle" TextMode="MultiLine" Width="218px"></asp:TextBox></td>
                    </tr>
                </table>
            </asp:Panel>
            </td>
            <td style="height: 40px">
                </td>
        </tr>
             <tr><td colspan="2" align="Center"><br />
                <asp:button id="btnCancel" runat="server" text="Cancel this Site" CssClass="buttonStyle" Width="129px" />
                <input type="button" id="btnBack" name="btnBack" runat="server" value="Back to List" class="buttonStyle" style="width: 79pt" />
                </td>
                </tr>
        </table>
            </div>
        </form>
</body>
</html>
