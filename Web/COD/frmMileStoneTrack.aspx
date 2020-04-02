<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMileStoneTrack.aspx.vb" Inherits="frmMileStoneTrack" %>


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
        if (IsEmptyCheck(document.getElementById("txtdesc").value) == false)
        {
            msg = msg + "Description should not be Empty\n";
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
      
         <table id="tblmilestone" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
        visible="false">
        <tr >
          <td colspan="2" class="pageTitle">
              Milestone Tracking</td><td align="right" runat="server" id="addrow" class="pageTitleSub">Create</td>
        </tr>
        <tr style="height: 5">
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td class="lblTitle">
              PoNo<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%">
            :</td>
          <td><asp:DropDownList ID="ddlpo" runat="server" Width="133px" CssClass="selectFieldStyle">
          </asp:DropDownList></td>
        </tr>
        <tr>
          <td class="lblTitle" valign="top" style="height: 20px">Tracking Description<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
          <td style="width: 1%; height: 20px;" valign="top">
            :</td>
          <td style="height: 20px">
              <asp:DropDownList ID="ddlgroup" runat="server" Width="185px" AutoPostBack="True" CssClass="selectFieldStyle">
              </asp:DropDownList></td>
        </tr>
          <tr>
              <td class="lblTitle" style="height: 29px" valign="top">
                  Default Milestone<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
              <td style="width: 1%; height: 29px" valign="top">
              </td>
              <td style="height: 29px">
                  <asp:Label ID="lbldefault" runat="server" Width="164px" CssClass="lblText" ForeColor="Red"></asp:Label></td>
          </tr>
          <tr>
              <td class="lblTitle" style="height: 15px; text-align: right;" valign="top">
                  Grouping &nbsp;Milestone<font style=" color:Red; font-size:16px"><sup> * </sup></font>
              </td>
              <td style="width: 1%; height: 15px" valign="top">:</td>
              <td style="height: 15px">
                  &nbsp;<asp:ListBox ID="lstmilestone" runat="server" Height="175px" SelectionMode="Multiple"
                      Width="126px"></asp:ListBox>
                  <asp:DropDownList ID="ddlmilestone" runat="server" Width="92px" Visible="False"></asp:DropDownList></td>
          </tr>        
          <tr>
              <td class="lblTitle" style="height: 15px; text-align: right" valign="top">
              </td>
              <td style="width: 1%; height: 15px" valign="top">
              </td>
              <td style="color: #0000cc; font-family: Arial;">
                  For multiple selection, hold Ctrl key and select the Milestones</td>
          </tr>
        <tr>
          <td colspan="2" style="height: 34px">
          </td>
          <td style="height: 34px">
            &nbsp;<br /><asp:Button ID="btnDelete" runat="server" Text="Delete" Visible="False" CssClass="buttonStyle" />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" /></td>
        </tr>
              </table>
              <br />
      <table cellpadding="0" cellspacing="0" width="100%">
        <tr class="pageTitle">
          <td colspan="2"  id="tdTitle" runat="server">
              Milestone Tracking</td><td align="right" class="pageTitleSub" style="width: 564px">List </td>
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
      <asp:GridView ID="grdmielstonetrack" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
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
              <asp:Label ID="lblno" runat="Server"></asp:Label></ItemTemplate>
          </asp:TemplateField>
          <asp:HyperLinkField DataTextField="pono" DataNavigateUrlFields="PONO,Description" DataNavigateUrlFormatString="frmMileStoneTrack.aspx?pono={0}&amp;&amp;de={1}&amp;&amp;Mode=E"
            HeaderText="PONO" SortExpression="PONO">
            <ItemStyle Width="30%" />
          </asp:HyperLinkField>
          <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
              <ItemStyle Width="30%" />
          </asp:BoundField>
            <asp:BoundField DataField="milestone" HeaderText="Grouping Milestone" />
        </Columns>
      </asp:GridView>
           
        </div>
  </form>
</body>
</html>
