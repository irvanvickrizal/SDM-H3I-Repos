<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmsitehelp.aspx.vb" Inherits="frmsitehelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CheckList Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <base target="_self" />

    <script language="javascript" type="text/javascript">
        function CheckAll( chb )
		{                                             
		    var frm = document.form1; 
		    var ChkState=chb.checked;
		    for(i=0;i< frm.length;i++)
		    { 
		        e=frm.elements[i];
		        if(e.type=='checkbox' && e.name.indexOf('Id') != -1)
		            e.checked= ChkState;
		    }
		}    
		function CheckChanged()
		{ 
		    var frm = document.form1;
		    var boolAllChecked;
		    boolAllChecked=true;                                       
		    for(i=0;i< frm.length;i++)                                 
		    {                                                                 
		        e=frm.elements[i];                                        
		        if( e.type=='checkbox' && e.name.indexOf('Id') != -1 )
		            if(e.checked== false)                                  
		            {                                                             
		                boolAllChecked=false;                               
		                break;                                                    
		            }                                                              
		      }                                                                  
		      for(i=0;i< frm.length;i++)                                  
		      {                                                                  
		        e=frm.elements[i];                                         
		        if ( e.type=='checkbox' && e.name.indexOf('checkAll') != -1 )
		        {                                                            
		          if( boolAllChecked==false)                         
		             e.checked= false ;                                
		             else                                                    
		             e.checked= true;                                  
		          break;                                                    
		        }                                                             
		       }                                                              
		 }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td class="lblTitle" style="width: 20%">
                        Search</td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
                            <asp:ListItem Value="SiteNo">SiteId</asp:ListItem>
                            <asp:ListItem Value="Scope">Scope</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle" Height="17px"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonStyle" Width="33px" /></td>
                </tr>
                <tr>
                    <td class="lblTitle" style="width: 10%">
                        Po No<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                    </td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <asp:DropDownList ID="ddlPO" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                        </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnok" runat="server" Text="Submit"
                            Visible="false" CssClass="buttonStyle" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdPOrawdata" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="true" AllowSorting="true">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle CssClass="PagerTitle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " ItemStyle-HorizontalAlign="right" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <%# container.DataItemIndex + 1 %>
                                        .
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="10%">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td valign="top">
                                                    <input id="checkAll" onclick="CheckAll(this);" type="checkbox" name="checkAll" value="0">Select
                                                    All</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="chkId" onclick="CheckChanged();" type="checkbox" name="chkId" runat="server"
                                            value='<%# DataBinder.Eval(Container.DataItem,"PO_Id") %>' /></ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PO_Id" HeaderText="POId" />
                                <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" SortExpression="SiteNo" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" SortExpression="Scope" />
                                <asp:BoundField DataField="workpkgID" HeaderText="WorkPackageID" SortExpression="WorkpkgId" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <input type="hidden" runat="server" id="hdnDisp" value="1" />
            <input type="hidden" runat="server" id="hdnSort" value="" />
        </div>
    </form>
</body>
</html>
