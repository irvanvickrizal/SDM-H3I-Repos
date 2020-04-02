<%@ Page Language="VB" AutoEventWireup="false" CodeFile="banner.aspx.vb" Inherits="banner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
            function addLoadEvent(func){
                var oldonload = window.onload;
                if (typeof window.onload != 'function') {
                    window.onload = func;
                }
                else {
                    window.onload = function(){
                        oldonload();
                        func();
                    }
                }
            }
            
            addLoadEvent(showTheTime);
            function showZeroFilled(inValue){
                if (inValue > 9) {
                    return "" + inValue;
                }
                return "0" + inValue;
            }
            
            function TimeRound(){
                realS = realS + 1;
                if (realS > 59) {
                    realS = 0;
                    realM = realM + 1;
                    if (realM > 59) {
                        realM = 0;
                        realH = realH + 1;
                        if (realH > 23) {
                            realH = 0;
                        }
                    }
                }
            }
            
            var realS = timeServer.getSeconds();
            var realM = timeServer.getMinutes();
            var realH = timeServer.getHours();
            
            function showTheTime(){
                TimeRound()
                document.getElementById("spanTime").innerHTML = showZeroFilled(realH) + ":" + showZeroFilled(realM) + ":" + showZeroFilled(realS);
                setTimeout("showTheTime()", 1000);
            }
            
            function showPage(url, s, p, r, pt){
                var aa = url + "?S=" + s + "&P=" + p + "&R=" + r + "&PT=" + pt;
                alert(aa);
                parent.frames.item(0).location.href = aa;
                window.location.replace(aa);//"'"+url +"?S=" + s + "&P=" + p +"&R=" + r + "&PT=" + pt+"'";
                return true;
            }
    </script>

</head>
<body style="vertical-align: top; text-align: left;">
    <form id="form1" runat="server">
        <table style="width: 100%; height: 100%;">
            <tr style="background-color: #e3e3e3; width: 100%" class="lblText" valign="top">
                <td align="left" valign="top" style="height: 18px;">
                    <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Navy">
                    </asp:Label>&nbsp;<span id="spanTime" class="serverTime2"></span>
                </td>
                <td style="color: Navy; width: 15%" class="lblText" align="center">
                    <b></b>
                </td>
                <%--<td align="center">
                    <asp:HyperLink ID="sislink" runat="server" CssClass="ASmall">SIS
                    </asp:HyperLink>&nbsp;&nbsp;
                    <asp:HyperLink ID="sitaclink" runat="server" CssClass="ASmall">SITAC
                    </asp:HyperLink>&nbsp;&nbsp;
                    <asp:HyperLink ID="cmelink" runat="server" CssClass="lblText">CME
                    </asp:HyperLink>
                </td>--%>
                <td align="right" valign="top" width="35%" class="lblText">
                    <asp:HyperLink ID="HyperLink2" runat="server" Target="_top" NavigateUrl="Main.aspx"
                        CssClass="lblText">Home
                    </asp:HyperLink>&nbsp;|&nbsp;
                    <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="#"
                        CssClass="lblText">Help
                    </asp:HyperLink>&nbsp;|&nbsp;
                    <asp:HyperLink ID="HyperLink3" runat="server" Target="_top" NavigateUrl="~/USR/frmChangeProfile.aspx"
                        CssClass="lblText">Change Profile
                    </asp:HyperLink>&nbsp;|&nbsp;
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_top" NavigateUrl="~/USR/frmChangePwd.aspx"
                        CssClass="lblText">Change Password
                    </asp:HyperLink>&nbsp;|&nbsp;
                    <asp:HyperLink ID="hyllogout" runat="server" Target="_top" NavigateUrl="~/frmLogout.aspx"
                        CssClass="lblText">Logout
                    </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" runat="server" Target="_top" NavigateUrl="" CssClass="lblText">
                    </asp:HyperLink>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
