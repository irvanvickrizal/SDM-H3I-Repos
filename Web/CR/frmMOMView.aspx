<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMOMView.aspx.vb" Inherits="frmMOMView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function ViewDocument(id)
    {
     window.open('../cr/frmviewdocument.aspx?id='+id,'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding="0" cellspacing="1">
      <tr>
        <td class="pageTitle" colspan="3">
            View MOM</td>
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
          <asp:Button ID="BtnSearch" runat="server" Text="GO" CssClass="goButtonStyle" />
          <input id="hdnSort" type="hidden" runat="server" /></td>
      </tr>
      <tr style="visibility:hidden"><td class="lblTitle" style="height: 19px">Status</td><td style="height: 19px">:</td><td style="height: 19px"><asp:DropDownList ID="ddlStatus" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
        <asp:ListItem Value="N" Text="Ready for CR"></asp:ListItem>        
      </asp:DropDownList></td></tr>
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
               <asp:TemplateField HeaderText="Document">
                                    <ItemTemplate>
                                        <a href="#" onclick="ViewDocument(<%# DataBinder.Eval(Container.DataItem,"mom_id") %>)">
                                            <%# DataBinder.Eval(Container.DataItem,"MOMRefNo") %>
                                        </a>
                                     </ItemTemplate>
                                </asp:TemplateField>               
                            <asp:BoundField DataField="MomWriter" HeaderText="Writer" SortExpression="MomWriter" />
              <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
              <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
              
            </Columns>
            
          </asp:GridView>
        </td>
      </tr>
    </table>
    </div>
    </form>
</body>
</html>
