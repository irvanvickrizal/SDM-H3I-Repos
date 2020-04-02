<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGeoTag_PMSetup.aspx.vb" Inherits="Admin_frmGeoTag_PMSetup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PM Registration Setup</title>
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

        .btnstyle_gray {
            font-family: Verdana;
            font-size: 10px;
            font-weight: bolder;
            border-style: solid;
            background-color: gray;
            border-width: 2px;
            border-color: #c3c3c3;
            color: black;
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
        <div>
            <table>
                <tr>
                    <td>
                        <span class="ltrLabel">PM Name</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPMName" runat="server" CssClass="ltrLabel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="ltrLabel">User ID Login</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtUserLogin" runat="server" CssClass="ltrLabel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="ltrLabel">Email</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="ltrLabel"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span class="ltrLabel">Phone No</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPhoneNo" runat="server" CssClass="ltrLabel"></asp:TextBox>
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
            </table>
            <div>
                <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="btnstyle" Width="100px" />
                <asp:Button ID="BtnResetPass" runat="server" Text="Auto Reset Password" CssClass="btnstyle_gray" Width="120px" />
            </div>
            <div>
                <asp:Label ID="LblErrorMessage" runat="server" CssClass="lblField"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
