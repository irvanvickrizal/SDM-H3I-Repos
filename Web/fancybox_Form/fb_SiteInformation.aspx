<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_SiteInformation.aspx.vb"
    Inherits="fancybox_Form_fb_SiteInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Information</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin-top: 0px; margin-left: 20px;">
                <span class="lblTextTitle">PO Number</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img1" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayPoNo" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">PO Name</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img2" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayPOName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Site No/Site Name refer to PO</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img8" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplaySiteNoPO" runat="server" CssClass="lblText"></asp:Label>/<asp:Label
                                ID="DisplaySiteNamePO" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Site No/Site Name refer to EPM</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img3" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplaySiteNo" runat="server" CssClass="lblText"></asp:Label>/<asp:Label
                                ID="DisplaySiteName" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Scope</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img5" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayScope" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">WorkpackageId</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img4" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayWPID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-top: 8px; margin-left: 20px;">
                <span class="lblTextTitle">Project Id</span>
            </div>
            <div style="margin-left: 30px;">
                <table>
                    <tr valign="top">
                        <td>
                            <img id="Img6" src="~/Images/help-icn.png" alt="helpicon" runat="server" width="16"
                                height="16" />
                        </td>
                        <td>
                            <asp:Label ID="DisplayProjectID" runat="server" CssClass="lblText"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
