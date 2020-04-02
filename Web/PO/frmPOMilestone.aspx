<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOMilestone.aspx.vb" Inherits="PO_frmPOMilestone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PoMileStones</title>
    <script type="text/javascript">
        function Close()
        {
            window.close();
        }
    </script>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div  style="width: 100%" >
            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                <tr>
                    <td align="left" colspan="2" class="pageTitle">
                        Milestones Report
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right"><input type="button" value="Close" class="buttonStyle" onclick="javascript:Close()" /></td>
                </tr>
                <tr>
                    <td align="left" colspan="2" class="SubPageTitle">
                        Purchase Order Details
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlPOInfo" runat="server" GroupingText="Purchase Order Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">PO No</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblPONo" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                                <tr>
                                    <td class="lblTitle">Site No</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblSiteno" runat="server" CssClass="lblText"></asp:Label></td>  
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <%--Purchase order details--%>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" GroupingText="Site Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Work Package Id</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblPOworkpkgId" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                                <tr>
                                    <td class="lblTitle">Work Package Name</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblworkPkgname" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                                <tr>
                                    <td class="lblTitle">Contract Date</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblcontactdate" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                                <tr>
                                    <td class="lblTitle">Site Name</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblSitename" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                               <%-- <tr>
                                    <td class="lblTitle">Zone</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblZone" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>
                                <tr>
                                    <td class="lblTitle">Region</td>    
                                    <td>:</td>    
                                    <td><asp:Label ID="lblRegion" runat="server" CssClass="lblText"></asp:Label></td>    
                                </tr>--%>
                               
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" GroupingText="Hardware Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Hardware</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblHardware" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Hardware Code</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblHrdCode" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr height="35px">
                                    <td colspan="3"></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" GroupingText="Scope Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Scope</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblScope" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Description</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblDescription" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pnlvalueinfo" runat="server" GroupingText="Cost Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Value In USD</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblValueInUsd" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Value In IDR</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblvalueinidr" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" GroupingText="Band Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Band</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblBand" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Band Type</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblBandtype" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Configuration</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblConfiguration" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Purchase 900</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblPurch900" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Purchase 1800</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblPurch1800" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel6" runat="server" GroupingText="Antenna Info" CssClass="lblText">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td class="lblTitle">Antenna Name</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblAntennaname" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Antenna Quantity</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblAntennaqty" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Feeder Length</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblFeederlength" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Feeder Type</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblFeedertype" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="lblTitle">Feeder Quantity</td>
                                    <td>:</td>
                                    <td><asp:Label ID="lblFeederqty" runat="server" CssClass="lblText"></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <%--EPMInfo....--%>
                <tr>
                    <td align="left" colspan="2" class="SubPageTitle">
                        EPM Details
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel4" runat="server" GroupingText="EPM Info" CssClass="lblText">
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td class="lblTitle" style="width:50%">PhaseTI</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblPHseTI" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Work Package Id</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblworkpkgId" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Site Integration</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblSiteIntegration" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Site Acceptence On Air</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblsiteacponair" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Site Acceptence On BAST</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblSiteacponbast" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                               <td width="440"></td>
                                <td>
                                    <table border="0" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="lblTitle">Package Type</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblPkgType" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Package Name</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblPkgName" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Package Status</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblPkgStatus" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">Customer PO Received Date</td>
                                            <td style="width:1%">:</td>
                                            <td><asp:Label ID="lblcustporecvdt" runat="server" CssClass="lblText"></asp:Label></td>
                                        </tr>
                                    </table>    
                                </td>
                            </tr>
                        </table>
                      </asp:Panel>
                    </td>
                </tr>
                 <tr>
                    <td class="SubPageTitle" style="text-decoration:underline;padding-left:330px">
                        Milestones
                    </td>
                </tr>
               <%-- <tr id="emptyRow">
                    <td colspan="2">&nbsp</td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grdMilestones" runat="server" AutoGenerateColumns="false" BorderWidth="1px" Width="50%" >
                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                        <RowStyle CssClass="GridOddRows" />
                            <Columns>
                                <asp:BoundField HeaderText="Milestone" DataField="Milestone" ItemStyle-Height="20px" ItemStyle-VerticalAlign="middle" ItemStyle-Width="320px" />
                                <asp:BoundField HeaderText="Planned" DataField="Planned" ItemStyle-VerticalAlign="middle" ItemStyle-Width="90px" />
                                <asp:BoundField HeaderText="Forecast" DataField="Fortune" ItemStyle-VerticalAlign="middle" ItemStyle-Width="90px" />
                                <asp:BoundField HeaderText="Actual" DataField="Actual" ItemStyle-VerticalAlign="middle" ItemStyle-Width="90px" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right"><input type="button" value="Close" class="buttonStyle" onclick="javascript:Close()" /></td>
                </tr>
           </table>
        </div>
    </form>
</body>
</html>
