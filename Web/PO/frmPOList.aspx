<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOList.aspx.vb" Inherits="PO_frmPOList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PO Management</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            padding-left:10px;
            color: white;
            font-family: Trebuchet MS;
            font-size: 16px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 99%; height: 450px;">
            <div class="headerpanel">
                Site Document Pending
            </div>
            <div style="margin-top: 5px; text-align: left;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="lblTitle" style="width: 20%">Search</td>
                        <td style="width: 1%">:</td>
                        <td>
                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                                <asp:ListItem Value="SiteNo">Site Id</asp:ListItem>
                                <asp:ListItem Value="WorkPkgId">Work Package Id</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
                    </tr>
                    <tr>
                        <td class="lblTitle" style="height: 21px; width: 20%">Select PO No.</td>
                        <td style="height: 21px">:</td>
                        <td style="height: 21px">
                            <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>

                </table>
            </div>
            <div style="margin-top: 10px;">
                <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" PageSize="15">
                    <PagerSettings Position="Bottom" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" Height="20px" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText=" No. ">
                            <ItemTemplate>
                                <%# container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SiteID(EPM)">
                            <ItemTemplate>
                                <a href="frmPODetails.aspx?id='<%# Eval("SiteNo") %>'&sno=<%# Eval("po_id") %>&TT=P" style="font-family:Verdana;font-size:11px;">
                                    <%# Eval("SiteNo") %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="siteidpo" HeaderText="SiteID(PO)" />
                        <asp:BoundField DataField="sitenamepo" HeaderText="Site Name(PO)" />
                        <asp:BoundField DataField="Scope" HeaderText="Scope" />
                        <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" />
                        <asp:BoundField DataField="tselprojectid" HeaderText="BTS Type" />
                        <asp:BoundField DataField="taskcompleted" HeaderText="Work Completed" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <%--Modified by Fauzan, 7 Nov 2018. Remove 3 Logo--%>
        <%--<div style="text-align: right; width: 99%; vertical-align: bottom;">
            <img src="~/images/three-logo.png" alt="threelogo" runat="server" id="threelogoid" height="70" width="50" />
        </div>--%>
    </form>
</body>
</html>
