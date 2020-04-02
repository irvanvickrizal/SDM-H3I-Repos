<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCODScopeGrouping.aspx.vb" Inherits="COD_frmCODScopeGrouping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
   <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" /> 
   <script language="javascript" type="text/javascript" src="../include/Validation.js"></script>

  <script language="javascript" type="text/javascript">

    function checkIsEmpty()    
    {
        var msg = "";
        if (IsEmptyCheck(document.getElementById("txtGroup").value) == false)
        {
            msg = msg + "Group Name should not be Empty\n";
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
   <table  id="tblScope" runat="server" cellpadding="0" cellspacing="0" width="100%" visible="false">
   <tr>
          <td colspan="2" class="pageTitle">
              Scope Grouping</td><td align="right" runat="server" id="addrow"  class="pageTitleSub">Create</td>
        </tr>
        <tr><td colspan="3"></td></tr>
        <tr>
          <td class="lblTitle">Group Name<font style="color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">:</td>
          <td><input type="text" id="txtGroup" runat="server" class="textFieldStyle" maxlength="50" style="width: 397px" /></td>
        </tr>
       <tr>
           <td class="lblTitle" valign="Top">Select Scope<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
           <td style="width: 1%" valign="Top">:</td>
           <td>
               <asp:CheckBoxList ID="chklistSelect" runat="server" RepeatLayout="Flow" CssClass="lblText">
               </asp:CheckBoxList></td>
       </tr>
        <tr>
          <td colspan="2"></td>
          <td><br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
        </tr> 
   </table>
   <br />
      <table cellpadding="0" cellspacing="0" width="100%">
       <tr >
       <td colspan="2" class="pageTitle" id="tdTitle" runat="server">Scope Grouping</td><td align="right" class="pageTitleSub">List </td>          
        </tr>        
        <tr>
          <td align="right" colspan="3"><br />
            <input type="hidden" runat="server" id="hdnSort" />
            <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;
            <input id="btnCanel" type="button" value="Cancel" runat="server" class="buttonStyle" />
            </td>
        </tr>
      </table>
      <br />
      <asp:GridView ID="grdScope" runat="server" CellSpacing="0" CellPadding="1" AllowPaging="True" EmptyDataText="No Records Found.."
        AllowSorting="True" Width="100%" AutoGenerateColumns="False">
        <PagerSettings Position="TopAndBottom" />
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="left" />
        <AlternatingRowStyle CssClass="GridEvenRows" />
        <RowStyle CssClass="GridOddRows" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
        <Columns>
          <asp:TemplateField HeaderText="&nbsp;Sno&nbsp;" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
              <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
          </asp:TemplateField>
          <asp:HyperLinkField DataTextField="GroupName" DataNavigateUrlFields="GroupName"
            DataNavigateUrlFormatString="frmCODScopeGrouping.aspx?id={0}&&Mode=E" HeaderText="Group Name" SortExpression="GroupName" />   
          
        </Columns>
      </asp:GridView>
    
    </div>
    </form>
</body>
</html>
