<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmManagePermission.aspx.vb"
    Inherits="Admin_frmManagePermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Feature Permission</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 8pt;
            color: #000;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: #000;
        }
        .HeaderPanel
        {
            background-color: #c3c3c3;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-top:-5px;
            margin-bottom: 10px;
            padding-top: 8px;
            padding-bottom: 8px;
            padding-left: 8px;
            border-style:solid;
            border-color:Gray;
            border-width:2px;
            color:White;
        }
        .HeaderGrid
        {
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
            height:25px;
        }
        .oddGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: White;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .evenGrid
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            border-color: black;
            border-style: solid;
            border-width: 1px;
        }
        .emptyrow
        {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
            border-color: black;
            border-style:solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style:solid;
            background-color: Green;
            border-width: 2px;
            border-color:Green;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        #PleaseWait
        {
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
        #blur
        {
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
                    progress.style.top = document.documentElement.clientHeight/3 - progress.style.height.replace('px','')/2 + 'px';
                    progress.style.left = document.body.offsetWidth/2 - progress.style.width.replace('px','')/2 + 'px';
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
        <span>Permission Management</span>
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
            <div style="border-bottom-style: solid; border-bottom-color: Gray; border-bottom-width: 2px;
                padding-bottom: 10px;">
                <asp:GridView ID="GvPermissions" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                    PageSize="10" AllowPaging="true" Width="99%">
                    <RowStyle CssClass="oddGrid" />
                    <AlternatingRowStyle CssClass="evenGrid" />
                    <HeaderStyle CssClass="HeaderGrid" />
                    <EmptyDataRowStyle CssClass="emptyrow" />
                    <Columns>
                        <asp:BoundField DataField="RoleName" HeaderText="Role" />
                        <asp:BoundField DataField="PermissionType" HeaderText="Feature" />
                        <asp:BoundField DataField="PermissionCategory" HeaderText="Category" />
                        <asp:BoundField DataField="Modifieduser" HeaderText="Modified User" />
                        <asp:BoundField DataField="LMDT" HeaderText="Modified Date" HtmlEncode="false" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                            ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ConvertEmptyStringToNull="true" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/images/gridview/cancel.jpg"
                                    Width="20px" Height="20px" CommandName="deletepermission" CommandArgument='<%#Eval("PermissionId") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this permission?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="LblGvDocumentWarningMessage" runat="server" Font-Name="Verdana" Font-Size="11px"
                    ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
            </div>
            <div>
                <br />
            </div>
            <div style="width: 45%; margin-top: 10px; border-style: solid; border-width: 1px;
                border-color: Gray;">
                <div style="background-color:Black; padding: 5px; margin-bottom:8px;">
                    <span class="lblField" style="color:White;">Create Permission</span>
                </div>
                <table>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrRole" runat="server" Text="Role"></asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlRoles" runat="server" CssClass="ltrLabel">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrPermissionType" runat="server" Text="Permission Type"></asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlPermissionType" runat="server" CssClass="ltrLabel">
                                <asp:ListItem Text="-- select feature --" Value="0"></asp:ListItem>
                                <asp:ListItem Text="SOAC" Value="SOAC"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrPermissionCategory" runat="server" Text="Permission Category"></asp:Literal>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlPermissionCategory" runat="server" CssClass="ltrLabel">
                                <asp:ListItem Text="-- select category --" Value="0"></asp:ListItem>
                                <asp:ListItem Text="FORM" Value="FORM"></asp:ListItem>
                                <asp:ListItem Text="REPORT" Value="REPORT"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="LblWarningMessage" runat="server" Font-Name="Verdana" Font-Size="11px"
                                ForeColor="Red" Font-Italic="true" Style="font-weight: bolder;"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
                <div style="text-align: right; margin-top: 5px;">
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" Width="80px" CssClass="btnstyle"
                        OnClientClick="return confirm('are you sure you want to add this permission?');" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
