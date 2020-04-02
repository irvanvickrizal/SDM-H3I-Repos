<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBORNDocTransferedATP.aspx.vb" Inherits="PO_frmBORNDocTransferedATP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doc Review Panel</title>
    <link href="../CSS/CheckList.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .lblBoldHeader {
            font-family: Verdana;
            font-size: 15px;
            font-weight: bolder;
        }
    </style>

    <script type="text/javascript">

        function ConfirmSuccess() {
            alert('Signed Sucessfully');
            var siteno = getQueryVariable('siteno');
            var wpid = getQueryVariable('wpid');
            window.opener.location.href = '../dashboard/frmDocApproved.aspx?id=' + siteno + '&wpid=' + wpid;
            if (window.opener.progressWindow) {
                window.opener.progressWindow.close();
            }
            window.close();
        }

    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(
            function () {
                if (document.getElementById) {
                    var progress = document.getElementById('PleaseWait');
                    var blur = document.getElementById('blur');
                    progress.style.width = '100%';
                    progress.style.height = '100%';
                }
            }
        )
    </script>

    <style type="text/css">
        #PleaseWait {
            z-index: 200;
            position: absolute;
            top: 0pt;
            left: 0pt;
            text-align: center;
            height: 100px;
            width: 100px;
            background-image: url(../Images/animation_processing.gif);
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
            z-index: 1;
            height: 100%;
            position: fixed;
            top: 0;
            left: 0;
        }
    </style>
    <script type="text/javascript">
        function Showmain(type) {
            if (type == "success") {
                alert('Document Successfully Migrated');
            }
            window.location = '../PO/frmSiteDocUploadTree.aspx'
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="HdWFID" runat="server" />
        <asp:HiddenField ID="HdTskId" runat="server" />
        <asp:HiddenField ID="hdnDocpath" runat="server" />
        <asp:Label ID="lblDocpath" runat="server"></asp:Label>
        <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%"
                id="TABLE1">
                <tr>
                    <td align="center">
                        <iframe runat="server" id="PDFViwer" width="99%" height="750px" style="overflow: scroll;"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <div style="margin-top: 5px;">
            <div style="margin-top: 10px; margin-bottom: 10px; text-align: right; width: 100%;">
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm & Submit" Width="180px" 
                    OnClientClick="return confirm('Are you sure you want to Confirm this ATP is Valid and Copy to this Document tree?')" />
                <asp:Button ID="BtnSignReject" runat="server" Text="Reject" CssClass="btnstylegray" Visible="false"
                    Width="80px" />
            </div>
            <asp:Panel ID="pnlapproval" runat="server" Visible="false">
                <div style="border-style: solid; border-color: Gray; border-width: 1px; margin-left: 2px; margin-right: 2px; width: 450px; text-align: left;">
                    <div style="font-family: Verdana; font-size: 12px; font-weight: bold; background-color: #c3c3c3; padding: 3px; margin-bottom: 10px; text-align: center;">
                        Panel Confirmation
                    </div>

                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
