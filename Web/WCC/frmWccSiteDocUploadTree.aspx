<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmWccSiteDocUploadTree.aspx.vb" Inherits="WCC_frmWccSiteDocUploadTree" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    <link rel="stylesheet" type="text/css" href="../CSS/Styles.css" />
    <style type="text/css">
        .lblTextC
        {
        	font-family:Verdana;
            font-size:8pt;
            color:Green;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function checkIsEmpty()
        {
            var msg="";
            var e = document.getElementById("ddlWF"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Workflow should be select\n" ;
            }
            if (msg != "")
            {
                alert("Mandatory field information required \n\n" + msg);
                return false;
            }
            else
            {
                return accessConfirm();
            }          
        }
        function accessConfirm()
        {
            var r = confirm("Are you sure you want to save the details?");
            if (r == true)
            {
                return true;
            }
            else
            {return false;}
        }
        
        function myPostBack()
        {    
          var o = window.event.srcElement;
          if (o.tagName == "INPUT" && o.type == "checkbox")
              {__doPostBack("","");
              } 
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <label id="lblError" runat="server"></label>
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
                    <td colspan="1">
                    </td>
                </tr>
                <tr class="pageTitle">
                    <td colspan="3">
                        Wcc
                        Site Document Upload
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr style="height: 5">
                    <td colspan="3">
                        &nbsp;
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 15%;">
                        Po No<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="height: 21px; width: 227px;">
                        <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="height: 21px">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Site<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 227px">
                        <asp:DropDownList ID="ddlsite" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                        
                                                </asp:DropDownList>
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>&nbsp;<asp:Button
                            ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        Integration Date
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td id="lblIntDate" runat="server" class="lblText" style="width: 227px">
                    </td>
                    <td runat="server" class="lblText">
                    </td>
                </tr>
                <tr>
                    <td class="lblTitle" valign="top">
                        Required Documents<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td valign="top" style="width: 10px">
                        :
                    </td>
                    <td valign="top" style="width: 227px">
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
                    <td valign="top">
                    </td>
                </tr>
                <tr class="lblTitle"><td align="left">
                    </td><td  colspan="2" align="left">
                        </td>
                    <td align="left" colspan="1">
                        &nbsp;</td>
                </tr>
                <tr class="lblTitle">
                    <td align="left">
                    </td>
                    <td align="right" colspan="2">
                        </td>
                    <td align="left" colspan="1">
                    </td>
                </tr>
                <tr class="lblTitle">
                    <td align="left" colspan="4">
                        </td>
                </tr>
            </table>
            <asp:Label ID="lblsite" runat="server" CssClass="lblText" Text="Label" Visible="False"
                Width="130px"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
        <table border="0" style="width: 574px">
            <tr>
                <td style="width: 424px">
                    <strong class="lblTitle">
                    Additional Doc</strong></td>
                <td style="width: 100px">
                    <asp:FileUpload ID="fileUpload" runat="server" EnableTheming="True" Width="392px" /></td>
                <td style="width: 100px">
                    <asp:Button ID="btnUpload" runat="server" CssClass="goButtonStyle" Text="Upload" /></td>
            </tr>
            <tr>
                <td colspan="2">
                        <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Visible="False" Width="389px" Font-Bold="True"></asp:Label></td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    &nbsp;&nbsp;
    <asp:HiddenField ID="hdndocid" runat="server" />
    <asp:HiddenField ID="hdnsiteid" runat="server" />
    <asp:HiddenField ID="hdnpoId" runat="server" />
    <input type="hidden" runat="server" id="hdnBAUT" />
    <input type="hidden" runat="server" id="hdnBAST1" />
    <input type="hidden" runat="server" id="hdnBAST2" />
    </form>
</body>
</html>
