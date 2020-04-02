<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODJava.aspx.vb" Inherits="COD_frmCODJava" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
 <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>  
</head>
<script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtJavaName").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
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

<body>
    <form id="form1" runat="server">
    <div>
      <table id="tblJava" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
        visible="false">
        <tr>
          <td colspan="2"  class="pageTitle">Java</td><td align="right" runat="server" id="addrow"  class="pageTitleSub">Create</td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle" style="width: 200px">Name<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">
            :</td>
          <td>
            <input type="text" id="txtJavaName" runat="server" class="textFieldStyle" maxlength="10" /></td>
        </tr>
        <tr>
          <td class="lblTitle" valign="top" style="width: 200px">Description</td>
          <td style="width: 1%" valign="top">
            :</td>
          <td>
            <textarea id="txtJavaDes" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
        </tr>        
        <tr>
          <td colspan="2" style="height: 18px">
          </td>
          <td style="height: 18px">
            &nbsp;<br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
        </tr>        
      </table>
      <br />
      <table cellpadding="0" cellspacing="0" width="100%">
        <tr >
          <td colspan="2" class="pageTitle" id="tdTitle" runat="server">Java</td><td align="right" class="pageTitleSub">List </td>
          
        </tr>
        <tr>
          <td class="lblTitle">
            Search Java</td>
          <td style="width: 1%">
            :</td>
          <td>
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
              <asp:ListItem Value="JV_Name">Name</asp:ListItem>
              <asp:ListItem Value="JV_Desc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
        <tr>
          <td align="right" colspan="3">
            <input type="hidden" runat="server" id="hdnSort" />
            <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
            <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
            </td>
        </tr>
      </table>
      <br />
      <asp:GridView ID="grdJava" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="True"
        AllowSorting="True" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Records Found">
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
          <asp:HyperLinkField ItemStyle-Width="25%" DataTextField="JV_Name" DataNavigateUrlFields="JV_Id"
            DataNavigateUrlFormatString="frmCODJava.aspx?id={0}&Mode=E" HeaderText="Name" SortExpression="JV_Name" />
          <asp:BoundField DataField="JV_Desc" HeaderText="Description" SortExpression="JV_Desc" />
        </Columns>
      </asp:GridView>
        <br />
    </div>
  </form>
</body>
</html>
