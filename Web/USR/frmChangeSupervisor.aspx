<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeSupervisor.aspx.vb" Inherits="USR_frmChangeSupervisor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>User Details</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Include/Validation.js"></script>
</head>
<script language="javascript" type="text/javascript">
function checkIsEmpty()
    {
        var msg="";
        var e = document.getElementById("ddlCurSup"); 
        var strUser = e.options[e.selectedIndex].value;
        if (strUser == 0)
        {
            msg = msg + "Current Supervisor should not be Empty\n"
        }
        else
        {
            var e = document.getElementById("ddlNewSup"); 
            var strUser = e.options[e.selectedIndex].value;
            if (strUser == 0)
            {
                msg = msg + "New Supervisor should not be Empty\n"
            }
        }
        if (msg != "")
        {
            alert("Mandatory field information required \n\n" + msg);
            return false;
        }
        else
        {
            return accessConfirm();
        } 
    }
      function accessConfirm()
        {
            var r = confirm("Are you sure you want to change the supervisor?");
            if (r == true)
            {
                return true;
            }
            else{return false;}
        }  
    </script>
<body>
    <form id="form1" runat="server">
    <div style="width:71%" id="divWidth">
    <table id="tblSetup" runat="Server" cellpadding="1" border="0" cellspacing="1" width="100%" >
            <tr>
                <td class="pageTitle" colspan="4" id="rowadd">Change Supervisor</td>
            </tr>
            <tr>
                <td style="width:20%" class="lblTitle">User Type</td>
                <td style="width: 1%">:</td>
                <td><asp:DropDownList ID="ddlUsertype" runat="Server" CssClass="selectFieldStyle" Enabled="False"></asp:DropDownList>&nbsp;                
                </td>
            </tr>
        <tr>
            <td class="lblTitle">Current Supervisor<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
            <td style="width: 1%">:</td>
            <td>
                <asp:DropDownList ID="ddlCurSup" runat="Server" AutoPostBack="True" CssClass="selectFieldStyle">
                </asp:DropDownList></td>
        </tr>
            <tr>
                <td class="lblTitle">New Supervisor<font style=" color:Red; font-size:16px"><sup> * </sup></font></td>
                <td style="width: 1%">:</td>
                <td><asp:DropDownList ID="ddlNewSup" runat="Server" CssClass="selectFieldStyle"></asp:DropDownList></td>
            </tr>  
        <tr>
            <td></td>
            <td style="width: 1%"></td>
            <td><br />
                <asp:Button ID="btnSave" runat="server" CssClass="buttonStyle" Text="Save"/>
                    <asp:Button ID="BtnCancel" runat="server" CssClass="buttonStyle" Text="Cancel"/></td>
        </tr>
        </table>
        <br />
                <asp:GridView ID="grdRoleList" runat="server" AllowPaging="True" AllowSorting="True" 
                                                AutoGenerateColumns="False" DataKeyNames="sno" EmptyDataText="No Records Found"
                                PageSize="5" Width="100%">
                                <PagerSettings Position="TopAndBottom" />
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="GridEvenRows" />
                                <RowStyle CssClass="GridOddRows" />
                                <PagerStyle CssClass="PagerTitle" HorizontalAlign="Right" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Total ">
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblno" runat="Server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AREA" HeaderText="Area" />
                                    <asp:BoundField DataField="REGION" HeaderText="Region" />
                                    <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                    <asp:BoundField DataField="SITE" HeaderText="Site" />
                                   </Columns>
                            </asp:GridView>
        &nbsp; &nbsp;
     </div>
    <script type="text/javascript">
    if(screen.width==1440)
    {
      document.getElementById("divWidth").style.width=screen.width-(232+416);
    }
    else if(screen.width==1024)
    {
     document.getElementById("divWidth").style.width=screen.width-(257+64);
    }
    else
    {
        document.getElementById("divWidth").style.width=screen.width-230;
    }
    </script>
    </form>
</body>
</html>
