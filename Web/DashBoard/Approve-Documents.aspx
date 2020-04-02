<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Approve-Documents.aspx.vb"
    Inherits="DashBoard_Approve_Documents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

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
		 function CheckMultiValueDelete(oSrc, args)
		{
		var frm = document.form1; 
		var id1=0;
		var checknull=0;
		var varcheck=0;		
		for(i=0;i< frm.length;i++)
		{ 
			e=frm.elements[i];
			if(e.checked )
			{
			    if( e.type=='checkbox' && e.name.indexOf('Id') != -1 )
		        {
				    checknull=checknull+1;	
				}				
			}
			else
			{
				varcheck=0;
			}
		}
		if(varcheck==0 && checknull==0)
		{
		   
		    args.IsValid = false;
			 return false;
		}		 
	}
	function ViewDocument(id,tskid,swid)
    {

     window.open('../digital-sign/Digital-signature-multi.aspx?tskid='+tskid+'&id='+id+'&swid='+swid,'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');

      //  window.open('../PO/frmViewDocument.aspx?id='+id,'welcome3','width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');

    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 98%">
                <tr>
                    <td>
                        <asp:Button ID="BtnApprovel" runat="server" CssClass="buttonStyle" Text="View All"
                            Width="115px" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="DLFirst" runat="server" AllowPaging="True" EmptyDataText="All documents approved"
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="pono">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="GridEvenRows" />
                            <RowStyle CssClass="GridOddRows" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" Total ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblno" runat="Server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pono" HeaderText="PoNo" />
                                <asp:ButtonField CommandName="Delete" DataTextField="SiteNo" HeaderText="Site No"
                                    CausesValidation="True" />
                                <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                <asp:TemplateField HeaderText="No of Pending Documents">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPending" runat="Server" Text='<%# DataBinder.Eval(Container.DataItem,"PendigDocumentCount") %>'></asp:Label>
                                        <asp:HiddenField ID="HdSiteid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"siteid") %>' />
                                        <asp:HiddenField ID="HDVersion" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"version") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td runat="server" style="height: 20px">
                        <asp:HiddenField ID="HDVersionSelect" runat="server" />
                        <asp:HiddenField ID="HDSiteSelect" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td runat="server" id="BindValue">
                        <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="grdDocuments" runat="server" AllowPaging="True" EmptyDataText="All documents approved"
                                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sno">
                                        <PagerSettings Position="TopAndBottom" />
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                        <AlternatingRowStyle CssClass="GridEvenRows" />
                                        <RowStyle CssClass="GridOddRows" />
                                        <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderText=" Total ">
                                                <ItemStyle HorizontalAlign="Right" Width="2%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnoSec" runat="Server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Siteno" HeaderText="Site NO" />
                                            <asp:BoundField DataField="Scope" HeaderText="Scope" />
                                            <asp:TemplateField HeaderText="Document">
                                                <ItemTemplate>
                                                    <a href="#" onclick="ViewDocument(<%# DataBinder.Eval(Container.DataItem,"sno") %>,<%# DataBinder.Eval(Container.DataItem,"taskid") %>,<%# DataBinder.Eval(Container.DataItem,"SiteDocId") %>)">
                                                        <%# DataBinder.Eval(Container.DataItem,"DocumentName") %>
                                                    </a>
                                                    <asp:HiddenField ID="HDSno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"sno") %>' />
                                                    <asp:HiddenField ID="HdDocName" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"DocumentName") %>' />
                                                    <asp:HiddenField ID="HdTaskid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"taskid") %>' />
                                                    <asp:HiddenField ID="HDDocPath" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"docpath") %>' />
                                                    <asp:HiddenField ID="HDXVal" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Xval") %>' />
                                                    <asp:HiddenField ID="HDYVal" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"YVal") %>' />
                                                    <asp:HiddenField ID="HDPage" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PageNo") %>' />
                                                               <asp:HiddenField ID="hdpono" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"pono") %>' />
                                                    <asp:HiddenField ID="hddocid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"documentid") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UploadedDate" HeaderText="Uploaded Date" />
                                            <asp:TemplateField>
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
                                                        value='<%# DataBinder.Eval(Container.DataItem,"sno") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" cellpadding="1" cellspacing="2" style="width: 100%">
                                        <tr>
                                            <td colspan="2" style="height: 20px;" class="pageTitle">
                                                Digital Signature Login</td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">
                                                User Name<font style="color: Red; font-size: 16px"><sup> * </sup></font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textFieldStyle" ReadOnly="True"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                                                    ErrorMessage="Enter User Name" ValidationGroup="vgSign">*</asp:RequiredFieldValidator><asp:LinkButton
                                                        ID="lnkrequest" runat="server">Request Password</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td class="lblTitle">
                                                Password <font style="color: Red; font-size: 16px"><sup>* </sup></font>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textFieldStyle"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                    ErrorMessage="Enter Password" ValidationGroup="vgSign">*</asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="You Should Select The Item"
                                                    ClientValidationFunction="CheckMultiValueDelete" ControlToValidate="txtUserName"
                                                    ValidationGroup="vgSign">*</asp:CustomValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="BtnSign" runat="server" Text="Sign" ValidationGroup="vgSign" Width="150px"
                                                    CssClass="buttonStyle" />&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="vgSign" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
