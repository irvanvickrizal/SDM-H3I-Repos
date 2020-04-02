<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMissingWFDoc.aspx.vb" Inherits="PO_frmMissingWFDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Missing WfDoc</title>
   <base target="_self">
   </base> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr  class="pageTitle">
                <td>
                    Process Flow not Attached for Following Documents</td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:GridView ID="grdwfmisdoc" runat="server" EmptyDataText="No Records">
                        <RowStyle CssClass="GridOddRows" />
                        <HeaderStyle CssClass="GridHeader" />
                        <AlternatingRowStyle CssClass="GridEvenRows" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td ><br />
                    <asp:Button ID="btnOK" runat="server" Text="OK" CssClass="buttonStyle" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
