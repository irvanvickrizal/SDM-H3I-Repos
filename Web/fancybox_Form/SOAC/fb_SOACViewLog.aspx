<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_SOACViewLog.aspx.vb"
    Inherits="fancybox_Form_fb_SOACViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Log</title>
    <style type="text/css">
        .ltrLabel
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblText
        {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }
        .lblField
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: #000;
        }
        .HeaderPanel
        {
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
        .HeaderGrid
        {
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            height: 30px;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
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
            border-style: solid;
            border-width: 1px;
            padding: 5px;
        }
        .btnstyle
        {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: Green;
            border-width: 2px;
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }
        .btnSearch
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }
        .btnSearch:hover
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_1.gif);
        }
        .btnSearch:click
        {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_2.gif);
        }
        .warningMessage
        {
            font-family: Verdana;
            font-size: 13px;
            font-weight: bolder;
            color: Red;
            font-style: italic;
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
         function invalidExportToExcel() {
            alert('Data is empty, please try another date!');
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM1" runat="server">
    </asp:ScriptManager>
    <div class="HeaderPanel">
        <table width="100%">
                <tr>
                    <td style="width: 80%">
                        View Log
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="btnstyle"
                            BackColor="#7f7f7f" />
                    </td>
                </tr>
            </table>
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
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div style="height: 500px;">
                <div>
                    <table cellpadding="1" cellspacing="2">
                        <tr>
                            <td style="width: 150px;">
                                <span class="lblField">Site No</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="LblSiteNo" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblField">Sitename</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="LblSitename" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblField">PONO</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="LblPONO" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblField">Workpackage ID</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="LblWorkpackageid" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblField">Scope</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="LblScope" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="lblField">Doc Type</span>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="DdlDocType" runat="server" CssClass="lblText" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:Label ID="LblDocType" runat="server" CssClass="lblText"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div style="margin-top: 10px;">
                    <asp:GridView ID="GvSOACLog" runat="server" AutoGenerateColumns="false" EmptyDataText="No record found"
                        Width="99%" HeaderStyle-Height="25px" BorderColor="Gray">
                        <HeaderStyle CssClass="HeaderGrid" />
                        <RowStyle CssClass="oddGrid" />
                        <AlternatingRowStyle CssClass="evenGrid" />
                        <EmptyDataRowStyle CssClass="emptyrow" />
                        <Columns>
                            <asp:BoundField DataField="TaskEvent" HeaderText="Task" HeaderStyle-BorderColor="Gray"  ItemStyle-BorderColor="Gray" />
                            <asp:TemplateField HeaderText="User" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray">
                                <ItemTemplate>
                                    <asp:Label ID="LblFullname" runat="server" Text='<%#Eval("UserInfo.Fullname") %>'
                                        CssClass="lblText"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray">
                                <ItemTemplate>
                                    <asp:Label ID="LblTitle" runat="server" Text='<%#Eval("UserInfo.SignTitle") %>' CssClass="lblText"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray">
                                <ItemTemplate>
                                    <asp:Label ID="LblRoleName" runat="server" Text='<%#Eval("RoleInf.RoleName") %>'
                                        CssClass="lblText"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="eventstarttime" HeaderText="Start Time" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" />
                            <asp:BoundField DataField="eventendtime" HeaderText="End Time" HtmlEncode="false"
                                DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" ConvertEmptyStringToNull="true" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" />
                            <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-BorderColor="Gray" HeaderStyle-BorderColor="Gray" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
