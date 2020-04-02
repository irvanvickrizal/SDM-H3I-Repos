<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPartnerSetup.aspx.vb"
    Inherits="USR_frmPartnerSetup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Partner Profile Setup</title>
    <style type="text/css">
        .HeaderReport
        {
            background-color:#cfcfcf;
            font-family:verdana;
            font-size:9.5pt;
            font-weight:bolder;
            margin-top:0px;
            margin-bottom:10px;
            text-align:center;
            padding-top:4px;
            padding-bottom:4px;
            color:red;
        }
        .ltrLabel
        {
            font-family:verdana;
            font-size:8pt;
            color:#000;
        }
        .lblField
        {
            font-family:verdana;
            font-size:8pt;
            font-weight:bolder;
            color:#000;
        }
        .btnStyle
        {
            border-style:solid;
            border-width:2px;
            border-color:#c3c3c3;
            padding:3px;
            font-family:verdana;
            font-size:8pt;
            cursor:Pointer;
        }
    </style>

    <script type="text/javascript">
        function NoCompanyDefined() {
            alert('You Must Choose Your Current Company First!');
            return false;
        }
        function ConfirmationBox(desc) {
            var url = '/PO/frmSiteDocUploadTreeSubcon.aspx';
            var result = confirm('Are you sure your current company is '+desc+' ?' );
            if (result) {
            location.href = url;
            return true;
            }
            else {
            return false;
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: auto; margin: 0 25%; margin-top: 10%;">
        <div style="width: 500px; padding: 5px; border-style: solid; border-width: 1px; border-color: Black;
            ">
            <div class="HeaderReport">
                Please Update Current Company for WCC Online requirement
            </div>
            <div style="padding: 3px;">
                <table>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrFullname" runat="server" Text="Fullname"></asp:Literal>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="LblFullname" runat="server" CssClass="ltrLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrSignTitle" runat="server" Text="Title"></asp:Literal>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="LblSignTitle" runat="server" CssClass="ltrLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrPhoneNo" runat="server" Text="PhoneNo"></asp:Literal>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="LblPhoneNo" runat="server" CssClass="ltrLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrEmail" runat="server" Text="Email"></asp:Literal>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:Label ID="LblEmail" runat="server" CssClass="ltrLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ltrLabel">
                            <asp:Literal ID="LtrCompanyName" runat="server" Text="Company Name"></asp:Literal>
                        </td>
                        <td>:</td>
                        <td>
                            <asp:MultiView ID="MvCompany" runat="server">
                                <asp:View ID="VwOnly" runat="server">
                                    <span class="ltrLabel">
                                        <asp:Literal ID="LtrCompany" runat="server"></asp:Literal></span>&nbsp;<asp:LinkButton
                                            ID="LbtEdit" runat="server" Text="Edit" CssClass="ltrLabel" ForeColor="blue" Font-Bold="true"></asp:LinkButton>
                                </asp:View>
                                <asp:View ID="VwEditable" runat="server">
                                    <asp:DropDownList ID="DdlCompany" runat="server" CssClass="ltrLabel">
                                    </asp:DropDownList>&nbsp;<asp:LinkButton ID="LbtSaveCompany" runat="server" Text="Save"
                                        CssClass="ltrLabel"></asp:LinkButton>
                                </asp:View>
                            </asp:MultiView>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width:100%; text-align:right;">
                <asp:Button ID="BtnGoToDashboard" runat="server" Text="Go To Dashboard" CssClass="btnStyle" />
            </div>
        </div>
        </div>
    </form>
</body>
</html>
