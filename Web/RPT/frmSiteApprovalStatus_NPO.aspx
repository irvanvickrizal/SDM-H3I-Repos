<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteApprovalStatus_NPO.aspx.vb" Inherits="RPT_frmSiteApprovalStatus_NPO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Site Approval Status</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left: 10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }

        .BtnExpt {
            border-style: solid;
            border-color: white;
            border-width: 1px;
            font-family: verdana;
            font-size: 11px;
            font-weight: bold;
            color: black;
            width: 120px;
            height: 25px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; height: 450px;">
            <div class="headerpanel">
                <table width="100%">
                    <tr>
                        <td style="width: 80%">Site Approval Status
                        </td>
                        <td style="width: 15%; text-align: right;">
                            <input id="btnExport" runat="server" type="button" value="Export to Excel" class="BtnExpt" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top:10px; width:100%;" id="pnlSearch" runat="server">
                <table>
                    <tr>
                        <td>
                            <span class="lblTextBold">Search by</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlDocuments" CssClass="lblText" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlCompany" CssClass="lblText" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlUserTask" CssClass="lblText" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="BtnSearch" />
                        </td>
                    </tr>
                </table>
                 <hr />
            </div>
            <div style="margin-top: 5px; width: 99%;">
                <table width="100%" cellspacing="1" cellpadding="1">
                    <tr>
                        <td align="right"></td>
                    </tr>
                    <tr>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            <asp:GridView ID="grdDB" runat="server" AutoGenerateColumns="False" Width="99%"
                                AllowPaging="true" EmptyDataText="No Criteria Met" PageSize="30">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" No. ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pono" HeaderText="PO Number" />
                                    <asp:BoundField DataField="siteno" HeaderText="Site No" />
                                    <asp:BoundField DataField="sitename" HeaderText="Site Name" />
                                    <asp:BoundField DataField="workpackageid" HeaderText="Work Package Id" />
                                    <asp:BoundField DataField="docname" HeaderText="Document Name" />
                                    <asp:BoundField DataField="UserLocation" HeaderText="Pending Location" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" />
                                    <asp:BoundField DataField="taskdesc" HeaderText="Task" />
                                    <asp:BoundField DataField="SubmitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        ConvertEmptyStringToNull="true" HtmlEncode="false" />
                                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                    <asp:BoundField DataField="rgnname" HeaderText="Region" />
                                    <asp:BoundField DataField="delays" HeaderText="Delay Days" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" style="display: none"></td>
                    </tr>
                </table>
            </div>
        </div>
        <%--Modified by Fauzan, 7 Nov 2018. Remove 3 Logo--%>
        <%--<div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>--%>
    </form>
</body>
</html>