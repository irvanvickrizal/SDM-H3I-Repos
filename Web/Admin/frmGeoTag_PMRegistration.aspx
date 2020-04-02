<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGeoTag_PMRegistration.aspx.vb"
    Inherits="Admin_frmGeoTag_PMRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PM GeoTag Registration</title>
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

        .HeaderPanel {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top: -5px;
            margin-bottom: 10px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-left: 8px;
            border-style: solid;
            border-color: Gray;
            border-width: 2px;
            color: White;
        }

        .HeaderGrid {
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
            height: 25px;
        }

        .oddGrid {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .evenGrid {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }

        .emptyrow {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }

        .btnstyle {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Green;
            color: White;
            cursor: pointer;
            padding: 5px;
        }

        #PleaseWait {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/preloader.gif);
            background-repeat: no-repeat;
            margin: 0 10%;
            margin-top: 10px;
        }

        #blur {
            width: 100%;
            background-color: #ffffff;
            moz-opacity: 0.7;
            khtml-opacity: .7;
            opacity: .7;
            filter: alpha(opacity=70);
            z-index: 120;
            height: 100%;
            position: absolute;
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
                progress.style.width = '300px';
                progress.style.height = '30px';
                blur.style.height = document.documentElement.clientHeight;
                progress.style.top = document.documentElement.clientHeight / 3 - progress.style.height.replace('px', '') / 2 + 'px';
                progress.style.left = document.body.offsetWidth / 2 - progress.style.width.replace('px', '') / 2 + 'px';
            }
        }
        )
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SM1" runat="server">
        </asp:ScriptManager>
        <div class="HeaderPanel">
            <span>PM Registration</span>
        </div>
        <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="blur">
                    <div style="position: relative; top: 30%; text-align: center; background-color: #ffffff;">
                        <img src="../Images/preloader.gif" style="vertical-align: middle" alt="Processing" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UP1" runat="server">
            <ContentTemplate>
                <div style="margin-top: 5px;">
                    <asp:GridView ID="GvRegistrationUser" runat="server" AutoGenerateColumns="false"
                        EmptyDataText="No Record Found" PageSize="10" AllowPaging="true" Width="99%">
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <HeaderStyle CssClass="HeaderGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Fullname" HeaderText="Fullname" />
                            <asp:BoundField DataField="UserLogin" HeaderText="User Login" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                            <asp:TemplateField HeaderText="Modified User">
                                <ItemTemplate>
                                    <asp:Label ID="LblModifieduser" runat="server" Text='<%# Eval("CMAInfo.ModifiedUser") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Modified Date">
                                <ItemTemplate>
                                    <asp:Label ID="LblModifiedDate" runat="server" Text='<%# Eval("CMAInfo.LMDT","{0:dd-MMMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/images/Edit-icon.png" ToolTip="Edit User" Width="20px" Height="20px" CommandName="edituser" CommandArgument='<%Eval("userid")%>' />
                                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                        ToolTip="Delete User" Width="20px" Height="20px" CommandName="deleteuser" CommandArgument='<%#Eval("userid") %>'
                                        OnClientClick="return confirm('Are you sure you want to delete this user?');" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <hr />
                <div style="width: 45%; margin-top: 10px; border-style: solid; border-width: 1px; border-color: Gray;">
                    <div style="background-color: Black; padding: 5px; margin-bottom: 8px;">
                        <span class="lblField" style="color: White;">Add Login Account</span>
                    </div>
                    <table cellpadding="2">
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrFullname" runat="server" Text="Fullname"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFullname" runat="server" CssClass="ltrLabel" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrEmail" runat="server" Text="Email"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtEmail" runat="server" Width="250px" CssClass="ltrLabel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="ltrPhone" runat="server" Text="PhoneNo"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="250px" CssClass="ltrLabel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrCompany" runat="server" Text="Company"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlCompany" runat="server" CssClass="ltrLabel">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrUserLogin" runat="server" Text="User Login"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtUserLogin" runat="server" Width="250px" CssClass="ltrLabel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrUserPassword" runat="server" Text="Password"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtUserPassword" runat="server" TextMode="Password" Width="250px"
                                    CssClass="ltrLabel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel">
                                <asp:Literal ID="LtrConfirmUserPassword" runat="server" Text="Confirm Password"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtConfirmPassword" runat="server" TextMode="Password" Width="250px"
                                    CssClass="ltrLabel"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="ltrLabel" valign="top">
                                <asp:Literal ID="LtrRegion" runat="server" Text="Region"></asp:Literal>
                            </td>
                            <td>
                                <asp:GridView ID="GvRegions" runat="server" AutoGenerateColumns="false" Width="255px"
                                    ShowHeader="false" GridLines="None">
                                    <AlternatingRowStyle CssClass="evenGrid" />
                                    <RowStyle CssClass="oddGrid" />
                                    <HeaderStyle CssClass="HeaderGrid" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input id="ChkReview" runat="server" type="checkbox" />
                                                <asp:Label ID="LblRgnId" runat="server" Text='<%#Eval("RgnId") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RgnName" ReadOnly="true" HeaderText="Region" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="LblWarningMessage" runat="server" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <div style="margin-top: 5px; text-align: right;">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="btnstyle" Width="100px" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
