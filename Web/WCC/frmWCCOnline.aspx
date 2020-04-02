<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWCCOnline.aspx.vb" Inherits="WCC_frmWCCOnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Online</title>
    <style type="text/css">
        .HeaderPanel
        {
            width:800px;
            height:60px;
        }
        .HeaderTitle
        {
            font-family:Verdana;
            font-size:14px;
            weight:bold;    
        }
        .ltrLabel
        {
            font-family:Verdana;
            font-size:8px;
            weight:bold;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="HeaderPanel">
            <table>
                <tr>
                    <td style="width: 20%;">
                        <img src="~/images/NSNLogo_New.png" runat="server" id="NSNLogo" alt="nsnlogo_" />
                    </td>
                    <td style="width: 80%;" class="">
                        <asp:Literal ID="LtrWCCTitle" runat="server" Text="Work Completion Certificate"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <p>
                This is to officially certify that nominated Partner Company has accomplished the
                assigned works according to the details ordered with complete/full required
            </p>
        </div>
        <div>
            <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%">
                <tr>
                    <td class="Hcap" colspan="5">
                    </td>
                </tr>
                <tr>
                    <td align="left" class="lblText" style="width: 25%">
                        &nbsp;Subcontractor Name</td>
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
                        Date Issued</td>
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
                        &nbsp;Site ID (EPM)</td>
                    <td valign="top" style="width: 9px; height: 20px">
                        <span style="font-size: 8pt; font-family: Arial Unicode MS">:</span></td>
                    <td class="lblText" style="height: 20px; width: 433px;">
                        <input id="txtSiteId" runat="Server" class="textFieldStyle" type="text" style="width: 100px" />
                    </td>
                    <td class="lblText" style="height: 20px">
                        <asp:Label ID="lblSiteID" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblText" style="height: 20px">
                        &nbsp;Site Name (EPM)</td>
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
                        &nbsp;Work Package ID (EPM)</td>
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
                        &nbsp;Work Description</td>
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
                    <td class="lblText" style="height: 21px">
                        &nbsp;Start Date</td>
                    <td valign="top" style="width: 9px; height: 21px;">
                        :</td>
                    <td class="lblText" style="width: 433px; height: 21px;">
                        <input id="txtStartDate" runat="Server" class="textFieldStyle" type="text" style="width: 125px" />
                        <asp:ImageButton ID="imgstdate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" /></td>
                    <td class="lblText" style="height: 21px">
                        <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblText">
                        &nbsp;Completion Date</td>
                    <td valign="top" style="width: 9px">
                        :</td>
                    <td class="lblText" style="width: 433px">
                        <input id="txtCompletionDate" runat="Server" class="textFieldStyle" type="text" style="width: 125px" />
                        <asp:ImageButton ID="imgcmpdate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" /></td>
                    <td class="lblText">
                        <asp:Label ID="lblCompletionDate" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblText" style="height: 20px">
                        &nbsp;Delay Date from PO</td>
                    <td valign="top" style="width: 9px; height: 20px;">
                        :</td>
                    <td class="lblText" style="width: 433px; height: 20px">
                        <input id="txtDelayDateFromPo" runat="Server" class="textFieldStyle" type="text"
                            style="width: 125px" />
                        <asp:ImageButton ID="imgdelaydate" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                            Width="18px" /></td>
                    <td class="lblText" style="height: 20px">
                        <asp:Label ID="lblDelayDateFromPo" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="lblText">
                        &nbsp;PO Partner</td>
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
                        &nbsp;PO Telkomsel</td>
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
                        <input id="Text1" runat="Server" class="textFieldStyle" type="text"
                            style="width: 100px" />
                    </td>
                    <td class="lblText" style="height: 20px">
                        <asp:Label ID="Label1" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div>
            
        </div>
    </form>
</body>
</html>
