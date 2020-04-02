<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDeemApprovalRpt.aspx.vb" Inherits="Deem_Approval_frmDeemApprovalRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deem Approval Report</title>
    <style type="text/css">
        .ltrLabel {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }

        .lblText {
            font-family: verdana;
            font-size: 11px;
            color: #000;
        }

        .lblTextSmall {
            font-family: verdana;
            font-size: 10px;
            color: #000;
        }

        .lblBold {
            font-family: verdana;
            font-size: 16px;
            color: #000;
            font-weight: bold;
        }

        .lblField {
            font-family: verdana;
            font-size: 11px;
            font-weight: bolder;
            color: #000;
        }

        .lblFieldSmall {
            font-family: verdana;
            font-size: 9px;
            font-weight: bolder;
            color: #000;
        }

        .lblFieldSmallRed {
            font-family: verdana;
            font-size: 10px;
            font-weight: bolder;
            color: White;
        }

        .HeaderPanel {
            background-color: #cfcfcf;
            font-family: verdana;
            font-size: 13px;
            font-weight: bold;
            margin-bottom: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 5px;
        }

        .HeaderGrid {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bold;
            color: white;
            background-color: Orange;
            border-color: black;
            border-style: solid;
            border-width: 1px;
            vertical-align: middle;
            padding-top: 5px;
            padding-bottom: 5px;
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

        .footerGrid {
            font-family: Verdana;
            font-size: 11px;
            color: white;
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
            border-color: Black;
            color: White;
            cursor: pointer;
            padding: 5px;
        }

        .btnSearch {
            height: 29px;
            width: 80px;
            background-image: url(../Images/button/BtSearch_0.gif);
        }

            .btnSearch:hover {
                height: 29px;
                width: 80px;
                background-image: url(../Images/button/BtSearch_1.gif);
            }

            .btnSearch:click {
                height: 29px;
                width: 80px;
                background-image: url(../Images/button/BtSearch_2.gif);
            }

        .warningMessage {
            font-family: Verdana;
            font-size: 11px;
            font-weight: bolder;
            color: Red;
            font-style: italic;
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
    <script type="text/javascript">
        function invalidExportToExcel() {
            alert('Data is empty, please try again!');
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
                    <td style="width: 75%">Deem Approval Report
                    </td>
                    <td style="width: 14%; text-align: right;">
                        <asp:Button ID="BtnExportExcel" runat="server" Text="Export To Excel" CssClass="btnstyle"
                            BackColor="#7f7f7f" Width="120px" />
                    </td>
                </tr>
            </table>
        </div>
        <div>

        </div>
    </form>
</body>
</html>
