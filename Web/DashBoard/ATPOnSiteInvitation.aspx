<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATPOnSiteInvitation.aspx.vb"
    Inherits="DashBoard_ATPOnSiteInvitation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ATP OnSite Invitation</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderPanel
        {
            background-color:#7f7f7f; 
            color:#ffffff;
            font-family:Verdana; 
            font-size:14px; 
            font-weight:bold; 
            padding:3px;
            width:98%;
        }
        .lblSearch
        {
            font-family:verdana;
            font-size:10px;
        }
        .TxtSearch
        {
            font-family:verdana;
            font-size:11px;
            height:14px;
        }
        .AccordionTitle, .AccordionContent, .AccordionContainer
        {
          position:relative;
          width:280px;
        }

        .AccordionTitle
        {
          height:20px;
          overflow:hidden;
          cursor:pointer;
          font-family:Arial;
          font-size:8pt;
          font-weight:bold;
          vertical-align:middle;
          text-align:center;
          background-repeat:repeat-x;
          display:table-cell;
          background-image:url('title_repeater.jpg');
          -moz-user-select:none;
        }

        .AccordionContent
        {
          height:0px;
          overflow:auto;
          display:none; 
        }

        .AccordionContainer
        {
          border-top: solid 1px #cfcfcf;
          border-bottom: solid 1px #cfcfcf;
          border-left: solid 2px #cfcfcf;
          border-right: solid 2px #cfcfcf;
          
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
        .EmptyDataRowStyle
        {
            padding:5px;
            font-family:verdana;
            font-size:11px;
        }
    </style>

    <script type="text/javascript">
        var ContentHeight = 100;
        var TimeToSlide = 250.0;

        var openAccordion = '';

        function runAccordion(index)
        {
          var nID = "Accordion" + index + "Content";
          if(openAccordion == nID)
            nID = '';
            
          setTimeout("animate(" + new Date().getTime() + "," + TimeToSlide + ",'" 
              + openAccordion + "','" + nID + "')", 33);
          
          openAccordion = nID;
        }
        function animate(lastTick, timeLeft, closingId, openingId)
        {  
          var curTick = new Date().getTime();
          var elapsedTicks = curTick - lastTick;
          
          var opening = (openingId == '') ? null : document.getElementById(openingId);
          var closing = (closingId == '') ? null : document.getElementById(closingId);
         
          if(timeLeft <= elapsedTicks)
          {
            if(opening != null)
              opening.style.height = ContentHeight + 'px';
            
            if(closing != null)
            {
              closing.style.display = 'none';
              closing.style.height = '0px';
            }
            return;
          }
         
          timeLeft -= elapsedTicks;
          var newClosedHeight = Math.round((timeLeft/TimeToSlide) * ContentHeight);

          if(opening != null)
          {
            if(opening.style.display != 'block')
              opening.style.display = 'block';
            opening.style.height = (ContentHeight - newClosedHeight) + 'px';
          }
          
          if(closing != null)
            closing.style.height = newClosedHeight + 'px';

          setTimeout("animate(" + curTick + "," + timeLeft + ",'" 
              + closingId + "','" + openingId + "')", 33);
        }
    </script>
    
    <script type="text/javascript">
    var atLeast = 1
    function ValidateCheckList(){     
                var CHK = document.getElementById("<%=gvPendingATPOnSiteInvitation.ClientID%>"); 
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
                    alert("Please tick at least one ATP On-site request");
                    return false;
                }
                return true;
            }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="HeaderPanel">
                ATP OnSite Invitation Request
            </div>
            <div>
                <div id="AccordionContainer" class="AccordionContainer">
                    <div onclick="runAccordion(1);">
                        <div class="AccordionTitle" onclick="return false;">
                            Advance Search
                        </div>
                    </div>
                    <div id="Accordion1Content" class="AccordionContent">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPoNo" CssClass="lblSearch" runat="server" Text="HOT Number"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlPoNo" runat="server" CssClass="lblSearch">
                                        <asp:ListItem Text="HOT" Value="HOT"></asp:ListItem>
                                        <asp:ListItem Text="HOG" Value="HOG"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPoNo" runat="server" Width="124px" CssClass="TxtSearch"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblSiteNo" runat="server" Text="Site No" CssClass="lblSearch"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="TxtSiteNo" runat="server" Width="180px" CssClass="TxtSearch"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPackageid" runat="server" Text="PackageId" CssClass="lblSearch"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="TxtPackageId" runat="server" Width="180px" CssClass="TxtSearch"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 262px; text-align: right;">
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="buttonStyle" />
                            <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="buttonStyle" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="margin-top: 20px;width:98%;margin-left:2px;">
                <asp:GridView ID="gvPendingATPOnSiteInvitation" runat="server" AutoGenerateColumns="false" EmptyDataText="No ATP On Site Request">
                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridEvenRows" />
                    <RowStyle CssClass="GridOddRows" />
                    <EmptyDataRowStyle ForeColor="red" Font-Names="Verdana" Font-Size="10pt" Font-Bold="true" Height="25px" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <asp:BoundField DataField="SiteNo" HeaderText="Site No" HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PoNo" HeaderText="PO Number" HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="Sitename" HeaderText="Site Name" HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PackageId" HeaderText="Package ID" HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="usernameexeatponsite" HeaderText="Requester" ItemStyle-Width="180px"
                            HeaderStyle-CssClass="headerGridPadding" ItemStyle-CssClass="itemGridPadding" />
                        <asp:BoundField DataField="PendingDate" HeaderText="Requested Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" 
                            HeaderStyle-CssClass="headerGridPadding" ItemStyle-Width="140px" ItemStyle-CssClass="itemGridPadding"
                            HtmlEncode="false" />
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="140px" />
                            <ItemTemplate>
                                <asp:DropDownList ID="DdlDecision" runat="server" CssClass="lblSearch">
                                    <asp:ListItem Text="-- Select Decision --" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="LblSiteId" runat="server" Text='<%#Eval("Siteid") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="width:98%; text-align:right;margin-top:15px;">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="buttonStyle" Width="120px" 
                    OnClientClick="return ValidateCheckList()" />
            </div>
        </div>
    </form>
</body>
</html>
