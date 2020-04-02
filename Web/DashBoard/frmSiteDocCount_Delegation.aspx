<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocCount_Delegation.aspx.vb"
    Inherits="DashBoard_frmSiteDocCount_Delegation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sites Document Count</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="1" border="0" cellspacing="1" width="100%">
                <tr>
                    <td class="pageTitle" colspan="4" id="rowadd" style="background-color: #cccccc">
                        Sites Document Count</td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PnlTaskUser" runat="server">
                            <div style="border-style: solid; border-width: 1px; font-family: Verdana; width: 300px;">
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                Type of Task :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DdlTaskUser" runat="server" OnSelectedIndexChanged="DdlSelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="Approver" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Review" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 10px;">Total document in Task pending
                                                    as approver : </span>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblTaskPendingApprover" runat="server" ForeColor="red"></asp:Label>
                                                <asp:Label ID="LblPendingApprover" runat="server" ForeColor="red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span style="font-family: Verdana; font-size: 10px;">Total document in Task pending
                                                    as reviewer : </span>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblTaskPendingReviewer" runat="server" ForeColor="red"></asp:Label>
                                                <asp:Label ID="LblPendingReviewer" runat="server" ForeColor="red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="GrdDocCount" runat="server" AllowPaging="false" EmptyDataText="All documents approved"
                            AllowSorting="True" AutoGenerateColumns="False" Width="99%">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No.">
                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Site_No" HeaderText="Site No" />
                                <asp:BoundField DataField="PoNo" HeaderText="PO Number" />
                                <asp:BoundField DataField="Site_Name" HeaderText="Site Name" />
                                <asp:BoundField DataField="workpkgid" HeaderText="Work Package ID" />
                                <asp:BoundField DataField="submitDate" HeaderText="Submit Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                                    HtmlEncode="false" />
                                <asp:TemplateField HeaderText="Document Count">
                                    <ItemTemplate>
                                        <asp:Label ID="LblSiteNo" runat="server" Text='<%#Eval("Site_No") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblTskId" runat="server" Text='<%#Eval("Tsk_Id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="LblPackageId" runat="server" Text='<%#Eval("workpkgid") %>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="LbtViewDocCount" runat="server" Text='<%#Eval("CountUsrType") %>'
                                            CommandName="opendetail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="LblResult" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="right" id="total" runat="server">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <input id="btnViewAll" type="submit" size="50px" value="View All" runat="server"
                            class="buttonStyle" style="width: 100pt" />
                        <input id="BtnClose" type="submit" size="50px" value="Go to Dashboard" runat="server"
                            class="buttonStyle" style="width: 100pt" />
                    </td>
                </tr>
            </table>
            <input id="hdnSort" type="hidden" runat="server" />
        </div>
    </form>
</body>
</html>
