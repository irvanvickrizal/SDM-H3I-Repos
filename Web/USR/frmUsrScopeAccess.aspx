<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUsrScopeAccess.aspx.vb" Inherits="USR_frmUsrScopeAccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scope of Work User Access</title>
    <style type="text/css">
        .textDesc
        {
            font-family:verdana;
            font-size:10px;
        }
        .textHeader
        {
            font-family:verdana;
            font-size:13px;
            font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 250px; text-align:left;">
            <asp:MultiView ID="MvMainPanel" runat="server">
                <asp:View ID="VwScope" runat="server">
                    <div>
                        <asp:CheckBox ID="ChkTIScope" runat="server" Text="TI Scope" CssClass="textDesc" />
                    </div>
                    <div>
                        <asp:CheckBox ID="ChkCMEScope" runat="server" Text="CME Scope" CssClass="textDesc" />
                    </div>
                    <div>
                        <asp:CheckBox ID="ChkSISScope" runat="server" Text="SIS Scope" CssClass="textDesc" />
                    </div>
                    <div>
                        <asp:CheckBox ID="ChkSitacScope" runat="server" Text="Sitac Scope" CssClass="textDesc" />
                    </div>
                    <div style="text-align:center; margin-top:5px;">
                        <asp:LinkButton ID="LbtSave" runat="server" Text="[Update Scope Access]" CssClass="textDesc"></asp:LinkButton>
                    </div>
                </asp:View>
                <asp:View ID="VwMessage" runat="server">
                    <asp:Label ID="LblMessage" runat="server"></asp:Label>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
