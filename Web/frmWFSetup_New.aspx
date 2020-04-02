<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWFSetup_New.aspx.vb" Inherits="frmWFSetup_New" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Master Workflow</title>
    <style type="text/css">
        .ltrLabel {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }

        .lblField {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }

        .lblFieldHeader {
            font-family: verdana;
            font-size: 10pt;
            font-weight: bolder;
            color: #000;
        }

        .HeaderGrid {
            font-family: Arial Unicode MS;
            font-size: 8pt;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
        }

        .oddGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid {
            font-family: Arial Unicode MS;
            font-size: 7.5pt;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .btnstyleold {
            font-family: verdana;
            font-size: 10px;
            font-weight: lighter;
            border-style: solid;
            padding: 4px;
            border-width: 1px;
            border-color: gray;
            background-color: gray;
            color: white;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div style="background-color: #c3c3c3; padding: 8px;">
            <span class="lblFieldHeader">Master Workflow Setup</span>
        </div>
        <div style="margin-top: 5px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="1" cellspacing="1">
                            <tr>
                                <td>
                                    <span class="ltrLabel">Process Flow Code <font style="Color: Red; font-size: 16px"><sup> * </sup></font>
                                    </span>
                                </td>
                                <td>
                                    <input id="txtWFCode" type="text" maxlength="2" class="ltrLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">Process Flow Name <font style="Color: Red; font-size: 16px"><sup> * </sup></font>
                                    </span>
                                </td>
                                <td>
                                    <input id="txtWFName" type="text" maxlength="20" class="ltrLabel" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="ltrLabel">SLA<font style="Color: Red; font-size: 16px"><sup> * </sup></font></span>
                                </td>
                                <td>
                                    <input id="txtTime" type="text" runat="server" maxlength="3" class="ltrLabel" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789');" />&nbsp;Hrs
                                </td>
                            </tr>
                            <tr>
                                <td class="ltrLabel">Distribution Group</td>
                                <td>
                                    <asp:DropDownList CssClass="ltrLabel" runat="server" ID="ddlGrp"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <asp:Label ID="LblErrorMessage" runat="server" CssClass="lblField"></asp:Label>
                        </div>
                        <div style="width: 400px; text-align: right;">
                            <asp:Button ID="BtnCreateNew" runat="server" Text="Create New" CssClass="btnstyleold" Width="80px" />
                        </div>
                    </div>
                    <div>
                        <div style="background-color: #c2c2c2; padding: 3px;">
                            <span class="lblFieldHeader">Master Workflow Definition Setup</span>
                        </div>
                        <div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
