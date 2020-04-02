<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDocView.aspx.vb" Inherits="frmDocView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Doc View</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            color: black;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function checkIsEmpty() {
            var msg = "";
            var e = document.getElementById("ddlPO1");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Please select Po No.\n";
            }
            if (msg != "") {
                alert("Mandatory field information : \n\n" + msg);
                return false;
            }
            else {
                return true;
            }
        }

        function DoPrint() {
            var str = '';
            var disp_setting = "toolbar=no,location=yes,status=yes,directories=yes,menubar=yes,";
            disp_setting += "scrollbars=yes,width=750,height=600,left=100,top=25";
            str += '<HTML>\n<head>\n';
            str += '<link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />\n</HEAD>\n<body>\n';
            str += '<center>\n' + document.getElementById("dvPrint").innerHTML + '\n</center></body>\n</HTML>';
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, disp_setting);
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
        <div style="width: 99%; height: 450px">
            <div class="headerpanel">
                Document View
            </div>
            <div style="margin-top: 10px;">
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td class="lblTitle" style="width: 15%">Po No <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                        </td>
                        <td style="width: 1%">:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlPO1" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="lblsrcname" runat="Server">
                        <td class="lblTitle">Site
                        </td>
                        <td style="width: 1%">:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlSite" runat="Server" CssClass="selectFieldStyle" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle">
                        </asp:TextBox>&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /><asp:Button
                            ID="Button1" runat="server" Text="Track" CssClass="goButtonStyle" Width="56px" />
                        </td>
                    </tr>
                </table>
                <table id="tblAudit" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%"
                    visible="false">
                    <tr valign="bottom" height="32px">
                        <td align="center" width="98%" class="lblTextGrey" id="thevent" runat="server">Event History
                        </td>
                        <td align="Right">
                            <asp:ImageButton ID="ibExcelExportTop" runat="server" ImageUrl="~/Images/xl.jpeg"
                                AlternateText="Excel Export" Height="22px" Width="22px" />
                        </td>
                    </tr>
                    <tr valign="Top">
                        <td align="Center" colspan="2">
                            <asp:Label ID="lblMsg" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="Center" colspan="2" valign="Top">
                            <div id="DvPrint">
                                <asp:GridView ID="gvSearch" runat="server" BackColor="White" EmptyDataText="No Records Found"
                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="false">
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <RowStyle CssClass="GridOddRows" />
                                    <AlternatingRowStyle CssClass="GridEvenRows" BackColor="#CCCCCC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Total " ItemStyle-BorderWidth="1">
                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblno" runat="Server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Document Name" DataField="docname"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Date" DataField="EventStart"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Name" DataField="Name"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Type" DataField="GrpDesc"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="User Role" DataField="RoleDesc"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                        <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderText="Event" DataField="Task"
                                            NullDisplayText="NA" ConvertEmptyStringToNull="true" ItemStyle-BorderWidth="1" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td align="Right" colspan="2">
                            <br />
                            <input id="Button2" runat="server" type="button" value="   Print   " onclick="DoPrint()"
                                class="buttonStyle" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td colspan="4">
                            <br />
                            <asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" AllowSorting="False"
                                Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found" DataKeyNames="Doc_Id">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server">
                                            </asp:Label>
                                            <asp:Label ID="LblDocId" runat="server" Text='<%#Eval("doc_id") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="doc_id" HeaderText="doc" />
                                    <asp:BoundField DataField="docpath" HeaderText="Path" />
                                    <asp:BoundField DataField="DocName" HeaderText="Document" />
                                    <asp:BoundField DataField="doc_id" HeaderText="Document" />
                                    <asp:BoundField DataField="site_no" HeaderText="SiteNo" />
                                    <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                    <asp:BoundField DataField="UploadedDate" HeaderText="Upload Date" />
                                    <asp:BoundField HeaderText="" />
                                    <asp:BoundField DataField="SW_Id" />
                                    <asp:BoundField DataField="OrgDocPath" HeaderText="OrgPath" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="Download">DownLoad</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <input id="hdnSort" type="hidden" runat="server" />
            </div>
        </div>
        <%--Modified by Fauzan, 7 Nov 2018. Remove 3 Logo--%>
        <%--<div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>--%>
    </form>
</body>
</html>
