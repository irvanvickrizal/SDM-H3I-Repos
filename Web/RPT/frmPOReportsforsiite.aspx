<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOReportsforsiite.aspx.vb"
    Inherits="RPT_POReportsforsiite" %>

<%@ Register Src="../DashBoard/GraphicalReport.ascx" TagName="GraphicalReport" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PO Reports</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function DoPrint()
    {
        var str='';
        var disp_setting="toolbar=no,location=no,status=no,directories=yes,menubar=yes,"; 
      disp_setting+="scrollbars=yes,width=750, height=600, left=100, top=25"; 
        str+='<HTML>\n<head>\n'
        str+='<link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<body>\n'
        str+='<center>\n'+document.getElementById("dvPrint").innerHTML+'\n</center></body>\n</HTML>'
        var windowUrl = 'about:blank';
         var uniqueName = new Date();
         var windowName = 'Print' + uniqueName.getTime();
        var printWindow = window.open(windowUrl,windowName,disp_setting);
        printWindow.document.write(str);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
       
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="71%" border="0">
            <tr>
                <td>
                    <table border="0" cellpadding="2" cellspacing="3" style="width: 98%">
                        <tr>
                            <td style="width: 18%" class="lblTitle">
                                Select PoNo</td>
                            <td style="width: 1%">
                                :</td>
                            <td>
                                <asp:DropDownList ID="DDPoDetails" runat="server" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvPrint" style="height: 450px; overflow-y: scroll;" runat="server">
                        <table cellpadding="0" border="0" cellspacing="0" width="98%">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="1" cellspacing="2" class="ContentText" width="100%">
                                        <tr>
                                            <td align="left">
                                                <img src="../Images/nsn-logo.gif" border="0" />
                                            </td>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td align="right">
                                                <img src="../Images/logo_tsel.png" border="0" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="pageTitle">
                                                TI SUPPORTING CHECKLIST</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td id="Td1" runat="server" class="lblText">
                                                Totals
                                            </td>
                                            <td id="Td2" runat="server" class="lblText">
                                                % Completed</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Total Active Sites</td>
                                            <td style="width: 1%">
                                                :</td>
                                            <td id="lblSiteName" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSite" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td3" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSite" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sites Integrated</td>
                                            <td>
                                                :</td>
                                            <td id="lblSiteNo" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSiteIntegrated" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td4" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSiteIntegrated" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Site Documents Not Started</td>
                                            <td>
                                                :</td>
                                            <td id="lblArea" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSiteDocumentStarted" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td5" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSiteDocumentStarted" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Site Documents in Progress</td>
                                            <td>
                                                :</td>
                                            <td id="lblWork" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSiteDocumentsProgress" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td6" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSiteDocumentsProgress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Site Documents Completed</td>
                                            <td>
                                                :</td>
                                            <td id="lblPONo" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSiteDocCompleted" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td7" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSiteDocCompleted" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sites Bast</td>
                                            <td>
                                                :</td>
                                            <td id="Td8" runat="server" class="lblText">
                                                <asp:Label ID="lblTotalSitesBast" runat="server"></asp:Label>
                                            </td>
                                            <td id="Td9" runat="server" class="lblText">
                                                <asp:Label ID="lblAvgSitesBast" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 44px">
                                                Average Lead time from Integration to First Doc uploaded (Hours)</td>
                                            <td style="height: 44px">
                                                :</td>
                                            <td id="Td10" runat="server" class="lblText" colspan="2" style="height: 44px">
                                                <asp:Label ID="lblIntegrationFirstUpload" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Average Lead time from Integration to Last Doc approved (Hours)
                                            </td>
                                            <td>
                                                :</td>
                                            <td id="Td11" runat="server" class="lblText" colspan="2">
                                                <asp:Label ID="lblLastDocApproved" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlReportSite" runat="server">
                                                    <asp:Image ID="Pichart" runat="server"/>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" style="clip: rect(auto 100px auto auto)">
                    <input id="Button1" type="button" value="   Print   " onclick="DoPrint()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
