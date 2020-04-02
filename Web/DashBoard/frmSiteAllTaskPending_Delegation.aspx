<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteAllTaskPending_Delegation.aspx.vb"
    Inherits="DashBoard_frmSiteAllTaskPending_Delegation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard Delegation User</title>
    <style type="text/css">
        .labelText
        {
            font-family:verdana;
            font-size:9pt;
        }
        #leftPane
        {
            width:60%;
            Float:left;
            Border-style:none;
        }
        #rightPane
        {
            width:30%;
            Float:Right;
            Border-style:none;
        }
        .warningmessage
        {
            font-family:verdana;
            font-size:11px;
            font-weight:bolder;
            color:maroon;
        }
         .btnstyle
        {
            font-family:verdana;
            font-size:11px;
            background-color:#c3c3c3;
            color:#fff;
            padding:4px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
         .btnstyle2
        {
            font-family:verdana;
            font-size:11px;
            background-color:maroon;
            color:#fff;
            padding:4px;
            cursor:pointer;
            border-width:1px;
            border-color:white;
            border-style:solid;
            
        }
        #PleaseWait
        {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align:center;
            height : 100px;
            width:100px;
            background-image: url(../Images/animation_processing.gif);
            background-repeat: no-repeat;
            margin: 0 10%; margin-top: 10px;
        }
        #blur
        {
            width: 100%;
            background-color:#ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 1;
            height: 100%;
            position:fixed;
            top: 0;
            left: 0;
        }
    </style>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
        function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UP1">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/animation_processing.gif" style="vertical-align: middle" width="150"
                            height="150" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div>
                    <table>
                        <tr>
                            <td>
                                <span class="labelText"><b>User</b></span>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlDelegatedUser" runat="server" CssClass="labelText" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="BtnViewTaskPending" runat="server" Text="View Task Pending" CssClass="btnstyle" />
                                <asp:Button ID="BtnReActived" runat="server" Text="Re-active" CssClass="btnstyle2"
                                    OnClientClick="return confirm('Are you sure you want to re-active Task to this user?')" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:MultiView ID="MvCorePanel" runat="server">
                    <asp:View ID="VwTaskPending" runat="server">
                        <asp:HiddenField ID="HdnTaskPendingUserId" runat="server" />
                        <div id="MainPanel">
                            <div id="leftPane">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../images/tick.jpg" alt="tick" width="20" />
                                            </td>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">General Document
                                                    [Waiting Your Review / Approval] </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 5px; margin-left: 25px;">
                                    <table border="1" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS2GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC2GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC2GLinkDisabled" runat="server" Text="0" CssClass="labelText">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME2GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:LinkButton ID="LbtTI2GLink" runat="server" CssClass="labelText">
                                                </asp:LinkButton>
                                                <asp:Label ID="LblTI2GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS3GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC3GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC3GLinkDisabled" runat="server" Text="0" CssClass="labelText">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME3GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpTI3GLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblTI3GLinkDisabled" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../images/tick.jpg" alt="tick" width="20" />
                                            </td>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">QC Document
                                                    [Waiting Your Review / Approval] </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 5px; margin-left: 25px;">
                                    <table border="1" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS2GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblSIS2GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC2GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblSITAC2GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME2GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblCME2GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:LinkButton ID="LbtTI2GQCLink" runat="server" CssClass="labelText">
                                                </asp:LinkButton>
                                                <asp:Label ID="lblTI2GQCLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS3GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblSIS3GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC3GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblSITAC3GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME3GQCLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblCME3GQCLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpTI3GQCLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="lblTI3GQCLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../images/tick.jpg" alt="tick" width="20" />
                                            </td>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">ATP Document
                                                    [Waiting Your Review / Approval] </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 5px; margin-left: 25px;">
                                    <table border="1" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS2GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS2GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC2GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC2GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME2GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME2GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:LinkButton ID="LbtTI2GATPLink" runat="server" CssClass="labelText">
                                                </asp:LinkButton>
                                                <asp:Label ID="LblTI2GATPLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS3GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS3GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC3GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC3GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME3GATPLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME3GATPLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpTI3GATPLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblTI3GATPLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <img src="../images/tick.jpg" alt="tick" width="20" />
                                            </td>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 9.5pt; font-weight: bolder;">CR/CO Document
                                                    [Waiting Your Review / Approval] </span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="margin-top: 5px; margin-left: 25px;">
                                    <table border="1" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">Scope </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SIS </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">SITAC </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">CME </span>
                                            </th>
                                            <th style="background-color: #ff7f27;">
                                                <span style="font-family: Verdana; font-size: 10pt;">TI </span>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">2G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS2GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS2GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC2GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC2GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME2GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME2GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:LinkButton ID="LbtTI2GCRLink" runat="server" CssClass="labelText">
                                                </asp:LinkButton>
                                                <asp:Label ID="LblTI2GCRLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25px; text-align: center; background-color: #cfcfcf;">
                                                <span style="font-family: Verdana; font-size: 10pt; font-weight: bold;">3G</span>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSIS3GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSIS3GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpSITAC3GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblSITAC3GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpCME3GCRLink" runat="server" CssClass="labelText" Visible="false">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblCME3GCRLink" runat="server" Text="0" CssClass="labelText" Visible="true">
                                                </asp:Label>
                                            </td>
                                            <td style="width: 50px; text-align: center; background-color: #cfcfcf;">
                                                <asp:HyperLink ID="HpTI3GCRLink" runat="server" CssClass="labelText">
                                                </asp:HyperLink>
                                                <asp:Label ID="LblTI3GCRLink" runat="server" Text="0" CssClass="labelText"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="VwNotDelegated" runat="server">
                        <span class="warningmessage">You are not in user delegated</span>
                    </asp:View>
                    <asp:View ID="vwNotTaskPending" runat="server">
                        <span class="warningmessage">No Task Pending</span>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlDelegatedUser" />
                <asp:AsyncPostBackTrigger ControlID="BtnViewTaskPending" />
                <asp:AsyncPostBackTrigger ControlID="BtnReActived" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
