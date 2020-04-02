<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WCCUserRegistration.aspx.vb"
    Inherits="COD_WCCUserRegistration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WCC User Registration</title>
    <style type="text/css">
        .lblText
        {
            font-family: verdana;
            font-size: 12px;
        }
        .lblBoldText
        {
            font-family: verdana;
            font-size: 12px;
            font-weight: bolder;
        }
        .emptyRowStyle
        {
            font-family: verdana;
            font-size: 8pt;
            font-weight: bolder;
            color: maroon;
            border-style: solid;
            padding: 3px;
            border-width: 1px;
            border-color: gray;
        }
        .GridHeader
        {
            background-color: #ffc90e;
            font-family: verdana;
            font-weight: bold;
            font-size: 9pt;
            text-align: center;
            height: 30px;
            color: white;
        }
        .GridOddRows
        {
            font-family: verdana;
            font-size: 7.5pt;
        }
        .GridEvenRows
        {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 7.5pt;
        }
        .btnstyle
        {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            background-color: #c3c3c3;
            color: #fff;
            padding: 8px;
            cursor: pointer;
            border-width: 1px;
            border-color: white;
            border-style: solid;
        }
        .btnSave
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_0.gif);
        }
        .btnSave:hover
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_1.gif);
        }
        .btnSave:click
        {
            height: 27px;
            width: 60.5px;
            background-image: url(../Images/button/BtnSave_2.gif);
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
        #panelHeader
        {
            background-color: #cfcfcf;
            padding: 5px;
            font-family: verdana;
            font-size: 15px;
            font-weight: bolder;
            color: #ffffff;
            width: 99%;
            margin-top:-10px;
        }
    </style>

    <script type="text/javascript">
        function ErrorSave(errMessage)  
        {
            alert("Error While Saving the Data: " + errMessage + " Please Try Again!");
            return false;
        }
        function SucceedSave()  
        {
            alert("Data Successful saved");
            return true;
        }
        function SucceedDelete()  
        {
            alert("Data Successful Deleted");
            return true;
        }
        function ConfirmSave()
        {
            var answer = confirm("Are you sure want to add new user?");
            if (answer){
                return true;
            }
            else{
                return false;
            }
        }
        function NYRoleSelect()
        {
            alert("Please Choose Role Type of User First");
            return false;
        }
    </script>

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
    <div>
        <div id="panelHeader">
            Register WCC Reviewer
        </div>
        <div style="margin-top: 10px;">
            <table>
                <tr>
                    <td>
                        <span class="lblBoldText">Search By</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlRoles" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 5px;">
            <asp:UpdateProgress ID="upgATPReport" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="Up1">
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
                    <div style="margin-bottom: 5px;">
                        <asp:GridView ID="GvUsers" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            PageSize="10" HeaderStyle-CssClass="GridHeader" Width="99%" EmptyDataText="No Record Found">
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <EmptyDataRowStyle CssClass="emptyRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="No." ItemStyle-Width="28px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Fullname" HeaderText="User Name" />
                                <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="28px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/gridview/Cancel.jpg"
                                            AlternateText="deleteicon" CommandName="deletereguser" CommandArgument='<%#Eval("RegisterUserId") %>'
                                            OnClientClick="return confirm('are you sure you want to delete this injector?');"
                                            Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <hr />
                    <div style="margin-top: 5px;">
                        <table>
                            <tr>
                                <td>
                                    <span class="lblBoldText">Role type</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlRoleSave" runat="server" CssClass="lblText" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="LblLevelCode" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlLevelCode" runat="server" CssClass="lblText">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="BtnSearch" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:GridView ID="GvUserFlow" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="GridHeader"
                                        EmptyDataText="No Record Found" Width="450px">
                                        <RowStyle CssClass="GridOddRows" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <EmptyDataRowStyle CssClass="emptyRowStyle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No." ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Fullname" HeaderText="User Name" />
                                            <asp:TemplateField ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgAdd" runat="server" ImageUrl="~/images/gridview/AddNewItem.jpg"
                                                        AlternateText="deleteicon" CommandName="AddUser" CommandArgument='<%#Eval("userid") %>'
                                                        OnClientClick="return confirm('are you sure you want to Add this User?');" Width="20px"
                                                        Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DdlRoles" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
