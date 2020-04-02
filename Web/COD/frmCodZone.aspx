<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCodZone.aspx.vb" Inherits="COD_frmCodZone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Untitled Page</title>
  <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>

</head>

<script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg = "";
        var e = document.getElementById("DDLRgn"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Region should not be Empty\n";
        }
        if (IsEmptyCheck(document.getElementById("txtName").value) == false)
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
  <form id="form1" runat="server">
    <div>
      <table id="tblZone" cellpadding="1" border="0" cellspacing="0" runat="server" width="100%" visible="false">
        <tr  >
          <td colspan="2" class="pageTitle">Zone </td><td align="right" id="rowadd" runat="server" class="pageTitleSub">Create </td>
        </tr>
        <tr>
          <td colspan="2">
          </td>
        </tr>
       
        <tr>
          <td class="lblTitle">Name<font style="color:Red; font-size:16px"><sup> * </sup></font></td>
          <td>
            <input type="text" id="txtName" runat="server" class="textFieldStyle" maxlength="30" size="75" /></td>
        </tr>
        <tr>
          <td class="lblTitle">
            Description<font style=" color:Red; font-size:16px"><sup> </sup></font></td>
          <td>
            <input type="text" id="txtDesc" runat="Server" class="textFieldStyle" maxlength="50" size="75" /></td>
        </tr>
         <tr>
          <td class="lblTitle">
            Select Region<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td>
            <asp:DropDownList ID="DDLRgn" runat="server" CssClass="selectFieldStyle">
            </asp:DropDownList></td>
        </tr>      
        <tr>
          <td>
          </td>
          <td><br />
            <asp:Button ID="BtnSave" runat="server" CssClass="buttonStyle" Text="Save" />
            <asp:Button ID="BtnDelete" runat="server" CssClass="buttonStyle" Text="Delete" Visible="false" /></td>
        </tr>
      </table>
    </div>
   <br /> 
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr class="pageTitle">
        <td colspan="2" id="tdTitle" runat="server">Zone</td><td align="right"class="pageTitleSub" style="width: 69px">List </td>
      </tr>      
      <tr>
        <td class="lblTitle">
          Search Zone</td>
        <td>
          <asp:DropDownList ID="ddlselect" runat="server" CssClass="selectFieldStyle">
            <asp:ListItem Value="ZNName">Name</asp:ListItem>
          </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
          <asp:Button ID="BtnSearch" runat="server" Text="GO" CssClass="goButtonStyle" /></td>
      </tr>
      <tr>
        <td class="lblTitle">Region</td>
        <td><asp:DropDownList ID="ddlRegion" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
          </asp:DropDownList></td>
      </tr>
      <tr>
        <td align="right" colspan="3">
          <input id="hdnSort" type="hidden" runat="server" />          
          <asp:Button ID="BtnCreate" runat="server" CssClass="buttonStyle" Text="Create" />&nbsp; 
          <asp:Button ID="btnCancel" runat="server" CssClass="buttonStyle" Text="Cancel" />          
        </td>
      </tr>
      <tr>
        <td colspan="3"><br />
          <asp:GridView ID="GrdZone" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" Width="100%">
            <PagerSettings Position="TopAndBottom" />
            <RowStyle CssClass="GridOddRows" />
            <Columns>
              <asp:TemplateField HeaderText=" Total ">
                <ItemStyle HorizontalAlign="Right" Width="1%" />
                <ItemTemplate>
                  <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
              </asp:TemplateField>
              <asp:HyperLinkField DataNavigateUrlFields="ZN_ID" DataNavigateUrlFormatString="frmCodZone.aspx?id={0}"
                DataTextField="ZNName" SortExpression="ZNName" HeaderText="Name">
                <ItemStyle Width="25%" />
              </asp:HyperLinkField>
              <asp:BoundField DataField="ZNDesc" HeaderText="Description" SortExpression="ZNDesc" />
              <asp:BoundField DataField="RGNName" HeaderText="Region" SortExpression="RGNNAMe" />
            </Columns>
            <PagerStyle CssClass="PagerTitle " />
            <HeaderStyle CssClass="GridHeader" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
          </asp:GridView>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
