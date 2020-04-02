<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashboardpopupBast.aspx.vb"
    Inherits="DashboardpopupBast" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ready For BAST</title>
    <link href="../CSS/Styles.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript">
            function WindowsClose(){
                window.opener.location.href = window.opener.location.href;
                if (window.opener.progressWindow) {
                    window.opener.progressWindow.close();
                }
                window.close();
            }
            
            function Approved(siteno, version, pono){
                alert(siteno);
                window.open('../digital-sign/customer-Digital-signature.aspx?siteno=' + siteno + '&version=' + version, 'welcome3', 'width=850,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
            
            function Approve(id, siteno, version){
                window.open('../BAUT/frmTI_BASTFinal.aspx?id=' + id + '&open=1', 'welcome5', 'width=950,height=700,menubar=no,resizable=yes,status=no,location=no,toolbar=no,scrollbars=yes');
            }
    </script>
    
    <style type="text/css">
        .GridEvenRowsNew
        {
            font-family:verdana;
            font-size:11px;
            background-color:#cfcfcf;
            
        }
        .GridOddRowsNew
        {
            font-family:verdana;
            font-size:11px;
        }
        body { 
	        letter-spacing:0;
	        color:#434343;
	        background:#efefef url(images/background.png) repeat top center;
	        padding:20px 0;
	        position:relative;
	        text-shadow:0 1px 0 rgba(255,255,255,.8);
	        -webkit-font-smoothing: subpixel-antialiased;
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.corner.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("div.BoxRounded").corner();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="width: auto; margin: 0 10%; margin-top: 10px;">
            <div class="BoxRounded" style="background-image:url('../Images/barbg.jpg'); background-repeat:repeat-x; width:100%; height:30px;">
                <img alt="" src="../Images/ReadyforBAST_bar_img.jpg" />
            </div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td id="Td1" runat="server">
                        <asp:GridView ID="grdDocuments" runat="server" AllowPaging="False" EmptyDataText="All documents approved"
                            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="siteno">
                            <PagerSettings Position="TopAndBottom" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridEvenRowsNew" />
                            <RowStyle CssClass="GridOddRowsNew" />
                            <PagerStyle HorizontalAlign="Right" CssClass="PagerTitle" VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateField HeaderText=" No ">
                                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Pono" HeaderText="PoNo"/>
                                <asp:TemplateField HeaderText="Site NO">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approved('<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>,'<%# DataBinder.Eval(Container.DataItem,"PONo") %>')">
                                            <%# DataBinder.Eval(Container.DataItem,"siteno") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site NO">
                                    <ItemTemplate>
                                        <a href="#" onclick="Approve(<%# DataBinder.Eval(Container.DataItem,"sw_id") %>,'<%# DataBinder.Eval(Container.DataItem,"siteno") %>',<%# DataBinder.Eval(Container.DataItem,"version") %>)">
                                            <asp:Label ID="LblSiteNo" runat="server" Text='<%#Eval("siteno") %>' Visible='<%# Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString())) %>'></asp:Label>
                                        </a>
                                        <asp:Label ID="LblSiteNoDisabled" runat="server" Text='<%#Eval("siteno") %>' Visible='<%#IIf((Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString()))),"false","true") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="LblReadyBAST" Text='<%#Eval("ReadyBAST") %>' runat="server" Visible="false"></asp:Label>
                                        <asp:Image ID="imgGreen" runat="server" ImageUrl="~/Images/ok.jpg" Height="12px" Width="12px" Visible='<%#Convert.ToBoolean(Convert.ToInt16(Eval("ReadyBAST"))) %>' />
                                        <asp:Image ID="imgRed" runat="server" ImageUrl="~/Images/notok.jpg" Height="13px" Width="13px" Visible='<%#IIf((Convert.ToBoolean(Integer.Parse(Eval("ReadyBAST").ToString()))),"false","true") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="workpackageid" HeaderText="Work Package Id"/>
                                <asp:BoundField DataField="Scope" HeaderText="Scope"  />
                                <asp:BoundField DataField="subdate" HeaderText="Ready for BAST Date"/>
                                <asp:BoundField DataField="nodays" HeaderText="Oldest Task" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="hgap">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <input id="BtnClose" type="submit" runat="server" value="Close" 
                            style="border-width:1px; border-style:solid; border-color:White; background-color:#cfcfcf; font-family:Verdana; font-size:11px; font-weight:bolder; height:25px; width:65px;" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
