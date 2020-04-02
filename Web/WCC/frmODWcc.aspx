<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmODWcc.aspx.vb" Inherits="WCC_frmODWcc"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="Stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
    function WindowsClose() 
    { 
  
       window.opener.location.href = '../dashboard/DashboardpopupBast.aspx?time='+(new Date()).getTime();
        if (window.opener.progressWindow)
        {
            window.opener.progressWindow.close()
        }
        window.close();
    }
    </script>
    
</head>
<body class="MainCSS">
    <form id="form1" runat="server">
        <asp:Label ID="lblPO" runat="server" Font-Bold="True"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table border="0" style="width: 100%">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 908px">
                        <tr>
                            <td style="height: 671px">
                                <div id="DivWCC" runat="server" style="width: 100%;">
                                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td align="left" style="width: 10%" valign="top">
                                                <img height="37" src="../Images/NSNLogo_New.png" alt="NSNLogo" width="105" />
                                            </td>
                                            <td align="center" class="lblBold" colspan="4" style="width: 67%" valign="top">
                                                <asp:Label ID="lblHeading" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Purple">Work Completion Certificate</asp:Label></td>
                                            <td align="right" style="width: 10%" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%">
                                        <tr>
                                            <td class="Hcap" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="lblText" style="width: 25%">
                                                Subcontractor Name</td>
                                            <td style="width: 9px">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtSubconractorName" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 200px" />
                                            </td>
                                            <td class="lblText" style="width: 74%">
                                                <asp:Label ID="lblSubcontractorName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 21px">
                                                WCC Issuance Date</td>
                                            <td style="width: 9px; height: 21px;" valign="top">
                                                :</td>
                                            <td class="lblText" style="width: 433px; height: 21px;">
                                                <input id="txtDateIssued" runat="Server" class="textFieldStyle" type="text" style="width: 127px" />
                                                <asp:ImageButton ID="imgdateissue" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" /></td>
                                            <td class="lblText" style="height: 21px">
                                                <asp:Label ID="lblDateIssued" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                Certificate Number</td>
                                            <td valign="top" style="width: 9px">
                                                :
                                            </td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtCertificateNumber" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 100px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblCertificateNumber" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                Site ID (EPM)</td>
                                            <td valign="top" style="width: 9px; height: 20px; font-family: Verdana;">
                                                <span style="font-size: 8pt;">:</span></td>
                                            <td class="lblText" style="height: 20px; width: 433px; font-family: Verdana;">
                                                <input id="txtSiteId" runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblSiteID" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                Site Name (EPM)</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtSiteName" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblSiteName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                Work Package ID (EPM)</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtWorkPackageID" runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                                                &nbsp;
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblWorkPackageId" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                Work Description</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtWorkDescription" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblWorkDescription" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                PO Subcontractor</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtPoPartner" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblPOPartner" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText">
                                                PO Telkomsel</td>
                                            <td valign="top" style="width: 9px">
                                                :</td>
                                            <td class="lblText" style="width: 433px">
                                                <input id="txtPoTelkomsel" runat="Server" class="textFieldStyle" type="text" style="width: 200px" />
                                            </td>
                                            <td class="lblText">
                                                <asp:Label ID="lblPoTelkomsel" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                Telkomsel BAUT/BAST Date</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtTelcomeSelBAUTDate" runat="Server" class="textFieldStyle" type="text"
                                                    style="width: 100px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="lblTelkomselBaut_BastDate" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height: 20px">
                                                WCTR Date</td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td class="lblText" style="height: 20px; width: 433px;">
                                                <input id="txtWCTRDate " runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                                                <asp:ImageButton ID="ImgWCTRDate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                                    Width="18px" />
                                            </td>
                                            <td class="lblText" style="height: 20px">
                                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="lblText" style="height:20px;">
                                                Workflow Type 
                                            </td>
                                            <td valign="top" style="width: 9px; height: 20px">
                                                :</td>
                                            <td>
                                                <asp:DropDownList ID="DdlWorkflow" runat="server" CssClass="lblText" AutoPostBack="true"></asp:DropDownList>
                                            </td>    
                                        </tr>
                                    </table>
                                </div>
                                <div id="SignaturePanel" style="margin-top: 5px;">
                                    <asp:Repeater ID="RptDigitalSign" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td style="width: 300px; text-align: center;">
                                                    <span class="lblCheckText">Title</span>
                                                </td>
                                                <td style="width: 250px; text-align: center;">
                                                    <span class="lblCheckText">Name</span>
                                                </td>
                                                <td style="width: 200px; text-align: center;">
                                                    <span class="lblCheckText">Signature</span>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="lblCheckText" style="vertical-align: middle;">
                                                    &nbsp;
                                                    <%#Container.DataItem("SignTitle")%>
                                                </td>
                                                <td class="lblCheckText" style="vertical-align: middle;">
                                                    &nbsp;
                                                    <%#Container.DataItem("name")%>
                                                </td>
                                                <td id="DgSign" runat="server" style="height: 40px; text-align: left;">
                                                    <div class="clearDiv">
                                                        &nbsp;</div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
