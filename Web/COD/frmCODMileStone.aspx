<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODMileStone.aspx.vb" Inherits="COD_frmCODMileStone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Untitled Page</title>
  <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

  <script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtCode").value) == false)
        {
            msg = msg + "Code should not be Empty\n";
        }
        
        if (msg != "")
        {
            alert("Mandatory field information : \n\n" + msg);
            return false;
        }
        else
        {
            return true;
        }           
    }               
  </script>

</head>
<body>
  <form id="form1" runat="server">
    <div>
      <table id="tblDetails" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" visible="false">
        <tr >
          <td colspan="2" class="pageTitle">MileStone</td><td align="right" runat="server" id="addrow" class="pageTitleSub">Create</td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle">Code<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">:</td>
          <td>
            <input type="text" id="txtCode" runat="server" class="textFieldStyle" maxlength="6" /></td>
        </tr>
          <tr>
              <td class="lblTitle">
                  Short Description</td>
              <td style="width: 1%">:</td>
              <td><input type="text" id="txtShortDesc" runat="server" class="textFieldStyle" maxlength="30" />
              </td>
          </tr>
        <tr>
          <td class="lblTitle" valign="top" style="height: 53px">Description<font style=" color:Red; font-size:16px"><sup> </sup></font></td>
          <td style="width: 1%; height: 53px;" valign="top">:</td>
          <td style="height: 53px">
            <textarea id="txtDesc" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
        </tr>
        <tr>
          <td colspan="2">
          </td>
          <td>
            &nbsp;<br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
        </tr>
      </table>
        <br />
      <table cellpadding="0" cellspacing="0" width="100%">
        <tr class="pageTitle">
          <td colspan="2"  id="tdTitle" runat="server">MileStone</td><td align="right" class="pageTitleSub">List</td>
        </tr>
        <tr>
          <td class="lblTitle">Search MileStone</td>
          <td style="width: 1%">:</td>
          <td>
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
              <asp:ListItem Value="INT_CODE">Code</asp:ListItem>
              <asp:ListItem Value="INT_DESC">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
        <tr>
          <td align="right" colspan="3">
            <input type="hidden" runat="server" id="hdnSort" />
            <input id="btnNew" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;            
            <input id="btnCancel" type="button" value="Cancel" runat="server" class="buttonStyle" />          
          </td>
        </tr>
      </table>
      <br />
      <asp:GridView ID="grdMilestone" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="True"
        AllowSorting="True" Width="100%" AutoGenerateColumns="False">
        <PagerSettings Position="TopAndBottom" />
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
        <AlternatingRowStyle CssClass="GridEvenRows" />
        <RowStyle CssClass="GridOddRows" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
        <Columns>
          <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
              <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
          </asp:TemplateField>
          <asp:HyperLinkField ItemStyle-Width="10%" DataTextField="INT_CODE" DataNavigateUrlFields="INT_ID"
            DataNavigateUrlFormatString="frmCODMileStone.aspx?id={0}" HeaderText="Code" SortExpression="INT_CODE" />
          <asp:BoundField DataField="INT_Short_DESC" HeaderText="Short Description" SortExpression="INT_Short_DESC" />  
          <asp:BoundField DataField="INT_DESC" HeaderText="Description" SortExpression="INT_DESC" />
        </Columns>
      </asp:GridView>
    </div>
  </form>
</body>
</html>
