<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmshowsites.aspx.vb" Inherits="frmshowsites" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Search Site</title>
 <link href="CSS/styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>
  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
    
  <script language="javascript" type="text/javascript">
   
     function PopSiteSearch()
   {       
        var w = 700, h = 550;
        if (document.all || document.layers)
        {
            w = screen.availWidth;
            h = screen.availHeight;
        }
        var popW = 650, popH = 550;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        var URL = "../frmshowReason.aspx"
        window.open(URL,'',"toolbar=0,status=0,menubar=0,resizable=1,scrollbars=1,Width="+popW+",Height="+popH+",left="+leftPos+",top="+topPos);  
//        window.showModalDialog(URL,'',"toolbar=0,status=0,menubar=0,resizable=1,scrollbars=1,Width="+popW+",Height="+popH+",left="+leftPos+",top="+topPos);         
    }
  </script>

</head>
<body>
  <form id="form1" runat="server">
    <div>
        <table id="tblDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
            width="100%">
            <tr>
                <td class="pageTitle" colspan="2">
                    Sites</td>
                <td id="addrow" runat="server" align="right" class="pageTitleSub">
                </td>
            </tr>
            <tr>
                <td colspan="3">
      <asp:GridView ID="grdSite" runat="server" CellPadding="1" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SNO" >
        <PagerSettings Position="TopAndBottom" />
        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
        <AlternatingRowStyle CssClass="GridEvenRows" />
        <RowStyle CssClass="GridOddRows" />
        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
        <Columns>
             <asp:TemplateField HeaderText="SNO" SortExpression="SNO" >
                    <ItemTemplate>
                        <asp:Label ID="lblno" Text='<%# Container.DataItemIndex + 1 %>' runat="Server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" Width="2%" />
                </asp:TemplateField>
             <asp:BoundField HeaderText="siteID" DataField="siteID" SortExpression="siteID"  />
      <%--  <asp:HyperLinkField DataNavigateUrlFields="sno" DataNavigateUrlFormatString="frmSiteSearch.aspx?id={0}"
                                    DataTextField="siteID" HeaderText="SITE NO" SortExpression="SITEID" />--%>
            <asp:BoundField HeaderText="Reason"  DataField="Reason" SortExpression="Reason" />
            <asp:BoundField HeaderText="Remark" DataField="Remark" SortExpression="Remark" />
            <asp:BoundField HeaderText="AddRemarks" DataField="AddRemarks" SortExpression="AddRemarks" />
             <asp:BoundField HeaderText="noofdays" DataField="noofdays" SortExpression="noofdays" />
        </Columns>
      </asp:GridView>
                    <br />
                    <input id="btnBack" runat="server" class="buttonStyle" type="button" value="Back" /></td>
            </tr>
        </table>
      <br />
        &nbsp;&nbsp;
    </div>
  </form>
</body>
</html>
