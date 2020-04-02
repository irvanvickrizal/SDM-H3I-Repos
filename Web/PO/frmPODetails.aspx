<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPODetails.aspx.vb" Inherits="PO_frmPODetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <script language="javascript" type="text/javascript">
    function viewMS()
    {
        var wpid = document.getElementById("hdnwpid").value;
        if (wpid > 0)
        {
        window.open('frmPOMileStone.aspx?id='+wpid,'welcome','width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes,resizable=yes');
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%">
        <input type="hidden" id="hdnwpid" runat="server" />
        <table border="0" cellpadding="1" cellspacing="1" width="100%">
            <tr>
                <td align="left" colspan="2" class="pageTitle">
                    Purchase Order Details
                </td>
            </tr>
            <tr id="rowMilestonesreport" runat="server">
                <td colspan="3" align="right">
                    <a id="A1" runat="server" href="#" onclick="viewMS();" class="ASmall">Milestones Report</a>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" GroupingText="Purchase Order Info" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    Purchase Order No
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td id="lblPONo" runat="server" class="lblText">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    PO Name
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td id="lblponame" runat="server" class="lblText">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    HOT As Per PO
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td class="lblText">
                                    <asp:TextBox ID="TxtHOTAsPerPo" runat="server" CssClass="textFieldStyle" Width="133px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    Site ID (EPM)
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td id="lblSiteNo" runat="Server" class="lblText">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    Site Name (EPM)
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td id="lblsitenameepm" runat="Server" class="lblText">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%">
                                    Site ID (PO)
                                </td>
                                <td style="width: 3px">
                                    :
                                </td>
                                <td class="lblText">
                                    <asp:TextBox ID="txtsiteidpo" runat="server" CssClass="textFieldStyle" Width="133px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 26px;">
                                    Site Name (PO)
                                </td>
                                <td style="width: 3px; height: 26px;">
                                    :
                                </td>
                                <td class="lblText" style="height: 26px">
                                    <asp:TextBox ID="txtsitenamepo" runat="server" CssClass="textFieldStyle" Width="215px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="remaprow" runat="server" visible="false">
                                <td class="lblTitle" style="width: 18%; height: 18px">
                                    Remapped from <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                                </td>
                                <td style="width: 3px; height: 18px">
                                    :
                                </td>
                                <td runat="Server" class="lblText" style="height: 18px">
                                    <asp:TextBox ID="txtremapsite" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 23px;">
                                    PO Date
                                </td>
                                <td style="width: 3px; height: 23px;">
                                    :
                                </td>
                                <td style="height: 23px" id="lblpodate" runat="Server" class="lblText">
                                    &nbsp;<asp:ImageButton ID="btnDateOfProduction" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                        Width="18px" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 23px">
                                    Remapped From / Site From
                                </td>
                                <td style="width: 3px; height: 23px">
                                    :
                                </td>
                                <td id="lblsitefrom" runat="Server" class="lblText" style="height: 23px">
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 23px;">
                                    Band
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtBand" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 23px;">
                                    Config
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtConfig" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 18%; height: 23px;">
                                    BTS Type
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtprojectid" runat="server" class="textFieldStyle" maxlength="80"
                                        style="width: 215px" /><span style="color: Red;">*</span>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel6" runat="server" GroupingText="WorkPackage Info" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Work Package Id
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtWPKGId" runat="server" class="textFieldStyle" maxlength="2147483647"
                                        readonly="readOnly" style="background-color: gray" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Work Package Name
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtWPName" runat="server" class="textFieldStyle" maxlength="50"
                                        style="width: 215px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel5" runat="server" GroupingText="Hardware Info" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Hardware
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtHW" runat="server" class="textFieldStyle" maxlength="100"
                                        style="width: 215px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Hardware Code
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtCode" runat="server" class="textFieldStyle" maxlength="20"
                                        style="width: 215px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" GroupingText="Scope Info" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Scope
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td id="txtFldType">
                                    <asp:DropDownList ID="ddlscope" runat="server" CssClass="selectFieldStyle" Width="212px">
                                    </asp:DropDownList>
                                    <input type="text" id="txtfldtype" runat="server" class="textFieldStyle" maxlength="2147483647" /><span
                                        style="color: Red;">*</span>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="lblTitle" style="width: 35%">
                                    Type of Scope
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlScopeMaster" runat="server" CssClass="textFieldStyle" ValidationGroup="podetails">
                                    </asp:DropDownList>
                                    <span style="color: Red;">*</span>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td class="lblTitle" style="width: 35%">
                                    Typeofwork
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlTypeofwork" runat="server" CssClass="textFieldStyle" ValidationGroup="podetails">
                                    </asp:DropDownList>
                                    <span style="color: Red;">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 35%">
                                    Description
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtDesc" runat="server" class="textFieldStyle" maxlength="100"
                                        style="width: 215px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="pnlCostInfo" runat="server" GroupingText="Cost Info" Width="100%">
                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                            <tr id="trValue1" runat="Server">
                                <td class="lblTitle" style="width: 35%">
                                    Value in USD
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtValue1" runat="server" class="textFieldStyle" maxlength="15"
                                        onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" />
                                </td>
                            </tr>
                            <tr id="trValue2" runat="Server">
                                <td class="lblTitle" style="width: 35%">
                                    Value in IDR
                                </td>
                                <td style="width: 1%">
                                    :
                                </td>
                                <td>
                                    <input type="text" id="txtValue2" runat="server" class="textFieldStyle" maxlength="15"
                                        onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="Center" style="height: 60px">
                    <asp:HiddenField ID="hdnpoid" runat="server" />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" ValidationGroup="podetails" />&nbsp;
                    <input type="button" id="btnBack" name="btnBack" runat="server" value="Back to List"
                        class="buttonStyle" style="width: 79pt" causesvalidation="false" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonStyle" CausesValidation="false" />
                </td>
                <asp:RequiredFieldValidator ID="RfvSiteIDPO" runat="server" ControlToValidate="txtsiteidpo"
                    ErrorMessage="Please fill Site ID PO field" Display="None" ValidationGroup="podetails"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RfvSiteNamePO" runat="server" ControlToValidate="txtsitenamepo"
                    ErrorMessage="Please fill Sitename PO field" Display="None" ValidationGroup="podetails"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RfvProjectID" runat="server" ControlToValidate="txtprojectid"
                    ErrorMessage="Please fill BTS Type field" Display="None" ValidationGroup="podetails"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RfvScope" runat="server" ControlToValidate="txtfldtype"
                    ErrorMessage="Please fill Scope field" Display="none" ValidationGroup="podetails"
                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CvMasterSscope" runat="server" ControlToValidate="DdlScopeMaster"
                    ErrorMessage="Please choose Master Scope first" Display="None" SetFocusOnError="true"
                    ValueToCompare="0" Operator="GreaterThan" ValidationGroup="podetails"></asp:CompareValidator>
                <asp:CompareValidator ID="CvTypeofwork" runat="server" ControlToValidate="DdlTypeofWork"
                    ErrorMessage="Please choose TypeofWork first" Display="None" SetFocusOnError="true"
                    ValueToCompare="0" Operator="GreaterThan" ValidationGroup="podetails"></asp:CompareValidator>
                <asp:ValidationSummary ID="VsPoDetails" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="podetails" />
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
