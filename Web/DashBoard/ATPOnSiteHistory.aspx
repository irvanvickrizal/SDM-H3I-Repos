<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATPOnSiteHistory.aspx.vb"
    Inherits="DashBoard_ATPOnSiteHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP ON-SITE REPORT</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="BtnViewAcceptance" runat="server" Text="ATP OnSite Acceptance" />
            <asp:Button ID="BtnViewRejection" runat="server" Text="ATP OnSite Rejection" />
        </div>
        <div>
            <asp:MultiView ID="MvATPHistorical" runat="server">
                <asp:View ID="vwATPOnSiteAcceptance" runat="server">
                    <div>
                        ATP ON-Site Acceptance
                    </div>
                    <div>
                        <asp:GridView ID="gvATPOnSiteAcceptance" runat="server" AutoGenerateColumns="false">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="vwATPOnSiteRejection" runat="server">
                    <div>
                        ATP ON-Site Rejection
                    </div>
                    <div>
                        <asp:GridView ID="gvATPOnSiteRejection" runat="server" AutoGenerateColumns="false">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
