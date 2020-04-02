<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMessageBoard.aspx.vb" Inherits="frmMessageBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>message Board</title>
   <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>  
</head>
<script language="javascript" type="text/javascript">
        function checkIsEmpty()    
        {
            var msg="";                       
            if (IsEmptyCheck(document.getElementById("txtTitle").value) == false)
            {
                msg = msg + "Title should not be empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtMessage").value) == false)
            {
                msg = msg + "Message should not be empty\n"
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
  
                   <table id="tblSetup" runat="server" border="0" cellpadding="1" cellspacing="1" width="100%">
                   <tr class="pageTitle">
                    <td colspan="3">
                        <strong>Message Board List</strong></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdMessageBoard" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" width="100%" DataKeyNames="MsgId" 
                             AutoGenerateColumns="False" EmptyDataText="No Records Found" PageSize="5" >
                            <PagerSettings Position="TopAndBottom" />
                            <AlternatingRowStyle CssClass="GridOddRows" />
                            <RowStyle CssClass="GridEvenRows" /> 
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px" VerticalAlign="Middle"/>
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:HyperLinkField HeaderText="Title" DataTextField="Title" DataNavigateUrlFields="MsgId"  DataNavigateUrlFormatString = "frmMessageBoard.aspx?ID={0}" SortExpression="Title"/>                                   
                                <asp:BoundField DataField="PostedBy" HeaderText="PostedBy" SortExpression="PostedBy" />                             
                               
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                        </asp:GridView>
                    </td>
                </tr>
            <tr class="pageTitle">
                <td align="left" colspan="4">Message Board</td>
            </tr>
                       <tr>
                           <td class="lblTitle" style="height: 26px;">
                               Title</td>
                           <td style="height: 26px">
                               <asp:TextBox ID="txtTitle" runat="server" CssClass="textFieldStyle"></asp:TextBox></td>
                       </tr>
            <tr valign="top">
                <td class="lblTitle">Message</td>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" Height="175px" TextMode="MultiLine" Width="391px" CssClass="textFieldStyle"></asp:TextBox></td>
            </tr>
            
                       <tr>
                           <td class="lblTitle">
                           </td>
                           <td style="height: 21px">
                               <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="buttonStyle" Width="75px" />&nbsp;
                               <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonStyle" Width="75px" /></td>
                       </tr>                       
        </table>
         <input id="hdnSort" type="hidden" runat="server" />
         </form>
</body>
</html>
