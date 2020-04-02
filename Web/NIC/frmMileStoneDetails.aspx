<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMileStoneDetails.aspx.vb"
    Inherits="MSD_frmMileStoneDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mile Stone Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/jscript">
    
    function checkKey()
    {
        if(window.event.keyCode < 47 || window.event.keyCode > 57) //48 57
        {
        window.event.keyCode = 0;
        return false;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr class="pageTitle">
                    <td colspan="2" style="width: 1159px; height: 21px;">
                        Mile Stone Details</td>
                </tr>
                <tr>
                    <td runat="server" colspan="2" style="width: 1159px; height: 459px;">
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td style="height: Auto; width: 177px;">
                                </td>
                                <td style="height: Auto; width: 10px;">
                                </td>
                                <td class="lblText" style="width: 10%; height: Auto;">Filter EPM : 
                                    <asp:DropDownList ID="ddlFilter" runat="server" AutoPostBack="true">
                                        <asp:ListItem Text="Actual" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Forecast" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Both" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="lblText" style="height: 18px">
                                </td>
                                <td style="width: 8px; height: Auto;">
                                    TotalFields</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="1" style="height: 138px; width: 177px;">
                                    EPMData Fields</td>
                                <td align="right" colspan="1" style="height: 138px; width: 10px;">
                                    :</td>
                                <td style="width: 10%; height: 138px">
                                    <asp:ListBox ID="lstEPMData" runat="server" CssClass="selectFieldStyle" Rows="10"></asp:ListBox></td>
                                <td align="center" rowspan="2" style="width: 5%" valign="middle">
                                    <input id="btnAddAll" runat="server" class="buttonStyle" type="button" value=">>" /><br />
                                    <br />
                                    <input id="btnAdd" runat="server" class="buttonStyle" type="button" value=">" /><br />
                                    <br />
                                    <input id="btnRemove" runat="server" class="buttonStyle" type="button" value="<" /><br />
                                    <br />
                                    <input id="btnRemoveAll" runat="server" class="buttonStyle" type="button" value="<<" /></td>
                                <td rowspan="2" style="width: 8px">
                                    <asp:ListBox ID="lstUnSelected" runat="server" CssClass="selectFieldStyle" Height="274px"
                                        Rows="10"></asp:ListBox></td>
                                <td rowspan="2" style="width: 20px" valign="middle">
                                    <input id="btnUp" runat="server" class="buttonStyle" disabled="disabled" type="button"
                                        value="Up" />
                                    <br />
                                    <input id="btnDown" runat="server" class="buttonStyle" disabled="disabled" type="button"
                                        value="Down" /><br />
                                </td>
                                <td rowspan="2" style="width: 286px" valign="middle">
                                </td>
                                <td rowspan="2" valign="middle">
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="1" style="height: 138px; width: 177px;">
                                    Custom NIC Fields</td>
                                <td align="right" colspan="1" style="height: 138px; width: 10px;">
                                    :</td>
                                <td style="width: 10%; height: 138px">
                                    <asp:ListBox ID="lstMileStoneDetails" runat="server" CssClass="selectFieldStyle"
                                        Rows="10"></asp:ListBox></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                                <td colspan="1" style="width: 177px">
                                </td>
                                <td colspan="1" style="width: 10px">
                                </td>
                                <td colspan="7">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="height: 14px; width: 177px;">
                                </td>
                                <td colspan="1" style="height: 14px; width: 10px;">
                                </td>
                                <td colspan="6" style="height: 14px">
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnDisply" runat="server" CssClass="buttonStyle" Text="Save Template"
                                        Width="103px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" style="height: 14px">
                                    <asp:ListBox ID="lstNICDummyData" runat="server" CssClass="selectFieldStyle" Rows="10"
                                        Height="11px" Visible="False"></asp:ListBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 1239px; height: 118px">
            <tr>
                <td colspan="18" style="width: 788px; height: 27px">
                    <div style="overflow: auto; width: 1230px; height: 84px">
                        <asp:GridView ID="grdNICEPMData" runat="server" Width="284px">
                            <RowStyle CssClass="GridOddRows" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="18" style="width: 788px; height: 27px">
                    <asp:Button ID="btnExport" runat="server" CssClass="buttonStyle" Text="Download New Report Template"
                        Width="285px" /></td>
            </tr>
            <tr>
                <td colspan="18" style="width: 788px; height: 27px">
                </td>
            </tr>
            <!--
            <tr>
                <td colspan="18" style="width: 788px; height: 27px">
                    <div style="overflow: auto; width: 1230px; height: 84px">
                        <asp:GridView ID="GvNICTemplate" runat="server" Width="284px">
                            <RowStyle CssClass="GridOddRows" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="18" style="width: 788px; height: 27px">
                    <asp:Button ID="btnExportExcel" runat="server" CssClass="buttonStyle" Text="Download New Export Template"
                        Width="285px" /></td>
            </tr>
            -->
        </table>
    </form>
</body>
</html>
