<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODDocumentUserAuthorized.aspx.vb"
    Inherits="COD_frmCODDocumentUserAuthorized" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Authorized Document Type</title>
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
        .BtnExpt
        {
           border-style:solid;
           border-color:gray;
           border-width:1px;
           font-family:verdana;
           font-size:11px;
           font-weight:bold;
           color:white;
           height:25px;
           cursor:hand;
        }
    </style>
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
        <asp:MultiView ID="MvPanelDocAuth" runat="server">
            <asp:View ID="VwDocUpdated" runat="server">
                <div>
                    <asp:Label ID="LblDocumentName" runat="server" CssClass="textHeader"></asp:Label>
                </div>
                <div id="accordionResizer" style="padding: 10px; width: 300px; height: 250px; margin-top: 10px;"
                    class="ui-widget-content">
                    <div id="divItems" style="width: 300px; height: 50px;">
                        <asp:Repeater ID="RpDocAuthorized" runat="server">
                            <ItemTemplate>
                                <h3>
                                    <a href="#">User Type:
                                        <%# Eval("GrpDesc") %>
                                    </a>
                                </h3>
                                <div style="height: 100px;">
                                    <asp:Label ID="LblGrpId" runat="server" Text='<%#Eval("GrpId") %>' Visible="false"
                                        CssClass="textDesc"></asp:Label>
                                    <asp:GridView ID="GvRoles" runat="server" AutoGenerateColumns="false" CellPadding="0"
                                        Height="50px" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRoleId" runat="server" Text='<%#Eval("RoleId") %>' Visible="false"></asp:Label>
                                                    <asp:CheckBox ID="ChkChecked" runat="server" Text='<%#Eval("RoleDesc") %>' CssClass="textDesc" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div style="margin-top: 10px; text-align: right; width: 200px;">
                    <asp:LinkButton ID="LbtSave" runat="server" Text="[Save Access Permit]" CssClass="textDesc"></asp:LinkButton>
                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="BtnExpt" Visible="false" />
                    <asp:Button ID="BtnClose" runat="server" Text="Close" CssClass="BtnExpt" Visible="false" />
                </div>
            </asp:View>
            <asp:View ID="VwMessagePanel" runat="server">
                <asp:Label ID="LblErrorMessage" runat="server"></asp:Label>
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
