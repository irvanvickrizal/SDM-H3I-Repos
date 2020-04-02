<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteDocUploadTree.aspx.vb"
    Inherits="frmSiteDocUploadTreeNew" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/Styles.css" />
    <style type="text/css">
        .lblTextC {
            font-family: Verdana;
            font-size: 8pt;
            color: Green;
        }

        .headerpanel {
            background-color: gray;
            padding: 4px;
            color: black;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function checkIsEmpty() {
            var msg = "";
            var e = document.getElementById("ddlWF");
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0) {
                msg = msg + "Workflow should be select\n";
            }
            if (msg != "") {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else {
                return accessConfirm();
            }
        }
        function accessConfirm() {
            var r = confirm("Are you sure you want to save the details?");
            if (r == true) {
                return true;
            }
            else { return false; }
        }
        function DocumentNotCompleted() {
            alert("Child Document(s) Not Yet Completed !");
            return false;
        }
        function RFTReadyCreationWarning() {
            alert("Please upload RFT Doc through the RFT Ready Creation in your Dashboard!");
            return false;
        }
        function myPostBack() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <label id="lblError" runat="server">
        </label>
        <div style="width: 99%; height: 450px">
            <div class="headerpanel">
                Site Document Upload
            </div>
            <div style="margin-top: 5px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblmsg" runat="server" Text="All Documents  Uploaded for this Site"
                                        Font-Bold="True" Font-Names="Verdana" ForeColor="#004000" Visible="False"></asp:Label><br />
                                    <asp:Button ID="btndone" runat="server" Text="Done" OnClick="btndone_Click" Visible="False"
                                        CssClass="buttonStyle" />
                                </td>
                            </tr>

                            <tr style="height: 5">
                                <td colspan="3">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle" style="width: 15%;">Po No<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                                </td>
                                <td>:
                                </td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">Site<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlsite" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>&nbsp;<asp:Button
                                ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblTitle">Work Completed Date
                                </td>
                                <td>:
                                </td>
                                <td id="lblIntDate" runat="server" class="lblText"></td>
                            </tr>
                            <tr>
                                <td class="lblTitle">WorkPackage ID</td>
                                <td>:
                                </td>
                                <td id="Td1" runat="server" class="lblText">
                                    <asp:Label ID="lblwpid" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="lblTitle" valign="top">Required Documents<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                                </td>
                                <td valign="top">:
                                </td>
                                <td valign="top">
                                    <asp:TreeView ID="TreeView1" runat="server" CssClass="tree" AutoGenerateDataBindings="false"
                                        NodeIndent="10" ExpandDepth="3" MaxDataBindDepth="4" ShowLines="True">
                                        <ParentNodeStyle Font-Bold="True" ForeColor="Blue" />
                                        <RootNodeStyle Font-Bold="True" ForeColor="Blue" />
                                        <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="None" BorderWidth="1px"
                                            Font-Underline="False" HorizontalPadding="1px" VerticalPadding="0px" />
                                        <NodeStyle CssClass="instructionalMessage" HorizontalPadding="1px" NodeSpacing="0px"
                                            VerticalPadding="0px" />
                                    </asp:TreeView>
                                </td>
                            </tr>
                            <tr style="height: 5">
                                <td colspan="2"></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--Modified by Fauzan, 7 Nov 2018. Remove 3 Logo--%>
        <%--<div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>--%>
        <asp:HiddenField ID="hdndocid" runat="server" />
        <asp:HiddenField ID="hdnsiteid" runat="server" />
        <asp:HiddenField ID="hdnpoId" runat="server" />
        <asp:HiddenField ID="hdnwpId" runat="server" />
        <input type="hidden" runat="server" id="hdnBAUT" />
        <input type="hidden" runat="server" id="hdnBAST1" />
        <input type="hidden" runat="server" id="hdnBAST2" />
    </form>
</body>
</html>
