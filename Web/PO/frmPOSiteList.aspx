<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPOSiteList.aspx.vb" Inherits="PO_frmPOSiteList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Document Checklist</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
    <style type="text/css">
        .headerpanel {
            background-color: gray;
            padding: 4px;
            color: black;
            font-family: Trebuchet MS;
            font-size: 14px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 99%; height: 450px">
            <div class="headerpanel">
                Document CheckList
            </div>
            <div style="margin-top: 5px">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="lblTitle" style="width: 20%">Search</td>
                        <td style="width: 1%">:</td>
                        <td>
                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                                <asp:ListItem Value="SiteNo">SiteId</asp:ListItem>
                                <asp:ListItem Value="Scope">Scope</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
                    </tr>
                    <tr>
                        <td class="lblTitle" style="width: 20%">Select PONo</td>
                        <td style="width: 1%">:</td>
                        <td style="height: 21px">
                            <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                            </asp:DropDownList>&nbsp;<asp:LinkButton ID="lnkAll" runat="server" CssClass="ASmall">Display All</asp:LinkButton></td>
                    </tr>

                </table>
            </div>
            <div style="margin-top: 10px;">
                <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="true" AllowSorting="true">
                    <PagerSettings Position="TopAndBottom" />
                    <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" Height="20px" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                        HorizontalAlign="Right" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField HeaderText=" Total " ItemStyle-HorizontalAlign="right" ItemStyle-Width="2%">
                            <ItemTemplate>
                                <%# container.DataItemIndex + 1 %>
                                    .
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:HyperLinkField DataNavigateUrlFields="SiteNo,Sno,PONo,Scope" DataNavigateUrlFormatString="frmSiteDocSetup.aspx?id={0}&Sno={1}&PNo={2}&SC={3}" HeaderText="SiteId" DataTextField="SiteNo" SortExpression="SiteNo" />--%>
                        <asp:TemplateField HeaderText="SiteId">
                            <ItemTemplate>
                                <a href="frmSiteDocSetup.aspx?id=<%# Eval("SiteNo")%>&Sno=<%# Eval("po_id") %>&PNo=<%# Eval("PONo") %>&SC=<%# Eval("Scope")%>&WPID=<%# Eval("WorkPkgId")%>">
                                    <%# Eval("SiteNo") %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Scope" HeaderText="Scope" SortExpression="Scope" />
                        <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" SortExpression="WorkPkgId" />
                        <asp:BoundField DataField="WorkPackageName" HeaderText="WorkPackageName" SortExpression="WorkPkgName" />
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
        <input type="hidden" runat="server" id="hdnDisp" value="1" />
        <input type="hidden" runat="server" id="hdnSort" value="" />
    </form>
</body>
</html>
