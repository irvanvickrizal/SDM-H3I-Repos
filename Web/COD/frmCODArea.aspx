<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODArea.aspx.vb" Inherits="frmCODArea" %>

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
        if (IsEmptyCheck(document.getElementById("txtARAName").value) == false)
        {
            msg = msg + "Name should not be Empty\n";
        }  
        var e = document.getElementById("DDLJava"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Java should not be empty\n";
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
      <table id="tblArea" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
        visible="false">
        <tr >
          <td colspan="2" class="pageTitle">Area</td><td align="right" runat="server" id="addrow" class="pageTitleSub">Create</td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle">Name<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">
            :</td>
          <td>
            <input type="text" id="txtARAName" runat="server" class="textFieldStyle" maxlength="10" /></td>
        </tr>
        <tr>
          <td class="lblTitle" valign="top">Description</td>
          <td style="width: 1%" valign="top">
            :</td>
          <td>
            <textarea id="txtDetails" runat="server" rows="5" cols="50" class="textFieldStyle"></textarea></td>
        </tr>
          <tr>
              <td class="lblTitle" style="height: 15px; text-align: right;" valign="top">Select Java<font style=" color:Red; font-size:16px"><sup> * </sup></font>
              </td>
              <td style="width: 1%; height: 15px" valign="top">:</td>
              <td style="height: 15px"><asp:DropDownList ID="DDLJava" runat="server"></asp:DropDownList></td>
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
          <td colspan="2"  id="tdTitle" runat="server">Area</td><td align="right" class="pageTitleSub" style="width: 564px">List </td>
        </tr>
        <tr>
          <td class="lblTitle">
            Search Area</td>
          <td style="width: 1%">
            :</td>
          <td style="width: 564px">
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
              <asp:ListItem Value="ARA_Name">Name</asp:ListItem>
              <asp:ListItem Value="ARA_Desc">Description</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
        </tr>
          <tr>
              <td class="lblTitle" style="height: 16px; text-align: right;">
                  Java</td>
              <td style="width: 1%; height: 16px">
                  :</td>
              <td style="height: 16px; width: 564px;">
                  <asp:DropDownList ID="DDLJVA" runat="server" CssClass="selectFieldStyle">
                  </asp:DropDownList></td>
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
      <asp:GridView ID="grdArea" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
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
          <asp:HyperLinkField DataTextField="ARA_Name" DataNavigateUrlFields="ARA_ID" DataNavigateUrlFormatString="frmCODArea.aspx?id={0}&amp;Mode=E"
            HeaderText="Name" SortExpression="ARA_Name">
            <ItemStyle Width="11" />
          </asp:HyperLinkField>
          <asp:BoundField DataField="ARA_Desc" ItemStyle-Width ="30%" HeaderText="Description" SortExpression="ARA_Desc" />
          <asp:BoundField DataField="JV_Name" ItemStyle-Width ="30%" HeaderText="Java" SortExpression="JV_Name" />
        </Columns>
      </asp:GridView>
    </div>
  </form>
</body>
</html>
