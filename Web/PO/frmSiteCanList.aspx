<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSiteCanList.aspx.vb" Inherits="PO_frmSiteCanList" %>

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
                <td colspan="3" runat="server" id="rowTitle">
                    Site Cancellation List</td>
            </tr>
            <tr>
                <td class="lblTitle" style="width:20%">Search</td>
                <td style="width:1%">:</td>
                <td><asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle"><asp:ListItem Value="p.SiteNo">SiteId</asp:ListItem></asp:DropDownList>&nbsp;
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Go" /></td>
            </tr>  
            <tr><td class="lblTitle">Select PONo</td><td>:</td><td><asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">                
            </asp:DropDownList>
                <input id="btnClose" runat="server" class="buttonStyle" type="button" value="Do Cancel" /></td>
                </tr>                   
             
            <tr>
                <td colspan="3" style="width:100%"><br />
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
               <asp:BoundField DataField="PoNo" HeaderText="PO No" />
               <asp:BoundField DataField="Po_Id" HeaderText="PO Id" />
                <asp:BoundField DataField="SiteNo" HeaderText="SiteId" />
                <asp:BoundField DataField="Scope" HeaderText=" Scope" />  
                <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" />
               <asp:BoundField DataField="WorkPackageName" HeaderText="WorkPackageName" /> 
                <asp:BoundField DataField="rsite" HeaderText="ReMappedSiteId" />
                <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                <asp:BoundField DataField="Band" HeaderText="Band" />
                <asp:BoundField DataField="AntennaName" HeaderText=" Antenna Name" />              
               
           </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
        &nbsp;        
    </form>
</body>
</html>
