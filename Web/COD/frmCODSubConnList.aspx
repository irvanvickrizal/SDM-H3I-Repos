<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODSubConnList.aspx.vb" Inherits="COD_frmCODSubConnList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/Validation.js">
  
  </script>
  <script language="javascript"  type="text/javascript" >
  function SUBCON(id)
            {
                window.open('frmCODSubConSiteDetailsView.aspx?id='+id,'welcome','width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

            }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>       
    <table cellpadding="0" cellspacing="0" width="100%">
      <tr class="pageTitle">
        <td colspan="2"  id="tdTitle" runat="server">Originator</td><td align="right"class="pageTitleSub">List</td>
      </tr>
       <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
      <tr>
        <td class="lblTitle">
          Search Site</td>
        <td style="width: 1%">
          :</td>
        <td>
          <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
            <asp:ListItem Value="Site_No">Site ID</asp:ListItem>
            <asp:ListItem Value="C.[Name]">Originator</asp:ListItem>
          </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
          <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
      </tr>
      <tr>
        <td class="lblTitle">
          Zone</td>
        <td>
          :</td>
        <td>
          <asp:DropDownList ID="DDLZone" CssClass="selectFieldStyle" runat="server" AutoPostBack="True">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right">
          <input type="hidden" runat="server" id="hdnSort" />
         <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp; 
         <input id="btnCancel" type="button" value="Cancel" runat="server" class="buttonStyle" />                   
         </td>
      </tr>
    </table>
   <br /> 
    <asp:GridView ID="grdSubCon" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
      AutoGenerateColumns="False" EmptyDataText="No Records Found">
      <PagerSettings Position="TopAndBottom" />
      <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
      <AlternatingRowStyle CssClass="GridEvenRows" />
      <RowStyle CssClass="GridOddRows" />
      <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
      <Columns>
      <asp:TemplateField HeaderText=" Total ">
          <ItemStyle HorizontalAlign="Right" Width="1%" />
          <ItemTemplate>
            <asp:Label ID="lblno" runat="Server"></asp:Label>
          </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="Site No" SortExpression="Site_No">
            <ItemTemplate>
                <a href="#" onclick="SUBCON('<%# DataBinder.Eval(Container.DataItem,"Site_ID") %>')"> <%#DataBinder.Eval(Container.DataItem, "Site_No")%>
                </a>
            </ItemTemplate>
        </asp:TemplateField>              
        <%--<asp:BoundField DataField="Site_No" HeaderText="Site ID" SortExpression="Site_No" />--%>
        <asp:BoundField DataField="Site_Name" HeaderText="Name" SortExpression="Site_Name" />
        <asp:BoundField DataField="Site_Desc" HeaderText="Description" SortExpression="Site_Desc" Visible="False" >
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="ZNName" HeaderText=" Zone " SortExpression="ZNName" >
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Originator" SortExpression="Name" >
            <ItemStyle Width="15%" />
        </asp:BoundField>
      </Columns>
    </asp:GridView>
        &nbsp; &nbsp; &nbsp;&nbsp;
    </div>
    </form>
</body>
</html>
