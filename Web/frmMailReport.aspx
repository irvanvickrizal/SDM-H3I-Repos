<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMailReport.aspx.vb" Inherits="MailReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Mail Report</title>
   <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
   <script language="javascript" type="text/javascript" src="Include/Validation.js"></script>  
</head>
 <script language="javascript" type="text/javascript">
        function checkIsEmpty()    
        {
            var msg="";   
            var e = document.getElementById("ddlmailtype"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Mail Type should not be Empty\n";
            }   
            if (IsEmptyCheck(document.getElementById("txtsalutation").value) == false)
            {
                msg = msg + "Salutation should not be Empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtsubject").value) == false)
            {
                msg = msg + "Subject should not be Empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtbody").value) == false)
            {
                msg = msg + "Body should not be Empty\n"
            } 
            if (IsEmptyCheck(document.getElementById("txtclosing").value) == false)
            {
                msg = msg + "Closing should not be Empty\n"
            }
            var e = document.getElementById("DDLFrequency"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "Frequency should not be Empty\n";
            }  
            var e = document.getElementById("DDLFrequency"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser !=1)
            {
               if (IsEmptyCheck(document.getElementById("txtHr").value) == false)
                {
                    msg = msg + "Frequency Hours should not be Empty\n"
                }     
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
    <div>
        <table id="tblMail" runat="server" border="0" cellpadding="1" cellspacing="0" width="100%" visible="false">
            <tr>
                <td  class="pageTitle" align="left" colspan="2">Mail Template</td><td id="add" runat="server" align="right" class="pageTitleSub">Create</td>
            </tr>
                       <tr>
                           <td class="lblTitle" width="10%">Mail Type<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                            <td width="1%">:</td> 
                           <td>
                               <asp:DropDownList ID="ddlmailtype" runat="server" CssClass="selectFieldStyle"> 
                               <asp:ListItem Value="0">--Select--</asp:ListItem>                                                    
                               <asp:ListItem Value="1">Ready For BAUT</asp:ListItem>
                               <asp:ListItem Value="2">Request For Change Order</asp:ListItem>
                               <asp:ListItem Value="3">Documents Waiting For User Task</asp:ListItem>
                               <asp:ListItem Value="4">Documents Rejected</asp:ListItem> 
                                   <asp:ListItem Value="5">Documents Approved</asp:ListItem>
                           </asp:DropDownList></td></tr>            
                
            <tr>
                <td class="lblTitle">Salutation<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td>:</td>                    
                <td>
                    <asp:TextBox ID="txtsalutation" runat="Server"  CssClass  ="textFieldStyle" Width="174px" ></asp:TextBox></td>                    
            </tr>            
            <tr>
                <td class="lblTitle">Subject<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td >:
                   </td>                 
                <td>
                   <asp:TextBox ID="txtsubject" runat="Server" CssClass  ="textFieldStyle" Width="429px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="lblTitle" valign="top">Body<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td valign="top">:</td>
                <td>
                    <asp:TextBox ID="txtbody" runat="Server"  CssClass  ="textFieldStyle" Height="65px" TextMode="MultiLine" Width="429px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="lblTitle">Closing<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txtclosing" runat="Server"  CssClass  ="textFieldStyle"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="lblTitle">Frequency<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td>:
                    </td>
                <td>
               <asp:DropDownList ID="DDLFrequency" runat="server" CssClass="selectFieldStyle" AutoPostBack="True">
                   <asp:ListItem  Value="0">--Select--</asp:ListItem>
                   <asp:ListItem Value="1">Every Upload</asp:ListItem>
                   <asp:ListItem Value="2"> Hourly</asp:ListItem>
                   <asp:ListItem Value="3">Accumulative/Hour</asp:ListItem>
               </asp:DropDownList></td>
            </tr>            
            <tr id="Hours" runat="server" >
                <td class="lblTitle" style="height: 25px">FrqHours<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="height: 25px" >:
               </td>
                <td style="height: 25px">
               <input type="text" id="txtHr" runat="server" class="textFieldStyle" maxlength="2" onkeypress="javascript:return allowKeyAcceptsSingleDot('0123456789.');"/></td>
                
            </tr>           
            <tr runat="server">
                <td class="lblTitle">
                </td>
                <td>
                </td>
                <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle"/>&nbsp;
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonStyle"/> 
                </td>
            </tr>  
            </table><br /> 
            <table id="tblList" runat="server" border="0" cellpadding="1" cellspacing="0" width="100%">       
    
                 <tr>
                   <td colspan="2" class="pageTitle" id="td1" runat="server">Mail Template</td><td align="right" class="pageTitleSub">List</td>    
                </tr>
               <tr>
              <td align="right" colspan="3">            
                 <input id="btnNewGroup" type="button" value="Create" runat="server" class="buttonStyle" />&nbsp;               
               </td>
            </tr>              

                <tr>
                    <td colspan="3">
                        <asp:GridView ID="grdMailReport" runat="server" CellPadding="1" AllowPaging="True" AllowSorting="True" Width="100%" DataKeyNames="Sno" 
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
                                <asp:HyperLinkField HeaderText="Mail Type" DataTextField="MailType" DataNavigateUrlFields="Sno"  DataNavigateUrlFormatString = "frmMailReport.aspx?ID={0}" SortExpression="MailType"/>                                  
                                
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                        </asp:GridView>
                    </td>
                </tr>   
               </table>         
        
        <input id="hdnSort" type="hidden" runat="server" />   
    </div>
        &nbsp;
    </form>
</body>
</html>
