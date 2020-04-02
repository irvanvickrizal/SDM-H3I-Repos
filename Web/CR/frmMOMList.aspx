<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMOMList.aspx.vb" Inherits="frmMOMList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding="0" cellspacing="1">
      <tr>
        <td class="pageTitle" colspan="3">
            Change Order (MOM)</td>
      </tr>
        <tr>
            <td class="lblTitle">
            </td>
            <td>
            </td>
        </tr>
      <tr>
        <td class="lblTitle" style="width:20%">Search MOM Ref No.</td><td style="width:1%">:</td>
        <td><asp:DropDownList ID="ddlselect" runat="server" CssClass="selectFieldStyle">
                <asp:ListItem Value="MOMRefNo">MOM Ref No</asp:ListItem>
                <asp:ListItem Value="Subject">Subject</asp:ListItem>
                <asp:ListItem Value="Location">Location</asp:ListItem>
          </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
          <asp:Button ID="BtnSearch" runat="server" Text="GO" CssClass="goButtonStyle" /></td>
      </tr>
      <tr style="visibility:hidden"><td class="lblTitle" style="height: 19px">Status</td><td style="height: 19px">:</td><td style="height: 19px"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
        <asp:ListItem Value="N" Text="Ready for CR"></asp:ListItem>        
      </asp:DropDownList></td></tr>
      <tr>
        <td align="right" colspan="3" style="height: 24px">
          <input id="hdnSort" type="hidden" runat="server" />
          <asp:Button ID="BtnCreate" runat="server" CssClass="buttonStyle" Text="Create" />&nbsp;
          <asp:Button ID="btnCancel" runat="server" CssClass="buttonStyle" Text="Cancel" Visible="false" />
        </td>
      </tr>      
      <tr>
        <td colspan="3"><br />
          <asp:GridView ID="GrdMOM" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" EmptyDataText="No Records Found" Width="100%">
            <PagerSettings Position="TopAndBottom" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle " />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <Columns>
              <asp:TemplateField HeaderText=" Total ">
                <ItemStyle HorizontalAlign="Right" Width="1%" />
                <ItemTemplate>
                  <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
              </asp:TemplateField>                
              <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmMOMGenerate.aspx?id={0}&Type=N"
                DataTextField="MOMRefNo" SortExpression="MOMRefNo" HeaderText="MOM Ref No ">
                <ItemStyle Width="15%" />
              </asp:HyperLinkField>
              <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmMOMGenerate.aspx?id={0}&Type=P"
                DataTextField="MOMRefNo" SortExpression="MOMRefNo" HeaderText="MOM Ref No ">
                <ItemStyle Width="15%" />
              </asp:HyperLinkField>
              <asp:BoundField DataField="MomWriter" HeaderText="Writer" SortExpression="MomWriter" />
              <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
              <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
              <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmMOM.aspx?id={0}&Type=E"
                Text="Edit"  HeaderText="Edit">
                <ItemStyle Width="15%" />
              </asp:HyperLinkField>
            </Columns>
            
          </asp:GridView>
        </td>
      </tr>
    </table>
    </div>
    </form>
</body>
</html>
