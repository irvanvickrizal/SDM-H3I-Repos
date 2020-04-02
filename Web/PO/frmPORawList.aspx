<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPORawList.aspx.vb" Inherits="PO_frmPORawList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%">
            <PagerSettings Position="TopAndBottom" />
            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="GridEvenRows" />
            <RowStyle CssClass="GridOddRows" />
            <PagerStyle CssClass="PagerTitle" Font-Names="Verdana" Font-Size="8pt" Height="5px"
                HorizontalAlign="Right" VerticalAlign="Middle" />
            <Columns>
               <asp:HyperLinkField DataNavigateUrlFields="SiteNo,Sno" DataNavigateUrlFormatString="frmPODetails.aspx?id={0}&Sno={1}&TT=R" HeaderText="SiteName" DataTextField="SiteName" />
                 <asp:BoundField DataField="PONo" HeaderText="PONo" />
                <%--<asp:BoundField DataField="Description" HeaderText="Description" />--%>
                <asp:BoundField DataField="Band_Type" HeaderText="Band Type" />
               <asp:BoundField DataField="Band" HeaderText="Band" />
              <%--<asp:BoundField DataField="TowerType" HeaderText="Tower Type" />  --%>
              <asp:BoundField DataField="AntennaName" HeaderText=" Antenna Name" />  
              <asp:BoundField DataField="Config" HeaderText="Configuration" />
              <asp:BoundField DataField="BSSHW" HeaderText="Hardware" />
            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                          </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
