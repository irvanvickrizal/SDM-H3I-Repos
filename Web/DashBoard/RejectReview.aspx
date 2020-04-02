<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RejectReview.aspx.vb" Inherits="RejectReview" %>
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Reject Document Reviewer</title>
        <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
        <script language="JavaScript" type="text/javascript">
            function WindowsCloseReject(){
                alert('Document Rejected sucessfully.');
                var tskid = getQueryVariable('tskid');
                window.opener.location.href = '../dashboard/frmDocReviewer.aspx?id=' + tskid + '&time=' + (new Date()).getTime();
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function getQueryVariable(variable){
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    if (pair[0] == variable) {
                        return pair[1];
                    }
                }
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <caption>
                    </caption>
                    <tr>
                        <td class="pageTitle" colspan="2">
                            Reviewer Reject Documents
                        </td>
                    </tr>
                    <tr>
                        <td class="lblTitle" style="height: 123px">
                            Enter Remarks:&nbsp;
                        </td>
                        <td style="height: 123px">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="textFieldStyle" Height="89px" Width="394px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="buttonStyle" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>