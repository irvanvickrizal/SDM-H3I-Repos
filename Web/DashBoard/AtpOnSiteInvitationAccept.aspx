<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AtpOnSiteInvitationAccept.aspx.vb"
    Inherits="DashBoard_AtpOnSiteInvitationAccept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>INVITATION PENDING</title>

    <script language="javascript" type="text/javascript" src="../Include/popcalendar.js"></script>

    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderPanel
        {
            background-color:#7f7f7f; 
            color:#ffffff;
            font-family:Verdana; 
            font-size:14px; 
            font-weight:bold; 
            padding:5px;
            width:98%;
        }
        .txtFieldStyle
        {
            height:12px;
            font-family:verdana;
            font-size:11px;
        }
        .headerGridPadding
        {
            Padding:5px;
            text-align:center;
            color:#ffffff;
        }
        .itemGridPadding
        {
            Padding:3px;
        }
    </style>

    <script type="text/javascript">
        function checkDate(sender, args)
        {
            if(sender._selectedDate < new Date())
            {
                alert("You cannot select a day earlier than today")
                sender._selectedDate = new Date()
                sender._textbox.set_Value("");
            }
        }
        
    </script>
    
    
</head>
<body>
<script type="text/javascript">
        var atLeast = 1
        function ValidateCheckList(){     
                var CHK = document.getElementById("<%=gvInvitationPending.ClientID%>"); 
                var checkbox = CHK.getElementsByTagName("input");
                var counter=0;
                for (var i=0;i<checkbox.length;i++)
                {
                    if (checkbox[i].checked)
                    {
                        counter++;
                    }
                }
                if(atLeast>counter)
                {
                    alert("Please tick at least one Site");
                    return false;
                }
                return true;
            }
    </script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
        </asp:ScriptManager>
        <div>
            <div class="HeaderPanel">
                INVITATION PENDING
            </div>
            <div style="width: 98%; margin-left: 2px; margin-top: 10px;">
                <asp:GridView ID="gvInvitationPending" runat="server" AutoGenerateColumns="false"
                    EmptyDataText="No ATP On-Site Record Pending">
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true"
                        Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkInvitation" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No.">
                            <HeaderStyle CssClass="headerGridPadding" />
                            <ItemStyle HorizontalAlign="Center" Width="2%" CssClass="itemGridPadding" />
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-CssClass="headerGridPadding"
                            ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-CssClass="headerGridPadding"
                            ItemStyle-Width="140px" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="Sitename" HeaderText="Site Name" HeaderStyle-CssClass="headerGridPadding"
                            ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PackageId" HeaderText="Package ID" HeaderStyle-CssClass="headerGridPadding"
                            ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="usernameexeatponsite" HeaderText="Requester" ItemStyle-Width="180px"
                            HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PendingDate" HeaderText="Requested Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}"
                            HeaderStyle-CssClass="headerGridPadding" ItemStyle-Width="140px" ItemStyle-CssClass="itemGridPadding"
                            HtmlEncode="false" />
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                            <ItemTemplate>
                                <asp:Label ID="LblSiteId" runat="server" Text='<%#Eval("SiteId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="LblPendingDate" runat="server" Text='<%#Eval("PendingDate") %>' Visible="false"></asp:Label>
                                <asp:TextBox ID="TxtCalendar" runat="server" CssClass="txtFieldStyle"></asp:TextBox>
                                <asp:ImageButton ID="BtnCalendar" runat="server" Height="16px" ImageUrl="~/Images/calendar_icon.jpg"
                                    Width="18px" />
                                <cc1:CalendarExtender ID="ceCalendar" runat="server" Format="dd-MMMM-yyyy" OnClientDateSelectionChanged="checkDate"
                                    PopupButtonID="BtnCalendar" TargetControlID="TxtCalendar">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="margin-top: 10px; width: 98%; text-align: right;">
                <asp:Label ID="LblSubmit" runat="server"></asp:Label>
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="buttonStyle" Width="120px" OnClientClick="return ValidateCheckList()" />
            </div>
        </div>
    </form>
</body>
</html>
