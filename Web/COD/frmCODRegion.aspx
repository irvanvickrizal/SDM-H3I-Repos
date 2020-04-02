<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODRegion.aspx.vb" Inherits="frmCODRegion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>NSN :: Region List</title>
  <link href="../css/Styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>

<script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("DDLARA"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Area should not be Empty\n";
        }
        if (IsEmptyCheck(document.getElementById("txtRGNName").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }       
        if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }   
    }    
</script>

<body>
  <form id="frmCODRegion" runat="server">
    <div>
      <table id="tblRegion" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%"
        visible="false">
        <tr >
          <td  align="left" colspan="2" class="pageTitle" style="height: 21px">Region</td><td id="rowadd" runat="server" align="right" class="pageTitleSub">Create</td>          
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>        
        <tr>
          <td class="lblTitle">Name <font style="color:Red; font-size:16px"><sup> * </sup></font></td>
          <td>:</td>
          <td>
            <input type="text" id="txtRGNName" runat="server" class="textFieldStyle" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="lblTitle">Description </td>
          <td>:</td>
          <td><input type="text" id="txtRGNDesc" runat="Server" class="textFieldStyle" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="lblTitle">Select Area <font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">:</td>
          <td><asp:DropDownList ID="DDLARA" runat="server" CssClass="selectFieldStyle" Width="109px"></asp:DropDownList></td>
        </tr>        
        <tr>
          <td colspan="2">
          </td>
          <td>
            &nbsp;<br />
            <asp:Button ID="btnSave" runat="server" CssClass="buttonStyle" Text="Save" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonStyle" Visible="False" />
          </td>
        </tr>
      </table>
      <br />
      <table cellpadding="0" cellspacing="0" width="100%">
        <tr class="pageTitle">
          <td colspan="2" id="tdTitle" runat="server">Region</td><td align="right"class="pageTitleSub">List</td>
        </tr>
        <tr>
          <td class="lblTitle">
            Search Region</td>
          <td style="width: 1%">
            :</td>
          <td>
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle" Width="74px">
              <asp:ListItem Value="RGNName">Name</asp:ListItem>
              <asp:ListItem Value="RGNDesc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
        <tr>
          <td class="lblTitle">
            Area</td>
          <td>
            :</td>
          <td>
            <asp:DropDownList ID="DDLArea" CssClass="selectFieldStyle" runat="server" AutoPostBack="True">
              <asp:ListItem Value="1">Area1</asp:ListItem>
              <asp:ListItem Value="2">Area2</asp:ListItem>
              <asp:ListItem Value="3">Area3</asp:ListItem>
              <asp:ListItem Value="4">Area4</asp:ListItem>
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td class="lblTitle" style="height: 24px">
          </td>
          <td style="height: 24px">
          </td>
          <td align="right" style="height: 24px">
            <input type="hidden" runat="server" id="hdnSort" />
            <asp:Button ID="btnNewGroup" runat="server" CssClass="buttonStyle" Text="Create" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" CssClass="buttonStyle" Text="Cancel" />                    
          </td>
        </tr>        
      </table>
      <br />
      <asp:GridView ID="grdRGN" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
        AutoGenerateColumns="False" PageSize="5" EmptyDataText="No Records Found">
        <PagerSettings Position="TopAndBottom" />
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
        <AlternatingRowStyle CssClass="GridEvenRows" />
        <RowStyle CssClass="GridOddRows" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
        <Columns>
          <asp:TemplateField HeaderText=" Total ">
            <ItemStyle HorizontalAlign="Right" Width="2%" />
            <ItemTemplate>
              <asp:Label ID="lblno" runat="Server"></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:HyperLinkField DataTextField="RGNName" DataNavigateUrlFields="RGN_ID" DataNavigateUrlFormatString="frmCODRegion.aspx?id={0}&amp;Mode=E"
            HeaderText="Name" SortExpression="RGNName">
            <ItemStyle Width="" />
          </asp:HyperLinkField>
          <asp:BoundField DataField="RGNDesc" ItemStyle-Width="30%" HeaderText="Description" SortExpression="RGNDesc" />
          <asp:BoundField DataField="ARA_Name" ItemStyle-Width="30%" HeaderText="Area" SortExpression="ARA_Name" />
        </Columns>
      </asp:GridView>
    </div>
  </form>
</body>
</html>
