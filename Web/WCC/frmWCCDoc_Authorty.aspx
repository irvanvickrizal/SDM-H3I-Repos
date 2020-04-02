<%@ Page Language="VB" AutoEventWireup="true" CodeFile="frmWCCDoc_Authorty.aspx.vb"
    Inherits="WCC_frmWCCDoc_Authorty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC Document Authority</title>
    <link href="~/CSS/MasterFormManagement.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function FailedSaved() {
            alert('Data Failed to Save, Please Try Again!');
            return false;
        }
        function SucceedSaved() {
            alert('Data Successfully Save');
            return true;
        }
        function NoRoleChecked(){
            alert('No Role already tick, please tick first!');
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div>
            <div style="background-color: #c3c3c3;">
                <div style="padding: 3px;">
                    <span class="ltrHeaderLabel" runat="server">WCC Document Authority List</span>
                </div>
            </div>
            <div style="margin-top: 5px;">
                <asp:GridView ID="GvWCCCreatorAuthorties" runat="server" AutoGenerateColumns="false" CellPadding="2" CellSpacing="2" Width="100%"
                    EmptyDataText="No Record Found">
                    <HeaderStyle CssClass="GridHeader" />
                    <EmptyDataRowStyle CssClass="emptyRowStyle" />
                    <RowStyle CssClass="GridOddRows" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <PagerStyle Font-Names="Verdana" Font-Size="6pt" Wrap="true" />
                    <FooterStyle CssClass="ltrLabel" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CommandName="deleteauth" runat="server" CommandArgument='<%#Eval("WCCAuthortyId") %>'
                                    ImageUrl="~/Images/gridview/delete.jpg" ToolTip="Delete" Height="16px" Width="16px"
                                    OnClientClick="return confirm('Are you sure you want to delete this data ?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RoleName" HeaderText="Role" NullDisplayText="Null" />
                        <asp:BoundField DataField="Username" HeaderText="Username" NullDisplayText="Null" />
                        <asp:BoundField DataField="LMBY" HeaderText="Last Modified" />
                        <asp:BoundField DataField="LMDT" HeaderText="Last Modified Date" HtmlEncode="false"
                            DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div style="width: 450px; margin-top: 10px; border-style:solid; border-color:Gray; border-width:1px;">
            <div style="background-color: #c3c3c3; padding: 3px;">
                <span class="lblField">WCC Document Authority New</span>
            </div>
            <div style="margin-top: 5px;">
                <table>
                    <tr runat="server" id="pnlAuthorityBase" valign="top">
                        <td>
                            <asp:Label ID="LblAuthorityBase" runat="server" Text="Authority Base" CssClass="ltrLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlAuthorityBase" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                                <asp:ListItem Text="--Choose Authority Base --" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Role Base" Value="1"></asp:ListItem>
                                <asp:ListItem Text="User Base" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlUserType" valign="top">
                        <td>
                            <asp:Label ID="LblUserType" runat="server" Text="User Type" CssClass="ltrLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlUserType" runat="server" CssClass="ltrLabel" AutoPostBack="true">
                                <asp:ListItem Text="--Select User Type--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="NSN" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Subcon" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Telkomsel" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlRoleBase" valign="top">
                        <td>
                            <asp:Label ID="LblRoleBase" runat="server" Text="Role Type" CssClass="ltrLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpRoleBase" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GvRoleBase" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        PageSize="5" Width="350px" CellPadding="2" CellSpacing="2" >
                                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                        <HeaderStyle CssClass="GridHeader2" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <input id="ChkChecked" runat="server" type="checkbox" />
                                                    <asp:HiddenField ID="HdnRoleId" Value='<%#Eval("RoleId") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RoleName" HeaderText="Role Description" />
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr runat="server" id="pnlUserBase">
                        <td>
                            <asp:Label ID="LblUserBase" runat="server" Text="User" CssClass="ltrLabel"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlUserBase" runat="server" CssClass="ltrLabel">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 5px; text-align: right;">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" />
                <asp:Button ID="BtnClear" runat="server" Text="Clear" />
            </div>
        </div>
    </form>
</body>
</html>
