<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fb_ATPMergeDocViewLog.aspx.vb"
    Inherits="fancybox_Form_fb_ATPMergeDocViewLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Merge Doc Log</title>
    <style type="text/css">
        .gridHeader_2
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #ffc727;
            font-weight: bolder;
            text-align: center;
            padding: 5px;
            color: white;
            border-style: solid;
            border-width: 2px;
            border-color: gray;
        }
        .gridOdd
        {
            font-family: Verdana;
            font-size: 11px;
            padding: 5px;
        }
        .gridEven
        {
            font-family: Verdana;
            font-size: 11px;
            background-color: #cfcfcf;
            padding: 5px;
        }
        #panelHeader
        {
            background-color: #cfcfcf;
            padding: 8px;
            font-family: verdana;
            font-size: 13px;
            font-weight: bolder;
            color: #ffffff;
            border-radius: 5px;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="panelHeader">
        <asp:Label ID="LblViewLogDocName" runat="server" Text="ATP Merge Document Log"></asp:Label>
    </div>
    <div style="margin-top: 10px;">
        <asp:GridView ID="GvAuditLog" runat="server" AutoGenerateColumns="false" Width="100%">
            <RowStyle CssClass="gridOdd" />
            <AlternatingRowStyle CssClass="gridEven" />
            <Columns>
                <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="gridHeader_2" ItemStyle-BorderColor="graytext"
                    ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PreparationStatus" HeaderText="Task Event" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                <asp:BoundField DataField="Fullname" HeaderText="User Name" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                <asp:BoundField DataField="Rolename" HeaderText="Role" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
                <asp:BoundField DataField="ExecuteDate" HeaderText="Date of Execution" HeaderStyle-CssClass="gridHeader_2"
                    ItemStyle-BorderColor="graytext" ItemStyle-BorderStyle="solid" ItemStyle-BorderWidth="2px" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
