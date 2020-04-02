<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteReMapList.aspx.vb" Inherits="PO_frmSiteReMapList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="../CSS/Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
     <table cellpadding="0" cellspacing="0" width="100%">
            <tr class="pageTitle">
                <td colspan="3" runat="server" id="rowTitle" style="height: 23px">Site Remapping List</td>
            </tr>
            <tr>
                <td class="lblTitle" style="width:10%">Search</td>
                <td style="width:1%">:</td>
                <td><asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle"><asp:ListItem Value="SiteNo">SiteId</asp:ListItem></asp:DropDownList>&nbsp;
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
            </tr>  
            <tr><td class="lblTitle">Select PONo</td><td style="height: 21px">:</td><td><asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">                
            </asp:DropDownList></td></tr>    
            <tr style="height:5px"></tr>      
            <tr>
                <td colspan="3">
                    <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" EmptyDataText="No Records Found">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
                <asp:TemplateField HeaderText=" Total ">
                    <ItemTemplate><%# container.DataItemIndex + 1 %>.
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                </asp:TemplateField>
                <asp:BoundField DataField="SiteNo" HeaderText="SiteId" /> 
                <asp:BoundField DataField="PoNo" HeaderText="PO No" />
                <asp:BoundField DataField="Scope" HeaderText="Scope" />   
                <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" />
               <asp:BoundField DataField="WorkPkgName" HeaderText="WorkPackageName" /> 
                <asp:BoundField DataField="oldsiteno" HeaderText="CancelledSiteId" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                <asp:BoundField DataField="Band" HeaderText="Band" />
                <asp:BoundField DataField="AntennaName" HeaderText=" Antenna Name" />  
               
           </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
        
    </form>
</body>
</html>
