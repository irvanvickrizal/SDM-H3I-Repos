<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeRequest.aspx.vb" Inherits="frmChangeRequest" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript"  >
        function MOM(id)
            {
                window.open('frmMOMDetailsView.aspx?id='+id,'welcome','width=750,height=600,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

            }
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="1" cellspacing="1" style="width: 100%">
            <tr class="pageTitle"><td colspan="2">Change Request List</td></tr>
            <tr>
              <td class="lblTitle" style="width:20%">Search MOM Ref No.</td>
                <td><asp:DropDownList ID="ddlselect" runat="server" CssClass="selectFieldStyle">
                <asp:ListItem Value="MOMRefNo">MOM Ref No</asp:ListItem>
                <asp:ListItem Value="Subject">Subject</asp:ListItem>
                <asp:ListItem Value="Location">Location</asp:ListItem>
          </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
          <asp:Button ID="BtnSearch" runat="server" Text="GO" CssClass="goButtonStyle" /></td>                
                 
              </tr>
              <tr><td class="lblTitle">Status</td><td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                <asp:ListItem Value="N" Text="Change Order"></asp:ListItem>        
                  <asp:ListItem Value="P">Pending Acknowledgement</asp:ListItem>
                <asp:ListItem Value="C" Text="Pending for Cost Impact"></asp:ListItem>        
                  <asp:ListItem Value="I">Completed Cost Impact</asp:ListItem>
                  <asp:ListItem Value="A">Pending for Approval</asp:ListItem>
                  <asp:ListItem Value="R">Generate CR</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <asp:Button ID="btnSndApproval" runat="server" Visible="false" CssClass="buttonStyle" Text="Send for Approval" Width="130px" />
                <asp:Button ID="generateCR" runat="server" CssClass="buttonStyle" Text="Generate CR" Width="96px" />
                </td>               
                </tr>
                       
              <tr visible="false" id="refNo" runat="server">
                <td class="lblTitle" visible="false">MOM Reference No.</td>
                <td visible="false"><asp:DropDownList ID="ddlMomRefNo" runat="server" CssClass="selectFieldStyle" AutoPostBack="false">
                    </asp:DropDownList>
                    <input id="hdnSort" type="hidden" runat="server" />&nbsp;
                </td>
               
              </tr>
              </table>
              <br />
              <table cellpadding="0" cellspacing="1" style="width: 100%">                           
              <tr>
                        <td colspan="2">
                            <asp:GridView ID="GrdMOM" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" EmptyDataText="No Records Found" Width="100%" DataKeyNames="MOM_ID">
                                <PagerSettings Position="TopAndBottom" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle CssClass="PagerTitle " />
                                <HeaderStyle CssClass="GridHeader" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Total ">
                                            <ItemStyle HorizontalAlign="Right" Width="1%" />
                                                <ItemTemplate>
                                                     <asp:Label ID="lblno" runat="Server"></asp:Label>
                                                 </ItemTemplate>
                                        </asp:TemplateField>
                                    <%--<asp:BoundField DataField="MOM_ID" Visible="false" SortExpression="MOM_ID" />--%>                                                       
                                   <asp:BoundField HeaderText="MOM Ref No" DataField="MOMRefNo" SortExpression="MOMRefNo" />
                                   
                                    <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" SortExpression="MOMRefNo" DataTextField="MOMRefNo" 
                                           DataNavigateUrlFormatString="frmGenerateMOM.aspx?id={0}&Type=G" HeaderText="MOM Ref No">
                                    <ItemStyle Width="15%" />
                                  </asp:HyperLinkField>
                                   <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmPendingforCostImpact.aspx?id={0}"
                                    DataTextField="MOMRefNo" SortExpression="MOMRefNo" HeaderText="MOM Ref No">
                                    <ItemStyle Width="15%" />
                                  </asp:HyperLinkField>
                                  <asp:HyperLinkField DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmGenerateMOM.aspx?id={0}&Type=AR"
                                    DataTextField="MOMRefNo" SortExpression="MOMRefNo" HeaderText="MOM Ref No">
                                    <ItemStyle Width="15%" />
                                  </asp:HyperLinkField>                                  
                                  
                                  <asp:BoundField DataField="MomWriter" HeaderText="Writer" SortExpression="MomWriter" />
                                  <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                  <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                                  <asp:BoundField DataField="Task" HeaderText = "Status" SortExpression="Status" />                                 
                                    <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="MOM_ID" DataNavigateUrlFormatString="frmMOM.aspx?id={0}&Type=E">                                    
                                  </asp:HyperLinkField> 
                                                                   
                                  <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkMOM" runat="server" CausesValidation="False" CommandName="edt"
                                        OnClientClick="javascript:return confirm('Are you sure you want to Generate');">Generate</asp:LinkButton>                                        
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                   
                                         <%--<asp:TemplateField HeaderText="MOM Ref No">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkMomref" runat="server" Text='<%#Container.DataItem("MOMRefNo") %>'>MOM Ref No</asp:LinkButton>
                                    </ItemTemplate>
                                   </asp:TemplateField>--%>
                            </Columns>            
                    </asp:GridView>
                        </td>
                    </tr>
        </table>
    </div>
    </form>
</body>
</html>
