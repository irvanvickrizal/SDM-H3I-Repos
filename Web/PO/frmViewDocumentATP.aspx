<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewDocumentATP.aspx.vb"
    Inherits="PO_frmViewDocumentATP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP Document View</title>
    <link rel="stylesheet" type="text/css" href="../Scripts/Styles/global.css" />

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <script src="../Scripts/UI/minified/jquery.ui.core.min.js" type="text/javascript"></script>

    <script src="../Scripts/UI/minified/jquery.ui.widget.min.js" type="text/javascript"></script>

    <script src="../Scripts/UI/minified/jquery.ui.accordion.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function(){
            $("#divItems").accordion({
                fillSpace: true
            });
            $(function() {
		$( "#accordionResizer" ).resizable({
			minHeight: 140,
			resize: function() {
				$( "#accordion" ).accordion( "resize" );
			}
		});
	});
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 100%; width: 100%">
            <div>
                <asp:HiddenField ID="HdnSiteid" runat="server" />
                <asp:HiddenField ID="HdnSiteVersion" runat="server" />
                <asp:MultiView ID="MvMainPanel" runat="server">
                    <asp:View ID="VwHTMLPDF" runat="server">
                        <div id="dvPrint" runat="server" style="width: 100%;">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="height: 295px">
                                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                                            <tr>
                                                <td align="left" valign="top" style="width: 20%">
                                                    <img src="http://www.telkomsel.nsnebast.com/Images/nsn-logo.gif" height="36px" width="104px" />
                                                </td>
                                                <td colspan="4" align="center" class="lblBold" valign="top" style="width: 60%; text-align: center;">
                                                    <b>ATP Approval Sheet</b>
                                                    <br />
                                                </td>
                                                <td align="right" valign="top" style="width: 20%">
                                                    <img src="http://www.telkomsel.nsnebast.com/Images/logo_tsel.png" alt="logotsel" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" style="margin-left: 0px; width: 100%;">
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" colspan="3">
                                                    This approval sheet declares that the Acceptance Test Procedure (ATP) has been performed
                                                    at the below location.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" colspan="3">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 206px;">
                                                    PO No
                                                </td>
                                                <td style="width: 1%;">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 1130px">
                                                    <div id="divPONO" runat="server" style="width: 100%; text-align: left;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 206px;">
                                                    Site Name
                                                </td>
                                                <td style="width: 1%;">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 1130px">
                                                    <div id="divSiteName" runat="server" style="width: 100%; text-align: left;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 206px">
                                                    Site ID
                                                </td>
                                                <td style="width: 1%">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 1130px">
                                                    <div id="divSiteID" runat="server" style="width: 100%; text-align: left;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblText" style="width: 206px">
                                                    Scope
                                                </td>
                                                <td style="width: 1%">
                                                    :
                                                </td>
                                                <td class="lblText" style="width: 1130px;">
                                                    <div id="divScope" runat="server" style="width: 100%; text-align: left;">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="lblText">
                                                    And the work has been considered as complete and satisfactory fulfil the standard
                                                    technical requirements in Telkomsel project.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="Hcap">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="lblText" colspan="4">
                                                    <div id="divReviewer" runat="server" style="width: 100%; text-align: left;">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="lblText">
                                        This approval sheet is automatically generated by eBAST upon acceptance of all person
                                        in charge and no more signatures on hard copy is required. This sheet is inseparable
                                        from the ATP report and should be the first thing seen when opening the report.
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="VwFirstPanel" runat="server">
                        <iframe runat="server" id="docView" width="100%" height="575px" scrolling="auto"></iframe>
                    </asp:View>
                    <asp:View ID="VwSecondPanel" runat="server">
                        <div id="accordionResizer" style="padding: 10px; width: 100%; height: 575px; margin-top: 10px;"
                            class="ui-widget-content">
                            <div id="divItems" style="width: 99%; height: 575px;">
                                <h3>
                                    <a href="#">Approval Sheet </a>
                                </h3>
                                <div>
                                    <iframe runat="server" id="IframeApprovalSheet" width="98%" height="500px" scrolling="auto">
                                    </iframe>
                                </div>
                                <h3>
                                    <a href="#">Supporting document </a>
                                </h3>
                                <div>
                                    <iframe runat="server" id="IframeATPDocument" width="98%" height="500px" scrolling="auto">
                                    </iframe>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </form>
</body>
</html>
