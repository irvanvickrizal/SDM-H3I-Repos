<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmUSRHelp.aspx.vb" Inherits="CR_frmUSRHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
                <tr class="pageTitle">
                    <td colspan="3">
                        Users</td>
                </tr>
                <tr>
                    <td class="lblTitle">
                        User Type</td>
                    <td style="width: 1%">
                        :</td>
                    <td>
                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle" AutoPostBack="true">
                            <asp:ListItem Value="N" Text="NSN"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Sub Con"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Customer"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <asp:GridView ID="grdUsers" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="false" Width="100%">
                            <HeaderStyle CssClass="GridHeader" />
                            <RowStyle CssClass="GridOddRows" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <PagerSettings Position="topAndBottom" />
                            <PagerStyle CssClass="PagerTitle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total " ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="25%">
                                    <HeaderTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td valign="top">
                                                    <input id="checkAll" onclick="CheckAll(this);" type="checkbox" name="checkAll" value="0">
                                                    Select All</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input id="EmpId" onclick="CheckChanged();" type="checkbox" name="EmpId1" runat="server"
                                            value='<%# DataBinder.Eval(Container.DataItem,"Usr_Id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="EPM_Id" HeaderText="EPM Id" SortExpression="EPM_Id" />
                                <asp:BoundField DataField="Email" HeaderText="Mail Address" SortExpression="Email" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <br />
                        <input type="button" runat="server" id="btnAdd" class="buttonStyle" value="Add Selected Users"
                            style="width: 109pt" />&nbsp;<input type="button" runat="server" id="btnClose" class="buttonStyle"
                                value="Close" /></td>
                </tr>
            </table>
            <input type="hidden" runat="server" id="hdnsort" />
            <input type="hidden" runat="server" id="hdnName" />
        </div>
    </form>
</body>
</html>
