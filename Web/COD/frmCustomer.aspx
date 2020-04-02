<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCustomer.aspx.vb" Inherits="COD_frmCustomer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer Page</title>
    <link href="../css/Styles.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
  <script language="javascript" type="text/javascript">
        var TotalChkBx;
        var Counter;
        
        window.onload = function()
        {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= grdCustomer.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }
        
        function HeaderClick(CheckBox)
        {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= grdCustomer.ClientID %>');
            var TargetChildControl = "chkBxSelect";
            
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            
            //Checked/Unchecked all the checkBoxes in side the GridView.
            for(var n = 0; n < Inputs.length; ++n)
                if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
                     Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        
        function ChildClick(CheckBox, HCheckBox)
        {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);
                     
            //Modifiy Counter;            
            if(CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if(Counter > 0) 
                Counter--;
                
            //Change state of the header CheckBox.
            if(Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if(Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }
        
         function viewUser()
          {
            var aa;
            aa = window.showModalDialog('../USR/frmCustomerList.aspx?SelMode=true','','width=400,height=400,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');        
            if(typeof aa != 'undefined')
            {
                document.getElementById('hdnSSId').value = aa
                var bb = aa.split('####')        
                document.getElementById('hdnSupId').value = bb[0];
                document.getElementById('txtSSName').value = bb[1];
                document.getElementById('hdncsname').value = bb[1];        
                          
                     
            }    
         }
         function CUSTOMER(id)
            {
                window.open('frmCODSubConSiteDetailsView.aspx?id='+id,'welcome','width=400,height=300,menubar=no,status=no,location=no,toolbar=no,scrollbars=yes');

            }
            
             function checkIsEmpty()    
            {
                var msg = "";
                if (IsEmptyCheck(document.getElementById("txtSSName").value) == false)
                {
                    msg = msg + "Supervisor should not be Empty\n";
                }       
                if (msg != "")
                {
                    alert("Mandatory field information : \n\n" + msg);
                    return false;
                }
                else
                {
                    return true;
                }           
            }              
    </script>

</head>
<body>
    <form id="frmCODSiteSetup" runat="server">
    <div> 
        <table id="tblSite" runat="server" border="0" cellpadding="1" cellspacing="0" width="100%">
      <tr>
        <td align="left" class="pageTitle" colspan="2">Customer Supervisor</td><td align="right" id="rowadd" runat="server" class="pageTitleSub">Create</td>
      </tr>
      <tr style="height: 5">
        <td colspan="3">
        </td>
      </tr>
      <tr>
        <td class="lblTitle">Add Supervisor</td><td style="width: 1%">:</td>
        <td><input type="text" runat="server" id="txtSSName" class="textFieldStyle" disabled="disabled" />&nbsp;
            <a id="A1" runat="server" href="#" onclick="viewUser();" class="ASmall">Select Supervisor</a>
            </td>    
      </tr>
      <tr style="height: 5">
        <td colspan="3" style="height: 18px">
        </td>
      </tr>
      <tr>
        <td colspan="2">
        </td>
        <td>
          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonStyle" />
          <input id="btnCancel" type="button" value="Cancel" runat="server" class="buttonStyle" /></td>
      </tr>
    </table>
    <table cellpadding="0" cellspacing="0" width="100%">
      <tr class="pageTitle">
        <td colspan="2"  id="tdTitle" runat="server">Site</td><td align="right"class="pageTitleSub">List</td>
      </tr>
      <tr>
        <td class="lblTitle">
          Search Site</td>
        <td style="width: 1%">
          :</td>
        <td>
          <asp:DropDownList ID="ddlSelect" runat="server" CssClass="selectFieldStyle">
            <asp:ListItem Value="Site_No">Site ID</asp:ListItem>
            <asp:ListItem Value="Site_Name">Name</asp:ListItem>            
          </asp:DropDownList>
          <asp:TextBox ID="txtSearch" runat="server" CssClass="textFieldStyle"></asp:TextBox>
          <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="goButtonStyle" /></td>
      </tr>
      <tr>
        <td class="lblTitle" style="height: 19px">
          Zone</td>
        <td style="height: 19px">
          :</td>
        <td style="height: 19px">
          <asp:DropDownList ID="DDLZone" CssClass="selectFieldStyle" runat="server" AutoPostBack="True">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right" style="height: 22px">
          <input type="hidden" runat="server" id="hdnSort" />&nbsp;
         </td>
      </tr>
    </table>
    <asp:GridView ID="grdCustomer" runat="server" AllowPaging="True" AllowSorting="True" Width="100%"
      AutoGenerateColumns="False" EmptyDataText="No Records Found">
      <PagerSettings Position="TopAndBottom" />
      <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
      <AlternatingRowStyle CssClass="GridEvenRows" />
      <RowStyle CssClass="GridOddRows" />
      <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
      <Columns>
      <asp:TemplateField HeaderText=" Total ">
          <ItemStyle HorizontalAlign="Right" Width="1%" />
          <ItemTemplate>
            <asp:Label ID="lblno" runat="Server"></asp:Label>
          </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
            <ItemTemplate>
                <input type="checkbox" id="chkBxSelect" name="chkBxSelect" runat="server" value='<%#DataBinder.Eval(Container.DataItem,"Site_ID") %>' />
                <asp:HiddenField ID="hfUserID" Value='<%#Eval("Site_ID")%>' runat="server" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            <HeaderTemplate>
                <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
            </HeaderTemplate>
        </asp:TemplateField>
        <%--<asp:HyperLinkField DataTextField="Site_No" DataNavigateUrlFields="Site_ID" DataNavigateUrlFormatString="frmCustomer.aspx?id={0}&amp;Mode=E"
          HeaderText="Site ID" SortExpression="Site_No">
            <ItemStyle Width="10%" />
        </asp:HyperLinkField>--%>
        <asp:TemplateField HeaderText="Site No" SortExpression="Site_No">
            <ItemTemplate>
                <a href="#" onclick="CUSTOMER('<%# DataBinder.Eval(Container.DataItem,"Site_ID") %>')"> <%#DataBinder.Eval(Container.DataItem, "Site_No")%>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Site_Name" HeaderText="Name" SortExpression="Site_Name" />
        <asp:BoundField DataField="Site_Desc" HeaderText="Description" SortExpression="Site_Desc" Visible="False" >
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="ZNName" HeaderText=" Zone " SortExpression="ZNName" >
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="SName" HeaderText="Supervisor" SortExpression="SName" Visible="false">
            <ItemStyle Width="15%" />
        </asp:BoundField>
      </Columns>
    </asp:GridView>
    <input type="hidden" runat="server" id="hdnSSId" />
     <input id="hdncsname" type="hidden" runat="server"/>
    <input type="hidden" runat="server" id="hdnSupId" value="0" />
    <input type="hidden" runat="server" id="hdnUserType" value="0" />
    <input type="text" runat="Server" id="txtNo" visible="false" />
    <input type="text" runat="Server" id="txtName" visible="false"  />
     <input type="text" id="txtSTDesc" runat="server"  visible="false" />
    </div>
    </form>
</body>
</html>
